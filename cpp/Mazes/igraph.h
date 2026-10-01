#pragma once

#include <vector>
#include <utility>

namespace Boots
{
    template <typename T>
    class iGraph
    {
        public:
            virtual ~iGraph() = default;
            virtual void addVertex(T vert) = 0;
            virtual void addEdge(T to, T from) = 0;
            virtual void removeEdge(T to, T from) = 0;
            virtual bool hasEdge(T to, T from) const = 0;
            virtual std::vector<T> getNeighbors(T v) const = 0;
            struct GraphNode
            {
                T val;
                std::vector<GraphNode*> neighbors;
            };
    };

    template <typename T, typename Weight=size_t>
    class iWeightedGraph
    {
        public:
            virtual ~iWeightedGraph() = default;
            virtual void addVertex(T vert) = 0;
            virtual void addEdge(T to, T from, Weight weight) = 0;
            virtual void removeEdge(T to, T from) = 0;
            virtual bool hasEdge(T to, T from) const = 0;
            virtual std::vector<std::pair<T, Weight>> getNeighbors(T v) const = 0;
            struct GraphNode
            {
                T val;
                std::vector<std::pair<GraphNode*, Weight>> neighbors;
            };
    };
}
