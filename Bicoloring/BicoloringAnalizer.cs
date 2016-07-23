using System;
using System.IO;
using System.Collections.Generic;

namespace Bicoloring
{
    /// <summary>
    /// Class that tests whether the given graph can be bicolored or not.
    /// </summary>
    class BicoloringAnalizer
    {
        public BicoloringAnalizer()
        {
            this.graph = new List<Vertex>();
        }

        /// <summary>
        /// Method that reads input data, creates graph, emulating
        /// an adjacency list, and fills data.
        /// </summary>
        /// <param name="fileName">Name of input file that holds graph related data</param>
        /// <remarks>
        /// Graph related data in the input file
        /// must conform to the conditions listed on the
        /// "Bicoloring" task webpage.
        /// </remarks>
        public void AnalizeGraphs(string fileName)
        {
            int numOfVertices;
            int numOfEdges;

            try
            {
                using (StreamReader sr = new StreamReader(fileName))
                {
                    string line = sr.ReadLine();
                    numOfVertices = Int32.Parse(line);

                    while (numOfVertices != 0)
                    {
                        if (numOfVertices < 2 || numOfVertices > 199)
                            throw new Exception("Wrong number of nodes." + Environment.NewLine +
                                "Must be \"1 < nodes < 200\"");

                        line = sr.ReadLine();
                        numOfEdges = Int32.Parse(line);
                        if (numOfEdges < 1)
                            throw new Exception("Wrong number of edges.");

                        graph.Clear();
                        for (int i = 0; i < numOfVertices; i++)
                            graph.Add(new Vertex(i));

                        for (int i = 0; i < numOfEdges; i++)
                        {
                            line = sr.ReadLine();
                            string[] readVertices = line.Split(' ');
                            int from = Int32.Parse(readVertices[0]);
                            int to = Int32.Parse(readVertices[1]);

                            if (from >= 0 && from < numOfVertices && to >= 0 && to < numOfVertices)
                                graph[from].AddAdjacent(to);
                            else
                                throw new Exception("Wrong vertex label. Must be \"0 <= vertex_label < numOfVertices\"");

                        }

                        if (IsBipartite(numOfVertices))
                            Console.WriteLine("BICOLORABLE.");
                        else
                            Console.WriteLine("NOT BICOLORABLE.");

                        line = sr.ReadLine();
                        numOfVertices = Int32.Parse(line);
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.Error.WriteLine("Error opening input file:");
                throw;
            }
            catch (IOException)
            {
                Console.Error.WriteLine("Error reading file:");
                throw;
            }
            catch (FormatException)
            {
                Console.Error.WriteLine("Wrong data in file:");
                throw;
            }
        }

        /// <summary>
        /// Method to test graph bipartiteness.
        /// We need the graph to be bipartite in order to bicolor it.
        /// Method uses Breadth-first search algorithm.
        /// </summary>
        /// <param name="n">Number of vertices in graph.</param>
        /// <returns>True or false depending on whether graph was bipartite or not.</returns>
        /// <remarks>
        /// More info:
        /// http://en.wikipedia.org/wiki/Bipartite_graph#Testing_bipartiteness
        /// </remarks>
        private bool IsBipartite(int n)
        {
            int startVertex = 0;
            Vertex.Colors currentColor = Vertex.Colors.BLUE;
            Queue<int> queueOfVertices = new Queue<int>(n);

            queueOfVertices.Enqueue(startVertex);
            graph[startVertex].Visited = true;
            graph[startVertex].Color = currentColor;

            while (queueOfVertices.Count > 0)
            {
                int currentVertex = queueOfVertices.Dequeue();

                for (int i = 0; i < graph[currentVertex].GetAdjacentCount(); i++)
                {
                    int adjacentVertex = graph[currentVertex][i];

                    if (!graph[adjacentVertex].Visited)
                    {
                        graph[adjacentVertex].Visited = true;
                        graph[adjacentVertex].Color = ~currentColor;
                        queueOfVertices.Enqueue(adjacentVertex);
                    }
                    else
                    {
                        if (graph[currentVertex].Color == graph[adjacentVertex].Color)
                            return false;
                    }
                
                }
                currentColor = ~currentColor;   // this toggles the color: BLUE => GREEN, GREEN => BLUE.
            }

            return true;
        }

        private List<Vertex> graph;

    }
}
