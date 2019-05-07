using BaseMvvmToolkit.Services;
using BaseMvvmToolkit.ViewModels;

namespace Limo.ViewModels
{
    public class SignUpViewModel : BaseViewModel
    {
        public SignUpViewModel(INavigationService navigationService):base(navigationService)
        {
        }
    }
}
