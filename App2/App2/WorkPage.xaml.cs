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
            for (int i = 0; i<7; i++)
            {
                AddNewBulletin(new Event(), new Event()) ;
            }
            
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
            StackLayout stackLayout1 = CreateStackLayout(newEvent1.Name, newEvent1.Date.ToString(), "App2.Images.logo.jpg");
            var tapGestureRecognizer1 = new TapGestureRecognizer();
            tapGestureRecognizer1.Tapped += (s, e) =>
            {
                // Обработка нажатия на первый StackLayout
            };
            stackLayout1.GestureRecognizers.Add(tapGestureRecognizer1);

            horizontalStack.Children.Add(stackLayout1);

            // Создаем второй StackLayout с изображением и текстом
            StackLayout stackLayout2 = CreateStackLayout(newEvent2.Name, newEvent2.Date.ToString(), "App2.Images.logo.jpg");
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
                WidthRequest = 100, // Устанавливаем ширину изображения
                HeightRequest = 100, // Устанавливаем высоту изображения
                Aspect = Aspect.AspectFit // Масштабируем изображение под заданные размеры
            };

            // Создаем текстовые метки
            Label titleLabel = new Label
            {
                Text = title,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Center
            };

            Label descriptionLabel = new Label
            {
                Text = description,
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
    }
}