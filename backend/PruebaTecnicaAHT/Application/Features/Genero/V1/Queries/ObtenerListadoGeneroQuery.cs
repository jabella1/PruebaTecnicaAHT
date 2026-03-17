using MediatR;
using PruebaTecnicaAHT.Application.Features.Genero.V1.DTOs;

namespace PruebaTecnicaAHT.Application.Features.Genero.V1.Queries;

public class ObtenerListadoGeneroQuery() : IRequest<List<ObtenerListadoGeneroResponse>>
{
}
