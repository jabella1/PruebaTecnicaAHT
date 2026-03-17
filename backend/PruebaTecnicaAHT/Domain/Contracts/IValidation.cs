using PruebaTecnicaAHT.Domain.Entities;

namespace PruebaTecnicaAHT.Domain.Contracts;

public interface IValidation
{ 
    Task ValidarGenero(int generoId);
    Task ValidarTipoDocumento(int tipoDocumentoId);
    Task<Paciente> ValidarPacienteExistente(int pacienteId);
    void ValidarSoloNumerosEnString(string input);
    void ValidarCorreoElectronico(string correoElectronico);
}