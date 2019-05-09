using System;
using Acr.UserDialogs;
using BaseMvvmToolkit.Services;
using BaseMvvmToolkit.ViewModels;
using Limo.DataBase.Repository;
using Limo.Models;
using Xamarin.Forms;

namespace Limo.ViewModels
{
    public class SignUpViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string NationalIDNumber { get; set; }
        public string CreditCardNumber { get; set; }
        public string Balance { get; set; }
        public bool EnableBTN { get; set; } = true;
        public Command SaveCMD { get; set; }

        public SignUpViewModel(INavigationService navigationService) : base(navigationService)
        {
            SaveCMD = new Command(saveAsync);
        }

        private async void saveAsync(object obj)
        {
            UserDialogs.Instance.ShowLoading();
            EnableBTN = false;
            if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(PhoneNumber) || string.IsNullOrWhiteSpace(NationalIDNumber) || string.IsNullOrWhiteSpace(CreditCardNumber) || string.IsNullOrWhiteSpace(Balance))
            {
                UserDialogs.Instance.HideLoading();
                EnableBTN = true;
                AlertConfig alert = new AlertConfig();
                alert.Title = "Invalid Data";
                alert.Message = "Data is Missing Or Incorrect";
                UserDialogs.Instance.Alert(alert);
                return;
            }
            User temp = new User() { Name = Name, Email = Email, Password = Password, NationalId = NationalIDNumber, PhoneNumber = PhoneNumber, CrieditCard = new CreditCard() { CardNumber = CreditCardNumber, Balance = Convert.ToDouble(Balance) } };
            var userrepo = new UserRepository(App.DbPath);
            var user = await userrepo.InsertAsync(temp);
            App.ActiveUser = user;
            UserDialogs.Instance.HideLoading();
            NavigationService.SetMainViewModel<HomeTabbedViewModel>();
        }
    }
}
