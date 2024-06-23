using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App2
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : ContentPage
    {
        public MenuPage()
        {
            Title = "Menu";
            InitializeComponent();
           
        }
        private async void CreateEventButtonClicked(object sender, EventArgs e)
        {
            var createEventButtonClicked = new MyMasterDetailPage(new MenuPage(), new CreationOfEventPage());
            await Navigation.PushAsync(createEventButtonClicked);
        }
        private async void ProfileButtonClicked(object sender, EventArgs e)
        {
            var createEventButtonClicked = new MyMasterDetailPage(new MenuPage(), new ProfilePage());
            await Navigation.PushAsync(createEventButtonClicked);
        }
        private async void MyEventsButtonClicked(object sender, EventArgs e)
        {
            var createEventButtonClicked = new MyMasterDetailPage(new MenuPage(), new MyEventsPage());
            await Navigation.PushAsync(createEventButtonClicked);
        }
        private async void FavoriteEventsButtonClicked(object sender, EventArgs e)
        {
            var createEventButtonClicked = new MyMasterDetailPage(new MenuPage(), new FavoriteEvents());
            await Navigation.PushAsync(createEventButtonClicked);
        }
        private async void AllEventsButtonClicked(object sender, EventArgs e)
        {
            var createEventButtonClicked = new MyMasterDetailPage(new MenuPage(), new WorkPage());
            await Navigation.PushAsync(createEventButtonClicked);
        }
        private async void HelpButtonClicked(object sender, EventArgs e)
        {
            var createEventButtonClicked = new MyMasterDetailPage(new MenuPage(), new HelpPage());
            await Navigation.PushAsync(createEventButtonClicked);
        }
    }
   
}