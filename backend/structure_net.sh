#!/usr/bin/env sh

PROJECT_NAME="$1"

if [ -z "$PROJECT_NAME" ]; then
  echo "Uso: ./structure_net.sh NombreProyecto"
  exit 1
fi

BASE_PATH="$PROJECT_NAME"
CSPROJ_PATH="$BASE_PATH/$PROJECT_NAME.csproj"

if [ ! -d "$BASE_PATH" ]; then
  echo "No existe la carpeta $BASE_PATH"
  exit 1
fi

if [ ! -f "$CSPROJ_PATH" ]; then
  echo "No se encontro $CSPROJ_PATH"
  exit 1
fi

mkdir -p "$BASE_PATH/Application/Features/Example/Endpoints"
mkdir -p "$BASE_PATH/Application/Features/Example/V1/Commands/DTOs"
mkdir -p "$BASE_PATH/Application/Features/Example/V1/Commands/Handlers"
mkdir -p "$BASE_PATH/Application/Features/Example/V1/Queries/DTOs"
mkdir -p "$BASE_PATH/Application/Features/Example/V1/Queries/Handlers"

mkdir -p "$BASE_PATH/Domain/Entities"
mkdir -p "$BASE_PATH/Domain/Options"
mkdir -p "$BASE_PATH/Domain/Validations"
mkdir -p "$BASE_PATH/Domain/Wrappers"

mkdir -p "$BASE_PATH/Infrastructure/Persistence"
mkdir -p "$BASE_PATH/Infrastructure/Repositories/Contracts"
mkdir -p "$BASE_PATH/Infrastructure/Repositories/RepositoryInjection"

mkdir -p "$BASE_PATH/Properties"

touch "$BASE_PATH/Application/Features/Example/Endpoints/ExampleEndpoint.cs"
touch "$BASE_PATH/Application/Features/Example/V1/Commands/CreateExampleCommand.cs"
touch "$BASE_PATH/Application/Features/Example/V1/Commands/Handlers/CreateExampleCommandHandler.cs"
touch "$BASE_PATH/Application/Features/Example/V1/Queries/GetExampleQuery.cs"
touch "$BASE_PATH/Application/Features/Example/V1/Queries/Handlers/GetExampleQueryHandler.cs"

touch "$BASE_PATH/Domain/Entities/ExampleEntity.cs"
touch "$BASE_PATH/Infrastructure/Repositories/Contracts/IExampleRepository.cs"
touch "$BASE_PATH/Infrastructure/Repositories/ExampleRepository.cs"
touch "$BASE_PATH/$PROJECT_NAME.http"

cat > "$CSPROJ_PATH" <<EOF
<Project Sdk="Microsoft.NET.Sdk.Web">

  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Ardalis.GuardClauses" Version="5.0.0" />
    <PackageReference Include="Ardalis.Specification" Version="9.3.1" />
    <PackageReference Include="Ardalis.Specification.EntityFrameworkCore" Version="9.3.1" />
    <PackageReference Include="Carter" Version="8.2.1" />
    <PackageReference Include="Dapper" Version="2.1.66" />
    <PackageReference Include="DotNetEnv" Version="3.1.1" />
    <PackageReference Include="MediatR" Version="13.1.0" />
    <PackageReference Include="Microsoft.AspNetCore.OpenApi" Version="10.0.0" />
    <PackageReference Include="Microsoft.CodeAnalysis.Common" Version="4.7.0" />
    <PackageReference Include="Microsoft.EntityFrameworkCore" Version="8.0.24" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="8.0.24" />
    <PackageReference Include="Swashbuckle.AspNetCore" Version="10.0.1" />
  </ItemGroup>

  <ItemGroup>
    <Folder Include="Application\Features\" />
    <Folder Include="Domain\Entities\" />
    <Folder Include="Infrastructure\Repositories\Contracts\" />
  </ItemGroup>

</Project>
EOF

cat > "$BASE_PATH/Domain/Options/AppSettings.cs" <<EOF
namespace ${PROJECT_NAME}.Domain.Options;

public class AppSettings
{
    public const string SectionKey = "CONNECTIONSTRING_PRUEBAT";
    public string ConnectionString { get; set; } = string.Empty;
}
EOF

cat > "$BASE_PATH/Domain/Validations/ConflictException.cs" <<EOF
namespace ${PROJECT_NAME}.Domain.Validations;

public class ConflictException : Exception
{
    public ConflictException()
    {
    }

