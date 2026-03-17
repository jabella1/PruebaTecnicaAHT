namespace PruebaTecnicaAHT.Domain.Entities;

public class Genero
{
    public int GeneroId { get; set; }
    public required string Nombre { get; set; }
    public ICollection<Paciente> Pacientes { get; set; } = [];
}