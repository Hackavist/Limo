using System;
using System.Collections.Generic;
using System.Text;
using BaseMvvmToolkit.Services;

namespace Limo.ViewModels
{
    class HomeViewModel : BaseMvvmToolkit.ViewModels.BaseViewModel
    {
        public HomeViewModel(INavigationService navigationService) : base(navigationService)
        {

        }
    }
}
