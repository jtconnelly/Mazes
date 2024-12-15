using System.Collections;

namespace Mazes
{
    /***********************************************************
     * a Graph Implementation for non null types.
     * A graph is a set of vertices with edges in between.
     * Given this relationship, we can create a structure that can later be visualized and implemented for other purposes.
     * Some Uses would be Mazes: Both Generation and Solving, as well as relational pathing between 2 objects.
     * Implements IEnumerable in order to be able to loop through the vertices.
     * @see DirectionalGraph
     * @see WeightedGraph
     ***********************************************************/
    public class Graph<T> : IEnumerable<T> where T : notnull
    {
        /**
         a Dictionary type where the key is a vertex and the value is it's list of neighbors.
         */
        protected readonly Dictionary<T, List<T>> _neighbors;
        public Graph()
        {
            this._neighbors = new Dictionary<T, List<T>>();
        }

        /**
         * Add a vertex to the map, default initialized with no neighbors.
         * @param v the value of the vertex we wish to add.
         */
        public void AddVertex(T v)
        {
            if (!this._neighbors.ContainsKey(v))
            {
                this._neighbors.Add(v, new List<T>());
            }
        }

        /**
          Add an Edge between u to v.
        Note: Will only add an edge if both vertices already exist.
        @see AddVertex(T v)
        @param u the vertex that you wish to add an edge to.
        @param v the other vertex that you wish to add an edge to.
         */
        public void AddEdge(T u, T v)
        {
            if (this._neighbors.ContainsKey(u) && this._neighbors.ContainsKey(v))
            {
                this._neighbors[u].Add(v);
                this._neighbors[v].Add(u);
            }
        }

