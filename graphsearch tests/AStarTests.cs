using Microsoft.VisualStudio.TestTools.UnitTesting;
using graphsearch;
using System.Collections.Generic;
namespace graphsearch_tests
{
    [TestClass]
    public class AStarTests
    {
        GetInformation parseData = new GetInformation();
        AStar runAStar = new AStar();

        /// <summary>
        /// Returns the result of attempting to run the AStar Method
        /// </summary>
        /// <param name="args">The console arguments given</param>
        /// <returns></returns>
        public string doRun(string[] args)
        {
            string failureMessage = parseData.GrabData(args, out string startNode, out string endNode, out string fileToRead, out string fileToWrite, out bool outputToConsole, out bool debugInfo, out GetInformation.sortingAlgorithm chosenAlgorithm, out bool failedBuild);
            if (failureMessage=="" && parseData.CanRun(failedBuild, startNode, endNode, fileToRead, chosenAlgorithm, failureMessage,out failureMessage))
            {
                if ( failureMessage=="" && parseData.ParseFile(fileToRead, out List<Node> nodes, out int[,] adjacencyMatrix,out failureMessage))
                {
                    if (parseData.SetStartNodeAndEndNode(startNode, endNode, nodes, out Node trueStartNode, out Node trueEndNode,out failureMessage))
                    {
                        if (failureMessage == "")
                        {
                            return runAStar.Run(nodes, trueStartNode, trueEndNode, adjacencyMatrix);
                        }
                    }
                }
            }
            return failureMessage;
        }
    [TestMethod]
        public void BasicTest()//tests a basic run through of the system
        {
            string[] args = { "-s", "A", "-e", "L", "-f", "test_data_01.txt", "-a", "ASTAR" };
            string expected = "A-C-G-K-L 24";
            string actual = doRun(args);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void InvalidStart()
        {
            string[] args = { "-s", ";", "-e", "L", "-f", "test_data_01.txt", "-a", "ASTAR" };
            string expected = "The start node and end nodes given do not match any given in the file!";
            string actual = doRun(args);
            Assert.AreEqual(expected, actual);
        }
    }
}
