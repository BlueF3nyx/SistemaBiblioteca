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

        public ObservableCollection<Emprestimo> Emprestimos { get; set; } = new ObservableCollection<Emprestimo>();

        public Emprestimo EmprestimoSelecionado
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

            // Exemplo de dados iniciais
            Emprestimos.Add(new Emprestimo
            {
                ID = 1,
                ID_Livro = 1,
                ID_Membro = 1,
                Data_Emprestimo = DateTime.Now,
                Status = "Emprestado"
            });
        }

        private void AdicionarEmprestimo()
        {
            if (EmprestimoSelecionado != null)
            {
                Emprestimos.Add(new Emprestimo
                {
                    ID = Emprestimos.Count + 1,
                    ID_Livro = EmprestimoSelecionado.ID_Livro,
                    ID_Membro = EmprestimoSelecionado.ID_Membro,
                    Data_Emprestimo = DateTime.Now,
                    Status = "Emprestado"
                });

                EmprestimoSelecionado = null;
            }
        }

        private void AtualizarEmprestimo()
        {
            //incluir lógica para atualizar o empréstimo no banco de dados ou API
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

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string nome = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nome));
    }
}
