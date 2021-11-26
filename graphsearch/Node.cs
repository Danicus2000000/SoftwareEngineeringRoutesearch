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
        public int xCoord { get; private set; }//the x coordinate of this node
        public int yCoord { get; private set; }//the y coordinate of this node
        /// <summary>
        /// Intialiser for a Node
        /// </summary>
        /// <param name="pName">The name of the node</param>
        /// <param name="pNodeIndex">The index of the node</param>
        /// <param name="pXCoord">The x coordinate of the node</param>
        /// <param name="pYCoord">The y coordinate of the node</param>
        public Node(string pName,int pNodeIndex,int pXCoord,int pYCoord)
        {
            name = pName;
            nodeIndex = pNodeIndex;
            visited = false;
            paths = new Dictionary<int, int>();
            xCoord = pXCoord;
            yCoord = pYCoord;
        }
    }
}
