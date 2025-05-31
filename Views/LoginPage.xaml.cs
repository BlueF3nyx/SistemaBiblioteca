using Microsoft.Maui.Controls;
using SistemaBiblioteca.Data;
using System;
using System.Security.Cryptography;
using System.Text;

namespace SistemaBiblioteca.Views
{
    public partial class LoginPage : ContentPage
    {
        private FuncionarioRepository funcionarioRepo = new FuncionarioRepository();

        public LoginPage()
        {
            InitializeComponent();
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            string? email = EmailEntry.Text?.Trim();
            string senha = SenhaEntry.Text;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(senha))
            {
                await DisplayAlert("Erro", "Por favor, preencha e-mail e senha.", "OK");
                return;
            }

            try
            {
                var funcionario = funcionarioRepo.ObterPorLogin(email);

                if (funcionario == null)
                {
                    await DisplayAlert("Erro", "Usuário não encontrado.", "OK");
                    return;
                }

                string senhaHashDigitada = HashSenha(senha);

                if (funcionario.senha == senhaHashDigitada)
                {
                    await DisplayAlert("Sucesso", $"Bem-vindo, {funcionario.Nome}!", "OK");
                    // Navegue para próxima página, ex:
                    // await Navigation.PushAsync(new MainPage());
                }
                else
                {
                    await DisplayAlert("Erro", "Senha incorreta.", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", $"Falha no login: {ex.Message}", "OK");
            }
        }

        private string HashSenha(string senha)
        {
            using var sha = SHA256.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(senha);
            byte[] hash = sha.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
