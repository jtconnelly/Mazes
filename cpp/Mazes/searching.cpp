#include "searching.h"

namespace Boots
{
    template <typename T>
    const std::vector<T> GraphSearch<T>::dfs(const iGraph<T>& graph, const T& start)
    {
        std::vector<T> visited;
        std::vector<T> stack;
        stack.push_back(start);

        while (!stack.empty())
        {
            T node = stack.back();
            stack.pop_back();

            if (std::find(visited.begin(), visited.end(), node) == visited.end())
            {
                visited.push_back(node);
                for (const auto& neighbor : graph.getNeighbors(node))
                {
                    stack.push_back(neighbor);
                }
            }
        }

        return visited;
    }
    
    template <typename T>
    const std::vector<T> GraphSearch<T>::bfs(const iGraph<T>& graph, const T& start)
    {
        std::vector<T> visited;
        std::vector<T> queue;
        queue.push_back(start);

        while (!queue.empty())
        {
            T node = queue.front();
            queue.erase(queue.begin());

            if (std::find(visited.begin(), visited.end(), node) == visited.end())
            {
                visited.push_back(node);
                for (const auto& neighbor : graph.getNeighbors(node))
                {
                    queue.push_back(neighbor);
                }
            }
        }

        return visited;
    }

    template <typename T>
    const std::vector<T> GraphSearch<T>::dfsPath(const iGraph<T>& graph, const T& start, const T& end)
    {
        std::vector<T> path;
        std::vector<T> stack;
        stack.push_back(start);

        while (!stack.empty())
        {
            T node = stack.back();
            stack.pop_back();

            if (node == end)
            {
                path.push_back(node);
                return path;
            }

            if (std::find(path.begin(), path.end(), node) == path.end())
            {
                path.push_back(node);
                for (const auto& neighbor : graph.getNeighbors(node))
                {
                    stack.push_back(neighbor);
                }
            }
        }

        return {};
    }

    template <typename T>
    const std::vector<T> GraphSearch<T>::bfsPath(const iGraph<T>& graph, const T& start, const T& end)
    {
        std::vector<T> path;
        std::vector<T> queue;
        queue.push_back(start);

        while (!queue.empty())
        {
            T node = queue.front();
            queue.erase(queue.begin());

            if (node == end)
            {
                path.push_back(node);
                return path;
            }

            if (std::find(path.begin(), path.end(), node) == path.end())
            {
                path.push_back(node);
                for (const auto& neighbor : graph.getNeighbors(node))
                {
                    queue.push_back(neighbor);
                }
            }
        }

        return {};
    }

    template <typename T>
    const std::vector<T> GraphSearch<T>::dijkstra(const iWeightedGraph<T>& graph, const T& start, const T& end)
    {
        std::vector<T> path;
        std::vector<T> queue;
        queue.push_back(start);

        while (!queue.empty())
        {
            T node = queue.front();
            queue.erase(queue.begin());

            if (node == end)
            {
                path.push_back(node);
                return path;
            }

            if (std::find(path.begin(), path.end(), node) == path.end())
            {
                path.push_back(node);
                for (const auto& neighbor : graph.getNeighbors(node))
                {
                    queue.push_back(neighbor);
                }
            }
        }

        return {};
    }

    template <typename T>
    const std::vector<T> GraphSearch<T>::aStar(const iWeightedGraph<T>& graph, const T& start, const T& end, const std::function<double(const T&, const T&)>& heuristic)
    {
        std::vector<T> path;
        std::vector<T> queue;
        queue.push_back(start);

        while (!queue.empty())
        {
            T node = queue.front();
            queue.erase(queue.begin());

            if (node == end)
            {
                path.push_back(node);
                return path;
            }

            if (std::find(path.begin(), path.end(), node) == path.end())
            {
                path.push_back(node);
                for (const auto& neighbor : graph.getNeighbors(node))
                {
                    queue.push_back(neighbor);
                }
            }
        }

        return {};
    }

    template <typename T>
    const std::vector<T> GraphSearch<T>::bellmanFord(const iWeightedGraph<T>& graph, const T& start, const T& end)
    {
        std::vector<T> path;
        std::vector<T> queue;
        queue.push_back(start);

        while (!queue.empty())
        {
            T node = queue.front();
            queue.erase(queue.begin());

            if (node == end)
            {
                path.push_back(node);
                return path;
            }

            if (std::find(path.begin(), path.end(), node) == path.end())
            {
                path.push_back(node);
                for (const auto& neighbor : graph.getNeighbors(node))
                {
                    queue.push_back(neighbor);
                }
            }
        }

        return {};
    }

    template <typename T>
    const std::vector<T> GraphSearch<T>::floydWarshall(const iWeightedGraph<T>& graph, const T& start, const T& end)
    {
        std::vector<T> path;
        std::vector<T> queue;
        queue.push_back(start);

        while (!queue.empty())
        {
            T node = queue.front();
            queue.erase(queue.begin());

            if (node == end)
            {
                path.push_back(node);
                return path;
            }

            if (std::find(path.begin(), path.end(), node) == path.end())
            {
                path.push_back(node);
                for (const auto& neighbor : graph.getNeighbors(node))
                {
                    queue.push_back(neighbor);
                }
            }
        }

        return {};
    }
}