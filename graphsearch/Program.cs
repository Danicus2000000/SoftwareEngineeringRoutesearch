using System;

namespace graphsearch
{
    class Program
    {
        static void Main(string[] args)
        {
            string startNode = null;
            string endNode = null;
            string fileToRead = null;
            string fileToOutput = null;
            bool outputToConsole = true;
            bool debugInfo = false;
            if (args.Length != 0) 
            {
                for(int i=0; i < args.Length; i++) 
                {
                    try 
                    {
                        switch (args[i]) 
                        {
                            case "-s":
                            case "-e":
                            case "-f":
                            case "-o":
                            case "-O":
                                try 
                                {
                                    if(args[i+1][0]=='\"' && args[i+1][args[i+1].Length-1]=='\"') //checks if the argument given for a node or filename is valid
                                    {
                                        if (args[i] == "-s")
                                        {
                                            startNode = args[i + 1].Replace("\"","");
                                            Console.WriteLine("Start Node Set!");
                                        }
                                        else if (args[i] == "-e") 
                                        {
                                            endNode = args[i + 1].Replace("\"", "");
                                            Console.WriteLine("End Node Set!");
                                        }
                                        else if (args[i] == "-f") 
                                        {
                                            fileToRead = args[i + 1].Replace("\"", "");
                                            Console.WriteLine("End Node Set!");
                                        }
                                        else if (args[i] == "-o" || args[i]=="-O")
                                        {
                                            fileToOutput = args[i + 1].Replace("\"", "");
                                            Console.WriteLine("End Node Set!");
                                        }
                                        if (args[i] == "-o") 
                                        {
                                            outputToConsole = false;
                                        }
                                    }
                                    else 
                                    {
                                        throw new InvalidOperationException();
                                    }
                                }
                                catch (Exception) 
                                {
                                    Console.WriteLine("A valid starting node was not given or was formatted incorrectly!");
                                }
                                break;
                            case "-d":
                                debugInfo = true;
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
                    catch (Exception) 
                    {
                        Console.WriteLine("An invalid Argument was given!");
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
