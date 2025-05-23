using System;
using System.Collections.Generic;
using System.Linq;
using SistemaBiblioteca.Models;

namespace SistemaBiblioteca.Services
{
    public class RelatorioService
    {
        public int TotalLivrosDisponiveis(IEnumerable<Livros> livros)
        {
            return livros.Count(l => l.Disponibilidade);
        }

        public int TotalLivrosEmprestados(IEnumerable<Livros> livros)
        {
            return livros.Count(l => !l.Disponibilidade);
        }

        public int TotalMembros(IEnumerable<Membro> membros)
        {
            return membros.Count();
        }

        public int TotalFuncionarios(IEnumerable<Funcionario> funcionarios)
        {
            return funcionarios.Count();
        }

        public int TotalEmprestimosAtivos(IEnumerable<Emprestimo> emprestimos)
        {
            return emprestimos.Count(e => e.Status == "Emprestado");
        }

        public int TotalEmprestimosConcluidos(IEnumerable<Emprestimo> emprestimos)
        {
            return emprestimos.Count(e => e.Status == "Devolvido");
        }

        public List<Emprestimo> EmprestimosPorData(IEnumerable<Emprestimo> emprestimos, DateTime dataInicio, DateTime dataFim)
        {
            return emprestimos
                .Where(e => e.Data_Emprestimo.Date >= dataInicio.Date && e.Data_Emprestimo.Date <= dataFim.Date)
                .ToList();
        }

        public List<HistoricoEmprestimo> HistoricoPorMembro(int idMembro, IEnumerable<HistoricoEmprestimo> historico)
        {
            return historico
                .Where(h => h.ID_Membro == idMembro)
                .OrderByDescending(h => h.Data_Acao)
                .ToList();
        }

        public Dictionary<string, int> LivrosPorCategoria(IEnumerable<Livros> livros)
        {
            return livros
                .GroupBy(l => l.Categoria)
                .ToDictionary(g => g.Key, g => g.Count());
        }
    }
}
