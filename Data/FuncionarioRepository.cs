using MySql.Data.MySqlClient;
using SistemaBiblioteca.Models;
using System.Collections.Generic;

namespace SistemaBiblioteca.Data
{
    public class FuncionarioRepository
    {
        public List<Funcionario> ObterTodos()
        {
            var lista = new List<Funcionario>();

            using var conn = new MySqlConnection(DatabaseConfig.ConnectionString);
            conn.Open();

            var cmd = new MySqlCommand("SELECT * FROM funcionarios", conn);

            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                lista.Add(new Funcionario
                {
                    ID = reader.GetInt32("id_funcionario"),  // ajusta para o nome da coluna no seu DB
                    Nome = reader.GetString("nome"),
                    Login = reader.GetString("email"),
                    Senha = reader.GetString("senha")

                });
            }

            return lista;
        }
    }
}