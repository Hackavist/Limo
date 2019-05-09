using System;
using BaseMvvmToolkit.Services;
using BaseMvvmToolkit.ViewModels;

namespace Limo.ViewModels
{
    public class UserTabbedViewModel : TabbedViewModel<HomeViewModel, HistoryViewModel, UserTabbedViewModel>
    {
        public UserTabbedViewModel(INavigationService navigationService) : base(navigationService)
        {
        }
    }
}
