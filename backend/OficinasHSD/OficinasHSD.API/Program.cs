using Microsoft.OpenApi.Models;
using OficinasHSD.API.Extensions;
using OficinasHSD.Application.IoC;
using OficinasHSD.Application.Mapping;
using OficinasHSD.Infrastructure.IoC;

namespace OficinasHSD.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddAutoMapper(
            typeof(UsuarioMapper).Assembly,
            typeof(OficinaMapper).Assembly
        );


        //JWT
        builder.Services.AddJwtAuth(builder.Configuration);

        // registra Infra 
        builder.Services.AddInfrastructure(builder.Configuration);


        builder.Services.AddAppService();

        // Configuração do Swagger/OpenAPI
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "OficinasHSD API", Version = "v1" });


            // Configuração de autenticação com Bearer Token
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Por favor, insira seu token JWT no formato: Bearer {seu token}"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
        });

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowFrontend", policy =>
            {
                policy
                    .WithOrigins("http://127.0.0.1:5500", "null")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials(); // só se estiver usando cookies/autenticação
            });
        });

        var app = builder.Build();
        app.UseCors("AllowFrontend");


        // Habilita o Swagger
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "OficinasHSD API V1");
        });



        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
