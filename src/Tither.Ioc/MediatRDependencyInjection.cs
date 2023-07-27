using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Tither.Domain.Pipelines;
using Tither.Domain.RequestHandlers;
using Tither.Shared.Requests;

namespace Tither.Ioc
{
    public static class MediatRDependencyInjection
    {
        public static void AddMediatR(this IServiceCollection services)
        {
            var assemblies = new[] { 
                typeof(GetAllMembersRequest).GetTypeInfo().Assembly, 
                typeof(GetAllMembersRequestHandler).GetTypeInfo().Assembly };

            services.AddMediatR(assemblies);

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ExceptionPipelineBehavior<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(FailFastRequestBehavior<,>));
        }
    }
}