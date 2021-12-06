using System;
using System.Collections.Generic;
using System.Text;

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
        public string Run(List<Node> nodes,Node startNode,Node endNode, int[,] adjacencyMatrix) 
        {
            Node currentNode= startNode;
            int adjacencyIndex=nodes.IndexOf(currentNode);
            while (currentNode != endNode)
            {
                for (int j = 0; j < adjacencyMatrix.GetLength(1); j++)
                {
                    if (adjacencyMatrix[adjacencyIndex, j] != 0)
                    {
                        nodes[j].distanceFromStartNode = currentNode.distanceFromStartNode + adjacencyMatrix[adjacencyIndex, j];
                        if (nodes[j].distanceFromStartNode < nodes[j].totalDistance)
                        {
                            nodes[j].totalDistance=nodes[j].distanceFromStartNode;
                            nodes[j].previousNode = nodes[adjacencyIndex].name;
                        }
                    }
                }
                Node cheapestNode = null;//once all adjacent nodes for current have been checked we remove the cheapest node and make it the current node
                double cheapestNodeValue = 999999999;
                foreach (Node node in nodes)
                {
                    if (node.totalDistance < cheapestNodeValue)
                    {
                        cheapestNode = node;
                        cheapestNodeValue = node.totalDistance;
                    }
                }
                currentNode = cheapestNode;
            }
            GetInformation infoParse = new GetInformation();
            List<string> pathToAdd = infoParse.getTakenPath(nodes, startNode, endNode, adjacencyMatrix, out int totalCost);
            return infoParse.BuildPathFromStartToEnd(pathToAdd, totalCost);
        }
    }
}
