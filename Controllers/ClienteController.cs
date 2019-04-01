using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Innovativo.Models;
using Innovativo.DTO;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Primitives;
using Innovativo;
using System.Data;
using Microsoft.AspNetCore.Authorization;
using System.Text;
using AutoMapper;
using Innovativo.Services;

namespace Innovativo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles="admin")]   
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        public ActionResult<List<ClienteDTO>> Listar()
        {
            return _clienteService.Listar();
        }


        [HttpGet("{id}")]
        public ActionResult<ClienteDTO> ObterPorID(int id)
        {
            ClienteDTO dto= _clienteService.ObterPorID(id);
            if (dto==null)
                return NotFound();
                
            return dto;
        } 

        [HttpPut("{id}")]
        public IActionResult Alterar(int id, ClienteDTO dto)
        {
            if(_clienteService.Alterar(id, dto))
                return NoContent();
            else
                return NotFound();
        }

        [HttpPost()]
        public ActionResult<ClienteDTO> Inserir(ClienteDTO cvm)
        {
            ClienteDTO c =_clienteService.Inserir(cvm);
            return CreatedAtAction(nameof(ObterPorID),new {id=c.ID},c);
        }
    }
}
