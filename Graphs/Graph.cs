using System;
using System.IO;
using System.Collections.Generic;

namespace Graphs
{
    public class Graph
    {
        int numberV; //number of vertices
        Matrix kirchhoffMatrix = null;
        List<Edge> edges = new List<Edge>();
        Graph ostovTree; //minimum ostov tree
        public int treeWeight { get; private set; }

        public Graph(string fileName)
        {
            //creating empty graph
            ostovTree = new Graph(0);
            //read from file
            string wpath = Directory.GetCurrentDirectory() + "\\" + fileName;
            if (!File.Exists(wpath)) throw new Exception("File doesn't exist");

            FileStream stream = new FileStream(wpath, FileMode.Open);
            StreamReader reader = new StreamReader(stream);
            //file should start with number of vertices
            bool correct = int.TryParse(reader.ReadLine(), out numberV);
            if (!correct || numberV < 1) throw new Exception("Incorrect number of vertices");

            while (!reader.EndOfStream)
            {
                //each line - new edge
                string newEdge = reader.ReadLine();
                newEdge = newEdge.Trim();
                //contains 3 parametrs splited with a space
                //two vertices and weight
                string[] bits = newEdge.Split(' ');
                int[] edgeParam = new int[3];
                //each parameter is an integer (should be)
                for (int i = 0; i < 3; i++)
                    if (!int.TryParse(bits[i], out edgeParam[i]))
                        throw new Exception("Edge is incorrect");
                //successfully create a new edge
                edges.Add(new Edge(edgeParam[0], edgeParam[1], edgeParam[2]));
            }
            reader.Close();
            //write graph in kirchhoff matrix
            FindKirchhoff();
            //find tree with a minimum weight
            PrimaAlg();

        }

        public int OstovNum()
        {
            //just in case
            if (kirchhoffMatrix == null) FindKirchhoff();
            if (numberV == 0) return 0;
            //find minor of kirchhoff matrix
            Matrix minor = new Matrix(new int[numberV - 1, numberV - 1]);
            kirchhoffMatrix.FindMinor(ref minor, numberV - 1, numberV - 1);
            //find determinant = number of ostov trees
            return (int)minor.Determinant();

        }

        public Graph OstovTree() { return ostovTree; } //to test
        public  Matrix GetKirchhoff() { return kirchhoffMatrix; }
        public List<Edge> Edges() { return edges; }
        
        
        void FindKirchhoff()
        {
            int[,] kMatrix = new int[numberV, numberV];
            foreach (Edge edge in edges)
            {
                //on a diaganol a number of edges that go out from vertex with this number
                kMatrix[edge.vertex1, edge.vertex1]++;
                kMatrix[edge.vertex2, edge.vertex2]++;
                //adjacent vertices are markes -1 in matrix
                //matrix is symmetric
                kMatrix[edge.vertex1, edge.vertex2] = -1;
                kMatrix[edge.vertex2, edge.vertex1] = -1;
            }
            kirchhoffMatrix = new Matrix(kMatrix);
        }
        //to create empty graph
        Graph(int numberV)
        {
            this.numberV = numberV;
        }
        //to be able to change graph, but only inside
        void AddEdge(Edge edge) { edges.Add(edge); }
        //finds minimum tree
        void PrimaAlg()
        {
            treeWeight = 0;
            ostovTree = new Graph(this.numberV);
            //unused edges
            List<Edge> notUsedEdges = new List<Edge>(edges);
            //used vertices
            List<int> usedV = new List<int>();
            usedV.Add(0);
            //unused vertices
            List<int> notUsedV = new List<int>();
            for (int i = 1; i < numberV; i++)
                notUsedV.Add(i);

            while (notUsedV.Count > 0)
            {
                int minE = -1; //number of a smallest edge
                //search for edge with min eright
                for (int i = 0; i < notUsedEdges.Count; i++)
                {
                    if (((usedV.IndexOf(notUsedEdges[i].vertex1) != -1) && (notUsedV.IndexOf(notUsedEdges[i].vertex2) != -1)) ||
                        ((usedV.IndexOf(notUsedEdges[i].vertex2) != -1) && (notUsedV.IndexOf(notUsedEdges[i].vertex1) != -1)))
                    {
                        if (minE != -1)
                        {
                            if (notUsedEdges[i].weight < notUsedEdges[minE].weight)
                                minE = i;
                        }
                        else
                            minE = i;
                    }
                }
                Edge minEdge = notUsedEdges[minE];
                //put new vertex in unsed and delete from unused
                if (usedV.IndexOf(minEdge.vertex1) != -1)
                {
                    usedV.Add(minEdge.vertex2);
                    notUsedV.Remove(minEdge.vertex2);
                }
                else
                {
                    usedV.Add(minEdge.vertex1);
                    notUsedV.Remove(minEdge.vertex1);
                }
                //put neew edge in tree and delete from unused
                treeWeight += minEdge.weight;
                ostovTree.AddEdge(minEdge);
                notUsedEdges.RemoveAt(minE);
            }
        }
    }
}
