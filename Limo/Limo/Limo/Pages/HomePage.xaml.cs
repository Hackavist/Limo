using System;
using TK.CustomMap;
using Xamarin.Forms.Xaml;

namespace Limo.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            AdjustZoom();
        }

        private void AdjustZoom()
        {
            try
            {
                tkmap.MoveToMapRegion(MapSpan.FromCenterAndRadius( new Position (30.078241, 31.284960),Distance.FromKilometers(1.0)),true);
            }
            catch (Exception ex)
            {
                string x = ex.Message;
            }
        }
    }
}