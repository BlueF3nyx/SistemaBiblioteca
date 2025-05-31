using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using SistemaBiblioteca.Models;

namespace SistemaBiblioteca.ViewModels
{
    public class HistoricoViewModel : INotifyPropertyChanged
    {
        private HistoricoEmprestimo historicoSelecionado;

        public ObservableCollection<HistoricoEmprestimo> Historicos { get; set; } = new();

        public HistoricoEmprestimo? HistoricoSelecionado
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

        public HistoricoViewModel()
        {
            AdicionarHistoricoCommand = new Command(AdicionarHistorico);
            RemoverHistoricoCommand = new Command(RemoverHistorico, PodeRemover);

            Historicos.Add(new HistoricoEmprestimo
            {
                IdHistorico = 1,
                Id_Livro = 1,
                Id_Membro = 1,
                Id_Emprestimo = 1,
                Dias_Contabilizados = 5,
                DataAcao = DateTime.Now 
            });
        }

        private void AdicionarHistorico()
        {
            if (HistoricoSelecionado != null)
            {
                Historicos.Add(new HistoricoEmprestimo
                {
                    IdHistorico = Historicos.Count + 1,
                    Id_Livro = HistoricoSelecionado.Id_Livro,
                    Id_Membro = HistoricoSelecionado.Id_Membro,
                    Id_Emprestimo = HistoricoSelecionado.Id_Emprestimo,
                    Dias_Contabilizados = HistoricoSelecionado.Dias_Contabilizados,
                    DataAcao = DateTime.Now 
                });

                HistoricoSelecionado = null;
            }
        }

        private void RemoverHistorico()
        {
            if (HistoricoSelecionado != null && Historicos.Contains(HistoricoSelecionado))
            {
                Historicos.Remove(HistoricoSelecionado);
                HistoricoSelecionado = null;
            }
        }

        private bool PodeRemover() => HistoricoSelecionado != null;

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? nome = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nome));
        }
    }
}
