namespace Innovativo.DTO
{
    public class UsuarioDTO
    {
        public int ID { get; set; }
        public string Email { get; set; }        
        public string Nome { get; set; }
        public string Senha { get; set; }
        public  int ClienteID  { get; set; }
        public  string ClienteDescricao  { get; set; }        
    }
}
