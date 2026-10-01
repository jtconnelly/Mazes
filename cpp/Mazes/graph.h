#pragma once

#include "igraph.h"

#include <algorithm>
#include <vector>

namespace Boots
{
  template <typename T>
  class Graph: public iGraph<T>
  {
    public:
        Graph();
        virtual void addVertex(T vert) override;
        virtual void addEdge(T to, T from) override;
        virtual void removeEdge(T to, T from) override;
        virtual bool hasEdge(T to, T from) const override;
        virtual std::vector<T> getNeighbors(T v) const override;
    // Marking as protected for inheritance
    protected:
        using typename iGraph<T>::GraphNode;
        std::vector<GraphNode*> m_graph;
        GraphNode *findNode(T val) const
        {
            auto it = std::find_if(m_graph.begin(), m_graph.end(),
                    [val](GraphNode * d){return d->val == val;});
            return it == m_graph.end() ? nullptr : *it;
        }
  };
}
