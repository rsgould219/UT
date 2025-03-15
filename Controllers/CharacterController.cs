using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using System;

public class CharacterController : MonoBehaviour{
    const int profMinRep = 1000;
    int curMajor;
    int z = 0;
    int tuitionPay = 10000;
    public int profCount;
    public int studCount;
    public int gradCount;
    public Sprite studentSprite1;
    public static CharacterController Instance;
    Character holdingChar;
    public int ageBuf, skillBuf;
    public string selectType;
    public Sprite[] bodyImages;
    public Sprite[] headImages;
    public List<Character> charactersActive = new List<Character>();
    public List<Character> toBeMadeInactive = new List<Character>();
    public List<Character> studList = new List<Character>();
    public List<Tuple<int, int, int, int[]>> newStudents = new List<Tuple<int, int, int, int[]>>();
    public List<Tuple<int, int, int, int[]>> newGradStudents = new List<Tuple<int, int, int, int[]>>();
    public List<Character> profList = new List<Character>();
    public List<Character> gradList = new List<Character>();
    List<int> removeBuffP = new List<int>();
    List<int> removeBuffS = new List<int>();
    Course[] courses;
    public Dictionary<int, int> profAge = new Dictionary<int, int>();
    public Dictionary<int, int> profSkill = new Dictionary<int, int>();
    public List<int> firedProfBuf = new List<int>();

    // Start is called before the first frame update
    void Start(){
        Instance = this;
        courses = AcedemicController.Instance.courses;
        curMajor = 0;
        profCount = 0;
        studCount = 0;
        gradCount = 0;
        ageBuf = 0;
        skillBuf = 0;
    }

    void Update(){
        //Debug.Log("There are " + charactersActive.Count );
        foreach (Character c in charactersActive)
            c.update(Time.deltaTime);
        if(toBeMadeInactive.Count != 0)
            makeInactive();
    }

    //update desination data for new day
    public void nextDayUpdates(){
        Tile spawnTile = BuildingController.Instance.building.getTileAt(0, 0);
        Debug.Log("Reseting character for day " + WorldController.Instance.getDay());
        /*foreach(Character c in charactersActive){
            nextDayHelper(c, spawnTile);
        }*/
        foreach(Character c in studList){
            //c.setOccupiedTill(0);
            nextDayHelper(c, spawnTile);
        }
        foreach(Character c in gradList){
            //c.setOccupiedTill(0);
            nextDayHelper(c, spawnTile);
        }
        foreach(Character c in profList){
            //c.setOccupiedTill(0);
            nextDayHelper(c, spawnTile);
        }
    }
    public void nextDayHelper(Character c, Tile spawnTile){
        c.getGO().transform.position = new Vector3(0, 0);
        c.setTile(spawnTile);
        c.setDest(spawnTile);
        int i = 0;
        while(i < c.timeList.Count){
            if(c.timeList[0].Item1 != WorldController.Instance.getDay()){
                c.timeList.Add(new Tuple<int, int, SectionLite>(c.timeList[0].Item1, c.timeList[0].Item2, c.timeList[0].Item3));
                c.timeList.RemoveAt(0);
            }
            else
                break;
            i++;
        }
        c.setOccupiedTill(0);
        c.setNextDest();
    }

    //creates a character, but not an object and doesn't add to lists
    public Character createCharacter(string charType, int major){
        Character ch = new Character(BuildingController.Instance.building.getTileAt(0, 0), major);
        switch (charType) {
            case "Prof":
                Debug.Log("Add prof");
                ch.setType(Character.CharType.Prof);
                break;
            case "Student":
                ch.setType(Character.CharType.Student);
                break;
            case "Grad":
                ch.setType(Character.CharType.Grad);
                break;
        }
        setImageIds(ch);
        return ch;
    }

