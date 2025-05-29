using System;
using SistemaBiblioteca.Models;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace SistemaBiblioteca.Data
{
    public class EmprestimoRepository
    {
        // CREATE
        public void Adicionar(Emprestimo emprestimo)
        {
            using var conn = new MySqlConnection(DatabaseConfig.ConnectionString);
            conn.Open();

            var cmd = new MySqlCommand(@"
                INSERT INTO emprestimo (id_livro, id_membro, data_emprestimo, data_devolucao, status, observacao)
                VALUES (@id_livro, @id_membro, @data_emprestimo, @data_devolucao, @status, @observacao)", conn);

            cmd.Parameters.AddWithValue("@id_livro", emprestimo.Id_Livro);
            cmd.Parameters.AddWithValue("@id_membro", emprestimo.Id_Membro);
            cmd.Parameters.AddWithValue("@data_emprestimo", emprestimo.Data_Emprestimo);
            cmd.Parameters.AddWithValue("@data_devolucao", emprestimo.Data_Devolucao);
            cmd.Parameters.AddWithValue("@status", emprestimo.Status);
            cmd.Parameters.AddWithValue("@observacao", emprestimo.Observacao);

            cmd.ExecuteNonQuery();
        }

        // READ - Obter todos os empréstimos
        public List<Emprestimo> ObterTodos()
        {
            List<Emprestimo> lista = new List<Emprestimo>();
            using MySqlConnection conn = new MySqlConnection(DatabaseConfig.ConnectionString);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM emprestimo", conn);
            using MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lista.Add(new Emprestimo
                {
                    Id_Emprestimo = reader.GetInt32("id_emprestimo"),
                    Id_Livro = reader.GetInt32("id_livro"),
                    Id_Membro = reader.GetInt32("id_membro"),
                    Data_Emprestimo = reader.GetDateTime("data_emprestimo"),
                    Data_Devolucao = reader.GetDateTime("data_devolucao"),
                    Status = reader.GetString("status"),
                    Observacao = reader.GetString("obervacao")
                });
            }
            return lista;
        }

        // READ - Obter por ID
        public Emprestimo ObterPorId(int id)
        {
            Emprestimo? emprestimo = null;
            using var conn = new MySqlConnection(DatabaseConfig.ConnectionString);
            conn.Open();

            var cmd = new MySqlCommand("SELECT * FROM emprestimo WHERE id_emprestimo = @id", conn);
            cmd.Parameters.AddWithValue("@id", id);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                emprestimo = new Emprestimo
                {
                    Id_Emprestimo = reader.GetInt32("id_emprestimo"),
                    Id_Livro = reader.GetInt32("id_livro"),
                    Id_Membro = reader.GetInt32("id_membro"),
                    Data_Emprestimo = reader.GetDateTime("data_emprestimo"),
                    Data_Devolucao = reader.GetDateTime("data_devolucao"),
                    Status = reader.GetString("status"),
                    Observacao = reader.GetString("obervacao")
                };
            }

            return emprestimo;
        }

        // UPDATE - atualizar status, devolução, etc.
        public void Atualizar(Emprestimo emprestimo)
        {
            using var conn = new MySqlConnection(DatabaseConfig.ConnectionString);
            conn.Open();

            var cmd = new MySqlCommand(@"
                UPDATE emprestimo
                SET id_livro = @id_livro,
                    id_membro = @id_membro,
                    data_emprestimo = @data_emprestimo,
                    data_devolucao = @data_devolucao,
                    status = @status,
                    obervação = @observacao
                WHERE id_emprestimo = @id", conn);

            cmd.Parameters.AddWithValue("@id", emprestimo.Id_Emprestimo);
            cmd.Parameters.AddWithValue("@id_livro", emprestimo.Id_Livro);
            cmd.Parameters.AddWithValue("@id_membro", emprestimo.Id_Membro);
            cmd.Parameters.AddWithValue("@data_emprestimo", emprestimo.Data_Emprestimo);
            cmd.Parameters.AddWithValue("@data_devolucao", emprestimo.Data_Devolucao);
            cmd.Parameters.AddWithValue("@status", emprestimo.Status);
            cmd.Parameters.AddWithValue("@observacao", emprestimo.Observacao);

            cmd.ExecuteNonQuery();
        }
    }
}
