using MongoDB.Driver;
using Tither.Shared.Settings;

namespace Tither.Data.Context
{
    public sealed class TitherContext
    {
        private readonly IMongoDatabase _database;

        public TitherContext(IMongoClient client, AppSettings settings)
         => _database = client.GetDatabase(settings.DatabaseName);

        public IMongoCollection<T> GetCollection<T>(string collectionName) where T : class
            => _database.GetCollection<T>(collectionName);
    }
}
