using Carter;
using MediatR;
using PruebaTecnicaAHT.Application.Features.Genero.V1.DTOs;
using PruebaTecnicaAHT.Application.Features.Genero.V1.Queries;

namespace PruebaTecnicaAHT.Application.Features.Genero.Endpoints;

public class ObtenerListadoGeneroEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/genero", async (ISender sender) =>
            {
                var query = new ObtenerListadoGeneroQuery();
                var result = await sender.Send(query);
                return Results.Ok(result);
            })
            .WithName("ObtenerListadoGenero")
            .WithTags("Genero")
            .Produces<List<ObtenerListadoGeneroResponse>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);
    }
}
