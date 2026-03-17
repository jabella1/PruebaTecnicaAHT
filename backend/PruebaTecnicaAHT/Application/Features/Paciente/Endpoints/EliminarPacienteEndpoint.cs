using Carter;
using MediatR;
using PruebaTecnicaAHT.Application.Features.Paciente.V1.Commands;
using PruebaTecnicaAHT.Application.Features.Paciente.V1.DTOs;

namespace PruebaTecnicaAHT.Application.Features.Paciente.Endpoints;

public class EliminarPacienteEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/paciente/{id:int}", async (int id, ISender sender) =>
            {
                var cmd = new EliminarPacienteCommand(id);
                var result = await sender.Send(cmd);
                return Results.Ok(result);
            })
            .WithName("EliminarPaciente")
            .WithTags("Paciente")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound);
    }
}
