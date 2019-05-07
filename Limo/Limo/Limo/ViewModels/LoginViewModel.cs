using System;
using BaseMvvmToolkit.Services;
using BaseMvvmToolkit.ViewModels;

namespace Limo.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public LoginViewModel(INavigationService navigationService) : base(navigationService)
        {
        }
    }
}
