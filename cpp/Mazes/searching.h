#include <vector>
#include <functional>

namespace Boots
{
    template <typename T>
    class iGraph;

    template <typename T>
    class iWeightedGraph;

    template <typename T>
    class GraphSearch
    {
        public:
            ~GraphSearch() = default;
            static const std::vector<T> dfs(const iGraph<T>&, const T&);
            static const std::vector<T> bfs(const iGraph<T>&, const T&);
            static const std::vector<T> dfsPath(const iGraph<T>& graph, const T& start, const T& end);
            static const std::vector<T> bfsPath(const iGraph<T>& graph, const T& start, const T& end);
            static const std::vector<T> dijkstra(const iWeightedGraph<T>& graph, const T& start, const T& end);
            static const std::vector<T> aStar(const iWeightedGraph<T>& graph, const T& start, const T& end, const std::function<double(const T&, const T&)>& heuristic);
            static const std::vector<T> bellmanFord(const iWeightedGraph<T>& graph, const T& start, const T& end);
            static const std::vector<T> floydWarshall(const iWeightedGraph<T>& graph, const T& start, const T& end);
    };

}