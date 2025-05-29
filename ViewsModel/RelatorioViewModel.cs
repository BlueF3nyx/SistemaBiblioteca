using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using SistemaBiblioteca.Models;
using SistemaBiblioteca.Services;

namespace SistemaBiblioteca.ViewsModel
{
    public class RelatorioViewModel : INotifyPropertyChanged
    {
        // Serviços e fontes de dados
        private readonly RelatorioService _relatorioService;
        private readonly IEnumerable<Livros> _livros;
        private readonly IEnumerable<Membro> _membros;
        private readonly IEnumerable<Emprestimo> _emprestimos;
        private readonly IEnumerable<Funcionario> _funcionarios;
        private readonly IEnumerable<HistoricoEmprestimo> _historico;

        public event PropertyChangedEventHandler PropertyChanged;

        // Propriedades que representam os totais mostrados no relatório
        public int TotalLivrosDisponiveis { get; private set; }
        public int TotalLivrosEmprestados { get; private set; }
        public int TotalMembros { get; private set; }
        public int TotalFuncionarios { get; private set; }
        public int TotalEmprestimosAtivos { get; private set; }
        public int TotalEmprestimosConcluidos { get; private set; }

        // Lista observável que atualiza a UI quando alterada
        public ObservableCollection<Emprestimo> EmprestimosFiltrados { get; } = new();

        // Datas utilizadas para filtrar os empréstimos
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

        // Comando usado na interface (ex: botão) para aplicar o filtro
        public ICommand FiltrarCommand { get; }

        // Construtor recebe as listas e inicializa os totais e comandos
        public RelatorioViewModel(
            IEnumerable<Livros> livros,
            IEnumerable<Membro> membros,
            IEnumerable<Emprestimo> emprestimos,
            IEnumerable<Funcionario> funcionarios,
            IEnumerable<HistoricoEmprestimo> historico)
        {
            _livros = livros;
            _membros = membros;
            _emprestimos = emprestimos;
            _funcionarios = funcionarios;
            _historico = historico;

            _relatorioService = new RelatorioService();

            // Associa o comando ao método de filtro
            FiltrarCommand = new RelayCommand(async () => await AplicarFiltro());

            // Calcula os totais iniciais
            CarregarTotais();
        }

        // Método opcional que pode ser chamado ao carregar a página
        public async Task InicializarAsync()
        {
            await AplicarFiltro();
        }

        // Calcula os totais usando métodos do serviço
        private void CarregarTotais()
        {
            TotalLivrosDisponiveis = _relatorioService.TotalLivrosDisponiveis(_livros);
            TotalLivrosEmprestados = _relatorioService.TotalLivrosEmprestados(_livros);
            TotalMembros = _relatorioService.TotalMembros(_membros);
            TotalFuncionarios = _relatorioService.TotalFuncionarios(_funcionarios);
            TotalEmprestimosAtivos = _relatorioService.TotalEmprestimosAtivos(_emprestimos);
            TotalEmprestimosConcluidos = _relatorioService.TotalEmprestimosConcluidos(_emprestimos);
        }

        // Aplica o filtro de datas e ordena por data mais recente
        private async Task AplicarFiltro()
        {
            var resultado = _relatorioService
                .EmprestimosPorData(_emprestimos, DataInicio, DataFim)
                .OrderByDescending(e => e.Data_Emprestimo) // Ordena por data mais recente
                .ToList();

            // Atualiza a coleção observável para a interface refletir
            EmprestimosFiltrados.Clear();
            foreach (var item in resultado)
            {
                EmprestimosFiltrados.Add(item);
            }
        }

        // Notifica a interface sobre alterações de propriedade
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    // Classe auxiliar para lidar com comandos assíncronos
    public class RelayCommand : ICommand
    {
        private readonly Func<Task> _execute;
        private readonly Func<bool>? _canExecute;

        public RelayCommand(Func<Task> execute, Func<bool>? canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter) => _canExecute == null || _canExecute();

        public async void Execute(object? parameter)
        {
            await _execute();
        }

        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
