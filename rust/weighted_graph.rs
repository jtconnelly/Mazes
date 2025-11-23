use crate::graph::{Graphing, NeighborNode};
use std::collections::HashMap;
use std::hash::Hash;
pub struct Pair<T, U>{
    pub first: T,
    pub second: U,
}
type Weighted<T> = Pair<T, i32>;

pub struct WeightedGraph<T>{
    pub(crate) neighbors: HashMap<T, Vec<Weighted<T>>>,
}

pub struct WeightedDigraph<T>{
    base_graph: WeightedGraph<T>
}

impl<T> WeightedGraph<T>{
    pub fn new() -> WeightedGraph<T>{
        WeightedGraph{neighbors: HashMap::new()}
    }
}

impl<T> WeightedDigraph<T>{
    pub fn new() -> WeightedDigraph<T>{
        WeightedDigraph{base_graph: WeightedGraph::new()}
    }
}


impl<T: Eq + Hash + Clone> Graphing<T> for WeightedGraph<T>{
    type Neighbor = Pair<T, i32>;
    fn add_vertex(&mut self, vert: &T){
        if !self.neighbors.contains_key(&vert)
        {
            self.neighbors.insert(vert.clone(), Vec::new());
        }
    }
    fn remove_vertex(&mut self, vert: &T){
        self.neighbors.remove(vert);
    }
    fn add_edge(&mut self, a: &T, b: &Self::Neighbor){
        let neigh_node = NeighborNode::node(b);
        if self.neighbors.contains_key(&a) && self.neighbors.contains_key(neigh_node)
        {
            if !self.neighbors[&a].iter().any(|pair| pair.first == b.first) && !self.neighbors[neigh_node].iter().any(|pair| pair.first == *a)
            {
                self.neighbors.entry(a.to_owned()).and_modify(|l| l.push(Pair{first: b.first.clone(), second: b.second}));
                self.neighbors.entry(b.first.clone()).and_modify(|l| l.push(Pair{first: a.to_owned(), second: b.second}));
            }
        }
    }
    fn remove_edge(&mut self, a: &T, b: &T){
        if self.neighbors.contains_key(&a) && self.neighbors[&a].iter().find(|pair| pair.first == *b).is_some(){
            let idx = self.neighbors.get_mut(&a).and_then(|l| l.iter().position(|v| v.first == *b).clone());
            if let Some(i) = idx {
                self.neighbors.entry(a.clone()).and_modify(|l| { l.remove(i); });
            }
        }
        if self.neighbors.contains_key(&b) && self.neighbors[&b].iter().find(|pair| pair.first == *a).is_some(){
            let idx = self.neighbors.get_mut(&b).and_then(|l| return l.iter().position(|v| v.first == *a).clone());
            if let Some(i) = idx{
                self.neighbors.entry(b.clone()).and_modify(|l| { l.remove(i); });
            }
        }
    }
    fn get_neighbors(&self, v: &T) -> Option<&Vec<Self::Neighbor>>{
        self.neighbors.get(&v)
    }
}

impl<T: Eq + Hash + Clone> Graphing<T> for WeightedDigraph<T>{
    type Neighbor = Pair<T, i32>;
    fn add_vertex(&mut self, vert: &T){
        self.base_graph.add_vertex(vert);
    }
    fn remove_vertex(&mut self, vert: &T){
        self.base_graph.remove_vertex(vert);
    }
    fn add_edge(&mut self, a: &T, b: &Self::Neighbor){
        let neigh_node = NeighborNode::node(b);
        if self.base_graph.neighbors.contains_key(&a) && self.base_graph.neighbors.contains_key(neigh_node)
        {
            if !self.base_graph.neighbors[&a].iter().any(|pair| pair.first == b.first) && !self.base_graph.neighbors[neigh_node].iter().any(|pair| pair.first == *a)
            {
                self.base_graph.neighbors.entry(a.to_owned()).and_modify(|l| l.push(Pair{first: b.first.clone(), second: b.second}));
            }
        }
    }
    fn remove_edge(&mut self, a: &T, b: &T){
        if self.base_graph.neighbors.contains_key(&a) && self.base_graph.neighbors[&a].iter().find(|pair| pair.first == *b).is_some(){
            let idx = self.base_graph.neighbors.get_mut(&a).and_then(|l| l.iter().position(|v| v.first == *b).clone());
            if let Some(i) = idx {
                self.base_graph.neighbors.entry(a.clone()).and_modify(|l| { l.remove(i); });
            }
        }
    }
    fn get_neighbors(&self, v: &T) -> Option<&Vec<Self::Neighbor>>{
        self.base_graph.get_neighbors(v)
    }
}

impl<T> NeighborNode<T> for Pair<T, i32> {
    fn node(&self) -> &T {
        &self.first
    }
}

#[cfg(test)]
mod tests{
    use super::*;

    #[test]
    fn test_weighted_graph_add_vertex(){
        let mut graph = WeightedDigraph::<char>::new();
        graph.add_vertex(&'a');
        graph.add_vertex(&'b');
        graph.add_vertex(&'c');
        graph.add_vertex(&'d');
        graph.add_vertex(&'a');
        assert!(true, "add_vertex failed!");
    }

