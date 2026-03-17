using PruebaTecnicaAHT.Domain.Contracts;

namespace PruebaTecnicaAHT.Infrastructure.Repositories.RepositoryInjection;

public static class RegistrarRepositorios
{
    public static IServiceCollection AgregarRepositorios(this IServiceCollection services)
    {
        services.AddScoped<IPacienteRepository, PacienteRepository>();
        services.AddScoped<IGeneroRepository, GeneroRepository>();
        services.AddScoped<ITipoDocumentoRepository, TipoDocumentoRepository>();
        return services;
    }
}
