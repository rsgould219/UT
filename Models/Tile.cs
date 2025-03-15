using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

public class Tile: IXmlSerializable{
    public enum TileType {Empty, Floor, Grass, Wall};
    Action<Tile> cbTileTypeChanged;

    TileType type = TileType.Grass;
    PlacedObject placedObject;
    //home tile of the obj placed on this tile
    Tile homeTileOfPlacedObject = null;
    Building building;
    int x;
    int y;

    public Tile(Building building, int x, int y){
        this.building = building;
        this.x = x;
        this.y = y;
    }

    public Tile(){
        
    }
    public Tile[] GetNeighbours(bool diagOkay = false) {
		Tile[] ns;

		if(diagOkay == false) {
			ns = new Tile[4];	// Tile order: N E S W
		}
		else {
			ns = new Tile[8];	// Tile order : N E S W NE SE SW NW
		}

		Tile n;

		n = building.getTileAt(x, y+1);
		ns[0] = n;	// Could be null, but that's okay.
		n = building.getTileAt(x+1, y);
		ns[1] = n;	// Could be null, but that's okay.
		n = building.getTileAt(x, y-1);
		ns[2] = n;	// Could be null, but that's okay.
		n = building.getTileAt(x-1, y);
		ns[3] = n;	// Could be null, but that's okay.

		if(diagOkay == true) {
			n = building.getTileAt(x+1, y+1);
			ns[4] = n;	// Could be null, but that's okay.
			n = building.getTileAt(x+1, y-1);
			ns[5] = n;	// Could be null, but that's okay.
			n = building.getTileAt(x-1, y-1);
			ns[6] = n;	// Could be null, but that's okay.
			n = building.getTileAt(x-1, y+1);
			ns[7] = n;	// Could be null, but that's okay.
		}

		return ns;
	}
    //getters and setters
    public int getX(){
        return x;
    }
    public int getY(){
        return y;
    }
    public TileType getType(){
        return type;
    }
    //set tile type, but check if the player has the money for appropriate types
    public void setType(TileType type){
        switch(type){
            case TileType.Grass:
                UIController.Instance.addMoney(50);
                this.type = type;
                break;
            case TileType.Floor:
                if(this.type == TileType.Floor)
                    return;
                if(UIController.Instance.getMoney() >= 25){
                    UIController.Instance.subtractMoney(25);
                    this.type = type;
                }else
                    UIController.Instance.setInfoBar("Not enough Money");
                break;
            case TileType.Wall:
                if(UIController.Instance.getMoney() >= 100){
                    UIController.Instance.subtractMoney(100);
                    this.type = type;
                }else
                    UIController.Instance.setInfoBar("Not enough Money");
                break;
        }
        if(cbTileTypeChanged != null){
            cbTileTypeChanged(this);
        }
    }
    public PlacedObject getObj(){
        return this.placedObject;
    }
    //set the tile as a home tile for adjacent tiles for a large object
    public bool setHomeTile(Tile t){
        if(this.homeTileOfPlacedObject != null){
            Debug.Log("this tile " + x + " " + y + " has a home tile at " + this.homeTileOfPlacedObject.getX() + " " + this.homeTileOfPlacedObject.getY());
            return false;
        }
        if(this.placedObject != null){
            Debug.Log("invalid placed object");
            return false;
        }
        Debug.Log("TileType is " + this.type.ToString());
        if(this.type != TileType.Floor){
            Debug.Log("invalid tile type. Tile is " + this.type);
            return false;
        }
        else{
            this.homeTileOfPlacedObject = t;
            return true;
        }
    }

    public Tile getHomeTile(){
        return this.homeTileOfPlacedObject;
    }

    public void registerTileTypeChange(Action<Tile> callback){
        cbTileTypeChanged += callback;
    }

    public bool checkNoObj(){
        if(placedObject == null){
            return true;
        }
        return false;
    }

    //Called from placedObjects.makePerototype to check if object can be placed on tile
    public bool placeObject(PlacedObject objt){
        if(objt == null){
            //unistall
            placedObject = null;
            return true;
        }
        //Check if placed object already exists
        if(placedObject != null){
            Debug.Log("error - tried to override placed object");
            Debug.Log(placedObject.getObjType());
            return false;
        }
        if(homeTileOfPlacedObject != null){
            Debug.Log("error - tried to override placed object(ht) at " + homeTileOfPlacedObject.getX() +
                " " + homeTileOfPlacedObject.getY());
            Debug.Log(homeTileOfPlacedObject.getObj().getObjType());
            return false;
        }
        //Check if placed object is on grass
        if(type != TileType.Floor && objt.getObjType() != PlacedObject.ObjectType.Wall){
            Debug.Log("error - tried to place on grass");
            return false;
        }
        placedObject = objt;
        return true;
    }