        /**
         * Get the list of neighbors of a given vertex
         * @returns the list of neighbors for a vertex if the vertex exists, else an empty list
         * @param v the vertex we wish to get the neighbors for.
         */
        public List<T> getNeighbors(T v)
        {
            if (this._neighbors.ContainsKey(v))
            {
                return this._neighbors[v];
            }
            return new List<T>();
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (T v in this._neighbors.Keys)
            {
                yield return v;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }

    /***********************************************************
     * a directional graph implementation based on the Graph Class.
     * @see Graph
     * @see WeightedGraph
     * A directional graph implementation does not reciprocate an edge between vertices, thus giving 
     * a direction in which a vertex is a neighbor to another. The rest of the implementation would stay the same.
     ***********************************************************/
    public class DirectionalGraph<T> : Graph<T> where T : notnull
    {
        /**
          Add an Edge from u to v, but not v to u

        @param u the vertex that you wish to add an edge to.
        @param v the vertex that will have an edge pointed to it.
         */
        public new void AddEdge(T u, T v)
        {
            if (this._neighbors.ContainsKey(u) && this._neighbors.ContainsKey(v))
            {
                this._neighbors[u].Add(v);
            }
        }
    }

    /***********************************************************
     * a weighted graph implementation based on the Graph Class.
     * @see Graph
     * @see WeightedDirectionalGraph
     * A weighted Graph assigns a weight to an edge. This weight can be used by algorithms to find the most opimal paths, such as Dijkstra's or A* algorithms.
     ***********************************************************/
    public class WeightedGraph<T> : IEnumerable<T> where T : notnull
    {
        /**
         a Dictionary type where the key is a vertex and the value is it's list of neighbors.
         */
        protected readonly Dictionary<T, List<KeyValuePair<T, int>>> _neighbors;
        public WeightedGraph()
        {
            this._neighbors = new Dictionary<T, List<KeyValuePair<T, int>>>();
        }

        /**
         * Add a vertex to the map, default initialized with no neighbors.
         * @param v the value of the vertex we wish to add.
         */
        public void AddVertex(T v)
        {
            if (!this._neighbors.ContainsKey(v))
            {
                this._neighbors.Add(v, new List<KeyValuePair<T, int>>());
            }
        }

        /**
          Add an Edge between u to v.
        Note: Will only add an edge if both vertices already exist.
        @see AddVertex(T v)
        @param u the vertex that you wish to add an edge to.
        @param v the other vertex that you wish to add an edge to.
        @param weight the given weight of said edge.
         */
        public void AddEdge(T u, T v, int weight)
        {
            if (this._neighbors.ContainsKey(u) && this._neighbors.ContainsKey(v))
            {
                this._neighbors[u].Add(new KeyValuePair<T, int>(v, weight));
                this._neighbors[v].Add(new KeyValuePair<T, int>(u, weight));
            }
        }

        /**
         * Get the list of neighbors of a given vertex
         * @returns the list of neighbors for a vertex if the vertex exists with their accompanying weights, else an empty list
         * @param v the vertex we wish to get the neighbors for.
         */
        public List<KeyValuePair<T, int>> getNeighbors(T v)
        {
            if (this._neighbors.ContainsKey(v))
            {
                return this._neighbors[v];
            }
            return new List<KeyValuePair<T, int>>();
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (T v in this._neighbors.Keys)
            {
                yield return v;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }

    /***********************************************************
     * a weighted directional graph implementation based on the Graph Class.
     * @see DirectionalGraph
     * @see WeightedGraph
     * A directional graph implementation does not reciprocate an edge between vertices, thus giving 
     * a direction in which a vertex is a neighbor to another. The rest of the implementation would stay the same.
     ***********************************************************/
    public class WeightedDirectionalGraph<T> : WeightedGraph<T> where T : notnull
    {
        /**
          Add an Edge from u to v, but not v to u

        @param u the vertex that you wish to add an edge to.
        @param v the vertex that will have an edge pointed to it.
        @param weight the given weight of said edge.
         */
        public new void AddEdge(T u, T v, int weight)
        {
            if (this._neighbors.ContainsKey(u) && this._neighbors.ContainsKey(v))
            {
                this._neighbors[u].Add(new KeyValuePair<T, int>(v, weight));
            }
        }
    }

    /***********************************************************
    * a weighted graph implementation based on the Graph Class forcing non-negative weights.
    * @see WeightedGraph
    * A weighted Graph assigns a weight to an edge. This weight can be used by algorithms to find the most opimal paths, such as Dijkstra's or A* algorithms.
    * Forcing non negative weights makes sure that we can always use Dijkstras
    ***********************************************************/
    public class NonNegativeWeightedGraph<T> : WeightedGraph<T> where T : notnull
    {
        /**
         a Dictionary type where the key is a vertex and the value is it's list of neighbors.
         */
        protected new readonly Dictionary<T, List<KeyValuePair<T, uint>>> _neighbors;
        public NonNegativeWeightedGraph()
        {
            this._neighbors = new Dictionary<T, List<KeyValuePair<T, uint>>>();
        }

        /**
         * Add a vertex to the map, default initialized with no neighbors.
         * @param v the value of the vertex we wish to add.
         */
        public new void AddVertex(T v)
        {
            if (!this._neighbors.ContainsKey(v))
            {
                this._neighbors.Add(v, new List<KeyValuePair<T, uint>>());
            }
        }

        /**
          Add an Edge between u to v.
        Note: Will only add an edge if both vertices already exist.
        @see AddVertex(T v)
        @param u the vertex that you wish to add an edge to.
        @param v the other vertex that you wish to add an edge to.
        @param weight the given weight of said edge.
         */
        public void AddEdge(T u, T v, uint weight)
        {
            if (this._neighbors.ContainsKey(u) && this._neighbors.ContainsKey(v))
            {
                this._neighbors[u].Add(new KeyValuePair<T, uint>(v, weight));
                this._neighbors[v].Add(new KeyValuePair<T, uint>(u, weight));
            }
        }

        /**
         * Get the list of neighbors of a given vertex
         * @returns the list of neighbors for a vertex if the vertex exists with their accompanying weights, else an empty list
         * @param v the vertex we wish to get the neighbors for.
         */
        public new List<KeyValuePair<T, uint>> getNeighbors(T v)
        {
            if (this._neighbors.ContainsKey(v))
            {
                return this._neighbors[v];
            }
            return new List<KeyValuePair<T, uint>>();
        }
    }
}
