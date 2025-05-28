using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaBiblioteca.Models
{
    public class Emprestimo
    {
        public int Id_Emprestimo { get; set; }
        public int Id_Livro { get; set; }
        public int Id_Membro { get; set; }
        public DateTime Data_Emprestimo { get; set; }
        public DateTime Data_Devolucao { get; set; }
        public required string Status { get; set; } // Visualizar os Empréstimos: "Emprestado", "Devolvido", "Atrasado"
        public required string Observacao { get; set; }
    }

}
