using Ardalis.GuardClauses;
using MediatR;
using PruebaTecnicaAHT.Application.Features.TipoDocumento.V1.DTOs;

namespace PruebaTecnicaAHT.Application.Features.TipoDocumento.V1.Queries;

public class ObtenerListadoTipoDocumentoQuery() : IRequest<List<ObtenerListadoTipoDocumentoResponse>>
{
}
