using System;
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

            }
        }
    }
}
