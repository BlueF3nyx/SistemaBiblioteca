using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using SistemaBiblioteca.Models;

namespace SistemaBiblioteca.ViewModels
{
    public class HistoricoEmprestimoViewModel : INotifyPropertyChanged
    {
        private HistoricoEmprestimo historicoSelecionado;

        public ObservableCollection<HistoricoEmprestimo> HistoricoEmprestimos { get; set; } = new ObservableCollection<HistoricoEmprestimo>();

        public HistoricoEmprestimo HistoricoSelecionado
        {
            get => historicoSelecionado;
            set
            {
                if (historicoSelecionado != value)
                {
                    historicoSelecionado = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand AdicionarHistoricoCommand { get; }
        public ICommand RemoverHistoricoCommand { get; }

        public HistoricoEmprestimoViewModel()
        {
            AdicionarHistoricoCommand = new Command(AdicionarHistorico);
            RemoverHistoricoCommand = new Command(RemoverHistorico, PodeRemover);

            // Exemplo de dado inicial
            HistoricoEmprestimos.Add(new HistoricoEmprestimo
            {
                ID = 1,
                ID_Livro = 1,
                ID_Membro = 1,
                Data_Acao = DateTime.Now
            });
        }

        private void AdicionarHistorico()
        {
            if (HistoricoSelecionado != null)
            {
                HistoricoEmprestimos.Add(new HistoricoEmprestimo
                {
                    ID = HistoricoEmprestimos.Count + 1,
                    ID_Livro = HistoricoSelecionado.ID_Livro,
                    ID_Membro = HistoricoSelecionado.ID_Membro,
                    Data_Acao = DateTime.Now
                });

                HistoricoSelecionado = null;
            }
        }

        private void RemoverHistorico()
        {
            if (HistoricoSelecionado != null && HistoricoEmprestimos.Contains(HistoricoSelecionado))
            {
                HistoricoEmprestimos.Remove(HistoricoSelecionado);
                HistoricoSelecionado = null;
            }
        }

        private bool PodeRemover() => HistoricoSelecionado != null;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string nome = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nome));
    }
}
