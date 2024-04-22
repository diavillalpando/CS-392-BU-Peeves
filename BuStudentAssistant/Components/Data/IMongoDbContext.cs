using BuStudentAssistant.Models;
using MongoDB.Driver;

namespace BuStudentAssistant.Data
{
    public interface IMongoDbContext
    {
        IMongoCollection<User> Users { get; }
        // Define other collections and methods that your context should expose
    }

}
