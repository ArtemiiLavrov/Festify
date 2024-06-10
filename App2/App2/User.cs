using System;
using System.Collections.Generic;
using System.Text;

namespace App2
{
    public class User
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public HashSet<Event> Events { get; set; } 
        public string Id { get; set; }
    }
}
