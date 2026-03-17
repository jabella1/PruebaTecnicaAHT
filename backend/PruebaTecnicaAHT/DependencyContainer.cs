using System.Data;
using Carter;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PruebaTecnicaAHT.Domain.Contracts;
using PruebaTecnicaAHT.Domain.DomainValidation;
using PruebaTecnicaAHT.Domain.Options;
using PruebaTecnicaAHT.Infrastructure.Persistence;
using PruebaTecnicaAHT.Infrastructure.Repositories.RepositoryInjection;

namespace PruebaTecnicaAHT;

public static class DependencyContainer
{
    public static IServiceCollection AddPruebaTecnicaAHTServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

        services.AddCarter();

        services.AddScoped<IDbConnection>(sp =>
        {
            var settings = sp.GetRequiredService<IOptions<AppSettings>>().Value;
            return new SqlConnection(settings.ConnectionString);
        });

        services.AddDbContext<PruebaTecnicaAhtContext>((sp, options) =>
        {
            var settings = sp.GetRequiredService<IOptions<AppSettings>>().Value;
            options.UseSqlServer(settings.ConnectionString, sqlOptions =>
            {
                sqlOptions.EnableRetryOnFailure();
            });
        });

        services.AgregarRepositorios();
        
        services.AddScoped<IValidation, Validation>();

        return services;
    }
}
