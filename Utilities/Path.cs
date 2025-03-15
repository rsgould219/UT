using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path{
    Queue<Tile> tPath;
    Node destSaved;

    //constructor for crating a path from skratch
    public Path(Building building, Tile start, Tile end){
        tPath = new Queue<Tile>();

        Queue<Node> nPath = new Queue<Node>();
        TileGraph tree = building.getGraph();
        Node destination = tree.getNode(end);
        bool destinationFound = false;
        destSaved = destination;

        nPath.Enqueue(tree.getNode(start));
        Debug.Log("Looking for path from " + start.getX() + start.getY() + " to " + end.getX() + end.getY());
        if(nPath.Count <= 0)
            Debug.Log("Empty starting path");
        if(destinationFound)
            Debug.Log("Destination somehow found");
        int i = 0;
        while(i < 10000 && nPath.Count > 0 && !(destinationFound)){
            Node current = nPath.Dequeue();
            current.marked = true;
            i++;
            foreach(Node n in current.edges){
                Debug.Log("mapping nodes");
                if (n != null && n.marked == false){
                    Debug.Log("Touched tile at " + n.data.getX() + " " + n.data.getY() + " from tile at " + current.data.getX() + " " + current.data.getY());
                    if(n != destination){
                        n.marked = true;
                        n.parent = current;
                        nPath.Enqueue(n);
                    }
                    else{
                        n.parent = current;
                        nPath.Enqueue(n);
                        destinationFound = true;
                        break;
                    }
                }
                else if(n != null){
                    Debug.Log("Found marked edge while looking for" + end.getX() + " " + end.getY() + " while at " + current.data.getX() + " " + current.data.getY());
                }
            }
            
            
        }
        Stack<Node> stack = new Stack<Node>(); 
        stack.Push(destination);
        //make a stack of the nodes
        while (destination.parent != null){ 
            Debug.Log("Pushing stack with tile at " + destination.data.getX() + " " + destination.data.getY()  + " " + destination.parent.data.getX() + " " + destination.parent.data.getY());
            stack.Push(destination.parent); 
            destination = destination.parent; 
        }
        //clean the nodes of markers
        foreach(Node n in stack){
            foreach(Node n2 in n.edges){
                if(n2 != null)
                    n2.marked = false;
            }
        }
        //make a queue of the nodes out of the stack
        while (stack.Count > 0)
            tPath.Enqueue(stack.Pop().data);
        tPath.Dequeue();
    }
    //constructor for creating a pth from a save file
    public Path(Queue<Tile> tQ, Tile t){
        tPath = tQ;
        destSaved = new Node(t);
    }
    public Path(){
        tPath = new Queue<Tile>();
        if(WorldController.Instance.getTime2() < 9){
            tPath.Enqueue(BuildingController.Instance.building.getTileAt(0, 1));
            tPath.Enqueue(BuildingController.Instance.building.getTileAt(0, 2));
            tPath.Enqueue(BuildingController.Instance.building.getTileAt(0, 3));
            tPath.Enqueue(BuildingController.Instance.building.getTileAt(0, 4));
        }
        else
        {
            tPath.Enqueue(BuildingController.Instance.building.getTileAt(0, 3));
            tPath.Enqueue(BuildingController.Instance.building.getTileAt(0, 2));
            tPath.Enqueue(BuildingController.Instance.building.getTileAt(0, 1));
            tPath.Enqueue(BuildingController.Instance.building.getTileAt(0, 0));
        }
    }
    //return the tile path
    public Queue<Tile> getNextTile(){
        return tPath;
    }
    //return the coordinates of the saved destination for bug testing purposes
    public string getDest(){
        return "x: " + destSaved.data.getX() + "y: " + destSaved.data.getY();
    }
}