using Android;
using Android.Content.PM;
using Android.Content.Res;
using Firebase.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Android.Media.Audiofx.DynamicsProcessing;

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
                string defaultImageName = "default_image.jpg";
                AssetManager assets = Android.App.Application.Context.Assets;
                using (var fileStream = assets.Open("Images/logo.jpg"))
                {
                    await helper.UploadImage(fileStream, defaultImageName);
                }

            }
        }

    }
}