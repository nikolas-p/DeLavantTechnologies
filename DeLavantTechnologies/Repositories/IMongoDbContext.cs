using DeLavantTechnologies.Data;
using MongoDB.Driver;

namespace DeLavantTechnologies.Repositories
{
    public interface IMongoDbContext
    {
        IMongoCollection<T> GetCollection<T>(string name);
    }

    public class MongoDbContext : IMongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IConfiguration config)
        {
            var settings = config.GetSection(nameof(MongoDbConfig)).Get<MongoDbConfig>();
            var client = new MongoClient(settings.ConnectionString);
            _database = client.GetDatabase(settings.Name);
        }

        public IMongoCollection<T> GetCollection<T>(string name) =>
            _database.GetCollection<T>(name);
    }
}
