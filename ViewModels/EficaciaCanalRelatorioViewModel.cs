using System;

namespace Innovativo.ViewModels
{
    public class EficaciaCanalRelatorioViewModel
    {
        public int ID { get; set; }
        public string Descricao { get; set; }
        public string ClienteNomeFantasia { get; set; }        
        public DateTime DataInicial { get; set; }
        public DateTime DataFinal { get; set; }

    }
}
