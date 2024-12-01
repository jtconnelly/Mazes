
namespace Mazes
{
    /***********************************************************
     * a wrapper class for our main function to test our graphs and algorithms.
     * @see Graph
     * @see SearchAlgorithmExtensions
     ***********************************************************/
    class GraphsMain
    { 
        static void Main()
        {
            Graph<char> graph = new Graph<char>();
            graph.AddVertex('a');
            graph.AddVertex('b');
            graph.AddVertex('c');
            graph.AddVertex('d');
        }
    }
}