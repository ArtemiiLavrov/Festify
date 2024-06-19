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
    public class MyMasterDetailPage : MasterDetailPage
    {
        public MyMasterDetailPage(MenuPage menu, WorkPage work)
        {
            NavigationPage.SetHasNavigationBar(this, false);
            Master = menu; // боковое меню
            var details = new NavigationPage(work);
            details.BarBackgroundColor = (Color)App.Current.Resources["ToolbarColor"];
            Detail = details; // основное содержимое страницы
        }
        public MyMasterDetailPage(MenuPage menu, CreationOfEventPage work)
        {
            NavigationPage.SetHasNavigationBar(this, false);
            Master = menu; // боковое меню
            var details = new NavigationPage(work);
            details.BarBackgroundColor = (Color)App.Current.Resources["ToolbarColor"];
            Detail = details; // основное содержимое страницы
        }
        public MyMasterDetailPage(MenuPage menu, ProfilePage work)
        {
            NavigationPage.SetHasNavigationBar(this, false);
            Master = menu; // боковое меню
            var details = new NavigationPage(work);
            details.BarBackgroundColor = (Color)App.Current.Resources["ToolbarColor"];
            Detail = details; // основное содержимое страницы
        }
        public MyMasterDetailPage(MenuPage menu, MyEventsPage work)
        {
            NavigationPage.SetHasNavigationBar(this, false);
            Master = menu; // боковое меню
            var details = new NavigationPage(work);
            details.BarBackgroundColor = (Color)App.Current.Resources["ToolbarColor"];
            Detail = details; // основное содержимое страницы
        }
    }
}