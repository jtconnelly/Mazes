#pragma once
#include <concepts>
#include <vector>
#include <functional>

#include "igraph.h"

namespace Boots
{
    template<typename T>
    concept ImplementsGraph = std::derived_from<T, GraphTag>;

    template <typename T>
    concept ImplementsWeightedGraph = std::derived_from<T, WeightedGraphTag>;

    template<typename T>
    concept DerivesFromGraph = ImplementsWeightedGraph<T> || ImplementsGraph<T>;

    template <typename T, DerivesFromGraph G>
    static const std::vector<T> dfs(const G&, const T&);
    template <typename T, DerivesFromGraph G>
    static const std::vector<T> bfs(const G&, const T&);
    template <typename T, DerivesFromGraph G>
    static const std::vector<T> dfsPath(const G& graph, const T& start, const T& end);
    template <typename T, DerivesFromGraph G>
    static const std::vector<T> bfsPath(const G& graph, const T& start, const T& end);

    template <typename T, typename Weight>
    static const std::vector<T> dijkstra(const iWeightedGraph<T, Weight>& graph, const T& start, const T& end);
    template <typename T, typename Weight>
    static const std::vector<T> aStar(const iWeightedGraph<T, Weight>& graph, const T& start, const T& end, const std::function<double(const T&, const T&)>& heuristic);
    template <typename T, typename Weight>
    static const std::vector<T> bellmanFord(const iWeightedGraph<T, Weight>& graph, const T& start, const T& end);
    template <typename T, typename Weight>
    static const std::vector<T> floydWarshall(const iWeightedGraph<T, Weight>& graph, const T& start, const T& end);

}
