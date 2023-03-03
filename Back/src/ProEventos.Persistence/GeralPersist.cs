using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Contextos;
using ProEventos.Persistence.Contratos;

namespace ProEventos.Persistence
{
  public class GeralPersist : IGeralPersist
  {
    private readonly ProEventosContext _context;
    public GeralPersist(ProEventosContext context)
    {
      this._context = context;

    }
    public void Add<T>(T entity) where T : class
    {
      _context.Add(entity);
    }

    public void Update<T>(T entity) where T : class
    {
      _context.Update(entity);
    }

    public void Delete<T>(T entity) where T : class
    {
      _context.Remove(entity);
    }

    public void DeleteRange<T>(T[] entityArray) where T : class
    {
      _context.RemoveRange(entityArray);
    }

    public async Task<bool> SaveChangesAsync()
    {
      return (await _context.SaveChangesAsync()) > 0;
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

    public async Task<Palestrante> GetAllPalestranteByIdAsync(int palestranteId, bool includeEventos = false)
    {
      IQueryable<Palestrante> query = _context.Palestrantes.Include(p => p.RedesSociais);

      if (includeEventos)
      {
        query = query.Include(p => p.PalestrantesEventos).ThenInclude(pe => pe.Evento);
      }

      query = query.OrderBy(p => p.Id).Where(p => p.Id == palestranteId);

      return await query.FirstOrDefaultAsync();
    }

    public async Task<Palestrante[]> GetAllPalestrantesAsync(bool includeEventos)
    {
      IQueryable<Palestrante> query = _context.Palestrantes.Include(p => p.RedesSociais);

      if (includeEventos)
      {
        query = query.Include(p => p.PalestrantesEventos).ThenInclude(pe => pe.Evento);
      }

      query = query.OrderBy(p => p.Id);

      return await query.ToArrayAsync();
    }

    public async Task<Palestrante[]> GetAllPalestrantesByNomeAsync(string nome, bool includeEventos)
    {
      IQueryable<Palestrante> query = _context.Palestrantes.Include(p => p.RedesSociais);

      if (includeEventos)
      {
        query = query.Include(p => p.PalestrantesEventos).ThenInclude(pe => pe.Evento);
      }

      query = query.OrderBy(p => p.Id).Where(p => p.Nome.ToLower().Contains(nome.ToLower()));

      return await query.ToArrayAsync();
    }
  }
}