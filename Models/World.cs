using System.Collections;
using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class World : IXmlSerializable{
    const int baseReputation = 100;
    const int verInfo = 1;
    public int nextZoneId = 0;
    public int reputation = 100;
    int lastGoodRepHit = 180;
    int lastGreatRepHit = 100;
    int currentBuilding = 1;
    public bool semFall = false;
    string firstBuildingName;
    
    public int totalUnderGrad, totalGrad, totalGoodUnderGrad, totalGoodGrad, totalGreatUnderGrad, totalGreatGrad = 0;
    public List<Building> buildings = new List<Building>();
    public List<Zone> classes;
    public List<Zone> labs;
    public List<Zone> offices;
    public List<Zone> dorms;
    List<Course> courses = new List<Course>();

    public World(){
        classes = new List<Zone>();
        offices = new List<Zone>();
        labs = new List<Zone>();
        dorms = new List<Zone>();
    }

    public void addBuilding(Building building){
        buildings.Add(building);
    }
    public void addClass(Zone zone){
        //zone.assignId(nextZoneId);
        zone.assignType("Class");
        nextZoneId++;
        classes.Add(zone);
    }
    public void addOffice(Zone zone){
        //zone.assignId(nextZoneId);
        zone.assignType("Office");
        nextZoneId++;
        offices.Add(zone);
    }
    public void addLab(Zone zone){
        //zone.assignId(nextZoneId);
        zone.assignType("Lab");
        nextZoneId++;
        labs.Add(zone);
    }

    public void addDorm(Zone z){
        //z.assignId(nextZoneId);
        z.assignType("Dorm");
        nextZoneId++;
        dorms.Add(z);
    }

    //finds the zone of a selected tile
    public Zone getZoneFromTile(Tile t){
        foreach(Zone z in classes){
            if (z.inZone(t.getX(), t.getY()) && z.building.id == getCurrentBuilding() - 1){
                return z;
            }
        }
            return null;
    }
    public List<Zone> getClasses(){
        return classes;
    }
    public List<Zone> getOffices(){
        return offices;
    }
    public List<Zone> getLabs(){
        return labs;
    }

    public XmlSchema GetSchema(){
        return null;
    }

    public void resetSeatsForZones(){
        foreach(Zone z in classes){
            z.resetSeat();
        }
        foreach(Zone z in labs){
            z.resetSeat();
        }
    }
    //displays zones with markers in the world
    public void displayZones(){
        foreach(Zone z in classes){
            z.displayZone();
            Debug.Log("Class created");
        }
        foreach(Zone z in offices){
            z.displayZone();
            Debug.Log("offices created");
        }
        foreach(Zone z in labs){
            z.displayZone();
            Debug.Log("labs created");
        }
        foreach(Zone z in dorms){
            z.displayZone();
            Debug.Log("dorms created");
        }
    }
    public void setCurrentBuilding(int i){
        this.currentBuilding = i;
    }
    public int getCurrentBuilding(){
        return currentBuilding;
    }

    public void flipWorldSemester(){
        if(semFall == true)
            semFall = false;
        else
            semFall = true;
    }

    //delete a zone and rremove it from any classes, labs, and offices
    public void deleteZone(int x, int y){
        bool foundZone = false;
        int i = 0;
        foreach(Zone z in classes){
            if(z.inZone(x, y)){
                foundZone = true;
                foreach(Course c in AcedemicController.Instance.courses){
                    foreach(Section s in c.sections){
                        if(z.Equals(s.getZone()))
                            s.removeZone();
                    }
                    foreach(Section s in c.sectionsS){
                        if(z.Equals(s.getZone()))
                            s.removeZone();
                    }
                }
                classes.RemoveAt(i);
                UIController.Instance.setInfoBar("Deleted Class");
                return;
            }
            else
                i++;
        }
        i = 0;
        foreach(Zone z in offices){
            if(z.inZone(x, y)){
                foundZone = true;
                offices.RemoveAt(i);
                UIController.Instance.setInfoBar("Deleted Office");
                return;
            }
            else
                i++;
        }
        i = 0;
        foreach(Zone z in labs){
            if(z.inZone(x, y)){
                foundZone = true;
                foreach(Course c in AcedemicController.Instance.courses){
                    foreach(Section s in c.sections){
                        if(z.Equals(s.getZone()))
                            s.removeZone();
                    }
                    foreach(Section s in c.sectionsS){
                        if(z.Equals(s.getZone()))
                            s.removeZone();
                    }
                }
                labs.RemoveAt(i);
                UIController.Instance.setInfoBar("Deleted lab");
                return;
            }
            else
                i++;
        }
        foreach(Zone z in dorms){
             if(z.inZone(x, y)){
                foundZone = true;
                dorms.RemoveAt(i);
                UIController.Instance.setInfoBar("Deleted dorm");
                return;
            }
            else
                i++;
        }
    }

    public int getDormCount(){
        int i = 0;
        foreach(Zone z in dorms){
            i += z.getBedCount();
        }
        return i;
    }
    //Add gratuated students to total counts
    public void addTotalCount(int underGrad, int grad){
        totalUnderGrad += underGrad;
        totalGrad += grad;
    }
    public void addToIntCount(int underGrad, int greatUnderGrad, int grad, int greatGrad){
        totalGoodUnderGrad += underGrad;
        totalGoodGrad += grad;
        totalGreatUnderGrad += greatUnderGrad;
        totalGreatGrad += greatGrad;
    }

    public void addSkillCounts(int goodUnderGrad, int goodGrad, int greatUnderGrad, int greatGrad){
        totalGoodUnderGrad += goodUnderGrad;
        totalGoodGrad += goodGrad;
        totalGreatUnderGrad += greatUnderGrad;
        totalGreatGrad += greatGrad;
    }
    //Calculates the total reputation
    public void reputationTally(){
        int rep = reputation;
        rep += (totalUnderGrad / 10 - totalUnderGrad / 110) + (totalGrad / 5 - totalGrad / 55);
        rep += (CharacterController.Instance.profCount / 2);
        rep += buildings.Count - 1;

        int majorAwards = 0;
        int majorNoms = 0;
        int minorAwards = 0;
        int minorNoms = 0;
        int accomplishments = 0;

        //calculates award rep
        foreach(Character ch in CharacterController.Instance.profList){
            if(ch.getSkill() < 75)
                rep++;
            foreach(int i in ch.getAwards()){
                switch(i){
                    case 1:
                        majorAwards++;
                        break;
                    case 2:
                        majorNoms++;
                        break;
                    case 3:
                        minorAwards++;
                        break;
                    case 4:
                        minorNoms++;
                        break;
                    case 5:
                        accomplishments++;
                        break;
                }
            }
        }
        rep += majorAwards * 3 + majorNoms;
        rep += minorAwards + minorNoms / 3;
        rep += accomplishments / 5;
        reputation = rep;
        UIController.Instance.updateRepUI(rep);
    }
    //checks if the reputation allows for additional talented students
    public void reputationCheck(){
        int curGoodTotal = ScheduleController.Instance.goodTotal();
        int curGreatTotal = ScheduleController.Instance.greatTotal();

        while(curGoodTotal < reputation / 20)
            UIController.Instance.addGoodStudent();
        while(curGreatTotal < reputation / 100)
            UIController.Instance.addGreatStudent();
    }
    //write the save file
    public void WriteXml(XmlWriter writer){
        WorldController.Instance.writeTime(writer);

        writer.WriteStartElement("WorldInfo");
        writer.WriteAttributeString("Money", WorldController.Instance.tempMoney.ToString());
        writer.WriteAttributeString("pCount", CharacterController.Instance.profCount.ToString());
        writer.WriteAttributeString("sCount", CharacterController.Instance.studCount.ToString());
        writer.WriteAttributeString("gCount", CharacterController.Instance.gradCount.ToString());
        writer.WriteAttributeString("rep", reputation.ToString());
        writer.WriteAttributeString("ver", WorldController.Instance.ver.ToString());
        writer.WriteAttributeString("tutorialProg", TutorialController.Instance.tutorialProg.ToString());
        if(TutorialController.Instance.tutorialActive)
            writer.WriteAttributeString("tutorialActive", "1");
        else
            writer.WriteAttributeString("tutorialActive", "0");
        writer.WriteEndElement();

        writer.WriteStartElement("GradInfo");
        writer.WriteAttributeString("tu", totalUnderGrad.ToString());
        writer.WriteAttributeString("gU", totalGoodUnderGrad.ToString());
        writer.WriteAttributeString("grU", totalGreatUnderGrad.ToString());
        writer.WriteAttributeString("tG", totalGrad.ToString());
        writer.WriteAttributeString("gG", totalGoodGrad.ToString());
        writer.WriteAttributeString("grG", totalGreatGrad.ToString());
        writer.WriteEndElement();

        writer.WriteStartElement("StInfo");
        GameObject avalableStudents = UIController.Instance.studentPanel.transform.GetChild(24).gameObject;
        writer.WriteAttributeString("oa", avalableStudents.transform.GetChild(0).GetComponent<Text>().text);
        writer.WriteAttributeString("ga", avalableStudents.transform.GetChild(2).GetComponent<Text>().text);
        writer.WriteAttributeString("gra", avalableStudents.transform.GetChild(4).GetComponent<Text>().text);
        writer.WriteAttributeString("of", ScheduleController.Instance.okStudCountF.ToString());
        writer.WriteAttributeString("gf", ScheduleController.Instance.goodStudCountF.ToString());
        writer.WriteAttributeString("grf", ScheduleController.Instance.greatStudCountF.ToString());
        writer.WriteAttributeString("oSO", ScheduleController.Instance.okStudCountSo.ToString());
        writer.WriteAttributeString("gSO", ScheduleController.Instance.goodStudCountSo.ToString());
        writer.WriteAttributeString("grSO", ScheduleController.Instance.greatStudCountSo.ToString());
        writer.WriteAttributeString("oJ", ScheduleController.Instance.okStudCountJ.ToString());
        writer.WriteAttributeString("gJ", ScheduleController.Instance.goodStudCountJ.ToString());
        writer.WriteAttributeString("grJ", ScheduleController.Instance.greatStudCountJ.ToString());
        writer.WriteAttributeString("oSR", ScheduleController.Instance.okStudCountSe.ToString());
        writer.WriteAttributeString("gSR", ScheduleController.Instance.goodStudCountSe.ToString());
        writer.WriteAttributeString("grSR", ScheduleController.Instance.greatStudCountSe.ToString());
        writer.WriteAttributeString("oG", ScheduleController.Instance.okStudCountG.ToString());
        writer.WriteAttributeString("gG", ScheduleController.Instance.goodStudCountG.ToString());
        writer.WriteAttributeString("grG", ScheduleController.Instance.greatStudCountG.ToString());
        writer.WriteAttributeString("oG2", ScheduleController.Instance.okStudCountG2.ToString());
        writer.WriteAttributeString("gG2", ScheduleController.Instance.goodStudCountG2.ToString());
        writer.WriteAttributeString("grG2", ScheduleController.Instance.greatStudCountG2.ToString());
        writer.WriteEndElement();
        
        writer.WriteStartElement("CurrentGoal");
        writer.WriteAttributeString("id", GoalController.Instance.curGoalId.ToString());
        GoalController.Instance.writeXml(writer);
        writer.WriteEndElement();

        writer.WriteStartElement("CompleteGoals");
        foreach(int gId in GoalController.Instance.completeGoals){
            writer.WriteStartElement("GL");
            writer.WriteAttributeString("gId", gId.ToString());
            writer.WriteEndElement();
        }
        writer.WriteEndElement();

        writer.WriteStartElement("CurrentBuilding");
        writer.WriteAttributeString("id", currentBuilding.ToString());
        writer.WriteEndElement();

        writer.WriteStartElement("Buildings");
        Debug.Log("Writing " + buildings.Count + " number of buildings");
        foreach(Building b in buildings){
            writer.WriteStartElement("Building");
            b.WriteXml(writer);
            writer.WriteEndElement();
        }
        writer.WriteEndElement();

        writer.WriteStartElement("Zones");
        foreach(Zone z in classes){
            writer.WriteStartElement("Zone");
            writer.WriteAttributeString("type", "1");
            z.writeXml(writer);
            writer.WriteEndElement();
        }
        foreach (Zone z in offices){
            writer.WriteStartElement("Zone");
            writer.WriteAttributeString("type", "2");
            z.writeXml(writer);
            writer.WriteEndElement();
        }
        foreach (Zone z in labs){
            writer.WriteStartElement("Zone");
            writer.WriteAttributeString("type", "3");
            z.writeXml(writer);
            writer.WriteEndElement();
        }
        foreach (Zone z in dorms){
            writer.WriteStartElement("Zone");
            writer.WriteAttributeString("type", "4");
            z.writeXml(writer);
            writer.WriteEndElement();
        }
        writer.WriteEndElement();

        writer.WriteStartElement("Characters");
        CharacterController.Instance.writeXml(writer);
        writer.WriteEndElement();

        //writer.WriteStartElement("StudentTuples");
        //CharacterController.Instance.writeCharTuples(writer);
        //writer.WriteEndElement();

        writer.WriteStartElement("Sections");
        AcedemicController.Instance.writeXml(writer);
        writer.WriteEndElement();

        //writer.WriteEndElement();
    }

    public void ReadXml(XmlReader reader){
        int money = 0;
        bool currentBuildingSet = false;
        UIController.Instance.setMoney(1000);
        while(reader.Read()){
            Debug.Log(reader.Name);
            foreach(Character ch in CharacterController.Instance.studList){
                Debug.Log("Name of finished character:" + ch.name + " with Size of sectionList:" + ch.sectionList.Count);
            }
            switch(reader.Name){
                case "Building":
                    //Building b = new Building();
                    if(currentBuildingSet != false){
                        WorldController.Instance.initialBuilding(reader.GetAttribute("name"));
                        Building b = new Building();
                        b.setId(buildings.Count + 1);
                        b.setName(reader.GetAttribute("name"));
                        b.ReadXml(reader);
                        buildings.Add(b);
                    }
                    else{
                        
                        BuildingController.Instance.building.setId(int.Parse(reader.GetAttribute("id")));
                        WorldController.Instance.initialBuilding(reader.GetAttribute("name"));
                        firstBuildingName = reader.GetAttribute("name");
                        Debug.Log("First building with name " + reader.GetAttribute("name"));
                        BuildingController.Instance.building.ReadXml(reader);
                        buildings.Add(new Building());
                        buildings[0].loadBuilding(BuildingController.Instance.building);
                        currentBuildingSet = true;
                    }
                    foreach(Building b in buildings){
                        Debug.Log("Builing in building with id " + b.id);
                    }
                    break;
                case "Characters":
                    CharacterController.Instance.readXml(reader, this);
                    break;
                case "StudentTuples":
                    CharacterController.Instance.readCharTuple(reader);
                    break;
                case "Sections":
                    Debug.Log("Sections");
                    readSections(reader);
                    break;
                case "Zones":
                    readZones(reader);
                    break;
                case "CurrentGoal":
                    GoalController.Instance.readXml(reader);
                    break;
                case "CurrentBuilding":
                    currentBuilding = int.Parse(reader.GetAttribute("id"));
                    break;
                case "CompleteGoals":
                    while(reader.Read()){
                        Debug.Log(reader.Name);
                        if(reader.Name.Equals("GL") == false)
                            break;
                        else
                            GoalController.Instance.completeGoals.Add( int.Parse(reader.GetAttribute("gId")));
                    }
                    GoalController.Instance.loadGoalList();
                    GoalController.Instance.updateGoalList();
                    break;
                case "Time":
                    WorldController.Instance.loadTime(int.Parse(reader.GetAttribute("time2")), 
                                                      int.Parse(reader.GetAttribute("time1")), 
                                                      int.Parse(reader.GetAttribute("day")),
                                                      int.Parse(reader.GetAttribute("year")));
                    if(reader.GetAttribute("sem").Equals("s")){
                        CourseUIController.Instance.semesterFall = false;
                        TimeSlotController.Instance.setSemAsFall();
                        UIController.Instance.setUISemester("S");
                    }
                    else{
                        semFall = true;
                        ScheduleController.Instance.semFall = true;
                        UIController.Instance.setUISemester("F");
                    }
                    break;
                case "WorldInfo":
                    //CharacterController.Instance.profCount = int.Parse(reader.GetAttribute("pCount"));
                    //CharacterController.Instance.studCount = int.Parse(reader.GetAttribute("sCount"));
                    //CharacterController.Instance.gradCount = int.Parse(reader.GetAttribute("gCount"));
                    money = int.Parse(reader.GetAttribute("Money"));
                    reputation = int.Parse(reader.GetAttribute("rep"));
                    UIController.Instance.updateRepUI(reputation);
                    //int saveVer = int.Parse(reader.GetAttribute("ver"));
                    break;
                case "StInfo":
                    GameObject avalableStudents = UIController.Instance.studentPanel.transform.GetChild(24).gameObject;
                    avalableStudents.transform.GetChild(0).GetComponent<Text>().text = reader.GetAttribute("oa");
                    avalableStudents.transform.GetChild(2).GetComponent<Text>().text = reader.GetAttribute("ga");
                    avalableStudents.transform.GetChild(4).GetComponent<Text>().text = reader.GetAttribute("gra");
                    ScheduleController.Instance.okStudCountF = int.Parse(reader.GetAttribute("of"));
                    ScheduleController.Instance.goodStudCountF = int.Parse(reader.GetAttribute("gf"));
                    ScheduleController.Instance.greatStudCountF = int.Parse(reader.GetAttribute("grf"));
                    ScheduleController.Instance.okStudCountSo = int.Parse(reader.GetAttribute("oSO"));
                    ScheduleController.Instance.goodStudCountSo = int.Parse(reader.GetAttribute("gSO"));
                    ScheduleController.Instance.greatStudCountSo = int.Parse(reader.GetAttribute("grSO"));
                    ScheduleController.Instance.okStudCountJ = int.Parse(reader.GetAttribute("oJ"));
                    ScheduleController.Instance.goodStudCountJ = int.Parse(reader.GetAttribute("gJ"));
                    ScheduleController.Instance.greatStudCountJ = int.Parse(reader.GetAttribute("grJ"));
                    ScheduleController.Instance.okStudCountSe = int.Parse(reader.GetAttribute("oSR"));
                    ScheduleController.Instance.goodStudCountSe = int.Parse(reader.GetAttribute("gSR"));
                    ScheduleController.Instance.greatStudCountSe = int.Parse(reader.GetAttribute("grSR"));
                    ScheduleController.Instance.okStudCountG = int.Parse(reader.GetAttribute("oG"));
                    ScheduleController.Instance.goodStudCountG = int.Parse(reader.GetAttribute("gG"));
                    ScheduleController.Instance.greatStudCountG = int.Parse(reader.GetAttribute("grG"));
                    ScheduleController.Instance.okStudCountG2 = int.Parse(reader.GetAttribute("oG2"));
                    ScheduleController.Instance.goodStudCountG2 = int.Parse(reader.GetAttribute("gG2"));
                    ScheduleController.Instance.greatStudCountG2 = int.Parse(reader.GetAttribute("grG2"));
                    break;
            }
        }
        UIController.Instance.setMoney(money);
        Debug.Log( WorldController.Instance.world.classes.Count);
        buildings[0].setName(firstBuildingName);
    }

    public void readZones(XmlReader reader){
        Zone z = null;
        Debug.Log("In zones");
        while(reader.Read()){
            if(reader.Name.Equals("Zone") == false){
                return;
            }
            Debug.Log("In zone with zid:" + int.Parse(reader.GetAttribute("zId")) + " and type " + reader.GetAttribute("type") + " with bid" + int.Parse(reader.GetAttribute("bId"))
                 );//+ " Where bid is " + WorldController.Instance.world.buildings[int.Parse(reader.GetAttribute("bId"))].id);

            //Find the zone type
            string s = "";
            switch (int.Parse(reader.GetAttribute("type"))){
                case 1:
                    s = "class";
                    break;
                case 2:
                    s = "office";
                    break;
                case 3:
                    s = "lab";
                    break;
                case 4:
                    s = "dorm";
                    break;
            }

            z = new Zone(WorldController.Instance.world.buildings[int.Parse(reader.GetAttribute("bId"))], 
                        s, int.Parse(reader.GetAttribute("sDesk")),
                        int.Parse(reader.GetAttribute("oDesk")),
                        int.Parse(reader.GetAttribute("board")),
                        int.Parse(reader.GetAttribute("door")),
                        int.Parse(reader.GetAttribute("bed")),
                        int.Parse(reader.GetAttribute("dresser")),
                        int.Parse(reader.GetAttribute("startX")),
                        int.Parse(reader.GetAttribute("startY")),
                        int.Parse(reader.GetAttribute("endX")),
                        int.Parse(reader.GetAttribute("endY")),
                        int.Parse(reader.GetAttribute("zId"))
            );
            //WorldController.Instance.world.buildings[int.Parse(reader.GetAttribute("bId"))].id = int.Parse(reader.GetAttribute("bId"));
            switch (int.Parse(reader.GetAttribute("type"))){
                case 1:
                    addClass(z);
                    break;
                case 2:
                    addOffice(z);
                    break;
                case 3:
                    addLab(z);
                    break;
                case 4:
                    addDorm(z);
                    break;
            }

            Debug.Log("In zone with class number of" + classes.Count);
        }
    }
    //Read section data from savefile
    public void readSections(XmlReader reader){
        if (reader.IsEmptyElement){
            return;
        }
        Section s = null;
        while(reader.Read()){
            Debug.Log(reader.Name);
            if(reader.Name.Equals("Section")){
                int zId = int.Parse(reader.GetAttribute("zId"));
                Zone zone = null;
                foreach(Zone z in classes){
                    if(z.getId() == zId)
                        zone = z;
                }
                foreach(Zone z in labs){
                    if(z.getId() == zId)
                        zone = z;
                }
                if(zone == null)
                    Debug.Log("Section without zone in save file");
                s = new Section(int.Parse(reader.GetAttribute("cId")), zone);
                s.readXml(reader);
                AcedemicController.Instance.courses[int.Parse(reader.GetAttribute("cId")) - 1].addSection(s);
            }
            else if(reader.Name.Equals("TS")){
                s.readXmlTS(reader);
            }
            else if(reader.Name.Equals("SectionS")){
                int zId = int.Parse(reader.GetAttribute("zId"));
                Zone zone = null;
                foreach(Zone z in classes){
                    if(z.getId() == zId)
                        zone = z;
                }
                foreach(Zone z in labs){
                    if(z.getId() == zId)
                        zone = z;
                }
                if(zone == null)
                    Debug.Log("Section without zone in save file");
                s = new Section(int.Parse(reader.GetAttribute("cId")), zone);
                s.readXml(reader);
                AcedemicController.Instance.courses[int.Parse(reader.GetAttribute("cId")) - 1].addSectionS(s);
            }
            else
                return;
        }
    }
}