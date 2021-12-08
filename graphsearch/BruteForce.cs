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
            while (currentNode != endNode)//loop until either the open list is empty or the end node is found
            {
                int iToSearch = nodes.IndexOf(open[0]);
                for (int j = 0; j < adjacencyMatrix.GetLength(1); j++)
                {
                    if (adjacencyMatrix[iToSearch, j] != 0 && !open.Contains(nodes[j]) && !closed.Contains(nodes[j]))
                    {
                        open.Add(nodes[j]);
                        nodes[j].distanceFromStartNode = adjacencyMatrix[iToSearch, j] + currentNode.distanceFromStartNode;
                        nodes[j].previousNode = nodes[iToSearch].name;
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