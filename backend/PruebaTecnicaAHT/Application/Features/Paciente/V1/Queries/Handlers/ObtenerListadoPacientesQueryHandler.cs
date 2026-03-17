using System.Data.Common;
using MediatR;
using PruebaTecnicaAHT.Application.Features.Paciente.V1.DTOs;
using PruebaTecnicaAHT.Domain.Contracts;

namespace PruebaTecnicaAHT.Application.Features.Paciente.V1.Queries.Handlers;

public class ObtenerListadoPacientesQueryHandler(IPacienteRepository pacienteRepository) : IRequestHandler<ObtenerListadoPacientesQuery, ObtenerListadoPacientesResponse>
{
    public async Task<ObtenerListadoPacientesResponse> Handle(ObtenerListadoPacientesQuery request, CancellationToken cancellationToken)
    {
        var pacientes = (await pacienteRepository.ObtenerPacientesPaginatedAsync(request.NumeroPagina, request.NumeroFilas)).ToList();
        return new ObtenerListadoPacientesResponse
        {
            TotalPacientes = pacientes.Count(),
            Pacientes = pacientes.Select(p => new ObtenerPacienteResponse
            {
                PacienteId = p.PacienteId,
                TipoDocumentoId = p.TipoDocumentoId,
                NumeroDocumento = p.NumeroDocumento,
                NombrePaciente = p.Nombre,
                FechaNacimiento = p.FechaNacimiento,
                CorreoElectronico = p.CorreoElectronico,
                GeneroId = p.GeneroId,
                Direccion = p.Direccion,
                Telefono = p.Telefono
            }).ToList()
        };
    }
}
