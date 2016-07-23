using System;
using System.Collections.Generic;

namespace Bicoloring
{
    /// <summary>
    /// Class to represent graph vertex (node).
    /// </summary>
    class Vertex
    {
        /// <summary>
        /// Enumeration of colors to use while doing "Breadth-first search"(BFS).
        /// Because we color vertices using just two colors,
        /// it is convinient to use for one color "0", and
        /// for another color "-1". That way we can toggle
        /// current color (using bitwise compliment operation) while doing BFS and coloring vertices.
        /// </summary>
        /// <remarks>
        /// "0 == ~(-1)" and "-1 == ~ 0", if appropriate type is used.
        /// That is why signed byte is chosen.
        /// </remarks>
        public enum Colors : sbyte {BLUE = -1, GREEN, NONE};

        public Vertex(int number)
        {
            this.Label = number;
            this.Visited = false;
            this.Color = Colors.NONE;
            this.adjacentVertices = new List<int>();
        }

        public int Label { get; private set; }  // vertex label (not used in this program, but just in case)
        public bool Visited { get; set; }       // was vertex visited during BFS or not
        public Colors Color { get; set; }       // to color the graph vertices during BFS

        /// <summary>
        /// To get the count of vertex labels from the list
        /// </summary>
        /// <returns>Count of vertices currently hold in the list</returns>
        public int GetAdjacentCount()
        {
            return adjacentVertices.Count;
        }

        /// <summary>
        /// Adds vertex label to its list
        /// </summary>
        /// <param name="adjacent">Label of added adjacent vertex</param>
        public void AddAdjacent(int adjacent)
        {
            adjacentVertices.Add(adjacent);
        }

        /// <summary>
        /// Indexer that makes referencing appropriate adjacent vertex label in the list
        /// more convinient.
        /// </summary>
        /// <param name="index">Index of adjacent vertex in the list of adjacent vertices</param>
        /// <returns>Label of the appropriate adjacent vertex</returns>
        public int this[int index]
        {
            get
            {
                return adjacentVertices[index];
            }
        }

        private List<int> adjacentVertices;     // every vertex contains list of its adjacent verteces' labels
    }
}
