using App2.Droid;
using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Firebase;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Essentials;
using Firebase.Database.Query;

namespace App2
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationPage : ContentPage
	{
		public RegistrationPage ()
		{
			InitializeComponent();
            regImage.Source = ImageSource.FromResource("App2.Images.main.png");
            //NavigationPage.SetHasBackButton(this, false);
            //NavigationPage.SetHasNavigationBar(this, false);
        }
        FirebaseClient client = new FirebaseClient("https://bulletin-app-1644c-default-rtdb.europe-west1.firebasedatabase.app/Users");
        public async void RegButtonClicked(object sender, EventArgs e)
        {
            Regex validateEmailRegex = new Regex("^\\S+@\\S+\\.\\S+$");
            string email = emailField.Text;
            string password = passField.Text;
            string checkPass = checkField.Text;
            if (string.IsNullOrEmpty(nameField.Text))
            {
                //errText.Text = "Имя не указано";
                nameField.PlaceholderColor = Color.Red;
            }
            if (string.IsNullOrEmpty(email))
            {
                //errText.Text = "E-mail не указан";
                emailField.PlaceholderColor = Color.Red;
            }
            else if (!validateEmailRegex.IsMatch(email))
            {
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
            else if (checkPass != password)
            {
                await DisplayAlert("Пароль", "Пароли не совпадают", "Хорошо");
            }
            else
            {                
                var fireBase = DependencyService.Get<IFirebaseAuthentificator>();
                try
                {
                    var token = await fireBase.CreateUserWithEmailAndPasswordAsync(email.ToLower(), password);
                    var isVerified = await fireBase.IsCurrentUserEmailVerified();
                    if (!isVerified)
                    {
                        await fireBase.SendEmailVerificationAsync();
                        SaveCredentials(nameField.Text);
                        // Создание модели пользователя
                        var userModel = new User()
                        {
                            Email = email,
                            Name = Preferences.Get("username", "user"),
                            Id = Preferences.Get("UUID", "1")
                        };
                        var response = await client.Child($"{userModel.Id}").PostAsync(userModel);
                        await DisplayAlert("Регистрация", $"Письмо для подтверждения e-mail было отправлено на почту {email.ToLower()}. " +
                            $"После верификации отправляйтесь на страницу входа.", "Хорошо");
                    }
                    else
                    {
                        regButton.Text = "УСПЕХ";
                        await Navigation.PushAsync(new MainPage());
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Ошибка", ex.Message, "OK");
                }
            }
        }
        public void SaveCredentials(string username)
        {
            Guid UUID = Guid.NewGuid();
            Preferences.Set("UUID", UUID.ToString());
            Preferences.Set("username", username);
            Preferences.Set("email", emailField.Text);
        }
    }
}