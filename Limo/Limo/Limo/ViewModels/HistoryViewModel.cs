using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using BaseMvvmToolkit.Services;
using Limo.DataBase.Repository;
using Limo.Models;
using Xamarin.Forms;

namespace Limo.ViewModels
{
    class HistoryViewModel : BaseMvvmToolkit.ViewModels.BaseViewModel
    {
        public ObservableCollection<RequestDTO> History { get; set; }
        public CarsRepository Carrepository { get; set; }
        public RequestRepository Requestrepo { get; set; }
        public DriverRepository DriverRepo { get; set; }
        public HistoryViewModel(INavigationService navigationService) : base(navigationService)
        {
            Icon = "history.png";
            Title = "History";
            History = new ObservableCollection<RequestDTO>();
            Carrepository = new CarsRepository(App.DbPath);
            DriverRepo = new DriverRepository(App.DbPath);
            Requestrepo = new RequestRepository(App.DbPath);
        }
        public override Task Init(object args)
        {
            UserDialogs.Instance.ShowLoading();
            Task.Run(async () =>
            {
                var x = await Requestrepo.GetAllAsync();
                foreach (var item in x)
                {
                    var temp = new RequestDTO();
                    temp.CarId = item.CarId;
                    var car = await Carrepository.GetByIdAsync(item.CarId);
                    temp.CarModel = car.Model;
                    temp.Price = item.Price;
                    temp.AddedDate = item.AddedDate;
                    if (item.DriverId > 0)
                    {
                        temp.DriverId = item.DriverId;
                        var driver = await DriverRepo.GetByIdAsync((int)item.DriverId);
                        temp.Drivername = driver.Name;
                    }
                    else
                    {
                        temp.Drivername = "No Driver";
                    }
                    History.Add(temp);
                }
                UserDialogs.Instance.HideLoading();
            });
            return base.Init(args);
        }

        public override void OnAppearing()
        {
            UserDialogs.Instance.ShowLoading();
            Device.BeginInvokeOnMainThread(async () =>
            {
                History.Clear();
                var x = await Requestrepo.GetAllAsync();
                foreach (var item in x)
                {
                    var temp = new RequestDTO();
                    temp.CarId = item.CarId;
                    var car = await Carrepository.GetByIdAsync(item.CarId);
                    temp.CarModel = car.Model;
                    temp.Price = item.Price;
                    temp.AddedDate = item.AddedDate;
                    if (item.DriverId > 0)
                    {
                        temp.DriverId = item.DriverId;
                        var driver = await DriverRepo.GetByIdAsync((int)item.DriverId);
                        temp.Drivername = driver.Name;
                    }
                    else
                    {
                        temp.Drivername = "No Driver";
                    }
                    History.Add(temp);
                }
                UserDialogs.Instance.HideLoading();
            });
            base.OnAppearing();
        }

    }
}
