using EFCoreCodeThirdDemo.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace EFCoreCodeSecondDemo
{
    internal class EFCoreDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //// Enable Logging
            //optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
            
            
            ////Connection String
            //optionsBuilder.UseSqlServer("Server=LAPTOP-6P5NK25R\\SQLSERVER2022DEV;Database=StudentDB;Trusted_Connection=True;TrustServerCertificate=True;");

            // Step 1: Load the Configuration File (appsettings.json).
            // The ConfigurationBuilder class is used to construct configuration settings from various sources.
            // Here, we add the appsettings.json file to the configuration sources and then build it.
            var configBuilder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json") // Specify the configuration file to load.
                .Build(); // Build the configuration object, making it ready to retrieve values.
            // Step 2: Get the "ConnectionStrings" section from the configuration.
            // The GetSection method is used to access a specific section within the configuration file.
            // Here, we are accessing the "ConnectionStrings" section which contains our database connection strings.
            var configSection = configBuilder.GetSection("ConnectionStrings");
            // Step 3: Retrieve the connection string value using its key ("SQLServerConnection").
            // The indexer [] is used to access the value corresponding to the "SQLServerConnection" key within the section.
            // The null-coalescing operator (??) ensures that if the key is not found, it will return null.
            var connectionString = configSection["SQLServerConnection"] ?? null;
            // Step 4: Configure the DbContext to use SQL Server with the retrieved connection string.
            // The UseSqlServer method is an extension method that configures the context to connect to a SQL Server database.
            optionsBuilder.UseSqlServer(connectionString);
        }

        // DbSet properties represent the tables in the database. 
        // Each DbSet corresponds to a table, and the type parameter corresponds to the entity class mapped to that table.
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Course> Courses { get; set; }
    }
}
