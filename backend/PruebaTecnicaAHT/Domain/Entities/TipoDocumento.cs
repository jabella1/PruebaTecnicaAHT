namespace PruebaTecnicaAHT.Domain.Entities;

public class TipoDocumento
{
    public int TipoDocumentoId { get; set; }
    public required string Tipo { get; set; }
    public virtual ICollection<Paciente> Pacientes { get; set; } = [];
}