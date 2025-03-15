using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Linq;
using UnityEngine;

public class Character: IXmlSerializable{
    public enum CharType{Student, Grad, Prof};
    CharType type;
    Tile currTile;
    Tile destTile;
    int test = 0;
    GameObject charGO;
    public Path_AStar path;
    float movementPercent;
    float x;
    float y;
    int buildingId;
    int id;
    int skill;
    int major;
    //year
    public int year;
    int occupiedTill = 0;
    int spriteId = 0;
    int headId = 0;
    public bool oddOccupiedTime;
    public string name;
    int salary;
    List<int> awards = null;
    public List<int> skilledCourses = null;
    List<int> rangeList = null;
    public bool active = false;
    public List<SectionLite> sectionList, sectionListS; 
    public List<Section> sectionListBuf = new List<Section>();
    public List<Tuple<int, int, SectionLite>> timeList;

    //basic professor
    public Character(Tile t, int major){
       this.currTile = t;
       this.destTile = t;
       this.major = major;
       this.movementPercent = 0f;
       this.buildingId = 1;
       sectionList = new List<SectionLite>();
       sectionListS = new List<SectionLite>();
       awards = new List<int>();
       timeList = new List<Tuple<int, int, SectionLite>>();
    }

    //student with set year
    public Character(Tile t, int major, int year){
       this.currTile = t;
       this.destTile = t;
       this.major = major;
       this.year = year;
       this.movementPercent = 0f;
       this.buildingId = 1;
       sectionList = new List<SectionLite>();
       sectionListS = new List<SectionLite>();
       timeList = new List<Tuple<int, int, SectionLite>>();
    }

    //update movement
    public void update(float time){
        if(currTile == destTile && active){
            CharacterController.Instance.markAsToMakeInactive(this);
            if(destTile.getX() == 0 && destTile.getY() == 0)
                //GameObject.Destroy(charGO);
                charGO.SetActive(false);
            return;
        }
        //Calculate distance for step
        float totalDist = Mathf.Sqrt(Mathf.Pow(currTile.getX() - destTile.getX(), 2) + 
            Mathf.Pow(currTile.getY() - destTile.getY(), 2));
        float distThisFrame = time * 2 / totalDist;
        movementPercent += distThisFrame * WorldController.Instance.timeScale;
        //Debug.Log("Movement of id " + id + ":" + movementPercent);

        //Check if target reached.
        if(movementPercent >= 1){
            //if its reached, update the current tile, then either try to get a new destination along the path
            //or edit character for its destination and set a new target
            //also check if a door need to open
            currTile = destTile;
            if(currTile.getObj() != null && currTile.getObj().getObjType() == PlacedObject.ObjectType.Door){
                Debug.Log("Opening door");
                BuildingController.Instance.openDoor(destTile);
            }
            try{
                destTile = path.Dequeue();
            }
            catch(InvalidOperationException e){
                if(currTile.getObj() != null){
                    switch(currTile.getObj().getRotation()){
                        case 0:
                            setRotation(3);
                            break;
                        case 1:
                            setRotation(0);
                            break;
                        case 2:
                            setRotation(1);
                            break;
                        case 3:
                            setRotation(2);
                            break;
                    }
                }
                if(currTile.getHomeTile() != null){
                    switch(currTile.getHomeTile().getObj().getRotation()){
                        case 0:
                            setRotation(3);
                            break;
                        case 1:
                            setRotation(0);
                            break;
                        case 2:
                            setRotation(1);
                            break;
                        case 3:
                            setRotation(2);
                            break;
                    }
                }
            }
            catch(NullReferenceException e){
                Debug.Log("Changed back to building with moving characters");
                active = false;
            }
            
            //rotate character to match its movement
            if(destTile.getX() < currTile.getX()){
                setRotation(1);
                Debug.Log("Found rotation at 2 at" + x + " " + y);
            }
            else if(destTile.getX() > currTile.getX()){
                setRotation(3);
                Debug.Log("Found rotation at 4 at" + x + " " + y);
            }
            else if(destTile.getY() > currTile.getY()){
                setRotation(2);
                Debug.Log("Found rotation at 3 at" + x + " " + y);
            }
            else if(destTile.getY() < currTile.getY()){
                setRotation(0);
                Debug.Log("Found rotation at 1 at" + x + " " + y);
            }
            movementPercent = 0;
        }

        //Set new x and y
        this.x = Mathf.Lerp( currTile.getX(), destTile.getX(), movementPercent);
        this.y = Mathf.Lerp( currTile.getY(), destTile.getY(), movementPercent);
        if(charGO != null){
            charGO.transform.position = new Vector3(this.x, this.y);
        }
    }

