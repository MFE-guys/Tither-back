using MediatR;
using Microsoft.AspNetCore.Mvc;
using OperationResult;
using Tither.Shared.Exceptions;

namespace Tither.Api.Controllers
{
    public partial class TitherControllerBase : ControllerBase
    {
        private readonly IMediator _mediator;

        public TitherControllerBase(IMediator mediator)
            => _mediator = mediator;

        protected async Task<IActionResult> SendRequest<T>(IRequest<Result<T>> request)
            => await _mediator.Send(request).ConfigureAwait(false) switch
            {
                (true, var result, _) => Ok(result),
                (_, _, var error) => HandleError(error)
            };

        protected async Task<IActionResult> SendRequest(IRequest<Result> request, int statusCode = 200)
            => await _mediator.Send(request).ConfigureAwait(false) switch
            {
                (true, _) => StatusCode(statusCode),
                (_, var error) => HandleError(error)
            };

        private IActionResult HandleError(Exception error)
            => error switch
            {
                TitherValidationException e => BadRequest(e.Errors),
                _ => BadRequest(new Exception(error.Message))
            };
    }
}
