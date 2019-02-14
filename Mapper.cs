using System;
using AutoMapper;
using Innovativo.Models;
using Innovativo.DTO;

namespace Innovativo
{
    public class Mapper: Profile
    {
        public Mapper()
        {
            CreateMap<EficaciaCanaisRelatorio,EficaciaCanalDTO>()
                .ForMember(dto =>dto.BuscaPagaLeads,m=> m.MapFrom(model=>model.BuscaPaga.Leads))
                .ForMember(dto =>dto.BuscaPagaOportunidades,m=> m.MapFrom(model=>model.BuscaPaga.Oportunidades))             
                .ForMember(dto =>dto.BuscaPagaVendas,m=> m.MapFrom(model=>model.BuscaPaga.Vendas))             
                .ForMember(dto =>dto.BuscaPagaVisitantes,m=> m.MapFrom(model=>model.BuscaPaga.Visitantes))                                             
                .ForMember(dto =>dto.OrganicoLeads,m=> m.MapFrom(model=>model.Organico.Leads))
                .ForMember(dto =>dto.OrganicoOportunidades,m=> m.MapFrom(model=>model.Organico.Oportunidades))             
                .ForMember(dto =>dto.OrganicoVendas,m=> m.MapFrom(model=>model.Organico.Vendas))             
                .ForMember(dto =>dto.OrganicoVisitantes,m=> m.MapFrom(model=>model.Organico.Visitantes)) 
                .ForMember(dto =>dto.DiretoLeads,m=> m.MapFrom(model=>model.Direto.Leads))
                .ForMember(dto =>dto.DiretoOportunidades,m=> m.MapFrom(model=>model.Direto.Oportunidades))             
                .ForMember(dto =>dto.DiretoVendas,m=> m.MapFrom(model=>model.Direto.Vendas))             
                .ForMember(dto =>dto.DiretoVisitantes,m=> m.MapFrom(model=>model.Direto.Visitantes)) 
                .ForMember(dto =>dto.EmailLeads,m=> m.MapFrom(model=>model.Email.Leads))
                .ForMember(dto =>dto.EmailOportunidades,m=> m.MapFrom(model=>model.Email.Oportunidades))             
                .ForMember(dto =>dto.EmailVendas,m=> m.MapFrom(model=>model.Email.Vendas))             
                .ForMember(dto =>dto.EmailVisitantes,m=> m.MapFrom(model=>model.Email.Visitantes))
                .ForMember(dto =>dto.ReferenciaLeads,m=> m.MapFrom(model=>model.Referencia.Leads))
                .ForMember(dto =>dto.ReferenciaOportunidades,m=> m.MapFrom(model=>model.Referencia.Oportunidades))             
                .ForMember(dto =>dto.ReferenciaVendas,m=> m.MapFrom(model=>model.Referencia.Vendas))             
                .ForMember(dto =>dto.ReferenciaVisitantes,m=> m.MapFrom(model=>model.Referencia.Visitantes))                                                              
                ;

            CreateMap<Usuario,UsuarioDTO>()
                .ForMember(dto =>dto.ID,m=> m.MapFrom(model=>model.ID))
                .ForMember(dto =>dto.Nome,m=> m.MapFrom(model=>model.Nome))
                .ForMember(dto =>dto.Email,m=> m.MapFrom(model=>model.Email)) 
                .ForMember(dto =>dto.Senha,m=> m.MapFrom(model=>model.Senha))
                .ForMember(dto =>dto.ClienteID,m=> m.MapFrom(model=>model.ClienteID)) 
                .ForMember(dto =>dto.ClienteDescricao,m=> m.MapFrom(model=>model.Cliente.NomeFantasia))                                                                             
                ;

            CreateMap<UsuarioDTO,Usuario>()
                .ForMember(model =>model.ID,m=> m.MapFrom(dto=>dto.ID))
                .ForMember(model =>model.Nome,m=> m.MapFrom(dto=>dto.Nome))
                .ForMember(model =>model.Email,m=> m.MapFrom(dto=>dto.Email)) 
                .ForMember(model =>model.Senha,m=> m.MapFrom(dto=>dto.Senha))
                .ForMember(model =>model.ClienteID,m=> m.MapFrom(dto=>dto.ClienteID)) 
                ;                                 
        }
    }
}


