using System;
using BaseMvvmToolkit.Services;
using BaseMvvmToolkit.ViewModels;
using Xamarin.Forms;

namespace Limo.ViewModels
{
    public class AdminViewModel : BaseViewModel
    {
        #region car
        public string color { get; set; }
        public string model { get; set; }
        public DateTime pdate { get; set; }
        #endregion

        #region Driver

        #endregion

        #region User
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string NationalIDNumber { get; set; }
        public string CreditCardNumber { get; set; }
        public string Balance { get; set; }
        public bool EnableBTN { get; set; } = true;
        public Command SaveCMD { get; set; }
        #endregion
        public AdminViewModel(INavigationService navigationService):base(navigationService)
        {
        }

    }
}
