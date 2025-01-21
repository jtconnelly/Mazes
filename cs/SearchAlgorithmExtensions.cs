
using System.Linq;
using System.Numerics;

namespace Graph
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
            parent.Add(start, default(T));
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
                        if (!parent.TryAdd(u, v))
                        {
                            parent[u] = v;
                        }
                    }
                    if (u.Equals(end))
                    {
                        List<T> path = new List<T>();
                        while (!u.Equals(default(T)))
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
            parent.Add(start, default(T));
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
                        if (!parent.TryAdd(u, v))
                        {
                            parent[u] = v;
                        }
                    }
                    if (u.Equals(end))
                    {
                        List<T> path = new List<T>();
                        while (!u.Equals(default(T)))
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

    public static class DistanceHeuristicAlgorithms
    {

        public static double EuclidianDistance<T>(T x1, T y1, T x2, T y2) where T : INumber<T>
        {
            T xDiff = x1 - x2;
            T yDiff = y1 - y2;
            return Math.Sqrt(Convert.ToDouble(xDiff * xDiff) + Convert.ToDouble(yDiff * yDiff));
        }

        public static double ManhattanDistance<T>(T x1, T y1, T x2, T y2) where T : INumber<T>
        {
            return Convert.ToDouble(Math.Abs(Convert.ToDecimal(x1 - x2)) + Math.Abs(Convert.ToDecimal(y1 - y2)));
        }

        public static double DiagonalDistance<T>(T x1, T y1, T x2, T y2) where T : INumber<T>
        {
            var dx = Convert.ToDouble(Math.Abs(Convert.ToDecimal(x1 - x2)));
            var dy = Convert.ToDouble(Math.Abs(Convert.ToDecimal(y1 - y2)));

            return (dx + dy) + (Math.Sqrt(2) - 2) * Math.Min(dx, dy);
        }
    }

    /***********************************************************
     * An extension class for WeightedGraph that implements greedy algorithms as well as their complementary paths.
     * @see WeightedGraph
     ***********************************************************/
    public static class GreedyAlgorithmExtensions
    {
        /**
         * Path function that will give a list of vertices to visit from start to end, given a start will not have a parent in the dictionary
         * 
         * This is used in every shortest path function to give us back the path
         * 
         * @returns a list of each visited vertex from start to end
         * @param parent dictionary of each value and their corresponding parent, where the start will have a default(T) value for null
         * @param the end point we want to reach
         **/
        internal static List<T> Path<T>(Dictionary<T, T> parent, T end)
        {
            List<T> ans = new List<T>();
            ans.Add(end);
            T u = parent[end];
            while (!u.Equals(default(T)))
            {
                ans.Add(u);
                u = parent[u];
            }
            ans.Reverse();
            return ans;
        }

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

            Dictionary<T, uint> distances = new Dictionary<T, uint>();
            foreach (var node in graph)
            {
                distances[node] = uint.MaxValue; // Logical infinity
            }
            if (!distances.ContainsKey(end) || !distances.ContainsKey(start))
            {
                return new List<T>(); // Get out before we waste time, end doesn't even exist in the graph
            }

            distances[start] = 0;
            ISet<T> found = new HashSet<T>(); // What Nodes we found
            PriorityQueue<T, uint> waiting = new PriorityQueue<T, uint>(); // Waiting queue that we will pop as we can, the given weight determines order
            Dictionary<T, T> parent = new Dictionary<T, T>(); // How we will trace back from a node for the path

            //First step: Add start
            found.Add(start);
            waiting.Enqueue(start, 0);
            parent.Add(start, default(T));

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
                        if (!parent.TryAdd(neighbor.Key, v)) // If this would actually be the shortest path
                        {
                            parent[neighbor.Key] = v;
                        }
                        distances[neighbor.Key] = neighbor.Value + distances[v]; // updating the distances
                    }
                    if (!found.Contains(neighbor.Key))
                    {
                        found.Add(neighbor.Key);
                        waiting.Enqueue(neighbor.Key, neighbor.Value);
                        if (!parent.TryAdd(neighbor.Key, v))
                        {
                            parent[neighbor.Key] = v;
                        }
                    }
                }
            }

            // Pathing step: Now that we have the distances, return the path if there is one.
            if (distances[end] == uint.MaxValue)
            {
                return new List<T>(); // Empty list: end is completely unreachable
            }
            return Path(parent, end);
        }


        /**
         * Implementation of Dijkstra's algorithm to return the shortest path from start to end.
         * 
         * This is not the most efficient algorithm, nor the most efficient implementation of the algorithm. The main issue is that for any start and end we will have to do the distance mapping step again.
         * One way to make this more time efficient for larger programs is to encapsulate a cache class.
         * Whenever a start is first called to be used in Dijkstras, we save the parent map so that from any endpoint we just have to follow back the trace.
         * This would be better in the long run, as long as any later addEdge or addVertex call either deletes this cache or updates it.
         * 
         * An important note: Dijkstras does not work with negative weights, so we have to enforce the weights being non-negative
         * @see Graph
         * @param graph since this is an extension, graph would be where we call this function to run on that specific graph instance.
         * @param start the starting vertex that we will begin the search from.
         * @param end the ending vertex that we are looking for.
         * @returns a list format of the found path between start and end, if any
         */
        public static List<T> Dijkstra<T>(this Graph<T> graph, T start, T end) where T : notnull {
            if (graph is null)
            {
                throw new ArgumentNullException(nameof(graph));
            }

            Dictionary<T, uint> distances = new Dictionary<T, uint>();
            foreach (var node in graph)
            {
                distances[node] = uint.MaxValue; // Logical infinity
            }
            if (!distances.ContainsKey(end) || !distances.ContainsKey(start))
            {
                return new List<T>(); // Get out before we waste time, end doesn't even exist in the graph
            }

            distances[start] = 0;
            ISet<T> found = new HashSet<T>(); // What Nodes we found
            PriorityQueue<T, uint> waiting = new PriorityQueue<T, uint>(); // Waiting queue that we will pop as we can, the given weight determines order
            Dictionary<T, T> parent = new Dictionary<T, T>(); // How we will trace back from a node for the path

            //First step: Add start
            found.Add(start);
            waiting.Enqueue(start, 0);
            parent.Add(start, default(T));

            // Distance Building Step: Go through every node that we can possibly reach from start, even if we find end
            while (waiting.Count > 0)
            {
                T v = waiting.Dequeue();
                var neighbors = graph.getNeighbors(v);
                for (int i = 0; i < neighbors.Count; i++)
                {
                    var neighbor = neighbors[i];
                    if (1 + distances[v] < distances[neighbor])
                    {
                        if (parent.TryAdd(neighbor, v)) // If this would actually be the shortest path
                        {
                            parent[neighbor] = v;
                        }
                        distances[neighbor] = 1 + distances[v]; // updating the distances
                    }
                    if (!found.Contains(neighbor))
                    {
                        found.Add(neighbor);
                        waiting.Enqueue(neighbor, distances[neighbor]);
                        if (parent.TryAdd(neighbor, v))
                        {
                            parent[neighbor] = v;
                        }
                    }
                }
            }

            // Pathing step: Now that we have the distances, return the path if there is one.
            if (distances[end] == uint.MaxValue)
            {
                return new List<T>(); // Empty list: end is completely unreachable
            }
            return Path(parent, end);
        }

        /**
         * Implementation of the Bellman-Ford algorithm to return the shortest path from start to end.
         * 
         * This will go through every possible lowest option in a greedy fashion, indiscriminately finding out the shortest possible route
         * This is not the fastest option, but will guarantee the lowest possible finding
         * Since the loop is locked into only how many vertices there are, we can use negative weights
         * 
         * @see WeightedGraph
         * @param graph since this is an extension, graph would be where we call this function to run on that specific graph instance.
         * @param start the starting vertex that we will begin the search from.
         * @param end the ending vertex that we are looking for.
         * @returns a list format of the found path between start and end, if any
         */
        public static List<T> BellmanFord<T>(this WeightedGraph<T> graph, T start, T end) where T : notnull {
            if (graph is null)
            {
                throw new ArgumentNullException(nameof(graph));
            }

            Dictionary<T, int> distances = new Dictionary<T, int>();
            foreach (var node in graph)
            {
                distances[node] = int.MaxValue; // Logical infinity
            }
            if (!distances.ContainsKey(end) || !distances.ContainsKey(start))
            {
                return new List<T>(); // Get out before we waste time, end doesn't even exist in the graph
            }

            distances[start] = 0;
            Dictionary<T, T> parent = new Dictionary<T, T>(); // How we will trace back from a node for the path
            parent.Add(start, default(T));

            for (int i = 0; i < graph.Count(); ++i)
            {
                foreach(var node in graph)
                {
                    foreach(var neighbor in graph.getNeighbors(node))
                    {
                        if (neighbor.Value + distances[node] < distances[neighbor.Key])
                        {
                            distances[neighbor.Key] = neighbor.Value + distances[node];
                            if (!parent.TryAdd(neighbor.Key, node))
                            {
                                parent[neighbor.Key] = node;
                            }
                        }
                    }
                }
            }

            // Pathing step: Now that we have the distances, return the path if there is one.
            if (distances[end] == uint.MaxValue)
            {
                return new List<T>(); // Empty list: end is completely unreachable
            }
            return Path(parent, end);
        }

        internal static List<List<int>> GetAdjacencyMatrix<T>(WeightedGraph<T> graph)
        {
            List<List<int>> ans = new List<List<int>> ();
            for (int i = 0; i < graph.Count(); i++)
            {
                var neighbors = graph.getNeighbors(graph.ElementAt(i));
                ans.Add(new List<int> ());
                for (int j = 0; j < graph.Count(); j++)
                {
                    if (i == j)
                    {
                        ans[i].Add(int.MinValue);
                        continue;
                    }
                    bool found = false;
                    foreach (var neighbor in neighbors)
                    {
                        if (neighbor.Key.Equals(graph.ElementAt(j)))
                        {
                            found = true;
                            ans[i].Add(neighbor.Value);
                            break;
                        }
                    }
                    if (!found)
                    {
                        ans[i].Add(int.MaxValue); // logical infinity
                    }
                }
            }
            return ans;
        }

        /**
         * Implementation of the Floyd-Warshall Algorithm for Weighted Graphs
         * 
         * This algorithm is not efficient and varies heavily from the others by requiring the be used in terms of the adjacency matrix. 
         * Given an adjacency matrix, we go through N^3 times over vertices to get the smallest value between 2 vertices. After this, we trace back to start from our end point by grabbing the lowest possible value for that column in the matrix.
         * 
         * @see WeightedGraph
         * @param graph since this is an extension, graph would be where we call this function to run on that specific graph instance.
         * @param start the starting vertex that we will begin the search from.
         * @param end the ending vertex that we are looking for.
         * @returns a list format of the found path between start and end, if any
         */
        public static List<T> FloydWarshall<T>(this WeightedGraph<T> graph, T start, T end) where T : notnull {
            if (graph is null)
            {
                throw new ArgumentNullException(nameof(graph));
            }
            Dictionary<T, T> parent = new Dictionary<T, T>(); // How we will trace back from a node for the path
            parent.Add(start, default(T));
            var asMatrix = GetAdjacencyMatrix(graph);
            for (int k = 0; k < asMatrix.Count(); k++)
            {
                for (int i = 0; i < asMatrix.Count(); i++)
                {
                    for (int j = 0; j < asMatrix.Count(); j++)
                    {
                        if (asMatrix[i][k] + asMatrix[k][j] < asMatrix[i][j])
                        {
                            asMatrix[i][j] = asMatrix[i][k] + asMatrix[k][j];
                        }
                    }
                }
            }

            List<T> ans = new List<T>();
            T curNode = end;
            while (!curNode.Equals(start))
            {
                bool foundSomething = false;
                for (int i = 0; i < asMatrix.Count(); i++)
                {
                    if (graph.ElementAt(i).Equals(curNode))
                    {
                        foundSomething = true;
                        ans.Add(curNode);
                        //find the min
                        int min = int.MaxValue;
                        int idx = 0;
                        for (int j = 0; j < asMatrix[i].Count(); ++j)
                        {
                            if (asMatrix[i][j] > int.MinValue && asMatrix[i][j] < min)
                            {
                                min = asMatrix[i][j];
                                idx = j;
                            }
                        }
                        curNode = graph.ElementAt(asMatrix[i][idx]);
                    }
                    if (!foundSomething)
                    {
                        return new List<T>();
                    }
                }
            }
            ans.Add(start);
            ans.Reverse();
            return ans;
        }

        /**
         * Implementation of the A* algorithm for non negative weighted graphs
         * @see AStar<T>(this Mazes.CoordinateGraph<T> graph, Mazes.Coordinate2D<T> start, Mazes.Coordinate2D<T> end)
         * 
         * A* is an evolution of Dijkstras algorithm, implementing a heuristic that is targeted towards the end goal, while Dijkstra's will get the shortest path for all possible nodes.
         * @see Dijkstra<T>(this NonNegativeWeightedGraph<T> graph, T start, T end)
         * 
         * The heuristic should be taken into account based on how we are able to "travel" around the nodes. For example: you can't traverse through walls, and some places you can only travel in 4 directions (mazes)
         * @param graph since this is an extension, graph would be where we call this function to run on that specific graph instance.
         * @param start the starting vertex that we will begin the search from.
         * @param end the ending vertex that we are looking for.
         * @returns a list format of the found path between start and end, if any
         */
        public static List<Mazes.Coordinate2D<T>> AStar<T>(this Mazes.NonNegativeWeightedCoordinateGraph<T> graph, Mazes.Coordinate2D<T> start, Mazes.Coordinate2D<T> end, Func<T, T, T, T, double> heuristic) where T : INumber<T> {
            if (graph is null)
            {
                throw new ArgumentNullException(nameof(graph));
            }
            Dictionary<Mazes.Coordinate2D<T>, uint> distances = new Dictionary<Mazes.Coordinate2D<T>, uint>();
            foreach (var node in graph)
            {
                distances[node] = uint.MaxValue; // Logical infinity
            }
            if (!distances.ContainsKey(end) || !distances.ContainsKey(start))
            {
                return new List<Mazes.Coordinate2D<T>>(); // Get out before we waste time, end doesn't even exist in the graph
            }

            distances[start] = 0;
            ISet<Mazes.Coordinate2D<T>> found = new HashSet<Mazes.Coordinate2D<T>>(); // What Nodes we found
            PriorityQueue<Mazes.Coordinate2D<T>, uint> waiting = new PriorityQueue<Mazes.Coordinate2D<T>, uint>(); // Waiting queue that we will pop as we can, the given weight determines order
            Dictionary<Mazes.Coordinate2D<T>, Mazes.Coordinate2D<T>> parent = new Dictionary<Mazes.Coordinate2D<T>, Mazes.Coordinate2D<T>>(); // How we will trace back from a node for the path

            //First step: Add start
            found.Add(start);
            waiting.Enqueue(start, 0);
            parent.Add(start, default(Mazes.Coordinate2D<T>));

            // Distance Building Step: Go through every node that we can possibly reach from start, even if we find end
            while (waiting.Count > 0)
            {
                var v = waiting.Dequeue();
                if (v == end)
                {
                    break;
                }
                var neighbors = graph.getNeighbors(v);
                for (int i = 0; i < neighbors.Count; i++)
                {
                    var neighbor = neighbors[i];
                    if (neighbor.Value + distances[v] < distances[neighbor.Key])
                    {
                        if (!parent.TryAdd(neighbor.Key, v)) // If this would actually be the shortest path
                        {
                            parent[neighbor.Key] = v;
                        }
                        distances[neighbor.Key] = neighbor.Value + distances[v]; // updating the distances
                    }
                    if (!found.Contains(neighbor.Key))
                    {
                        found.Add(neighbor.Key);
                        // Really where the main difference starts: We add the heuristic to the value in waiting to change the priority so we focus on getting to the end faster
                        waiting.Enqueue(neighbor.Key, neighbor.Value + Convert.ToUInt32(heuristic(neighbor.Key.x, neighbor.Key.y, end.x, end.y)));
                        if (!parent.TryAdd(neighbor.Key, v))
                        { 
                            parent[neighbor.Key] = v; 
                        }
                    }
                }
            }

            // Pathing step: Now that we have the distances, return the path if there is one.
            if (distances[end] == uint.MaxValue)
            {
                return new List<Mazes.Coordinate2D<T>>(); // Empty list: end is completely unreachable
            }
            return Path(parent, end);
        }

        /**
         * Implementation of the A* algorithm for non-weighted graphs
         * @see AStar<T>(this WeightedGraph<T> graph, T start, T end)
         * 
         * A* is an evolution of Dijkstras algorithm, implementing a heuristic that is targeted towards the end goal, while Dijkstra's will get the shortest path for all possible nodes.
         * @see Dijkstra<T>(this NonNegativeWeightedGraph<T> graph, T start, T end)
         * We can use this algorithm with non-weighted graphs, since we are just calculating movement from A to B, so every "move" is the same as adding a distance of 1 in Dijkstra.
         * 
         * The heuristic should be taken into account based on how we are able to "travel" around the nodes. For example: you can't traverse through walls, and some places you can only travel in 4 directions (mazes)
         * @param graph since this is an extension, graph would be where we call this function to run on that specific graph instance.
         * @param start the starting vertex that we will begin the search from.
         * @param end the ending vertex that we are looking for.
         * @returns a list format of the found path between start and end, if any
         */
        public static List<Mazes.Coordinate2D<T>> AStar<T>(this Mazes.CoordinateGraph<T> graph, Mazes.Coordinate2D<T> start, Mazes.Coordinate2D<T> end, Func<T, T, T, T, double> heuristic) where T : INumber<T> {
            if (graph is null)
            {
                throw new ArgumentNullException(nameof(graph));
            }
            Dictionary<Mazes.Coordinate2D<T>, uint> distances = new Dictionary<Mazes.Coordinate2D<T>, uint>();
            foreach (var node in graph)
            {
                distances[node] = uint.MaxValue; // Logical infinity
            }
            if (!distances.ContainsKey(end) || !distances.ContainsKey(start))
            {
                return new List<Mazes.Coordinate2D<T>>(); // Get out before we waste time, end doesn't even exist in the graph
            }

            distances[start] = 0;
            ISet<Mazes.Coordinate2D<T>> found = new HashSet<Mazes.Coordinate2D<T>>(); // What Nodes we found
            PriorityQueue<Mazes.Coordinate2D<T>, uint> waiting = new PriorityQueue<Mazes.Coordinate2D<T>, uint>(); // Waiting queue that we will pop as we can, the given weight determines order
            Dictionary<Mazes.Coordinate2D<T>, Mazes.Coordinate2D<T>> parent = new Dictionary<Mazes.Coordinate2D<T>, Mazes.Coordinate2D<T>>(); // How we will trace back from a node for the path

            //First step: Add start
            found.Add(start);
            waiting.Enqueue(start, 0);
            parent.Add(start, default(Mazes.Coordinate2D<T>));

            // Distance Building Step: Go through every node that we can possibly reach from start, even if we find end
            while (waiting.Count > 0)
            {
                var v = waiting.Dequeue();
                if (v == end)
                {
                    break;
                }
                var neighbors = graph.getNeighbors(v);
                for (int i = 0; i < neighbors.Count; i++)
                {
                    var neighbor = neighbors[i];
                    if (1 + distances[v] < distances[neighbor])
                    {
                        if (!parent.TryAdd(neighbor, v)) // If this would actually be the shortest path
                        {
                            parent[neighbor] = v;
                        }
                        distances[neighbor] = 1 + distances[v]; // updating the distances
                    }
                    if (!found.Contains(neighbor))
                    {
                        found.Add(neighbor);
                        // Really where the main difference starts: We add the heuristic to the value in waiting to change the priority so we focus on getting to the end faster
                        waiting.Enqueue(neighbor, 1 + Convert.ToUInt32(heuristic(neighbor.x, neighbor.y, end.x, end.y)));
                        if (!parent.TryAdd(neighbor, v))
                        { 
                            parent[neighbor] = v; 
                        }
                    }
                }
            }

            // Pathing step: Now that we have the distances, return the path if there is one.
            if (distances[end] == uint.MaxValue)
            {
                return new List<Mazes.Coordinate2D<T>>(); // Empty list: end is completely unreachable
            }
            return Path(parent, end);
        }
    }
}
