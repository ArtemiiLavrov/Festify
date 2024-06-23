using System;
using System.Collections.Generic;
using System.Text;

namespace App2
{
    public class Event
    {
        DateTime date = DateTime.Now;
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public int Day 
        {
            get => day;
            set
            {
                if (value < 1 || value > 31) { throw new Exception("Некорректное значение дня"); }

                else { day = value; }
            }
        }
        public int Month 
        {
            get => month;
            set
            {
                if (value < 1 || value > 12) { throw new Exception("Некорректное значение номера месяца"); }

                else { month = value; }
            }
        }
        public int Year 
        { 
            get => year;
            set
            {
                if (value < date.Year) { throw new Exception("Некорректное значение года: объявление не может быть размещено с прошедшим годом"); }

                else { year = value; }
            }
        }
        public string Duration { get; set; }
        public int Price 
        { 
            get => price;
            set
            {
                if (value < 0) { throw new Exception("Некорректное значение цены: цена не может быть меньше нуля"); }

                else { price = value; }
            }
        }
        public string Id { get; set; }
        public int MinimalAge
        {
            get => minimalAge;
            set
            {
                if (value < 0) { throw new Exception("Некорректное значение минимального возраста: возраст не может быть меньше нуля"); }
                else { minimalAge = value; }
            }
        }
        private int minimalAge;
        private int day;
        private int month;
        private int year;
        private int price;
        //public List<string> PhotoReferences { get; set; }
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var other = (Event)obj;

            return Name == other.Name &&
                Type == other.Type &&
                Description == other.Description &&
                Address == other.Address &&
                Day == other.Day &&
                Month == other.Month &&
                Year == other.Year &&
                Duration == other.Duration &&
                Price == other.Price &&
                Id == other.Id &&
                MinimalAge == other.MinimalAge;
        }
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + Name.GetHashCode();
                hash = hash * 23 + Type.GetHashCode();
                hash = hash * 23 + Description.GetHashCode();
                hash = hash * 23 + Address.GetHashCode();
                hash = hash * 23 + Day.GetHashCode();
                hash = hash * 23 + Month.GetHashCode();
                hash = hash * 23 + Year.GetHashCode();
                hash = hash * 23 + Duration.GetHashCode();
                hash = hash * 23 + Price.GetHashCode();
                hash = hash * 23 + Id.GetHashCode();
                hash = hash * 23 + MinimalAge.GetHashCode();
                return hash;
            }
        }
    }
}
