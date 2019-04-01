using System;
using System.Collections.Generic;
using System.Linq;
using Innovativo.Models;
using Innovativo;
using Innovativo.DTO;
using AutoMapper;

namespace Innovativo.Services
{
    public interface IClienteService
    {
        List<ClienteDTO> Listar();
        ClienteDTO ObterPorID(int id);
        bool Alterar (int id,ClienteDTO dto);
        ClienteDTO Inserir(ClienteDTO dto);
    }
 
    public class ClienteService : IClienteService
    {
        private readonly InnovativoContext _context;
        private readonly IMapper _mapper;

        public ClienteService(InnovativoContext context,IMapper mapper)
        {
             _context = context;
             _mapper = mapper;
        }        

        public List<ClienteDTO> Listar()
        {
            return _mapper.Map<List<ClienteDTO>>(_context.Cliente.ToList());      
        }

        public ClienteDTO ObterPorID(int id)
        {
            return _mapper.Map<ClienteDTO>(_context.Cliente.FirstOrDefault(x=>x.ID==id));
        }

        public bool Alterar (int id,ClienteDTO dto)
        {
            Cliente c = _context.Cliente.FirstOrDefault(x=>x.ID==id);
            if (c == null)
                return false;

            c.NomeFantasia = dto.NomeFantasia;
            _context.Cliente.Update(c);
            _context.SaveChanges();
            return true;
        }

        public ClienteDTO Inserir(ClienteDTO dto)
        {
            Cliente c = new Cliente();
            c.NomeFantasia = dto.NomeFantasia;
            _context.Cliente.Add(c);
            _context.SaveChanges();
            dto.ID = c.ID;
            return dto;
        }


    }
}

