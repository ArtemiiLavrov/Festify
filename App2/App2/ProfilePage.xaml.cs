using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();
        }
        private async void DeregButtonClicked(object sender, EventArgs e)
        {
            Preferences.Set("auth", false);
            NavigationPage.SetHasNavigationBar(this, false);
            await Navigation.PushAsync(new MainPage());
        }
    }

}