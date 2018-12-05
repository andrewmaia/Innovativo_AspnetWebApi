using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Innovativo.Models;
using Innovativo.ViewModels;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Primitives;
using Innovativo;
using System.Data;
using Microsoft.AspNetCore.Authorization;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]    
      public class ClienteController : ControllerBase
    {
        private readonly InnovativoContext _context;

        public ClienteController(InnovativoContext context)
        {
             _context = context;
        }

        [HttpGet]
        public ActionResult<List<ClienteViewModel>> GetAll()
        {
            List<ClienteViewModel> lista = new List<ClienteViewModel>();
            foreach(Cliente c in _context.Cliente){
                lista.Add( new ClienteViewModel{ ID= c.ID, NomeFantasia = c.NomeFantasia });
            }
            return lista;
        }


        [HttpGet("{id}")]
        public ActionResult<ClienteViewModel> GetById(int id)
        {
            Cliente c = _context.Cliente.Find(id);
            if (c == null)
                return NotFound();

            ClienteViewModel cvm = new ClienteViewModel();
            cvm.ID = c.ID;
            cvm.NomeFantasia = c.NomeFantasia;

            return cvm;
        } 

        [HttpPut("{id}")]
        public IActionResult Update(int id, ClienteViewModel cvm)
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
        public IActionResult Create( ClienteViewModel cvm)
        {
            Cliente c = new Cliente();
            c.NomeFantasia = cvm.NomeFantasia;
            _context.Cliente.Add(c);
            _context.SaveChanges();
            return NoContent();
            //return CreatedAtRoute("GetTodo", new { id = item.Id }, item);
        }
    }
}
