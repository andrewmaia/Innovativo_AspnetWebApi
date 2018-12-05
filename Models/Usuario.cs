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

    }
}
