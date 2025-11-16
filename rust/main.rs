use crate::graph::{Graph, Graphing};
use crate::searching::SearchAlgorithm;

mod graph;
mod searching;

fn test_searching(graph: &Graph<char>){
    println!("test_searching function");

    println!("DFS");
    let dfs_res = graph.dfs('a', 'd');
    assert!(!dfs_res.is_empty());
    let dfs_str = format!("{:?}", dfs_res);
    println!("DFS Result: {dfs_str}");

    println!();

    println!("BFS");
    let bfs_res = graph.bfs('a', 'd');
    assert!(!bfs_res.is_empty());
    let bfs_str = format!("{:?}", bfs_res);
    println!("BFS Result: {bfs_str}");
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
    assert_eq!(*graph.get_neighbors(&'a').unwrap(), vec!['b', 'c', 'd']);
    graph.remove_edge(&'a', &'b');
    assert_eq!(*graph.get_neighbors(&'a').unwrap(), vec!['c', 'd']);
    assert!(true, "get_neighbors failed!");
    test_searching(&graph);
}

fn main() {
    println!("Testing Graph Class");
    test_graph();
}
