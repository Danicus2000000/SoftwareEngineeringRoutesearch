using System;

namespace graphsearch
{
    class Program
    {
        enum sortingAlgorithm
        {
           Null,
           Dijkstra,
           AStar,
           BF
        }
        static void Main(string[] args)
        {
            string startNode = null;
            string endNode = null;
            string fileToRead = null;
            string fileToOutput = null;
            bool outputToConsole = true;
            bool debugInfo = false;
            bool failedBuild = false;
            sortingAlgorithm chosenAlgorithm=sortingAlgorithm.Null;
            if (args.Length != 0) 
            {
                for(int i=0; i < args.Length; i++) 
                {
                    try 
                    {
                        switch (args[i]) 
                        {
                            case "-s":
                                startNode = args[i + 1];
                                break;
                            case "-e":
                                endNode = args[i + 1];
                                break;
                            case "-f":
                                fileToRead = args[i + 1];
                                break;
                            case "-o":
                            case "-O":
                                if (args[i] == "-o" || args[i]=="-O")//if -o or -O flag set file output
                                {
                                    fileToOutput = args[i + 1];
                                }
                                if (args[i] == "-o")//if -o flag don't output to console
                                { 
                                    outputToConsole = false;
                                }
                                if (args[i] == "-O") //if -O flag output to console
                                {
                                    outputToConsole = true;
                                }
                                break;
                            case "-d":
                                debugInfo = true;//if -d flag enable debug info
                                break;
                            case "-a"://if a flag find sorting algorithm or if inocorrect entered throw an exception
                                    if(args[i+1].ToUpper()== "DIJKSTRA") 
                                    {
                                        chosenAlgorithm = sortingAlgorithm.Dijkstra;
                                    }
                                    else if (args[i + 1].ToUpper() == "ASTAR") 
                                    {
                                        chosenAlgorithm = sortingAlgorithm.AStar;
                                    }
                                    else if (args[i + 1].ToUpper() == "BF")
                                    {
                                        chosenAlgorithm = sortingAlgorithm.BF;
                                    }
                                    else 
                                    {
                                        throw new InvalidOperationException("Could not find selected sorting algorithm!");
                                    }
                                break;
                            case "-h":
                                Console.WriteLine("-s <number or string>: Specify starting node, either as an integer or a string.  If a string, it must be in quotes, such as \"A\" or \"Norwich\".");
                                Console.WriteLine("-e <number or string>: Specify end node either as an integer or a string.  If a string, it must be in quotes, such as \"Z\" or \"Zetland\".");
                                Console.WriteLine("-a BF|DIJKSTRA|ASTAR: Use the relevant algorithm (brute force, Dijkstra's or A*)");
                                Console.WriteLine("-f <filename>: read data from <filename> as the input");
                                Console.WriteLine("-o <filename>: Write the output to file <filename> only");
                                Console.WriteLine("-O <filename>: Write the output to both the console and file <filename>");
                                Console.WriteLine("-d: Output debugging information to the console.");
                                Console.WriteLine("-h: Shows the help dialog");
                                break;
                        }
                    }
                    catch (Exception ex) 
                    {
                       Console.WriteLine(ex.Message);
                       failedBuild = true;
                    }
                }
            
            }
            else 
            {
                Console.WriteLine("No arguments were given!");
            }

        }
    }
}
