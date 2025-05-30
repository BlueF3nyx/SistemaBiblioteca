using Microsoft.Maui.Controls;
using SistemaBiblioteca.Views;

namespace SistemaBiblioteca
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new LoginPage()); 
        }
    }
}
