using System.Threading.Tasks;
using ProEventos.Domain;

namespace ProEventos.Persistence.Contratos
{
  public interface IEventoPersist
  {
    Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes);
    Task<Evento[]> GetAllEventosAsync(bool includePalestrantes);
    Task<Evento> GetAllEventoByIdAsync(int EventoId, bool includePalestrantes);
  }
}