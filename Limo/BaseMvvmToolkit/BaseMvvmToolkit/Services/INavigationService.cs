using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BaseMvvmToolkit.ViewModels;
using Xamarin.Forms;

namespace BaseMvvmToolkit.Services
{
    public interface INavigationService
    {
        INavigation CurrentNavigation { get; }

        Task<bool> DisplayAlert(string title, string message, string accept, string cancel);

        Task<string> DisplayActionSheet(string title, string cancel, string destruction, string[] buttons);

        void SetMainViewModel<T>(object args = null) where T : IBaseViewModel;

        Task NavigateToAsync<T>(object args = null) where T : IBaseViewModel;

        Task NavigateToAsync(Type baseViewModelPage, object args = null);

        Task NavigateToModalAsync<T>(object args = null) where T : IBaseViewModel;

        Task NavigateToModalAsync(Type baseViewModelPage, object args = null);

        void NavigateToMenuItem<T>(object args = null) where T : IBaseViewModel;

        Task PopAsync(object args = null);

        Task PopModalAsync(object args = null);

        Task PopToRootAsync(object args = null);

        Page ResolvePageFor<T>(object args = null) where T : IBaseViewModel;

        void RemoveFromNavigationStack<T>(bool removeFirstOccurenceOnly = true) where T : IBaseViewModel;

        IReadOnlyList<IBaseViewModel> GetNavigationStack();

        bool IsRootPage { get; }

        IBaseViewModel CurrentViewModel { get; }

        Page CurrentPage { get; }
        IBaseViewModel GetPreviousViewModel();
        void OpenDrawerMenu();

        void CloseDrawerMenu();

        void ToggleDrawerMenu();
    }
}
