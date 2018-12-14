using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Innovativo.Models;
using Innovativo.DTO;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Primitives;
using Microsoft.AspNetCore.Authorization;
using Innovativo;
using System.Data;
using Innovativo.Services;
namespace TodoApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
      public class EficaciaCanaisController : ControllerBase
    {
        private readonly InnovativoContext _context;
        private readonly IEficaciaCanaisService _eficaciaCanaisService;
        private readonly IUsuarioService _usuarioService;        

        public EficaciaCanaisController(
            InnovativoContext context,
            IEficaciaCanaisService eficaciaCanaisService,
            IUsuarioService usuarioService)
        {
             _context = context;
             _eficaciaCanaisService =eficaciaCanaisService;
             _usuarioService= usuarioService;
        }

        [HttpGet("{id}")]
        public ActionResult<EficaciaCanalDTO> GetById(int id)
        {
            EficaciaCanaisRelatorio ecr = _context.EficaciaCanaisRelatorio.Find(id);
            if (ecr == null)
                return NotFound();

            int usuarioID = int.Parse(HttpContext.User.Identity.Name);  
            Usuario usuario = _usuarioService.ObterPorID(usuarioID);
            if(usuario.ClienteID.HasValue){
                if(ecr.IdCliente != usuario.ClienteID.Value)
                    return Forbid();
            }

            EficaciaCanalDTO ecvm = new EficaciaCanalDTO();
            ecvm.BuscaPagaLeads = ecr.BuscaPaga.Leads;
            ecvm.BuscaPagaOportunidades = ecr.BuscaPaga.Oportunidades;
            ecvm.BuscaPagaVendas = ecr.BuscaPaga.Vendas;
            ecvm.BuscaPagaVisitantes = ecr.BuscaPaga.Visitantes;

            ecvm.DiretoLeads = ecr.Direto.Leads;
            ecvm.DiretoOportunidades = ecr.Direto.Oportunidades;
            ecvm.DiretoVendas = ecr.Direto.Vendas;
            ecvm.DiretoVisitantes = ecr.Direto.Visitantes;

            ecvm.EmailLeads = ecr.Email.Leads;
            ecvm.EmailOportunidades = ecr.Email.Oportunidades;
            ecvm.EmailVendas = ecr.Email.Vendas;
            ecvm.EmailVisitantes = ecr.Email.Visitantes;

            ecvm.OrganicoLeads = ecr.Organico.Leads;
            ecvm.OrganicoOportunidades = ecr.Organico.Oportunidades;
            ecvm.OrganicoVendas = ecr.Organico.Vendas;
            ecvm.OrganicoVisitantes = ecr.Organico.Visitantes;

            ecvm.ReferenciaLeads = ecr.Referencia.Leads;
            ecvm.ReferenciaOportunidades= ecr.Referencia.Oportunidades;
            ecvm.ReferenciaVendas= ecr.Referencia.Vendas;
            ecvm.ReferenciaVisitantes = ecr.Referencia.Visitantes;

            return ecvm;
        } 

        [HttpPost()]
        public IActionResult Create( EficaciaCanalDTO ecvm)
        {
            EficaciaCanaisRelatorio ecr = new EficaciaCanaisRelatorio();
            ecr.IdCliente = ecvm.Cliente;
            ecr.Descricao = ecvm.Descricao;
            ecr.DataInicial = ecvm.DataInicial;
            ecr.DataFinal = ecvm.DataFinal;                        
            _context.EficaciaCanaisRelatorio.Add(ecr);

            EficaciaCanalBuscaPaga ecbp = new EficaciaCanalBuscaPaga();
            ecbp.EficaciaCanalID = ecr.ID;
            ecbp.Visitantes = ecvm.BuscaPagaVisitantes;
            ecbp.Leads = ecvm.BuscaPagaLeads;
            ecbp.Oportunidades = ecvm.BuscaPagaOportunidades;
            ecbp.Vendas = ecvm.BuscaPagaVendas;
            _context.EficaciaCanalBuscaPaga.Add(ecbp);

            EficaciaCanalDireto ecd = new EficaciaCanalDireto();
            ecd.EficaciaCanalID = ecr.ID;
            ecd.Visitantes = ecvm.DiretoVisitantes;
            ecd.Leads = ecvm.DiretoLeads;
            ecd.Oportunidades = ecvm.DiretoOportunidades;
            ecd.Vendas = ecvm.DiretoVendas;
            _context.EficaciaCanalDireto.Add(ecd);

            EficaciaCanalEmail ece = new EficaciaCanalEmail();
            ece.EficaciaCanalID = ecr.ID;
            ece.Visitantes = ecvm.EmailVisitantes;
            ece.Leads = ecvm.EmailLeads;
            ece.Oportunidades = ecvm.EmailOportunidades;
            ece.Vendas = ecvm.EmailVendas;
            _context.EficaciaCanalEmail.Add(ece);

            EficaciaCanalOrganico eco = new EficaciaCanalOrganico();
            eco.EficaciaCanalID = ecr.ID;
            eco.Visitantes = ecvm.OrganicoVisitantes;
            eco.Leads = ecvm.OrganicoLeads;
            eco.Oportunidades = ecvm.OrganicoOportunidades;
            eco.Vendas = ecvm.OrganicoVendas;
            _context.EficaciaCanalOrganico.Add(eco);

            EficaciaCanalReferencia ecref = new EficaciaCanalReferencia();
            ecref.EficaciaCanalID = ecr.ID;
            ecref.Visitantes = ecvm.ReferenciaVisitantes;
            ecref.Leads = ecvm.ReferenciaLeads;
            ecref.Oportunidades = ecvm.ReferenciaOportunidades;
            ecref.Vendas = ecvm.ReferenciaVendas;
            _context.EficaciaCanalReferencia.Add(ecref);            

            _context.SaveChanges();
            return NoContent();
        }     

        [HttpGet]
        public ActionResult<List<EficaciaCanalRelatorioDTO>> GetAll()
        {
            int usuarioID = int.Parse(HttpContext.User.Identity.Name);
            List<EficaciaCanalRelatorioDTO> lista = new List<EficaciaCanalRelatorioDTO>();
            foreach(EficaciaCanaisRelatorio ecr in _eficaciaCanaisService.SelecionarPorUsuario(usuarioID)){
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
