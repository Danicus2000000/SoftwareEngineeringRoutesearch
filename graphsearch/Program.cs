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
            string startNode = null;//default values
            string endNode = null;
            string fileToRead = null;
            string fileToWrite = null;
            bool outputToConsole = true;
            bool debugInfo = false;
            bool failedBuild = false;
            GetInformation.sortingAlgorithm chosenAlgorithm=GetInformation.sortingAlgorithm.Null;
            GetInformation infoGet = new GetInformation();
            infoGet.GrabData(args, out startNode, out endNode, out fileToRead, out fileToWrite, out outputToConsole, out debugInfo, out chosenAlgorithm, out failedBuild);//uses grab method to parse console arguments
            if (infoGet.CanRun(failedBuild, startNode, endNode, fileToRead,chosenAlgorithm)) //checks whether the program can begin
            {
                if(infoGet.ParseFile(fileToRead, out List<Node> nodes, out int[,] adjacencyMatrix))//parses all file data and continues to run if it is parsed correctly
                {
                    if (infoGet.SetStartNodeAndEndNode(startNode, endNode, nodes,out Node trueStartNode,out Node trueEndNode))//sets start and end node properties on the appropriate files and validates start and end node strings
                    {
                        string result = "";
                        switch (chosenAlgorithm)//runs the selected algorithm using the node list given in the file
                        {
                            case GetInformation.sortingAlgorithm.BF:
                                BruteForce bruteForce= new BruteForce();
                                result=bruteForce.Run(nodes,trueStartNode,trueEndNode,adjacencyMatrix);
                                break;
                            case GetInformation.sortingAlgorithm.Dijkstra:
                                Dijkstras dijkstras= new Dijkstras();
                                result=dijkstras.Run(nodes,trueStartNode,trueEndNode,adjacencyMatrix);
                                break;
                            case GetInformation.sortingAlgorithm.AStar:
                                AStar aStar = new AStar();
                                result=aStar.Run(nodes,trueStartNode,trueEndNode,adjacencyMatrix);
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
