using Domain.Interfaces.Cadastro;
using Domain.Interfaces.Generics;
using Domain.Interfaces.IServices.Cadastro;
using Domain.Interfaces.IServices.Tesouraria;
using Domain.Interfaces.Tesouraria;
using Domain.Services.Cadastro;
using Domain.Services.Tesouraria;
using Entities.Notification;
using Infra.Repositories.Cadastro;
using Infra.Repositories.Generics;
using Infra.Repositories.Tesouraria;
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

            services.AddScoped<DepartamentoInterface, DepartamentoRepository>();
            services.AddScoped<DepartamenteIService, DepartamentoService>();

            services.AddScoped<TipoDocumentoInterface, TipoDocumentoRepository>();
            services.AddScoped<TipoDocumentoIService, TipoDocumentoService>();

            services.AddScoped<BancoInterface, BancoRepository>();
            services.AddScoped<BancoIService, BancoService>();

            services.AddScoped<FormaPagamentoInterface, FormaPagamentoRepository>();
            services.AddScoped<FormaPagamentoIService, FormaPagamentoService>();

            //Tesouraria
            services.AddScoped<ContaInterface, ContaRepository>();
            services.AddScoped<ContaIService, ContaService>();

            services.AddScoped<TipoMovimentoInterface, TipoMovimentoRepository>();
            services.AddScoped<TipoMovimentoIService, TipoMovimentoService>();

            return services;
        }
    }
}
