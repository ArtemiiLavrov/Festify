using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HelpPage : ContentPage
    {
        public HelpPage()
        {
            InitializeComponent();
        }
        //private async void OpenFileButtonClicked(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        var assembly = Assembly.GetExecutingAssembly();

        //        Stream stream = assembly.GetManifestResourceStream("userguide.pdf");


        //        var pdfViewer = new PdfDocumentView();    
        //        await Navigation.PushAsync(new ContentPage { Content = pdfViewer });
        //    }
        //    catch (Exception ex)
        //    {
        //        await DisplayAlert("Error", ex.Message, "OK");
        //    }
        //}


    }
}