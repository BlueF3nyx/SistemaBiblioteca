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
    //interface que notifica a UI sobre mudanças de propriedades.
    public class LivroViewModel : INotifyPropertyChanged
    {
        private Livros livroselecionado;
        private string filtroTitulo;

        //coleção que notifica automaticamente a UI quando é modificada (itens adicionados/removidos)
        public ObservableCollection<Livros> Livros { get; set; } = new ObservableCollection<Livros>();

        public Livros? LivroSelecionado
        {
            get => livroselecionado;
            set
            {
                if (livroselecionado != value)
                {
                    livroselecionado = value;
                    OnPropertyChanged();

                }
            }
        }

        public string FiltroTitulo
        {
            //Armazena um texto usado para filtrar os livros. Pode ser utilizado para implementar uma busca dinâmica.
            get => filtroTitulo;
            set
            {
                if (filtroTitulo != value)
                {
                    filtroTitulo = value;
                    OnPropertyChanged();
                }
            }
        }
        public ICommand AdicionarLivroCommand { get; }
        public ICommand AtualizarLivroCommand { get; }
        public ICommand RemoverLivroCommand { get; }
    
    public LivroViewModel()
        {
            //Estes comandos são usados para associar ações de botões na interface
            AdicionarLivroCommand = new Command(AdicionarLivro);
            AtualizarLivroCommand = new Command(AtualizarLivro, PodeEditarOuRemover);
            RemoverLivroCommand = new Command(RemoverLivro, PodeEditarOuRemover);
            Livros.Add(new Livros { ID = 1, Titulo = "Curso Intensivo de Python", Autor = "Eric Matthes", Categoria = "Exatas e Tecnológicas", Disponibilidade = true });


        }
        private void AdicionarLivro()
        {
            if (LivroSelecionado != null)
            {
                // Aqui deveria ter lógica para adicionar (ex: chamar serviço, BD)
                Livros.Add(LivroSelecionado);
                LivroSelecionado = null; // limpa seleção
            }
        }
        private void AtualizarLivro()
        {
            // Implementar atualização via serviço/banco
            // Com ObservableCollection, alterações nas propriedades da entidade refletem na UI
        }
        private void RemoverLivro()
        {
            if (LivroSelecionado != null && Livros.Contains(LivroSelecionado))
            {
                Livros.Remove(LivroSelecionado);
                LivroSelecionado = null;
            }
        }
        private bool PodeEditarOuRemover() => LivroSelecionado != null;

        public event PropertyChangedEventHandler PropertyChanged;

        //Esse método é chamado sempre que alguma propriedade muda
        protected void OnPropertyChanged([CallerMemberName] string? nome = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nome));
    }
   }
