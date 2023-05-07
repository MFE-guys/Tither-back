using MediatR;
using OperationResult;
using Tither.Shared.Requests;
using Tither.Shared.ViewModels;

namespace Tither.Domain.RequestHandlers
{
    public sealed class WeatherForecastRequestHandler : IRequestHandler<WeatherForecastRequest, Result<WeatherForecastViewModel>>
    {
        public Task<Result<WeatherForecastViewModel>> Handle(WeatherForecastRequest request, CancellationToken cancellationToken)
        {
            // TODO Add logic down bellow
            var data = new WeatherForecastViewModel("test");

            return Result.Success(data);
        }
    }
}