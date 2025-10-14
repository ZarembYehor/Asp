using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using AbsoluteCinema.Models;

namespace AbsoluteCinema
{
    public class AppIdentityDbContextFactory : IDesignTimeDbContextFactory<AppIdentityDbContext>
    {
        public AppIdentityDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<AppIdentityDbContext>();

            optionsBuilder.UseSqlServer(configuration.GetConnectionString("IdentityConnection"));

            return new AppIdentityDbContext(optionsBuilder.Options);
        }
    }
}