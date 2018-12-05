using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Innovativo.Models
{
    public class EficaciaCanal
    {
        public int ID { get; set; }

        [ForeignKey("EficaciaCanalID")]
        public virtual EficaciaCanalRelatorio EficaciaCanalRelatorio  { get; set; }
        public int EficaciaCanalID { get; set; }
        public int Visitantes { get; set; }
        public int Leads { get; set; }
        public int Oportunidades { get; set; }
        public int Vendas { get; set; }
    }
}
