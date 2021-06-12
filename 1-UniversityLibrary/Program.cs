using System;

namespace lab9
{
    class Program
    {
        static void Main(string[] args)
        {
            //create library
            Library library = new UniversityLibrary();
            //adding some books
            library.AddNewBook(null, 7, "Devilish", "A. J. Crowley", 666);
            library.AddNewBook(true, 5, "Something", "Someone", 30, 1);
            library.AddNewBook(true, 10, "Meteora", "Bubble", 100, 1);
            library.AddNewBook(false, 1, "Commersant", "Someone1 and Someone2", 15, 3, 2000);
            library.PrintAllInfo();
            //giving out books (if book doesn't exist, nothing happens)
            Console.WriteLine();
            Interface(library);
        }

        public static void Interface(Library library)
        {
            Console.WriteLine("Do you want to get or return book? Get - 0, return - 1");
            string read = Console.ReadLine();
            if (read == "0")
            {
                Console.WriteLine("Enter book ID");
                int id = Int32.Parse(Console.ReadLine());
                library.GiveBook(id);
            }
            else if (read == "1")
            {
                Console.WriteLine("Enter book ID");
                int id = Int32.Parse(Console.ReadLine());
                library.ReturnBook(id);
            }
            else Console.WriteLine("Wrong number");

            if (Continue()) Interface(library);
        }

        public static bool Continue()
        {
            Console.WriteLine("Do you to continue? No - 0, Yes - 1");
            string read = Console.ReadLine();
            if (read == "0")
            {
                return false;
            }
            else if (read == "1")
            {
                return true;
            }
            else { Console.WriteLine("Wrong number"); return Continue(); }
        }
    }
}
