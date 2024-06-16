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
            var newPage = new NavigationPage(new MainPage());
            newPage.BarBackgroundColor = (Color)App.Current.Resources["ToolbarColor"];
            MainPage = newPage;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
