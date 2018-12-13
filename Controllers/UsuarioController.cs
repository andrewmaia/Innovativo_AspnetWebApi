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

namespace TodoApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
      public class UsuarioController : ControllerBase
    {
        private readonly InnovativoContext _context;
        private readonly AppSettings _appSettings;

        public UsuarioController(InnovativoContext context,IOptions<AppSettings> appSettings)
        {
             _context = context;
            _appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost("autenticar")]
        public IActionResult Autenticar([FromBody]UsuarioLoginDTO usvm)
        {
            Usuario usuario;
            string mensagem = Autenticar(usvm.Usuario, usvm.Senha,out usuario);

            if (mensagem != string.Empty)
                return BadRequest(new { message = mensagem });

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(_appSettings.Segredo);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] 
                {
                    new Claim(ClaimTypes.Name, usuario.ID.ToString())
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
                Token = tokenString
            });
        }

        public string Autenticar(string email, string senha, out Usuario usuario)
        {
            usuario =null;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(senha))
                return "Para autenticar é necessário fornecer email e senha";

            usuario = _context.Usuario.SingleOrDefault(x => x.Email == email);

            if (usuario == null)
                return string.Format("Email {0} não foi encontrado", email);

            if (usuario.Senha!= senha)
                return string.Format("Senha incorreta",email);

            return string.Empty;
        }

    }
}
