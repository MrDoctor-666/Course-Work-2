using Microsoft.VisualStudio.TestTools.UnitTesting;
using Graphs;
using System;

namespace TestGraph
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void EdgeCreate()
        {
            Edge edge = new Edge(2, 1, 13); //create edge between vertex 1 and 2
            Assert.AreEqual(edge.vertex1, 1); //smaller go first - true
            Assert.AreEqual(edge.vertex2, 2);
            Assert.AreEqual(edge.weight, 13);
        }

        [TestMethod]
        public void MatrixMinorTest()
        {
            int[,] arr = { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
            Matrix testM = new Matrix(arr);
            Matrix minorA33 = new Matrix(2); ; testM.FindMinor(ref minorA33, 2, 2);
            Assert.AreEqual(minorA33.matrix[0, 0], 1);
            Assert.AreEqual(minorA33.matrix[0, 1], 2);
            Assert.AreEqual(minorA33.matrix[1, 0], 4);
            Assert.AreEqual(minorA33.matrix[1, 1], 5);
        }

        [TestMethod]
        public void MatrixDeterminantTest()
        {
            int[,] arr = { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
            Matrix testM = new Matrix(arr);
            Assert.AreEqual(testM.Determinant(), 0);
        }

        [TestMethod]
        public void GraphReadError1()
        {
            Graph testG;
            try
            {
                testG = new Graph("a");
            }
            catch (Exception ex) { Assert.AreEqual(ex.Message, "File doesn't exist"); }
        }

        [TestMethod]
        public void GraphReadError2()
        {
            Graph testG;
            try
            {
                testG = new Graph("graphError.txt");
            }
            catch (Exception ex) { Assert.AreEqual(ex.Message, "Incorrect number of vertices"); }
        }

        [TestMethod]
        public void GraphReadError3()
        {
            Graph testG;
            try
            {
                testG = new Graph("graphError3.txt");
            }
            catch (Exception ex) { Assert.AreEqual(ex.Message, "Edge is incorrect"); }
        }

        [TestMethod]
        public void GraphOstovNum()
        {
            Graph testG;
            testG = new Graph("graph.txt");
            Assert.AreEqual(testG.OstovNum(), 3289);
        }

        [TestMethod]
        public void GraphPrimaAlg()
        {
            Graph testG;
            testG = new Graph("graph.txt");
            Graph testTree = testG.OstovTree();
            Assert.AreEqual(9, testTree.Edges().Count);
        }
    }
}
