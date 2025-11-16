mod result;
mod graph;

pub trait search_algorithm<T>
{
    fn dfs(self: &impl graph) -> Vec<T>;
    fn bfs() -> Vec<T>;
    fn dijkstra() -> Vec<T>;
    fn bellman_ford() -> Vec<T>;
    fn floyd_warshall() -> Vec<T>;
    fn a_star() -> Vec<T>;
}