using System;
using System.Collections.Generic;
using System.Text;

namespace graphsearch
{
    public class Node
    {
        public string name { get; private set; } //name of node
        public int nodeIndex { get; private set; }//node index
        public bool visited { get; private set; }//if the node has been visited
        public Dictionary<int, int> paths;//contains all paths takeable from this node first int represents nodeIndex second represents cost to traverse it
        public double xCoord { get; private set; }//the x coordinate of this node
        public double yCoord { get; private set; }//the y coordinate of this node
        public bool isStartNode { get; set; }//wether this node is the start node
        public bool isEndNode { get; set; }//wether this node is the end node
        public int distanceFromStartNode = 0;//distance from start node at any given time during runtime
        public double heuristicDistance = 0;//distance from end node ignoring all obsticals
        public string previousNode=null;//previous node used to traverse to this point
        public double totalDistance = 999999999;//calculates total distance including from start and heuristic

        /// <summary>
        /// Intialiser for a Node
        /// </summary>
        /// <param name="pName">The name of the node</param>
        /// <param name="pNodeIndex">The index of the node</param>
        /// <param name="pXCoord">The x coordinate of the node</param>
        /// <param name="pYCoord">The y coordinate of the node</param>
        public Node(string pName,int pNodeIndex,double pXCoord,double pYCoord)
        {
            name = pName;
            nodeIndex = pNodeIndex;
            visited = false;
            paths = new Dictionary<int, int>();
            xCoord = pXCoord;
            yCoord = pYCoord;
            isEndNode = false;
            isStartNode = false;
        }
    }
}
