#include "weighted_graph.h"

namespace Boots
{
    /*
     * ---
     *  Begin WeightedGraph 
     *  ---
     */
    template <typename T, typename Weight>
    void WeightedGraph<T, Weight>::addVertex(T vert)
    {
        if (!findNode(vert))
        {
            m_graph.push_back(new GraphNode{vert, {}});
        }
    }

    template <typename T, typename Weight>
    void WeightedGraph<T, Weight>::addEdge(T to, T from, Weight weight)
    {
        auto toNode = findNode(to);
        auto fromNode = findNode(from);
        if (!toNode || ! fromNode) return;
        toNode->neighbors.push_back(std::make_pair(fromNode, weight));
        fromNode->neighbors.push_back(std::make_pair(toNode, weight));
    }

    template <typename T, typename Weight>
    void WeightedGraph<T, Weight>::removeEdge(T to, T from)
    {
        auto toNode = findNode(to);
        if (!toNode) return;
        auto it = std::find_if(toNode->neighbors.begin(), toNode->neighbors.end(),
                [from](const std::pair<GraphNode*, Weight>& n){return n.first->val == from;});
        if (it != toNode->neighbors.end())
        {
            *it->neighbors.erase(std::remove_if(it->neighbors.begin(), it->neighbors.end(),
                    [to](const std::pair<GraphNode*, Weight>& n){return n.first->val == to;}), it->neighbors.end());
            toNode->neighbors.erase(it);
        }
    }

    template <typename T, typename Weight>
    bool WeightedGraph<T, Weight>::hasEdge(T to, T from) const
    {
        auto toNode = findNode(to);
        if (!toNode) return false;
        auto it = std::find_if(toNode->neighbors.begin(), toNode->neighbors.end(),
                [from](const std::pair<GraphNode*, Weight>& n){return n.first->val == from;});
        return it != toNode->neighbors.end();
    }

    template <typename T, typename Weight>
    std::vector<std::pair<T, Weight>> WeightedGraph<T, Weight>::getNeighbors(T v) const
    {
        auto node = findNode(v);
        return node ? std::vector<T>(node->neighbors.begin(), node->neighbors.end(), [](const std::pair<GraphNode*, Weight>& n){return n.first->val;}) : std::vector<T>();
    }

    /*
     * ---
     *  Begin NonNegativeWeightedGraph 
     *  ---
     */

    template <typename T, typename Weight>
    void NonNegativeWeightedGraph<T, Weight>::addVertex(T vert)
    {
        if (!findNode(vert))
        {
            m_graph.push_back(new GraphNode{vert, {}});
        }
    }

    template <typename T, typename Weight>
    void NonNegativeWeightedGraph<T, Weight>::addEdge(T to, T from, Weight weight)
    {
        if (weight < 0) return;
        auto toNode = findNode(to);
        auto fromNode = findNode(from);
        if (!toNode || !fromNode) return;
        toNode->neighbors.push_back(std::make_pair(fromNode, weight));
        fromNode->neighbors.push_back(std::make_pair(toNode, weight));
    }

    template <typename T, typename Weight>
    void NonNegativeWeightedGraph<T, Weight>::removeEdge(T to, T from)
    {
        auto toNode = findNode(to);
        if (!toNode) return;
        auto it = std::find_if(toNode->neighbors.begin(), toNode->neighbors.end(),
                [from](const std::pair<GraphNode*, Weight>& n){return n.first->val == from;});
        if (it != toNode->neighbors.end())
        {
            *it->neighbors.erase(std::remove_if(it->neighbors.begin(), it->neighbors.end(),
                    [to](const std::pair<GraphNode*, Weight>& n){return n.first->val == to;}), it->neighbors.end());
            toNode->neighbors.erase(it);
        }
    }

    template <typename T, typename Weight>
    bool NonNegativeWeightedGraph<T, Weight>::hasEdge(T to, T from) const
    {
        auto toNode = findNode(to);
        if (!toNode) return false;
        auto it = std::find_if(toNode->neighbors.begin(), toNode->neighbors.end(),
                [from](const std::pair<GraphNode*, Weight>& n){return n.first->val == from;});
        return it != toNode->neighbors.end();
    }

    template <typename T, typename Weight>
    std::vector<std::pair<T, Weight>> NonNegativeWeightedGraph<T, Weight>::getNeighbors(T v) const
    {
        auto node = findNode(v);
        return node ? std::vector<T>(node->neighbors.begin(), node->neighbors.end(), [](const std::pair<GraphNode*, Weight>& n){return n.first->val;}) : std::vector<T>();
    }
}
