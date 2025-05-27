using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaBiblioteca.Models
{
    public class HistoricoEmprestimo
    {
        public int ID { get; internal set; }
        public int ID_Livro { get; internal set; }
        public int ID_Membro { get; internal set; }
        public DateTime Data_Acao { get; internal set; }
    }
}
