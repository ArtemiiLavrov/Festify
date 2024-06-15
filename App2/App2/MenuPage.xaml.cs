using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App2
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : ContentPage
    {
        public MenuPage()
        {
            InitializeComponent();
            Title = "Menu";
            //try
            //{
            //    //profileIconImage.Source = ImageSource.FromResource("App2.Images.profileIcon.png");
            //}
            //catch (Exception ex)
            //{
            //}
        }
    }
   
}