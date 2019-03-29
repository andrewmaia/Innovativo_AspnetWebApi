using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Innovativo.Models;
using Innovativo.DTO;
using Microsoft.AspNetCore.Cors;
using Innovativo;
using System.Data;
using Microsoft.AspNetCore.Authorization;
using System.Text;
using System;
using Innovativo.Services;

namespace TodoApi.Controllers
{
    [Authorize(Roles="admin")]
    [Route("api/[controller]")]
    [ApiController]
      public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }


        [AllowAnonymous]
        [HttpPost("autenticar")]
        public IActionResult Autenticar([FromBody]UsuarioLoginDTO usvm)
        {
            UsuarioLogadoDTO  ulDTO  ;

            if (!_usuarioService.Autenticar(usvm.Usuario, usvm.Senha,out ulDTO ))
                return Unauthorized();

            return Ok(ulDTO);
        }

        [HttpPost()]
        public ActionResult<UsuarioDTO> Inserir(UsuarioDTO dto)
        {
            dto.ID = _usuarioService.Inserir(dto);
            return dto;
        }        

        [HttpGet]
        public ActionResult<List<UsuarioDTO>> GetAll()
        {
            return _usuarioService.Listar();
        }

        [HttpGet("{id}")]
        public ActionResult<UsuarioDTO> GetById(int id)
        {
            UsuarioDTO dto = _usuarioService.ObterPorIdDTO(id);
             if (dto == null)
                return NotFound();

            return dto;
        } 

        [HttpPut("{id}")]
        public IActionResult Update(int id, UsuarioDTO dto)
        {
            _usuarioService.Alterar(id,dto);
            return NoContent();
        }

    }
}
