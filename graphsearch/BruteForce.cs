using System.Collections.Generic;
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
        public static string Run(List<Node> nodes, Node startNode, Node endNode, int[,] adjacencyMatrix)
        {
            List<Node> openNodes = [];
            nodes[nodes.IndexOf(startNode)].TotalDistance = 0;//sets the distance of the start node to zero so it will be picked 
            foreach (Node node in nodes)//populates the unvisited node list
            {
                openNodes.Add(node);
            }
            while (openNodes.Count != 0)//While unvisited nodes remain
            {
                Node currentNode = GetInformation.GetCheapestNode(openNodes);//Takes an unvisited noed with the smallest distance 
                nodes[nodes.IndexOf(currentNode)].TotalDistance = currentNode.TotalDistance;
                int AdjacentRowToSearch = nodes.IndexOf(currentNode);//gets the index for the adjecent row
                for (int j = 0; j < adjacencyMatrix.GetLength(1); j++)
                {
                    if (adjacencyMatrix[AdjacentRowToSearch, j] != 0)//checks if there is a weights and if the node has not been visited
                    {
                        double cost = adjacencyMatrix[AdjacentRowToSearch, j] + currentNode.TotalDistance;//calculates cost for this route
                        if (cost < nodes[j].TotalDistance) //if the new total is a cheaper route
                        {
                            nodes[j].TotalDistance = cost;//update the route cost and previous path to this node
                            nodes[j].PreviousNode = currentNode.Name;
                        }
                    }
                }
                openNodes.Remove(currentNode);//removes the node from visited nodes
            }
            List<string> pathToAdd = GetInformation.GetTakenPath(nodes, startNode, endNode, adjacencyMatrix, out int totalCost);//converts the node list to a list contraining the true path
            return GetInformation.BuildPathFromStartToEnd(pathToAdd, totalCost);//builds the path into the required string format
        }
    }
}