    /*
    0 - engr
    1 - cse
    2 - mec eng
    3 - chem eng
    4 - electrical
    5 - civil eng
    6 - industrial
    7 - material sci
    8 - physics
    9 - chem
    10 - englsih
    11 - history
    12 - math
    13 - eco
    14 - Account
    15 - Law
     */
    //Create professor and set skilled courses
    public Character addProfessor(int major){
        courses = AcedemicController.Instance.courses;
        Character ch = new Character(BuildingController.Instance.building.getTileAt(0, 0), major);
        ch.setType(Character.CharType.Prof);
        setImageIds(ch);
        ch.setId(profCount);
        profCount++;

        List<int> li = new List<int>();
        if(major < 8){
            li.Add(0);//add engr101
        }

        //Generate character age and skill
        int age = UnityEngine.Random.Range(1, 100);
            if(age > 60)
                age = age / 2;
            else if(age < 30)
                age = age + 14;
            if(age < 30)
                age = age * 2;
        ageBuf = age;
        skillBuf = (UnityEngine.Random.Range(1, 50) + age) % 100;     //ch.getId(), (UnityEngine.Random.Range(1, 50) + age) % 100);

        //check if skill is allowed by reputation
        if(skillBuf > 70 && profMinRep > WorldController.Instance.world.reputation){
            Debug.Log("1st rep check fail");
            skillBuf = (UnityEngine.Random.Range(20 + age/2, 70));
        }
        if(skillBuf > 85 && profMinRep > WorldController.Instance.world.reputation){
            Debug.Log("2nd rep check fail");
            skillBuf = (UnityEngine.Random.Range(25 + age/2, 85));
        }
        //assign different types of awards
        assignAwards(ch, age, 70, 70, 60, 40, 30, 50, 1, 2);
        assignAwards(ch, age, 60, 60, 50, 30, 10, 20, 3, 4);
        assignAwards(ch, age, 60, 60, 45, 30, 10, 20, 5, 10);

        //assign salary
        int s = 30000 + (50 * ageBuf) + (5 * skillBuf) + ch.getAwards().Count * 200;
        ch.setSalary(s);
        
        holdingChar = ch;
        li = profListWriter.Instance.sortCourses(li, major);
        ch.setSkilledCourses(li);
        Debug.Log(String.Join(", ", ch.skilledCourses.ToArray()));
        courses = null;
        return ch;
    }
    //assign awards
    public void assignAwards(Character ch, int age, int skill1, int skill2, int skill3, int req1, int req2, int req3, int awardType, int attempts, int awardOverwrite = 1){
        while (attempts > 0){
            bool nominated = false;
            if(age > 35 && skillBuf > skill1){
                if(UnityEngine.Random.Range(1, req1) == 5)
                    nominated = true;
            }
            if (skillBuf > skill2){
                if(UnityEngine.Random.Range(1, req2) == 5)
                    nominated = true;
            }
            if (skillBuf > skill3){
                if(UnityEngine.Random.Range(1, req3) == 5)
                    nominated = true;
            }
            //chance for any professor to win a minor award
            if(awardType == 3 && UnityEngine.Random.Range(1, 40 * awardOverwrite) == 5){
                if(UnityEngine.Random.Range(1, 5) == 5)
                    ch.setAward(3);
                ch.setAward(4);
            }

            //chance for any professor to get a major work
            if(awardType == 5 && UnityEngine.Random.Range(1, 40 * awardOverwrite) == 5){
                if(UnityEngine.Random.Range(1, 2) == 2){
                    ch.setAward(5);
                    ch.setAward(6);
                }
            }

            if(nominated != false && UnityEngine.Random.Range(1, 5) == 2){
                ch.setAward(awardType);
                ch.setAward(awardType + 1);
                Debug.Log("Setting Award");
            }
            else if(nominated != false){
                ch.setAward(awardType + 1);
                Debug.Log("Setting Award Nominations");
            }
            attempts--;
        }
    }

    public void addCharHolder(){
         charactersActive.Add(holdingChar);
         createCharObj(holdingChar);
         holdingChar = null;
    }

    public Character setImageIds(Character ch){
        ch.setSpriteId(1);
        switch(UnityEngine.Random.Range(0, 7)){
            case 2:
                ch.setHeadId(1);
                break;
            case 3:
                ch.setHeadId(3);
                break;
            case 4:
                ch.setHeadId(4);
                break;
            case 5:
                ch.setHeadId(5);
                break;
            case 6:
                ch.setHeadId(5);
                break;
            default:
                ch.setHeadId(2);
                break;
        }
        return ch;
    }

    //sets a character to the game lists, making it "hired"
    public void setCharacter(Character ch){
        switch (ch.getType()){
            case Character.CharType.Prof:
                ch.setId(profCount);

                //profAge.Add(ch.getId(), ageBuf);
                //profSkill.Add(ch.getId(), skillBuf);
                ch.year = ageBuf;
                ch.setSkill(skillBuf);
                Debug.Log("Age:" + ch.year);

                profCount++;
                profList.Add(ch);
                createCharObj(ch);
                UIController.Instance.setInfoBar("Professor Hired");
                break;
            case Character.CharType.Student:
                ch.setId(studCount);
                studCount++;
                studList.Add(ch);
                createCharObj(ch);
                break;
            case Character.CharType.Grad:
                ch.setId(gradCount);
                gradCount++;
                gradList.Add(ch);
                break;
        }
    }