    //set the head sprite as the character rotates
    public void setRotation(int rotation){    
        Debug.Log("headId:" + headId);
        charGO.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = 
            CharacterController.Instance.headImages[4 * (headId - 1) + rotation];
    }

    //sets the courses a professor can teach
    public void setSkilledCourses(List<int> list){
        List<int> courses = new List<int>();
        foreach (int i in list){
            if(!(courses.Contains(i))){
                courses.Add(i);
            }
        }
        this.skilledCourses = courses;
    }

    public void addSectionList(Section s){
        if (sectionListBuf == null){
            sectionListBuf = new List<Section>();
        }
        sectionListBuf.Add(s);
    }

    public void addSkilled(int i){
        if (skilledCourses == null){
            skilledCourses = new List<int>();
        }
        skilledCourses.Add(i);
    }

    public void setDest(Tile t){
       this.destTile = t;
    }
    public Tile getTile(){
       return this.currTile;
    }
    public void setTile(Tile t){
        this.currTile = t;
    }
    public int getBuilding() {
        return this.buildingId;
    }
    public void setBuilding(int i){
        buildingId = i;
    }
    public void setGO(GameObject go){
       this.charGO = go;
    }
    public GameObject getGO(){
        return charGO;
    }
    public void setType(CharType c){
       this.type = c;
    }
    public CharType getType(){
       return this.type;
    }
    public void setPath(Path_AStar path){
       this.path = path;
        try{
            this.destTile = path.Dequeue();
            Debug.Log("Set destination at " + destTile.getX() + " " + destTile.getY() + "from " + currTile.getX() + " " + currTile.getY());
            Debug.Log("Total path Length:" + path.Length());
        }
        catch(InvalidOperationException e){
            Debug.Log("No Path for " + id + " going to " + path.getDest());
        }
    }
    public int getMajor(){
       return this.major;
    }
    public void setName(string s){
       this.name = s;
    }
    public int getId(){
       return this.id;
    }
    public void setId(int i){
       this.id = i;
    }
    public int getSkill(){
        return this.skill;
    }
    public void setSkill(int i){
        this.skill = i;
    }
    public int getSpriteId(){
       return this.spriteId;
    }
    public void setSpriteId(int i){
       this.spriteId = i;
    }
    public int getHeadId(){
        return this.headId;
    }
    public void setHeadId(int i){
        Debug.Log("setting head id to " + i);
       this.headId = i;
    }
    public void setAward(int i){
        awards.Add(i);
    }
    public List<int> getAwards(){
        return this.awards;
    }
    public void setSalary(int s){
        this.salary = s;
    }
    public int getSalary(){
        return this.salary;
    }
    public void setOccupiedTill(int time){
        occupiedTill = time;
    }
    //add section, used by prof when a section is created
    public void addSection(Section s){
        sectionList.Add(new SectionLite(s.getCourseId(), s.sectionId, s.getZone()));
    }

    //add section to the spring for professors, used by prof when a section is created
    public void addSectionS(Section s){
        sectionListS.Add(new SectionLite(s.getCourseId(), s.sectionId, s.getZone()));
    }
    //add sectionlist, used by reader
    public void addSection(SectionLite s){
        sectionList.Add(s);
    }
    //add sectionlist to spring for professor, used by reader
    public void addSectionS(SectionLite s){
        sectionListS.Add(s);
    }
    public List<SectionLite> getSections(){
        return sectionList;
    }
    public List<SectionLite> getSectionsS(){
        return sectionListS;
    }
    public void removeSection(Section s){
        sectionList.Remove(new SectionLite(s.getCourseId(), s.sectionId, s.getZone()));
    }
    public void removeSectionS(Section s){
        sectionListS.Remove(new SectionLite(s.getCourseId(), s.sectionId, s.getZone()));
    }

