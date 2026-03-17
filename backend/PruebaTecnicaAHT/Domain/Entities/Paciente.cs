using System.Collections.ObjectModel;

namespace PruebaTecnicaAHT.Domain.Entities;

public class Paciente
{
    public int PacienteId { get; private set; }
    public int TipoDocumentoId { get; private set; }
    public string NumeroDocumento { get; private set; }
    public string Nombre { get; private set; }
    public DateTime FechaNacimiento { get; private set; }
    public string? CorreoElectronico { get; private set; }
    public int GeneroId { get; private set; }
    public string Direccion { get; private set; }
    public string Telefono { get; private set; }
    public bool Activo { get; private set; }
    public virtual TipoDocumento TipoDocumento { get; set; } = null!;
    public virtual Genero Genero { get; set; } = null!;
    
    protected Paciente(){}
    
    public static Paciente Crear(int tipoDocumentoId, string numeroDocumento, string nombre, DateTime fechaNacimiento, string? correoElectronico, int generoId, string direccion, string telefono)
    {
        //TODO: agregar validaciones de dominio.
        return new Paciente(tipoDocumentoId, numeroDocumento, nombre, fechaNacimiento, correoElectronico, generoId, direccion, telefono);
    }

    public void Actualizar(int tipoDocumentoId, string numeroDocumento, string nombre, DateTime fechaNacimiento, string? correoElectronico, int generoId, string direccion, string telefono)
    {
        //TODO: agregar validaciones de dominio.
        TipoDocumentoId = tipoDocumentoId;
        NumeroDocumento = numeroDocumento;
        Nombre = nombre;
        FechaNacimiento = fechaNacimiento;
        CorreoElectronico = correoElectronico;
        GeneroId = generoId;
        Direccion = direccion;
        Telefono = telefono;
        Activo = true;
    }
    
    private Paciente(int tipoDocumentoId, string numeroDocumento, string nombre, DateTime fechaNacimiento, string? correoElectronico, int generoId, string direccion, string telefono)
    {
        TipoDocumentoId = tipoDocumentoId;
        NumeroDocumento = numeroDocumento;
        Nombre = nombre;
        FechaNacimiento = fechaNacimiento;
        CorreoElectronico = correoElectronico;
        GeneroId = generoId;
        Direccion = direccion;
        Telefono = telefono;
        Activo = true;
    }
}
