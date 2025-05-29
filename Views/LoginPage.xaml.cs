namespace SistemBiblioteca.Views // CORREÇÃO: Namespace exato
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent(); // CORREÇÃO: Ortografia correta
        }
        private void OnLoginClicked(object sender, EventArgs e)
        {
            // Lógica de login
        }

        private void OnForgotPasswordTapped(object sender, EventArgs e)
        {
            // Navega para recuperação de senha
        }

        private async void OnRegisterTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CadastroPage());
        }
    }
}