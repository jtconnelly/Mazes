#pragma once

#include "igraph.h"
namespace Boots
{
  template <typename T>
  class Graph: public iGraph<T>
  {
    public:
      virtual void addVertex(T vert) override;
      virtual void addEdge(T to, T from) override;
      virtual void removeEdge(T to, T from) override;
      virtual bool hasEdge(T to, T from) const override;
  };
}
