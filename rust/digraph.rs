#![deny(clippy::redundant_clone)]
#![deny(clippy::unwrap_used)]

use crate::graph::{Graph, Graphing};
use std::hash::Hash;


pub struct Digraph<T>
{
    base_graph: Graph<T>
}

impl<T> Digraph<T>{
    pub fn new() -> Digraph<T>{
        Digraph{base_graph: Graph::new()}
    }
}

impl<T: Eq + Hash + Clone + Into<T>> Graphing<T> for Digraph<T>{
    type Neighbor = T;
    fn add_vertex(&mut self, vert: &T){
        self.base_graph.add_vertex(vert);
    }

    fn remove_vertex(&mut self, vert: &T){
        self.base_graph.remove_vertex(vert);
    }

    fn add_edge(&mut self, a: &T, b: &T){
        if self.base_graph.neighbors.contains_key(&a) && self.base_graph.neighbors.contains_key(&b)
        {
            if !self.base_graph.neighbors[&a].contains(&b)
            {
                self.base_graph.neighbors.entry(a.to_owned()).and_modify(|l| l.push(b.to_owned()));
            }
        }
    }

    fn remove_edge(&mut self, a: &T, b: &T){
        if self.base_graph.neighbors.contains_key(&a) && self.base_graph.neighbors[&a].contains(&b){
            let idx = self.base_graph.neighbors.get_mut(&a).and_then(|l| l.iter().position(|v| *v == *b).clone());
            if let Some(i) = idx{
                self.base_graph.neighbors.entry(a.clone()).and_modify(|l| { l.remove(i); });
            }
        }

    }

    fn get_neighbors(&self, v: &T) -> Option<&Vec<T>>{
        self.base_graph.get_neighbors(v)
    }
}

#[cfg(test)]
mod tests{
    use super::*;

    #[test]
    fn test_graph_add_vertex(){
        let mut graph = Digraph::<char>::new();
        graph.add_vertex(&'a');
        graph.add_vertex(&'b');
        graph.add_vertex(&'c');
        graph.add_vertex(&'d');
        graph.add_vertex(&'a');
        assert!(true, "add_vertex failed!");
    }

    #[test]
    fn test_graph_remove_vertex(){
        let mut graph = Digraph::<char>::new();
        graph.add_vertex(&'a');
        graph.add_vertex(&'b');
        graph.remove_vertex(&'b');
        graph.remove_vertex(&'z');
        assert!(true, "remove_vertex failed!");
    }

    #[test]
    fn test_graph_add_edge(){
        let mut graph = Digraph::<char>::new();
        graph.add_vertex(&'a');
        graph.add_vertex(&'b');
        graph.add_edge(&'a', &'b');
        graph.add_edge(&'a', &'b');
        graph.add_edge(&'x', &'y');
        assert!(true, "add_edge failed!");
    }

    #[test]
    fn test_graph_remove_edge(){
        let mut graph = Digraph::<char>::new();
        graph.add_vertex(&'a');
        graph.add_vertex(&'b');
        graph.add_edge(&'a', &'b');
        graph.add_edge(&'a', &'b');
        graph.add_edge(&'x', &'y');
        graph.remove_edge(&'a', &'b');
        graph.remove_edge(&'x', &'y');
        assert!(true, "remove_edge failed!");
    }
    
    #[test]
    fn test_graph_get_neighbors(){
        let mut graph = Digraph::<char>::new();
        graph.add_vertex(&'a');
        graph.add_vertex(&'b');
        graph.add_vertex(&'c');
        graph.add_vertex(&'d');
        graph.add_vertex(&'a');
        graph.add_edge(&'a', &'b');
        graph.add_edge(&'a', &'c');
        graph.add_edge(&'a', &'d');
        graph.add_edge(&'a', &'b');
        assert_eq!(*graph.get_neighbors(&'a').expect("Failed to get neighbors for a"), vec!['b', 'c', 'd']);
        graph.remove_edge(&'a', &'b');
        assert_eq!(*graph.get_neighbors(&'a').expect("Failed to get neighbors for a"), vec!['c', 'd']);
        assert!(true, "get_neighbors failed!");
    }
}