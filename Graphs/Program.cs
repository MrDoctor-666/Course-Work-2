using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    class Program
    {
        static void Main(string[] args)
        {
            string flag = "1";
            while (flag != "0")
            {

                Console.WriteLine("Insert file name: ");
                string fileName = Console.ReadLine();
                //fileName = "graph.txt";

                Console.WriteLine("Will be searched in " + Directory.GetCurrentDirectory());
                Graph smth;
                try
                {
                    smth = new Graph(fileName);
                    ConsolePrinter cPrint = new ConsolePrinter();
                    cPrint.PrintTree(smth);
                    Console.WriteLine("All spaning trees: " + smth.OstovNum());
                    Console.WriteLine("Insert file name to print results");
                    fileName = Console.ReadLine();
                    FilePrinter fPrint = new FilePrinter(fileName);
                    fPrint.PrintGraph(smth);
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }

                Console.WriteLine("Do you want to continue? Exit - 0");
                flag = Console.ReadLine();
            }
        }
    }
}
