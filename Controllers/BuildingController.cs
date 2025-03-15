using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BuildingController : MonoBehaviour{
    //Instance of current building
    public static BuildingController Instance;

    public Building building;
    public Sprite grassSprite;
    public Sprite floorSprite;
    public Sprite wallSprite, wallSpriteNSEW, wallSpriteNS;
    public Sprite wallSpriteNE, wallSpriteNW, wallSpriteSE, wallSpriteSW;
    public Sprite doorSprite, doorRightSprite, doorLeftSprite;
    public Sprite deskSprite;
    public Sprite deskSpriteFlip, deskSpriteTop, deskSpriteBottom;
    public Sprite oDeskSprite, oDeskTopSprite, oDeskFlipSprite, oDeskBottomSprite;
    public Sprite[] openDoorSprites = new Sprite[4];
    public Sprite boardSprite;
    public Sprite bedSprite;
    public Sprite dresserSprite;
    public Section clickedSection;
    public Zone selectedZ;

    // initialization 
    void Start(){
        Instance = this;
        building = new Building();
        building.setId(1);
        building.setName("Building 1");
        //WorldController.Instance.world.addBuilding(building);
        building.registerPlacedObjtCreated(onPlacedObjtCreated);

        //Sets a grid of grass tiles
        for (int i = 0; i < building.getX(); i++){
            for (int j = 0; j < building.getY(); j++){
                GameObject tileGO = new GameObject();
                Tile tileData = building.getTileAt(i, j);
                SpriteRenderer tileSR = tileGO.AddComponent<SpriteRenderer>();

                tileGO.name = "Tile_" + i + "_" + j;
                tileSR.sprite = grassSprite;
                tileGO.transform.position = new Vector3(tileData.getX(), tileData.getY(), 0);
                tileGO.transform.SetParent(this.transform, true);
                tileData.registerTileTypeChange((tile) => {
                    OnTileTypeChange(tile, tileGO);
                    });
            }
        }
    }

    //float randomTileTimer = 1f;

    // Update is called once per frame
    void Update(){
        foreach (Tuple<int, int, GameObject, int, int> door in building.openDoorList){
            if((door.Item5 < 55 && door.Item5 + 4 < WorldController.Instance.getTime1()) || (door.Item5 > 55 && WorldController.Instance.getTime1() < 6 && door.Item5 - 55 < WorldController.Instance.getTime1())){
                closeDoor(door);
            }
        }
    }
    //Utility fuction to change the sprit of the tile if any change in tile type occured
    public void OnTileTypeChange(Tile tileData, GameObject tileGO){
        if(tileData.getType() == Tile.TileType.Grass){
            tileGO.GetComponent<SpriteRenderer>().sprite = grassSprite;
        }
        else if(tileData.getType() == Tile.TileType.Floor){
            tileGO.GetComponent<SpriteRenderer>().sprite = floorSprite;
        }
        else if(tileData.getType() == Tile.TileType.Wall){
            tileGO.GetComponent<SpriteRenderer>().sprite = wallSprite;
        }
        else{
            Debug.Log("Invalid tile type");
        }
    }
    //Utility fuction to get the tile when the world coordinates are known
    public static Tile getTileAtWorldCoord(Vector3 coord){
        int x = Mathf.FloorToInt(coord.x);
        int y = Mathf.FloorToInt(coord.y);

        return  BuildingController.Instance.building.getTileAt(x, y);
    }
    //Utility fuction to get the place object when the world coordinates are known
    public static PlacedObject getObjtAtWorldCoord(Vector3 coord){
        int x = Mathf.FloorToInt(coord.x);
        int y = Mathf.FloorToInt(coord.y);

        return  BuildingController.Instance.building.getTileAt(x, y).getObj();
    }

    public void setRotation(PlacedObject objt, GameObject objtGO, int i){
        Tile tile;
        switch(i){
            case 1:
                tile = objt.getTile();
                if(objt.getHeight() != 1)
                    objtGO.transform.position = new Vector3(tile.getX() + 2, tile.getY());
                else
                    objtGO.transform.position = new Vector3(tile.getX(), tile.getY() + 2);
                objtGO.transform.Rotate(0, 0, 270, Space.World);
                break;
            case 2:
                tile = objt.getTile();
                if(objt.getHeight() != 1)
                    objtGO.transform.position = new Vector3(tile.getX() + 2, tile.getY() + 2);
                else
                    objtGO.transform.position = new Vector3(tile.getX() + 2, tile.getY() + 1);
                objtGO.transform.Rotate(0, 0, 180, Space.World);
                break;
            case 3:
                tile = objt.getTile();
                if(objt.getHeight() != 1)
                    objtGO.transform.position = new Vector3(tile.getX(), tile.getY() + 2);
                else
                    objtGO.transform.position = new Vector3(tile.getX() + 1, tile.getY());
                objtGO.transform.Rotate(0, 0, 90, Space.World);
                break;
        }
    }

    //creates GameObject for the object and pay for it
    public void onPlacedObjtCreated(PlacedObject objt){
        GameObject objtGO = new GameObject();
        Tile tile = objt.getTile();

        objtGO.name = objt.getObjType() + " " + tile.getX() + " " + tile.getY();
        objtGO.transform.position = new Vector3(tile.getX(), tile.getY());
        objtGO.transform.SetParent(this.transform, true);
        //check the type and set the appropriate sprite, roation, and price
        switch(objt.getObjType()){
            case PlacedObject.ObjectType.Wall:
                if(UIController.Instance.getMoney() >= 100){
                    UIController.Instance.subtractMoney(100);
                    objtGO.AddComponent<SpriteRenderer>();
                    setWallSprite(objtGO, tile);
                    adjacentWallCheck(objtGO, tile);
                    //set as obstacle for pathfinding
                    objtGO.AddComponent<NavMeshObstacle>();
                    objtGO.GetComponent<NavMeshObstacle>().size = new Vector3(1,1,0);
                }else
                    UIController.Instance.setInfoBar("Not enough Money");
                break;
            case PlacedObject.ObjectType.Door:
                if(UIController.Instance.getMoney() >= 75){
                    UIController.Instance.subtractMoney(75);
                    objtGO.AddComponent<SpriteRenderer>().sprite = MouseController.Instance.Door[objt.getRotation()];
                }else
                    UIController.Instance.setInfoBar("Not enough Money");
                break;
            case PlacedObject.ObjectType.StudentDesk: 
                if(UIController.Instance.getMoney() >= 100){
                    UIController.Instance.subtractMoney(100);
                    switch(objt.getRotation()){
                        case 0:
                            objtGO.AddComponent<SpriteRenderer>().sprite = deskSprite;
                            break;
                        case 1:
                            objtGO.AddComponent<SpriteRenderer>().sprite = oDeskTopSprite;
                            break;
                        case 2:
                            objtGO.AddComponent<SpriteRenderer>().sprite = deskSpriteFlip;
                            break;
                        case 3:
                            objtGO.AddComponent<SpriteRenderer>().sprite = oDeskBottomSprite;
                            break;
                    }
                }else
                    UIController.Instance.setInfoBar("Not enough Money");
                break;
            case PlacedObject.ObjectType.Desk:
                if(UIController.Instance.getMoney() >= 150){
                    UIController.Instance.subtractMoney(150);
                    switch(objt.getRotation()){
                        case 0:
                            objtGO.AddComponent<SpriteRenderer>().sprite = oDeskSprite;
                            break;
                        case 1:
                            objtGO.AddComponent<SpriteRenderer>().sprite = oDeskTopSprite;
                            break;
                        case 2:
                            objtGO.AddComponent<SpriteRenderer>().sprite = oDeskFlipSprite;
                            break;
                        case 3:
                            objtGO.AddComponent<SpriteRenderer>().sprite = oDeskBottomSprite;
                            break;
                    }
                }else
                    UIController.Instance.setInfoBar("Not enough Money");
                break;
            case PlacedObject.ObjectType.Board:
                if(UIController.Instance.getMoney() >= 100){
                    UIController.Instance.subtractMoney(75);
                    objtGO.AddComponent<SpriteRenderer>().sprite = boardSprite;
                    setRotation(objt, objtGO, objt.getRotation());
                }else
                    UIController.Instance.setInfoBar("Not enough Money");
                break;
            case PlacedObject.ObjectType.Bed:
                if(UIController.Instance.getMoney() >= 100){
                    UIController.Instance.subtractMoney(75);
                    objtGO.AddComponent<SpriteRenderer>().sprite = bedSprite;
                    setRotation(objt, objtGO, objt.getRotation());
                }else
                    UIController.Instance.setInfoBar("Not enough Money");
                break;
            case PlacedObject.ObjectType.Dresser:
                if(UIController.Instance.getMoney() >= 100){
                    UIController.Instance.subtractMoney(75);
                    objtGO.AddComponent<SpriteRenderer>().sprite = dresserSprite;
                    setRotation(objt, objtGO, objt.getRotation());
                }else
                    UIController.Instance.setInfoBar("Not enough Money");
                break;
        }
        objtGO.GetComponent<SpriteRenderer>().sortingLayerName = "Object";
        objt.registerOnChangeCallback(onPlacedObjectChange);
    }

    void onPlacedObjectChange(PlacedObject objt){
        Debug.Log("In onPlacedObhjectChange");
    }

    //creates the sprite for walls depending on the other neighboring tiles
    public void setWallSprite(GameObject objtGO, Tile tile) {
        Tile tu = building.getTileAt(tile.getX() + 1, tile.getY());
        Tile td = building.getTileAt(tile.getX() - 1, tile.getY());
        Tile tl = building.getTileAt(tile.getX(), tile.getY() - 1);
        Tile tr = building.getTileAt(tile.getX(), tile.getY() + 1);


        if (checkWall(tu) && checkWall(td) && checkWall(tl) && checkWall(tr))
            objtGO.GetComponent<SpriteRenderer>().sprite = wallSpriteNSEW;
        else if (checkWall(tu) && checkWall(td) && checkWall(tl))
            objtGO.GetComponent<SpriteRenderer>().sprite = wallSpriteNSEW;
        else if (checkWall(tu) && checkWall(td) && checkWall(tr))
            objtGO.GetComponent<SpriteRenderer>().sprite = wallSprite;
        else if (checkWall(tu) && checkWall(tl) && checkWall(tr))
            objtGO.GetComponent<SpriteRenderer>().sprite = wallSpriteSE;
        else if (checkWall(td) && checkWall(tl) && checkWall(tr))
            objtGO.GetComponent<SpriteRenderer>().sprite = wallSpriteSW;
        else if (checkWall(tu) && checkWall(tr))
            objtGO.GetComponent<SpriteRenderer>().sprite = wallSpriteNE;
        else if (checkWall(td) && checkWall(tr))
            objtGO.GetComponent<SpriteRenderer>().sprite = wallSpriteNW;
        else if (checkWall(tu) && checkWall(tl))
            objtGO.GetComponent<SpriteRenderer>().sprite = wallSpriteSE;
        else if (checkWall(td) && checkWall(tl))
            objtGO.GetComponent<SpriteRenderer>().sprite = wallSpriteSW;
        else if (checkWall(tu) && checkWall(td))
            objtGO.GetComponent<SpriteRenderer>().sprite = wallSprite;
        else if (checkWall(tl) && checkWall(tr))
            objtGO.GetComponent<SpriteRenderer>().sprite = wallSpriteNS;
        else if (checkWall(tl))
            objtGO.GetComponent<SpriteRenderer>().sprite = wallSpriteNS;
        else if (checkWall(tr))
            objtGO.GetComponent<SpriteRenderer>().sprite = wallSpriteNS;
        else if (checkWall(tu))
            objtGO.GetComponent<SpriteRenderer>().sprite = wallSprite;
        else if (checkWall(td))
            objtGO.GetComponent<SpriteRenderer>().sprite = wallSprite;
        else
            objtGO.GetComponent<SpriteRenderer>().sprite = wallSprite;
    }

    //adjusts the image for neighboring tiles when placing a wall
    public void adjacentWallCheck(GameObject objtGO, Tile tile) {
        if (checkWall(building.getTileAt(tile.getX() + 1, tile.getY()))) {
            Debug.Log("Finding" + "Wall" + " " + (tile.getX() + 1) + " " + tile.getY());
            setWallSprite(GameObject.Find("Wall" + " " + (tile.getX() + 1) + " " + tile.getY()),
                building.getTileAt(tile.getX() + 1, tile.getY()));
        }
        if (checkWall(building.getTileAt(tile.getX() - 1, tile.getY()))) {
            Debug.Log("Finding" + "Wall" + " " + (tile.getX() - 1) + " " + tile.getY());
            setWallSprite(GameObject.Find("Wall" + " " + (tile.getX() - 1) + " " + tile.getY()),
                building.getTileAt(tile.getX() - 1, tile.getY()));
        }
        if (checkWall(building.getTileAt(tile.getX(), tile.getY() + 1))) {
            Debug.Log("Finding" + "Wall" + " " + tile.getX() + " " + (tile.getY() + 1));
            setWallSprite(GameObject.Find("Wall" + " " + tile.getX() + " " + (tile.getY() + 1)),
                building.getTileAt(tile.getX(), tile.getY() + 1));
        }
        if (checkWall(building.getTileAt(tile.getX(), tile.getY() - 1))) {
            Debug.Log("Finding" + "Wall" + " " + tile.getX() + " " + (tile.getY() - 1));
            setWallSprite(GameObject.Find("Wall" + " " + tile.getX() + " " + (tile.getY() - 1)),
                building.getTileAt(tile.getX(), tile.getY() - 1));
        }
    }
    
    public bool checkWall(Tile t) {
        if (t == null)
            return false;
        if (t.getObj() != null && t.getObj().getObjType() == PlacedObject.ObjectType.Wall)
            return true;
        else
            return false;
    }
    //open the door with an open door sprite and mark the door as closed
    public void openDoor(Tile t){
        try{
            //check if the door is already open
            bool doorAlreadyOpen = false;
            foreach (Tuple<int, int, GameObject, int, int> doorList in building.openDoorList){
                if(t.getX() == doorList.Item1 && t.getY() == doorList.Item2){
                    doorAlreadyOpen = true;
                    break;
                }
            }
            if(doorAlreadyOpen != true){
                GameObject door = GameObject.Find("Door " + (t.getX()) + " " + t.getY());
                int rotation = t.getObj().getRotation();
                door.GetComponent<SpriteRenderer>().sprite = openDoorSprites[rotation];
                building.openDoorList.Add(new Tuple<int, int, GameObject, int, int>(t.getX(), t.getX(), door, rotation, WorldController.Instance.getTime2()));
            }
        }catch(NullReferenceException e){
            Debug.Log("Error Opening door");
        }
    }
    //Set a close door sprit and remove from list by making a new list without the door
    public void closeDoor(Tuple<int, int, GameObject, int, int> door){
        try{
            door.Item3.GetComponent<SpriteRenderer>().sprite = MouseController.Instance.Door[door.Item4];
            int i = 0;
            List<Tuple<int, int, GameObject, int, int>> tempList = new List<Tuple<int, int, GameObject, int, int>>();
            foreach (Tuple<int, int, GameObject, int, int> searchedDoor in building.openDoorList){
                if(searchedDoor.Item1 != door.Item1 && searchedDoor.Item2 != door.Item2){
                    tempList.Add(searchedDoor);
                    break;
                }
            }
            building.openDoorList = tempList;
        }catch(NullReferenceException e){
            Debug.Log("Error Closing door");
        }
    }

    public void graphTest(){
        TileGraph tg = new TileGraph(BuildingController.Instance.building);
    }

    //Manages selected zone
    public void selectedZone(Vector3 curFramPos){
        Zone z = WorldController.Instance.world.getZoneFromTile(getTileAtWorldCoord(curFramPos));
        if(z == null){
            Debug.Log("Invalid Zone Search");
            UIController.Instance.setInfoBar("No Zone Present");
        }
        else{
            Debug.Log("Creating class from tile at " + getTileAtWorldCoord(curFramPos).getX() + " " +  getTileAtWorldCoord(curFramPos).getY());
            UIController.Instance.setInfoBar("Zone Selected");
            selectedZ = z;
        }
    }

    //clear building of objects
    public void clearBuilding(){
        for (int i = 0; i <= building.getX(); i++){
            for (int j = 0; j <= building.getY(); j++){
                Tile t = building.getTileAt(i, j);
                if(t != null){
                    t.removeHomeTile();
                    if(t.getObj() != null){
                        t.destroyFixture();
                        t.removeObject();
                    }
                    t.destroyFixture();
                }
            }
        }
        building.openDoorList.Clear();
    }
}