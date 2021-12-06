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
        public string Run(List<Node> nodes, Node startNode, Node endNode, int[,] adjacencyMatrix)
        {
            List<Node> openNodes=new List<Node>();
            List<Node> closedNodes = new List<Node>();//creates list to store completed nodes
            startNode.heuristicDistance=GetHeuristicDistance(startNode.xCoord,startNode.yCoord,endNode.xCoord,endNode.yCoord);//sets all nodes heuristic distance from the end node
            foreach(Node node in nodes) 
            {
                node.heuristicDistance=GetHeuristicDistance(node.xCoord,node.yCoord,endNode.xCoord,endNode.yCoord);
            }
            openNodes.Add(startNode);//adds start node to the open nodes list
            Node currentNode=startNode;//sets current node to start node
            while (currentNode!=endNode) //while we are not at the destination
            {
                int adjacentRowToSearch =nodes.IndexOf(currentNode);
                for(int j=0; j< adjacencyMatrix.GetLength(1); j++) 
                {
                    if (adjacencyMatrix[adjacentRowToSearch, j] != 0 && !openNodes.Contains(nodes[j]) && !closedNodes.Contains(nodes[j])) //if a matrix value has a weight and has not been cheked already
                    {
                        nodes[j].distanceFromStartNode=adjacencyMatrix[adjacentRowToSearch, j]+nodes[adjacentRowToSearch].distanceFromStartNode;//increase distance from start node on specified node
                        if (nodes[j].distanceFromStartNode + nodes[j].heuristicDistance < nodes[j].totalDistance) //if the new total is a cheaper route
                        {
                            nodes[j].totalDistance = (double)nodes[j].distanceFromStartNode + nodes[j].heuristicDistance;//update the route cost and previous path to this node
                            nodes[j].previousNode = currentNode.name;
                        }
                        openNodes.Add(nodes[j]);//add to open nodes
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
                //closedNodes.Add(cheapestNode);
                currentNode = cheapestNode;
            }
            GetInformation infoParse= new GetInformation();
            List<string> pathToAdd = infoParse.getTakenPath(nodes, startNode, endNode, adjacencyMatrix, out int totalCost);
            return infoParse.BuildPathFromStartToEnd(pathToAdd,totalCost);
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
