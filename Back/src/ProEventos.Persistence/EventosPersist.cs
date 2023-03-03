using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Contextos;
using ProEventos.Persistence.Contratos;

namespace ProEventos.Persistence
{
  public class EventoPersist : IEventoPersist
  {
    private readonly ProEventosContext _context;
    public EventoPersist(ProEventosContext context)
    {
      this._context = context;

    }
    public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false)
    {
      IQueryable<Evento> query = _context.Eventos.Include(e => e.Lote).Include(e => e.RedesSociais);

      if (includePalestrantes)
      {
        query = query.Include(e => e.PalestrantesEventos).ThenInclude(pe => pe.Palestrante);
      }

      query = query.OrderBy(e => e.Id);

      return await query.ToArrayAsync();
    }

    public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
    {
      IQueryable<Evento> query = _context.Eventos.Include(e => e.Lote).Include(e => e.RedesSociais);

      if (includePalestrantes)
      {
        query = query.Include(e => e.PalestrantesEventos).ThenInclude(pe => pe.Palestrante);
      }

      query = query.OrderBy(e => e.Id).Where(e => e.Tema.ToLower().Contains(tema.ToLower()));

      return await query.ToArrayAsync();
    }

    public async Task<Evento> GetAllEventoByIdAsync(int EventoId, bool includePalestrantes = false)
    {
      IQueryable<Evento> query = _context.Eventos.Include(e => e.Lote).Include(e => e.RedesSociais);

      if (includePalestrantes)
      {
        query = query.Include(e => e.PalestrantesEventos).ThenInclude(pe => pe.Palestrante);
      }

      query = query.OrderBy(e => e.Id).Where(e => e.Id == EventoId);

      return await query.FirstOrDefaultAsync();
    }
  }
}