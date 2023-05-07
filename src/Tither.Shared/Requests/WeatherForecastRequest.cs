using MediatR;
using OperationResult;
using Tither.Shared.ViewModels;

namespace Tither.Shared.Requests
{
    public class WeatherForecastRequest : IRequest<Result<WeatherForecastViewModel>>
    {
        public WeatherForecastRequest(string name)
        {
            Name = name;
        }

        public string Name { get; set; } = "";
    }
}
