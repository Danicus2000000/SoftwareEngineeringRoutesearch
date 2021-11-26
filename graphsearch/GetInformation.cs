using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
namespace graphsearch
{
    internal class GetInformation
    {
        public enum sortingAlgorithm//enum for sorting algorithm type
        {
            Null,
            Dijkstra,
            AStar,
            BF
        }
        /// <summary>
        /// Gets the information from the console arguments
        /// </summary>
        /// <param name="pArgs">The console arguments</param>
        /// <param name="startNode">The Start Node</param>
        /// <param name="endNode">The End Node</param>
        /// <param name="fileToRead">The File to read from</param>
        /// <param name="fileToWrite">The file to write to</param>
        /// <param name="outputToConsole">Whether the console should be outputted to</param>
        /// <param name="debugInfo">Whether debug infromation should be shown</param>
        /// <param name="chosenAlgorithm">The name of the chosen algorithm</param>
        /// <param name="failedBuild">Whether there has been an error during information collection</param>
        public void GrabData(string[] pArgs, out string startNode, out string endNode, out string fileToRead, out string fileToWrite, out bool outputToConsole, out bool debugInfo, out sortingAlgorithm chosenAlgorithm, out bool failedBuild)
        {
            startNode = null;//intialises variables to default values
            endNode = null;
            fileToRead = null;
            fileToWrite = null;
            outputToConsole = true;
            debugInfo = false;
            chosenAlgorithm = sortingAlgorithm.Null;
            failedBuild = false;
            bool helpAlreadyShown = false; //variable to stop help showing multiple times if -h flag is used more than once in args
            if (pArgs.Length != 0)//if there are any console arguments
            {
                for (int i = 0; i < pArgs.Length; i++)//loop through all console arguments
                {
                    try//catch any errors that may occur
                    {
                        switch (pArgs[i])
                        {
                            case "-s"://if arg is -s set start node
                                startNode = pArgs[i + 1];
                                break;
                            case "-e"://if arg is -e set end node
                                endNode = pArgs[i + 1];
                                break;
                            case "-f"://if arg is -f set file to read
                                fileToRead = pArgs[i + 1];
                                break;
                            case "-o":
                            case "-O":
                                if (pArgs[i] == "-o" || pArgs[i] == "-O")//if -o or -O flag set file output
                                {
                                    fileToWrite = pArgs[i + 1];
                                }
                                if (pArgs[i] == "-o")//if -o flag don't output to console
                                {
                                    outputToConsole = false;
                                }
                                if (pArgs[i] == "-O") //if -O flag output to console
                                {
                                    outputToConsole = true;
                                }
                                break;
                            case "-d":
                                debugInfo = true;//if -d flag enable debug info
                                break;
                            case "-a"://if a flag find sorting algorithm or if inocorrect entered throw an exception
                                if (pArgs[i + 1].ToUpper() == "DIJKSTRA")
                                {
                                    chosenAlgorithm = sortingAlgorithm.Dijkstra;
                                }
                                else if (pArgs[i + 1].ToUpper() == "ASTAR")
                                {
                                    chosenAlgorithm = sortingAlgorithm.AStar;
                                }
                                else if (pArgs[i + 1].ToUpper() == "BF")
                                {
                                    chosenAlgorithm = sortingAlgorithm.BF;
                                }
                                else
                                {
                                    throw new InvalidOperationException("Could not find selected sorting algorithm!");
                                }
                                break;
                            case "-h":
                                if (!helpAlreadyShown)//shows help unless it has already been shown in which case it is ignored
                                {
                                    Console.WriteLine("-s <number or string>: Specify starting node, either as an integer or a string.  If a string, it must be in quotes, such as \"A\" or \"Norwich\".");
                                    Console.WriteLine("-e <number or string>: Specify end node either as an integer or a string.  If a string, it must be in quotes, such as \"Z\" or \"Zetland\".");
                                    Console.WriteLine("-a BF|DIJKSTRA|ASTAR: Use the relevant algorithm (brute force, Dijkstra's or A*)");
                                    Console.WriteLine("-f <filename>: read data from <filename> as the input");
                                    Console.WriteLine("-o <filename>: Write the output to file <filename> only");
                                    Console.WriteLine("-O <filename>: Write the output to both the console and file <filename>");
                                    Console.WriteLine("-d: Output debugging information to the console.");
                                    Console.WriteLine("-h: Shows the help dialog");
                                    helpAlreadyShown = true;
                                }
                                break;
                        }
                    }
                    catch (Exception)//if there is an exception fail the build
                    {
                        failedBuild = true;
                    }
                }

            }
            else
            {
                Console.WriteLine("No arguments were given!");
            }
        }

        /// <summary>
        /// Checks whether the program is okay to run
        /// </summary>
        /// <param name="failedBuild">If the Build Failed</param>
        /// <param name="startNode">The start node</param>
        /// <param name="endNode">The end node</param>
        /// <param name="fileToRead">The file to read</param>
        /// <returns>A bool representing whether the program can begin</returns>
        public bool CanRun(bool failedBuild,string startNode,string endNode,string fileToRead) 
        {
            if (!failedBuild && startNode != null && endNode != null && fileToRead != null) //if the build succeeds check file location given is valid
            {
                if (File.Exists(fileToRead)) 
                {
                    return true;
                }
                else 
                {
                    return false;
                }
            }
            else //else warn that the information given is incorrect or formatted incorrectly
            {
                Console.WriteLine("One or more required arguments where missing or invalid!");
                return false;
            }
        }
    }
}
