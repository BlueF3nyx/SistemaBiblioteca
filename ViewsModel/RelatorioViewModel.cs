using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using SistemaBiblioteca.Models;
using SistemaBiblioteca.Services;

namespace SistemaBiblioteca.ViewsModel
{
    // ViewModel que representa os dados e comandos para a tela de relatório
    // Implementa INotifyPropertyChanged para notificar a UI sobre mudanças nas propriedades
    public class RelatorioViewModel : INotifyPropertyChanged
    {
        // Serviço que contém lógica para calcular e filtrar relatórios
        private readonly RelatorioService _relatorioService;

        // Campos privados que guardam as coleções de dados passadas ao ViewModel
        private readonly IEnumerable<Livros> _livros;
        private readonly IEnumerable<Membro> _membros;
        private readonly IEnumerable<Emprestimo> _emprestimos;
        private readonly IEnumerable<Funcionario> _funcionarios;
        private readonly IEnumerable<HistoricoEmprestimo> _historico;

        // Evento padrão do INotifyPropertyChanged usado para notificar mudanças na UI
        public event PropertyChangedEventHandler PropertyChanged;

        // Campos e propriedades para armazenar totais que serão exibidos na UI
        private int _totalLivrosDisponiveis;
        public int TotalLivrosDisponiveis
        {
            get => _totalLivrosDisponiveis;
            private set
            {
                // Atualiza o valor e notifica a UI se mudou
                if (_totalLivrosDisponiveis != value)
                {
                    _totalLivrosDisponiveis = value;
                    OnPropertyChanged(nameof(TotalLivrosDisponiveis));
                }
            }
        }

        private int _totalLivrosEmprestados;
        public int TotalLivrosEmprestados
        {
            get => _totalLivrosEmprestados;
            private set
            {
                if (_totalLivrosEmprestados != value)
                {
                    _totalLivrosEmprestados = value;
                    OnPropertyChanged(nameof(TotalLivrosEmprestados));
                }
            }
        }

        private int _totalMembros;
        public int TotalMembros
        {
            get => _totalMembros;
            private set
            {
                if (_totalMembros != value)
                {
                    _totalMembros = value;
                    OnPropertyChanged(nameof(TotalMembros));
                }
            }
        }

        private int _totalFuncionarios;
        public int TotalFuncionarios
        {
            get => _totalFuncionarios;
            private set
            {
                if (_totalFuncionarios != value)
                {
                    _totalFuncionarios = value;
                    OnPropertyChanged(nameof(TotalFuncionarios));
                }
            }
        }

        private int _totalEmprestimosAtivos;
        public int TotalEmprestimosAtivos
        {
            get => _totalEmprestimosAtivos;
            private set
            {
                if (_totalEmprestimosAtivos != value)
                {
                    _totalEmprestimosAtivos = value;
                    OnPropertyChanged(nameof(TotalEmprestimosAtivos));
                }
            }
        }

        private int _totalEmprestimosConcluidos;
        public int TotalEmprestimosConcluidos
        {
            get => _totalEmprestimosConcluidos;
            private set
            {
                if (_totalEmprestimosConcluidos != value)
                {
                    _totalEmprestimosConcluidos = value;
                    OnPropertyChanged(nameof(TotalEmprestimosConcluidos));
                }
            }
        }

        // Coleção observável que representa a lista de empréstimos filtrados pela UI
        // ObservableCollection notifica automaticamente a UI quando itens são adicionados ou removidos
        public ObservableCollection<Emprestimo> EmprestimosFiltrados { get; } = new();

        // Propriedades de data para filtro na UI (início e fim do período)
        private DateTime _dataInicio = DateTime.Today.AddDays(-7);
        public DateTime DataInicio
        {
            get => _dataInicio;
            set
            {
                if (_dataInicio != value)
                {
                    _dataInicio = value;
                    OnPropertyChanged(nameof(DataInicio));
                }
            }
        }