    //check if an object is valid for previews
    public bool checkObject(PlacedObject objt){
         //Check if placed object already exists
        if(placedObject != null){
            Debug.Log("error - tried to override placed object");
            return false;
        }
        if(homeTileOfPlacedObject != null){
            Debug.Log("error - tried to override placed object(ht) at " + homeTileOfPlacedObject.getX() +
                " " + homeTileOfPlacedObject.getY());
            Debug.Log(homeTileOfPlacedObject.getObj().getObjType());
            return false;
        }
        if(objt.getWidth() != 1 && (MouseController.Instance.rotate == 0 || MouseController.Instance.rotate == 2)){
            if(BuildingController.Instance.building.getTileAt(this.getX() + 1, this.getY()).placedObject != null){
                Debug.Log("error - tried to override placed object(right) at " + (this.getX() + 1) +
                " " + this.getY());
                return false;
            }
        }
        //Check if placed object is on grass
        if(type != TileType.Floor && objt.getObjType() != PlacedObject.ObjectType.Wall){
            Debug.Log("error - tried to place on grass");
            return false;
        }
        return true;
    }

    //removes an object
    public void removeObject(){
        if(this.getHomeTile() != null){
            this.getHomeTile().removeObject();
            return;
        }

        if(this.placedObject == null)
            return;
        //remove gameobject associated with the placed object
        switch(this.placedObject.getObjType()){
            case PlacedObject.ObjectType.Board:
                checkGO(GameObject.Find("Board " + this.x + " " + this.y), 50);
                break;
            case PlacedObject.ObjectType.Desk:
                checkGO(GameObject.Find("Desk " + this.x + " " + this.y), 125);
                break;
            case PlacedObject.ObjectType.Door:
                checkGO(GameObject.Find("Door " + this.x + " " + this.y), 50);
                break;
            case PlacedObject.ObjectType.StudentDesk:
                checkGO(GameObject.Find("StudentDesk " + this.x + " " + this.y), 50);
                break;
            case PlacedObject.ObjectType.Bed:
                checkGO(GameObject.Find("Bed " + this.x + " " + this.y), 75);
                break;
            case PlacedObject.ObjectType.Dresser:
                checkGO(GameObject.Find("Dresser " + this.x + " " + this.y), 75);
                break;
        }
        this.placedObject = null;
    }

    //removes wall
    public void destroyFixture(){
        if (this.placedObject != null){
            if (this.placedObject.getObjType() == PlacedObject.ObjectType.Wall){
                this.placedObject = null;
                UIController.Instance.destroyGameObject(GameObject.Find("Wall " + this.x + " " + this.y));
            }
        }
        else 
            setType(TileType.Grass);
    }
    //check if a game object is present and remove it if it is, with a refund to the player
    public void checkGO(GameObject GO, int refund){
        Tile t;
        if(GO != null){
            UIController.Instance.destroyGameObject(GO);
            UIController.Instance.addMoney(refund);
            t = BuildingController.Instance.building.getTileAt(this.x + 1, this.y);
            //check if other tiles have this tile as hometile for large objects
            if(t.getHomeTile() != null && t.getHomeTile().getX() == this.x && t.getHomeTile().getY() == this.y){
                t.removeHomeTile();
            }
            t = BuildingController.Instance.building.getTileAt(this.x, this.y + 1);
            if(t.getHomeTile() != null && t.getHomeTile().getX() == this.x && t.getHomeTile().getY() == this.y){
                t.removeHomeTile();
            }
            t = BuildingController.Instance.building.getTileAt(this.x + 1, this.y + 1);
            if(t.getHomeTile() != null && t.getHomeTile().getX() == this.x && t.getHomeTile().getY() == this.y){
                t.removeHomeTile();
            }
        }
    }

    //remove home tile
    public void removeHomeTile(){
        this.homeTileOfPlacedObject = null;
    }

    //checks if wall is placed for graphing purposes
    public bool hasWall(){
        if(placedObject != null){
            if(placedObject.getObjType() != PlacedObject.ObjectType.Wall){
                return false;
            }
            else{
                return true;
            }
        }
        return false;
    }

    //checks if this tile is the neighbor of any other
    public bool isNeighbor(Tile tile, bool diag = false){
        if ( Mathf.Abs(this.x - tile.getX()) + Mathf.Abs(this.y - tile.getY()) == 1){
            return true;
        }
        else if (diag && Mathf.Abs(this.x - tile.getX()) == 1 && Mathf.Abs(this.y - tile.getY()) == 1){
            return true;
        }
        return false;
    }

    //gets neighbors of this tile
    public Tile[] getNeighbors(bool diag = false){
        Tile[] t;

        if( !(diag) ){
            t = new Tile[4];
        }
        else{
            t = new Tile[8];
        }

        t[0] = building.getTileAt(x, y + 1);
        t[1] = building.getTileAt(x + 1, y);
        t[2] = building.getTileAt(x - 1, y);
        t[3] = building.getTileAt(x, y - 1);
        if(diag == true){
            t[4] = building.getTileAt(x + 1, y + 1);
            t[5] = building.getTileAt(x + 1, y - 1);
            t[6] = building.getTileAt(x - 1, y + 1);
            t[7] = building.getTileAt(x - 1, y - 1);
        }
        return t;
    }

    //Handle XML serialization
    public XmlSchema GetSchema(){
        return null;
    }

    public void WriteXml(XmlWriter writer){
        writer.WriteAttributeString("x", x.ToString());
		writer.WriteAttributeString("y", y.ToString());
        writer.WriteAttributeString("Tp", ((int)type).ToString());
    }

    public void ReadXml(XmlReader reader){
        //BuildingController.Instance.OnTileTypeChange(this, GameObject.Find("Tile_" + this.x + "_" + this.y));
    }
}