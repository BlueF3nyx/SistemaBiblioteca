using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaBiblioteca.Models
{
    public class Emprestimo
    {
        public int ID { get; set; }
        public int ID_Livro { get; set; }
        public int ID_Membro { get; set; }
        public DateTime Data_Emprestimo { get; set; }
        public DateTime? Data_Devolucao { get; set; }
        public string Status { get; set; } // Visualizar os Empréstimos: "Emprestado", "Devolvido", "Atrasado"
    }
}
