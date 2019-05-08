using System;
using System.Threading.Tasks;
using Acr.UserDialogs;
using BaseMvvmToolkit.Services;
using BaseMvvmToolkit.ViewModels;
using Limo.DataBase.Repository;
using Limo.Models;
using Xamarin.Forms;

namespace Limo.ViewModels
{
    public class ProfileViewModel : BaseViewModel
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

        public ProfileViewModel(INavigationService navigationService) : base(navigationService)
        {
            Name = App.ActiveUser.Name;
            PhoneNumber = App.ActiveUser.PhoneNumber;
            Password = App.ActiveUser.Password;
            Email = App.ActiveUser.Email;
            NationalIDNumber = App.ActiveUser.NationalId;
            SaveCMD = new Command(UpdateAsync);
        }

        private async void UpdateAsync()
        {
            UserDialogs.Instance.ShowLoading();
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
            var cardr = new CreditcardRepository(App.DbPath);
            var user = await userrepo.UpdateAsync(temp);
            var a = cardr.GetAllAsync();
            var s = userrepo.GetAllAsync();
            App.ActiveUser = user;
            UserDialogs.Instance.HideLoading();
        }

        public override void OnAppearing()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                var cardrepo = new CreditcardRepository(App.DbPath);
                var card = await cardrepo.GetByIdAsync(App.ActiveUser.CreditCardId);
                CreditCardNumber = card.CardNumber;
                Balance = card.Balance.ToString();
                OnPropertyChanged(nameof(Balance));
                OnPropertyChanged(nameof(CreditCardNumber));
            });
            base.OnAppearing();
        }

    }
}
