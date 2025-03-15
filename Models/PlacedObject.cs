using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//objects such as doors and funiture
public class PlacedObject: IXmlSerializable{
    public enum ObjectType{Wall, ClassDesk, Door, Board, Desk, StudentDesk, Lab, Bed, Dresser};
    Tile tile;
    ObjectType type;
    int width;
    int height;
    int movement;
    int rotation;

    Action<PlacedObject> cbOnObjectChange;

    //Enstablished protected constructor 
    protected PlacedObject(){ 

    }
    static public PlacedObject createPrototype(ObjectType objType, int movement, int width, int height){
        PlacedObject obj = new PlacedObject();
        obj.type = objType;
        obj.width = width;
        obj.height = height;
        obj.movement = movement;
        return obj;
    }
    //More convienent constuctor for the common 1x1 object
    static public PlacedObject createPrototype(ObjectType objType, int movement){
        PlacedObject obj = new PlacedObject();
        obj.type = objType;
        obj.movement = movement;
        obj.width = 1;
        obj.height = 1;
        return obj;
    }
    static public PlacedObject makePrototype(PlacedObject prototype, Tile tile, int rotation, Building b){ 
        PlacedObject obj = new PlacedObject();
        obj.type = prototype.type;
        obj.width = prototype.width;
        obj.height = prototype.height;
        obj.movement = prototype.movement;
        obj.tile = tile;
        obj.rotation = rotation;

        //Checks if object can be placed on tile just return null if placing the object fails
        if(tile.placeObject(obj) == false)
            return null;

        //Check for object with a wide size, adgusting for rotation
        //Debug.Log("Tile at " + tile.getX() + " " + tile.getY());
        if(obj.width != 1){
            if(rotation == 1 || rotation == 3){
                if(b.getTileAt(tile.getX(), tile.getY() + 1).setHomeTile(tile) == false){
                    tile.removeObject();
                    Debug.Log("error - tried to override placed object(1)");
                    UIController.Instance.setInfoBar("Can't Place Object There");
                    return null;
                }
            }
            else if(b.getTileAt(tile.getX() + 1, tile.getY()).setHomeTile(tile) == false){
                tile.removeObject();
                Debug.Log("error - tried to override placed object(1)");
                UIController.Instance.setInfoBar("Can't Place Object There");
                return null;
            }
        }
        //check extra tiles for large objects
        if(obj.height != 1){
            if(rotation == 1){
                if(b.getTileAt(tile.getX() + 1, tile.getY()).setHomeTile(tile) == false){
                    tile.removeObject();
                    Debug.Log("error - tried to override placed object(2)");
                    UIController.Instance.setInfoBar("Can't Place Object There");
                }
            }
            else if(b.getTileAt(tile.getX(), tile.getY() + 1).setHomeTile(tile)== false){
                tile.removeObject();
                Debug.Log("error - tried to override placed object(2)");
                UIController.Instance.setInfoBar("Can't Place Object There");
                return null;
            }
        }
        if(obj.width != 1 && obj.height != 1){
            if(b.getTileAt(tile.getX() + 1, tile.getY() + 1).setHomeTile(tile)== false){
                tile.removeObject();
                Debug.Log("error - tried to override placed object(3)");
                UIController.Instance.setInfoBar("Can't Place Object There");
                b.getTileAt(tile.getX() + 1, tile.getY()).removeHomeTile();
                b.getTileAt(tile.getX(), tile.getY() + 1).removeHomeTile();
                return null;
            }
        }
        return obj;
    }

    public int getWidth(){
        return this.width;
    }
    public int getHeight(){
        return this.height;
    }
    public Tile getTile(){
        return tile;
    }
    public ObjectType getObjType(){
        return this.type;
    }
    public int getRotation(){
        return this.rotation;
    }
    public int getMovement(){
        return this.movement;
    }
    public void registerOnChangeCallback(Action<PlacedObject> callback){
        cbOnObjectChange += callback;
    }

     //Handle XML serialization
    public XmlSchema GetSchema(){
        return null;
    }

    public void WriteXml(XmlWriter writer){
        writer.WriteAttributeString("Tp", ((int)type).ToString());
        writer.WriteAttributeString("x", tile.getX().ToString());
		writer.WriteAttributeString("y", tile.getY().ToString());
        writer.WriteAttributeString("Rotation", rotation.ToString());
    }

    public void ReadXml(XmlReader reader){
      //load
    }
}