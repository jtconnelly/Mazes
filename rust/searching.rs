use crate::graph::{Graph, Graphing};
use std::hash::Hash;
pub trait SearchAlgorithm<T: Clone + Eq + Hash>: Graphing<T>
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
                for neighbor in self.get_neighbors(node).unwrap()
                {
                    stack.push(neighbor);
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
                for neighbor in self.get_neighbors(node).unwrap()
                {
                    stack.push(neighbor);
                }
            }
        }

        return Vec::new();
    }
}

impl<T: Clone + Eq + Hash> SearchAlgorithm<T> for Graph<T>{}

pub trait GreedySearch<T>
{
    fn dijkstra(&self) -> Vec<T>;
    fn bellman_ford(&self) -> Vec<T>;
    fn floyd_warshall(&self) -> Vec<T>;
    fn a_star(&self) -> Vec<T>;
}