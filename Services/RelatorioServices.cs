using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SistemaBiblioteca.Models;

namespace SistemaBiblioteca.Services
{
    public class RelatorioService
    {
        //Retorna o total de livros disponíveis para empréstimo 
        public int TotalLivrosDisponiveis(IEnumerable<Livros> livros)
        {
            return livros.Count(l => l.Disponibilidade);
        }
        // Retorna o total de livros que estão emprestados
        public int TotalLivrosEmprestados(IEnumerable<Livros> livros)
        {
            return livros.Count(l => !l.Disponibilidade);
        }
        // Conta o total de membros cadastrados
        public int TotalMembros(IEnumerable<Membro> membros)
        {
            return membros.Count();
        }
        // Conta o total de funcionários cadastrados
        public int TotalFuncionarios(IEnumerable<Funcionario> funcionarios)
        {
            return funcionarios.Count();
        }
        // Conta o total de empréstimos que estão ativos 
        public int TotalEmprestimosAtivos(IEnumerable<Emprestimo> emprestimos)
        {
            return emprestimos.Count(e => e.Status == "Emprestado");
        }
        // Conta o total de empréstimos que foram concluídos
        public int TotalEmprestimosConcluidos(IEnumerable<Emprestimo> emprestimos)
        {
            return emprestimos.Count(e => e.Status == "Devolvido");
        }
        //Retorna a lista de empréstimos realizados dentro do intervalo de dados especificados
        public List<Emprestimo> EmprestimosPorData(IEnumerable<Emprestimo> emprestimos, DateTime dataInicio, DateTime dataFim)
        {
            return emprestimos
                .Where(e => e.Data_Emprestimo.Date >= dataInicio.Date && e.Data_Emprestimo.Date <= dataFim.Date)
                .ToList();
        }
        // Retorna o histórico de empréstimos de um membro específico, ordenado do mais recente para o mais antigo
        public List<HistoricoEmprestimo> HistoricoPorMembro(int idMembro, IEnumerable<HistoricoEmprestimo> historico)
        {
            return historico
                .Where(h => h.Id_Membro == idMembro)
                .OrderByDescending(h => h.DataAcao)
                .ToList();
        }
        // Agrupa os livros por categoria e retorna um dicionário com a contagem de livros por categoria
        public Dictionary<string, int> LivrosPorCategoria(IEnumerable<Livros> livros)
        {
            return livros
                .GroupBy(l => l.Categoria)
                .ToDictionary(g => g.Key, g => g.Count());
        }
    }
}
