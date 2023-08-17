using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Tither.Data.Context;

using Tither.Shared.Settings;

namespace Tither.Ioc
{
    public static class DataContextDependencyInjection
    {
        public static void DataContextConfiguration(this IServiceCollection services, AppSettings appSettings)
        {
            services.AddSingleton((IMongoClient) new MongoClient(appSettings.ConnectionString));
            services.AddSingleton<TitherContext>();
        }
    }
}
