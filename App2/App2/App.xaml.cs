using Android.Content.Res;
using Android.Service.Autofill;
using App2.Droid;
using Newtonsoft.Json;
using Plugin.Settings;
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
            if (Preferences.Get("auth", false) == true)
            {
                var myMasterDetailPage = new MyMasterDetailPage(new MenuPage(), new WorkPage());
                var navigationPage = new NavigationPage(myMasterDetailPage);
                navigationPage.BarBackgroundColor = (Color)App.Current.Resources["ToolbarColor"];
                //NavigationPage.SetHasNavigationBar(this, false);
                //NavigationPage.SetHasBackButton(this, false);
                MainPage = navigationPage;
            }
            else
            {
                var newMain = new MainPage();
                var newNavigationPage = new NavigationPage(newMain);
                newNavigationPage.BarBackgroundColor = (Color)App.Current.Resources["ToolbarColor"];
                MainPage = newNavigationPage;
            }
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