    //create the go for a character
    public void createCharObj(Character ch) {
        Debug.Log("creating character");
        GameObject charGO = new GameObject();

        charGO.name = "char";
        charGO.transform.position = new Vector3(0, 0);
        charGO.transform.SetParent(this.transform, true);

        //make then join head and body sprites
        GameObject charHeadObj = new GameObject();
        charHeadObj.transform.position = new Vector3 (charGO.transform.position.x, charGO.transform.position.y + 1.3f, charGO.transform.position.z);
        charHeadObj.layer = 5;
        charHeadObj.transform.SetParent(charGO.transform, true);
        charGO.AddComponent<SpriteRenderer>().sprite = bodyImages[0];
        charHeadObj.AddComponent<SpriteRenderer>().sprite = headImages[0];
        Debug.Log("headid1 : " + ch.getHeadId());
        charHeadObj.GetComponent<SpriteRenderer>().sprite = headImages[4 * (ch.getHeadId()) - 1];
        charHeadObj.GetComponent<SpriteRenderer>().sortingLayerName = "Head-Lower";
        charGO.GetComponent<SpriteRenderer>().sortingLayerName = "Character";

        //set nav agent
        charGO.AddComponent<NavMeshAgent>();
        charGO.GetComponent<NavMeshAgent>().speed = 3;
        ch.setGO(charGO);
    }

    public int getSkill(Character ch){
        return ch.getSkill();
    }

    //updates the list that stores the count of new students
    public void updateNewStudents() {
        int[,] studMap = new int[5, 5];
        for(int i = 0; i < 5; i++){
            for(int j = 0; i < 5; i++){
                studMap[i, j] = 0;
            }
            
        }
        //removes tuples of the major that is being updated
        Debug.Log("major: " + curMajor);
        for (int i = 0; i < newStudents.Count; i++) {
            if (newStudents[i].Item1 == curMajor) {
                newStudents.RemoveAt(i);
                i--;
            }
        }
        for (int i = 0; i < newGradStudents.Count; i++) {
            if (newGradStudents[i].Item1 == curMajor) {
                newGradStudents.RemoveAt(i);
                i--;
            }
        }

        for(int i = 0; i < UIController.Instance.studBuf; i++){
            Tuple<int, int> t = UIController.Instance.studTypeList[UIController.Instance.studTypeList.Count - 1];
            switch(t.Item1){
                case 2:
                    studMap[t.Item2 - 1, 0]++;
                    break;
                case 3:
                    studMap[t.Item2 - 1, 1]++;
                    break;
                case 11:
                    studMap[t.Item2 - 1, 2]++;
                    break;
                case 12:
                    studMap[t.Item2 - 1, 3]++;
                    break;
                case 13:
                    studMap[t.Item2 - 1, 4]++;
                    break;
            }
        }

        //adds new tuples
        int[] t1 = {studMap[0, 0], studMap[0, 1], studMap[0, 2], studMap[0, 3], studMap[0, 4]};
        newStudents.Add(new Tuple<int, int, int, int[]>(curMajor, 1, Int32.Parse(GameObject.Find("FreshmenCount").GetComponent<Text>().text), t1)); 
        int[] t2 = {studMap[1, 0], studMap[1, 1], studMap[1, 2], studMap[1, 3], studMap[1, 4]};
        newStudents.Add(new Tuple<int, int, int, int[]>(curMajor, 2, Int32.Parse(GameObject.Find("SofCount").GetComponent<Text>().text), t2));
        int[] t3 = {studMap[2, 0], studMap[2, 1], studMap[2, 2], studMap[2, 3], studMap[2, 4]};
        newStudents.Add(new Tuple<int, int, int, int[]>(curMajor, 3, Int32.Parse(GameObject.Find("JuniorCount").GetComponent<Text>().text), t3));
        int[] t4 = {studMap[3, 0], studMap[3, 1], studMap[3, 2], studMap[3, 3], studMap[3, 4]};
        newStudents.Add(new Tuple<int, int, int, int[]>(curMajor, 4, Int32.Parse(GameObject.Find("SeniorCount").GetComponent<Text>().text), t4));
        int[] t5 = {studMap[4, 0], studMap[4, 1], studMap[4, 2], studMap[4, 3], studMap[4, 4]};
        newGradStudents.Add(new Tuple<int, int, int, int[]>(curMajor, 1, Int32.Parse(GameObject.Find("GradCount").GetComponent<Text>().text), t5));
        curMajor = GameObject.Find("StudentDeptDropdown").gameObject.GetComponent<Dropdown>().value;
        //resets values
        GameObject.Find("FreshmenCount").GetComponent<Text>().text = findNewStudentNum(1).ToString();
        GameObject.Find("SofCount").GetComponent<Text>().text = findNewStudentNum(2).ToString();
        GameObject.Find("JuniorCount").GetComponent<Text>().text = findNewStudentNum(3).ToString();
        GameObject.Find("SeniorCount").GetComponent<Text>().text = findNewStudentNum(4).ToString();
        GameObject.Find("GradCount").GetComponent<Text>().text = findGradStudentNum().ToString();

        UIController.Instance.setInfoBar("Added new students for next semester");
    }

