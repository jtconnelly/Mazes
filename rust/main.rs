use crate::graph::{Graph, Graphing};

mod graph;

fn test_graph(){
    let mut graph = Graph::<char>::new();
    graph.add_vertex('a');
    graph.add_vertex('b');
    graph.add_vertex('c');
    graph.add_vertex('d');
    graph.add_vertex('a');
    graph.add_vertex('x');
    graph.remove_vertex('x');
    graph.add_edge('a', 'b');
    graph.add_edge('a', 'c');
    graph.add_edge('a', 'd');
    graph.add_edge('a', 'b');
    assert_eq!(*graph.get_neighbors('a').unwrap(), vec!['b', 'c', 'd']);
    graph.remove_edge('a', 'b');
    assert_eq!(*graph.get_neighbors('a').unwrap(), vec!['c', 'd']);
    assert!(true, "get_neighbors failed!");
}

fn main() {
    test_graph();
}
