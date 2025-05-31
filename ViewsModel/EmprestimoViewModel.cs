using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using SistemaBiblioteca.Models;

namespace SistemaBiblioteca.ViewModels
{
    public class EmprestimoViewModel : INotifyPropertyChanged
    {
        private Emprestimo emprestimoSelecionado;

        public ObservableCollection<Emprestimo> Emprestimos { get; set; } = new();

        public Emprestimo? EmprestimoSelecionado
        {
            get => emprestimoSelecionado;
            set
            {
                if (emprestimoSelecionado != value)
                {
                    emprestimoSelecionado = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand AdicionarEmprestimoCommand { get; }
        public ICommand AtualizarEmprestimoCommand { get; }
        public ICommand RemoverEmprestimoCommand { get; }

        public EmprestimoViewModel()
        {
            AdicionarEmprestimoCommand = new Command(AdicionarEmprestimo);
            AtualizarEmprestimoCommand = new Command(AtualizarEmprestimo, PodeEditarOuRemover);
            RemoverEmprestimoCommand = new Command(RemoverEmprestimo, PodeEditarOuRemover);

            // Exemplo de dado inicial
            Emprestimos.Add(new Emprestimo
            {
                Id_Emprestimo = 1,
                Id_Livro = 1,
                Id_Membro = 1,
                Data_Emprestimo = DateTime.Now,
                Status = "Emprestado",
                Observacao = ""
            });
        }

        private void AdicionarEmprestimo()
        {
            if (EmprestimoSelecionado != null)
            {
                Emprestimos.Add(new Emprestimo
                {
                    Id_Emprestimo = Emprestimos.Count + 1,
                    Id_Livro = EmprestimoSelecionado.Id_Livro,
                    Id_Membro = EmprestimoSelecionado.Id_Membro,
                    Data_Emprestimo = DateTime.Now,
                    Status = "Emprestado",
                    Observacao = ""
                });

                EmprestimoSelecionado = null;
            }
        }

        private void AtualizarEmprestimo()
        {
            // Lógica futura para atualização no banco
        }

        private void RemoverEmprestimo()
        {
            if (EmprestimoSelecionado != null && Emprestimos.Contains(EmprestimoSelecionado))
            {
                Emprestimos.Remove(EmprestimoSelecionado);
                EmprestimoSelecionado = null;
            }
        }

        private bool PodeEditarOuRemover() => EmprestimoSelecionado != null;

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? nome = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nome));
    }
}
