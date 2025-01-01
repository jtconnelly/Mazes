
namespace Graph
{


    /***********************************************************
     * a wrapper class for our main function to test our graphs and algorithms.
     * @see Graph
     * @see SearchAlgorithmExtensions
     ***********************************************************/
    class GraphsMain
    {     
        internal static string printList<T>(List<T> list)
        {
            string ans = "";
            foreach (var val in list)
            {
                ans += val + " ";
            }
            return ans;
        }
        static void Main()
        {
            Graph<char> graph = new Graph<char>();
            graph.AddVertex('a');
            graph.AddVertex('b');
            graph.AddVertex('c');
            graph.AddVertex('d');
            graph.AddVertex('e');
            graph.AddEdge('a', 'd');
            graph.AddEdge('a', 'b');
            graph.AddEdge('a', 'c');
            graph.AddEdge('b', 'd');
            graph.AddEdge('d', 'c');
            graph.AddEdge('d', 'e');
            Console.WriteLine("DFSPath: " + printList(graph.DFSPath('a', 'e')));
            Console.WriteLine("BFSPath: " + printList(graph.BFSPath('a', 'e')));
            Console.WriteLine("Dijkstra: " + printList(graph.Dijkstra('a', 'e')));
        }
    }
}