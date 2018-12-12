using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Innovativo.Models
{
    public class Cliente
    {
        public int ID { get; set; }
        public string NomeFantasia { get; set; }
        public virtual List<EficaciaCanaisRelatorio> EficaciaCanalRelatorioLista { get; set; }
    }
}
