using System;
using System.Threading.Tasks;
using ProEventos.Application.Contratos;
using ProEventos.Domain;
using ProEventos.Persistence.Contratos;

namespace ProEventos.Application
{
  public class EventosService : IEventosService
  {
    private readonly IGeralPersist _geralPersist;
    private readonly IEventoPersist _eventoPersist;
    public EventosService(IGeralPersist geralPersist, IEventoPersist eventoPersist)
    {
      _eventoPersist = eventoPersist;
      _geralPersist = geralPersist;

    }
    public async Task<Evento> AddEvento(Evento model)
    {
      try
      {
        _geralPersist.Add<Evento>(model);

        if (await _geralPersist.SaveChangesAsync())
        {
          return await _eventoPersist.GetEventoByIdAsync(model.Id, false);
        }
        return null;
      }
      catch (Exception ex)
      {

        throw new Exception(ex.Message);
      }
    }
    public async Task<Evento> UpdateEvento(Evento model, int eventoId)
    {
      try
      {
        var evento = await _eventoPersist.GetEventoByIdAsync(eventoId, false);
        if (evento == null) return null;

        model.Id = evento.Id;

        _geralPersist.Update(model);

        if (await _geralPersist.SaveChangesAsync())
        {
          return await _eventoPersist.GetEventoByIdAsync(model.Id, false);
        }
        return null;
      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message);
      }
    }
    public async Task<bool> DeleteEvento(int eventoId)
    {
      try
      {
        var evento = await _eventoPersist.GetEventoByIdAsync(eventoId, false);
        if (evento == null) throw new Exception("Evento para delete não encontrado.");

        _geralPersist.Delete<Evento>(evento);

        return await _geralPersist.SaveChangesAsync();
      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message);
      }
    }

    public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes)
    {
      try
      {
        var eventos = await _eventoPersist.GetAllEventosAsync(includePalestrantes);
        if (eventos == null) return null;

        return eventos;
      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message);
      }
    }
    public async Task<Evento[]> GetEventosByTemaAsync(string tema, bool includePalestrantes)
    {
      try
      {
        var eventos = await _eventoPersist.GetEventosByTemaAsync(tema, includePalestrantes);
        if (eventos == null) return null;

        return eventos;
      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message);
      }
    }

    public async Task<Evento> GetEventoByIdAsync(int eventoId, bool includePalestrantes)
    {
      try
      {
        var evento = await _eventoPersist.GetEventoByIdAsync(eventoId, includePalestrantes);
        if (evento == null) return null;

        return evento;
      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message);
      }
    }

  }
}