using System;
using System.Collections.Generic;
using System.Text;

namespace App2
{
    public class Event
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public DateTime Date { get; set; }
        public DateTime Duration { get; set; }
        public Money Price { get; set; }
        public int MinimalAge
        {
            get => minimalAge;
            set
            {
                if (value < 0) { throw new Exception("Incorrect age: value was < 0"); }
                else { minimalAge = value; }
            }
        }
        private int minimalAge;
        public List<string> PhotoReferences { get; set; }
        public override bool Equals(object obj)
        {
            return Equals(obj as Event);
        }
        public bool Equals(Event other)
        {
            if (GetHashCode() != other.GetHashCode()) return false;
            if (Name != other.Name || Type != other.Type || Description != other.Description || Address != other.Address || Date != other.Date || 
                Duration != other.Duration || Price != other.Price || MinimalAge != other.MinimalAge) return false;
            else return true;
        }
        public override int GetHashCode()
        {
            return GetHashCode();
        }
    }
}
