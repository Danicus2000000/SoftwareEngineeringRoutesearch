using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace graphsearch
{
    public class Dijkstras
    {
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
            List<Node> openNodes = new List<Node>();
            List<Node> closedNodes = new List<Node>();//creates list to store completed nodes
            List<Node> nodesQueue = nodes;
            openNodes.Add(startNode);//adds start node to the open nodes pile
            Node currentNode = startNode;//sets current node to start node
            do //while we are not at the destination
            {
                nodesQueue.OrderBy(x => x.distanceFromStartNode).ToList();
                int AdjacentRowToSearch = nodesQueue.IndexOf(currentNode);
                for (int j = 0; j < adjacencyMatrix.GetLength(1); j++)
                {
                    if (adjacencyMatrix[AdjacentRowToSearch, j] != 0 && !openNodes.Contains(nodesQueue[j]) && !closedNodes.Contains(nodesQueue[j])) //if a matrix value has a weight and has not been cheked and is adjacent to current node
                    {
                        nodesQueue[j].distanceFromStartNode = adjacencyMatrix[AdjacentRowToSearch, j] + nodesQueue[AdjacentRowToSearch].distanceFromStartNode;//increase distance from start node on specified node
                        if (nodesQueue[j].distanceFromStartNode < nodesQueue[j].totalDistance) //if the new total is a cheaper route
                        {
                            nodesQueue[j].totalDistance = (double)nodesQueue[j].distanceFromStartNode;//update the route cost and previous path to this node
                            nodesQueue[j].previousNode = currentNode.name;
                        }
                        openNodes.Add(nodesQueue[j]);//add to open nodes
                    }
                }
                Node cheapestNode = null;//once all adjacent nodes for current have been checked we remove the cheapest node and make it the current node
                double cheapestNodeValue = 999999999;
                openNodes.Remove(currentNode);
                closedNodes.Add(currentNode);
                foreach (Node node in openNodes)
                {
                    if (node.totalDistance < cheapestNodeValue)
                    {
                        cheapestNode = node;
                        cheapestNodeValue = node.totalDistance;
                    }
                }
                openNodes.Remove(cheapestNode);
                currentNode = cheapestNode;
            } while (currentNode != endNode);
            Node current = endNode;//we then work our way backwards through the tree to find the path the algorithm took along with the cost of this path
            int totalCost = 0;
            List<string> pathToAdd = new List<string>();
            pathToAdd.Add(endNode.name);
            do
            {
                foreach (Node node in nodes)
                {
                    if (node.name == current.previousNode)
                    {
                        pathToAdd.Add(node.name);
                        totalCost += adjacencyMatrix[node.nodeIndex - 1, current.nodeIndex - 1];
                        current = node;
                        break;
                    }
                }

            } while (current != startNode);
            return BuildPathFromStartToEnd(pathToAdd, totalCost);
        }

        /// <summary>
        /// Inverses the list of path adding the hythens between them and appends total cost to the end of the string in order to create valid path value
        /// </summary>
        /// <param name="pathToAdd">The list of all nodes taken in the path in inverse order</param>
        /// <param name="totalCost">The total cost to traverse all nodes in the path</param>
        /// <returns></returns>
        private static string BuildPathFromStartToEnd(List<string> pathToAdd, int totalCost)
        {
            string pathFromStartToEnd = "";
            for (int i = pathToAdd.Count - 1; i != -1; i--) //this is then rebuilt from the list into a human readable string
            {
                if (i == 0)
                {
                    pathFromStartToEnd += "-" + pathToAdd[i] + " " + totalCost.ToString();
                }
                else if (pathToAdd.Count - 1 != i)
                {
                    pathFromStartToEnd += "-" + pathToAdd[i];
                }
                else
                {
                    pathFromStartToEnd = pathToAdd[i];
                }
            }
            return pathFromStartToEnd;
        }
    }
}
