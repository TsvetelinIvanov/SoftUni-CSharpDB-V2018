using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using PhotoShare.Data;

namespace PhotoShare.Client.Configuration
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<PhotoShareContext>
    {
        public PhotoShareContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            DbContextOptionsBuilder<PhotoShareContext> builder = new DbContextOptionsBuilder<PhotoShareContext>();
            string connectionString = configuration.GetConnectionString("DefaultConnection");
            builder.UseSqlServer(connectionString);

            return new PhotoShareContext(builder.Options);
        }
    }
}