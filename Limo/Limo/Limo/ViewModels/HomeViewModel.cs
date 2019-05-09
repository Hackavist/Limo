using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using BaseMvvmToolkit.Services;
using Limo.DataBase.Repository;
using Limo.Models;
using Plugin.Geolocator;
using TK.CustomMap;
using Xamarin.Forms;

namespace Limo.ViewModels
{
    public class HomeViewModel : BaseMvvmToolkit.ViewModels.BaseViewModel
    {
        public TKCustomMapPin SelectedPin { get; set; }
        public ObservableCollection<TKCustomMapPin> Pins { get; set; }
        public ObservableCollection<Car> Cars { get; set; }
        public ObservableCollection<Driver> Drivers { get; set; }
        public Car SelectedCar { get; set; }
        public Driver SelectedDriver { get; set; }
        public Command Drag { get; set; }
        public Command Drop { get; set; }
        public Command OrderCMD { get; set; }
        public CarsRepository Carrepository { get; set; }
        public RequestRepository Requestrepo { get; set; }
        public DriverRepository DriverRepo { get; set; }
        public bool WithDriver { get; set; } = false;

        public HomeViewModel(INavigationService navigationService) : base(navigationService)
        {
            Icon = "map.png";
            Title = "Home";
            SelectedPin = new TKCustomMapPin();
            SelectedPin.IsDraggable = true;
            Pins = new ObservableCollection<TKCustomMapPin>();
            Cars = new ObservableCollection<Car>();
            Drivers = new ObservableCollection<Driver>();
            SelectedCar = new Car();
            SelectedDriver = new Driver();
            Drag = new Command(drag);
            Drop = new Command(drop);
            OrderCMD = new Command(orderAsync);
            Carrepository = new CarsRepository(App.DbPath);
            DriverRepo = new DriverRepository(App.DbPath);
            Requestrepo = new RequestRepository(App.DbPath);
        }

        private async void orderAsync(object obj)
        {
            UserDialogs.Instance.ShowLoading();
            Request x = new Request() { CarId = SelectedCar.Id, UserId = App.ActiveUser.Id, Price = 50.0 };
            if (WithDriver)
            {
                x.DriverId = SelectedDriver.Id;
                x.Price = 78.5;
                SelectedDriver.IsAvailable = false;
                await DriverRepo.UpdateAsync(SelectedDriver);
            }
            var l = Requestrepo.InsertAsync(x);
            SelectedCar.IsAvailable = false;
            await Carrepository.UpdateAsync(SelectedCar);
            UserDialogs.Instance.HideLoading();
            AlertConfig alert = new AlertConfig();
            alert.Title = "Payment Confirmation";
            alert.Message = "Just Confirming You are Paying " + x.Price + "with your CreditCard";
            alert.OkText = "Pay";
            UserDialogs.Instance.Alert(alert);
            UserDialogs.Instance.ShowLoading();
            var cardrepo = new CreditcardRepository(App.DbPath);
            await cardrepo.UpdateAsync(App.ActiveUser.CrieditCard);
            UserDialogs.Instance.HideLoading();
            alert.Title = "Sucess";
            alert.Message = "Done , Your Balance is " + (App.ActiveUser.CrieditCard.Balance - x.Price);
            UserDialogs.Instance.Alert(alert);
        }

        private void drop(object obj)
        {
            throw new NotImplementedException();
        }

        private void drag(object obj)
        {
            throw new NotImplementedException();
        }

        public override Task Init(object args)
        {
            UserDialogs.Instance.ShowLoading();
            Task.Run(async () =>
            {
                var x = await CrossGeolocator.Current.GetPositionAsync();
                SelectedPin.Position = new Position(x.Latitude, x.Longitude);
                Pins.Add(SelectedPin);
            });
            return base.Init(args);
        }

        public override void OnAppearing()
        {
            UserDialogs.Instance.ShowLoading();
            Device.BeginInvokeOnMainThread(async () =>
            {
                var x = await Carrepository.GetAllAsync();
                foreach (var item in x)
                {
                    if (!Cars.Contains(item))
                    {
                        Cars.Add(item);
                    }
                }

                var d = await DriverRepo.GetAllAsync();
                foreach (var item in d)
                {
                    if (!Drivers.Contains(item))
                    {
                        Drivers.Add(item);
                    }
                }
                UserDialogs.Instance.HideLoading();
            });
            base.OnAppearing();
        }
    }
}
