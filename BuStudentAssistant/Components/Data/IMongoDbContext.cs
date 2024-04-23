using BuStudentAssistant.Models;
using MongoDB.Driver;

namespace BuStudentAssistant.Data
{
    public interface IMongoDbContext
    {
        IMongoCollection<User> Users { get; }

        IMongoCollection<Comment> Comments { get; }

        IMongoCollection<StudySpot> StudySpots { get; }
        // Define other collections and methods that your context should expose
    }

}
