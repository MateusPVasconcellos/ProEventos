using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProEventos.API.Dtos;
using ProEventos.Application.Contratos;
using ProEventos.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventosController : ControllerBase
    {
        private readonly IEventosService _eventosService;
        public EventosController(IEventosService eventosService)
        {
            _eventosService = eventosService;

        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var eventos = await _eventosService.GetAllEventosAsync(true);
                if (eventos == null) return NotFound("Nenhum evento encontrado.");

                var eventoRetorno = new List<EventoDto>();

                foreach (var evento in eventos)
                {
                    eventoRetorno.Add(new EventoDto
                    {
                        Id = evento.Id,
                        Local = evento.Local,
                        DataEvento = evento.DataEvento.ToString(),
                        Tema = evento.Tema,
                        QtdPessoas = evento.QtdPessoas,
                        ImagemUrl = evento.ImagemUrl,
                        Telefone = evento.Telefone,
                        Email = evento.Email,
                    });
                }

                return Ok(eventos);
            }
            catch (Exception ex)
            {
                return StatusCode(
                  StatusCodes.Status500InternalServerError,
                  $"Erro ao tentar recuperar eventos. Erro: {ex.Message}"
                );
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var evento = await _eventosService.GetEventoByIdAsync(id, true);
                if (evento == null) return NotFound("Nenhum evento encontrado.");

                return Ok(evento);
            }
            catch (Exception ex)
            {
                return StatusCode(
                  StatusCodes.Status500InternalServerError,
                  $"Erro ao tentar recuperar eventos. Erro: {ex.Message}"
                );
            }
        }

        [HttpGet("tema/{tema}")]
        public async Task<IActionResult> GetByTema(string tema)
        {
            try
            {
                var evento = await _eventosService.GetEventosByTemaAsync(tema, true);
                if (evento == null) return NotFound("Nenhum evento encontrado.");

                return Ok(evento);
            }
            catch (Exception ex)
            {
                return StatusCode(
                  StatusCodes.Status500InternalServerError,
                  $"Erro ao tentar recuperar eventos. Erro: {ex.Message}"
                );
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Evento model)
        {
            try
            {
                var evento = await _eventosService.AddEvento(model);
                if (evento == null) return BadRequest("Erro ao adicionar evento.");

                return Ok(evento);
            }
            catch (Exception ex)
            {
                return StatusCode(
                  StatusCodes.Status500InternalServerError,
                  $"Erro ao tentar adicionar eventos. Erro: {ex.Message}"
                );
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Evento model)
        {
            try
            {
                var evento = await _eventosService.UpdateEvento(model, id);
                if (evento == null) return BadRequest("Erro ao atualizar evento.");

                return Ok(evento);
            }
            catch (Exception ex)
            {
                return StatusCode(
                  StatusCodes.Status500InternalServerError,
                  $"Erro ao tentar atualizar eventos. Erro: {ex.Message}"
                );
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (await _eventosService.DeleteEvento(id)) return Ok("Deletado");
                else return BadRequest("Erro ao deletar evento.");
            }
            catch (Exception ex)
            {
                return StatusCode(
                  StatusCodes.Status500InternalServerError,
                  $"Erro ao tentar deletar eventos. Erro: {ex.Message}"
                );
            }
        }
    }
}
