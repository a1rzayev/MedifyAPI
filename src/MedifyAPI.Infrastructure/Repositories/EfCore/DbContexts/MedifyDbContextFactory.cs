using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MedifyAPI.Infrastructure.Repositories.EfCore.DbContexts;
using System.IO;
using Microsoft.EntityFrameworkCore.Design;

public class MedifyDbContextFactory : IDesignTimeDbContextFactory<MedifyDbContext>
{
    public MedifyDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "../MedifyAPI.Api/appsettings.json"), optional: false, reloadOnChange: true) // Correct relative path
            .Build();

        var connectionString = configuration.GetConnectionString("MsSql");

        var optionsBuilder = new DbContextOptionsBuilder<MedifyDbContext>();
        optionsBuilder.UseSqlServer(connectionString);

        return new MedifyDbContext(optionsBuilder.Options);
    }
}
