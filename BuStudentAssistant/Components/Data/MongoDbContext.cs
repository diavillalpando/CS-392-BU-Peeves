using BuStudentAssistant.Models;
using MongoDB.Driver;

namespace BuStudentAssistant.Data
{
    public class MongoDbContext : IMongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MongoDB");
            Console.WriteLine($"Connecting to MongoDB with: {connectionString}");

            var client = new MongoClient(connectionString);
            _database = client.GetDatabase("LoginDb"); // replace "yourDatabaseName" with the actual database name
        }

        public IMongoCollection<User> Users => _database.GetCollection<User>("Users");

        public IMongoCollection<Comment> Comments => _database.GetCollection<Comment>("Comments");

        public IMongoCollection<StudySpot> StudySpots => _database.GetCollection<StudySpot>("StudySpots");

        public IMongoCollection<BusyScore> BusyScores => _database.GetCollection<BusyScore>("BusyScores");
    }
}