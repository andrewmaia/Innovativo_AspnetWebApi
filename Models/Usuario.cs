using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Innovativo.Models
{
    public class Usuario
    {
        public int ID { get; set; }
        public string Email { get; set; }        
        public string Nome { get; set; }
        public string Senha { get; set; }
        public virtual Cliente Cliente  { get; set; }
        public  int? ClienteID  { get; set; }

        public string ObterPapel(){
            if (ClienteID.HasValue)
                return "cliente";

            return "adm";
        }

    }
}
