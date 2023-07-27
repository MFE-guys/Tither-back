
using System.Reflection;
using FluentValidation.Results;
using FluentValidation;
using MediatR;
using OperationResult;
using Tither.Shared.Exceptions;
using Tither.Shared.Validators;

namespace Tither.Domain.Pipelines
{
    public sealed class FailFastRequestBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
           where TRequest : IValidatable
    {
        private readonly IEnumerable<IValidator> _validators;
        private readonly MethodInfo _operationResultError;
        private readonly Type _type = typeof(TResponse);
        private readonly Type _typeOperationResultGeneric = typeof(Result<>);
        private readonly Type _typeOperationResult = typeof(Result);

        public FailFastRequestBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;

            if (_type.IsGenericType)
            {
                _operationResultError = _typeOperationResult.GetMethods().FirstOrDefault(m => m.Name == "Error" && m.IsGenericMethod);
                _operationResultError = _operationResultError.MakeGenericMethod(_type.GetGenericArguments().First());
            }
        }

        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            TitherValidationException validationError;
            List<ValidationFailure> errors;

            if (_type == _typeOperationResult)
            {
                errors = GetErrors(request);
                if (errors?.Any() != true)
                    return next?.Invoke();

                validationError = new TitherValidationException(errors.GroupBy(v => v.PropertyName, v => v.ErrorMessage).ToDictionary(v => v.Key, v => v.Select(y => y)));
                return Task.FromResult((TResponse)Convert.ChangeType(Result.Error(validationError), _type));
            }

            if (!_type.IsGenericType || _type.GetGenericTypeDefinition() != _typeOperationResultGeneric)
                return next?.Invoke();

            errors = GetErrors(request);

            if ((bool)!errors?.Any())
                return next?.Invoke();

            validationError = new TitherValidationException(errors.GroupBy(v => v.PropertyName, v => v.ErrorMessage).ToDictionary(v => v.Key, v => v.Select(y => y)));
            
            return Task.FromResult((TResponse)Convert.ChangeType(_operationResultError.Invoke(null, new object[] { validationError }), _type));
        }

        private List<ValidationFailure> GetErrors(TRequest request)
        {
            var context = new ValidationContext<TRequest>(request);
            var validationResults = _validators
                .Select(v => v.Validate(context))
                .ToList();

            return validationResults
                .SelectMany(result => result.Errors)
                .Where(failure => failure != null)
                .ToList();
        }
    }
}
