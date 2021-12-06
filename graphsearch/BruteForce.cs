using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace graphsearch
{
    public class BruteForce
    {
        /// <summary>
        /// Runs Brute force on a list of nodes
        /// </summary>
        /// <param name="nodes">The list of nodes to parse</param>
        /// <param name="startNode">A refrence to the start node in the list</param>
        /// <param name="endNode">A refrence to the end node in the list</param>
        /// <param name="adjacencyMatrix">A matrix describing the relation between nodes</param>
        /// <returns>A string describing the lowest cost path</returns>
        public string Run(List<Node> nodes, Node startNode, Node endNode, int[,] adjacencyMatrix)
        {
            GetInformation infoParse=new GetInformation();
            Node currentNode = startNode;
            List<List<Node>> paths = new List<List<Node>>() { new List<Node>() { currentNode } };
            List<Node> open = new List<Node>() { currentNode };
            List<Node> closed = new List<Node>();
            while (open.Count != 0 && currentNode != endNode)//loop until either the open list is empty or the end node is found
            {
                int iToSearch = nodes.IndexOf(open[0]);
                for (int j = 0; j < adjacencyMatrix.GetLength(1); j++)
                {
                    if (adjacencyMatrix[iToSearch, j] != 0 && !open.Contains(nodes[j]) && !closed.Contains(nodes[j]))
                    {
                        open.Add(nodes[j]);
                        nodes[j].distanceFromStartNode = adjacencyMatrix[iToSearch, j] + currentNode.distanceFromStartNode;
                        nodes[j].previousNode = nodes[iToSearch].name;
                        for (int pathToClone = 0; pathToClone < paths.Count; pathToClone++)
                        {
                            if (paths[pathToClone][paths[pathToClone].Count - 1] == currentNode)
                            {
                                List<Node> clone = new List<Node>();
                                for (int element = 0; element < paths[pathToClone].Count - 1; element++)
                                {
                                    clone.Add(paths[pathToClone][element]);
                                }
                                clone.Add(nodes[j]);
                                paths.Add(clone);
                                break;
                            }
                        }
                        if (currentNode.isStartNode)
                        {
                            paths.Add(new List<Node>() { currentNode, nodes[j] });
                        }
                        else
                        {
                            bool found = false;//if a path is found add the new node on the end of it
                            for (int pathInPaths = 0; pathInPaths < paths.Count - 1; pathInPaths++)
                            {
                                if (paths[pathInPaths][paths[pathInPaths].Count - 1] == currentNode)
                                {
                                    paths[pathInPaths].Add(nodes[j]);
                                    found = true;
                                    break;
                                }
                            }
                            if (!found)//if no path is found create a clone of the previous path FIX
                            {
                                List<Node> pathClone = new List<Node>();
                                foreach (Node node in paths[paths.Count - 1])
                                {
                                    pathClone.Add(node);
                                }
                                paths.Add(pathClone);
                                paths[paths.Count - 1].Remove(paths[paths.Count - 1][paths[paths.Count - 1].Count - 1]);
                                paths[paths.Count - 1].Add(nodes[j]);
                            }
                        }
                    }

                }
                open.Remove(currentNode);
                closed.Add(currentNode);
                if (open.Count != 0)
                {
                    currentNode = open[0];
                }
            }
            List<string> pathToAdd = infoParse.getTakenPath(nodes, startNode, endNode, adjacencyMatrix, out int totalCost);
            return infoParse.BuildPathFromStartToEnd(pathToAdd, totalCost);
        }
    }
}