    public int findNewStudentNum(int year) {
        foreach (Tuple<int, int, int, int[]> t in newStudents) {
            if (t.Item1 == curMajor && t.Item2 == year) {
                return t.Item3;
            }
        }
        return 0;
    }
    public int findGradStudentNum(){
        int i = 0;
        foreach (Tuple<int, int, int, int[]> t in newGradStudents) {
            if (t.Item1 == curMajor) {
                i += t.Item3;
            }
        }
        return i;
    }

    //test add character function
    /*public void addTest() {
        Character ch = createCharacter("Prof", 1);
        //ch.setDest(BuildingController.Instance.building.getTileAt(10, 10));
        //charactersActive.Add(ch);
        ch.setPath(new Path(BuildingController.Instance.building, ch.getTile(), BuildingController.Instance.building.getTileAt(3, 3)));
        List<int> li = new List<int>();
        li.Add(1);
        ch.setSkilledCourses(li);
    }*/

    //makes a character active by adding it to the active list and removing it from a charcter list
    public void makeActive(Character ch) {
        //int i = 0;
        Debug.Log("Making active");
        //make a Gameobject if none exists due to coming from a new building
        /*if(ch.getGO() == null)
            createCharObj(ch);*/
            if(ch.getGO().activeSelf != true){
                ch.getGO().SetActive(true);
            }
        /*if(ch.getType() == Character.CharType.Prof) {
            foreach (Character c in profList) {
                if (c.getId() == ch.getId())
                    removeBuffP.Add(ch.getId());
            }
        }
        else if(ch.getType() == Character.CharType.Student) {
            foreach (Character c in studList){
                if (c.getId() == ch.getId())
                    removeBuffS.Add(i);
                    break;
            }
        }*/
        charactersActive.Add(ch);
        ch.active = true;
    }

    //make marked characters inactive, then set a new destination
    public void makeInactive(){
        Debug.Log("Making Inactive");
        int i = 0, s = 0, p = 0;
        foreach(Character ch in toBeMadeInactive){
            i = 0;
            foreach(Character ca in charactersActive){
                if(ch.getId() == ca.getId()){
                    /*if (ch.getType() == Character.CharType.Prof){
                        profList.Add(ch);
                        p++;
                    }
                    else if (ch.getType() == Character.CharType.Student){
                        studList.Add(ch);
                        s++;
                    }
                    else if (ch.getType() == Character.CharType.Grad){
                        gradList.Add(ch);
                    }*/
                    ch.setNextDest();
                    charactersActive.RemoveAt(i);
                    ch.active = false;
                    break;
                }
                else
                    i++;
            }
        }
        Debug.Log(p +" prof and " + s + " students made inactive");
        toBeMadeInactive.Clear();
    }

    //mark character as to be made inactive
    public void markAsToMakeInactive(Character ch){
        toBeMadeInactive.Add(ch);
    }
    //check if characters are still active or should be removed from active list
    public void activeCheck() {
        foreach(Character c in studList) {
            c.goToNextDest();
            //createCharObj(c);
        }
        foreach(Character c in profList) {
            c.goToNextDest();
        }
        /*for(int i = 0; i < charactersActive.Count; i++){
            if(charactersActive[i].getType() == Character.CharType.Prof && removeBuffP.Contains(charactersActive[i].getId())){
                profList.Add(charactersActive[i]);
                removeBuffP.Remove(charactersActive[i].getId());
                charactersActive.RemoveAt(i);
                Debug.Log("Prof Removed from active");
            }
            else if(charactersActive[i].getType() == Character.CharType.Student && removeBuffS.Contains(charactersActive[i].getId())){
                studList.Add(charactersActive[i]);
                removeBuffS.Remove(charactersActive[i].getId());
                charactersActive.RemoveAt(i);
                Debug.Log("Student Removed from active");
            }
        }
        if(removeBuffP.Count != 0 && removeBuffS.Count != 0)
            Debug.Log("Students were not removed");
        removeBuffS.Clear();
        removeBuffP.Clear();*/
    }
    public void activeCheckOdd(List<Character> oddList){
        Debug.Log("Checking " + oddList.Count + " odd characters");
        foreach(Character c in oddList){
            c.oddOccupiedTime = false;
            c.goToNextDest();
        }
    }