        private DateTime _dataFim = DateTime.Today;
        public DateTime DataFim
        {
            get => _dataFim;
            set
            {
                if (_dataFim != value)
                {
                    _dataFim = value;
                    OnPropertyChanged(nameof(DataFim));
                }
            }
        }

        // Comando que será ligado a um botão ou ação na UI para aplicar o filtro de empréstimos
        public ICommand FiltrarCommand { get; }

        // Construtor do ViewModel: recebe as coleções de dados e inicializa o serviço e comandos
        public RelatorioViewModel(
            IEnumerable<Livros> livros,
            IEnumerable<Membro> membros,
            IEnumerable<Emprestimo> emprestimos,
            IEnumerable<Funcionario> funcionarios,
            IEnumerable<HistoricoEmprestimo> historico)
        {
            // Guarda as coleções passadas para uso interno do ViewModel
            _livros = livros;
            _membros = membros;
            _emprestimos = emprestimos;
            _funcionarios = funcionarios;
            _historico = historico;

            // Cria o serviço que possui a lógica dos relatórios
            _relatorioService = new RelatorioService();

            // Cria o comando para filtrar que executa a função AplicarFiltro assincronamente
            FiltrarCommand = new RelayCommand(async () => await AplicarFiltro());

            // Carrega os totais iniciais para as propriedades que serão exibidas na UI
            CarregarTotais();
        }

        // Método para inicialização assíncrona (para ser chamado fora do construtor)
        public async Task InicializarAsync()
        {
            // Aplica filtro inicial na lista de empréstimos com as datas padrão
            await AplicarFiltro();
        }

        // Atualiza os totais das propriedades com dados do serviço, notificando a UI
        private void CarregarTotais()
        {
            TotalLivrosDisponiveis = _relatorioService.TotalLivrosDisponiveis(_livros);
            TotalLivrosEmprestados = _relatorioService.TotalLivrosEmprestados(_livros);
            TotalMembros = _relatorioService.TotalMembros(_membros);
            TotalFuncionarios = _relatorioService.TotalFuncionarios(_funcionarios);
            TotalEmprestimosAtivos = _relatorioService.TotalEmprestimosAtivos(_emprestimos);
            TotalEmprestimosConcluidos = _relatorioService.TotalEmprestimosConcluidos(_emprestimos);
        }

        // Aplica o filtro de empréstimos baseado nas datas e atualiza a coleção observável
        private async Task AplicarFiltro()
        {
            // Obtém a lista filtrada usando o serviço
            var resultado = _relatorioService.EmprestimosPorData(_emprestimos, DataInicio, DataFim);

            // Limpa os itens atuais na coleção observável
            EmprestimosFiltrados.Clear();

            // Adiciona os empréstimos filtrados um por um para a coleção observável (notifica UI automaticamente)
            foreach (var item in resultado)
            {
                EmprestimosFiltrados.Add(item);
            }

            // Não é necessário chamar OnPropertyChanged para ObservableCollection,
            // pois ela já notifica mudanças automaticamente
        }

        // Método que dispara o evento PropertyChanged para notificar a UI sobre atualização de uma propriedade
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    // Classe auxiliar que implementa ICommand para comandos assíncronos
    public class RelayCommand : ICommand
    {
        private readonly Func<Task> _execute;  // Ação assíncrona que o comando executa
        private readonly Func<bool>? _canExecute;  // Função que indica se o comando pode ser executado

        public RelayCommand(Func<Task> execute, Func<bool>? canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public event EventHandler? CanExecuteChanged;

        // Verifica se o comando pode ser executado, chama a função opcional _canExecute
        public bool CanExecute(object? parameter) => _canExecute == null || _canExecute();

        // Executa o comando assincronamente quando chamado (por exemplo, botão clicado)
        public async void Execute(object? parameter)
        {
            await _execute();
        }

        // Método para forçar a reavaliação se o comando pode ser executado (ex: para habilitar/desabilitar botão)
        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
