using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SistemaBiblioteca.Models;

namespace SistemaBiblioteca.ViewsModel
{
    public class FuncionarioViewModel : INotifyPropertyChanged
    {
        private Funcionario funcionarioSelecionado;

        public ObservableCollection<Funcionario> Funcionarios { get; set; } = new ObservableCollection<Funcionario>();

        public Funcionario FuncionarioSelecionado
        {
            get => funcionarioSelecionado;
            set
            {
                if (funcionarioSelecionado != value)
                {
                    funcionarioSelecionado = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand AdicionarFuncionarioCommand { get; }
        public ICommand AtualizarFuncionarioCommand { get; }
        public ICommand RemoverFuncionarioCommand { get; }

        public FuncionarioViewModel()
        {
            AdicionarFuncionarioCommand = new Command(AdicionarFuncionario);
            AtualizarFuncionarioCommand = new Command(AtualizarFuncionario, PodeEditarOuRemover);
            RemoverFuncionarioCommand = new Command(RemoverFuncionario, PodeEditarOuRemover);

            
            Funcionarios.Add(new Funcionario{ID = 1, Nome = "", Cargo = "", Login = "", Senha = ""});
        
        }

        private void AdicionarFuncionario()
        {
            if (FuncionarioSelecionado != null)
            {
                Funcionarios.Add(FuncionarioSelecionado);
                FuncionarioSelecionado = null;
            }
        }

        private void AtualizarFuncionario()
        {
            // Lógica de atualização deve ser colocada aqui ou na pasta de services
        }

        private void RemoverFuncionario()
        {
            if (FuncionarioSelecionado != null && Funcionarios.Contains(FuncionarioSelecionado))
            {
                Funcionarios.Remove(FuncionarioSelecionado);
                FuncionarioSelecionado = null;
            }
        }

        private bool PodeEditarOuRemover() => FuncionarioSelecionado != null;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string nome = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nome));
    }
}
