using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Innovativo.Models;
using Innovativo.DTO;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Primitives;
using Innovativo;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System;
using Microsoft.Extensions.Options;
using Innovativo.Services;

namespace TodoApi.Controllers
{
    [Authorize(Roles="adm")]
    [Route("api/[controller]")]
    [ApiController]
      public class UsuarioController : ControllerBase
    {
         private readonly AppSettings _appSettings;
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IOptions<AppSettings> appSettings,IUsuarioService usuarioService)
        {
             _appSettings = appSettings.Value;
            _usuarioService = usuarioService;
        }


        [AllowAnonymous]
        [HttpPost("autenticar")]
        public IActionResult Autenticar([FromBody]UsuarioLoginDTO usvm)
        {
            Usuario usuario;
            string mensagem = _usuarioService.Autenticar(usvm.Usuario, usvm.Senha,out usuario);

            if (mensagem != string.Empty)
                return BadRequest(new { message = mensagem });

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(_appSettings.Segredo);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] 
                {
                    new Claim(ClaimTypes.Name, usuario.ID.ToString()),
                    new Claim(ClaimTypes.Role, usuario.ObterPapel() )                    
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new {
                Id = usuario.ID,
                Usuario = usuario.Email,
                Nome = usuario.Nome,
                Token = tokenString,
                Papeis = (!usuario.ClienteID.HasValue?"admin":string.Empty)
            });
        }

        [HttpPost()]
        public ActionResult<UsuarioDTO> Create(UsuarioDTO dto)
        {
            dto.ID = _usuarioService.Criar(dto);
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
