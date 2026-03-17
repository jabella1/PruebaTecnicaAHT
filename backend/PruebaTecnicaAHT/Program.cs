using System.Text.Json;
using Carter;
using DotNetEnv;
using Microsoft.AspNetCore.Diagnostics;
using PruebaTecnicaAHT;
using PruebaTecnicaAHT.Domain.Options;
using PruebaTecnicaAHT.Domain.Validations;

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

builder.Services.AddPruebaTecnicaAHTServices(builder.Configuration);

var app = builder.Build();

app.UseExceptionHandler(appBuilder =>
{
    appBuilder.Run(async context =>
    {
        context.Response.ContentType = "application/json";

        var exception = context.Features.Get<IExceptionHandlerPathFeature>()?.Error;

        if (exception is DomainValidationException)
        {
            context.Response.StatusCode = 422;
        } else if (exception is KeyNotFoundException)
        {
            context.Response.StatusCode = 404;
        }
        else if (exception is DomainValidationException)
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
        c.DocumentTitle = "PruebaTecnicaAHT.API";
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
