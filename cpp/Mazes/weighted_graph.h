#pragma once
#include "igraph.h"

#include <cstddef>
#include <vector>
#include <algorithm>

namespace Boots
{
    template<typename T, typename Weight = size_t>
    class WeightedGraph: public iWeightedGraph<T, Weight>
    {
        virtual void addVertex(T val) override;
        virtual void addEdge(T to, T from, Weight weight) override;
        virtual void removeEdge(T to, T from) override;
        virtual bool hasEdge(T to, T from) const override;
        virtual std::vector<std::pair<T, Weight>> getNeighbors(T v) const override;
    // Marking as protected for inheritance
    protected:
        using typename iWeightedGraph<T, Weight>::GraphNode;
        std::vector<GraphNode*> m_graph;
        GraphNode *findNode(T val) const
        {
            auto it = std::find_if(m_graph.begin(), m_graph.end(),
                    [val](GraphNode * d){return d->val == val;});
            return it == m_graph.end() ? nullptr : *it;
        }
    };

    template<typename T, typename Weight = size_t>
    class NonNegativeWeightedGraph: public iWeightedGraph<T, Weight>
    {
        virtual void addVertex(T val) override;
        virtual void addEdge(T to, T from, Weight weight) override;
        virtual void removeEdge(T to, T from) override;
        virtual bool hasEdge(T to, T from) const override;
        virtual std::vector<std::pair<T, Weight>> getNeighbors(T v) const override;
    // Marking as protected for inheritance
    protected:
        using typename iWeightedGraph<T, Weight>::GraphNode;
        std::vector<GraphNode*> m_graph;
        GraphNode *findNode(T val) const
        {
            auto it = std::find_if(m_graph.begin(), m_graph.end(),
                    [val](GraphNode * d){return d->val == val;});
            return it == m_graph.end() ? nullptr : *it;
        }

    };
}
