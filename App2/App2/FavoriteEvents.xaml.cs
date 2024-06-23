using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FavoriteEvents : ContentPage
    {
        public FavoriteEvents()
        {
            InitializeComponent();
            LoadFavoriteEvents();
        }
        public void AddOneNewBulletin(Event newEvent1)
        {
            // Создаем новый горизонтальный StackLayout
            StackLayout horizontalStack = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand // Растягиваем по горизонтали
            };
            // Создаем StackLayout с изображением и текстом
            string date = newEvent1.Day.ToString() + "/" + newEvent1.Month.ToString() + "/" + newEvent1.Year.ToString();
            StackLayout stackLayout1 = CreateStackLayout(newEvent1.Name, date, "App2.Images.logo.jpg");
            stackLayout1.BackgroundColor = Color.FromHex("#4a4b4d");
            var tapGestureRecognizer1 = new TapGestureRecognizer();
            tapGestureRecognizer1.Tapped += (s, e) =>
            {
                Navigation.PushAsync(new EventPage(newEvent1));
            };
            stackLayout1.GestureRecognizers.Add(tapGestureRecognizer1);

            horizontalStack.Children.Add(stackLayout1);
            // Создаем черный StackLayout 
            StackLayout stackLayout2 = new StackLayout();
            stackLayout2.BackgroundColor = Color.Black;
            stackLayout2.WidthRequest = 185;
            horizontalStack.Children.Add(stackLayout2);

            // Добавляем горизонтальный StackLayout в StackLayout "Bulletins"
            Bulletins.Children.Add(horizontalStack);

        }
        public void AddTwoNewBulletins(Event newEvent1, Event newEvent2)
        {
            // Создаем новый горизонтальный StackLayout
            StackLayout horizontalStack = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand // Растягиваем по горизонтали
            };

            // Создаем первый StackLayout с изображением и текстом
            string date1 = newEvent1.Day.ToString() + "/" + newEvent1.Month.ToString() + "/" + newEvent1.Year.ToString();
            StackLayout stackLayout1 = CreateStackLayout(newEvent1.Name, date1, "App2.Images.logo.jpg");
            stackLayout1.BackgroundColor = Color.FromHex("#4a4b4d");
            var tapGestureRecognizer1 = new TapGestureRecognizer();
            tapGestureRecognizer1.Tapped += (s, e) =>
            {
                Navigation.PushAsync(new EventPage(newEvent1));
            };
            stackLayout1.GestureRecognizers.Add(tapGestureRecognizer1);

            horizontalStack.Children.Add(stackLayout1);

            // Создаем второй StackLayout с изображением и текстом
            string date2 = newEvent1.Day.ToString() + "/" + newEvent1.Month.ToString() + "/" + newEvent1.Year.ToString();
            StackLayout stackLayout2 = CreateStackLayout(newEvent1.Name, date2, "App2.Images.logo.jpg");
            stackLayout2.BackgroundColor = Color.FromHex("#4a4b4d");
            var tapGestureRecognizer2 = new TapGestureRecognizer();
            tapGestureRecognizer2.Tapped += (s, e) =>
            {
                Navigation.PushAsync(new EventPage(newEvent2));
            };
            stackLayout2.GestureRecognizers.Add(tapGestureRecognizer2);

            horizontalStack.Children.Add(stackLayout2);

            // Добавляем горизонтальный StackLayout в StackLayout "Bulletins"
            Bulletins.Children.Add(horizontalStack);
        }

        private StackLayout CreateStackLayout(string title, string date, string imagePath) //создание макета объявления с краткой информацией в ленте
        {
            // Создаем StackLayout для изображения и текста
            StackLayout stackLayout = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            // Создаем изображение
            Image image = new Image
            {
                Source = ImageSource.FromResource(imagePath),
                WidthRequest = 185, // Устанавливаем ширину изображения
                HeightRequest = 185, // Устанавливаем высоту изображения
                Aspect = Aspect.AspectFill // Обрезаем изображение под заданные размеры
            };

            // Создаем текстовые метки
            Label titleLabel = new Label
            {
                Text = title,
                TextColor = Color.White,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center
            };

            Label dateLabel = new Label
            {
                Text = date,
                TextColor = Color.White,
                HorizontalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center
            };

            stackLayout.Children.Add(image);
            stackLayout.Children.Add(titleLabel);
            stackLayout.Children.Add(dateLabel);

            return stackLayout;
        }
        public async void LoadFavoriteEvents()
        {
            try
            {
                FirebaseClient firebaseClient = new FirebaseClient("https://bulletin-app-1644c-default-rtdb.europe-west1.firebasedatabase.app/");
                // Получаем все записи о событиях из базы данных
                var events = await firebaseClient
                    .Child("Users").Child("Users").Child(Preferences.Get("UUID", "1")).Child("FavoriteEvents")
                    .OnceAsync<Event>();
                List<Event> allFavoriteEventsList = new List<Event>();
                //выгружаем все объявления
                foreach (var eventSnapshot in events)
                {
                    allFavoriteEventsList.Add(eventSnapshot.Object);
                }
                //создаём лист своих объявлений
                List<Event> myEventsList = new List<Event>();
                foreach (Event eventSnapshot in allFavoriteEventsList)
                {
                        myEventsList.Add(eventSnapshot);
                }
                if (myEventsList.Count == 0)
                {
                    Label nullNotification = new Label() { Text = "У вас пока нет избранных объявлений", TextColor = Color.White, BackgroundColor = Color.Transparent, 
                        VerticalOptions = LayoutOptions.CenterAndExpand, HorizontalOptions = LayoutOptions.CenterAndExpand, FontFamily = "conthrax-sb", FontSize = 13 };
                    Bulletins.Children.Add(nullNotification);
                }
                if (myEventsList.Count % 2 == 0)
                {
                    for (int i = 0; i < myEventsList.Count; i = i + 2)
                    {
                        AddTwoNewBulletins(myEventsList[i], myEventsList[i + 1]); //Выводим записи попарно
                    }
                }
                else
                {
                    for (int i = 0; i < myEventsList.Count - 1; i = i + 2)
                    {
                        AddTwoNewBulletins(myEventsList[i], myEventsList[i + 1]); //Выводим записи попарно
                    }
                    AddOneNewBulletin(myEventsList[myEventsList.Count - 1]);
                }

            }
            catch (Exception ex)
            {
                await DisplayAlert("Ошибка", ex.Message, "OK");
            }
        }


    }
}