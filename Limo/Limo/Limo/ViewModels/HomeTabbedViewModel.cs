using System;
using System.Collections.Generic;
using System.Text;
using BaseMvvmToolkit.Services;

namespace Limo.ViewModels
{
    class HomeTabbedViewModel : BaseMvvmToolkit.ViewModels.TabbedViewModel<HomeViewModel,HistoryViewModel,AdminViewModel>
    {
        public HomeTabbedViewModel(INavigationService navigationService) : base(navigationService)
        {
        }
    }
}
