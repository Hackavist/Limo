﻿using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using BaseMvvmToolkit.Services;
using BaseMvvmToolkit.ViewModels;
using Limo.DataBase.Repository;
using Limo.Models;
using Xamarin.Forms;

namespace Limo.ViewModels
{
    public class TestViewModel : BaseViewModel
    {
        public string color { get; set; }
        public string model { get; set; }
        public DateTime pdate { get; set; }
        public ObservableCollection<Car> Cars { get; set; }
        public Command Savecommand { get; set; }

        public TestViewModel(INavigationService navigationService) : base(navigationService)
        {
            Icon = "map.png";
            Title = "test";
            Savecommand = new Command(save);
            Cars = new ObservableCollection<Car>();
        }

        private async void save(object obj)
        {
            var x = new Car() { Model = model, Color = color, ProducationDate = pdate };
            var rep = new CarsRepository(App.DbPath);
            var a = await rep.InsertAsync(x);
            Cars = new ObservableCollection<Car>(await rep.GetAllAsync());
        }
    }
}
