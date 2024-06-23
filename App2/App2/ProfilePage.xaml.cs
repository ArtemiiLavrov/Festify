using Java.Util;
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
            Profile.Source = ImageSource.FromResource("App2.Images.profile.png");
            CreateUserData();
        }
        public void CreateUserData()
        {
            Name.Text = Preferences.Get("username", "John Doe");
            Email.Text = Preferences.Get("email", "festify.help@gmail.com");
        }
        private async void DeregButtonClicked(object sender, EventArgs e)
        {
            Preferences.Set("auth", false);
            NavigationPage.SetHasNavigationBar(this, false);
            await Navigation.PopToRootAsync();
            await Navigation.PushAsync(new MainPage());
        }
    }

}