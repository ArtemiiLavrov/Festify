
using Android.Widget;
using Firebase.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using static Android.Media.Audiofx.DynamicsProcessing;

namespace App2
{
    public class FirebaseStorageHelper
    {
        FirebaseStorage firebaseStorage = new FirebaseStorage("gs://bulletin-app-1644c.appspot.com");

        public async Task<string> UploadImage(System.IO.Stream imageStream, string imageName)
        {
            var imageUrl = await firebaseStorage.Child("images").Child(imageName).PutAsync(imageStream);

            return imageUrl;
        }
        public async Task<string> UploadImageFromFile(string filePath, string imageName)
        {
            using (var fileStream = File.Open(filePath, FileMode.Open))
            {
                var imageUrl = await firebaseStorage
                    .Child("images")
                    .Child(imageName).PutAsync(fileStream);
                return imageUrl;
            }
        }
    }
}
