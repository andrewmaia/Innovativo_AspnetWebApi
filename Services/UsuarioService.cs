using System;
using System.Collections.Generic;
using System.Linq;
using Innovativo.Models;
using Innovativo;
using System.Text;

namespace Innovativo.Services
{
    public interface IUsuarioService
    {
        string Autenticar(string email, string senha, out Usuario usuario);
        Usuario ObterPorID(int id);    
    }
 
    public class UsuarioService : IUsuarioService
    {
        private readonly InnovativoContext _context;
        public UsuarioService(InnovativoContext context)
        {
             _context = context;
        }        
        public string Autenticar(string email, string senha, out Usuario usuario)
        {
            usuario =null;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(senha))
                return "Para autenticar é necessário fornecer email e senha";

            usuario = _context.Usuario.SingleOrDefault(x => x.Email == email);

            if (usuario == null)
                return string.Format("Email {0} não foi encontrado", email);


             if (usuario.Senha!= SenhaCriptografada(senha))
                return string.Format("Senha incorreta",email);

            return string.Empty;
        }

        public Usuario ObterPorID(int id)
        {
            return _context.Usuario.FirstOrDefault(x=>x.ID==id);
        }

        private string SenhaCriptografada(string senha)
        {
            Encoding enc = Encoding.GetEncoding(65001);
            byte[] buffer = enc.GetBytes(senha);

            var sha1 = System.Security.Cryptography.SHA1.Create();
            var hash = sha1.ComputeHash(buffer);
            return enc.GetString(hash);
        }

    }
}