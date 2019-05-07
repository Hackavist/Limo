using System;
using System.Reflection;
using Autofac;
using BaseMvvmToolkit.Extensions;
using BaseMvvmToolkit.Services;
using Limo.Models;
using Limo.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Limo
{

    public static class AppContainer
    {
        public static IContainer Container { get; set; }
    }

    public partial class App : Application
    {
        public IContainer Container { get; private set; }
        public static double ScreenHeight;
        public static double ScreenWidth;
        public static string DbPath = "";
        public static User ActiveUser;

        public App()
        {
            InitializeComponent();
            SetupDependencyInjection();
            SetStartPage();

        }

        private void SetupDependencyInjection()
        {
            if (Container != null)
            {
                return;
            }
            var builder = new ContainerBuilder();
            builder.RegisterMvvmComponents(typeof(App).GetTypeInfo().Assembly);
            builder.RegisterType<NavigationService>().AsImplementedInterfaces().SingleInstance();
            Container = builder.Build();
            AppContainer.Container = Container;
        }
        private void SetStartPage()
        {
            var navigationService = Container.Resolve<INavigationService>();
            ActiveUser = new User();
            navigationService.SetMainViewModel<SignUpViewModel>();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