    //clears the active characters when switching buildings
    public void clearActiveChar(){
        charactersActive.Clear();
        foreach(Character c in profList){
            if(c.active == true)
                c.active = false;
        }
        foreach(Character c in studList){
            if(c.active == true)
                c.active = false;
        }
        foreach(Character c in gradList){
            if(c.active == true)
                c.active = false;
        }
        charactersActive.Clear();

        //clear character objects
        foreach (Transform child in GameObject.Find("CharacterController").transform) {
            GameObject.Destroy(child.gameObject);
        }   
    }

    //assigns characters when switching buildings
    public void assignCharactersToBuilding(int id){
        //List<Character> list = new List<Character>();

        foreach(Character c in profList){
            Debug.Log("Charcter building id: " + c.getBuilding() + " Current building id: " + id);
            if(c.getBuilding() == id){
                //charactersActive.Add(c);
                //list.Add(c);
                createCharObj(c);
                c.getBuildingChangeDest();
                //c.getGO().transform.position = new Vector3(c.getTile().getX(), c.getTile().getY());
            }
        }
        /*foreach(Character c in list){
            profList.RemoveAt(profList.IndexOf(c));
        }
        list.Clear();*/

        foreach(Character c in studList){
            Debug.Log("Charcter building id: " + c.getBuilding() + " Current building id: " + id);
            if(c.getBuilding() == id){
                //charactersActive.Add(c);
                //list.Add(c);
                createCharObj(c);
                c.getBuildingChangeDest();
                //c.getGO().transform.position = new Vector3(c.getTile().getX(), c.getTile().getY());
            }
        }
        /*foreach(Character c in list){
            studList.RemoveAt(studList.IndexOf(c));
        }
        list.Clear();*/

        foreach(Character c in gradList){
            if(c.getBuilding() == id){
                charactersActive.Add(c);
                //list.Add(c);
                createCharObj(c);
                c.getBuildingChangeDest();
            }
        }
        /*foreach(Character c in list){
            gradList.RemoveAt(gradList.IndexOf(c));
        }
        list.Clear();*/
    }

    public void fireProfessor(int id){
        firedProfBuf.Add(id);
    }
    public void professorProgress(){
        foreach(Character ch in profList){
            professorProgressHelper(ch);
            z++;
        }
        foreach(Character ch in charactersActive){
            if(ch.GetType().Equals(Character.CharType.Prof)){
                professorProgressHelper(ch);
                z++;
            }
        }
    }
    public void professorProgressHelper(Character ch){
        //Advance age
        if(WorldController.Instance.world.semFall == true){
            if(ch.getId() % 2 == 0)
                ch.year++;
        }
        else if(ch.getId() % 2 != 0)
            ch.year++;
        //check if prof wants to retire
        /*if(profSkill[ch.getId()] > 60){
            if(UnityEngine.Random.Range(1, 15) == 1)
                firedProfBuf.Add(ch.getId());
            if(profSkill[ch.getId()] > 65 && UnityEngine.Random.Range(1, 5) == 1)
                firedProfBuf.Add(ch.getId());
            else if(profSkill[ch.getId()] > 68)
                firedProfBuf.Add(ch.getId());
        }*/

        //Check if prof gets better
        if(UnityEngine.Random.Range(1, 3) == 1)
            ch.setSkill(ch.getSkill() + 1);
        if(UnityEngine.Random.Range(1, 3) == 1 && ch.getSkill() > 85)
            ch.setSkill(ch.getSkill() + 1);

        //check if prof gets an award, nomination, or achivenment
        int i = ch.getAwards().Count;
        //check major work
        assignAwards(ch, ch.year, 60, 60, 45, 6, 6, 6, 5, 10, 10);
        if(i != ch.getAwards().Count){
            UIController.Instance.addAchiveSlice(ch, 5, z);
            UIController.Instance.showAchieve();
            i++;
        }
        assignAwards(ch, ch.year, 70, 70, 60, 6, 6, 6, 1, 2, 10);
        if(i == ch.getAwards().Count - 1){
            UIController.Instance.addAchiveSlice(ch, 2, z);
            UIController.Instance.showAchieve();
            i++;
        }
        if(i == ch.getAwards().Count - 2){
            UIController.Instance.addAchiveSlice(ch, 1, z);
            UIController.Instance.addAchiveSlice(ch, 2, z);
            UIController.Instance.showAchieve();
            i = i + 2;
        }
        assignAwards(ch, ch.year, 60, 60, 50, 6, 6, 6, 3, 4, 10);
        if(i == ch.getAwards().Count - 1){
            UIController.Instance.addAchiveSlice(ch, 4, z);
            UIController.Instance.showAchieve();
            i++;
        }
        if(i == ch.getAwards().Count - 2){
            UIController.Instance.addAchiveSlice(ch, 3, z);
            UIController.Instance.addAchiveSlice(ch, 4, z);
            UIController.Instance.showAchieve();
            i = i + 2;
        }
    }

