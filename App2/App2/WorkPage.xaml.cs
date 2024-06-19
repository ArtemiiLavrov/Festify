using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WorkPage : ContentPage
    {
        public WorkPage()
        {
            InitializeComponent();
            LoadAllEvents();
            
        }
        public void AddNewBulletin(Event newEvent1, Event newEvent2)
        {
            // Создаем новый горизонтальный StackLayout
            StackLayout horizontalStack = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand // Растягиваем по горизонтали
            };

            // Создаем первый StackLayout с изображением и текстом
            StackLayout stackLayout1 = CreateStackLayout(newEvent1.Name, newEvent1.Day.ToString(), "App2.Images.logo.jpg");
            stackLayout1.BackgroundColor = Color.FromHex("#4a4b4d");
            var tapGestureRecognizer1 = new TapGestureRecognizer();
            tapGestureRecognizer1.Tapped += (s, e) =>
            {
               
            };
            stackLayout1.GestureRecognizers.Add(tapGestureRecognizer1);

            horizontalStack.Children.Add(stackLayout1);

            // Создаем второй StackLayout с изображением и текстом
            StackLayout stackLayout2 = CreateStackLayout(newEvent2.Name, newEvent2.Day.ToString(), "App2.Images.logo.jpg");
            stackLayout2.BackgroundColor = Color.FromHex("#4a4b4d");
            var tapGestureRecognizer2 = new TapGestureRecognizer();
            tapGestureRecognizer2.Tapped += EventTapped;
            stackLayout2.GestureRecognizers.Add(tapGestureRecognizer2);

            horizontalStack.Children.Add(stackLayout2);

            // Добавляем горизонтальный StackLayout в StackLayout "Bulletins"
            Bulletins.Children.Add(horizontalStack);
        }

        private StackLayout CreateStackLayout(string title, string description, string imagePath)
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
                HorizontalOptions = LayoutOptions.Center
            };

            Label descriptionLabel = new Label
            {
                Text = description,
                TextColor = Color.White,
                HorizontalOptions = LayoutOptions.Center
            };

            stackLayout.Children.Add(image);
            stackLayout.Children.Add(titleLabel);
            stackLayout.Children.Add(descriptionLabel);

            return stackLayout;
        }
        private async void EventTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EventPage());
        }
        public async void LoadAllEvents()
        {

            try
            {
                FirebaseClient firebaseClient = new FirebaseClient("https://bulletin-app-1644c-default-rtdb.europe-west1.firebasedatabase.app/");
                // Получаем все записи о событиях из базы данных
                var events = await firebaseClient
                    .Child("Events")
                    .OnceAsync<Event>();
                List<Event> eventsList = new List<Event>();
                foreach (var eventSnapshot in events)
                {
                    eventsList.Add(eventSnapshot.Object);
                }
                for (int i = 0; i < eventsList.Count; i = i+2)
                {
                    AddNewBulletin(eventsList[i], eventsList[i + 1]);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ошибка", ex.Message, "OK");
            }
        }
    }
}