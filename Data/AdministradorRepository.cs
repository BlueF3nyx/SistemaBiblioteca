using SistemaBiblioteca.Models;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace SistemaBiblioteca.Data
{
    public class AdministradorRepository
    {
        public List<Administrador> ObterTodos()
        {
            List<Administrador> lista = new List<Administrador>();
            using MySqlConnection conn = new MySqlConnection(DatabaseConfig.ConnectionString);
            conn.Open();

            MySqlCommand cmd = new MySqlCommand("SELECT * FROM administrador", conn);
            using MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lista.Add(new Administrador
                {
                    Id = reader.GetInt32("id"),
                    Nome = reader.GetString("nome"),
                    Email = reader.GetString("email"),
                    Senha = reader.GetString("senha")
                });
            }

            return lista;
        }

        public Administrador ObterPorId(int id)
        {
            Administrador admin = null;
            using MySqlConnection conn = new MySqlConnection(DatabaseConfig.ConnectionString);
            conn.Open();

            MySqlCommand cmd = new MySqlCommand("SELECT * FROM administrador WHERE id_administrador = @id", conn);
            cmd.Parameters.AddWithValue("@id", id);

            using MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                admin = new Administrador
                {
                    Id = reader.GetInt32("id_administrador"),
                    Nome = reader.GetString("nome"),
                    Email = reader.GetString("email"),
                    Senha = reader.GetString("senha")
                };
            }

            return admin;
        }

        public void Adicionar(Administrador administrador)
        {
            using MySqlConnection conn = new MySqlConnection(DatabaseConfig.ConnectionString);
            conn.Open();

            MySqlCommand cmd = new MySqlCommand(@"
                INSERT INTO administrador (nome, email, senha)
                VALUES (@nome, @email, @senha)", conn);

            cmd.Parameters.AddWithValue("@nome", administrador.Nome);
            cmd.Parameters.AddWithValue("@email", administrador.Email);
            cmd.Parameters.AddWithValue("@senha", administrador.Senha);

            cmd.ExecuteNonQuery();
        }

        public void Atualizar(Administrador administrador)
        {
            using MySqlConnection conn = new MySqlConnection(DatabaseConfig.ConnectionString);
            conn.Open();

            MySqlCommand cmd = new MySqlCommand(@"
                UPDATE administrador
                SET nome = @nome,
                    email = @email,
                    senha = @senha
                WHERE id_administrador = @id", conn);

            cmd.Parameters.AddWithValue("@nome", administrador.Nome);
            cmd.Parameters.AddWithValue("@email", administrador.Email);
            cmd.Parameters.AddWithValue("@senha", administrador.Senha);
            cmd.Parameters.AddWithValue("@id", administrador.Id);

            cmd.ExecuteNonQuery();
        }

        public void Remover(int id)
        {
            using MySqlConnection conn = new MySqlConnection(DatabaseConfig.ConnectionString);
            conn.Open();

            MySqlCommand cmd = new MySqlCommand("DELETE FROM administrador WHERE id_administrador = @id", conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }
    }
}
