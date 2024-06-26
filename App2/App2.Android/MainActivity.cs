﻿using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Android.Content;
using Android.Preferences;

namespace App2.Droid
{
    [Activity(Label = "Festify", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            Firebase.FirebaseApp.InitializeApp(Application.Context);
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }
        //public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        //{
        //    Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

        //    base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        //}
        // Сохранение токена доступа при успешной авторизации
        //public void SaveAccessToken(string accessToken)
        //{
        //    ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(Application.Context);
        //    ISharedPreferencesEditor editor = prefs.Edit();
        //    editor.PutString("accessToken", accessToken);
        //    editor.Apply();
        //}

        // Проверка состояния авторизации при запуске приложения
        //private bool IsUserLoggedIn()
        //{
        //    ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(Application.Context);
        //    return !string.IsNullOrEmpty(prefs.GetString("accessToken", null));
        //}
    }
}