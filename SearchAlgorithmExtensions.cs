﻿namespace Mazes
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
    public static ISet<T> DFS<T>(this Graph<T> graph, T start)
        {
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
        public static ISet<T> BFS<T>(this Graph<T> graph, T start)
        {
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
         * A Breadth First Search Algorithm to find a path between two vertices.
         * Given a starting value, discover the possible paths and routes available from the edges until we find a path between the start and end.
         * @see BFS<T>(this Graph<T> graph, T start)
         * @param graph since this is an extension, graph would be where we call this function to run on that specific graph instance.
         * @param start the starting vertex that we will begin the search from.
         * @param end the ending vertex that we are looking for.
         * @returns a list format of the found path between start and end.
         */
        public static List<T> BFSPath<T>(this Graph<T> graph, T start, T end)
        {
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
                    if (!u.Equals(end))
                    {
                        List<T> path = new List<T>();
                        while (u.Equals(default))
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
         * A Depth First Search Algorithm to find a path between two vertices.
         * Given a starting value, discover the possible paths and routes available from the edges until we find a path between the start and end.
         * @see DFS<T>(this Graph<T> graph, T start)
         * @param graph since this is an extension, graph would be where we call this function to run on that specific graph instance.
         * @param start the starting vertex that we will begin the search from.
         * @param end the ending vertex that we are looking for.
         * @returns a list format of the found path between start and end.
         */
        public static List<T> DFSPath<T>(this Graph<T> graph, T start, T end)
        {
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
                    if (!u.Equals(end))
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
}
