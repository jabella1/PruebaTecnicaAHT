using MediatR;
using PruebaTecnicaAHT.Application.Features.Genero.V1.DTOs;
using PruebaTecnicaAHT.Domain.Contracts;

namespace PruebaTecnicaAHT.Application.Features.Genero.V1.Queries.Handlers;

public class ObtenerListadoGeneroQueryHandler(IGeneroRepository generoRepository) : IRequestHandler<ObtenerListadoGeneroQuery, List<ObtenerListadoGeneroResponse>>
{
    public async Task<List<ObtenerListadoGeneroResponse>> Handle(ObtenerListadoGeneroQuery request, CancellationToken cancellationToken)
    {
        var generos = await generoRepository.ObtenerGenerosAsync();
        var generosMapped = generos.Select(g => new ObtenerListadoGeneroResponse
        {
            Id = g.GeneroId,
            Nombre = g.Nombre
        }).ToList();
        return generosMapped;
    }
}
