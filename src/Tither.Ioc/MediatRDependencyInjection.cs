using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Tither.Domain.RequestHandlers;
using Tither.Shared.Requests;

namespace Tither.Ioc
{
    public static class MediatRDependencyInjection
    {
        public static void AddMediatR(this IServiceCollection services)
        {
            var assemblies = new[] { 
                typeof(WeatherForecastRequest).GetTypeInfo().Assembly, 
                typeof(WeatherForecastRequestHandler).GetTypeInfo().Assembly };

            services.AddMediatR(assemblies);

            // TODO: Add pipelines Behavior
        }
    }
}