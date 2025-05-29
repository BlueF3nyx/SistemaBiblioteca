using MySql.Data.MySqlClient;
using SistemaBiblioteca.Models;
using System.Collections.Generic;

namespace SistemaBiblioteca.Data
{
    public class LivroRepository
    {
        // CREATE
        public void Adicionar(Livros livro)
        {
            using var conn = new MySqlConnection(DatabaseConfig.ConnectionString);
            conn.Open();

            var cmd = new MySqlCommand(@"
                INSERT INTO livros (titulo, autor, categoria, disponibilidade)
                VALUES (@titulo, @autor, @categoria, @disponibilidade)", conn);

            cmd.Parameters.AddWithValue("@titulo", livro.Titulo);
            cmd.Parameters.AddWithValue("@autor", livro.Autor);
            cmd.Parameters.AddWithValue("@categoria", livro.Categoria);
            cmd.Parameters.AddWithValue("@disponibilidade", livro.Disponibilidade);

            cmd.ExecuteNonQuery();
        }

        // READ - Obter todos os livros
        public List<Livros> ObterTodos()
        {
            var lista = new List<Livros>();
            using var conn = new MySqlConnection(DatabaseConfig.ConnectionString);
            conn.Open();
            var cmd = new MySqlCommand("SELECT * FROM livros", conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lista.Add(new Livros
                {
                    ID = reader.GetInt32("id_livro"),
                    Titulo = reader.GetString("titulo"),
                    Autor = reader.GetString("autor"),
                    Categoria = reader.GetString("categoria"),
                    Disponibilidade = reader.GetBoolean("disponibilidade")
                });
            }
            return lista;
        }

        // READ - Obter livro por ID
        public Livros ObterPorId(int id)
        {
            Livros? livro = null;

            using var conn = new MySqlConnection(DatabaseConfig.ConnectionString);
            conn.Open();

            var cmd = new MySqlCommand("SELECT * FROM livros WHERE id_livro = @id", conn);
            cmd.Parameters.AddWithValue("@id", id);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                livro = new Livros
                {
                    ID = reader.GetInt32("id_livro"),
                    Titulo = reader.GetString("titulo"),
                    Autor = reader.GetString("autor"),
                    Categoria = reader.GetString("categoria"),
                    Disponibilidade = reader.GetBoolean("disponibilidade")
                };
            }

            return livro;
        }

        // UPDATE
        public void Atualizar(Livros livro)
        {
            using var conn = new MySqlConnection(DatabaseConfig.ConnectionString);
            conn.Open();

            var cmd = new MySqlCommand(@"
                UPDATE livros
                SET titulo = @titulo,
                    autor = @autor,
                    categoria = @categoria,
                    disponibilidade = @disponibilidade
                WHERE id_livro = @id", conn);

            cmd.Parameters.AddWithValue("@titulo", livro.Titulo);
            cmd.Parameters.AddWithValue("@autor", livro.Autor);
            cmd.Parameters.AddWithValue("@categoria", livro.Categoria);
            cmd.Parameters.AddWithValue("@disponibilidade", livro.Disponibilidade);
            cmd.Parameters.AddWithValue("@id", livro.ID);

            cmd.ExecuteNonQuery();
        }

        // DELETE
        public void Remover(int id)
        {
            using var conn = new MySqlConnection(DatabaseConfig.ConnectionString);
            conn.Open();

            var cmd = new MySqlCommand("DELETE FROM livros WHERE id_livro = @id", conn);
            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();
        }
    }
}
