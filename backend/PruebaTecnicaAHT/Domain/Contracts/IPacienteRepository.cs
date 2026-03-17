using PruebaTecnicaAHT.Domain.Entities;

namespace PruebaTecnicaAHT.Domain.Contracts;

public interface IPacienteRepository
{
    Task CrearPacienteAsync(Paciente paciente);
    Task<Paciente?> ObtenerPacientePorIdAsync(int pacienteId);
    Task EliminarPacienteAsync(int pacienteId);
    Task ActualizarPacienteAsync(Paciente paciente);
    Task<IEnumerable<Paciente>> ObtenerPacientesPaginatedAsync(int pageNumber, int pageSize);
}