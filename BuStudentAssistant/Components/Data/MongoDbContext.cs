using BuStudentAssistant.Models;
using MongoDB.Driver;

namespace BuStudentAssistant.Data
{
    public class MongoDbContext : IMongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetConnectionString("MongoDB"));
            _database = client.GetDatabase("LoginDb");
        }

        public IMongoCollection<User> Users => _database.GetCollection<User>("Users");
    }
}
