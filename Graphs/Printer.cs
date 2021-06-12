using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    interface IPrinter
    {
        void PrintGraph(Graph graph);
        void PrintMatrix(Matrix matrix);
    }
    class ConsolePrinter : IPrinter
    {
        public void PrintGraph(Graph graph)
        {
            //print inital graph on console
            Console.WriteLine("Printing graph");
            foreach (Edge edge in graph.Edges())
                Console.WriteLine(edge.vertex1.ToString() + " " + edge.vertex2.ToString() + " " + edge.weight.ToString());
        }

        public void PrintTree(Graph graph)
        {
            //print tree in console
            Console.WriteLine("Minimum tree information");
            //minimum tree weight
            PrintGraph(graph.OstovTree());
            Console.WriteLine("Minimum tree weight is: " + graph.treeWeight);
        }

        public void PrintMatrix(Matrix mToPrint)
        {
            Console.WriteLine();
            int n = (int)Math.Sqrt(mToPrint.matrix.Length);
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                    Console.Write(mToPrint.matrix[i, j].ToString() + " ");
                Console.WriteLine();
            }
        }
    }
    class FilePrinter : IPrinter
    {
        string wpath;
        public FilePrinter(string fileName)
        {
            wpath = Directory.GetCurrentDirectory() + "\\" + fileName;
            
        }
        public void PrintGraph(Graph graph)
        {
            PrintMatrix(graph.GetKirchhoff());
            using (StreamWriter sw = new StreamWriter(wpath, true, System.Text.Encoding.Default))
            {
                //printing matrix + all spanning trees num
                sw.WriteLine("All spaning trees: " + graph.OstovNum());
                sw.WriteLine();
                //printing tree + weight
                sw.WriteLine("Minimum tree information");
                int treeWeight = 0;
                foreach (Edge edge in graph.OstovTree().Edges())
                {
                    sw.WriteLine(edge.vertex1.ToString() + " " + edge.vertex2.ToString() + " " + edge.weight.ToString());
                    treeWeight += edge.weight;
                }
                sw.WriteLine("Minimum tree weight is: " + treeWeight.ToString());
            }
        }

        public void PrintMatrix(Matrix mToPrint)
        {
            int n = (int)Math.Sqrt(mToPrint.matrix.Length);
            using (StreamWriter sw = new StreamWriter(wpath, true, System.Text.Encoding.Default))
            {
                sw.WriteLine("Kirchhoff matrix");
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                        sw.Write(mToPrint.matrix[i, j].ToString() + " ");
                    sw.WriteLine();
                }
            }
        }
    }
}
