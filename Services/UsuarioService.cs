using System;
using System.Collections.Generic;
using System.Linq;
using Innovativo.Models;
using Innovativo;
using System.Text;
using Innovativo.DTO;
using AutoMapper;

namespace Innovativo.Services
{
    public interface IUsuarioService
    {
        string Autenticar(string email, string senha, out Usuario usuario);
        Usuario ObterPorID(int id);
        UsuarioDTO ObterPorIdDTO(int id); 
        int Criar(UsuarioDTO dto);
        List<UsuarioDTO> Listar();
        void Alterar (int id, UsuarioDTO dto);
    }
 
    public class UsuarioService : IUsuarioService
    {
        private readonly InnovativoContext _context;
        private readonly IMapper _mapper;
        public UsuarioService(InnovativoContext context,IMapper mapper)
        {
             _context = context;
             _mapper = mapper;
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

        private string SenhaCriptografada(string senha)
        {
            Encoding enc = Encoding.GetEncoding(65001);
            byte[] buffer = enc.GetBytes(senha);

            var sha1 = System.Security.Cryptography.SHA1.Create();
            var hash = sha1.ComputeHash(buffer);
            return enc.GetString(hash);
        }

        public int Criar(UsuarioDTO dto)
        {
            Usuario u =_mapper.Map<Usuario>(dto); 
            _context.Usuario.Add(u);
            _context.SaveChanges();
            return u.ID;
        }

        public void Alterar (int id, UsuarioDTO dto)
        {
            Usuario u = _context.Usuario.FirstOrDefault(x=>x.ID==id);
            if (u==null)
                return;
            
            u.Nome= dto.Nome;
            u.Email= dto.Email;
            u.Senha = dto.Senha;
            u.ClienteID = dto.ClienteID;

            _context.Usuario.Update(u);
            _context.SaveChanges();            
        }

        public List<UsuarioDTO> Listar()
        {
            return _mapper.Map<List<UsuarioDTO>>(_context.Usuario.Where(x=>x.ClienteID.HasValue).ToList());
        }

    }
}