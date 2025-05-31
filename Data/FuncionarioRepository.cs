using MySql.Data.MySqlClient;
using SistemaBiblioteca.Models;
using System.Collections.Generic;

namespace SistemaBiblioteca.Data
{
    public class FuncionarioRepository
    {
        // Método para obter todos os funcionários
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
                    ID = reader.GetInt32("id_funcionario"),
                    Nome = reader.GetString("nome"),
                    Login = reader.GetString("email"),
                    senha = reader.GetString("senha")
                });
            }

            return lista;
        }

        // Método para obter um funcionário pelo login (email)
        public Funcionario? ObterPorLogin(string login)
        {
            using var conn = new MySqlConnection(DatabaseConfig.ConnectionString);
            conn.Open();

            var cmd = new MySqlCommand("SELECT * FROM funcionarios WHERE email = @login", conn);
            cmd.Parameters.AddWithValue("@login", login);

            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new Funcionario
                {
                    ID = reader.GetInt32("id_funcionario"),
                    Nome = reader.GetString("nome"),
                    Login = reader.GetString("email"),
                    senha = reader.GetString("senha")
                };
            }

            return null;
        }
    }
}
