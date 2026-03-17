using MediatR;
using PruebaTecnicaAHT.Application.Features.Paciente.V1.DTOs;
using PruebaTecnicaAHT.Domain.Contracts;

namespace PruebaTecnicaAHT.Application.Features.Paciente.V1.Commands.Handlers;

public class EliminarPacienteCommandHandler(IPacienteRepository pacienteRepository) : IRequestHandler<EliminarPacienteCommand, EliminarPacienteResponse>
{
    public async Task<EliminarPacienteResponse> Handle(EliminarPacienteCommand request, CancellationToken cancellationToken)
    {
        var paciente = await pacienteRepository.ObtenerPacientePorIdAsync(request.PacienteId);
        if (paciente == null)
        {
            throw new Exception("El paciente que se intenta eliminar no existe.");
        }
        await pacienteRepository.EliminarPacienteAsync(request.PacienteId);
        return new EliminarPacienteResponse
        {
            Id = request.PacienteId
        };
    }
}
