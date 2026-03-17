namespace PruebaTecnicaAHT.Application.Features.Paciente.V1.DTOs;

public class ObtenerPacienteResponse
{
    public int PacienteId { get; set; }
    public int TipoDocumentoId { get; set; }
    public required string NumeroDocumento { get; set; }
    public required string NombrePaciente { get; set; }
    public DateTime FechaNacimiento { get; set; }
    public string? CorreoElectronico { get; set; }
    public int GeneroId { get; set; } 
    public required string Direccion { get; set; }
    public required string Telefono { get; set; }
}
