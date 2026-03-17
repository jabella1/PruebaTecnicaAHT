using Ardalis.GuardClauses;
using MediatR;
using PruebaTecnicaAHT.Application.Features.Paciente.V1.DTOs;

namespace PruebaTecnicaAHT.Application.Features.Paciente.V1.Queries;

public class ObtenerListadoPacientesQuery(int numeroPagina, int numeroFilas) : IRequest<ObtenerListadoPacientesResponse>
{
    public int NumeroPagina { get; init; } =
        Guard.Against.NegativeOrZero(numeroPagina, nameof(numeroPagina), "El número de pagina no puede ser negativo o cero.");

    public int NumeroFilas { get; init; } =
        Guard.Against.NegativeOrZero(numeroFilas, nameof(numeroFilas), "El número de filas no puede ser negativo o cero.");
}
