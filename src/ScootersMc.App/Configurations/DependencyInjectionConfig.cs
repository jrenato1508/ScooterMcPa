using ScootersMc.Business.Interfaces;
using ScootersMc.Business.Interfaces.IServices;
using ScootersMc.Business.Models.Notificacoes;
using ScootersMc.Business.Services;
using ScootersMc.Data.Context;
using ScootersMc.Data.Repository;

namespace ScootersMc.App.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<MeuDbContext>();
            services.AddScoped<IMembroRepository, MembrosRepository>();
            services.AddScoped<IMembroMcService, MembroMcService>();
            services.AddScoped<IContatoEmergenciaRepository, ContatoEmergenciaRepository>();
            services.AddScoped<IEnderecoRepository, EnderecoRepository>();
            services.AddScoped<INotificador, Notificador>();

            return services;
        }
    }
}
