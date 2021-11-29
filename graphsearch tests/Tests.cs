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
                string failureMessage = parseData.GrabData(args, out string startNode, out string endNode, out string fileToRead, out string fileToWrite, out bool outputToConsole, out bool debugInfo, out GetInformation.sortingAlgorithm chosenAlgorithm, out bool failedBuild);
                if (failureMessage == "" && parseData.CanRun(failedBuild, startNode, endNode, fileToRead, chosenAlgorithm, failureMessage))
                {
                    if (failureMessage == "" && parseData.ParseFile(fileToRead, out List<Node> nodes, out int[,] adjacencyMatrix))
                    {
                        if (parseData.SetStartNodeAndEndNode(startNode, endNode, nodes, out Node trueStartNode, out Node trueEndNode))
                        {
                            if (failureMessage == "")
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
            string[] args = { "-s", "A", "-e", "L", "-f", ";", "-a", ";" };
            string expected = "One or more required arguments where missing or invalid!";
            string actual = doRun(args);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void BasicAStarTest()//tests a basic run through of the A star system
        {
            string[] args = { "-s", "A", "-e", "L", "-f", "test_data_01.txt", "-a", "ASTAR" };
            string expected = "A-B-F-I-L 19";
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
    }
}
