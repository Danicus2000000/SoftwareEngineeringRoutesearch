using System;

namespace graphsearch
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 0) 
            {
                for(int i=0; i < args.Length; i++) 
                {
                    try 
                    {
                        switch (args[i]) 
                        {
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
