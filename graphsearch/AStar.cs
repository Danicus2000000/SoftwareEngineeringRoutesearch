using System;
using System.Collections.Generic;
using System.Text;

namespace graphsearch
{
    class AStar
    {
        /// <summary>
        /// Runs A* on a list of nodes
        /// </summary>
        /// <param name="nodes">The list of nodes to parse</param>
        /// <param name="startNode">A refrence to the start node in the list</param>
        /// <param name="endNode">A refrence to the end node in the list</param>
        /// <param name="adjacencyMatrix">The Adjacency Matrix of the edges and nodes</param>
        /// <returns>A string describing the lowest cost path</returns>
        public string Run(List<Node> nodes, Node startNode, Node endNode, int[,] adjacencyMatrix)
        {
            List<Node> openNodes=new List<Node>();
            List<Node> closedNodes = new List<Node>();//creates list to store completed nodes
            startNode.heuristicDistance=GetHeuristicDistance(startNode.xCoord,startNode.yCoord,endNode.xCoord,endNode.yCoord);//sets all nodes heuristic distance from the end node
            foreach(Node node in nodes) 
            {
                node.heuristicDistance=GetHeuristicDistance(node.xCoord,node.yCoord,endNode.xCoord,endNode.yCoord);
            }
            openNodes.Add(startNode);//adds start node to the open nodes pile
            Node currentNode=startNode;//sets current node to start node
            while (currentNode!=endNode) //while we are not at the destination
            {
                for(int i = 0; i < adjacencyMatrix.GetLength(0); i++) //loops through all adjacency matrix to current node
                {
                    for(int j=0; j< adjacencyMatrix.GetLength(1); j++) 
                    {
                        if (adjacencyMatrix[i, j] != 0 && !openNodes.Contains(nodes[j]) && !closedNodes.Contains(nodes[j]) && nodes.IndexOf(currentNode)==i) //if a matrix value has a weight and has not been cheked and is adjacent to current node
                        {
                            nodes[j].distanceFromStartNode=adjacencyMatrix[i, j]+nodes[i].distanceFromStartNode;//increase distance from start node on specified node
                            if (nodes[j].distanceFromStartNode + nodes[j].heuristicDistance < nodes[j].totalDistance) //if the new total is a cheaper route
                            {
                                nodes[j].totalDistance = (double)nodes[j].distanceFromStartNode + nodes[j].heuristicDistance;//update the route cost and previous path to this node
                                nodes[j].previousNode = currentNode.name;
                            }
                            openNodes.Add(nodes[j]);//add to open nodes
                        }
                    }
                }
                Node cheapestNode = null;//once all adjacent nodes for current have been checked we remove the cheapest node and make it the current node
                double cheapestNodeValue = 999999999;
                openNodes.Remove(currentNode);
                closedNodes.Add(currentNode);
                foreach(Node node in openNodes) 
                {
                    if (node.totalDistance < cheapestNodeValue) 
                    {
                        cheapestNode = node;
                        cheapestNodeValue = node.totalDistance;
                    }
                }
                openNodes.Remove(cheapestNode);
                closedNodes.Add(cheapestNode);
                currentNode = cheapestNode;
            }
            Node current = endNode;//we then work our way backwards through the tree to find the path the algorithm took along with the cost of this path
            string pathFromStartToEnd = "";
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
                        totalCost += adjacencyMatrix[node.nodeIndex-1, current.nodeIndex-1];
                        current = node;
                        break;
                    }
                }

            } while (current != startNode);
            return BuildPathFromStartToEnd(pathToAdd,totalCost);
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
