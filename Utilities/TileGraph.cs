using UnityEngine;
using System.Collections.Generic;

public class TileGraph{

    Dictionary<Tile, Node> nodes;

    //graph of building
    public TileGraph(Building building){

        nodes = new Dictionary<Tile, Node>();

        for(int x = 0; x < building.getX(); x++){
            for(int y = 0; y < building.getY(); y++){
                Tile t = building.getTileAt(x, y);

                if(/*t.getType() == Tile.TileType.Floor &&*/ !(t.hasWall())){
                    Node n = new Node(t);
                    nodes.Add(t, n);
                }
            }
        }
        Debug.Log("Graph Count: " + nodes.Count);
        int edgeCount = 0;

        foreach(Tile t in nodes.Keys){
            Node n = nodes[t];
            List<Edge> edges = new List<Edge>();

            Tile[] neighbors = t.getNeighbors(true);
            //Debug.Log("There are " + neighbors.Length + " neighbors");

            n.edges = new Node[neighbors.Length];
            for (int i = 0; i < neighbors.Length; i++){
                //change to use updating movementcost
                if(neighbors[i] != null && /*neighbors[i].getType() == Tile.TileType.Floor &&*/ !(neighbors[i].hasWall())){
                    //Neighbor exists and is walkable, so create an edge  TODO: replace with node pointers with removal of edges
                    n.edges[i] = nodes[neighbors[i]];
                    edgeCount++;
                }
            }
            //Debug.Log("Graph: Created " + edgeCount + " edges");
        }
    }
    public Node getNode(Tile t){
        return nodes[t];
    }
}