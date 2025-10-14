using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using RestAPI.Models; 

namespace RestAPI
{
    public class CinemaDbContextFactory : IDesignTimeDbContextFactory<CinemaDbContext>
    {
        public CinemaDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<CinemaDbContext>();

            optionsBuilder.UseSqlServer(configuration.GetConnectionString("AbsoluteCinemaConnection"));

            return new CinemaDbContext(optionsBuilder.Options);
        }
    }
}