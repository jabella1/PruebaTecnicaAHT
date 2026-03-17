using Carter;
using MediatR;
using PruebaTecnicaAHT.Application.Features.Paciente.V1.Commands;
using PruebaTecnicaAHT.Application.Features.Paciente.V1.DTOs;

namespace PruebaTecnicaAHT.Application.Features.Paciente.Endpoints;

public class ActualizarPacienteEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/api/paciente", async (ActualizarPacienteCommand request, ISender sender) =>
            {
                var result = await sender.Send(request);
                return Results.Ok(result);
            })
            .WithName("ActualizarPaciente")
            .WithTags("Paciente")
            .Accepts<ActualizarPacienteCommand>("application/json")
            .Produces<ActualizarPacienteResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest);
    }
}
