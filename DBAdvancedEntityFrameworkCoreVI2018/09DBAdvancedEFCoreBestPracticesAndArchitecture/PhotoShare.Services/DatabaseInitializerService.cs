using Microsoft.EntityFrameworkCore;
using PhotoShare.Data;
using PhotoShare.Services.Contracts;

namespace PhotoShare.Services
{
    public class DatabaseInitializerService : IDatabaseInitializerService
    {
        private readonly PhotoShareContext context;

        public DatabaseInitializerService(PhotoShareContext context)
        {
            this.context = context;
        }

        public void InitializeDatabase()
        {
            this.context.Database.Migrate();
        }
    }
}