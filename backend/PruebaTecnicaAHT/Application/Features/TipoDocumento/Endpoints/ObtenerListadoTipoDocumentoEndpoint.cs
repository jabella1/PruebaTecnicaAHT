using Carter;
using MediatR;
using PruebaTecnicaAHT.Application.Features.TipoDocumento.V1.DTOs;
using PruebaTecnicaAHT.Application.Features.TipoDocumento.V1.Queries;

namespace PruebaTecnicaAHT.Application.Features.TipoDocumento.Endpoints;

public class ObtenerListadoTipoDocumentoEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/tipodocumento", async (ISender sender) =>
            {
                var query = new ObtenerListadoTipoDocumentoQuery();
                var result = await sender.Send(query);
                return Results.Ok(result);
            })
            .WithName("ObtenerListadoTipoDocumento")
            .WithTags("TipoDocumento")
            .Produces<List<ObtenerListadoTipoDocumentoResponse>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);
    }
}
