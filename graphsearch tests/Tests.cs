using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using graphsearch;
using System.IO;
using System.Collections.Generic;
namespace graphsearch_tests
{
    [TestClass]
    public class Tests
    {
        /// <summary>
        /// Returns the result of attempting to run the given Method extracted from main method in program.cs, modified in order to give return values that can be fully tested
        /// </summary>
        /// <param name="args">The console arguments given</param>
        /// <returns></returns>
        private string doRun(string[] args)
        {
            GetInformation parseData = new GetInformation();
            StringWriter failure= new StringWriter();//ensures any console outputs are caught and can be checked by the assertion
            Console.SetOut(failure);
            using (failure)
            {
                parseData.GrabData(args, out string startNode, out string endNode, out string fileToRead, out string fileToWrite, out bool outputToConsole, out bool debugInfo, out GetInformation.sortingAlgorithm chosenAlgorithm, out bool failedBuild);
                if (parseData.CanRun(failedBuild, startNode, endNode, fileToRead, chosenAlgorithm))
                {
                    if (parseData.ParseFile(fileToRead, out List<Node> nodes, out int[,] adjacencyMatrix))
                    {
                        if (parseData.SetStartNodeAndEndNode(startNode, endNode, nodes, out Node trueStartNode, out Node trueEndNode))
                        {
                            switch (chosenAlgorithm)
                            {
                                case GetInformation.sortingAlgorithm.AStar:
                                    AStar runAStar = new AStar();
                                    return runAStar.Run(nodes, trueStartNode, trueEndNode, adjacencyMatrix);
                                case GetInformation.sortingAlgorithm.Dijkstra:
                                    Dijkstras runDijkstra = new Dijkstras();
                                    return runDijkstra.Run(nodes, trueStartNode, trueEndNode, adjacencyMatrix);
                                case GetInformation.sortingAlgorithm.BF:
                                    BruteForce runBruteForce = new BruteForce();
                                    return runBruteForce.Run(nodes, trueStartNode, trueEndNode, adjacencyMatrix);
                            }
                        }
                    }
                }
            }
            return failure.ToString().Replace(System.Environment.NewLine,"");//removes the newline terminators from the console output allowing them to be checked correctly
        }

        [TestMethod]
        public void InvalidStartNode()
        {
            string[] args = { "-s", ";", "-e", "L", "-f", "test_data_01.txt", "-a", "ASTAR" };
            string expected = "The start node and end nodes given do not match any given in the file!";
            string actual = doRun(args);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void InvalidEndNode()
        {
            string[] args = { "-s", "A", "-e", ";", "-f", "test_data_01.txt", "-a", "ASTAR" };
            string expected = "The start node and end nodes given do not match any given in the file!";
            string actual = doRun(args);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void InvalidFile()
        {
            string[] args = { "-s", "A", "-e", "L", "-f", ";", "-a", "ASTAR" };
            string expected = "The requested file does not exist!";
            string actual = doRun(args);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void InvalidSort()
        {
            string[] args = { "-s", "A", "-e", "L", "-f", "test_data_01.txt", "-a", ";" };
            string expected = "An invalid sort option was entered!";
            string actual = doRun(args);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void usingBothOFlags()
        {
            string[] args = { "-s", "A", "-e", "L", "-f", "test_data_01.txt", "-a", "ASTAR","-o", "test.txt","-O","test.txt"};
            string expected = "-o or -O flag cannot be used more than once!";
            string actual = doRun(args);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void multipleUseOfSFlag()
        {
            string[] args = { "-s", "A", "-e", "L", "-f", "test_data_01.txt", "-a", "ASTAR", "-o", "test.txt", "-s", "B" };
            string expected = "-s flag cannot be used more than once!";
            string actual = doRun(args);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void multipleUseOfEFlag()
        {
            string[] args = { "-s", "A", "-e", "L", "-f", "test_data_01.txt", "-a", "ASTAR", "-o", "test.txt", "-e", "B" };
            string expected = "-e flag cannot be used more than once!";
            string actual = doRun(args);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void multipleUseOfFFlag()
        {
            string[] args = { "-s", "A", "-e", "L", "-f", "test_data_01.txt", "-a", "ASTAR", "-o", "test.txt", "-f", "test_data_02.txt" };
            string expected = "-f flag cannot be used more than once!";
            string actual = doRun(args);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void multipleUseOfAFlag()
        {
            string[] args = { "-s", "A", "-e", "L", "-f", "test_data_01.txt", "-a", "ASTAR", "-o", "test.txt", "-a", "BF" };
            string expected = "-a flag cannot be used more than once!";
            string actual = doRun(args);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void multipleUseOfDFlag()
        {
            string[] args = { "-s", "A", "-e", "L", "-f", "test_data_01.txt", "-a", "ASTAR", "-o", "test.txt", "-d", "-d" };
            string expected = "You cannot call the -d flag more than once!";
            string actual = doRun(args);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void BasicAStarTest()//tests a basic run through of the A star system
        {
            string[] args = { "-s", "A", "-e", "L", "-f", "test_data_01.txt", "-a", "ASTAR" };
            string expected = "A-C-G-K-L 24";
            string actual = doRun(args);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void BasicDijkstraTest()//tests a basic run through of the Dijkstra system
        {
            string[] args = { "-s", "A", "-e", "L", "-f", "test_data_01.txt", "-a", "DIJKSTRA" };
            string expected = "A-B-F-I-L 19";
            string actual = doRun(args);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void BasicBruteForceTest()//tests a basic run through of the Dijkstra system
        {
            string[] args = { "-s", "A", "-e", "L", "-f", "test_data_01.txt", "-a", "BF" };
            string expected = "A-B-F-I-L 19";
            string actual = doRun(args);
            Assert.AreEqual(expected, actual);
        }
    }
}
