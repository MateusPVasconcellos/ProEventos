using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProEventos.Domain;

namespace ProEventos.Application.Contratos
{
  public interface IEventosService
  {
    Task<Evento> AddEvento(Evento model);
    Task<Evento> UpdateEvento(Evento model, int eventoId);
    Task<bool> DeleteEvento(int eventoId);
    Task<Evento[]> GetEventosByTemaAsync(string tema, bool includePalestrantes);
    Task<Evento[]> GetAllEventosAsync(bool includePalestrantes);
    Task<Evento> GetEventoByIdAsync(int eventoId, bool includePalestrantes);

  }
}