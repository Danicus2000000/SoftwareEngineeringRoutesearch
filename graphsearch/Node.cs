namespace graphsearch
{
    /// <summary>
    /// Intialiser for a Node
    /// </summary>
    /// <param name="pName">The name of the node</param>
    /// <param name="pNodeIndex">The index of the node</param>
    /// <param name="pXCoord">The x coordinate of the node</param>
    /// <param name="pYCoord">The y coordinate of the node</param>
    public class Node(string pName, int pNodeIndex, double pXCoord, double pYCoord)
    {
        public string Name { get; private set; } = pName;
        public int NodeIndex { get; private set; } = pNodeIndex;
        public double XCoord { get; private set; } = pXCoord;
        public double YCoord { get; private set; } = pYCoord;
        public double HeuristicDistance = 0;//distance from end node ignoring all obsticals
        public string PreviousNode = null;//previous node used to traverse to this point
        public double TotalDistance = double.PositiveInfinity;//calculates total distance including from start and heuristic
    }
}
