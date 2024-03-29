﻿using System;
using System.Collections.Generic;
using System.Text;

namespace graphsearch
{
    public class Node
    {
        public string name { get; private set; } //name of node
        public int nodeIndex { get; private set; }//node index
        public double xCoord { get; private set; }//the x coordinate of this node
        public double yCoord { get; private set; }//the y coordinate of this node
        public double heuristicDistance = 0;//distance from end node ignoring all obsticals
        public string previousNode=null;//previous node used to traverse to this point
        public double totalDistance = double.PositiveInfinity;//calculates total distance including from start and heuristic

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
            xCoord = pXCoord;
            yCoord = pYCoord;
        }
    }
}
