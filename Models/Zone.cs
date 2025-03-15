using System.Collections;
using System;
using System.Xml;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Zone {
    int startX;
    int startY;
    int endX;
    int endY;
    Tile nextSeat = null;
    //List of zone types
    public enum ZoneType{Class, Office, Lab, Zone};
    public Building building;
    ZoneType type;
    int zoneId;
    int sDeskCount;
    int oDeskCount;
    int boardCount;
    int doorCount;
    int bedCount;
    int dresserCount;

    //Constructors
    public Zone(Building b, string type, int sDeskCount, int oDeskCount, int boardCount, int doorCount,
                int bedCount, int dresserCount, int startX, int startY, int endX, int endY, int id){
        this.building = b;
        this.sDeskCount = sDeskCount;
        this.oDeskCount = oDeskCount;
        this.boardCount = boardCount;
        this.doorCount = doorCount;
        this.bedCount = bedCount;
        this.dresserCount = dresserCount;
        assignType(type);
        this.startX = startX;
        this.startY = startY;
        this.endX = endX;
        this.endY = endY;
        this.zoneId = id;
    }

    public Zone(Building b, string type, int startX, int startY, int endX, int endY, int id){
        this.building = b;
        this.sDeskCount = 0;
        this.oDeskCount = 0;
        this.boardCount = 0;
        this.doorCount = 0;
        this.bedCount = 0;
        this.dresserCount = 0;
        assignType(type);
        this.startX = startX;
        this.startY = startY;
        this.endX = endX;
        this.endY = endY;
        this.zoneId = id;
    }
    //Getters and Seters
    public void addSDesk(){
        sDeskCount++;
    }
    public void addODesk(){
        oDeskCount++;
    }
    public void addDoor(){
        doorCount++;
    }
    public void addBoard(){
        boardCount++;
    }
    public void addBed(){
        bedCount++;
    }
    public void addDresser(){
        dresserCount++;
    }
    public void assignId(int id){
        this.zoneId = id;
    }
    public void assignType(string s){
        switch(s){
            case "Lab":
                this.type = ZoneType.Lab;
                break;
            case "Class":
                this.type = ZoneType.Class;
                break;
            case "Office":
                this.type = ZoneType.Office;
                break;
            case "Dorm":
                this.type = ZoneType.Zone;
                ScheduleController.Instance.addIntStudents();
                if(ScheduleController.Instance.semFall == false)
                    UIController.Instance.addLocalStudents();
                break;
        }
    }
    public int getId(){
        Debug.Log("Zoneid is " + zoneId);
        return zoneId;
    }
    public int getSeatCount(){
        return this.sDeskCount;
    }
    public int getBedCount(){
        return this.bedCount;
    }
    public Tile getNextSeat() {
        Tile oldSeat = this.nextSeat;
        findNextSeat();
        if(oldSeat != null && this.nextSeat.getX() == oldSeat.getX() && this.nextSeat.getY() == oldSeat.getY()){
            Debug.Log("No change in seats. Zone is " + this.startX + this.startY + this.endX + this.endY);
        }
        return this.nextSeat;
    }
    public ZoneType getType(){
        return this.type;
    }
    public string getMaxDimensions(){
        return "Looking for Desk at endx" + endX + "and endY" + endY;
    }

    //check if a tile coord is in the zone
    public bool inZone(int X, int Y){
        if (this.startX <= X && X <= this.endX){
            if (this.startY <= Y && Y <= this.endY)
                return true;
            else
                return false;
        }
        else
            return false;
    }

    //check if a zone area matches another zone area
    public bool zoneMatch(int startX2, int startY2, int endX2, int endY2){
        if(startX2 >= this.startX && this.endX > startX2){
            if(startY2 >= this.startY && this.endY > startY2){
                return true;
            }
        }
        else if(endX2 > this.startX && this.endX >= endX2){
           if(endY2 > this.startY && this.endY >= endY2){
                return true;
            }
        }
        return false;
    }

    //find the next student seat in the zone
    public void findNextSeat(){
        int x = startX;
        int y = startY;
        if(nextSeat != null) {
            x = nextSeat.getX();
            y = nextSeat.getY();
            y++;
            if(endY == y){
                y = startY;
                x++;
            }
            if(endX == x){
                x = startX;
            }
        }

        if(this.type == ZoneType.Lab){
            findNextLabSeat(x, y);
        }
        Debug.Log("Starting to Look for seat at x:" + x + " y:" + y);
        for(int i = x; i < endY; i++) {
            for(int j = y; j < endY; j++) {
                Debug.Log("Looking for seat at x:" + i + " y:" + j);
                if (BuildingController.Instance.building.getTileAt(i, j).getObj() != null){
                    Debug.Log("Found2 seat at x:" + i + " y:" + j);
                    if (BuildingController.Instance.building.getTileAt(i, j).getObj().getObjType() == PlacedObject.ObjectType.StudentDesk){
                        Debug.Log("Found seat at x:" + i + " y:" + j);
                        nextSeat = BuildingController.Instance.building.getTileAt(i, j);
                        return;
                    }
                }
                y = startY;
            }
        }
        resetSeat();
        findNextSeat();
    }

    //find the next seat in the lab
    public void findNextLabSeat(int x, int y){
        for(int i = x; i < endY; i++) {
            for(int j = y; j < endY; j++) {
                Debug.Log("Looking for lab desk at x:" + i + " y:" + j);
                if (BuildingController.Instance.building.getTileAt(i, j).getObj() != null){
                    Debug.Log("Found2 seat at x:" + i + " y:" + j);
                    if (BuildingController.Instance.building.getTileAt(i, j).getObj().getObjType() == PlacedObject.ObjectType.Desk){
                        Debug.Log("Found seat at x:" + i + " y:" + j);
                        nextSeat = BuildingController.Instance.building.getTileAt(i, j);
                        return;
                    }
                }
                y = startY;
            }
        }
    }

    public void resetSeat(){
        nextSeat = null;
    }

    //find the desk in the zone
    public Tile getDesk(){
        PlacedObject obj = null;
        int x = -1, y = -1;
        for(int i = startX; i < endX; i++) {
            for(int j = startY; j < endY; j++) {
                //Debug.Log("Looking for desk at x:" + i + " y:" + j);
                if (BuildingController.Instance.building.getTileAt(i, j).getObj() != null){
                    if (BuildingController.Instance.building.getTileAt(i, j).getObj().getObjType() == PlacedObject.ObjectType.Desk){
                        Debug.Log("Found desk at x:" + i + " y:" + j);
                        obj = BuildingController.Instance.building.getTileAt(i, j).getObj();
                        x = i;
                        y = j;
                    }
                }
            }
        }
        if(obj == null){
            Debug.Log("No desk in zone");
            return null;
        }
        //find out the rotation of the desk and adjust dest tile accordingly
        switch(obj.getRotation()){
            case (0):
                return BuildingController.Instance.building.getTileAt(x, y);
            case (1):
                return BuildingController.Instance.building.getTileAt(x + 1, y + 1);
            case (2):
                return BuildingController.Instance.building.getTileAt(x + 1, y);
            case (3):
                return BuildingController.Instance.building.getTileAt(x, y);
        }
        return null;
    }

    //make the green markers to show the zone area when deleting zones
    public void displayZone(){ 
        Debug.Log("Checking building ids " + building.id + " vs " + (WorldController.Instance.world.getCurrentBuilding() - 1));
        if(building != null && building.id == WorldController.Instance.world.getCurrentBuilding() - 1)
            UIController.Instance.makeZoneMarkers(startX, startY, endX, endY);
    }

    //Save writer
    public void writeXml(XmlWriter writer){
        writer.WriteAttributeString("startX", this.startX.ToString());
        writer.WriteAttributeString("startY", this.startY.ToString());
        writer.WriteAttributeString("endX", this.endX.ToString());
        writer.WriteAttributeString("endY", this.endY.ToString());
        writer.WriteAttributeString("sDesk", this.sDeskCount.ToString());
        writer.WriteAttributeString("oDesk", this.oDeskCount.ToString());
        writer.WriteAttributeString("board", this.boardCount.ToString());
        writer.WriteAttributeString("door", this.doorCount.ToString());
        writer.WriteAttributeString("bed", this.bedCount.ToString());
        writer.WriteAttributeString("dresser", this.dresserCount.ToString());
        writer.WriteAttributeString("zId", this.zoneId.ToString());
        writer.WriteAttributeString("bId", this.building.id.ToString());
    }
}