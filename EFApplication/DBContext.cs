using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Configuration;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


public class AppDbContext : DbContext
{
    public DbSet<Account> accounts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

        string connectionString = configuration.GetConnectionString("testdbthread");
        optionsBuilder.UseNpgsql(connectionString);
        
    }
}

