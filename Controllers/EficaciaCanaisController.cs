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
using AutoMapper;
namespace TodoApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
      public class EficaciaCanaisController : ControllerBase
    {
        private readonly IEficaciaCanaisService _eficaciaCanaisService;
        private readonly IMapper _mapper;              

        public EficaciaCanaisController(
            IEficaciaCanaisService eficaciaCanaisService,
            IMapper mapper)
        {
             _eficaciaCanaisService =eficaciaCanaisService;
		    _mapper = mapper;                  
        }

        [HttpGet("{id}")]
        public ActionResult<EficaciaCanalDTO> ObterPorID(int id)
        {
            EficaciaCanaisRelatorio ecr = _eficaciaCanaisService.ObterPorID(id);
            if (ecr == null)
                return NotFound();

            if(!_eficaciaCanaisService.PodeAcessarRelatorio(ecr,int.Parse(HttpContext.User.Identity.Name)))
                return Forbid();

            return _mapper.Map<EficaciaCanalDTO>(ecr);
        } 

        [HttpPost()]
        public IActionResult Inserir(EficaciaCanalDTO dto)
        {
            string mensagem;
            int id=_eficaciaCanaisService.Inserir(dto, out mensagem);
            if (mensagem!=string.Empty)
                return Conflict(mensagem);

            return CreatedAtAction(nameof(ObterPorID),new {id=id},dto);
        }     

        [HttpGet]
        public ActionResult<List<EficaciaCanalRelatorioDTO>> Listar()
        {
            return _eficaciaCanaisService.Listar(int.Parse(HttpContext.User.Identity.Name));
        }

        [HttpGet("ObterUltimo")]
        public ActionResult<EficaciaCanalDTO> ObterUltimo()
        {
            return _mapper.Map<EficaciaCanalDTO>(_eficaciaCanaisService.ObterUltimoDoCliente(int.Parse(HttpContext.User.Identity.Name)));            
        }

    }
}
