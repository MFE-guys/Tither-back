using Microsoft.Extensions.DependencyInjection;
using Tither.Data.Repositories;
using Tither.Domain.Repositories;
using Tither.Shared.Settings;

namespace Tither.Ioc
{
    public static class TitherDependencyInjection
    {
        public static void AddTitherConfiguration(this IServiceCollection services, AppSettings appSettings)
        {
            services.AddSingleton(appSettings);
            services.AddScoped<IMemberRepository, MemberRepository>();
        }
    }
}
