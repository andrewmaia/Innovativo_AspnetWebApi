using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Innovativo.Models;
using Innovativo.ViewModels;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Primitives;
using Microsoft.AspNetCore.Authorization;
using Innovativo;
using System.Data;

namespace TodoApi.Controllers
{
    [Authorize]        
    [Route("api/[controller]")]
    [ApiController]
      public class EficaciaCanalController : ControllerBase
    {
        private readonly InnovativoContext _context;

        public EficaciaCanalController(InnovativoContext context)
        {
             _context = context;
        }

        [HttpGet("{id}")]
        public ActionResult<EficaciaCanalViewModel> GetById(int id)
        {
            EficaciaCanalRelatorio ecr = _context.EficaciaCanalRelatorio.Find(id);
            if (ecr == null)
                return NotFound();

            EficaciaCanalViewModel ecvm = new EficaciaCanalViewModel();
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
        public IActionResult Create( EficaciaCanalViewModel ecvm)
        {
            EficaciaCanalRelatorio ecr = new EficaciaCanalRelatorio();
            ecr.IdCliente = ecvm.Cliente;
            ecr.Descricao = ecvm.Descricao;
            ecr.DataInicial = ecvm.DataInicial;
            ecr.DataFinal = ecvm.DataFinal;                        
            _context.EficaciaCanalRelatorio.Add(ecr);

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
        public ActionResult<List<EficaciaCanalRelatorioViewModel>> GetAll()
        {
            List<EficaciaCanalRelatorioViewModel> lista = new List<EficaciaCanalRelatorioViewModel>();
            foreach(EficaciaCanalRelatorio ecr in _context.EficaciaCanalRelatorio){
                lista.Add( new EficaciaCanalRelatorioViewModel{ 
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
