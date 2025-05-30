using Microsoft.Maui;
using Microsoft.Maui.Hosting;


namespace SistemaBiblioteca
{
    public class Program : MauiApplication
    {
        public Program() : base() { }

        protected override MauiApp CreateMauiApp()
        {
            return MauiProgram.CreateMauiApp();
        }

        public static void Main(string[] args)
        {
            var app = new Program();
            app.Run(args);
        }

        private void Run(string[] args)
        {
            throw new NotImplementedException();
        }
    }

    
}
