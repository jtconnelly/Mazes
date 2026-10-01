#pragma once

#include "igraph.h"
#include "graph.h"

namespace Boots
{
    template <typename T>
    class Digraph : public Graph<T>, public iGraph<T>
    {
        public:
        Digraph() = default;
        virtual void addEdge(T to, T from) override;
        virtual void removeEdge(T to, T from) override;
    };
}
