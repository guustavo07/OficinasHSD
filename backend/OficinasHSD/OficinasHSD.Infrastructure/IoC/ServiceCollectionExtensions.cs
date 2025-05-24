using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OficinasHSD.Application.Interfaces.Repositorios;
using OficinasHSD.Infrastructure.Persistence.Context;
using OficinasHSD.Infrastructure.Persistence.Repositories;

namespace OficinasHSD.Infrastructure.IoC;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
                                                       IConfiguration configuration)
    {
        services.AddDbContext<OficinasHSDDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        // aqui você registra seus repositórios:
        // services.AddScoped<IClienteRepository, ClienteRepository>();
        // …
        // Registra Repositório
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<IOficinaRepository, OficinaRepository>();

        return services;
    }
}