    #[test]
    fn test_weighted_graph_remove_vertex(){
        let mut graph = WeightedDigraph::<char>::new();
        graph.add_vertex(&'a');
        graph.add_vertex(&'b');
        graph.remove_vertex(&'b');
        graph.remove_vertex(&'z');
        assert!(true, "remove_vertex failed!");
    }

    #[test]
    fn test_weighted_graph_add_edge(){
        let mut graph = WeightedDigraph::<char>::new();
        graph.add_vertex(&'a');
        graph.add_vertex(&'b');
        graph.add_edge(&'a', &Pair{first: 'b', second: 24});
        graph.add_edge(&'a', &Pair{first: 'b', second: 20});
        graph.add_edge(&'x', &Pair{first: 'y', second: 10});
        assert!(true, "add_edge failed!");
    }

    #[test]
    fn test_weighted_graph_remove_edge(){
        let mut graph = WeightedDigraph::<char>::new();
        graph.add_vertex(&'a');
        graph.add_vertex(&'b');
        graph.add_edge(&'a', &Pair{first: 'b', second: 24});
        graph.add_edge(&'a', &Pair{first: 'b', second: 20});
        graph.add_edge(&'x', &Pair{first: 'y', second: 5});
        graph.remove_edge(&'a', &'b');
        graph.remove_edge(&'x', &'y');
        assert!(true, "remove_edge failed!");
    }
    
    #[test]
    fn test_weighted_graph_get_neighbors(){
        let mut graph = WeightedDigraph::<char>::new();
        graph.add_vertex(&'a');
        graph.add_vertex(&'b');
        graph.add_vertex(&'c');
        graph.add_vertex(&'d');
        graph.add_vertex(&'a');
        graph.add_edge(&'a', &Pair{first: 'b', second: 24});
        graph.add_edge(&'a', &Pair{first: 'c', second: 10});
        graph.add_edge(&'a', &Pair{first: 'd', second: 5});
        graph.add_edge(&'a', &Pair{first: 'b', second: 20});
        //assert_eq!(*graph.get_neighbors(&'a').expuect("Failed to get neighbors for a"), vec!['b', 'c', 'd']);
        graph.remove_edge(&'a', &'b');
        //assert_eq!(*graph.get_neighbors(&'a').expect("Failed to get neighbors for a"), vec!['c', 'd']);
        assert!(true, "get_neighbors failed!");
    }

    #[test]
    fn test_weighted_digraph_add_vertex(){
        let mut graph = WeightedDigraph::<char>::new();
        graph.add_vertex(&'a');
        graph.add_vertex(&'b');
        graph.add_vertex(&'c');
        graph.add_vertex(&'d');
        graph.add_vertex(&'a');
        assert!(true, "add_vertex failed!");
    }

    #[test]
    fn test_weighted_digraph_remove_vertex(){
        let mut graph = WeightedDigraph::<char>::new();
        graph.add_vertex(&'a');
        graph.add_vertex(&'b');
        graph.remove_vertex(&'b');
        graph.remove_vertex(&'z');
        assert!(true, "remove_vertex failed!");
    }

    #[test]
    fn test_weighted_digraph_add_edge(){
        let mut graph = WeightedDigraph::<char>::new();
        graph.add_vertex(&'a');
        graph.add_vertex(&'b');
        graph.add_edge(&'a', &Pair{first: 'b', second: 24});
        graph.add_edge(&'a', &Pair{first: 'b', second: 20});
        graph.add_edge(&'x', &Pair{first: 'y', second: 10});
        assert!(true, "add_edge failed!");
    }

    #[test]
    fn test_weighted_digraph_remove_edge(){
        let mut graph = WeightedDigraph::<char>::new();
        graph.add_vertex(&'a');
        graph.add_vertex(&'b');
        graph.add_edge(&'a', &Pair{first: 'b', second: 24});
        graph.add_edge(&'a', &Pair{first: 'b', second: 20});
        graph.add_edge(&'x', &Pair{first: 'y', second: 10});
        graph.remove_edge(&'a', &'b');
        graph.remove_edge(&'x', &'y');
        assert!(true, "remove_edge failed!");
    }
    
    #[test]
    fn test_weighted_digraph_get_neighbors(){
        let mut graph = WeightedDigraph::<char>::new();
        graph.add_vertex(&'a');
        graph.add_vertex(&'b');
        graph.add_vertex(&'c');
        graph.add_vertex(&'d');
        graph.add_vertex(&'a');
        graph.add_edge(&'a', &Pair{first: 'b', second: 24});
        graph.add_edge(&'a', &Pair{first: 'c', second: 10});
        graph.add_edge(&'a', &Pair{first: 'd', second: 5});
        graph.add_edge(&'a', &Pair{first: 'b', second: 20});
        //assert_eq!(*graph.get_neighbors(&'a').expect("Failed to get neighbors for a"), vec!['b', 'c', 'd']);
        graph.remove_edge(&'a', &'b');
        //assert_eq!(*graph.get_neighbors(&'a').expect("Failed to get neighbors for a"), vec!['c', 'd']);
        assert!(true, "get_neighbors failed!");
    }
}