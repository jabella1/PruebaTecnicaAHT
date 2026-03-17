using MediatR;
using PruebaTecnicaAHT.Application.Features.Paciente.V1.DTOs;
using PruebaTecnicaAHT.Domain.Contracts;

namespace PruebaTecnicaAHT.Application.Features.Paciente.V1.Queries.Handlers;

public class ObtenerPacienteQueryHandler(IPacienteRepository pacienteRepository) : IRequestHandler<ObtenerPacienteQuery, ObtenerPacienteResponse>
{
    public async Task<ObtenerPacienteResponse> Handle(ObtenerPacienteQuery request, CancellationToken cancellationToken)
    {
        var paciente = await pacienteRepository.ObtenerPacientePorIdAsync(request.Id);
        if (paciente == null)
        {
            throw new KeyNotFoundException($"No se encontró un paciente con el ID {request.Id}.");
        }

        return new ObtenerPacienteResponse
        {
            PacienteId = paciente.PacienteId,
            TipoDocumentoId = paciente.TipoDocumentoId,
            NumeroDocumento = paciente.NumeroDocumento,
            NombrePaciente = paciente.Nombre,
            FechaNacimiento = paciente.FechaNacimiento,
            CorreoElectronico = paciente.CorreoElectronico,
            GeneroId = paciente.GeneroId,
            Direccion = paciente.Direccion,
            Telefono = paciente.Telefono
        };
    }
}
