using System;
using System.Collections.Generic;
using System.Text;

namespace App2
{
    public class Money
    {
        /// <summary>
        /// Рубли
        /// </summary>
        int rub;

        /// <summary>
        /// Копейки
        /// </summary>
        int kop;

        /// <summary>
        /// Свойство для обработки бизнес-правил для величины рублей
        /// </summary>
        public int Rub
        {
            get => rub;
            set
            {
                // Количество рублей не может быть меньше 0
                if (value < 0)
                {
                    throw new Exception("Incorrect value of rub");
                    rub = 0;
                }
                else rub = value;
            }
        }

        /// <summary>
        /// Свойство для обработки бизнес-правил для величины копеек
        /// </summary>
        public int Kop
        {
            get => kop;
            // Доступ на изменение только внутри класса
            private set
            {
                // Количество копеек не может быть меньше 0
                if (value < 0)
                {
                    throw new Exception("Incorrect value of kop");
                    kop = 0;
                }
                // При значении для копеек больше 99
                if (value > 99)
                {
                    rub += value / 100; // Добавляем каждые 100 копеек к рублям
                    kop = value % 100; // Остаток оставляем в копейках
                }
                else kop = value;
            }
        }

        /// <summary>
        /// Конструктор по умолчанию (создаётся объект типа Money с 0 рублей и 0 копеек)
        /// </summary>
        public Money()
        {
            Rub = 0;
            Kop = 0;
        }

        /// <summary>
        /// Конструктор объекта типа Money по заданному количеству рублей и копеек
        /// </summary>
        /// <param name="r">Количество рублей</param>
        /// <param name="k">Количество копеек</param>
        public Money(int r, int k)
        {
            Rub = r;
            Kop = k;
        }
        /// <summary>
        /// Конструктор глубокой копии существующего объекта типа Money
        /// </summary>
        /// <param name="m">Исходный объект типа Money</param>
        public Money(Money m)
        {
            Rub = m.Rub;
            Kop = m.Kop;
        }

        /// <summary>
        /// Вывод информации об объекте в формате "N руб. M коп."
        /// </summary>
        public void Show()
        {
            Console.WriteLine($"{Rub} руб. {Kop} коп.");
        }

        /// <summary>
        /// Добавление копейки к объекту Money.
        /// </summary>
        /// <param name="m">Объект типа Money (операнд)</param>
        /// <returns>Новый объект типа Money с увеличением на 1 копейку</returns>
        public static Money operator ++(Money m)
        {
            Money result = new Money(m);
            result.Kop = result.Kop + 1;
            return result;
        }

        /// <summary>
        /// Получение количества копеек из объекта Money.
        /// </summary>
        /// <param name="m">Объект типа Money (операнд)</param>
        /// <returns>Целое число копеек из рублей и копеек объекта Money</returns>
        public static int operator -(Money m)
        {
            return m.Rub * 100 + m.Kop;
        }

        /// <summary>
        /// Сложение двух объектов типа Money.
        /// </summary>
        /// <param name="m1">Левый операнд: Объект типа Money</param>
        /// <param name="m2">Правый операнд: Объект типа Money (операнд)</param>
        /// <returns>Новый объект Money с суммой по рублям и копейкам</returns>
        public static Money operator +(Money m1, Money m2)
        {                              
            Money sum = new Money();    
            sum.Rub = m1.Rub + m2.Rub;  
            sum.Kop = m1.Kop + m2.Kop;  
            return sum;
        }

        /// <summary>
        /// Бинарная операция добавления к объекту типа Money вещественного числа (количество денег в числовом эквиваленте)
        /// </summary>
        /// <param name="m">Левый операнд: Объект типа Money</param>
        /// <param name="d">Правый операнд: Вещественное число, где целая часть - количество рублей, после запятой - количество копеек
        /// (учитываем до 2 знака после запятой, остальное игнорируем для упрощения)</param>
        /// <returns>Новый объект Money, увеличенный на заданное число</returns>
        public static Money operator +(Money m, double d)
        {
            Money sum = new Money(m);
            //sum.Rub = m.Rub;
            sum.Kop += ((int)(d * 100));
            return sum;
        }

        /// <summary>
        /// Бинарная операция добавления к вещественному числу (количество денег в числовом эквиваленте) объект типа Money
        /// </summary>
        /// <param name="d">Левый операнд: Вещественное число, где целая часть - количество рублей, после запятой - количество копеек
        /// (учитываем до 2 знака после запятой, остальное игнорируем для упрощения)</param>
        /// <param name="m">Правый операнд: Объект типа Money</param>
        /// <returns>Новый объект, увеличенный на заданное число (используется вызов операции от Money и double)</returns>
        public static Money operator +(double d, Money m)
        {
            return m + d;
        }

        /// <summary>
        /// Неявное приведение объекта Money к типу double.
        /// Выполняется возврат числового эквивалента копеек в рублях (число от 0.0 до 0.99)
        /// </summary>
        /// <param name="m">Объект типа Money (операнд)</param>
        public static implicit operator double(Money m)
        {
            return m.Kop / 100.0;
        }

        /// <summary>
        /// Явное приведение объекта Money к типу int.
        /// Выполняется возврат количества рублей в объекте Money.
        /// </summary>
        /// <param name="m">Объект типа Money (операнд)</param>
        public static explicit operator int(Money m)
        {
            return m.Rub;
        }
    }
    public class Event
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string Adress { get; set; }
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
            if (Name != other.Name || Type != other.Type || Description != other.Description || Adress != other.Adress || Date != other.Date || 
                Duration != other.Duration || Price != other.Price || MinimalAge != other.MinimalAge) return false;
            else return true;
        }
        public override int GetHashCode()
        {
            return GetHashCode();
        }
    }
}
