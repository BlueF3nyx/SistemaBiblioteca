using MySql.Data.MySqlClient;
using SistemaBiblioteca.Models;
using System.Collections.Generic;
using System;

namespace SistemaBiblioteca.Data
{
    public class HistoricoEmprestimoRepository
    {
        public List<HistoricoEmprestimo> ObterTodos()
        {
            List<HistoricoEmprestimo> lista = new List<HistoricoEmprestimo>();
            using MySqlConnection conn = new MySqlConnection(DatabaseConfig.ConnectionString);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM historico", conn);
            using MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {

                lista.Add(new HistoricoEmprestimo()
                {
                    IdHistorico = reader.GetInt32("idhistorico"),
                    Id_Emprestimo = reader.GetInt32("id_emprestimo"),
                    Id_Membro = reader.GetInt32("id_membro"),
                    Id_Livro = reader.GetInt32("id_livro"),
                    Dias_Contabilizados = reader.GetInt32("Dias_contabilizados"),
                    DataAcao = reader.GetDateTime("Data_Acao")

                });
            }
            return lista;
        }
    }
}
