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
            //all code is from program.cs however it has been modified slightly to ensure it can be tested fully
            GetInformation parseData = new GetInformation();
            string result = "";
            StringWriter failure= new StringWriter();//ensures any console outputs are caught and can be checked by the assertion
            Console.SetOut(failure);
            using (failure)//uses stringwritter to catch console outputs each layer gets and error checks the required data
            {
                parseData.GrabData(args, out string startNode, out string endNode, out string fileToRead, out string fileToWrite, out bool outputToConsole, out bool debugInfo, out GetInformation.sortingAlgorithm chosenAlgorithm, out bool failedBuild);
                if (parseData.CanRun(failedBuild, startNode, endNode, fileToRead, chosenAlgorithm))
                {
                    if (parseData.ParseFile(fileToRead, out List<Node> nodes, out int[,] adjacencyMatrix))
                    {
                        if (parseData.SetStartNodeAndEndNode(startNode, endNode, nodes, out Node trueStartNode, out Node trueEndNode))
                        {
                            switch (chosenAlgorithm)//gets sorting algorithm and runs it 
                            {
                                case GetInformation.sortingAlgorithm.AStar:
                                    AStar runAStar = new AStar();
                                    result = runAStar.Run(nodes, trueStartNode, trueEndNode, adjacencyMatrix);
                                    break;
                                case GetInformation.sortingAlgorithm.Dijkstra:
                                    Dijkstras runDijkstra = new Dijkstras();
                                    result =runDijkstra.Run(nodes, trueStartNode, trueEndNode, adjacencyMatrix);
                                    break;
                                case GetInformation.sortingAlgorithm.BF:
                                    BruteForce runBruteForce = new BruteForce();
                                    result= runBruteForce.Run(nodes, trueStartNode, trueEndNode, adjacencyMatrix);
                                    break;
                            }
                        }
                    }
                }
                if (outputToConsole)//if console output used then output
                {
                    Console.WriteLine(result);
                }
                if (fileToWrite != null)//if the file needs writting to write to it
                {
                    File.WriteAllText(fileToWrite, result);
                }
            }
            if (result == "")//return failure message if there is no result otherwise return result
            {
                return failure.ToString().Replace(System.Environment.NewLine, "");//removes the newline terminators from the console output allowing them to be checked correctly
            }
            else 
            {
                return result;
            }
        }

        [TestMethod]
        public void InvalidStartNode()//tests that an invalid start node selection is handled
        {
            string[] args = { "-s", ";", "-e", "L", "-f", "test_data_01.txt", "-a", "ASTAR" };
            string expected = "The start node or end node does not match any given in the file!";
            string actual = doRun(args);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void InvalidEndNode()//tests that an invalid end node selection is handled
        {
            string[] args = { "-s", "A", "-e", ";", "-f", "test_data_01.txt", "-a", "ASTAR" };
            string expected = "The start node or end node does not match any given in the file!";
            string actual = doRun(args);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void InvalidFile()//tests that an invalid file selection is handled
        {
            string[] args = { "-s", "A", "-e", "L", "-f", ";", "-a", "ASTAR" };
            string expected = "The requested file does not exist!";
            string actual = doRun(args);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void InvalidSort() //tests that an invalid sort selection is handled
        {
            string[] args = { "-s", "A", "-e", "L", "-f", "test_data_01.txt", "-a", ";" };
            string expected = "An invalid sort option was entered!";
            string actual = doRun(args);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void usingBothOFlags()//tests that using both the -o and -O flag is handled
        {
            string[] args = { "-s", "A", "-e", "L", "-f", "test_data_01.txt", "-a", "ASTAR","-o", "test.txt","-O","test.txt"};
            string expected = "-o or -O flag cannot be used more than once!";
            string actual = doRun(args);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void multipleUseOfSFlag()//tests that multiple uses of the -s flag is handled
        {
            string[] args = { "-s", "A", "-e", "L", "-f", "test_data_01.txt", "-a", "ASTAR", "-o", "test.txt", "-s", "B" };
            string expected = "-s flag cannot be used more than once!";
            string actual = doRun(args);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void multipleUseOfEFlag()//tests that multiple uses of the -e flag is handled
        {
            string[] args = { "-s", "A", "-e", "L", "-f", "test_data_01.txt", "-a", "ASTAR", "-o", "test.txt", "-e", "B" };
            string expected = "-e flag cannot be used more than once!";
            string actual = doRun(args);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void multipleUseOfFFlag()//tests that multiple uses of the -f flag is handled
        {
            string[] args = { "-s", "A", "-e", "L", "-f", "test_data_01.txt", "-a", "ASTAR", "-o", "test.txt", "-f", "test_data_02.txt" };
            string expected = "-f flag cannot be used more than once!";
            string actual = doRun(args);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void multipleUseOfAFlag()//tests that multiple uses of the -a flag is handled
        {
            string[] args = { "-s", "A", "-e", "L", "-f", "test_data_01.txt", "-a", "ASTAR", "-o", "test.txt", "-a", "BF" };
            string expected = "-a flag cannot be used more than once!";
            string actual = doRun(args);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void multipleUseOfDFlag()//tests that multiple uses of the -d flag is handled
        {
            string[] args = { "-s", "A", "-e", "L", "-f", "test_data_01.txt", "-a", "ASTAR", "-d", "-d" };
            string expected = "You cannot call the -d flag more than once!";
            string actual = doRun(args);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void multipleUseOfHFlag()//tests that multiple uses of the -h flag is handled
        {
            string[] args = { "-s", "A", "-e", "L", "-f", "test_data_01.txt", "-a", "ASTAR", "-h", "-h" };
            string expected = "-s <number or string>: Specify starting node, either as an integer or a string.  If a string, it must be in quotes, such as \"A\" or \"Norwich\".-e <number or string>: Specify end node either as an integer or a string.  If a string, it must be in quotes, such as \"Z\" or \"Zetland\".-a BF|DIJKSTRA|ASTAR: Use the relevant algorithm (brute force, Dijkstra's or A*)-f <filename>: read data from <filename> as the input-o <filename>: Write the output to file <filename> only-O <filename>: Write the output to both the console and file <filename>-d: Output debugging information to the console.-h: Shows the help dialogYou cannot use the -h flag more than once";
            string actual = doRun(args);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CheckFileCreated()//tests a file is created when requested
        {
            string[] args = { "-s", "A", "-e", "L", "-f", "test_data_01.txt", "-a", "BF", "-o", "ashjdsjahdjhsahdsjahdjshaj.txt" };
            bool expected = true;
            doRun(args);
            bool actual = File.Exists("ashjdsjahdjhsahdsjahdjshaj.txt");
            if (actual)
            {
                File.Delete("ashjdsjahdjhsahdsjahdjshaj.txt");
            }
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException (typeof(AssertFailedException))]
        public void FileWeightingErrorAStarTest()//tests a basic run through of the A star system with the incorrect weighted file we expect this to fail due to weighting errors
        {
            string[] args = { "-s", "A", "-e", "L", "-f", "test_data_01.txt", "-a", "ASTAR" };
            string expected = "A-B-F-I-L 19";
            string actual = doRun(args);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void BasicAStarTest()///tests a basic run through of the A* System
        {
            string[] args = { "-s", "A", "-e", "L", "-f", "test_data_01a.txt", "-a", "ASTAR" };
            string expected = "A-B-F-I-L 116";
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
        public void BasicBruteForceTest()//tests a basic run through of the Brute Force system
        {
            string[] args = { "-s", "A", "-e", "L", "-f", "test_data_01.txt", "-a", "BF" };
            string expected = "A-B-F-I-L 19";
            string actual = doRun(args);
            Assert.AreEqual(expected, actual);
        }
    }
}
