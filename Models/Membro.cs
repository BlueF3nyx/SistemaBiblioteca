using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaBiblioteca.Models
{
    public class Membro
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string CPF { get; set; }
        public required string Telefone { get; set; }
        public required string Email { get; set; }

        public required string senha { get; set; }

        
    }
}
