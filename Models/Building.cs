using System.Collections;
using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Building: IXmlSerializable {
    string name;
    int x;
    int y;
    public int id;
    Tile[,] tiles;
    public Dictionary<string, PlacedObject> placedObjectPrototypes;
    Action<PlacedObject> cbPlacedObjectCreated;
    TileGraph tileGraph;
    public Path_TileGraph pathTileGraph;
    //list of open doors with X dimension, y dimension , GameObject, rotation, time opened
    public List<Tuple<int, int, GameObject, int, int>> openDoorList = new List<Tuple<int, int, GameObject, int, int>>();

    ///constructor
    public Building(int x = 100, int y = 100){
        this.x = x;
        this.y = y;

        //sets up tiles
        tiles = new Tile[x, y];
        for (int i = 0; i < x; i++){
            for (int j = 0; j < y; j++){
                tiles[i, j] = new Tile(this, i, j);
            }
        }

        Debug.Log ("Building created: " + x + " by " + y);
        createPlaceObjtProto();
    }

    //Gets tile at world coordinates
    public Tile getTileAt(int x, int y){
        if(x > this.x || y > this.y || x < 0 || y < 0){
            //Debug.LogError("Call for tile out of range");
            return null;
        }
        try{
            return tiles[x, y];
        }catch(IndexOutOfRangeException e){
            return null;
        }
    }
    //Other getters and setters
    public int getX() {
        return x;
    }
    public int getY() {
        return y;
    }
    public void setId(int id){
        this.id = id;
    }
    public void setName(string name){
        this.name = name;
    }
    public string getName(){
        return name;
    }
    public TileGraph getGraph(){
        return tileGraph;
    }

    //Place an object
    public void placeObject(string objectType, Tile tile, int rotation){
        Debug.Log("Attempting to place object");
        if( !(placedObjectPrototypes.ContainsKey(objectType))){
            Debug.LogError("placedObjectProtoypes dictonary doesn't contain a proto for key: " + objectType);
            return;
        }

        PlacedObject obj = PlacedObject.makePrototype(placedObjectPrototypes[objectType], tile, rotation, this);

        if (obj == null){
            return; 
        }

        if(cbPlacedObjectCreated != null){
            cbPlacedObjectCreated(obj);
        }
        UIController.Instance.setInfoBar("Object Placed");
    }

    //Manage Placed Objects Prototypes
    public void registerPlacedObjtCreated(Action<PlacedObject> callback){
        cbPlacedObjectCreated += callback;
    }
    void createPlaceObjtProto(){
         placedObjectPrototypes = new Dictionary<string, PlacedObject>();

        //objects (name, prototype(object Type, movement bool, width, height))
        placedObjectPrototypes.Add("Wall",  PlacedObject.createPrototype(PlacedObject.ObjectType.Wall, 0));
        placedObjectPrototypes.Add("Door",  PlacedObject.createPrototype(PlacedObject.ObjectType.Door, 1));
        placedObjectPrototypes.Add("StudentDesk",  PlacedObject.createPrototype(PlacedObject.ObjectType.StudentDesk, 1, 2, 1));
        placedObjectPrototypes.Add("Desk",  PlacedObject.createPrototype(PlacedObject.ObjectType.Desk, 1, 2, 2));
        placedObjectPrototypes.Add("Board",  PlacedObject.createPrototype(PlacedObject.ObjectType.Board, 1, 2, 1));
        placedObjectPrototypes.Add("Bed",  PlacedObject.createPrototype(PlacedObject.ObjectType.Bed, 1, 2, 1));
        placedObjectPrototypes.Add("Dresser",  PlacedObject.createPrototype(PlacedObject.ObjectType.Dresser, 1, 2, 1));
    }

    //utility to check if the area for a proposed zone in the area of a preexisting zone
    public bool checkOverlap(int startX, int startY, int endX, int endY){
        Debug.Log("Checking zone");
        foreach(Zone z in WorldController.Instance.world.getClasses()){
            Debug.Log("Checking building ids " + z.building.id + " vs " + (WorldController.Instance.world.getCurrentBuilding() - 1));
            if(z.zoneMatch(startX, startY, endX, endY) &&  z.building.id == WorldController.Instance.world.getCurrentBuilding() - 1){
                Debug.Log("Class already present");
                UIController.Instance.setInfoBar("Class already present");
                return false;
            }
        }
        foreach(Zone z in WorldController.Instance.world.getLabs()){
            Debug.Log("Checking building ids " + z.building.id + " vs " + (WorldController.Instance.world.getCurrentBuilding() - 1));
            if(z.zoneMatch(startX, startY, endX, endY) &&  z.building.id == WorldController.Instance.world.getCurrentBuilding() - 1){
                Debug.Log("Lab already present");
                UIController.Instance.setInfoBar("Lab already present");
                return false;
            }
        }
        foreach(Zone z in WorldController.Instance.world.getOffices()){
            Debug.Log("Checking building ids " + z.building.id + " vs " + (WorldController.Instance.world.getCurrentBuilding() - 1));
            if(z.zoneMatch(startX, startY, endX, endY) &&  z.building.id == WorldController.Instance.world.getCurrentBuilding() - 1){
                Debug.Log("Office already present");
                UIController.Instance.setInfoBar("Office already present");
                return false;
            }
        }
        return true;
    }

    //Checks if a zone meets the requirement for creation
    public bool checkZone(int startX, int startY, int endX, int endY, string zoneType){
        int sDeskCount = 0;
        int oDeskCount = 0;
        int boardCount = 0;
        int doorCount = 0;
        int bedCount = 0;
        int dresserCount = 0;

        if(checkOverlap(startX, startY, endX, endY) == false)
            return false;

        if(startX < 0 || startY < 0 || endX > 99 || endY > 99){
            Debug.Log("Off screen");
            return false;
        }

        for (int i = startX; i <= endX; i++){
            for (int j = startY; j <= endY; j++){
                if (getTileAt(i, j).getObj() != null){
                    switch (getTileAt(i, j).getObj().getObjType()){
                        case PlacedObject.ObjectType.Door:
                            doorCount++;
                            break;
                        case PlacedObject.ObjectType.StudentDesk:
                            sDeskCount++;
                            break;
                        case PlacedObject.ObjectType.Desk:
                            oDeskCount++;
                            break;
                        case PlacedObject.ObjectType.Board:
                            boardCount++;
                            break;
                        case PlacedObject.ObjectType.Bed:
                            bedCount++;
                            break;
                        case PlacedObject.ObjectType.Dresser:
                            dresserCount++;
                            break;
                    }
                }
            }
        }

        if(zoneType.Equals("Class")){
            if (oDeskCount > 0 && sDeskCount > 0 && boardCount > 0 && doorCount > 0)
                return true;
            else{
                Debug.Log("Failed to add class zone");
                UIController.Instance.setInfoBar("Failed to add class zone");
                return false;
            }
        }
        else if(zoneType.Equals("Office")){
            if (oDeskCount > 0 && doorCount > 0)
                return true;
            else{
                Debug.Log("Failed to add office zone");
                UIController.Instance.setInfoBar("Failed to add office zone");
                return false;
            }
        }
        else if(zoneType.Equals("Lab")){
            if (oDeskCount > 0 && doorCount > 0)
                return true;
            else{
                Debug.Log("Failed to add lab zone");
                UIController.Instance.setInfoBar("Failed to add lab zone");
                return false;
            }
        }
        else if(zoneType.Equals("Dorm")){
            if (bedCount != 0 && dresserCount != 0 && oDeskCount != 0 && bedCount == dresserCount && bedCount == oDeskCount && doorCount != 0)
                return true;
            else{
                Debug.Log("Failed to add dorm zone");
                UIController.Instance.setInfoBar("Failed to add dorm zone");
                return false;
            }
        }
        else{
            Debug.Log("Failed to add zone");
            UIController.Instance.setInfoBar("Failed to add zone");
            return false;
        }
    }

    //creates zone with number of placed objects and adding each tile to the zone
    public void addZone(int startX, int startY, int endX, int endY, string zoneType){
        int idForZone = WorldController.Instance.world.getCurrentBuilding() - 1;
        Zone zone = new Zone(WorldController.Instance.world.buildings[idForZone], zoneType, 
            startX, startY, endX, endY, WorldController.Instance.world.nextZoneId);
        for (int i = startX; i <= endX; i++){
            for (int j = startY; j <= endY; j++){
                if (getTileAt(i, j).getObj() != null){
                    switch (getTileAt(i, j).getObj().getObjType()){
                        case PlacedObject.ObjectType.Door:
                            zone.addDoor();
                            break;
                        case PlacedObject.ObjectType.StudentDesk:
                            zone.addSDesk();
                            break;
                        case PlacedObject.ObjectType.Desk:
                            zone.addODesk();
                            break;
                        case PlacedObject.ObjectType.Board:
                            zone.addBoard();
                            break;
                        case PlacedObject.ObjectType.Bed:
                            zone.addBed();
                            break;
                        case PlacedObject.ObjectType.Dresser:
                            zone.addDresser();
                            break;
                    }
                }
            }
        }
        //add to zone lists
        switch (zoneType){
            case "Class":
                WorldController.Instance.world.addClass(zone);
                UIController.Instance.setInfoBar("Class Added");
                Debug.Log("Class added");
                break;
            case "Office":
                WorldController.Instance.world.addOffice(zone);
                UIController.Instance.setInfoBar("Office Added");
                Debug.Log("Office added");
                break;
            case "Lab":
                WorldController.Instance.world.addLab(zone);
                UIController.Instance.setInfoBar("Lab Added");
                Debug.Log("Lab added");
                break;
            case "Dorm":
                WorldController.Instance.world.addDorm(zone);
                UIController.Instance.setInfoBar("Dorm Added");
                UIController.Instance.addIntStudent();
                Debug.Log("Dorm added");
                break;
            default:
                Debug.LogError("Unknown Zone");
                break;
        }
        WorldController.Instance.world.nextZoneId++;
    }

    public void refreshGraph(){
        tileGraph = new TileGraph(this);
        pathTileGraph = new Path_TileGraph(this);
    } 

    public void loadBuilding(Building b){
        for(int i = 0; i < x; i++){
            for(int j = 0; j < y; j++){
                Tile t = b.getTileAt(i, j);
                tiles[i, j].setType(t.getType());
                if(t.getType() == Tile.TileType.Floor)
                    Debug.Log("Tile saved at " + i  + " " + j);
                }
            }
        for(int i = 0; i < x; i++){
            for(int j = 0; j < y; j++){
                Tile t = b.getTileAt(i, j);
                if(t.getObj() != null){
                    switch(t.getObj().getObjType()){
                        case PlacedObject.ObjectType.Wall:
                            placeObject("Wall", tiles[i, j], t.getObj().getRotation());
                            break;
                        case PlacedObject.ObjectType.ClassDesk:
                            placeObject("ClassDesk", tiles[i, j], t.getObj().getRotation());
                            break;
                        case PlacedObject.ObjectType.Door:
                            placeObject("Door", tiles[i, j], t.getObj().getRotation());
                            break;
                        case PlacedObject.ObjectType.Board:
                            placeObject("Board", tiles[i, j], t.getObj().getRotation());
                            break;
                        case PlacedObject.ObjectType.Desk:
                            placeObject("Desk", tiles[i, j], t.getObj().getRotation());
                            break;
                        case PlacedObject.ObjectType.StudentDesk:
                            placeObject("StudentDesk", tiles[i, j], t.getObj().getRotation());
                            break;
                        case PlacedObject.ObjectType.Lab:
                            placeObject("Lab", tiles[i, j], t.getObj().getRotation());
                            break;
                        case PlacedObject.ObjectType.Bed:
                            placeObject("Bed", tiles[i, j], t.getObj().getRotation());
                            break;
                        case PlacedObject.ObjectType.Dresser:
                            placeObject("Dresser", tiles[i, j], t.getObj().getRotation());
                            break;
                        }
                }
            }
        }
        Debug.Log("Setting building id after goToBuilding to " + b.id);
        //id = b.id;
    }

    //Handle XML serialization
    public XmlSchema GetSchema(){
        return null;
    }

    public void WriteXml(XmlWriter writer){
        //WorldController.Instance.world.buildings[WorldController.Instance.world.getCurrentBuilding() - 1].loadBuilding(BuildingController.Instance.building);
        writer.WriteAttributeString("X", x.ToString());
		writer.WriteAttributeString("Y", y.ToString());
		writer.WriteAttributeString("id", id.ToString());
        //if(name != "")
		    writer.WriteAttributeString("name", name);
        //else{
            //writer.WriteAttributeString("name",GameObject.Find("buildingPanel").transform.GetChild(id + 1).name);
        //}

        writer.WriteStartElement("Tiles");
        for (int i = 0; i < y; i++){
            writer.WriteStartElement("T");
            tiles[0, i].WriteXml(writer);
            writer.WriteEndElement();
        }
        for (int i = 1; i < x; i++){
            for (int j = 0; j < y; j++){
                if(tiles[i, j].getType() == Tile.TileType.Floor){
                    writer.WriteStartElement("T");
                    tiles[i, j].WriteXml(writer);
                    writer.WriteEndElement();
                }
            }
        }
        writer.WriteEndElement();

        int oCount = 0;
        writer.WriteStartElement("Objects");
        for (int i = 0; i < x; i++){
            for (int j = 0; j < y; j++){
                if(tiles[i, j].getObj() != null){
                    writer.WriteStartElement("Obj");
				    tiles[i, j].getObj().WriteXml(writer);
				    writer.WriteEndElement();
                    oCount++;
                }
            }
        }
        if(oCount == 0){
            writer.WriteStartElement("ObjPlaceholder");
            writer.WriteEndElement();
        }
        writer.WriteEndElement();
    }

    public void ReadXml(XmlReader reader){
        while(reader.Read()) {
            Debug.Log("Building reader: " + reader.Name);
			switch(reader.Name) {
				case "Tiles":
					readTiles(reader);
					break;
                case "Objects":
                    readObj(reader);
                    break;
                default:
                    refreshGraph();
                    Debug.Log("Loaded from file building with bid" + this.id);
                    return;
			}
		}
        refreshGraph();
    }

    void readTiles(XmlReader reader) {
		while(reader.Read()) {
            if(reader.Name != "T"){
				return;
            }

			int x = int.Parse(reader.GetAttribute("x"));
			int y = int.Parse(reader.GetAttribute("y"));
			tiles[x,y].ReadXml(reader);
            if(int.Parse(reader.GetAttribute("Tp")) == 1){
                tiles[x,y].setType(Tile.TileType.Floor);
                UIController.Instance.addMoneyOnLoad(75);
            }
		}
	}

    void readObj(XmlReader reader){
        while(reader.Read()){
            if(reader.Name != "Obj"){
                if(reader.Name == "ObjPlaceholder"){
                    reader.Read();
                }
				return;
            }

            Debug.Log("Making Obj from file");
            int i= int.Parse(reader.GetAttribute("Tp")); 
            Tile t = this.getTileAt(int.Parse(reader.GetAttribute("x")), int.Parse(reader.GetAttribute("y")));
            switch(i){
                case 0:
                    placeObject("Wall", t, int.Parse(reader.GetAttribute("Rotation")));
                    break;
                case 1:
                    placeObject("ClassDesk", t, int.Parse(reader.GetAttribute("Rotation")));
                    break;
                case 2:
                    placeObject("Door", t, int.Parse(reader.GetAttribute("Rotation")));
                    break;
                case 3:
                    placeObject("Board", t, int.Parse(reader.GetAttribute("Rotation")));
                    break;
                case 4:
                    placeObject("Desk", t, int.Parse(reader.GetAttribute("Rotation")));
                    break;
                case 5:
                    placeObject("StudentDesk", t, int.Parse(reader.GetAttribute("Rotation")));
                    break;
                case 6:
                    placeObject("Lab", t, int.Parse(reader.GetAttribute("Rotation")));
                    break;
                case 7:
                    placeObject("Bed", t, int.Parse(reader.GetAttribute("Rotation")));
                    break;
                case 8:
                    placeObject("Dresser", t, int.Parse(reader.GetAttribute("Rotation")));
                    break;
            }
            UIController.Instance.addMoneyOnLoad(100);
        }
    }
}