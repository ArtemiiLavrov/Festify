using Android.Content.Res;
using Android.Service.Autofill;
using App2.Droid;
using Newtonsoft.Json;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App2
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            if ((SecureStorage.GetAsync("username").Result != null) &&
                SecureStorage.GetAsync("password").Result != null)
            {
                MainPage = new MyMasterDetailPage(new MenuPage(), new WorkPage());
            }
            else
            {
                MainPage = new NavigationPage(new MainPage());
            }
            //MainPage.BarBackgroundColor = (Color)App.Current.Resources["ToolbarColor"];
            //MainPage = newPage;
        }

        protected override void OnStart()
        {
            //base.OnStart();

            
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
