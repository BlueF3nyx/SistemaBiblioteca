using Microsoft.Maui.Controls;
using SistemaBiblioteca.Views;

namespace SistemaBiblioteca;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState activationState)
    {
        var window = new Window(new NavigationPage(new LoginPage()));
        return window;
    }
}
