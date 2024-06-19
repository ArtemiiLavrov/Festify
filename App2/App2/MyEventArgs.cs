using System;
using System.Collections.Generic;
using System.Text;

namespace App2
{
    public delegate void MyHandler(object sender, MyHandlerEventArgs args);
    public class MyHandlerEventArgs : EventArgs
    {
        public Event Ev { get; set; }

        public MyHandlerEventArgs(Event ev)
        {
           Ev = ev;
        }
    }
}
