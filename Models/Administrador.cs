namespace SistemaBiblioteca.Models
{
    public class Administrador
    {
        public int Id {  get; set; }
        public required string Nome { get; set; }
        public required string Email { get; set; }

        public required string senha { get; set; }
    }
}
