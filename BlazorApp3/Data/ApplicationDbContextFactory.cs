
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design; // Required for IDesignTimeDbContextFactory
using Microsoft.Extensions.Configuration; // Required for ConfigurationBuilder
using System.IO; // Required for Directory.GetCurrentDirectory

namespace BlazorApp3.Data // Use your actual project's namespace here
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // Looks for appsettings.json
                .AddJsonFile("appsettings.json") // Adds appsettings.json 
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseMySQL(connectionString); 

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}