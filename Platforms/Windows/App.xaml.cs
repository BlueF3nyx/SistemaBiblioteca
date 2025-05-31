using System.Diagnostics;
using Microsoft.UI.Xaml;

namespace SistemaBiblioteca.WinUI
{
    public partial class App : MauiWinUIApplication
    {
        public App()
        {
            this.InitializeComponent();

            // Evento para tratar exceções não capturadas
            this.UnhandledException += App_UnhandledException;
        }

        private void App_UnhandledException(object sender, Microsoft.UI.Xaml.UnhandledExceptionEventArgs e)
        {
            // Loga a exceção para ajudar a diagnosticar
            Debug.WriteLine($"Unhandled Exception: {e.Exception.Message}");

            
            e.Handled = true;

            // Opcional: só pausa o debugger se estiver anexado
            if (Debugger.IsAttached)
            {
                Debugger.Break();
            }
        }

        protected override MauiApp CreateMauiApp() => SistemaBiblioteca.MauiProgram.CreateMauiApp();
    }
}
