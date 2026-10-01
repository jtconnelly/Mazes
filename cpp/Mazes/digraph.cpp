#include "digraph.h"

namespace Boots
{
    template <typename T>
    void Digraph<T>::addEdge(T to, T from)
    {
        auto toNode = findNode(to);
        auto fromNode = findNode(from);
        if (!toNode || !fromNode) return;
        toNode->neighbors.push_back(fromNode);
    }

    template <typename T>
    void Digraph<T>::removeEdge(T to, T from) 
    {
        auto toNode = findNode(to);
        if (!toNode) return;
        auto it = std::find_if(toNode->neighbors.begin(), toNode->neighbors.end(),
                [from](GraphNode * n){return n->val == from});
        if (it != toNode->neighbors.end())
        {
            toNode->neighbors.erase(it);
        }
    }
}