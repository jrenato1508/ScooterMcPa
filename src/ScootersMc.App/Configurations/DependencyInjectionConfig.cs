using ScootersMc.Business.Interfaces;
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
            services.AddScoped<IContatoEmergenciaRepository, ContatoEmergenciaRepository>();
            services.AddScoped<IEnderecoRepository, EnderecoRepository>();

            return services;
        }
    }
}
