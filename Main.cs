
using System.Diagnostics;

namespace Graph
{


    /***********************************************************
     * a wrapper class for our main function to test our graphs and algorithms.
     * @see Graph
     * @see SearchAlgorithmExtensions
     ***********************************************************/
    class GraphsMain
    {     
        /**
         * Prints each item of a list in a simple for loop
         */
        internal static string printList<T>(List<T> list)
        {
            string ans = "";
            foreach (var val in list)
            {
                ans += val + " ";
            }
            return ans;
        }

        /**
         * Function for testing the base graph implementation and given search functions
         * Search Functions Tested: DFSPath, BFSPath, Dijkstra
         */
        internal static void TestBaseGraph()
        {
            Console.WriteLine("Regular Graph Implementation:");
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
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            var path = graph.DFSPath('a', 'e');
            stopwatch.Stop();
            Console.WriteLine("\tDFSPath: ");
            Console.WriteLine("\t\tTime: " + stopwatch);
            Console.WriteLine("\t\tPath: " + printList(path));

            stopwatch.Restart();
            path = graph.BFSPath('a', 'e');
            stopwatch.Stop();
            Console.WriteLine("\tBFSPath: ");
            Console.WriteLine("\t\tTime: " + stopwatch);
            Console.WriteLine("\t\tPath: " + printList(path));

            stopwatch.Restart();
            path = graph.Dijkstra('a', 'e');
            stopwatch.Stop();
            Console.WriteLine("\tDijkstra: ");
            Console.WriteLine("\t\tTime: " + stopwatch);
            Console.WriteLine("\t\tPath: " + printList(path));
        }
        static void Main()
        {
            TestBaseGraph();
        }
    }
}