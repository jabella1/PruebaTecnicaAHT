namespace PruebaTecnicaAHT.Application.Features.Paciente.V1.DTOs;

public class ObtenerListadoPacientesResponse
{
    public int TotalPacientes { get; set; }
    public List<ObtenerPacienteResponse> Pacientes { get; set; } = new List<ObtenerPacienteResponse>();
}
