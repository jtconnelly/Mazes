#include "graph.h"
#include <algorithm>

namespace Boots
{
    template <typename T>
    Graph<T>::Graph()
    {

    }

    template <typename T>
    void Graph<T>::addVertex(T vert)
    {
        if (!findNode(vert))
        {
            m_graph.push_back(new GraphNode{vert, {}});
        }
    }

    template <typename T>
    void Graph<T>::addEdge(T to, T from)
    {
        auto toNode = findNode(to);
        auto fromNode = findNode(from);
        if (!toNode || !fromNode) return;
        toNode->neighbors.push_back(fromNode);
        fromNode->neighbors.push_back(toNode);
    }

    template <typename T>
    void Graph<T>::removeEdge(T to, T from)
    {
        auto toNode = findNode(to);
        if (!toNode) return;
        auto it = std::find_if(toNode->neighbors.begin(), toNode->neighbors.end(),
                [from](GraphNode * n){return n->val == from;});
        if (it != toNode->neighbors.end())
        {
            *it->neighbors.erase(std::remove_if(it->neighbors.begin(), it->neighbors.end(),
                    [to](GraphNode * n){return n->val == to;}), it->neighbors.end());
            toNode->neighbors.erase(it);
        }
    }

    template <typename T>
    bool Graph<T>::hasEdge(T to, T from) const
    {
        auto toNode = findNode(to);
        if (!toNode) return false;
        auto it = std::find_if(toNode->neighbors.begin(), toNode->neighbors.end(),
                [from](GraphNode * n){return n->val == from;});
        return it != toNode->neighbors.end();
    }

    template <typename T>
    std::vector<T> Graph<T>::getNeighbors(T v) const
    {
        auto node = findNode(v);
        return node ? std::vector<T>(node->neighbors.begin(), node->neighbors.end(), [](GraphNode *n){return n->val;}) : std::vector<T>();
    }
}