    public ConflictException(string message) : base(message)
    {
    }

    public ConflictException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
EOF

cat > "$BASE_PATH/Infrastructure/Persistence/${PROJECT_NAME}Context.cs" <<EOF
using Microsoft.EntityFrameworkCore;

namespace ${PROJECT_NAME}.Infrastructure.Persistence;

public partial class ${PROJECT_NAME}Context : DbContext
{
    public ${PROJECT_NAME}Context()
    {
    }

    public ${PROJECT_NAME}Context(DbContextOptions<${PROJECT_NAME}Context> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
EOF

cat > "$BASE_PATH/Infrastructure/Repositories/RepositoryInjection/RegistrarRepositorios.cs" <<EOF
namespace ${PROJECT_NAME}.Infrastructure.Repositories.RepositoryInjection;

public static class RegistrarRepositorios
{
    public static IServiceCollection AgregarRepositorios(this IServiceCollection services)
    {
        // services.AddScoped<IExampleRepository, ExampleRepository>();
        return services;
    }
}
EOF

cat > "$BASE_PATH/DependencyContainer.cs" <<EOF
using System.Data;
using Carter;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ${PROJECT_NAME}.Domain.Options;
using ${PROJECT_NAME}.Infrastructure.Persistence;
using ${PROJECT_NAME}.Infrastructure.Repositories.RepositoryInjection;

namespace ${PROJECT_NAME};

public static class DependencyContainer
{
    public static IServiceCollection Add${PROJECT_NAME}Services(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

        services.AddCarter();

        services.AddScoped<IDbConnection>(sp =>
        {
            var settings = sp.GetRequiredService<IOptions<AppSettings>>().Value;
            return new SqlConnection(settings.ConnectionString);
        });

        services.AddDbContext<${PROJECT_NAME}Context>((sp, options) =>
        {
            var settings = sp.GetRequiredService<IOptions<AppSettings>>().Value;
            options.UseSqlServer(settings.ConnectionString, sqlOptions =>
            {
                sqlOptions.EnableRetryOnFailure();
            });
        });

        services.AgregarRepositorios();

        return services;
    }
}
EOF

cat > "$BASE_PATH/Program.cs" <<EOF
using System.Text.Json;
using Carter;
using DotNetEnv;
using Microsoft.AspNetCore.Diagnostics;
using ${PROJECT_NAME};
using ${PROJECT_NAME}.Domain.Options;
using ${PROJECT_NAME}.Domain.Validations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

Env.Load();

var configuration = builder.Configuration
    .AddEnvironmentVariables()
    .Build();

var connectionString = configuration.GetValue<string>(AppSettings.SectionKey)
    ?? throw new Exception("CONNECTIONSTRING is not set");

builder.Services.Configure<AppSettings>(appSettings =>
{
    appSettings.ConnectionString = connectionString;
});

builder.Services.Add${PROJECT_NAME}Services(builder.Configuration);

var app = builder.Build();

app.UseExceptionHandler(appBuilder =>
{
    appBuilder.Run(async context =>
    {
        context.Response.ContentType = "application/json";

        var exception = context.Features.Get<IExceptionHandlerPathFeature>()?.Error;

        if (exception is KeyNotFoundException)
        {
            context.Response.StatusCode = 404;
        }
        else if (exception is ConflictException)
        {
            context.Response.StatusCode = 409;
        }
        else
        {
            context.Response.StatusCode = 500;
        }

        var result = JsonSerializer.Serialize(new
        {
            error = exception?.Message
        });

        await context.Response.WriteAsync(result);
    });
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Prueba tecnica API");
        c.DocumentTitle = "${PROJECT_NAME}.API";
        c.DefaultModelsExpandDepth(-1);
        c.DisplayOperationId();
        c.DisplayRequestDuration();
    });
}

app.UseCors("AllowAll");
app.UseRouting();

app.MapCarter();

app.UseHttpsRedirection();

app.Run();
EOF

cat > "$BASE_PATH/appsettings.json" <<EOF
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
EOF

cat > "$BASE_PATH/appsettings.Development.json" <<EOF
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
EOF

cat > "$BASE_PATH/.env" <<EOF
CONNECTIONSTRING_PRUEBAT='Data Source=localhost,1433;Initial Catalog=${PROJECT_NAME};User ID=sa;Password=Juanfernando1.;MultipleActiveResultSets=false;Encrypt=false;'
EOF

echo "Estructura y archivos base creados/sobrescritos en $PROJECT_NAME"