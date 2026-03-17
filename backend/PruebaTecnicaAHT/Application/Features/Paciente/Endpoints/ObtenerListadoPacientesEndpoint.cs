using Carter;
using MediatR;
using PruebaTecnicaAHT.Application.Features.Paciente.V1.DTOs;
using PruebaTecnicaAHT.Application.Features.Paciente.V1.Queries;

namespace PruebaTecnicaAHT.Application.Features.Paciente.Endpoints;

public class ObtenerListadoPacientesEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/paciente", async (int numeroPagina, int numeroFilas, ISender sender) =>
            {
                var query = new ObtenerListadoPacientesQuery(numeroPagina, numeroFilas);
                var result = await sender.Send(query);
                return Results.Ok(result);
            })
            .WithName("ObtenerListadoPacientes")
            .WithTags("Paciente")
            .Produces<ObtenerListadoPacientesResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);
    }
}