    public void addRange(int id){
        rangeList.Add(id);
    }

    public bool checkRange(int id){
        if(rangeList.Contains(id))
            return true;
        else
            return false;
    }

    public bool rangeEmpty(){
        if(rangeList == null)
            return true;
        else if(rangeList.Count == 0)
            return true;
        else
            return false;
    }

    //assing the section buf as the section list after scheduling checks
    public void assignBuf(){
        if(type != CharType.Prof){
            sectionList.Clear();
            sectionListS.Clear();
            Debug.Log("Clearing sectionList with buf size:" + sectionListBuf.Count);
        }
        if(/*type == CharType.Prof &&*/ ScheduleController.Instance.semFall == false){
            foreach(Section s in sectionListBuf){
                sectionListS.Add(new SectionLite(s.getCourseId(), s.getSectionId(), s.getZone()));
            }
        }
        else{
            foreach(Section s in sectionListBuf){
                sectionList.Add(new SectionLite(s.getCourseId(), s.getSectionId(), s.getZone()));
            }
        }
    }
    //manage list of timeslots to make a sorted schedule
    public void makeTimeList(){
        timeList.Clear();
        //manage for spring
        if(ScheduleController.Instance.semFall != true){
            foreach(SectionLite sl in sectionListS){
                try{
                    foreach(TimeSlot ts in AcedemicController.Instance.courses[sl.courseId].sectionsS[sl.sectionId].getSlotList())
                        timeList.Add(new Tuple<int, int, SectionLite>(ts.getDay(), ts.getStartTime(), sl));
                    }
                catch (ArgumentOutOfRangeException e){
                    Debug.Log("Error sorting times on next semester for " +  AcedemicController.Instance.courses[sl.courseId].courseName);
                }
            }
        }
        //manage for fall
        else{
            foreach(SectionLite sl in sectionList){
                try{
                    foreach(TimeSlot ts in AcedemicController.Instance.courses[sl.courseId].sections[sl.sectionId].getSlotList())
                        timeList.Add(new Tuple<int, int, SectionLite>(ts.getDay(), ts.getStartTime(), sl));
                    }
                catch (ArgumentOutOfRangeException e){
                    Debug.Log("Error sorting times on next semester for " +  AcedemicController.Instance.courses[sl.courseId].courseName);
                }
            }
        }
        timeList.Sort((y, x) => {
            int result = y.Item1.CompareTo(x.Item1);
            return result == 0 ? y.Item2.CompareTo(x.Item2) : result;
        });
        Debug.Log("Made timeList of size: " + timeList.Count);
    }
    public void addToTimeList(Section s){
        Debug.Log("Adding TimeSlots to prof");
        foreach(TimeSlot ts in s.getSlotList())
            timeList.Add(new Tuple<int, int, SectionLite>(ts.getDay(), ts.getStartTime(), new SectionLite(s.getSectionId(), s.getCourseId(), s.getZone())));
    }
    public void sortTime(){
        timeList.Sort((y, x) => {
            int result = y.Item1.CompareTo(x.Item1);
            return result == 0 ? y.Item2.CompareTo(x.Item2) : result;
        });
        Debug.Log("Made timeList of size: " + timeList.Count);
    }

