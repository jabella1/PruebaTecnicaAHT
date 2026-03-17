using Ardalis.GuardClauses;
using MediatR;
using PruebaTecnicaAHT.Application.Features.Paciente.V1.DTOs;

namespace PruebaTecnicaAHT.Application.Features.Paciente.V1.Queries;

public class ObtenerPacienteQuery(int id) : IRequest<ObtenerPacienteResponse>
{
    public int Id { get; init; } =
        Guard.Against.NegativeOrZero(id, nameof(Id), "El ID del paciente no puede ser negativo o cero.");
}
