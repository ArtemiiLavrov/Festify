using Android.Content;
using Android.Preferences;
using App2.Droid;
using Firebase.Database;
using Firebase.Database.Query;
using Org.Json;
using Plugin.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace App2
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            regImage.Source = ImageSource.FromResource("App2.Images.main.png");
            NavigationPage.SetHasBackButton(this, false);
            NavigationPage.SetHasNavigationBar(this, false);
        }
        Regex validateEmailRegex = new Regex("^\\S+@\\S+\\.\\S+$");
        FirebaseClient client = new FirebaseClient("https://bulletin-app-1644c-default-rtdb.europe-west1.firebasedatabase.app/Users");
        private async void LogButtonClick(object sender, EventArgs e)
        {
            string email = emailField.Text;
            string password = passField.Text;
            var firebase = DependencyService.Get<IFirebaseAuthentificator>();
            try
            {
                var token = await firebase.SignWithEmailAndPasswordAsync(email.ToLower(), password);
                var isVerified = await firebase.IsCurrentUserEmailVerified();  
                if (!isVerified)
                {
                    await firebase.SendEmailVerificationAsync();
                    await DisplayAlert("Подтверждение email", $"Ваш e-mail не верифицирован. Письмо для верификации отправлено на {email}", "Хорошо");
                    return;
                }
                logButton.Text = "УСПЕХ";
                SaveCredentials(email);
                var menuPlusWorkPage = new MyMasterDetailPage(new MenuPage(), new WorkPage());
                NavigationPage.SetHasBackButton(menuPlusWorkPage, false);
                NavigationPage.SetHasNavigationBar(menuPlusWorkPage, false);
                await Navigation.PushAsync(menuPlusWorkPage);
            }
            catch
            {
                await DisplayAlert("Авторизация", "Пользователь с такими данными не был зарегистрирован", "Зарегистрироваться");
                await Navigation.PushAsync(new RegistrationPage());
            }
        }
        private async void RegButtonClicked(object sender, EventArgs e)
        {
            var newReg = new RegistrationPage();
            
            await Navigation.PushAsync(newReg);
        }
        private async void ResetPasswordClicked(object sender, EventArgs e)
        {
            var email = emailField.Text;
            var password = passField.Text;
            if (string.IsNullOrEmpty(email))
            {
                //errText.Text = "E-mail не указан";
                emailField.PlaceholderColor = Color.Red;
            }
            else if (!validateEmailRegex.IsMatch(email))
            {
                //errText.Text = "E-mail должен быть указан в формате 'name@mail.com'";
                await DisplayAlert("E-mail", "E-mail должен быть указан в формате 'name@mail.com'", "Хорошо");
            }
            var firebase = DependencyService.Get<IFirebaseAuthentificator>();
            try
            {
                var token = await firebase.SignWithEmailAndPasswordAsync(email, password);
                var isVerified = await firebase.IsCurrentUserEmailVerified();

                if (!isVerified)
                {
                    await firebase.SendEmailVerificationAsync();
                    await DisplayAlert("Подтверждение email", $"Ваш e-mail не верифицирован. Письмо для верификации отправлено на {email}", "Хорошо");
                    return;
                }
                logButton.Text = "УСПЕХ";
                logButton.TextColor = Color.FromHex("#5E17EB");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Сброс пароля", "Аккаунта с таким e-mail не существует", "OK");
            }
        }
        public void SaveCredentials(string email)
        {
            Preferences.Set("auth", true);
            LoadUsersData(email);
            
        }
        public async void LoadUsersData(string email)
        {
            try
            {
                FirebaseClient firebaseClient = new FirebaseClient("https://bulletin-app-1644c-default-rtdb.europe-west1.firebasedatabase.app/");
                // Получаем все записи о событиях из базы данных
                var users = await firebaseClient
                    .Child("Users")
                    .Child("Users")
                    .Child(email.ToLower().Replace(".","-"))
                    .Child(email.ToLower().Replace(".", "-")+"2")
                    .OnceAsync<User>();
                User user = users.First().Object;
                Preferences.Set("email", email);
                Preferences.Set("username", user.Name);
                Preferences.Set("UUID", user.UUID);
                Preferences.Set("UPID", user.UPID);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ошибка", ex.Message, "OK");
            }
        }
    }
}
