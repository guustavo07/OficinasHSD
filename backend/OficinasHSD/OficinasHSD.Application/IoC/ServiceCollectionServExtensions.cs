using Microsoft.Extensions.DependencyInjection;
using OficinasHSD.Application.Interfaces.Servicos;
using OficinasHSD.Application.Services;

namespace OficinasHSD.Application.IoC;
public static class ServiceCollectionServExtensions
{
    public static IServiceCollection AddAppService(this IServiceCollection services)
    {
        // Registra Application Services
        services.AddScoped<IUsuarioService, UsuarioService>();
        services.AddScoped<IOficinaService, OficinaService>();
        services.AddScoped<IAuthService, AuthService>();

        return services;
    }
}
