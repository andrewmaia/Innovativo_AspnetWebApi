using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Innovativo.Models;
using Innovativo.DTO;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Primitives;
using Innovativo;
using System.Data;
using Microsoft.AspNetCore.Authorization;
using System.Text;
using AutoMapper;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles="adm")]   
      public class ClienteController : ControllerBase
    {
        private readonly InnovativoContext _context;
        private readonly IMapper _mapper;

        public ClienteController(InnovativoContext context,IMapper mapper)
        {
             _context = context;
		    _mapper = mapper;           
        }

        [HttpGet]
        public ActionResult<List<ClienteDTO>> GetAll()
        {
            List<ClienteDTO> lista = new List<ClienteDTO>();
            foreach(Cliente c in _context.Cliente){
                lista.Add( new ClienteDTO{ ID= c.ID, NomeFantasia = c.NomeFantasia });
            }
            return lista;
        }


        [HttpGet("{id}")]
        public ActionResult<ClienteDTO> GetById(int id)
        {
            Cliente c = _context.Cliente.Find(id);
            if (c == null)
                return NotFound();

            ClienteDTO cvm = _mapper.Map<ClienteDTO>(c);
            return cvm;
        } 

        [HttpPut("{id}")]
        public IActionResult Update(int id, ClienteDTO cvm)
        {
            Cliente c = _context.Cliente.Find(id);
            if (c == null)
                return NotFound();

            c.NomeFantasia = cvm.NomeFantasia;
            _context.Cliente.Update(c);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpPost()]
        public ActionResult<ClienteDTO> Create( ClienteDTO cvm)
        {
            Cliente c = new Cliente();
            c.NomeFantasia = cvm.NomeFantasia;
            _context.Cliente.Add(c);
            _context.SaveChanges();
            cvm.ID = c.ID;
            return cvm;
        }
    }
}
