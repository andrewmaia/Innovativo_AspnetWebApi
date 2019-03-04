using System;
using System.Collections.Generic;
using System.Linq;
using Innovativo.Models;
using Innovativo;
using Innovativo.DTO;

namespace Innovativo.Services
{
    public interface IEficaciaCanaisService
    {
        IList<EficaciaCanaisRelatorio> SelecionarPorUsuario(int usuarioID);
        void Inserir(EficaciaCanalDTO dto);
        EficaciaCanaisRelatorio ObterPorID(int id);
        bool PodeAcessarRelatorio(EficaciaCanaisRelatorio ecr,int usuarioID);
        List<EficaciaCanalRelatorioDTO> Listar(int usuarioID);
        EficaciaCanaisRelatorio ObterUltimoDoCliente(int usuarioID);
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
        public EficaciaCanaisRelatorio ObterPorID(int id)
        {
            return _context.EficaciaCanaisRelatorio.FirstOrDefault(x=>x.ID==id);
        }

        public EficaciaCanaisRelatorio ObterUltimoDoCliente(int usuarioID)
        {
            Usuario usuario =_usuarioService.ObterPorID(usuarioID);
            if (usuario==null)
                return null;

            if (usuario.ClienteID.HasValue)
                return usuario.Cliente.EficaciaCanalRelatorioLista.OrderByDescending(x=>x.DataFinal).FirstOrDefault();
            
            return _context.EficaciaCanaisRelatorio.OrderByDescending(x=>x.DataFinal).FirstOrDefault();            
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

        public bool PodeAcessarRelatorio(EficaciaCanaisRelatorio ecr,int usuarioID)
        {
            Usuario usuario = _usuarioService.ObterPorID(usuarioID);
            if(usuario.ClienteID.HasValue){
                if(ecr.IdCliente != usuario.ClienteID.Value)
                    return false;
            }
            return true;
        }

        public void Inserir(EficaciaCanalDTO dto)
        {
            EficaciaCanaisRelatorio ecr = new EficaciaCanaisRelatorio();
            ecr.IdCliente = dto.Cliente;
            ecr.Descricao = dto.Descricao;
            ecr.DataInicial = dto.DataInicial;
            ecr.DataFinal = dto.DataFinal;                        
            _context.EficaciaCanaisRelatorio.Add(ecr);

            EficaciaCanalBuscaPaga ecbp = new EficaciaCanalBuscaPaga();
            ecbp.EficaciaCanalID = ecr.ID;
            ecbp.Visitantes = dto.BuscaPagaVisitantes;
            ecbp.Leads = dto.BuscaPagaLeads;
            ecbp.Oportunidades = dto.BuscaPagaOportunidades;
            ecbp.Vendas = dto.BuscaPagaVendas;
            _context.EficaciaCanalBuscaPaga.Add(ecbp);

            EficaciaCanalDireto ecd = new EficaciaCanalDireto();
            ecd.EficaciaCanalID = ecr.ID;
            ecd.Visitantes = dto.DiretoVisitantes;
            ecd.Leads = dto.DiretoLeads;
            ecd.Oportunidades = dto.DiretoOportunidades;
            ecd.Vendas = dto.DiretoVendas;
            _context.EficaciaCanalDireto.Add(ecd);

            EficaciaCanalEmail ece = new EficaciaCanalEmail();
            ece.EficaciaCanalID = ecr.ID;
            ece.Visitantes = dto.EmailVisitantes;
            ece.Leads = dto.EmailLeads;
            ece.Oportunidades = dto.EmailOportunidades;
            ece.Vendas = dto.EmailVendas;
            _context.EficaciaCanalEmail.Add(ece);

            EficaciaCanalOrganico eco = new EficaciaCanalOrganico();
            eco.EficaciaCanalID = ecr.ID;
            eco.Visitantes = dto.OrganicoVisitantes;
            eco.Leads = dto.OrganicoLeads;
            eco.Oportunidades = dto.OrganicoOportunidades;
            eco.Vendas = dto.OrganicoVendas;
            _context.EficaciaCanalOrganico.Add(eco);

            EficaciaCanalReferencia ecref = new EficaciaCanalReferencia();
            ecref.EficaciaCanalID = ecr.ID;
            ecref.Visitantes = dto.ReferenciaVisitantes;
            ecref.Leads = dto.ReferenciaLeads;
            ecref.Oportunidades = dto.ReferenciaOportunidades;
            ecref.Vendas = dto.ReferenciaVendas;
            _context.EficaciaCanalReferencia.Add(ecref);            

            _context.SaveChanges();
        }
        

        public List<EficaciaCanalRelatorioDTO> Listar(int usuarioID)
        {
            List<EficaciaCanalRelatorioDTO> lista = new List<EficaciaCanalRelatorioDTO>();
            foreach(EficaciaCanaisRelatorio ecr in this.SelecionarPorUsuario(usuarioID)){
                lista.Add( new EficaciaCanalRelatorioDTO{ 
                    ID= ecr.ID,
                    Descricao=ecr.Descricao,
                    ClienteNomeFantasia= ecr.Cliente.NomeFantasia,
                    DataInicial= ecr.DataInicial,                    
                    DataFinal= ecr.DataFinal
                });
            }
            return lista;            
        }
        


    }
}