using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaBiblioteca.Models
{
    public class Administrador
    {
        public int ID {  get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }

        public required string Senha { get; set; }
    }
}
