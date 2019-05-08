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
        public string NationalIDNumber { get; set; }
        public string CreditCardNumber { get; set; }
        public string Balance { get; set; }
        public Command SaveCMD { get; set; }

        public SignUpViewModel(INavigationService navigationService) : base(navigationService)
        {
            SaveCMD = new Command(saveAsync);
        }

        private async void saveAsync(object obj)
        {
            if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(PhoneNumber) || string.IsNullOrWhiteSpace(NationalIDNumber) || string.IsNullOrWhiteSpace(CreditCardNumber) || string.IsNullOrWhiteSpace(Balance))
            {
                AlertConfig alert = new AlertConfig();
                alert.Title = "Invalid Data";
                alert.Message = "Data is Missing Or Incorrect";
                UserDialogs.Instance.Alert(alert);
                return;
            }
            User temp = new User() { Name = Name , NationalId = NationalIDNumber , PhoneNumber = PhoneNumber , CrieditCard = new CreditCard() { CardNumber = CreditCardNumber , Balance = Convert.ToDecimal(Balance) } };
            var userrepo = new UserRepository(App.DbPath);
            var user = await userrepo.InsertAsync(temp);
            App.ActiveUser = user;

            await NavigationService.NavigateToAsync<HomeTabbedViewModel>();
        }
    }
}
