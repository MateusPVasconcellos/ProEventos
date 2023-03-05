using System.Threading.Tasks;
using ProEventos.Domain;

namespace ProEventos.Persistence.Contratos
{
  public interface IPalestrantePersist
  {
    Task<Palestrante[]> GetPalestrantesByNomeAsync(string nome, bool includeEventos);
    Task<Palestrante[]> GetAllPalestrantesAsync(bool includeEventos);
    Task<Palestrante> GetPalestranteByIdAsync(int palestranteId, bool includeEventos);
  }
}