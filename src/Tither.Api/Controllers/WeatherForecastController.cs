using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tither.Shared.Requests;

namespace Tither.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : TitherControllerBase
    {
        public WeatherForecastController(IMediator mediator)
            : base(mediator) { }

        [HttpGet(Name = "GetWeatherForecast")]
        public Task<IActionResult> Get(string name)
         => SendRequest(new WeatherForecastRequest(name));
    }
}