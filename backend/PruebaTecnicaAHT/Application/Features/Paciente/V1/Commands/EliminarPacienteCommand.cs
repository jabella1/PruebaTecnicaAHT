using Ardalis.GuardClauses;
using MediatR;
using PruebaTecnicaAHT.Application.Features.Paciente.V1.DTOs;

namespace PruebaTecnicaAHT.Application.Features.Paciente.V1.Commands;

public class EliminarPacienteCommand(int id) : IRequest<EliminarPacienteResponse>
{
    public int PacienteId { get; init; } =
        Guard.Against.NegativeOrZero(id, nameof(id), "El ID del paciente no puede ser negativo o cero.");
}
