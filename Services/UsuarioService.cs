using System;
using System.Collections.Generic;
using System.Linq;
using Innovativo.Models;
using Innovativo;
using System.Text;
using Innovativo.DTO;
using AutoMapper;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Security.Cryptography;


namespace Innovativo.Services
{
    public interface IUsuarioService
    {
        bool Autenticar(string email, string senha,out UsuarioLogadoDTO usuarioLogadoDTO );
        Usuario ObterPorID(int id);
        UsuarioDTO ObterPorIdDTO(int id); 
        int Inserir(UsuarioDTO dto);
        List<UsuarioDTO> Listar();
        bool Alterar (int id, UsuarioDTO dto);
    }
 
    public class UsuarioService : IUsuarioService
    {
        private readonly InnovativoContext _context;
        private readonly IMapper _mapper;

         private readonly AppSettings _appSettings;        
        public UsuarioService(InnovativoContext context,IMapper mapper,IOptions<AppSettings> appSettings)
        {
             _context = context;
             _mapper = mapper;
            _appSettings = appSettings.Value;
        }        

        public bool Autenticar(string email, string senha,out UsuarioLogadoDTO usuarioLogadoDTO )
        {
            usuarioLogadoDTO=null;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(senha))
                return false;

            Usuario usuario = _context.Usuario.SingleOrDefault(x => x.Email == email && Convert.ToBase64String(x.Senha)==Convert.ToBase64String(SenhaCriptografada(senha)));
            if (usuario == null)
                return false;

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(_appSettings.Segredo);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(ObterClaims(usuario)),
                Expires = DateTime.UtcNow.AddDays(_appSettings.ExpiracaoToken),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            usuarioLogadoDTO= new UsuarioLogadoDTO {
                ID = usuario.ID,
                Usuario = usuario.Email,
                Nome = usuario.Nome,
                Token = tokenString,
                Papeis = usuario.ObterPapeis()
            };

            return true;
        }

        private Claim[] ObterClaims(Usuario usuario)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name,usuario.ID.ToString()));
            foreach (string papel in usuario.ObterPapeis())
            {
                claims.Add(new Claim(ClaimTypes.Role,papel));
            }
            return claims.ToArray();
        }

        public Usuario ObterPorID(int id)
        {
            return _context.Usuario.FirstOrDefault(x=>x.ID==id);
        }

        public UsuarioDTO ObterPorIdDTO(int id)
        {
            Usuario u = ObterPorID(id);
            if (u==null)
                return null;
            
            return _mapper.Map<UsuarioDTO>(u);
        }

        private byte[] SenhaCriptografada(string senha)
        {
            Encoding enc = Encoding.GetEncoding(65001);
            byte[] buffer = enc.GetBytes(senha);

            SHA1 sha1 = SHA1.Create();
            return sha1.ComputeHash(buffer);
        }
        

        public int Inserir(UsuarioDTO dto)
        {
            Usuario u =_mapper.Map<Usuario>(dto);
            u.Senha = SenhaCriptografada(dto.Senha);
            _context.Usuario.Add(u);
            _context.SaveChanges();
            return u.ID;
        }

        public bool Alterar (int id, UsuarioDTO dto)
        {
            Usuario u = _context.Usuario.FirstOrDefault(x=>x.ID==id);
            if (u==null)
                return false;
            
            u.Nome= dto.Nome;
            u.Email= dto.Email;
            u.ClienteID = dto.ClienteID;

            if(!String.IsNullOrEmpty(dto.Senha))
                u.Senha = SenhaCriptografada(dto.Senha);

            _context.Usuario.Update(u);
            _context.SaveChanges();
            return true;
        }


        public List<UsuarioDTO> Listar()
        {
            return _mapper.Map<List<UsuarioDTO>>(_context.Usuario.Where(x=>x.ClienteID.HasValue).ToList());
        }

    }
}