    public void sortTimes(){
        foreach(Character c in studList)
           c.makeTimeList();
        foreach(Character c in gradList)
           c.makeTimeList();
        //foreach(Character c in charactersActive)
           //c.makeTimeList();
        /*foreach(Character c in profList){
            //clear
        }
        foreach(Course c in AcedemicController.Instance.courses){
            if(ScheduleController.Instance.semFall){
                foreach(Section s in c.sections){
                    s.getProf().addToTimeList(s);
                }
            }
            else{
                foreach(Section s in c.sectionsS){
                    s.getProf().addToTimeList(s);
                }
            }
        }*/
        foreach(Character c in profList)
            c.makeTimeList();
        foreach(Character c in profList)
            c.sortTime();
    }
    //returns a tuple of character finance info
    public Tuple<int, int> calcCharFinance(){
        int tuitionTotal, salaryTotal = 0;

        tuitionTotal = (studList.Count + gradList.Count) * tuitionPay;
        foreach (Character c in profList)
            salaryTotal = salaryTotal + c.getSalary();

        return new Tuple<int, int>(tuitionTotal, salaryTotal);
    }

    public void clearForLoad(){
        profAge.Clear();
        profSkill.Clear();
    }
    //writes xml data for prof and student lists
    public void writeXml(XmlWriter writer){
        Debug.Log("Saving " + profList.Count + " professors and " + studList.Count + " students");
        foreach(Character c in profList){
            writer.WriteStartElement("Prof");
            writer.WriteAttributeString("cId", c.getId().ToString());
            writer.WriteAttributeString("bId", c.getBuilding().ToString());
			c.WriteXml(writer);
			//writer.WriteEndElement();
        }
        foreach(Character c in studList){
            writer.WriteStartElement("Student");
            writer.WriteAttributeString("cId", c.getId().ToString());
            writer.WriteAttributeString("bId", c.getBuilding().ToString());
            c.WriteXml(writer);
			//writer.WriteEndElement();
        }
        foreach(Character c in gradList){
            writer.WriteStartElement("Grad");
            writer.WriteAttributeString("cId", c.getId().ToString());
            writer.WriteAttributeString("bId", c.getBuilding().ToString());
            c.WriteXml(writer);
			//writer.WriteEndElement();
        }
        foreach(Character c in charactersActive){
            writer.WriteStartElement("ActiveCharacter");
            writer.WriteAttributeString("cId", c.getId().ToString());
            //writer.WriteAttributeString("bId", c.getBuilding().ToString());
            if (c.getType() == Character.CharType.Student){
                writer.WriteAttributeString("cType", "1");
                //writer.WriteAttributeString("year", c.year.ToString());
            }
            else if (c.getType() == Character.CharType.Prof)
                writer.WriteAttributeString("cType", "2");
            else if (c.getType() == Character.CharType.Grad)
                writer.WriteAttributeString("cType", "3");
            //c.WriteXml(writer);
            writer.WriteEndElement();
        }
    }
    //write tuples for new students
    public void writeCharTuples(XmlWriter writer){
        foreach(Tuple<int, int, int, int[]> t in newStudents){
            writer.WriteStartElement("NewStudentTuple");
            writer.WriteAttributeString("major", t.Item1.ToString());
            writer.WriteAttributeString("year", t.Item2.ToString());
            writer.WriteAttributeString("count", t.Item3.ToString());
            writer.WriteAttributeString("info1", t.Item4[0].ToString());
            writer.WriteAttributeString("info2", t.Item4[1].ToString());
            writer.WriteAttributeString("info3", t.Item4[2].ToString());
            writer.WriteAttributeString("info4", t.Item4[3].ToString());
            writer.WriteAttributeString("info5", t.Item4[4].ToString());
            writer.WriteEndElement();
        }
        foreach(Tuple<int, int, int, int[]> t in newGradStudents){
            writer.WriteStartElement("NewGradTuple");
            writer.WriteAttributeString("major", t.Item1.ToString());
            writer.WriteAttributeString("year", t.Item2.ToString());
            writer.WriteAttributeString("count", t.Item3.ToString());
            writer.WriteAttributeString("info1", t.Item4[0].ToString());
            writer.WriteAttributeString("info2", t.Item4[1].ToString());
            writer.WriteAttributeString("info3", t.Item4[2].ToString());
            writer.WriteAttributeString("info4", t.Item4[3].ToString());
            writer.WriteAttributeString("info5", t.Item4[4].ToString());
            writer.WriteEndElement();
        }
    }

