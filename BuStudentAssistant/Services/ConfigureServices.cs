using BuStudentAssistant.Data;

namespace BuStudentAssistant.Services
{
    public class Program
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IMongoDbContext, MongoDbContext>();
            // Ensure IConfiguration is properly injected or available
        }

    }
}
