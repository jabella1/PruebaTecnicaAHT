using PruebaTecnicaAHT.Domain.Contracts;
using PruebaTecnicaAHT.Domain.Entities;
using PruebaTecnicaAHT.Domain.Validations;

namespace PruebaTecnicaAHT.Domain.DomainValidation;

public class Validation(IGeneroRepository generoRepository, ITipoDocumentoRepository tipoDocumentoRepository
, IPacienteRepository pacienteRepository) : IValidation
{
    public async Task ValidarGenero(int generoId)
    {
        var genero = await generoRepository.ObtenerGeneroPorIdAsync(generoId);
        if (genero == null)
        {
            throw new DomainValidationException($"No se encontró un género con el ID {generoId}.");
        }
    }

    public async Task ValidarTipoDocumento(int tipoDocumentoId)
    {
        var tipoDocumento = await tipoDocumentoRepository.ObtenerTipoDocumento(tipoDocumentoId);
        if (tipoDocumento == null)
        {
            throw new DomainValidationException($"No se encontró un tipo de documento con el ID {tipoDocumento}.");
        }
    }

    public async Task<Paciente> ValidarPacienteExistente(int pacienteId)
    {
        var paciente = await pacienteRepository.ObtenerPacientePorIdAsync(pacienteId);
        if (paciente == null)
        {
            throw new DomainValidationException($"No se encontró un paciente con el ID {pacienteId}.");
        }
        return paciente;
    }

    public void ValidarSoloNumerosEnString(string input)
    {
        if (!input.All(char.IsDigit))
        {
            throw new DomainValidationException("El campo número de documento debe contener solo números.");
        }
    }
    
    public void ValidarCorreoElectronico(string correoElectronico)
    {
        if (!correoElectronico.Contains("@") || !correoElectronico.Contains("."))
        {
            throw new DomainValidationException("El correo electrónico no es válido.");
        }
    }
}