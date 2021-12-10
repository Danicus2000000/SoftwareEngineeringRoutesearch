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
            GetInformation infoParse = new GetInformation();
            List<Node> openNodes = new List<Node>();
            nodes[nodes.IndexOf(startNode)].totalDistance = 0;//sets the distance of the start node to zero so it will be picked 
            foreach (Node node in nodes)//populates the unvisited node list
            {
                openNodes.Add(node);
            }
            while (openNodes.Count != 0)//While unvisited nodes remain
            {
                Node currentNode = infoParse.GetCheapestNode(openNodes);//Takes an unvisited noed with the smallest distance 
                nodes[nodes.IndexOf(currentNode)].totalDistance = currentNode.totalDistance;
                int AdjacentRowToSearch = nodes.IndexOf(currentNode);//gets the index for the adjecent row
                for (int j = 0; j < adjacencyMatrix.GetLength(1); j++)
                {
                    if (adjacencyMatrix[AdjacentRowToSearch, j] != 0)//checks if there is a weights and if the node has not been visited
                    {
                        double cost = adjacencyMatrix[AdjacentRowToSearch, j] + currentNode.totalDistance;//calculates cost for this route
                        if (cost < nodes[j].totalDistance) //if the new total is a cheaper route
                        {
                            nodes[j].totalDistance = cost;//update the route cost and previous path to this node
                            nodes[j].previousNode = currentNode.name;
                        }
                    }
                }
                openNodes.Remove(currentNode);//removes the node from visited nodes
            }
            List<string> pathToAdd = infoParse.GetTakenPath(nodes, startNode, endNode, adjacencyMatrix, out int totalCost);//converts the node list to a list contraining the true path
            return infoParse.BuildPathFromStartToEnd(pathToAdd, totalCost);//builds the path into the required string format
        }
    }
}