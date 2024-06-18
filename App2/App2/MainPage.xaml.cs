using Android.Content;
using Android.Preferences;
using App2.Droid;
using Plugin.Settings;
using System;
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
        private async void LogButtonClick(object sender, EventArgs e)
        {
            string email = emailField.Text;
            string password = passField.Text;
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
            else if (string.IsNullOrEmpty(password))
            {
                passField.PlaceholderColor = Color.Red;
            }
            else if (password.Length < 8)
            {
                await DisplayAlert("Пароль", "Для сохранения безопасности Ваших данных пароль не может быть короче, чем 8 символов", "Хорошо");
            }
            else if (!(password.Contains("!") || password.Contains("*") || password.Contains("#")))
            {
                await DisplayAlert("Пароль", "Для сохранения безопасности Ваших данных пароль хотя бы один символ из следующего набора: !, *, #", "Хорошо");
            }
            else
            {
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
                    SaveCredentials(email, password);
                    var menuPlusWorkPage = new MyMasterDetailPage(new MenuPage(), new WorkPage());
                    NavigationPage.SetHasBackButton(menuPlusWorkPage, false);
                    NavigationPage.SetHasNavigationBar(menuPlusWorkPage, false);
                    await Navigation.PushAsync(menuPlusWorkPage);
                }
                catch
                {
                    await DisplayAlert("Авторизация", "Пользователь с таким e-mail не был зарегистрирован", "Зарегистрироваться");
                    await Navigation.PushAsync(new RegistrationPage());
                }
            }
        }
        private async void RegButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegistrationPage());
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
        public void SaveCredentials(string username, string password)
        {
            Preferences.Set("auth", true);
        }

    }
}
