using MediatR;
using PruebaTecnicaAHT.Application.Features.Paciente.V1.DTOs;
using PruebaTecnicaAHT.Domain.Contracts;

namespace PruebaTecnicaAHT.Application.Features.Paciente.V1.Commands.Handlers;

public class ActualizarPacienteCommandHandler(IPacienteRepository pacienteRepository, IValidation validation) : IRequestHandler<ActualizarPacienteCommand, ActualizarPacienteResponse>
{
    public async Task<ActualizarPacienteResponse> Handle(ActualizarPacienteCommand request, CancellationToken cancellationToken)
    {
        var paciente = await validation.ValidarPacienteExistente(request.PacienteId);
        await validation.ValidarTipoDocumento(request.TipoDocumentoId);
        await validation.ValidarGenero(request.GeneroId);
        validation.ValidarSoloNumerosEnString(request.NumeroDocumento);
        validation.ValidarSoloNumerosEnString(request.Telefono);
        paciente.Actualizar(request.TipoDocumentoId, request.NumeroDocumento, request.NombrePaciente, request.FechaNacimiento,
            request.CorreoElectronico, request.GeneroId, request.Direccion, request.Telefono);
        await pacienteRepository.ActualizarPacienteAsync(paciente);
        return new ActualizarPacienteResponse
        {
            Id = paciente.PacienteId
        };
    }
}
