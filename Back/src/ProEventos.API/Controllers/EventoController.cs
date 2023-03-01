using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProEventos.API.Models;

namespace ProEventos.API.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class EventoController : ControllerBase
  {
    public IEnumerable<Evento> _evento = new Evento[] {
        new Evento() {
        EventoId = 1,
        Tema = "Angula 11",
        Local = "Juiz de Fora",
        Lote = "Primeiro Lote",
        ImagemUrl = "image.png",
        QtdPessoas = 250,
        DataEvento = DateTime.Now.AddDays(2).ToString("dd/MM/yyyy")
        },
        new Evento() {
        EventoId = 2,
        Tema = "Angula 12",
        Local = "Juiz de Fora",
        Lote = "Primeiro Lote",
        ImagemUrl = "image.png",
        QtdPessoas = 350,
        DataEvento = DateTime.Now.AddDays(2).ToString("dd/MM/yyyy")
        }
      };
    public EventoController()
    {

    }

    [HttpGet]
    public IEnumerable<Evento> Get()
    {
      return _evento;
    }

    [HttpGet("{id}")]
    public IEnumerable<Evento> GetById(int id)
    {
      return _evento.Where(evento => evento.EventoId == id);
    }
  }
}
