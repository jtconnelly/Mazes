#![deny(clippy::redundant_clone)]
#![deny(clippy::unwrap_used)]

use crate::graph::{Graph, Graphing, NeighborNode};
use crate::digraph::Digraph;
use crate::weighted_graph::{WeightedDigraph, WeightedGraph};
use std::hash::Hash;
pub trait SearchAlgorithm<T: Clone + Eq + Hash>: Graphing<T>
where
    <Self as Graphing<T>>::Neighbor: NeighborNode<T>,
{
    fn bfs(&self, start: &T, end: &T) -> Vec<T>{
        let mut path: Vec<T> = Vec::new();
        let mut stack: Vec<&T> = Vec::new();
        stack.push(start);

        while !stack.is_empty()
        {
            let node = stack.remove(0);

            if node == end
            {
                path.push(node.clone());
                return path;
            }

            if path.iter().find(|&x| *x == *node).is_none()
            {
                path.push(node.clone());
                if let Some(neighbors) = self.get_neighbors(node)
                {
                    for neighbor in neighbors {
                        let neigh_node: &T = NeighborNode::node(neighbor);
                        stack.push(neigh_node);
                    }
                }
            }
        }

        return Vec::new();
    }

    fn dfs(&self, start: &T, end: &T) -> Vec<T>{
        let mut path: Vec<T> = Vec::new();
        let mut stack: Vec<&T> = Vec::new();
        stack.push(start);

        while !stack.is_empty()
        {
            let node = stack.pop().unwrap();

            if node == end
            {
                path.push(node.clone());
                return path;
            }

            if path.iter().find(|&x| *x == *node).is_none()
            {
                path.push(node.clone());
                if let Some(neighbors) = self.get_neighbors(node)
                {
                    for neighbor in neighbors {
                        let neigh_node: &T = NeighborNode::node(neighbor);
                        stack.push(neigh_node);
                    }
                }
            }
        }

        return Vec::new();
    }
}

impl<T: Clone + Eq + Hash> SearchAlgorithm<T> for Graph<T>{}
impl<T: Clone + Eq + Hash> SearchAlgorithm<T> for Digraph<T>{}
impl<T: Clone + Eq + Hash> SearchAlgorithm<T> for WeightedGraph<T>{}
impl<T: Clone + Eq + Hash> SearchAlgorithm<T> for WeightedDigraph<T>{}

pub trait GreedySearch<T>
{
    fn dijkstra(&self, start: &T, end: &T) -> Vec<T>;
    fn bellman_ford(&self, start: &T, end: &T) -> Vec<T>;
    fn floyd_warshall(&self, start: &T, end: &T) -> Vec<T>;
    fn a_star(&self, start: &T, end: &T) -> Vec<T>;
}