use std::collections::HashMap;
use std::hash::Hash;

pub trait Graphing<T>
{
    fn add_vertex(&mut self, vert: &T);
    fn remove_vertex(&mut self, vert: &T);
    fn add_edge(&mut self, a: &T, b: &T);
    fn remove_edge(&mut self, a: &T, b: &T);
    fn get_neighbors(&self, v: &T) -> Option<&Vec<T>>;
}

pub struct Graph<T>
{
    pub(crate) neighbors: HashMap<T, Vec<T>>
}

impl<T> Graph<T>{
    pub fn new() -> Graph<T>{
        Graph{neighbors: HashMap::new()}
    }
}

impl<T: Eq + Hash + Clone> Graphing<T> for Graph<T>{
    fn add_vertex(&mut self, vert: &T){
        if !self.neighbors.contains_key(&vert)
        {
            self.neighbors.insert(vert.clone(), Vec::new());
        }
    }

    fn remove_vertex(&mut self, vert: &T){
        self.neighbors.remove(vert);
    }

    fn add_edge(&mut self, a: &T, b: &T){
        if self.neighbors.contains_key(&a) && self.neighbors.contains_key(&b)
        {
            if !self.neighbors[&a].contains(&b) && !self.neighbors[&b].contains(&a)
            {
                self.neighbors.entry(a.to_owned()).and_modify(|l| l.push(b.to_owned()));
                self.neighbors.entry(b.to_owned()).and_modify(|l| l.push(a.to_owned()));
            }
        }
    }

    fn remove_edge(&mut self, a: &T, b: &T){
        if self.neighbors.contains_key(&a) && self.neighbors[&a].contains(&b){
            let idx = self.neighbors.get_mut(&a).and_then(|l| l.iter().position(|v| *v == *b).clone()).unwrap();
            self.neighbors.entry(a.clone()).and_modify(|l| { l.remove(idx); });
        }
        if self.neighbors.contains_key(&b) && self.neighbors[&b].contains(&a){
            let idx = self.neighbors.get_mut(&b).and_then(|l| return l.iter().position(|v| *v == *a).clone()).unwrap();
            self.neighbors.entry(b.clone()).and_modify(|l| { l.remove(idx); });
        }
    }

    fn get_neighbors(&self, v: &T) -> Option<&Vec<T>>{
        self.neighbors.get(&v)
    }
}

#[cfg(test)]
mod tests{
    use super::*;

    #[test]
    fn test_graph_add_vertex(){
        let mut graph = Graph::<char>::new();
        graph.add_vertex(&'a');
        graph.add_vertex(&'b');
        graph.add_vertex(&'c');
        graph.add_vertex(&'d');
        graph.add_vertex(&'a');
        assert!(true, "add_vertex failed!");
    }

    #[test]
    fn test_graph_remove_vertex(){
        let mut graph = Graph::<char>::new();
        graph.add_vertex(&'a');
        graph.add_vertex(&'b');
        graph.remove_vertex(&'b');
        graph.remove_vertex(&'z');
        assert!(true, "remove_vertex failed!");
    }

    #[test]
    fn test_graph_add_edge(){
        let mut graph = Graph::<char>::new();
        graph.add_vertex(&'a');
        graph.add_vertex(&'b');
        graph.add_edge(&'a', &'b');
        graph.add_edge(&'a', &'b');
        graph.add_edge(&'x', &'y');
        assert!(true, "add_edge failed!");
    }

    #[test]
    fn test_graph_remove_edge(){
        let mut graph = Graph::<char>::new();
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
        let mut graph = Graph::<char>::new();
        graph.add_vertex(&'a');
        graph.add_vertex(&'b');
        graph.add_vertex(&'c');
        graph.add_vertex(&'d');
        graph.add_vertex(&'a');
        graph.add_edge(&'a', &'b');
        graph.add_edge(&'a', &'c');
        graph.add_edge(&'a', &'d');
        graph.add_edge(&'a', &'b');
        assert_eq!(*graph.get_neighbors(&'a').unwrap(), vec!['b', 'c', 'd']);
        graph.remove_edge(&'a', &'b');
        assert_eq!(*graph.get_neighbors(&'a').unwrap(), vec!['c', 'd']);
        assert!(true, "get_neighbors failed!");
    }
}