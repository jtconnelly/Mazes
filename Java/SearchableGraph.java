import java.util.ArrayList;
import java.util.Iterator;
import java.util.List;



public abstract class SearchableGraph<T extends Comparable<T>> implements Graph<T>{
    private class Node{
        public Node(T x) {
            this.val = x;
            this.edges = new ArrayList<Node>();
        }
        T val;
        List<Node> edges;
    }

    private List<Node> verts;
    SearchableGraph(){
        this.verts = new ArrayList<Node>();
    }

    /**
     * adds a node in sorted order based on the value of T
     * If a node with that value already exists, then we skip it
     * @param node the value we want to add to the list.
     */
    public void addNode(T node)
    {
        if (this.verts.isEmpty()) {
            this.verts.add(new Node(node));
            return;
        }
        int right = this.verts.size() - 1;
        int left = 0;
        while (right > left){
            int middle = (left + right)/2;
            T val = this.verts.get(middle).val;
            if (val == node) {
                return;
            }
            else if (val.compareTo(node) > 0)
            {
                right = middle - 1;
            }
            else {
                left = middle;
            }
        }
        this.verts.add(left, new Node(node));
    }

    public void addEdge(T x, T y){

    }

    public List<T> bfsPath(T start, T end) {
        return new ArrayList<T>();
    }


}
