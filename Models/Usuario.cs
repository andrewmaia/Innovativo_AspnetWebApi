using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

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

        public string [] ObterPapeis()
        {
            List<string> papeis = new List<string>();
            if(ClienteID.HasValue)
                papeis.Add("cliente");
            else
                papeis.Add("admin");            
                
            return papeis.ToArray();
        }

    }
}
