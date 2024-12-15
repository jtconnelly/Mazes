
namespace Mazes
{
    /***********************************************************
     * An extension class for Graph that implements searching algorithms as well as their complementary paths.
     * @see Graph
     ***********************************************************/
    public static class SearchAlgorithmExtensions
    {
        /**
         * A Depth First Search Algorithm.
         * Given a starting value, discover the possible paths and routes available from the edges.
         * In the algorithm, we use a stack to pop the last found neighbor, meaning we will follow a path to it's entirety before we go back up to a previous path.
         * @see BFS<T>(this Graph<T> graph, T start)
         * @see DFSPath<T>(this Graph<T> graph, T start, T end)
         * @param graph since this is an extension, graph would be where we call this function to run on that specific graph instance.
         * @param start the starting vertex that we will begin the search from.
         * @returns a set of all vertexes found that have a path from start.
         */
    public static ISet<T> DFS<T>(this Graph<T> graph, T start) where T : notnull {
            if (graph is null)
            {
                throw new ArgumentNullException(nameof(graph));
            }

            ISet<T> found = new HashSet<T>();
            Stack<T> waiting = new Stack<T>();
            found.Add(start);
            waiting.Push(start);
            while (waiting.Count > 0)
            {
                T v = waiting.Pop();
                foreach (T u in graph.getNeighbors(v))
                {
                    if (!found.Contains(u))
                    {
                        found.Add(u);
                        waiting.Push(u);
                    }
                }
            }
            return found;
        }

        /**
         * A Breadth First Search Algorithm.
         * Given a starting value, discover the possible paths and routes available from the edges.
         * In the algorithm, we use a queue to pop the first found neighbor, meaning we will follow incrementally work through our closest neighbors before moving on to their neighbors.
         * @see DFS<T>(this Graph<T> graph, T start)
         * @see BFSPath<T>(this Graph<T> graph, T start, T end)
         * @param graph since this is an extension, graph would be where we call this function to run on that specific graph instance.
         * @param start the starting vertex that we will begin the search from.
         * @returns a set of all vertexes found that have a path from start.
         */
        public static ISet<T> BFS<T>(this Graph<T> graph, T start) where T : notnull {
            if (graph is null)
            {
                throw new ArgumentNullException(nameof(graph));
            }

            ISet<T> found = new HashSet<T>();
            Queue<T> waiting = new Queue<T>();
            found.Add(start);
            waiting.Enqueue(start);
            while (waiting.Count > 0)
            {
                T v = waiting.Dequeue();
                foreach (T u in graph.getNeighbors(v))
                {
                    if (!found.Contains(u))
                    {
                        found.Add(u);
                        waiting.Enqueue(u);
                    }
                }
            }
            return found;
        }

        /**
         * A Depth First Search Algorithm to find a path between two vertices.
         * Given a starting value, discover the possible paths and routes available from the edges until we find a path between the start and end.
         * @see DFS<T>(this Graph<T> graph, T start)
         * @param graph since this is an extension, graph would be where we call this function to run on that specific graph instance.
         * @param start the starting vertex that we will begin the search from.
         * @param end the ending vertex that we are looking for.
         * @returns a list format of the found path between start and end.
         */
        public static List<T> DFSPath<T>(this Graph<T> graph, T start, T end) where T : notnull {
            if (graph is null)
            {
                throw new ArgumentNullException(nameof(graph));
            }

            ISet<T> found = new HashSet<T>();
            Stack<T> waiting = new Stack<T>();
            Dictionary<T, T> parent = new Dictionary<T, T>();
            found.Add(start);
            waiting.Push(start);
            parent.Add(start, default);
            while (waiting.Count > 0)
            {
                T v = waiting.Pop();
                T u;
                List<T> neighbors = graph.getNeighbors(v);
                for (int i = 0; i < neighbors.Count; i++)
                {
                    u = neighbors[i];
                    if (!found.Contains(u))
                    {
                        found.Add(u);
                        waiting.Push(u);
                        parent.Add(u, v);
                    }
                    if (u.Equals(end))
                    {
                        List<T> path = new List<T>();
                        while (!u.Equals(default))
                        {
                            path.Add(u);
                            u = parent[u];
                        }
                        path.Reverse();
                        return path;
                    }
                }
            }
            return new List<T>();
        }

