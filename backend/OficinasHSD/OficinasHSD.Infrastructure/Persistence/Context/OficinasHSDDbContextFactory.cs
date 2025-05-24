using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace OficinasHSD.Infrastructure.Persistence.Context;
public class OficinasHSDDbContextFactory
        : IDesignTimeDbContextFactory<OficinasHSDDbContext>
{
    public OficinasHSDDbContext CreateDbContext(string[] args)
    {
        //   monta o caminho para o appsettings.json do projeto API
        var basePath = Path.Combine(Directory.GetCurrentDirectory(), "../OficinasHSD.API");

        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
            .Build();

        // pega a connection string
        var conn = configuration.GetConnectionString("DefaultConnection")
                   ?? throw new InvalidOperationException(
                      "Connection string 'DefaultConnection' não encontrada.");

       
        var builder = new DbContextOptionsBuilder<OficinasHSDDbContext>();
        builder.UseSqlServer(conn);

        // retorna nova instância
        return new OficinasHSDDbContext(builder.Options);
    }
}