    //sets the next destination for the character
    public void setNextDest() {
       
    if(timeList.Count != 0 && currTile != destTile && occupiedTill == 0)
            return;
        Debug.Log("Setting New Dest" + timeList.Count);
        int time1 = 20;
        int time2 = 20;
        //check if the character is occupied
        if(occupiedTill == 0 || occupiedTill == 99){
            if(timeList.Count == 0){
                return;
            }
            //check if the day or building is incorrect
            if(timeList[0].Item1 != WorldController.Instance.getDay() && destTile != null || buildingId != timeList[0].Item3.zone.building.id  + 1){
                //if not currect day send character to spawn tile at the end of whatever they are doing at the last location
                Debug.Log("Wrong day/building. Next class: " + timeList[0].Item1 +  " Current course ends at: " + timeList[timeList.Count - 1].Item2);
                setPath(new Path_AStar(BuildingController.Instance.building, currTile, BuildingController.Instance.building.getTileAt(0, 0)));
                //if this is the fist setDest of a semester, move for first class isntead of waiting for current class to end
                if(timeList[timeList.Count - 1].Item1 > timeList[0].Item1){
                    occupiedTill = 7 + (timeList[0].Item2 / 2) + timeList[0].Item2 % 2;
                    if(timeList[0].Item2 % 2 == 0)
                            oddOccupiedTime = true;
                }
                else{
                int lastMoveTime = timeList[timeList.Count - 1].Item2;
                occupiedTill = 7 + (lastMoveTime / 2) + lastMoveTime % 2;
                occupiedTill += timeList[timeList.Count - 1].Item3.getLength() / 2;
                    //check if not even hour for moving from last location
                    if(timeList[timeList.Count - 1].Item3.getLength() == 3)
                        oddOccupiedTime = true;
                }
                //move the timeLists if the characters are here due to different buildings
                if(buildingId != timeList[0].Item3.zone.building.id + 1){
                    buildingId = timeList[0].Item3.zone.building.id + 1;
                    Debug.Log("Wrong /building. Next class: " + timeList[0].Item1 +  " Current course ends at: " + 
                        occupiedTill + " new building id:" + timeList[0].Item3.zone.building.id);
                    timeList.Add(new Tuple<int, int, SectionLite>(timeList[0].Item1, timeList[0].Item2, timeList[0].Item3));
                    timeList.RemoveAt(0);
                }
                else{
                    Debug.Log("Wrong day. Next class: " + timeList[0].Item1 +  " Current course ends at: " + timeList[timeList.Count - 1].Item2);
                }
            }
            else{
                //set path to next destination
                Debug.Log("Setting New Path at " + timeList[0].Item2 + "from " + currTile.getX() + " " + currTile.getY());
                
                //normal option for next class
                    if(timeList[0].Item3.zone.getId() != timeList[timeList.Count - 1].Item3.zone.getId() /*&& (currTile.getX() != 0 && currTile.getY() != 0)*/){
                        if(type == CharType.Student)
                            setPath(new Path_AStar(BuildingController.Instance.building, currTile, timeList[0].Item3.zone.getNextSeat()));
                        else if(type == CharType.Prof)
                             setPath(new Path_AStar(BuildingController.Instance.building, currTile, timeList[0].Item3.zone.getDesk()));
                        occupiedTill = 7 + (timeList[0].Item2 / 2) + timeList[0].Item2 % 2;
                        if(timeList[0].Item2 % 2 == 0)
                            oddOccupiedTime = true;
                        Debug.Log("Setting New Path at " + timeList[0].Item2 + "from " + currTile.getX() + " " + currTile.getY());
                        timeList.Add(new Tuple<int, int, SectionLite>(timeList[0].Item1, timeList[0].Item2, timeList[0].Item3));
                        timeList.RemoveAt(0);
                    }
                //if the zone is the same as last destination, but its okay because the character just spawned in
                    else if(timeList[0].Item3.zone.getId() == timeList[timeList.Count - 1].Item3.zone.getId() && currTile.getX() == 0 && currTile.getY() == 0){
                        if(type == CharType.Student)
                            setPath(new Path_AStar(BuildingController.Instance.building, currTile, timeList[0].Item3.zone.getNextSeat()));
                        else if(type == CharType.Prof)
                             setPath(new Path_AStar(BuildingController.Instance.building, currTile, timeList[0].Item3.zone.getDesk()));
                        occupiedTill = 7 + (timeList[0].Item2 / 2) + timeList[0].Item2 % 2;
                        if(timeList[0].Item2 % 2 == 0)
                            oddOccupiedTime = true;
                        Debug.Log("Setting New Path from spawn at " + timeList[0].Item2 + "from " + currTile.getX() + " " + currTile.getY());
                        timeList.Add(new Tuple<int, int, SectionLite>(timeList[0].Item1, timeList[0].Item2, timeList[0].Item3));
                        timeList.RemoveAt(0);
                    }
                //if the zone is the same as last destination, but the class starts right after, skip this current timelist, and set next dest again
                    else if(timeList[0].Item3.zone.getId() == timeList[timeList.Count - 1].Item3.zone.getId() && timeList[0].Item1 == WorldController.Instance.getDay()
                            && timeList[0].Item2 == timeList[timeList.Count - 1].Item2 + timeList[0].Item3.getLength()){
                        
                        timeList.Add(new Tuple<int, int, SectionLite>(timeList[0].Item1, timeList[0].Item2, timeList[0].Item3));
                        timeList.RemoveAt(0);
                        setNextDest();
                        int lastOccupiedTill = 7 + (timeList[timeList.Count - 2].Item2 / 2) + timeList[timeList.Count - 2].Item2 % 2;
                        occupiedTill = lastOccupiedTill + ((timeList[timeList.Count - 1].Item3.getLength() + timeList[timeList.Count - 2].Item3.getLength()) / 2);
                        if(timeList[timeList.Count - 1].Item3.getLength() + timeList[timeList.Count - 2].Item3.getLength() / 2 == 5)
                            oddOccupiedTime = true;
                        }
                //If the character should go off screen due to no clas for a while
                    else{
                        setPath(new Path_AStar(BuildingController.Instance.building, currTile, BuildingController.Instance.building.getTileAt(0, 0)));
                        int lastOccupiedTill = 7 + (timeList[timeList.Count - 1].Item2 / 2) + timeList[timeList.Count - 1].Item2 % 2;
                        occupiedTill = lastOccupiedTill + (timeList[timeList.Count - 1].Item3.getLength() / 4);
                        if(timeList[timeList.Count - 1].Item3.getLength() == 3){
                             oddOccupiedTime = true;
                        }
                        
                        Debug.Log("Setting New Path at " + timeList[0].Item2 + "from " + currTile.getX() + " " + currTile.getY() + " to spawn");
                    }
                
            }
        }
    }

