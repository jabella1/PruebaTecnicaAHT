using System.Data;
using Dapper;
using PruebaTecnicaAHT.Domain.Contracts;
using PruebaTecnicaAHT.Domain.Entities;

namespace PruebaTecnicaAHT.Infrastructure.Repositories;

public class GeneroRepository(IDbConnection dbConnection) : IGeneroRepository
{
    public Task<IEnumerable<Genero>> ObtenerGenerosAsync()
    {
        var query = @"SELECT * FROM Genero";
        return dbConnection.QueryAsync<Genero>(query);
    }

    public Task<Genero?> ObtenerGeneroPorIdAsync(int generoId)
    {
        var query = @"SELECT * FROM Genero WHERE GeneroId = @GeneroId";
        return dbConnection.QuerySingleOrDefaultAsync<Genero>(query, new { GeneroId = generoId });
    }
}