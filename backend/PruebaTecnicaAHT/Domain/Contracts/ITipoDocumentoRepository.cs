using PruebaTecnicaAHT.Domain.Entities;

namespace PruebaTecnicaAHT.Domain.Contracts;

public interface ITipoDocumentoRepository
{
    Task<IEnumerable<TipoDocumento>> ObtenerTiposDocumento();
    Task<TipoDocumento?> ObtenerTipoDocumento(int id);
}