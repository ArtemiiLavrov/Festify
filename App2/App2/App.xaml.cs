using Android.Content.Res;
using Android.Service.Autofill;
using App2.Droid;
using Newtonsoft.Json;
using Plugin.Settings;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Android.Telecom.Call;

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
                //NavigationPage.SetHasNavigationBar(this, false);
                //NavigationPage.SetHasBackButton(this, false);
                MainPage = navigationPage;
            }
            else
            { 
                MainPage = new NavigationPage(new MainPage());
            }
           
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
