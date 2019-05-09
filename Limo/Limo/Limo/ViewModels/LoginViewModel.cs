using System;
using Acr.UserDialogs;
using BaseMvvmToolkit.Services;
using BaseMvvmToolkit.ViewModels;
using Limo.DataBase.Repository;
using Xamarin.Forms;

namespace Limo.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public Command LoginCMD { get; set; }
        public Command NavCMD { get; set; }
        public LoginViewModel(INavigationService navigationService) : base(navigationService)
        {
            LoginCMD = new Command(loginAsync);
            NavCMD = new Command(navigate);
        }

        private void navigate(object obj)
        {
            NavigationService.SetMainViewModel<SignUpViewModel>();
        }

        private async void loginAsync(object obj)
        {
            UserDialogs.Instance.ShowLoading();
            var userrepo = new UserRepository(App.DbPath);
            var res = await userrepo.GetAllAsync();
            foreach (var item in res)
            {
                if (item.Email == Email)
                {
                    if (item.Password == Password)
                    {
                        App.ActiveUser = item;
                        var cardrepo = new CreditcardRepository(App.DbPath);
                        var card = await cardrepo.GetByIdAsync(item.CreditCardId);
                        App.ActiveUser.CrieditCard = card;
                        App.ActiveUser.CrieditCard.Balance = card.Balance;
                        UserDialogs.Instance.HideLoading();
                        NavigationService.SetMainViewModel<HomeTabbedViewModel>();
                        return;
                    }
                }
            }
            UserDialogs.Instance.HideLoading();
            AlertConfig alert = new AlertConfig();
            alert.Title = "User Not Found";
            alert.Message = "UserName Or Password is Incorrect";
            UserDialogs.Instance.Alert(alert);
        }
    }
}
