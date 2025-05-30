using MySql.Data.MySqlClient;
using SistemaBiblioteca.Models;
using System.Collections.Generic;

namespace SistemaBiblioteca.Data
{
    public class MembroRepository
    {
        // CREATE
        public void Adicionar(Membro membro)
        {
            using var conn = new MySqlConnection(DatabaseConfig.ConnectionString);
            conn.Open();

            var cmd = new MySqlCommand(@"
                INSERT INTO membro (nome, cpf_membro, email_membro, telefone, senha)
                VALUES (@nome, @cpf, @email, @telefone, @senha)", conn);

            cmd.Parameters.AddWithValue("@nome", membro.Name);
            cmd.Parameters.AddWithValue("@cpf", membro.CPF);
            cmd.Parameters.AddWithValue("@email", membro.Email);
            cmd.Parameters.AddWithValue("@telefone", membro.Telefone);
            cmd.Parameters.AddWithValue("@senha", membro.Senha);

            cmd.ExecuteNonQuery();
        }

        // READ - Obter todos os membros
        public List<Membro> ObterTodos()
        {
            List<Membro> lista = new List<Membro>();
            using MySqlConnection conn = new MySqlConnection(DatabaseConfig.ConnectionString);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM membro", conn);
            using MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lista.Add(new Membro
                {
                    Id = reader.GetInt32("id"),
                    Name = reader.GetString("nome"),
                    CPF = reader.GetString("cpf_membro"),
                    Email = reader.GetString("email_membro"),
                    Telefone = reader.GetString("telefone"),
                    Senha = reader.GetString("senha")
                });
            }
            return lista;
        }

        // READ - Obter membro por ID
        public Membro ObterPorId(int id)
        {
            Membro? membro = null;
            using var conn = new MySqlConnection(DatabaseConfig.ConnectionString);
            conn.Open();

            var cmd = new MySqlCommand("SELECT * FROM membro WHERE id = @id", conn);
            cmd.Parameters.AddWithValue("@id", id);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                membro = new Membro
                {
                    Id = reader.GetInt32("id"),
                    Name = reader.GetString("nome"),
                    CPF = reader.GetString("cpf_membro"),
                    Email = reader.GetString("email_membro"),
                    Telefone = reader.GetString("telefone"),
                    Senha = reader.GetString("senha")
                };
            }

            return membro;
        }

        // UPDATE
        public void Atualizar(Membro membro)
        {
            using var conn = new MySqlConnection(DatabaseConfig.ConnectionString);
            conn.Open();

            var cmd = new MySqlCommand(@"
                UPDATE membro
                SET nome = @nome,
                    cpf_membro = @cpf,
                    email_membro = @email,
                    telefone = @telefone,
                    senha = @senha
                WHERE id = @id", conn);

            cmd.Parameters.AddWithValue("@nome", membro.Name);
            cmd.Parameters.AddWithValue("@cpf", membro.CPF);
            cmd.Parameters.AddWithValue("@email", membro.Email);
            cmd.Parameters.AddWithValue("@telefone", membro.Telefone);
            cmd.Parameters.AddWithValue("@senha", membro.Senha);
            cmd.Parameters.AddWithValue("@id", membro.Id);

            cmd.ExecuteNonQuery();
        }

        // DELETE
        public void Remover(int id)
        {
            using var conn = new MySqlConnection(DatabaseConfig.ConnectionString);
            conn.Open();

            var cmd = new MySqlCommand("DELETE FROM membro WHERE id = @id", conn);
            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();
        }
    }
}
