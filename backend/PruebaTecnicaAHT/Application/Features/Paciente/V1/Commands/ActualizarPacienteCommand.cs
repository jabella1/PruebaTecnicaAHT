using Ardalis.GuardClauses;
using MediatR;
using PruebaTecnicaAHT.Application.Features.Paciente.V1.DTOs;

namespace PruebaTecnicaAHT.Application.Features.Paciente.V1.Commands;

public class ActualizarPacienteCommand(int pacienteId, int tipoDocumentoId, DateTime fechaNacimiento, string? correoElectronico,
        int generoId, string direccion, string telefono
) : IRequest<ActualizarPacienteResponse>
{
        public int PacienteId { get; set; } = Guard.Against.NegativeOrZero(pacienteId, nameof(PacienteId), "El ID del paciente no puede ser negativo o cero.");
        public int TipoDocumentoId { get; set; } = Guard.Against.NegativeOrZero(tipoDocumentoId, nameof(TipoDocumentoId), "Este campo no puede ser negativo o cero.");
        public string NumeroDocumento { get; set; } = string.Empty;
        public string NombrePaciente { get; set; } = string.Empty;
        public DateTime FechaNacimiento { get; set; } = fechaNacimiento;
        public string? CorreoElectronico { get; set; } = correoElectronico;
        public int GeneroId { get; set; } = Guard.Against.NegativeOrZero(generoId, nameof(GeneroId), "Este campo no puede ser negativo o cero.");
        public string Direccion { get; set; } = direccion;
        public string Telefono { get; set; } = telefono;
    
}