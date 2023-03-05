using System.Threading.Tasks;
using ProEventos.Domain;

namespace ProEventos.Persistence.Contratos
{
  public interface IEventoPersist
  {
    Task<Evento[]> GetEventosByTemaAsync(string tema, bool includePalestrantes);
    Task<Evento[]> GetAllEventosAsync(bool includePalestrantes);
    Task<Evento> GetEventoByIdAsync(int eventoId, bool includePalestrantes);
  }
}