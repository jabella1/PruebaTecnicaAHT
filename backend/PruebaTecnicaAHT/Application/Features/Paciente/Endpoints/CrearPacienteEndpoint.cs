using Carter;
using MediatR;
using PruebaTecnicaAHT.Application.Features.Paciente.V1.Commands;
using PruebaTecnicaAHT.Application.Features.Paciente.V1.DTOs;

namespace PruebaTecnicaAHT.Application.Features.Paciente.Endpoints;

public class CrearPacienteEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/paciente", async (CrearPacienteCommand command, ISender sender) =>
            {
                var result = await sender.Send(command);
                return Results.Created("/api/paciente", result);
            })
            .WithName("CrearPaciente")
            .WithTags("Paciente")
            .Accepts<CrearPacienteCommand>("application/json")
            .Produces<CrearPacienteResponse>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest);
    }
}
