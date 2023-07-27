using System.Reflection;
using MediatR;
using OperationResult;
using Serilog;
using Tither.Shared.Validators;

namespace Tither.Domain.Pipelines
{
    public sealed class ExceptionPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IValidatable
    {
        private readonly MethodInfo _operationResultError;
        private readonly Type _type = typeof(TResponse);
        private readonly Type _typeOperationResult = typeof(Result);
        private readonly ILogger _logger;

        public ExceptionPipelineBehavior(ILogger logger)
        {
            if (_type.IsGenericType)
            {
                _operationResultError = _typeOperationResult.GetMethods().FirstOrDefault(m => m.Name == "Error" && m.IsGenericMethod);
                _operationResultError = _operationResultError.MakeGenericMethod(_type.GetGenericArguments().First());
            }
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                return await next().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error on request handling");
                return _type == _typeOperationResult
                  ? (TResponse)Convert.ChangeType(Result.Error(ex), _type)
                  : (TResponse)Convert.ChangeType(_operationResultError.Invoke(null, new object[] { ex }), _type);
            }
        }
    }
}
