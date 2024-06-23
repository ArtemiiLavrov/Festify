using System;
using System.Collections.Generic;
using System.Text;

namespace App2
{
    public class User
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public List<string> FavoriteEventLinks { get; set; }
        public string UUID { get; set; }
        public string UPID { get; set; }
    }
}
