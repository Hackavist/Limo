using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using System.IO;
using TK.CustomMap.Droid;
using Plugin.CurrentActivity;
using Acr.UserDialogs;

namespace Limo.Droid
{
    [Activity(Label = "Limo" , Icon = "@mipmap/icon" , Theme = "@style/MainTheme" , MainLauncher = true , ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);
            App.DbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal) , "RentalDb.db");
            UserDialogs.Init(this);
            TKGoogleMaps.Init(this, savedInstanceState);
            CrossCurrentActivity.Current.Init(this, savedInstanceState);

            Xamarin.Essentials.Platform.Init(this , savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this , savedInstanceState);
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode , string[] permissions , [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode , permissions , grantResults);

            base.OnRequestPermissionsResult(requestCode , permissions , grantResults);
        }
    }
}