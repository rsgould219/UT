using UnityEngine;
using System.Collections;

public class Node{

    public Tile data;
    public Node parent;
    public bool marked;
    public Node[] edges;

    public Node(Tile t){
        this.data = t;
        marked = false;
    }
}