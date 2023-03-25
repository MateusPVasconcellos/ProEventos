using ProEventos.Application.Dtos;
using System.Threading.Tasks;

namespace ProEventos.Application.Contratos
{
    public interface IEventosService
    {
        Task<EventoDto> AddEvento(EventoDto model);
        Task<EventoDto> UpdateEvento(EventoDto model, int eventoId);
        Task<bool> DeleteEvento(int eventoId);
        Task<EventoDto[]> GetEventosByTemaAsync(string tema, bool includePalestrantes);
        Task<EventoDto[]> GetAllEventosAsync(bool includePalestrantes);
        Task<EventoDto> GetEventoByIdAsync(int eventoId, bool includePalestrantes);

    }
}