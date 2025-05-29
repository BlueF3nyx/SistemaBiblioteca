
using SistemBiblioteca.Views;

namespace SistemaBiblioteca
{
    public partial class App : Application
    {
        public App()
        {
            CadastroPage = new NavigationPage(new CadastroPage()); // Nome correto
        }

        public NavigationPage CadastroPage { get; }
    }
}