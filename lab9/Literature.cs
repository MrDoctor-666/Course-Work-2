using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab9
{
    abstract class Literature
    {
        private static int count = 0;
        int id;
        protected string author; //can be publisher in case of journals
        protected string name;
        protected int pages;
        //how much is in stock
        public int Amount { get; set; }
        //how much is given to students
        public int AmountGiven { get; set; }
        public int Id
        {
            get { return id; }
        }
        protected Literature(int amount, string author, string name, int pages)
        {
            this.id = count;
            Amount = amount;
            this.author = author;
            this.name = name;
            this.pages = pages;
            AmountGiven = 0;
            count++;
        }
        public abstract void PrintInfo();
    }

    class Book : Literature
    {
        public Book(int amount, string author, string name, int pages) : base(amount, author, name, pages) { }
        public override void PrintInfo()
        {
            string type = this.GetType().ToString();
            Console.WriteLine("A " + type.Substring(type.IndexOf(".")+1) + " with id = " + Id);
            Console.WriteLine("Name - " + this.name + " by " + this.author + ", " + this.pages + " pages");
        }
    }
    class Journal : Literature
    {
        int volume;
        public Journal(int amount, string author, string name, int pages, int volume) : base(amount, author, name, pages) { this.volume = volume; }
        public override void PrintInfo()
        {
            string type = this.GetType().ToString();
            Console.WriteLine("A " + type.Substring(type.IndexOf(".") + 1) + " with id = " + Id);
            Console.WriteLine("Name - " + this.name + " volume #" + this.volume + " by " + this.author + ", " + this.pages + " pages");
        }
    }
    class Paper : Literature
    {
        int volume;
        int year;
        public Paper(int amount, string author, string name, int pages, int volume, int year) : base(amount, author, name, pages) { this.volume = volume; this.year = year; }
        public override void PrintInfo()
        {
            string type = this.GetType().ToString();
            Console.WriteLine("A " + type.Substring(type.IndexOf(".") + 1) + " with id = " + Id);
            Console.WriteLine("Name - " + this.name + " volume #" + this.volume + " by " + this.author + ", " + this.pages + " pages");
        }
    }
}
