using System;
using System.Collections.Generic;
using System.IO;
namespace graphsearch
{
    class Program
    {
        /*Notes: 
         * The GetInfromation.Grab method will fail if muliple of the same flag type are used i.e. -s "A" -s "D" and an error message will be outputted to the user specifying the flag causing the failure
         * Naming Convention:
         * Any variable uses camel case
         * Any function uses pascal case
         */
        static void Main(string[] args)
        {
            //default values
            GetInformation.GrabData(args, out string startNode, out string endNode, out string fileToRead, out string fileToWrite, out bool outputToConsole, out bool debugInfo, out GetInformation.SortingAlgorithm chosenAlgorithm, out bool failedBuild);//uses grab method to parse console arguments
            if (GetInformation.CanRun(failedBuild, startNode, endNode, fileToRead, chosenAlgorithm)) //checks whether the program can begin
            {
                if (GetInformation.ParseFile(fileToRead, out List<Node> nodes, out int[,] adjacencyMatrix))//parses all file data and continues to run if it is parsed correctly
                {
                    if (GetInformation.SetStartNodeAndEndNode(startNode, endNode, nodes, out Node trueStartNode, out Node trueEndNode))//sets start and end node properties on the appropriate files and validates start and end node strings
                    {
                        string result = "";
                        switch (chosenAlgorithm)//runs the selected algorithm using the node list given in the file
                        {
                            case GetInformation.SortingAlgorithm.BF:
                                result = BruteForce.Run(nodes, trueStartNode, trueEndNode, adjacencyMatrix);
                                break;
                            case GetInformation.SortingAlgorithm.Dijkstra:
                                result = Dijkstras.Run(nodes, trueStartNode, trueEndNode, adjacencyMatrix);
                                break;
                            case GetInformation.SortingAlgorithm.AStar:
                                result = AStar.Run(nodes, trueStartNode, trueEndNode, adjacencyMatrix);
                                break;
                        }
                        if (outputToConsole)
                        {
                            Console.WriteLine(result);
                        }
                        if (fileToWrite != null)
                        {
                            File.WriteAllText(fileToWrite, result);
                            if (debugInfo)
                            {
                                Console.WriteLine("Data written to " + fileToWrite);
                            }
                        }
                    }
                }
            }
        }
    }
}
