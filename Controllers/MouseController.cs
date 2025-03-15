using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class MouseController : MonoBehaviour{

    Tile.TileType buildMode = Tile.TileType.Empty;
    string buildObjectType = null;
    string zoneType;
    string selectType;
    Vector3 lastFramePos;
    Vector2 previewObjectVector;
    Vector3 dragStartPos;
    bool dragFromUI = false;
    bool objectMode = false;
    bool zoneMode = false;
    bool selectMode = false;
    bool removeMode = false;
    bool destroyMode = false;
    bool paintMode = false;
    bool rotatingSprite = false;
    public int rotate = 0;
    public Sprite DefaultSprite;
    
    public GameObject tileMark;
    public int zoomMax = 30;
    GameObject previewObject = null;
    //default color for previews
    Color defaultColor;
    int oldStartX, oldStartY, oldEndX, oldEndY;
    public Sprite[] Wall = new Sprite[4];
    public Sprite[] Desk = new Sprite[4];
    public Sprite[] Door = new Sprite[4];
    public Sprite[] StudentDesk = new Sprite[4];
    public Sprite[] Blackboard = new Sprite[4]; 
    public Sprite[] BedSprites = new Sprite[4]; 
    public Sprite[] DresserSprites = new Sprite[4]; 
    public Sprite ClassDesk, Lab, SpriteBuffer;
    public Tuple <byte, byte, byte> wallPaint = new Tuple<byte, byte, byte>(0,0,0); 
    List<GameObject> dragPreviewGameObjects;
    public static MouseController Instance;
    
    bool deselectZoneMode = false;

    // Start is called before the first frame update
    void Start(){
        Instance = this;
        defaultColor = new Color(33, 229, 69, 134);
        Wall[0] = Resources.Load<Sprite>("images/Wall2");
        Wall[1] = Resources.Load<Sprite>("images/WallNS");
        Desk[0] = Resources.Load<Sprite>("images/desk");
        Desk[1] = Resources.Load<Sprite>("images/deskTop");
        Desk[2] = Resources.Load<Sprite>("images/deskFlip");
        Desk[3] = Resources.Load<Sprite>("images/deskBottom");
        Door[0] = Resources.Load<Sprite>("images/door");
        Door[1] = Resources.Load<Sprite>("images/doorLeft");
        Door[2] = Resources.Load<Sprite>("images/doorFlip");
        Door[3] = Resources.Load<Sprite>("images/doorRight");
        Blackboard[0] = Resources.Load<Sprite>("images/blackboard");
        Blackboard[1] = Resources.Load<Sprite>("images/blackboardRight");
        Blackboard[2] = Resources.Load<Sprite>("images/blackboardFlip");
        Blackboard[3] = Resources.Load<Sprite>("images/blackboardLeft");
        StudentDesk[0] = Resources.Load<Sprite>("images/studentChair");
        StudentDesk[1] = Resources.Load<Sprite>("images/studentChairBottom");
        StudentDesk[2] = Resources.Load<Sprite>("images/studentChairFlip");
        StudentDesk[3] = Resources.Load<Sprite>("images/studentChairTop");
        BedSprites[0] = Resources.Load<Sprite>("images/bedSprite");
        BedSprites[1] = Resources.Load<Sprite>("images/bedSpriteRight");
        BedSprites[2] = Resources.Load<Sprite>("images/bedSpriteFlip");
        BedSprites[3] = Resources.Load<Sprite>("images/bedSpriteLeft");
        DresserSprites[0] = Resources.Load<Sprite>("images/dresser");
        DresserSprites[1] = Resources.Load<Sprite>("images/dresserSide");
        DresserSprites[2] = Resources.Load<Sprite>("images/dresserBack");
        DresserSprites[3] = Resources.Load<Sprite>("images/dresserSide");
        dragPreviewGameObjects = new List<GameObject>();
    }

    // Update is called once per frame
    void Update(){
        //Vector of current frame positi on
        Vector3 curFramPos =  Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        //Open menu with the esc key
        if(Input.GetKeyDown(KeyCode.Escape)){
            UIController.Instance.spawnMainMenu();
            return;
        }
        if(objectMode){
            //Handle rotations
            if(Input.GetKeyDown("r")){
                if(rotate > 2){
                    rotate = 0;
                }
                else
                    rotate++;
                setSpriteBuffer();
                //check if preview for objects need to update due to rotation
                previewObject.GetComponent<SpriteRenderer>().sprite = SpriteBuffer;
                Tile t = BuildingController.Instance.building.getTileAt(Mathf.FloorToInt(curFramPos.x), Mathf.FloorToInt(curFramPos.y));
                if(t != null && previewObject != null){
                    if(t.checkObject(BuildingController.Instance.building.placedObjectPrototypes[buildObjectType]) == false)
                        previewObject.GetComponent<SpriteRenderer>().color = Color.red;
                    else
                        previewObject.GetComponent<SpriteRenderer>().color = Color.green;
                }
            }
            

            //Handle object previews
            if(previewObjectVector != null && !EventSystem.current.IsPointerOverGameObject()){
                if(curFramPos.x != previewObjectVector.x && curFramPos.y != previewObjectVector.y){ 
                    //If a preivew already exists, just move it 
                    //Debug.Log("Preview Sprite Movement");
                    Tile t;
                    if(previewObject != null){
                        //SimplePool.Despawn(previewObject);;
                        t = BuildingController.Instance.building.getTileAt(Mathf.FloorToInt(curFramPos.x), Mathf.FloorToInt(curFramPos.y));
                        if(t != null)
                            previewObject.transform.position = new Vector3(t.getX(), t.getY(), 0);
                    }
                    else{
                        t = BuildingController.Instance.building.getTileAt(Mathf.FloorToInt(curFramPos.x), Mathf.FloorToInt(curFramPos.y));
                        if(t != null && tileMark != null){
                            Debug.Log("Set Sprite");
                            previewObject = SimplePool.Spawn(tileMark, new Vector3(t.getX(), t.getY(), 0), Quaternion.identity);
                            previewObject.GetComponent<SpriteRenderer>().sprite = SpriteBuffer;
                            previewObject.transform.SetParent(this.transform, true);
                        }
                    }
                    if(t != null && t.checkObject(BuildingController.Instance.building.placedObjectPrototypes[buildObjectType]) == false){
                        previewObject.GetComponent<SpriteRenderer>().color = Color.red;                
                    }
                    else if(previewObject != null)
                        previewObject.GetComponent<SpriteRenderer>().color = Color.green;
                }
                previewObjectVector = new Vector2(curFramPos.x, curFramPos.y);
            }
        }
        //check if right mouse is click to exit mouse modes
        if(Input.GetMouseButton(1)){
            if(zoneMode){
                zoneMode = false;
            }
            else if(previewObject != null){
                SimplePool.Despawn(previewObject);
                objectMode = false;
                previewObject = null;
            }
            if(UIController.Instance.markerHolder.transform.childCount != 0){
                UIController.Instance.deleteZoneMarkers();
                deselectZoneMode = true;
            }
            
            removeMode = false;
            return;
        }
        checkScroll(curFramPos);
        checkZoom();

        //check left mouse click
        if(Input.GetMouseButton(0)){
            if(removeMode){
                //if(Mathf.FloorToInt(curFramPos.x) >= 0 || Mathf.FloorToInt(curFramPos.x)
                Debug.Log("Removing object");
                Tile t = BuildingController.Instance.building.getTileAt(Mathf.FloorToInt(curFramPos.x), Mathf.FloorToInt(curFramPos.y));
                if(t != null)
                    t.removeObject();
            }
            else if(paintMode){
                int x = Mathf.FloorToInt(curFramPos.x);
                int y = Mathf.FloorToInt(curFramPos.y);
                Debug.Log("Painting Wall at " + x + " " + y);
                GameObject go = GameObject.Find("Wall " + x + " " + y);
                if(go != null)
                    go.GetComponent<SpriteRenderer>().color = new Color32(wallPaint.Item1, wallPaint.Item2, wallPaint.Item3, 255);
            }
            else if(selectMode){
                handleSelect(curFramPos);
                selectMode = false;
                return;
            }
            else if(deselectZoneMode){
                Debug.Log("DeselectZone");
                WorldController.Instance.world.deleteZone(Mathf.FloorToInt(curFramPos.x), Mathf.FloorToInt(curFramPos.y));
                UIController.Instance.deleteZoneMarkers();
                WorldController.Instance.world.displayZones();
            }
            else
                drag(curFramPos);
        }
        else
            drag(curFramPos);

        /*//Moves object preview
        Tile tileUnderMouse = BuildingController.getTileAtWorldCoord(curFramPos);
        if(tileUnderMouse != null){
            Vector3 cursorPos = new Vector3(tileUnderMouse.getX(), tileUnderMouse.getY(), 0);
            tileMark.transform.position = cursorPos;
            tileMark.SetActive(true);
        }
        else{
            tileMark.SetActive(false);
        }*/
        lastFramePos = curFramPos;
    }

    private void checkScroll(Vector3 curFramPos){
        //check WASD scroll
        float translationX = Input.GetAxis("Horizontal");
        float translationY = Input.GetAxis("Vertical");

        //Screen drag with middle or right mouse button
        if(Input.GetMouseButton(1) || Input.GetMouseButton(2)){
                Vector3 diff = lastFramePos - curFramPos;
                Camera.main.transform.Translate(diff);
        }
        
        Camera.main.transform.Translate(translationX, translationY, 0);;
    }

    //Check for camera zoom with mousewheel
     private void checkZoom()
    {
        if(EventSystem.current.IsPointerOverGameObject()){
            return;
        }
        //Zoom in
        if (Input.GetAxis("Mouse ScrollWheel") > 0.0 && Camera.main.orthographicSize > 5){
            Camera.main.orthographicSize --;
        }
        //Zoom out
        if (Input.GetAxis("Mouse ScrollWheel") < 0.0 && Camera.main.orthographicSize < zoomMax){
            Camera.main.orthographicSize ++;
        }
        if(Input.GetAxis("Mouse ScrollWheel") != 0.0){
            Debug.Log(Input.GetAxis("Mouse ScrollWheel"));
        }
    }

    private void drag(Vector3 curFramPos){
        //Check if mouse over UI element
        if(EventSystem.current.IsPointerOverGameObject()){
            dragFromUI = true;
            return;
        }
        //Check if there some mode is enabled  *must be updates with new modes*
        if(buildMode == Tile.TileType.Empty && !(objectMode) && !(zoneMode) && !(selectMode)){
            return;
        }

        //Start Drag or click
        if(Input.GetMouseButtonDown(0)){
            if(destroyMode){
                BuildingController.Instance.building.getTileAt(Mathf.FloorToInt(curFramPos.x), Mathf.FloorToInt(curFramPos.y)).destroyFixture();
            }
            dragFromUI = false;
            dragStartPos = curFramPos;
        }

        //flip dimentions 
        int startX = Mathf.FloorToInt(dragStartPos.x);
        int endX = Mathf.FloorToInt(curFramPos.x);
        int startY = Mathf.FloorToInt(dragStartPos.y);
        int endY = Mathf.FloorToInt(curFramPos.y);

        if(endX < startX){
            int temp = endX;
            endX = startX;
            startX = temp;
        }
        if(endY < startY){
            int temp = endY;
            endY = startY;
            startY = temp;
        }
        /*if(endX == oldEndX && startX == oldStartX && endY == oldStartY){
            return;
        }*/

        //Preview While Dragging
        if(curFramPos != lastFramePos){
            cleanDragPreview();
            setPreview(endX, endY, startX, startY);
        }
/*
        //End Drag
        if(Input.GetMouseButtonUp(0) && selectMode){
            handleSelect(curFramPos);
            selectMode = false;
            return;
        }*/
        if(Input.GetMouseButtonUp(0) && dragFromUI == false){
            Debug.Log("End drag");
            //checks for valid zone, adds if valid
            if(zoneMode){
                Debug.Log("Zone check");
                if(BuildingController.Instance.building.checkZone(startX, startY, endX, endY, zoneType))
                    BuildingController.Instance.building.addZone(startX, startY, endX, endY, zoneType);
                else
                    cleanDragPreview();
            }
            else{
            //sets type of each tile in drag area
                setDrag(endX, endY, startX, startY);
            }
        }
    }

    //Preview While Dragging
    public void setPreview(int endX, int endY, int startX, int startY){
        if(Input.GetMouseButton(0) && dragFromUI == false){
            for (int i = startX; i <= endX; i++){
                for (int j = startY; j <= endY; j++){
                    Tile t = BuildingController.Instance.building.getTileAt(i, j);
                    if(t != null){
                        GameObject go = SimplePool.Spawn(tileMark, new Vector3(i, j, 0), Quaternion.identity);
                        if(objectMode != false && buildObjectType.Equals("Wall") != false && startY != endY)
                            go.GetComponent<SpriteRenderer>().sprite = Wall[1];
                        else
                            go.GetComponent<SpriteRenderer>().sprite = SpriteBuffer;
                        if(zoneMode)
                            go.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);
                        else if(t.getObj() != null || t.getHomeTile() != null)
                            go.GetComponent<SpriteRenderer>().color = Color.red;
                        else
                            go.GetComponent<SpriteRenderer>().color = Color.green;
                        go.transform.SetParent(this.transform, true);
                        dragPreviewGameObjects.Add(go);
                    }
                }
            }
        }
        /*oldStartX = startX;
        oldStartY = startY;
        oldEndX = endX;
        oldEndY = endY;*/
    }

    //sets type of each tile or places an object in drag area
    public void setDrag(int endX, int endY, int startX, int startY){
        Debug.Log("Setting Drag");
        for (int i = startX; i <= endX; i++){
            for (int j = startY; j <= endY; j++){
                Tile t = BuildingController.Instance.building.getTileAt(i, j);
                if(t != null){
                    if(objectMode){
                        //assign placedObjec to a tile
                        Debug.Log("Attempting to place object");
                        BuildingController.Instance.building.placeObject(buildObjectType, t, rotate);
                    }
                    else{
                        //assign tile type to a tile
                        if(t.getType() != buildMode)
                            t.setType(buildMode);
                    }
                }
            }
        }
        if(objectMode && !(buildObjectType.Equals("Wall")))
           return;
        else
            BuildingController.Instance.building.refreshGraph();  
    }
    public void cleanDragPreview(){
        //Clean up drag previews
        for(int i = 0; i < dragPreviewGameObjects.Count; i++)
            SimplePool.Despawn(dragPreviewGameObjects[i]);
        dragPreviewGameObjects.Clear();
    }

    //handles a select mode, such as selecting an object or zone
    public void handleSelect(Vector3 curFramPos){ 
        switch(selectType){
            case "Zone":
                BuildingController.Instance.selectedZone(curFramPos);
                CourseUIController.Instance.setInitProfConflict();
                TimeSlotController.Instance.setInvalidSlots();
                TimeSlotController.Instance.refreshSlotList();
                break; 
        }
    }
    //Handles setting the sprite of the preview
    public void setSpriteBuffer(){
        switch(buildObjectType){
            case "Wall":
                SpriteBuffer = Wall[0];
                break;
            case "Desk":
                SpriteBuffer = Desk[rotate];
                break;
            case "Door":
                SpriteBuffer = Door[rotate];
                break;
            case "Board":
                SpriteBuffer = Blackboard[rotate];
                break;
            case "StudentDesk":
                SpriteBuffer = StudentDesk[rotate];
                break;
            case "Bed":
                SpriteBuffer = BedSprites[rotate];
                break;
            case "Dresser":
                SpriteBuffer = DresserSprites[rotate];
                break;
        }
    }

    //Sets mouse modes
    public void SetMouseModeAsFloor(){
        objectMode = false;
        zoneMode = false;
        selectMode = false;
        removeMode = false;
        buildMode = Tile.TileType.Floor;
        deselectZoneMode = false;
        paintMode = false;
        UIController.Instance.deleteZoneMarkers();
        //set preview sprite
        if(previewObject != null){
            previewObject.GetComponent<SpriteRenderer>().sprite = DefaultSprite;
            SimplePool.Despawn(previewObject);;
            previewObject = null;
        }
        SpriteBuffer = DefaultSprite;
    }
    public void SetMouseModeAsObject(string objectType){
        destroyMode = false;
        objectMode = true;
        zoneMode = false;
        selectMode = false;
        removeMode = false;
        buildObjectType = objectType;
        rotate = 0;
        deselectZoneMode = false;
        paintMode = false;
        UIController.Instance.deleteZoneMarkers();
        //set preview sprite
        if(previewObject != null){
            SimplePool.Despawn(previewObject);;
            previewObject = null;
        }
        setSpriteBuffer();
    }
    public void SetMouseModeAsZone(string type){
        destroyMode = false;
        zoneMode = true;
        objectMode = false;
        selectMode = false;
        removeMode = false;
        zoneType = type;
        deselectZoneMode = false;
        paintMode = false;
        //set preview sprite
        if(previewObject != null){
            previewObject.GetComponent<SpriteRenderer>().sprite = DefaultSprite;
            SimplePool.Despawn(previewObject);;
        }
        SpriteBuffer = DefaultSprite;
        UIController.Instance.deleteZoneMarkers();
    }
    public void SetMouseModeAsSelect(string type){
        destroyMode = false;
        zoneMode = false; 
        objectMode = false;
        removeMode = false;
        selectMode = true;
        selectType = type;
        paintMode = false;
        if(selectType.Equals("Prof")){
            CharacterController.Instance.selectType = "Prof";
        }
        deselectZoneMode = false;
        UIController.Instance.deleteZoneMarkers();
    }
    public void SetMouseModeAsRemove(){
        destroyMode = false;
        zoneMode = false;
        objectMode = false;
        selectMode = false;
        removeMode = true;
        deselectZoneMode = false;
        paintMode = false;
        UIController.Instance.deleteZoneMarkers();
        
    }
    public void SetMouseModeAsDestroy(){
        destroyMode = true;
        zoneMode = false;
        objectMode = false;
        selectMode = false;
        removeMode = false;
        buildMode = Tile.TileType.Grass;
        deselectZoneMode = false;
        paintMode = false;
        UIController.Instance.deleteZoneMarkers();
    }
    public void SetMouseModeAsDeZone(){
        WorldController.Instance.world.displayZones();
        destroyMode = false;
        zoneMode = false;
        objectMode = false;
        selectMode = false;
        removeMode = false;
        deselectZoneMode = true;
        paintMode = false;
    }
    public void SetMouseModeAsPaint(){
        destroyMode = false;
        zoneMode = false;
        objectMode = false;
        selectMode = false;
        removeMode = false;
        deselectZoneMode = false;
        paintMode = true;
    }
}