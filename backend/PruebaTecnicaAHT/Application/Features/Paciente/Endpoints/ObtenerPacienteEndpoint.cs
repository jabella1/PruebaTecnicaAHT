using Carter;
using MediatR;
using PruebaTecnicaAHT.Application.Features.Paciente.V1.DTOs;
using PruebaTecnicaAHT.Application.Features.Paciente.V1.Queries;

namespace PruebaTecnicaAHT.Application.Features.Paciente.Endpoints;

public class ObtenerPacienteEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/paciente/{id:int}", async (int id, ISender sender) =>
            {
                var query = new ObtenerPacienteQuery(id);
                var result = await sender.Send(query);
                return Results.Ok(result);
            })
            .WithName("ObtenerPaciente")
            .WithTags("Paciente")
            .Produces<ObtenerPacienteResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);
    }
}
