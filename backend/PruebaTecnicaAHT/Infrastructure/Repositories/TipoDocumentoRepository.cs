using System.Data;
using Dapper;
using PruebaTecnicaAHT.Domain.Contracts;
using PruebaTecnicaAHT.Domain.Entities;

namespace PruebaTecnicaAHT.Infrastructure.Repositories;

public class TipoDocumentoRepository(IDbConnection dbConnection) : ITipoDocumentoRepository
{
    public Task<IEnumerable<TipoDocumento>> ObtenerTiposDocumento()
    {
        var query = @"SELECT * FROM TipoDocumento";
        return dbConnection.QueryAsync<TipoDocumento>(query);
    }
    
    public Task<TipoDocumento?> ObtenerTipoDocumento(int id)
    {
        var query = @"SELECT * FROM TipoDocumento WHERE TipoDocumentoId = @Id";
        return dbConnection.QuerySingleOrDefaultAsync<TipoDocumento>(query, new { Id = id });
    }
}