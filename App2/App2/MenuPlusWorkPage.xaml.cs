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
    public class MyMasterDetailPage : MasterDetailPage
    {
        public MyMasterDetailPage(MenuPage menu, WorkPage work)
        {
            Master = menu; // боковое меню
            Detail = new NavigationPage(work); // основное содержимое страницы
        }
    }
}