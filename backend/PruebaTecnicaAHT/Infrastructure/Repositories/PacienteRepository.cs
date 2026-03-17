using System.Data;
using Dapper;
using PruebaTecnicaAHT.Domain.Contracts;
using PruebaTecnicaAHT.Domain.Entities;

namespace PruebaTecnicaAHT.Infrastructure.Repositories;

public class PacienteRepository(IDbConnection dbConnection) : IPacienteRepository
{
    public Task CrearPacienteAsync(Paciente paciente)
    {
        var query = @"INSERT INTO Paciente (TipoDocumentoId, NumeroDocumento, Nombre, FechaNacimiento, CorreoElectronico, GeneroId, Direccion, Telefono, Activo)
                      VALUES (@TipoDocumentoId, @NumeroDocumento, @Nombre, @FechaNacimiento, @CorreoElectronico, @GeneroId, @Direccion, @Telefono, @Activo)";

        var parameters = new
        {
            paciente.TipoDocumentoId,
            paciente.NumeroDocumento,
            paciente.Nombre,
            paciente.FechaNacimiento,
            paciente.CorreoElectronico,
            paciente.GeneroId,
            paciente.Direccion,
            paciente.Telefono,
            paciente.Activo
        };

        return dbConnection.ExecuteAsync(query, parameters);
    }

    public Task<Paciente?> ObtenerPacientePorIdAsync(int pacienteId)
    {
        var query = @"SELECT * FROM Paciente WHERE PacienteId = @PacienteId";
        return dbConnection.QuerySingleOrDefaultAsync<Paciente>(query, new { PacienteId = pacienteId });
    }
    
    public Task EliminarPacienteAsync(int pacienteId)
    {
        var query = @"DELETE FROM Paciente WHERE PacienteId = @PacienteId";
        return dbConnection.ExecuteAsync(query, new { PacienteId = pacienteId });
    }

    public Task ActualizarPacienteAsync(Paciente paciente)
    {
        var query = @"UPDATE Paciente SET TipoDocumentoId = @TipoDocumentoId, NumeroDocumento = @NumeroDocumento, Nombre = @Nombre, 
                      FechaNacimiento = @FechaNacimiento, CorreoElectronico = @CorreoElectronico, GeneroId = @GeneroId, 
                      Direccion = @Direccion, Telefono = @Telefono, Activo = @Activo WHERE PacienteId = @PacienteId";

        var parameters = new
        {
            paciente.TipoDocumentoId,
            paciente.NumeroDocumento,
            paciente.Nombre,
            paciente.FechaNacimiento,
            paciente.CorreoElectronico,
            paciente.GeneroId,
            paciente.Direccion,
            paciente.Telefono,
            paciente.Activo,
            paciente.PacienteId
        };

        return dbConnection.ExecuteAsync(query, parameters);
    }

    public Task<IEnumerable<Paciente>> ObtenerPacientesPaginatedAsync(int pageNumber, int pageSize)
    {
        var query = @"SELECT * FROM Paciente ORDER BY PacienteId OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";
        var parameters = new
        {
            Offset = (pageNumber - 1) * pageSize,
            PageSize = pageSize
        };
        return dbConnection.QueryAsync<Paciente>(query, parameters);
    }
}