using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Microsoft.Maui.Controls;  
using SistemaBiblioteca.Models;

namespace SistemaBiblioteca.ViewsModel
{
    public class FuncionarioViewModel : INotifyPropertyChanged
    {
        private Funcionario funcionarioSelecionado;

        public ObservableCollection<Funcionario> Funcionarios { get; set; } = new ObservableCollection<Funcionario>();

        public Funcionario? FuncionarioSelecionado
        {
            get => funcionarioSelecionado;
            set
            {
                if (funcionarioSelecionado != value)
                {
                    funcionarioSelecionado = value;
                    OnPropertyChanged();
                    // Atualizar estado dos comandos (se for necessário)
                    ((Command)AtualizarFuncionarioCommand).ChangeCanExecute();
                    ((Command)RemoverFuncionarioCommand).ChangeCanExecute();
                }
            }
        }

        public ICommand AdicionarFuncionarioCommand { get; }
        public ICommand AtualizarFuncionarioCommand { get; }
        public ICommand RemoverFuncionarioCommand { get; }

        public FuncionarioViewModel()
        {
            AdicionarFuncionarioCommand = new Command(AdicionarFuncionario, PodeAdicionar);
            AtualizarFuncionarioCommand = new Command(AtualizarFuncionario, PodeEditarOuRemover);
            RemoverFuncionarioCommand = new Command(RemoverFuncionario, PodeEditarOuRemover);

            
            Funcionarios.Add(new Funcionario
            {
                ID = 1,
                Nome = "Admin",
                Login = "admin",
                senha = "1234"
            });
        }

        private void AdicionarFuncionario()
        {
            if (FuncionarioSelecionado != null)
            {
                // Garantir que propriedades required estejam preenchidas
                if (string.IsNullOrWhiteSpace(FuncionarioSelecionado.Nome) ||
                    string.IsNullOrWhiteSpace(FuncionarioSelecionado.Login) ||
                    string.IsNullOrWhiteSpace(FuncionarioSelecionado.senha))
                {
                    // Aqui você pode exibir uma mensagem de erro para o usuário
                    return;
                }

                Funcionarios.Add(new Funcionario
                {
                    ID = Funcionarios.Count + 1,
                    Nome = FuncionarioSelecionado.Nome,
                    Login = FuncionarioSelecionado.Login,
                    senha = FuncionarioSelecionado.senha
                });

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

        private bool PodeAdicionar()
        {
            return FuncionarioSelecionado != null &&
                   !string.IsNullOrWhiteSpace(FuncionarioSelecionado.Nome) &&
                   !string.IsNullOrWhiteSpace(FuncionarioSelecionado.Login) &&
                   !string.IsNullOrWhiteSpace(FuncionarioSelecionado.senha);
        }

        private bool PodeEditarOuRemover() => FuncionarioSelecionado != null;

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? nome = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nome));
    }
}
