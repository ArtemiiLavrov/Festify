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
    public partial class EventPage : ContentPage
    {
        public EventPage(Event e)
        {
            Content = new StackLayout
            {
                Children =
                {
                    new Label { Text = $"**Event Name:** {e.Name}", FontSize = 18 },
                    new Label { Text = $"**Event Type:** {e.Type}", FontSize = 18 },
                    new Label { Text = $"**Description:** {e.Description}", FontSize = 18 },
                    new Label { Text = $"**Address:** {e.Address}", FontSize = 18 },
                    new Label { Text = $"**Date:** {e.Day}/{e.Month}/{e.Year}", FontSize = 18 },
                    new Label { Text = $"**Duration:** {e.Duration}", FontSize = 18 },
                    new Label { Text = $"**Price:** {e.Price} рублей", FontSize = 18 },
                    new Label { Text = $"**Minimal Age:** {e.MinimalAge}", FontSize = 18 }
                }
            };
            Title = e.Name;
        }
    }
}