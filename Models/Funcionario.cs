using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaBiblioteca.Models
{
    public class Funcionario
    {
        public int ID { get; set; }
        public required string Nome { get; set; }
        public required string Login { get; set; }
        public required string Senha { get; set; }

        internal static void Add(Funcionario funcionario)
        {
            throw new NotImplementedException();
        }
    }
}
