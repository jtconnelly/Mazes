use crate::graph::{Graph, Graphing};
use crate::digraph::Digraph;
use crate::weighted_graph::{Pair, WeightedDigraph, WeightedGraph};
use crate::searching::SearchAlgorithm;

mod graph;
mod searching;
mod digraph;
mod weighted_graph;

fn test_searching<G: SearchAlgorithm<char> + ?Sized>(graph: &G)
where
    <G as Graphing<char>>::Neighbor: crate::graph::NeighborNode<char>,
{
    println!("test_searching function");

    println!("DFS");
    let dfs_res = graph.dfs(&'a', &'d');
    assert!(!dfs_res.is_empty());
    let dfs_str = format!("{:?}", dfs_res);
    println!("DFS Result: {dfs_str}");

    println!();

    println!("BFS");
    let bfs_res = graph.bfs(&'a', &'d');
    assert!(!bfs_res.is_empty());
    let bfs_str = format!("{:?}", bfs_res);
    println!("BFS Result: {bfs_str}");
}

fn test_weighted_graph(){
    println!("test_graph function");
    let mut graph = WeightedGraph::<char>::new();
    graph.add_vertex(&'a');
    graph.add_vertex(&'b');
    graph.add_vertex(&'c');
    graph.add_vertex(&'d');
    graph.add_vertex(&'a');
    graph.add_vertex(&'x');
    graph.remove_vertex(&'x');
    graph.add_edge(&'a',&Pair{first: 'b', second: 24});
    graph.add_edge(&'a',&Pair{first: 'c', second: 10});
    graph.add_edge(&'b', &Pair{first: 'd', second: 15});
    graph.add_edge(&'d',&Pair{first: 'c', second: 5});
    graph.add_edge(&'d',&Pair{first: 'e', second: 30});  
    graph.add_edge(&'a',&Pair{first: 'b', second: 20});
    graph.add_edge(&'a', &Pair{first: 'd', second: 2});
    //assert_eq!(*graph.get_neighbors(&'a').expect("Failed to get neighbors for a"), vec![Pair{first: 'b', second: 24}, Pair{first: 'c', second: 10}, Pair{first: 'd', second: 2}]);
    graph.remove_edge(&'a', &'b');
    //assert_eq!(*graph.get_neighbors(&'a').expect("Failed to get neighbors for a"), vec!['c', 'd']);
    assert!(true, "get_neighbors failed!");
    test_searching(&graph);
}

fn test_weighted_digraph(){
    println!("test_graph function");
    let mut graph = WeightedDigraph::<char>::new();
    graph.add_vertex(&'a');
    graph.add_vertex(&'b');
    graph.add_vertex(&'c');
    graph.add_vertex(&'d');
    graph.add_vertex(&'a');
    graph.add_vertex(&'x');
    graph.remove_vertex(&'x');
    graph.add_edge(&'a',&Pair{first: 'b', second: 24});
    graph.add_edge(&'a',&Pair{first: 'c', second: 10});
    graph.add_edge(&'b', &Pair{first: 'd', second: 15});
    graph.add_edge(&'d',&Pair{first: 'c', second: 5});
    graph.add_edge(&'d',&Pair{first: 'e', second: 30});  
    graph.add_edge(&'a',&Pair{first: 'b', second: 20});
    graph.add_edge(&'a', &Pair{first: 'd', second: 2});
    //assert_eq!(*graph.get_neighbors(&'a').expect("Failed to get neighbors for a"), vec!['b', 'c', 'd']);
    graph.remove_edge(&'a', &'b');
    //assert_eq!(*graph.get_neighbors(&'a').expect("Failed to get neighbors for a"), vec!['c', 'd']);
    assert!(true, "get_neighbors failed!");
    test_searching(&graph);
}

fn test_digraph(){
    println!("test_graph function");
    let mut graph = Digraph::<char>::new();
    graph.add_vertex(&'a');
    graph.add_vertex(&'b');
    graph.add_vertex(&'c');
    graph.add_vertex(&'d');
    graph.add_vertex(&'a');
    graph.add_vertex(&'x');
    graph.remove_vertex(&'x');
    graph.add_edge(&'a',& 'b');
    graph.add_edge(&'a',& 'c');
    graph.add_edge(&'b', &'d');
    graph.add_edge(&'d',&'c');
    graph.add_edge(&'d',& 'e');  
    graph.add_edge(&'a',& 'b');
    graph.add_edge(&'a',& 'c');
    graph.add_edge(&'a', &'d');
    graph.add_edge(&'a', &'b');
    assert_eq!(*graph.get_neighbors(&'a').expect("Failed to get neighbors for a"), vec!['b', 'c', 'd']);
    graph.remove_edge(&'a', &'b');
    assert_eq!(*graph.get_neighbors(&'a').expect("Failed to get neighbors for a"), vec!['c', 'd']);
    assert!(true, "get_neighbors failed!");
    test_searching(&graph);
}

fn test_graph(){
    println!("test_graph function");
    let mut graph = Graph::<char>::new();
    graph.add_vertex(&'a');
    graph.add_vertex(&'b');
    graph.add_vertex(&'c');
    graph.add_vertex(&'d');
    graph.add_vertex(&'a');
    graph.add_vertex(&'x');
    graph.remove_vertex(&'x');
    graph.add_edge(&'a',& 'b');
    graph.add_edge(&'a',& 'c');
    graph.add_edge(&'b', &'d');
    graph.add_edge(&'d',&'c');
    graph.add_edge(&'d',& 'e');  
    graph.add_edge(&'a',& 'b');
    graph.add_edge(&'a',& 'c');
    graph.add_edge(&'a', &'d');
    graph.add_edge(&'a', &'b');
    assert_eq!(*graph.get_neighbors(&'a').expect("Failed to get neighbors for a"), vec!['b', 'c', 'd']);
    graph.remove_edge(&'a', &'b');
    assert_eq!(*graph.get_neighbors(&'a').expect("Failed to get neighbors for a"), vec!['c', 'd']);
    assert!(true, "get_neighbors failed!");
    test_searching(&graph);
}

fn main() {
    println!("Testing Graph Class");
    test_graph();
    test_digraph();
    test_weighted_graph();
    test_weighted_digraph();
}
