using MediatR;
using PruebaTecnicaAHT.Application.Features.Paciente.V1.DTOs;
using PruebaTecnicaAHT.Domain.Contracts;

namespace PruebaTecnicaAHT.Application.Features.Paciente.V1.Commands.Handlers;

public class CrearPacienteCommandHandler(IPacienteRepository pacienteRepository, 
    ITipoDocumentoRepository tipoDocumentoRepository, IGeneroRepository generoRepository, IValidation validation) : IRequestHandler<CrearPacienteCommand, CrearPacienteResponse>
{
    public async Task<CrearPacienteResponse> Handle(CrearPacienteCommand request, CancellationToken cancellationToken)
    {
        await validation.ValidarTipoDocumento(request.TipoDocumentoId);
        await validation.ValidarGenero(request.GeneroId);
        validation.ValidarSoloNumerosEnString(request.NumeroDocumento);
        validation.ValidarSoloNumerosEnString(request.Telefono);
        if (request.CorreoElectronico is not null)
        {
            validation.ValidarCorreoElectronico(request.CorreoElectronico);
        }
        var paciente = Domain.Entities.Paciente.Crear(request.TipoDocumentoId , request.NumeroDocumento, request.NombrePaciente, request.FechaNacimiento,
            request.CorreoElectronico, request.GeneroId, request.Direccion, request.Telefono);

        await pacienteRepository.CrearPacienteAsync(paciente);
        return new CrearPacienteResponse
        {
            Id = paciente.PacienteId
        };
    }
}
