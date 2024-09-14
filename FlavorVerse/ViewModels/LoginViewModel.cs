using FlavorVerse.BusinessLogic.Dto;
using FlavorVerse.Common;
using FlavorVerse.Extensions;
using FlavorVerse.Validators;
using FluentValidation.Results;
using RestSharp;
using System.Windows.Input;

namespace FlavorVerse.ViewModels
{
    public class LoginViewModel
    {
        public MProp<string> Email { get; set; } = new MProp<string>();

        public MProp<string> Password { get; set; } = new MProp<string>();

        public MProp<bool> ButtonEnabled { get; set; } = new MProp<bool>();

        public MProp<bool> InvalidCredentials { get; set; } = new MProp<bool>();

        public ICommand LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new Command(Login);

            Email.OnChange = Validate;
            Password.OnChange = Validate;
        }

        private void Validate()
        {
            var validator = new LoginViewModelValidator();

            ValidationResult result = validator.Validate(this);

            if (!result.IsValid)
            {
                Email.Error = result.GetError(nameof(Email));
                Password.Error = result.GetError(nameof(Password));
            }
            else
            {
                Email.Error = string.Empty;
                Password.Error = string.Empty;

                ButtonEnabled.Value = true;
            }
        }

        private async void Login()
        {
            var data = new SignInDto
            {
                Email = Email.Value,
                Password = Password.Value,
            };

            var req = new RestRequest("auth/sign-in", Method.Post);
            req.AddJsonBody(data);

            var res = Api.Client.Execute<TokenDto>(req);

            if (res.IsSuccessful)
            {
                await SecureStorage.Default.SetAsync("token", res.Data!.Token);

                UserDto user = SecureStorage.Default.GetUser();

                InvalidCredentials.Value = false;

                App.Current.MainPage = new MainPage();
            }
            else
            {
                InvalidCredentials.Value = true;
            }
        }
    }
}