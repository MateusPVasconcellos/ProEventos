using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProEventos.Application.Contratos;
using ProEventos.Application.Dtos;
using System;
using System.Threading.Tasks;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LotesController : ControllerBase
    {
        private readonly ILotesService _lotesService;
        public LotesController(ILotesService lotesService)
        {
            _lotesService = lotesService;

        }

        [HttpGet("{eventoId}")]
        public async Task<IActionResult> Get(int eventoId)
        {
            try
            {
                var lotes = await _lotesService.GetLotesByEventoIdAsync(eventoId);
                if (lotes == null) return NotFound("Nenhum evento encontrado.");

                return Ok(lotes);
            }
            catch (Exception ex)
            {
                return StatusCode(
                  StatusCodes.Status500InternalServerError,
                  $"Erro ao tentar recuperar lotes. Erro: {ex.Message}"
                );
            }
        }

        [HttpPut("{eventoId}")]
        public async Task<IActionResult> Put(int eventoId, LoteDto[] model)
        {
            try
            {
                var evento = await _lotesService.SaveLotes(eventoId, model);
                if (evento == null) return BadRequest("Erro ao atualizar lote.");

                return Ok(evento);
            }
            catch (Exception ex)
            {
                return StatusCode(
                  StatusCodes.Status500InternalServerError,
                  $"Erro ao tentar atualizar lotes. Erro: {ex.Message}"
                );
            }
        }

        [HttpDelete("{eventoId}/{loteId}")]
        public async Task<IActionResult> Delete(int eventoId, int LoteId)
        {
            try
            {
                if (await _lotesService.DeleteLote(eventoId, LoteId)) return Ok(new { menssagem = "Deletado" });
                else return BadRequest("Erro ao deletar evento.");
            }
            catch (Exception ex)
            {
                return StatusCode(
                  StatusCodes.Status500InternalServerError,
                  $"Erro ao tentar deletar lotes. Erro: {ex.Message}"
                );
            }
        }
    }
}
