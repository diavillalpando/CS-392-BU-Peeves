using BuStudentAssistant.Data;
using BuStudentAssistant.Models;

namespace BuStudentAssistant.Services
{
    public class UserService
    {
        private readonly IMongoDbContext _dbContext;

        public UserService(IMongoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task RegisterUser(User newUser)
        {
            try
            {
                await _dbContext.Users.InsertOneAsync(newUser);
            }
            catch (Exception ex)
            {
                // Log the exception details
                Console.WriteLine($"Error inserting user: {ex.Message}");
                // Optionally rethrow or handle the exception differently
                throw;
            }
        }
    }
}
