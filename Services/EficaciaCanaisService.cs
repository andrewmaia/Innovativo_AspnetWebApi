using System;
using System.Collections.Generic;
using System.Linq;
using Innovativo.Models;
using Innovativo;

namespace Innovativo.Services
{
    public interface IEficaciaCanaisService
    {
        IList<EficaciaCanaisRelatorio> SelecionarPorUsuario(int usuarioID);
    }
 
    public class EficaciaCanaisService : IEficaciaCanaisService
    {
        private readonly InnovativoContext _context;
        private readonly IUsuarioService _usuarioService;
        public EficaciaCanaisService(InnovativoContext context,IUsuarioService usuarioService)
        {
             _context = context;
             _usuarioService =usuarioService;
        }        
        public IList<EficaciaCanaisRelatorio> SelecionarPorUsuario(int usuarioID)
        {
            Usuario usuario =_usuarioService.ObterPorID(usuarioID);
            if (usuario==null)
                return null;

            if (usuario.ClienteID.HasValue)
                return usuario.Cliente.EficaciaCanalRelatorioLista;
            
            return _context.EficaciaCanaisRelatorio.ToList();
        }


    }
}