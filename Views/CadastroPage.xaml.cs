namespace SistemBiblioteca.Views
{
    public partial class CadastroPage : ContentPage
    {
        public CadastroPage()
        {
            InitializeComponent(); // CORRIGIDO: Sem erro de digitação
        }

        private void OnRegisterClicked(object sender, EventArgs e)
        {
            // Lógica de cadastro
            DisplayAlert("Sucesso", "Cadastro realizado com sucesso!", "OK");
        }

        private async void OnLoginTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
        }
    }
}
