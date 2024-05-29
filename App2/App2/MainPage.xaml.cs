using Firebase.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App2
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            regImage.Source = ImageSource.FromResource("App2.Images.regimage.png");
        }
        private async void LogButtonClick(object sender, EventArgs e)
        {
            Regex validateEmailRegex = new Regex("^\\S+@\\S+\\.\\S+$");
            if (string.IsNullOrEmpty(nameField.Text))
            {
                //errText.Text = "Имя не указано";
                nameField.PlaceholderColor = Color.Red;
            }
            else if (string.IsNullOrEmpty(emailField.Text))
            {
                //errText.Text = "E-mail не указан";
                emailField.PlaceholderColor = Color.Red;
            }
            else if (!validateEmailRegex.IsMatch(emailField.Text))
            {
                //errText.Text = "E-mail должен быть указан в формате 'name@mail.com'";
                await DisplayAlert("E-mail", "E-mail должен быть указан в формате 'name@mail.com'", "Хорошо");

            }
            else if (string.IsNullOrEmpty(passField.Text))
            {
                passField.PlaceholderColor = Color.Red;
            }
            else if (passField.Text.Length < 8)
            {
                await DisplayAlert("Пароль", "Для сохранения безопасности Ваших данных пароль не может быть короче, чем 8 символов", "Хорошо");
            }
            else if (!(passField.Text.Contains("!") || passField.Text.Contains("*") || passField.Text.Contains("#")))
            {
                await DisplayAlert("Пароль", "Для сохранения безопасности Ваших данных пароль хотя бы один символ из следующего набора: !, *, #", "Хорошо");
            }
            else
            {
                errText.Text = "";
                logButton.Text = "УСПЕХ";
                logButton.TextColor = Color.FromHex("#5E17EB");
                using (var firebase = new FirebaseClient("https://bulletin-app-1644c-default-rtdb.europe-west1.firebasedatabase.app/"))
                {
                    var result = await firebase.Child("test_key").OnceSingleAsync<string>();
                    await DisplayAlert("Регистрация в приложении", result, "Отлично");
                }  
            }
        }
        private async void RegButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegistrationPage());
        }
    }
}
