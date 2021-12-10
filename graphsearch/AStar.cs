using System;
using System.Collections.Generic;
using System.Text;

namespace graphsearch
{
    public class AStar
    {
        /// <summary>
        /// Runs A* on a list of nodes
        /// </summary>
        /// <param name="nodes">The list of nodes to parse</param>
        /// <param name="startNode">A refrence to the start node in the list</param>
        /// <param name="endNode">A refrence to the end node in the list</param>
        /// <param name="adjacencyMatrix">The Adjacency Matrix of the edges and nodes</param>
        /// <returns>A string describing the lowest cost path</returns>
        public string Run(List<Node> nodes, Node startNode, Node endNode, int[,] adjacencyMatrix)//Note: Due to file 1's weightings being incorrect most paths in file 1 are not found correctly however it works with all others
        {
            GetInformation infoParse = new GetInformation();
            List<Node> openNodes = new List<Node>();
            List<Node> closedNodes = new List<Node>();//creates list to store completed nodes
            startNode.heuristicDistance = GetHeuristicDistance(startNode.xCoord, startNode.yCoord, endNode.xCoord, endNode.yCoord);//sets all nodes heuristic distance from the end node
            foreach (Node node in nodes)
            {
                node.heuristicDistance = GetHeuristicDistance(node.xCoord, node.yCoord, endNode.xCoord, endNode.yCoord);
            }
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
                    if (adjacencyMatrix[AdjacentRowToSearch, j] != 0 && !closedNodes.Contains(nodes[j]))//checks if there is a weights and if the node has not been visited
                    {
                        double cost = adjacencyMatrix[AdjacentRowToSearch, j] + currentNode.totalDistance+nodes[j].heuristicDistance;//calculates cost for this route
                        if (cost < nodes[j].totalDistance) //if the new total is a cheaper route
                        {
                            nodes[j].totalDistance = cost;//update the route cost and previous path to this node
                            nodes[j].previousNode = currentNode.name;
                        }
                    }
                }
                closedNodes.Add(currentNode);//removes the node from visited nodes and adds it to the closed node list
                openNodes.Remove(currentNode);
            }
            List<string> pathToAdd = infoParse.GetTakenPath(nodes, startNode, endNode, adjacencyMatrix, out int totalCost);//converts the node list to a list contraining the true path
            return infoParse.BuildPathFromStartToEnd(pathToAdd, totalCost);//builds the path into the required string format
        }

        /// <summary>
        /// Gets the Heuristic distance from any given node to the end node
        /// </summary>
        /// <param name="currentNodeX">The current nodes x value</param>
        /// <param name="currentNodeY">The current nodes Y value</param>
        /// <param name="endNodeX">The end nodes x value</param>
        /// <param name="endNodeY">The end nodes Y value</param>
        /// <returns></returns>
        private static double GetHeuristicDistance(double currentNodeX, double currentNodeY, double endNodeX, double endNodeY)
        {
            return Math.Sqrt(Math.Pow((endNodeX - currentNodeX), 2) + Math.Pow((endNodeY - currentNodeY), 2));
        }
    }
}
