using System;

namespace Innovativo.DTO
{
    public class EficaciaCanalDTO
    {
        public int Cliente { get; set; }
        public string Descricao { get; set; }
        public DateTime DataInicial { get; set; }
        public DateTime DataFinal { get; set; }
        public int DiretoVisitantes { get; set; }
        public int DiretoLeads { get; set; }
        public int DiretoOportunidades { get; set; }                
        public int DiretoVendas { get; set; }

        public int BuscaPagaVisitantes { get; set; }
        public int BuscaPagaLeads { get; set; }
        public int BuscaPagaOportunidades { get; set; }                
        public int BuscaPagaVendas { get; set; }

        public int OrganicoVisitantes { get; set; }
        public int OrganicoLeads { get; set; }
        public int OrganicoOportunidades { get; set; }                
        public int OrganicoVendas { get; set; }        

        public int EmailVisitantes { get; set; }
        public int EmailLeads { get; set; }
        public int EmailOportunidades { get; set; }                
        public int EmailVendas { get; set; }  

        public int ReferenciaVisitantes { get; set; }
        public int ReferenciaLeads { get; set; }
        public int ReferenciaOportunidades { get; set; }                
        public int ReferenciaVendas { get; set; }          

    }
}