        /**
         * A Breadth First Search Algorithm to find a path between two vertices.
         * Given a starting value, discover the possible paths and routes available from the edges until we find a path between the start and end.
         * @see BFS<T>(this Graph<T> graph, T start)
         * @param graph since this is an extension, graph would be where we call this function to run on that specific graph instance.
         * @param start the starting vertex that we will begin the search from.
         * @param end the ending vertex that we are looking for.
         * @returns a list format of the found path between start and end.
         */
        public static List<T> BFSPath<T>(this Graph<T> graph, T start, T end) where T: notnull {
            if (graph is null)
            {
                throw new ArgumentNullException(nameof(graph));
            }

            ISet<T> found = new HashSet<T>();
            Queue<T> waiting = new Queue<T>();
            Dictionary<T, T> parent = new Dictionary<T, T>();
            found.Add(start);
            waiting.Enqueue(start);
            parent.Add(start, default);
            while (waiting.Count > 0)
            {
                T v = waiting.Dequeue();
                T u;
                List<T> neighbors = graph.getNeighbors(v);
                for (int i = 0; i < neighbors.Count; i++)
                {
                    u = neighbors[i];
                    if (!found.Contains(u))
                    {
                        found.Add(u);
                        waiting.Enqueue(u);
                        parent.Add(u, v);
                    }
                    if (u.Equals(end))
                    {
                        List<T> path = new List<T>();
                        while (!u.Equals(default))
                        {
                            path.Add(u);
                            u = parent[u];
                        }
                        path.Reverse();
                        return path;
                    }
                }
            }
            return new List<T>();
        }
    }


    /***********************************************************
     * An extension class for WeightedGraph that implements greedy algorithms as well as their complementary paths.
     * @see WeightedGraph
     ***********************************************************/
    public static class GreedyAlgorithmExtensions
    {
        /**
         * Implementation of Dijkstra's algorithm to return the shortest path from start to end.
         * 
         * This is not the most efficient algorithm, nor the most efficient implementation of the algorithm. The main issue is that for any start and end we will have to do the distance mapping step again.
         * One way to make this more time efficient for larger programs is to encapsulate a cache class.
         * Whenever a start is first called to be used in Dijkstras, we save the parent map so that from any endpoint we just have to follow back the trace.
         * This would be better in the long run, as long as any later addEdge or addVertex call either deletes this cache or updates it.
         * 
         * An important note: Dijkstras does not work with negative weights, so we have to enforce the weights being non-negative
         * @see NonNegativeWeightedGraph
         * @param graph since this is an extension, graph would be where we call this function to run on that specific graph instance.
         * @param start the starting vertex that we will begin the search from.
         * @param end the ending vertex that we are looking for.
         * @returns a list format of the found path between start and end, if any
         */
        public static List<T> Dijkstra<T>(this NonNegativeWeightedGraph<T> graph, T start, T end) where T : notnull {
            if (graph is null)
            {
                throw new ArgumentNullException(nameof(graph));
            }
            int count = graph.Count();

            Dictionary<T, uint> distances = new Dictionary<T, uint>();
            foreach (var node in graph)
            {
                distances[node] = uint.MaxValue; // Logical infinity
            }
            if (!distances.ContainsKey(end))
            {
                return new List<T>(); // Get out before we waste time, end doesn't even exist in the graph
            }
            ISet<T> found = new HashSet<T>(); // What Nodes we found
            PriorityQueue<T, uint> waiting = new PriorityQueue<T, uint>(); // Waiting queue that we will pop as we can, the given weight determines order
            Dictionary<T, T> parent = new Dictionary<T, T>(); // How we will trace back from a node for the path

            //First step: Add start
            found.Add(start);
            waiting.Enqueue(start, 0);
            parent.Add(start, default);

            // Distance Building Step: Go through every node that we can possibly reach from start, even if we find end
            while (waiting.Count > 0)
            {
                T v = waiting.Dequeue();
                var neighbors = graph.getNeighbors(v);
                for (int i = 0; i < neighbors.Count; i++)
                {
                    var neighbor = neighbors[i];
                    if (neighbor.Value + distances[v] < distances[neighbor.Key])
                    {
                        parent.Add(neighbor.Key, v); // If this would actually be the shortest path
                        distances[neighbor.Key] = neighbor.Value + distances[v]; // updating the distances
                    }
                    if (!found.Contains(neighbor.Key))
                    {
                        found.Add(neighbor.Key);
                        waiting.Enqueue(neighbor.Key, neighbor.Value);
                        parent.Add(neighbor.Key, v);
                    }
                }
            }

            // Pathing step: Now that we have the distances, return the path if there is one.
            if (distances[end] == uint.MaxValue)
            {
                return new List<T>(); // Empty list: end is completely unreachable
            }
            List<T> ans = new List<T>();
            ans.Add(end);
            T u = parent[end];
            while (!u.Equals(default))
            {
                ans.Add(u);
                u = parent[u];
            }
            ans.Reverse();
            return ans;
        }

        public static void BellmanFord<T>(this WeightedGraph<T> graph, T start, T end) where T : notnull {
            if (graph is null)
            {
                throw new ArgumentNullException(nameof(graph));
            }
        }

        public static void FloydWarshall<T>(this WeightedGraph<T> graph, T start, T end) where T : notnull {
            if (graph is null)
            {
                throw new ArgumentNullException(nameof(graph));
            }
        }

        public static void AStar<T>(this WeightedGraph<T> graph, T start, T end) where T : notnull {
            if (graph is null)
            {
                throw new ArgumentNullException(nameof(graph));
            }
        }

        public static void AStar<T>(this Graph<T> graph, T start, T end) where T : notnull {
            if (graph is null)
            {
                throw new ArgumentNullException(nameof(graph));
            }
        }
    }
}
