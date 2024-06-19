using Android;
using Android.Content.PM;
using Android.Content.Res;
using Android.Mtp;
using Firebase.Database;
using Firebase.Database.Query;
using Firebase.Storage;
using Java.Util.Jar;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Android.Media.Audiofx.DynamicsProcessing;
using static Xamarin.Essentials.Permissions;

namespace App2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreationOfEventPage : ContentPage
    {
        public CreationOfEventPage()
        {
            InitializeComponent();
        }
        private async void SelectImage_Clicked(object sender, EventArgs e)
        {
            var result = await MediaPicker.PickPhotoAsync();
            FirebaseStorageHelper helper = new FirebaseStorageHelper();
            if (result != null)
            {
                selectedImage.Source = ImageSource.FromStream(() => result.OpenReadAsync().Result);

                using (var imageStream = await result.OpenReadAsync())
                {
                    string imageName = Guid.NewGuid().ToString(); // Генерируем уникальное имя изображения
                    await helper.UploadImage(imageStream, imageName);
                }
            }
            else
            {
                // Вставка загрузки изображения по умолчанию в Firebase Storage
                Stream imageStream;
                var storage = new FirebaseStorage("gs://bulletin-app-1644c.appspot.com");
                using (HttpClient client = new HttpClient())
                {
                    string link = storage.Child("logo.jpg").GetDownloadUrlAsync().Result;
                    HttpResponseMessage response = await client.GetAsync(link);
                    imageStream = await response.Content.ReadAsStreamAsync();
                }
                string defaultImageName = "default_image";
                //AssetManager assets = Android.App.Application.Context.Assets;
                //using (Stream fileStream = assets.Open("Images/logo.jpg"))
                //{
                    await helper.UploadImage(imageStream, defaultImageName);
                //}
            }
        }
        private async void AcceptButtonClicked(object sender, EventArgs e)
        {
            int day;
            Int32.TryParse(Day.Text, out day);
            int month;
            Int32.TryParse(Month.Text, out month);
            int year;
            Int32.TryParse(Year.Text, out year);
            int price;
            Int32.TryParse(Price.Text, out price);
            int age;
            Int32.TryParse(MinimalAge.Text, out age);
            var eventModel = new Event()
            {
                Name = Name.Text,
                Type = Type.Text,
                Description = Description.Text,
                Address = Address.Text,
                Day = day,
                Month = month,
                Year = year,
                Duration = Duration.Text,
                Price = price,
                MinimalAge = age,
                Id = Preferences.Get("UUID", "1")
            };

            FirebaseClient client = new FirebaseClient("https://bulletin-app-1644c-default-rtdb.europe-west1.firebasedatabase.app/");
            string jsonString = JsonConvert.SerializeObject(eventModel);
            await client.Child("Events").PostAsync(jsonString);
            await DisplayAlert("Объявление", "Объявление успешно размещено", "Хорошо");
            await Navigation.PushAsync(new MyMasterDetailPage(new MenuPage(), new WorkPage())); 
        }
    }
}