    public void getBuildingChangeDest(){
        Tile dTile = null;
        if(timeList.Count != 0){
            if(type  == CharType.Student || type  == CharType.Grad)
                dTile = timeList[timeList.Count - 1].Item3.zone.getNextSeat();
            else
                dTile = timeList[timeList.Count - 1].Item3.zone.getDesk();
        }
        if(dTile != null){
            charGO.transform.position = new Vector3(dTile.getX(), dTile.getY());
            Debug.Log("Setting new position for character at building " + buildingId);
        }
        if(currTile.getObj() != null){
            switch(currTile.getObj().getRotation()){
                case 0:
                    setRotation(3);
                    break;
                case 1:
                    setRotation(0);
                    break;
                case 2:
                    setRotation(1);
                    break;
                case 3:
                    setRotation(2);
                    break;
            }
        }
        if(currTile.getHomeTile() != null){
            switch(currTile.getHomeTile().getObj().getRotation()){
                case 0:
                    setRotation(3);
                    break;
                case 1:
                    setRotation(0);
                    break;
                case 2:
                    setRotation(1);
                     break;
                case 3:
                    setRotation(2);
                    break;
            }
        }
    }

    //finds if there is a destination waiting
    public bool destBuf(){
        if (destTile.getX() != currTile.getX() || destTile.getY() != currTile.getY())
            return true;
        else
            return false;
    }

    //checks if it is time to go to the next dest
    public void goToNextDest() {
        Debug.Log("goToNextDest: at timetill: " + occupiedTill + "when real time is" + WorldController.Instance.getTime2());
        if(currTile != destTile && WorldController.Instance.getTime2() >= occupiedTill) {
            if(oddOccupiedTime){
                WorldController.Instance.addOddTimingCharacter(this);
                return;
            }
            Debug.Log("Making active");
            CharacterController.Instance.makeActive(this);
            occupiedTill = 0;
        }
        //if the current tile and destination tile are equal, make active if they are the spawn tile (0,0)
        else if(WorldController.Instance.getTime2() >= occupiedTill && currTile.getX() == 0 && currTile.getY() == 0){
            if(oddOccupiedTime){
                WorldController.Instance.addOddTimingCharacter(this);
                return;
            }
            Debug.Log("Making active");
            CharacterController.Instance.makeActive(this);
            occupiedTill = 0;
        }
        else if(WorldController.Instance.getTime2() == occupiedTill){
            Debug.Log("Time to move but dest not set currectly");
        }
    }

