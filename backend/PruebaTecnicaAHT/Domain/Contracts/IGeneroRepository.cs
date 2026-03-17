using PruebaTecnicaAHT.Domain.Entities;

namespace PruebaTecnicaAHT.Domain.Contracts;

public interface IGeneroRepository
{
    Task<IEnumerable<Genero>> ObtenerGenerosAsync();
    Task<Genero?> ObtenerGeneroPorIdAsync(int generoId);
}