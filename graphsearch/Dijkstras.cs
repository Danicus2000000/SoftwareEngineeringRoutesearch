using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace graphsearch
{
    public class Dijkstras
    {
        /*
         * A-K E: 15 A: 17
         * A-G E: 9  A: 11
         * A-E E: 13 A: 6
         */
        /// <summary>
        /// Runs Dijkstras on a list of nodes
        /// </summary>
        /// <param name="nodes">The list of nodes to parse</param>
        /// <param name="startNode">A refrence to the start node in the list</param>
        /// <param name="endNode">A refrence to the end node in the list</param>
        /// <param name="adjacencyMatrix">The Adjacency Matrix of the edges and nodes</param>
        /// <returns>A string describing the lowest cost path</returns>
        public string Run(List<Node> nodes, Node startNode, Node endNode, int[,] adjacencyMatrix)
        {
            GetInformation infoParse = new GetInformation();
            List<Node> openNodes = new List<Node>();
            List<Node> closedNodes = new List<Node>();//creates list to store completed nodes
            openNodes.Add(startNode);//adds start node to the open nodes pile
            Node currentNode = startNode;//sets current node to start node
            do //while we are not at the destination
            {
                int AdjacentRowToSearch = nodes.IndexOf(currentNode);
                for (int j = 0; j < adjacencyMatrix.GetLength(1); j++)
                {
                    if (adjacencyMatrix[AdjacentRowToSearch, j] != 0 && !openNodes.Contains(nodes[j]) && !closedNodes.Contains(nodes[j])) //if a matrix value has a weight and has not been cheked and is adjacent to current node
                    {
                        nodes[j].distanceFromStartNode = adjacencyMatrix[AdjacentRowToSearch, j] + nodes[AdjacentRowToSearch].distanceFromStartNode;//increase distance from start node on specified node
                        if (nodes[j].distanceFromStartNode < nodes[j].totalDistance) //if the new total is a cheaper route
                        {
                            nodes[j].totalDistance = (double)nodes[j].distanceFromStartNode;//update the route cost and previous path to this node
                            nodes[j].previousNode = currentNode.name;
                        }
                        openNodes.Add(nodes[j]);
                        //add to open nodes
                    }
                }
                currentNode.visited = true;
                //once all adjacent nodes for current have been checked we remove the cheapest node and make it the current node
                openNodes.Remove(currentNode);
                closedNodes.Add(currentNode);
                currentNode = infoParse.getCheapestNode(openNodes);
                openNodes.Remove(currentNode);
            } while (currentNode != endNode);
            List<string> pathToAdd=infoParse.getTakenPath(nodes, startNode, endNode, adjacencyMatrix, out int totalCost);
            return infoParse.BuildPathFromStartToEnd(pathToAdd, totalCost);
        }
    }
}
