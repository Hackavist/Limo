using System;
using Acr.UserDialogs;
using BaseMvvmToolkit.Services;
using BaseMvvmToolkit.ViewModels;
using Limo.DataBase.Repository;
using Limo.Models;
using Xamarin.Forms;

namespace Limo.ViewModels
{
    public class AdminViewModel : BaseViewModel
    {
        #region car
        public string color { get; set; }
        public string model { get; set; }
        public DateTime pdate { get; set; }
        public Command SaveCarCMD { get; set; }
        #endregion

        #region Driver
        public string DriverName { get; set; }
        public string DriverPhoneNumber { get; set; }
        public string DriverNationalID { get; set; }
        public Command SaveDriverCMD { get; set; }
        #endregion

        #region User
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string NationalIDNumber { get; set; }
        public string CreditCardNumber { get; set; }
        public string Balance { get; set; }
        public Command SaveUserCMD { get; set; }
        #endregion
        public AdminViewModel(INavigationService navigationService) : base(navigationService)
        {
            Icon = "admin.png";
            Title = "Admin";
            SaveCarCMD = new Command(savecarAsync);
            SaveDriverCMD = new Command(savedriverAsync);
            SaveUserCMD = new Command(saveuserAsync);
        }

        private async void savecarAsync()
        {
            UserDialogs.Instance.ShowLoading();
            var car = new Car() { Color = color , Model = model , ProducationDate = pdate };
            var CarRepo = new CarsRepository(App.DbPath);
            var res = await CarRepo.InsertAsync(car);
            UserDialogs.Instance.HideLoading();
        }

        private async void savedriverAsync()
        {
            UserDialogs.Instance.ShowLoading();
            var driver = new Driver() { Name = DriverName , NationalId = DriverNationalID , PhoneNumber = DriverPhoneNumber };
            var driverrepo = new DriverRepository(App.DbPath);
            var res = await driverrepo.InsertAsync(driver);
            UserDialogs.Instance.HideLoading();
        }

        private async void saveuserAsync()
        {
            UserDialogs.Instance.ShowLoading();
            User temp = new User() { Name = Name , Email = Email , Password = Password , NationalId = NationalIDNumber , PhoneNumber = PhoneNumber , CrieditCard = new CreditCard() { CardNumber = CreditCardNumber , Balance = Convert.ToDouble(Balance) } };
            var userrepo = new UserRepository(App.DbPath);
            var res = await userrepo.InsertAsync(temp);
            UserDialogs.Instance.HideLoading();
        }
    }
}