    //reads xml data for characters and their skilled courses
    public void readXml(XmlReader reader, World worldLoading){
        bool courseBuff = false;
        Character ch = new Character(BuildingController.Instance.building.getTileAt(0,0), 0);
        while(reader.Read()) {
            Debug.Log(reader.Name);
            switch(reader.Name){
                case "Prof":
                    readProf(ch, reader, worldLoading);
                    break;
                case "Student":
                    ch.setType(Character.CharType.Student);
                    ch.year= int.Parse(reader.GetAttribute("year"));
                    ch = readHelper(ch, reader, worldLoading);
                    setCharacter(ch);
                    break;
                case "Grad":
                    ch.setType(Character.CharType.Grad);
                    setCharacter(ch);
                    ch = readHelper(ch, reader, worldLoading);
                    break;
                case "ActiveCharacter":
                    //ch = readHelper(ch, reader);
                    if(int.Parse(reader.GetAttribute("cType")) == 1){
                        foreach(Character c in studList){
                            if(int.Parse(reader.GetAttribute("cId")) == c.getId())
                                makeActive(c);
                            break;
                        }
                    }
                    else if (int.Parse(reader.GetAttribute("cType")) == 2){
                        foreach(Character c in profList){
                            if(int.Parse(reader.GetAttribute("cId")) == c.getId())
                                makeActive(c);
                            break;
                        }
                    }
                    else{
                        foreach(Character c in gradList){
                            if(int.Parse(reader.GetAttribute("cId")) == c.getId())
                                makeActive(c);
                            break;
                        }
                        
                    }
                    break;
                default:
                    if(studList.Count != 0)
                        studList[studList.Count - 1].year += 1;
                    return;
            }
        }
    }
    //loads info unique to prof and sets the character
    public Character readProf(Character ch, XmlReader reader, World worldLoading){
        Debug.Log("Add prof");
        int skill = int.Parse(reader.GetAttribute("Skill"));
        int salaryProf = int.Parse(reader.GetAttribute("Salary"));
        ch.year =  int.Parse(reader.GetAttribute("Age"));
        profSkill.Add(int.Parse(reader.GetAttribute("cId")), skill);
        profCount = int.Parse(reader.GetAttribute("cId")) + 1;
        ch = readHelper(ch, reader, worldLoading);
        ch.setType(Character.CharType.Prof);

        setCharacter(ch);
        ch.setSkill(skill);
        ch.setSalary(salaryProf);
        return ch;
    }

