using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab9
{
    interface Library
    {
        void AddNewBook(bool? type, int amount, string name, string author, int pages, int volume = 0, int year = 0);
        void PrintAllInfo();
        Literature GiveBook(int id);
        void ReturnBook(int id);

    }
    class UniversityLibrary : Library
    {
        List<Literature> inStock = new List<Literature>(); //что есть в библиотеке сейчас
        List<Literature> given = new List<Literature>(); //что выдано
        public void AddNewBook(bool? type, int amount, string name, string author, int pages, int volume = 0, int year = 0)
        {
            //new book in the library
            //в библиотеку завезли новую книгу
            //null - book, true - journal, false - paper
            Literature lit;
            if (type == null) lit = new Book(amount, author, name, pages);
            else if (type == true) lit = new Journal(amount, author, name, pages, volume);
            else lit = new Paper(amount, author, name, pages, volume, year);
            inStock.Add(lit);
        }
        public void PrintAllInfo()
        {
            Console.WriteLine("Literature that is available now: ");
            foreach (Literature lit in inStock)
                lit.PrintInfo();
        }
        public Literature GiveBook(int id)
        {
            //someone takes book from library
            //кто-то берет книгу из библиотеки
            foreach (Literature lit in inStock)
                if (lit.Id == id) {
                    Console.WriteLine("Givinig: "); lit.PrintInfo();
                    if (lit.Amount == 1) inStock.Remove(lit);
                    lit.Amount--;
                    if (lit.AmountGiven == 0) given.Add(lit);
                    lit.AmountGiven++;
                    Console.WriteLine("Available: " + lit.Amount);
                    return lit; 
                }
            Console.WriteLine("Literature with id " + id.ToString() + " doesn't exist or isn't available now");
            return null;
        }

        public void ReturnBook(int id)
        {
            //someone returns book to the library
            //кто-то возвращает книгу в библиотеку
            foreach (Literature lit in given)
                if (lit.Id == id)
                {
                    Console.WriteLine("Returning: "); lit.PrintInfo();
                    if (lit.AmountGiven == 1) given.Remove(lit);
                    lit.AmountGiven--;
                    if (lit.Amount == 0) inStock.Add(lit);
                    lit.Amount++;
                    Console.WriteLine("Available: " + lit.Amount);
                    return;
                }
            Console.WriteLine("Literature with id " + id.ToString() + " doesn't exist or weren't given to anyone");
        }
    }
}
