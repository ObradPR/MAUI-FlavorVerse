using FlavorVerse.BusinessLogic.Dto;
using FlavorVerse.Common;
using FlavorVerse.Extensions;
using FlavorVerse.Validators;
using FluentValidation.Results;
using RestSharp;
using System.Windows.Input;

namespace FlavorVerse.ViewModels
{
    public class RegisterViewModel
    {
        public MProp<string> FirstName { get; set; } = new MProp<string>();

        public MProp<string> LastName { get; set; } = new MProp<string>();

        public MProp<string> Email { get; set; } = new MProp<string>();

        public MProp<string> Phone { get; set; } = new MProp<string>();

        public MProp<string> Password { get; set; } = new MProp<string>();

        public MProp<string> ConfirmPassword { get; set; } = new MProp<string>();

        public MProp<DateOnly?> DateOfBirth { get; set; } = new MProp<DateOnly?>();

        public MProp<bool> ButtonEnabled { get; set; } = new MProp<bool>();

        public MProp<bool> InvalidCredentials { get; set; } = new MProp<bool>();

        public ICommand RegisterCommand { get; }

        // Proxy DateTime property to bind to the DatePicker
        private DateTime? _datePickerDate;

        public DateTime? DatePickerDate
        {
            get => _datePickerDate;
            set
            {
                if (_datePickerDate != value)
                {
                    _datePickerDate = value;
                    OnDateOfBirthChanged();  // Convert DateTime to DateOnly and trigger validation
                }
            }
        }

        private void OnDateOfBirthChanged()
        {
            if (DatePickerDate.HasValue)
            {
                DateOfBirth.Value = DateOnly.FromDateTime(DatePickerDate.Value);
            }
            else
            {
                DateOfBirth.Value = null;
            }

            Validate();
        }

        public RegisterViewModel()
        {
            RegisterCommand = new Command(Register);

            FirstName.OnChange = Validate;
            LastName.OnChange = Validate;
            Email.OnChange = Validate;
            Phone.OnChange = Validate;
            Password.OnChange = Validate;
            ConfirmPassword.OnChange = Validate;
        }

        private void Validate()
        {
            var validator = new RegisterViewModelValidator();

            ValidationResult result = validator.Validate(this);

            if (!result.IsValid)
            {
                FirstName.Error = result.GetError(nameof(FirstName));
                LastName.Error = result.GetError(nameof(LastName));
                Email.Error = result.GetError(nameof(Email));
                Phone.Error = result.GetError(nameof(Phone));
                Password.Error = result.GetError(nameof(Password));
                ConfirmPassword.Error = result.GetError(nameof(ConfirmPassword));
                DateOfBirth.Error = result.GetError(nameof(DateOfBirth));
            }
            else
            {
                FirstName.Error = string.Empty;
                LastName.Error = string.Empty;
                Email.Error = string.Empty;
                Phone.Error = string.Empty;
                Password.Error = string.Empty;
                ConfirmPassword.Error = string.Empty;
                DateOfBirth.Error = string.Empty;

                ButtonEnabled.Value = true;
            }
        }

        private async void Register()
        {
            var data = new SignUpDto
            {
                FirstName = FirstName.Value,
                LastName = LastName.Value,
                Email = Email.Value,
                Phone = Phone.Value,
                Password = Password.Value,
                ConfirmPassword = ConfirmPassword.Value,
                DateOfBirth = DateOfBirth.Value
            };

            var req = new RestRequest("auth/sign-up", Method.Post);
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