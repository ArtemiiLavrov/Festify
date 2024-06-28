using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Mtp;
using Android.Preferences;
using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json;
using Org.W3c.Dom;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Android.Provider.CalendarContract;
using static Android.Provider.ContactsContract.CommonDataKinds;

namespace App2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyEventPage : ContentPage
    {
        FirebaseClient client = new FirebaseClient("https://bulletin-app-1644c-default-rtdb.europe-west1.firebasedatabase.app/");
        Event thisEvent;
        public MyEventPage()
        {
            StackLayout stackLayout = new StackLayout();
            Label nonExistentBulletin = new Label()
            {
                Text = "Данное объявление не существует, так как было удалено",
                TextColor = Color.White,
                FontAttributes = FontAttributes.Bold,
                FontFamily = "conthrax-sb",
                FontSize = 20,
                HorizontalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center
            };
            stackLayout.Children.Add(nonExistentBulletin);  
        }
        public MyEventPage(Event e)
        {
                InitializeComponent();
                thisEvent = e;
                Name.Text = e.Name;
                Description.Text = e.Description;
                Type.Text = e.Type;
                Address.Text = e.Address;
                Date.Text = $"{e.Day}/{e.Month}/{e.Year}";
                Duration.Text = e.Duration;
                Price.Text = $"{e.Price} рублей";
                MinimalAge.Text = e.MinimalAge.ToString();
                LoadFavoriteEvents();
        }
        private async void DeleteEvent(object sender, EventArgs e)
        {
            await DeleteObjectByComparisonAsync("Events", thisEvent);
        }
        public async Task DeleteObjectByComparisonAsync(string node, Event targetEvent)
        {
            try
            {
                var allEvents = await client.Child(node).OnceAsync<Event>();

                foreach (var firebaseEvent in allEvents)
                {
                    var eventData = firebaseEvent.Object;
                    if (targetEvent.Equals(eventData))
                    {
                        await client.Child(node).Child(firebaseEvent.Key).DeleteAsync();
                        await DisplayAlert("Удаление", "Объявление было успешно удалено", "ОК");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ошибка", ex.Message, "OK");
            }
        }
        private async void AddToFavorite(object sender, EventArgs e)
        {
            FirebaseClient firebaseClient = new FirebaseClient("https://bulletin-app-1644c-default-rtdb.europe-west1.firebasedatabase.app/");
            //string jsonString = JsonConvert.SerializeObject(thisEvent);
            var events = await firebaseClient
                    .Child("Users").Child("Users").Child(Preferences.Get("UUID", "1")).Child("FavoriteEvents")
                    .OnceAsync<Event>();
            List<Event> eventsList = new List<Event>();
            foreach (var eventSnapshot in events)
            {
                eventsList.Add(eventSnapshot.Object);
            }
            bool contains = eventsList.Contains(thisEvent);
            if (contains)
            {
                var favoriteEvents = await client
                .Child("Users").Child("Users").Child(Preferences.Get("UUID", "1")).Child("FavoriteEvents")
                .OnceAsync<Event>();
                foreach (var eventSnapshot in favoriteEvents)
                {
                    if (eventSnapshot.Object.Equals(thisEvent))
                    {
                        await client.Child("Users").Child("Users").Child(Preferences.Get("UUID", "1")).Child("FavoriteEvents").Child(eventSnapshot.Key).DeleteAsync();
                        break;
                    }
                }
                FavButton.BackgroundColor = Color.FromHex("#952ab8");
                FavButton.TextColor = Color.White;
                FavButton.Text = "Добавить в избранное";
            }
            else
            {
                string jsonString = JsonConvert.SerializeObject(thisEvent);
                await client.Child("Users").Child("Users").Child(Preferences.Get("UUID", "1")).Child("FavoriteEvents").PostAsync(jsonString);
                FavButton.BackgroundColor = Color.Green;
                FavButton.TextColor = Color.White;
                FavButton.Text = "В избранном";
            }
        }
        
        private async void LoadFavoriteEvents()
        {
            string uuid = Preferences.Get("UUID", "1");
            var favoriteEvents = await client
                .Child("Users").Child("Users").Child(uuid).Child("FavoriteEvents")
                .OnceAsync<Event>();
            List<Event> favoriteEventsList = new List<Event>();
            foreach (var eventSnapshot in favoriteEvents)
            {
                favoriteEventsList.Add(eventSnapshot.Object);
            }
            bool isFavorite = favoriteEventsList.Contains(thisEvent);
            if (isFavorite)
            {
                FavButton.BackgroundColor = Color.Green;
                FavButton.TextColor = Color.White;
                FavButton.Text = "В избранном";
            }
        }

    }
}