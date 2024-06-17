using Domain.Interfaces.Cadastro;
using Domain.Interfaces.Generics;
using Domain.Interfaces.IServices.Cadastro;
using Domain.Services.Cadastro;
using Entities.Notification;
using Infra.Repositories.Cadastro;
using Infra.Repositories.Generics;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using System.Data;

namespace WebApi.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            // Connection
            services.AddScoped<IDbConnection, NpgsqlConnection>();

            //Notificador
            services.AddScoped<INotificador, Notificador>();

            //Cadastro
            services.AddScoped<PaisInterface, PaisRepository>();
            services.AddScoped<PaisIService, PaisService>();

            services.AddScoped<UnidadeFederativaInterface, UnidadeFederativaRepository>();
            services.AddScoped<UnidadeFederativaIService, UnidadeFederativaService>();

            services.AddScoped<CidadeInterface, CidadeRepository>();
            services.AddScoped<CidadeIService, CidadeService>();

            return services;
        }
    }
}