    //helps assign data to characters
    public Character readHelper(Character ch, XmlReader reader, World worldLoading){
        Tile t = BuildingController.Instance.building.getTileAt(int.Parse(reader.GetAttribute("curX")), int.Parse(reader.GetAttribute("curY")));
        ch = new Character(t, int.Parse(reader.GetAttribute("major")));
        t = BuildingController.Instance.building.getTileAt(int.Parse(reader.GetAttribute("destX")), int.Parse(reader.GetAttribute("destY")));
        ch.setDest(t);
        Debug.Log("Name of loaded character:" + reader.GetAttribute("name"));
        ch.setName(reader.GetAttribute("name"));
        ch.setHeadId(int.Parse(reader.GetAttribute("headId")));
        ch.setSpriteId(int.Parse(reader.GetAttribute("bodyId")));
        ch.setBuilding(int.Parse(reader.GetAttribute("bId")));
        ch.setId(int.Parse(reader.GetAttribute("cId")));

        while(reader.Read()){
            Debug.Log(reader.Name);
            switch(reader.Name){
                case "Course":
                    ch.addSkilled(int.Parse(reader.GetAttribute("id")));
                    break;
                case "Section":
                    readSectionLite(ch, int.Parse(reader.GetAttribute("cId")) + 1, int.Parse(reader.GetAttribute("sId")) + 1, int.Parse(reader.GetAttribute("zId")), worldLoading, 0);
                    break;
                case "SectionS":
                    readSectionLite(ch, int.Parse(reader.GetAttribute("cId")) + 1, int.Parse(reader.GetAttribute("sId")) + 1, int.Parse(reader.GetAttribute("zId")), worldLoading, 1);
                    break;
                case "Award":
                    ch.getAwards().Add(int.Parse(reader.GetAttribute("id")));
                    break;
                case "tList":
                    Zone sectionZone = null;
                    Debug.Log("Classes Count: " + worldLoading.classes.Count);
                    foreach(Zone z in worldLoading.classes){
                        Debug.Log("Comparing zone id " + z.getId() + " with timelist " + int.Parse(reader.GetAttribute("zId")));
                        if(z.getId() == int.Parse(reader.GetAttribute("zId"))){
                            sectionZone = z;
                            break;
                        }
                    }
                    if(sectionZone == null){
                        foreach(Zone z in worldLoading.labs){
                            if(z.getId() == int.Parse(reader.GetAttribute("zId"))){
                                sectionZone = z;
                                break;
                            }
                        }
                    }
                    if(sectionZone == null){
                        Debug.Log("NoZoneFound for tuple list loading for zid:" + int.Parse(reader.GetAttribute("zId")) + " and cId " + int.Parse(reader.GetAttribute("cId")));
                    }
                    SectionLite sl = new SectionLite(int.Parse(reader.GetAttribute("cId")) + 1, int.Parse(reader.GetAttribute("sId")) + 1, sectionZone);
                    Tuple<int, int, SectionLite> time = new Tuple<int, int, SectionLite>(int.Parse(reader.GetAttribute("day")), int.Parse(reader.GetAttribute("time")), sl);
                    ch.timeList.Add(time);
                    break;
                case "Destination":
                    Tile destination = BuildingController.Instance.building.getTileAt(int.Parse(reader.GetAttribute("x")), int.Parse(reader.GetAttribute("y")));
                    ch.setDest(destination);
                    int occupiedTill = int.Parse(reader.GetAttribute("time"));
                    ch.setOccupiedTill(occupiedTill);
                    Queue<Tile> tQ = new Queue<Tile>();
                    while(reader.Read()){
                        if(reader.Name.Equals("Node"))
                            tQ.Enqueue(BuildingController.Instance.building.getTileAt(int.Parse(reader.GetAttribute("x")), int.Parse(reader.GetAttribute("y"))));
                        else
                            break;
                    }
                    ch.path = new Path_AStar(tQ, destination);
                    Debug.Log("set Dest at " + destination.getX() + " " + destination.getY());
                    break;
                default:
                    Debug.Log("Name of finished character:" + ch.name + " with Size of sectionList:" + ch.sectionList.Count);
                    return ch;
            }
        }
        Debug.Log("Name of finished character:" + ch.name + " with Size of sectionList:" + ch.sectionList.Count);
        return ch;
    }
    //simple section info for current schedule
    public void readSectionLite(Character ch, int cId, int sId, int zId, World worldLoading, int sem){
        foreach(Zone z in worldLoading.classes){
            if(z.getId() == zId){
                if(sem == 0)
                    ch.addSection(new SectionLite(cId, sId, z));
                else
                    ch.addSectionS(new SectionLite(cId, sId, z));
                return;
            }
        }
        foreach(Zone z in worldLoading.labs){
            if(z.getId() == zId){
                if(sem == 0)
                    ch.addSection(new SectionLite(cId, sId, z));
                else
                    ch.addSectionS(new SectionLite(cId, sId, z));
                return;
            }
        }
    }
    //load buffer of new studdents to be added next scheduling
    public void readCharTuple(XmlReader reader){
        if (reader.IsEmptyElement){
            return;
        }
        while(reader.Read()){
            Debug.Log(reader.Name);
            switch(reader.Name){
                case "NewStudentTuple":
                    int[] i = {int.Parse(reader.GetAttribute("info1")), int.Parse(reader.GetAttribute("info2")),
                        int.Parse(reader.GetAttribute("info3")), int.Parse(reader.GetAttribute("info4")),
                        int.Parse(reader.GetAttribute("info5"))};
                    newStudents.Add(new Tuple<int, int, int, int[]>(int.Parse(reader.GetAttribute("major")), 
                                                            int.Parse(reader.GetAttribute("year")), 
                                                            int.Parse(reader.GetAttribute("count")), i));
                    break;
                case "NewGradTuple":
                    int[] j = {int.Parse(reader.GetAttribute("info1")), int.Parse(reader.GetAttribute("info2")),
                            int.Parse(reader.GetAttribute("info3")), int.Parse(reader.GetAttribute("info4")),
                            int.Parse(reader.GetAttribute("info5"))};
                    newGradStudents.Add(new Tuple<int, int, int, int[]>(int.Parse(reader.GetAttribute("major")), 
                                                                int.Parse(reader.GetAttribute("year")),
                                                                int.Parse(reader.GetAttribute("count")), j));
                    break;
                default:
                    return;
            }
        }
    }
}