using AutoMapper;
using ProEventos.Application.Contratos;
using ProEventos.Application.Dtos;
using ProEventos.Domain;
using ProEventos.Persistence.Contratos;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProEventos.Application
{
    public class LotesService : ILotesService
    {
        private readonly IGeralPersist _geralPersist;
        private readonly IMapper _mapper;
        private readonly ILotePersist _lotePersist;
        public LotesService(IGeralPersist geralPersist, ILotePersist lotePersist, IMapper mapper)
        {
            _lotePersist = lotePersist;
            _geralPersist = geralPersist;
            _mapper = mapper;

        }

        public async Task AddLote(int eventoId, LoteDto model)
        {
            try {
                var lote = _mapper.Map<Lote>(model);
                lote.EventoId = eventoId;

                _geralPersist.Add<Lote>(lote);

                await _geralPersist.SaveChangesAsync();
            } catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteLote(int eventoId, int loteId)
        {
            try
            {
                var lote = await _lotePersist.GetLotesByIdsAsync(eventoId, loteId);
                if (lote == null) throw new Exception("Lote não encontrado");

                _geralPersist.Delete(lote);

                return await _geralPersist.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<LoteDto> GetLoteByIdsAsync(int eventoId, int loteId)
        {
            try {
                var lote = await _lotePersist.GetLotesByIdsAsync(eventoId, loteId);
                if (lote == null) return null;

                var resultado = _mapper.Map<LoteDto>(lote);

                return resultado;

            } catch(Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        public async Task<LoteDto[]> GetLotesByEventoIdAsync(int eventoId)
        {
            try
            {
                var lotes = await _lotePersist.GetLotesByEventoIdAsync(eventoId);
                if (lotes == null) return null;

                var resultado = _mapper.Map<LoteDto[]>(lotes);

                return resultado;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<LoteDto[]> SaveLotes(int eventoId, LoteDto[] model)
        {
            try
            {
                var lotes = await _lotePersist.GetLotesByEventoIdAsync(eventoId);
                if (lotes == null) return null;

                foreach (var m in model) { 
                    if (m.Id == 0)
                    {
                        await AddLote(eventoId, m);
                    }
                    else
                    {
                        var lote = lotes.FirstOrDefault(lote => lote.Id == m.Id);
                        m.EventoId = eventoId;
                        _mapper.Map(m, lote);
                        _geralPersist.Update(lote);

                        await _geralPersist.SaveChangesAsync();
                    }
                }

                var resultado = await _lotePersist.GetLotesByEventoIdAsync(eventoId);
                return _mapper.Map<LoteDto[]>(resultado);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}