using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using SistemaBiblioteca.Models;

namespace SistemaBiblioteca.ViewsModel
{
    public class MembroViewModel : INotifyPropertyChanged
    {
        private Membro _membroSelecionado;

        public ObservableCollection<Membro> Membros { get; set; } = new ObservableCollection<Membro>();

        public Membro? MembroSelecionado
        {
            get => _membroSelecionado;
            set
            {
                if (_membroSelecionado != value)
                {
                    _membroSelecionado = value;
                    OnPropertyChanged();
                    ((Command)AtualizarMembroCommand).ChangeCanExecute();
                    ((Command)RemoverMembroCommand).ChangeCanExecute();
                }
            }
        }

        public ICommand AdicionarMembroCommand { get; }
        public ICommand AtualizarMembroCommand { get; }
        public ICommand RemoverMembroCommand { get; }

        public MembroViewModel()
        {
            AdicionarMembroCommand = new Command(AdicionarMembro);
            AtualizarMembroCommand = new Command(AtualizarMembro, PodeEditarOuRemover);
            RemoverMembroCommand = new Command(RemoverMembro, PodeEditarOuRemover);

            // Exemplo de membro inicial (pode ser removido depois)
            Membros.Add(new Membro { Id = 1, Name = "", CPF = "", Telefone = "", Email = "", senha = "" });
        }

        private void AdicionarMembro()
        {
            if (MembroSelecionado != null)
            {
                Membros.Add(MembroSelecionado);
                MembroSelecionado = null;
            }
        }

        private void AtualizarMembro()
        {
            // Lógica de atualização a ser implementada
        }

        private void RemoverMembro()
        {
            if (MembroSelecionado != null && Membros.Contains(MembroSelecionado))
            {
                Membros.Remove(MembroSelecionado);
                MembroSelecionado = null;
            }
        }

        private bool PodeEditarOuRemover() => MembroSelecionado != null;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string nome = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nome));
    }
}
