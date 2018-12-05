using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Innovativo.Models
{
    public class EficaciaCanalRelatorio
    {
        public int ID { get; set; }


        public virtual Cliente Cliente  { get; set; }
        public  int IdCliente  { get; set; }
        public string Descricao { get; set; }
        public DateTime DataInicial { get; set; }
        public DateTime DataFinal { get; set; } 

        public virtual EficaciaCanalBuscaPaga BuscaPaga { get; set; }        
        public virtual EficaciaCanalDireto Direto { get; set; }        
        public virtual EficaciaCanalEmail Email { get; set; }        
        public virtual EficaciaCanalOrganico Organico { get; set; }                
        public virtual EficaciaCanalReferencia Referencia { get; set; }                        

    }
}
