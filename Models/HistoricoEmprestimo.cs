using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaBiblioteca.Models
{
    public class HistoricoEmprestimo
    {
        public int IdHistorico { get; set; }
        public int Id_Emprestimo { get; set; }
        public int Id_Membro { get; set; }
        public int Id_Livro { get; set; }
        public int Dias_Contabilizados { get; set; }

        public DateTime DataAcao { get; set; }  // Nova propriedade
    }

}