    public XmlSchema GetSchema(){
        return null;
    }
    //write save data for character
    public void WriteXml(XmlWriter writer){
        //general character info
        writer.WriteAttributeString("name", name);
        writer.WriteAttributeString("curX", currTile.getX().ToString());
        writer.WriteAttributeString("curY", currTile.getX().ToString());
        writer.WriteAttributeString("destX", destTile.getX().ToString());
        writer.WriteAttributeString("destY", destTile.getY().ToString());
        writer.WriteAttributeString("major", major.ToString());
        writer.WriteAttributeString("headId", headId.ToString());
        writer.WriteAttributeString("bodyId", spriteId.ToString());

        //If char is a prof, write the age, skill, and any awards
        if(type == CharType.Prof){
            writer.WriteAttributeString("Age", year.ToString());
            writer.WriteAttributeString("Skill", skill.ToString());
            writer.WriteAttributeString("Salary", this.salary.ToString());
            foreach(int i in awards){
                writer.WriteStartElement("Award");
                writer.WriteAttributeString("id", i.ToString());
                writer.WriteEndElement();
            }
            if(skilledCourses != null){
                foreach(int i in skilledCourses){
                    writer.WriteStartElement("Course");
                    writer.WriteAttributeString("id", i.ToString());
                    writer.WriteEndElement();
                }
            }
        }
        else{
            writer.WriteAttributeString("year", year.ToString());
            Debug.Log("writing sectionlist of size: " + sectionList.Count + " for " + name);
        }
        //write class info
        if(sectionList != null){
            foreach(SectionLite s in sectionList){
                writer.WriteStartElement("Section");
                writer.WriteAttributeString("cId", s.courseId.ToString());
                writer.WriteAttributeString("sId", s.sectionId.ToString());
                writer.WriteAttributeString("zId", s.zone.getId().ToString());
                writer.WriteEndElement();
            }
            foreach(SectionLite s in sectionListS){
                writer.WriteStartElement("SectionS");
                writer.WriteAttributeString("cId", s.courseId.ToString());
                writer.WriteAttributeString("sId", s.sectionId.ToString());
                writer.WriteAttributeString("zId", s.zone.getId().ToString());
                writer.WriteEndElement();
            }
            foreach(Tuple<int, int, SectionLite> t in timeList){
                writer.WriteStartElement("tList");
                writer.WriteAttributeString("day", t.Item1.ToString());
                writer.WriteAttributeString("time", t.Item2.ToString());
                writer.WriteAttributeString("cId", t.Item3.courseId.ToString());
                writer.WriteAttributeString("sId", t.Item3.sectionId.ToString());
                writer.WriteAttributeString("zId", t.Item3.zone.getId().ToString());
                writer.WriteEndElement();
            }
        }
        //write any path and destination info
        if((destTile.getX() != currTile.getX() || destTile.getY() != currTile.getY()) && path != null){
            writer.WriteStartElement("Destination");
            writer.WriteAttributeString("x", destTile.getX().ToString());
            writer.WriteAttributeString("y", destTile.getY().ToString());
            writer.WriteAttributeString("time", occupiedTill.ToString());
            Tile[] pathCopy = new Tile[path.Length()];
            path.getPath().CopyTo(pathCopy, 0);
            foreach(Tile tile in pathCopy){
                writer.WriteStartElement("Node");
                writer.WriteAttributeString("x", tile.getX().ToString());
                writer.WriteAttributeString("y", tile.getY().ToString());
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }
        //close writer
        writer.WriteEndElement();
    }

    public void ReadXml(XmlReader reader){
        //name = reader.SelectSingleNode("name").InnerText;
    }
}
