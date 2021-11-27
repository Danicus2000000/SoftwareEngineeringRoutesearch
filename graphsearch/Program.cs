using System;
using System.Collections.Generic;
using System.IO;
namespace graphsearch
{
    class Program
    {
        /*Notes: The GetInfromation.Grab method will overwrite old flags with the latest ones, so if -s is declared earlier and then again further on it will take the latest mention as input and ignore the ealier one
         * e.g. graphsearch.exe -s "hello" -e "Goodbye" -s "Update" would cause the start node to be 'update'
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
                if(infoGet.ParseFile(fileToRead, out List<Node> nodes))//parses all file data and continues to run if it is parsed correctly
                {
                    if (infoGet.SetStartNodeAndEndNode(startNode, endNode, nodes,out Node trueStartNode,out Node trueEndNode))//sets start and end node properties on the appropriate files and validates start and end node strings
                    { 
                        switch (chosenAlgorithm)//runs the selected algorithm using the node list given in the file
                        {
                            case GetInformation.sortingAlgorithm.BF:
                                BruteForce bruteForce= new BruteForce();
                                bruteForce.Run(nodes,trueStartNode,trueEndNode);
                                break;
                            case GetInformation.sortingAlgorithm.Dijkstra:
                                Dijkstras dijkstras= new Dijkstras();
                                dijkstras.Run(nodes,trueStartNode,trueEndNode);
                                break;
                            case GetInformation.sortingAlgorithm.AStar:
                                AStar aStar = new AStar();
                                aStar.Run(nodes,trueStartNode,trueEndNode);
                                break;
                        }
                    }
                }
            }
        }
    }
}
