using MediatR;
using PruebaTecnicaAHT.Application.Features.Genero.V1.DTOs;
using PruebaTecnicaAHT.Application.Features.TipoDocumento.V1.DTOs;
using PruebaTecnicaAHT.Domain.Contracts;

namespace PruebaTecnicaAHT.Application.Features.TipoDocumento.V1.Queries.Handlers;

public class ObtenerListadoTipoDocumentoQueryHandler(ITipoDocumentoRepository tipoDocumentoRepository) : IRequestHandler<ObtenerListadoTipoDocumentoQuery, List<ObtenerListadoTipoDocumentoResponse>>
{
    public async Task<List<ObtenerListadoTipoDocumentoResponse>> Handle(ObtenerListadoTipoDocumentoQuery request, CancellationToken cancellationToken)
    {
        var tiposDocumento = await tipoDocumentoRepository.ObtenerTiposDocumento();
        var tiposDocumentoMapped = tiposDocumento.Select(td => new ObtenerListadoTipoDocumentoResponse()
        {
            Id = td.TipoDocumentoId,
            Tipo = td.Tipo
        }).ToList();
        return tiposDocumentoMapped;
    }
}
