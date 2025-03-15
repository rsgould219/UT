using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScheduleController : MonoBehaviour{
    public bool semFall = false;
    public int intOkStudCountF, intOkStudCountSo, intOkStudCountJ, intOkStudCountSe,  intOkStudCountG, intOkStudCountG2 = 0;
    public int intGoodStudCountF, intGoodStudCountSo, intGoodStudCountJ, intGoodStudCountSe,  intGoodStudCountG, intGoodStudCountG2 = 0;
    public int intGreatStudCountF, intGreatStudCountSo, intGreatStudCountJ, intGreatStudCountSe,  intGreatStudCountG, intGreatStudCountG2 = 0;
    public int okStudCountF, okStudCountSo, okStudCountJ, okStudCountSe, okStudCountG, okStudCountG2 = 0;

    public int goodStudCountF, goodStudCountSo, goodStudCountJ, goodStudCountSe, goodStudCountG, goodStudCountG2 = 0;
    public int greatStudCountF, greatStudCountSo, greatStudCountJ, greatStudCountSe, greatStudCountG, greatStudCountG2 = 0;
    public int intOkStudCountBufF, intOkStudCountBufSo, intOkStudCountBufJ, intOkStudCountBufSe, intOkStudCountBufG = 0;
    public int intGoodStudCountBufF, intGoodStudCountBufSo, intGoodStudCountBufJ, intGoodStudCountBufSe, intGoodStudCountBufG = 0;
    public int intGreatStudCountBufF, intGreatStudCountBufSo, intGreatStudCountBufJ, intGreatStudCountBufSe, intGreatStudCountBufG = 0;
    public int okStudCountBufF, okStudCountBufSo, okStudCountBufJ, okStudCountBufSe, okStudCountBufG, okStudCountBufG2 = 0;
    public int goodStudCountBufF, goodStudCountBufSo, goodStudCountBufJ, goodStudCountBufSe, goodStudCountBufG, goodStudCountBufG2 = 0;
    public int greatStudCountBufF, greatStudCountBufSo, greatStudCountBufJ, greatStudCountBufSe, greatStudCountBufG, greatStudCountBufG2 = 0;
    public int humanityListCount = 0;
    public static ScheduleController Instance;
    List<Character> studBuf = new List<Character>();
    List<Character> gradBuf = new List<Character>();
    List<int> electiveQueueId = new List<int>();
    public Dictionary<string, int> perfScores = new Dictionary<string, int>();
    public Dictionary<string, int> archive4Ago = new Dictionary<string, int>();
    public Dictionary<string, int> archive3Ago = new Dictionary<string, int>();
    public Dictionary<string, int> archive2Ago = new Dictionary<string, int>();
    public Dictionary<string, int> archive1Ago = new Dictionary<string, int>();
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }
    public int okTotal(){
        return okStudCountF + okStudCountSo + okStudCountSe + okStudCountJ + okStudCountG + okStudCountG2;
    }
    public int goodTotal(){
        return goodStudCountF + goodStudCountSo + goodStudCountSe + goodStudCountJ + goodStudCountG + goodStudCountG2;
    }
    public int greatTotal(){
        return greatStudCountF + greatStudCountSo + goodStudCountSe + greatStudCountJ + greatStudCountG + greatStudCountG2;
    }
    public int okIntTotal(){
        return intOkStudCountF + intOkStudCountSo + intOkStudCountJ + intOkStudCountSe + intOkStudCountG + intOkStudCountG2;
    }
    public int goodIntTotal(){
        return intGoodStudCountF + intGoodStudCountSo + intGoodStudCountJ + intGoodStudCountSe + intGoodStudCountG + intGoodStudCountG2;
    }
    public int greatIntTotal(){
        return intGreatStudCountF + intGreatStudCountSo + intGreatStudCountJ + intGreatStudCountSe + intGreatStudCountG + intGreatStudCountG2;
    }

    public void setSemester(int i){
        if(i == 1)
            semFall = true;
        else
            semFall = false;
    }

    public void setSemToWorld(){
        semFall = WorldController.Instance.world.semFall;
    }

    public void addToBuf(){
        foreach(Tuple<int, int, int, int[]> t  in CharacterController.Instance.newStudents){
            if(t.Item3 != 0){

                //makes students for each tuple
                for(int i = 0; i < t.Item3; i++){
                    Character ch = new Character(BuildingController.Instance.building.getTileAt(0, 0), t.Item1, t.Item2);
                    ch.setType(Character.CharType.Student);
                    CharacterController.Instance.setImageIds(ch);
                    ch.setName("TestStudent");
                    studBuf.Add(ch);
                }
                //assigns student records
                switch(t.Item2){
                    case 1:
                        intOkStudCountBufF += t.Item4[2];
                        intGoodStudCountBufF += t.Item4[3];
                        intGreatStudCountBufF += t.Item4[4];
                        goodStudCountBufF += t.Item4[0];
                        greatStudCountBufF += t.Item4[1];
                        break;
                    case 2:
                        intOkStudCountBufSo += t.Item4[2];
                        intGoodStudCountBufSo += t.Item4[3];
                        intGreatStudCountBufSo += t.Item4[4];
                        goodStudCountBufSo += t.Item4[0];
                        greatStudCountBufSo += t.Item4[1];
                    
                        break;
                    case 3:
                        intOkStudCountBufJ += t.Item4[2];
                        intGoodStudCountBufJ += t.Item4[3];
                        intGreatStudCountBufJ += t.Item4[4];
                        goodStudCountBufJ += t.Item4[0];
                        greatStudCountBufJ += t.Item4[1];
                        break;
                    case 4:
                        intOkStudCountBufSe += t.Item4[2];
                        intGoodStudCountBufSe += t.Item4[3];
                        intGreatStudCountBufSe += t.Item4[4];
                        goodStudCountBufSe += t.Item4[0];
                        greatStudCountBufSe += t.Item4[1];
                        break;
                }
            }
        }
        foreach(Tuple<int, int, int, int[]> t  in CharacterController.Instance.newGradStudents){
            if(t.Item3 != 0){
                for(int i = 0; i < t.Item3; i++){
                    Character c = new Character(BuildingController.Instance.building.getTileAt(0, 0), t.Item1, t.Item2 - 1);
                    c.setType(Character.CharType.Grad);
                    CharacterController.Instance.setImageIds(c);
                    c.setName("TestGrad");
                    Debug.Log("Adding grad" + t.Item3);
                    gradBuf.Add(c);
                }
                intOkStudCountBufF += t.Item4[2];
                intGoodStudCountBufF += t.Item4[3];
                intGreatStudCountBufF += t.Item4[4];
                goodStudCountBufG += t.Item4[0];
                greatStudCountBufG += t.Item4[1];
            }
        }
    }

    //cleans up buffers after failed scheduling
    public void failureCleanUp(){ 
        foreach(Character c in CharacterController.Instance.studList){
            if (c.sectionListBuf != null && c.sectionListBuf.Count != 0)
                c.sectionListBuf.Clear();
        }
        foreach(Character c in CharacterController.Instance.gradList){
            if (c.sectionListBuf != null && c.sectionListBuf.Count != 0)
                c.sectionListBuf.Clear();
        }
        foreach (Character c in CharacterController.Instance.charactersActive){
            if (c.getType() == Character.CharType.Student && c.sectionListBuf.Count != 0)
                c.sectionListBuf.Clear();
            if (c.getType() == Character.CharType.Grad && c.sectionListBuf.Count != 0)
                c.sectionListBuf.Clear();
        }
        if (studBuf.Count != 0)
            studBuf.Clear();
        if(gradBuf.Count != 0)
            gradBuf.Clear();
        foreach(Course c in AcedemicController.Instance.courses){
            foreach(Section s in c.sections)
                s.clearBuf();
        }
        WorldController.Instance.world.flipWorldSemester();
        setSemToWorld();
    }

    public void checkSchedule(){
        WorldController.Instance.autoSave();
        //check if there are more prof then offices (aka some office was deleted)
        if(WorldController.Instance.world.offices.Count < CharacterController.Instance.profList.Count){
            Debug.Log("Not enough offices for professors. Only " + WorldController.Instance.world.offices.Count);
            UIController.Instance.setInfoBar("Not enough offices for professors");
            return;
        }
        //checks if any section doesn't have any zone or prof
        foreach (Course c in AcedemicController.Instance.courses){
            foreach (Section s in c.sections){
                if(s.getZone() == null){
                    Debug.Log("Some section does not have a zone");
                    UIController.Instance.setInfoBar("Course " + c.courseName + " is missing a class or lab");
                    return;
                }
                if(CharacterController.Instance.firedProfBuf.Contains(s.getProf().getId()) == true){
                    Debug.Log("Some section does not have a prof due to a firing");
                    UIController.Instance.setInfoBar("Course " + c.courseName + " is missing a professor");
                    return;
                }
            }
        }
        //check if there are errors firing/retireing professors
        if(CharacterController.Instance.firedProfBuf.Count != 0){
            foreach(int i in CharacterController.Instance.firedProfBuf){
                Tuple<int, int> t = findProf(i);
                if(t != null && t.Item1 == 0){
                    UIController.Instance.setInfoBar("Error finding professor to fire, reseting requests");
                    CharacterController.Instance.firedProfBuf.Clear();
                    return;
                }
            }
        }
        WorldController.Instance.world.flipWorldSemester();
        setSemToWorld();
        addToBuf();
        //check if spring
        if(semFall == false){
            Debug.Log("Scheduling spring semeseter for " + CharacterController.Instance.studList.Count + " students " + CharacterController.Instance.profList.Count + " professors " + CharacterController.Instance.charactersActive.Count + " actives");
            //manage Seniors
            /*foreach (Character c in CharacterController.Instance.charactersActive){
                if (c.getType() == Character.CharType.Student && c.year == 4){
                    Debug.Log("Senior moving");
                    if (!(scheduleExistingSeniorSpring(c)))
                        return;
                }
            }*/
            foreach(Character c in CharacterController.Instance.studList){
                if(c.year == 4){
                    Debug.Log("Senior moving");
                    if( !(scheduleExistingSeniorSpring(c)))
                        return;
                }
            }
            //manage Juniors
            foreach(Character c in CharacterController.Instance.studList){
                if(c.year == 3){
                    Debug.Log("Junior moving");
                    if( !(scheduleExistingJuniorSpring(c)))
                        return;
                }
            }
            /*foreach (Character c in CharacterController.Instance.charactersActive){
                if (c.getType() == Character.CharType.Student && c.year == 3){
                    Debug.Log("Junior moving");
                    if (!(scheduleExistingJuniorSpring(c)))
                        return;
                }
            }*/
            //manage soft
            foreach(Character c in CharacterController.Instance.studList){
                if(c.year == 2){
                    Debug.Log("Soft moving");
                    if( !(scheduleExistingSoftSpring(c)))
                        return;
                }
            }
            /*foreach (Character c in CharacterController.Instance.charactersActive){
                if (c.getType() == Character.CharType.Student && c.year == 2){
                    Debug.Log("Soft moving");
                    if (!(scheduleExistingSoftSpring(c)))
                        return;
                }
            }*/
            //manage fresh
            foreach(Character c in CharacterController.Instance.studList){
                Debug.Log("Year: " + c.year);
                if(c.year == 1){
                    Debug.Log("Fresh moving");
                    if( !(scheduleExistingFreshSpring(c))){  
                        Debug.Log("Returning complex check as flase");
                        return;
                    }
                }
            }
            /*foreach (Character c in CharacterController.Instance.charactersActive){
                if (c.getType() == Character.CharType.Student && c.year == 1){
                    Debug.Log("Active Fresh moving");
                    if (!(scheduleExistingFreshSpring(c)))
                        return;
                }
            }

            foreach(Character c in CharacterController.Instance.charactersActive){
                if(c.getType() != Character.CharType.Prof){
                    if(scheduleRangeSpring(c) == false)
                        return;
                }
            }*/
            Debug.Log("Scheduling electives for students");
            foreach(Character c in CharacterController.Instance.studList){
                if(scheduleRangeSpring(c) == false)
                    return;
            }
            Debug.Log("Finished scheduling students");
        }
        else{
            Debug.Log("Scheduling fall semeseter for " + (CharacterController.Instance.studList.Count + studBuf.Count) +
                    " students " + CharacterController.Instance.profList.Count + " professors " + CharacterController.Instance.charactersActive.Count + " actives");
            //manage Seniors
            foreach(Character c in studBuf){
                if(c.year == 4){
                    Debug.Log("Senior in buf");
                    if( !(scheduleExistingSenior(c)))
                        return;
                }
            }
            foreach(Character c in CharacterController.Instance.studList){
                if(c.year == 3){
                    Debug.Log("Junior moving up");
                    if( !(scheduleExistingSenior(c)))
                        return;
                }
            }
            /*foreach (Character c in CharacterController.Instance.charactersActive){
                if (c.getType() == Character.CharType.Student && c.year == 3){
                    Debug.Log("Junior moving up");
                    if (!(scheduleExistingSenior(c)))
                        return;
                }
            }*/
            //manage Juniors
            foreach (Character c in studBuf){
                if(c.year == 3){
                    Debug.Log("Junior in buf");
                    if( !(scheduleExistingJunior(c)))
                        return;
                }
            }
            foreach(Character c in CharacterController.Instance.studList){
                if(c.year == 2){
                    Debug.Log("Soft moving");
                    if( !(scheduleExistingJunior(c)))
                        return;
                }
            }
            /*foreach (Character c in CharacterController.Instance.charactersActive){
                if (c.getType() == Character.CharType.Student && c.year == 2){
                    Debug.Log("Soft moving");
                    if (!(scheduleExistingJunior(c)))
                        return;
                }
            }*/
            //manage soft
            foreach (Character c in studBuf){
                if(c.year == 2){
                    Debug.Log("Soft in buf");
                    if( !(scheduleExistingSoft(c)))
                        return;
                }
            }
            foreach(Character c in CharacterController.Instance.studList){
                if(c.year == 1){
                    Debug.Log("Fresh moving");
                    if( !(scheduleExistingSoft(c)))
                        return;
                }
            }
            /*foreach (Character c in CharacterController.Instance.charactersActive){
                if (c.getType() == Character.CharType.Student && c.year == 1){
                    Debug.Log("Fresh moving");
                    if (!(scheduleExistingSoft(c)))
                        return;
                }
            }*/
            //manage fresh
            foreach (Character c in studBuf){
                if(c.year == 1){
                    Debug.Log("Fresh in buf");
                    if( !(scheduleExistingFresh(c)))
                        return;
                }
            }

            /*foreach(Character c in CharacterController.Instance.charactersActive){
                if(c.getType() == Character.CharType.Student){
                    if(scheduleRange(c) == false)
                        return;
                }
            }*/
            foreach(Character c in CharacterController.Instance.studList){
                if(scheduleRange(c) == false)
                    return;
            }
            foreach(Character c in CharacterController.Instance.gradList){
                if(scheduleRangeGrad(c) == false)
                    return;
            }
            foreach(Character c in studBuf){
                if(scheduleRange(c) == false)
                    return;
            }
            foreach(Character c in gradBuf){
                Debug.Log("Starting range schedule for grad");
                if(scheduleRangeGrad(c) == false)
                    return;
            }

             //manage 1st year grads
            foreach (Character c in gradBuf){
                if(c.year == 1){
                    Debug.Log("Fresh grad in buf");
                    if( !(scheduleExistingFreshGrad(c)))
                        return;
                }
            }
            //manage 2nd year grads
            foreach (Character c in gradBuf){
                if(c.year == 2){
                    Debug.Log("Fresh grad in buf");
                    if( !(scheduleExistingSoftGrad(c)))
                        return;
                }
            }
            
            /*foreach(Character c in CharacterController.Instance.charactersActive){
                if(c.getType() == Character.CharType.Grad){
                    if(scheduleRangeGrad(c) == false)
                        return;
                }
            }*/
            Debug.Log("Finished scheduling students");

            //manage humanities
            int i = 0;
            /*foreach(int id in electiveQueueId)
                Debug.Log("elective Queue id:" + id);*/
            foreach(Character c in CharacterController.Instance.studList){
                //Debug.Log("Checking id: " + c.getId() + " with elective Queue id");
                if(electiveQueueId.Contains(c.getId())){
                    Debug.Log("Checking Humanity Elective");
                    if(scheduleHumanityElective(c) != true)
                        return;
                }
            }
            /*foreach (Character c in CharacterController.Instance.charactersActive){
                if(c.getType() == Character.CharType.Student && electiveQueueId.Contains(c.getId())){
                    Debug.Log("Checking Humanity Elective");
                    if(scheduleHumanityElective(c) != true)
                        return;
                }
            }*/
        }
        if(semFall != false)
            checkPerformace();

        //assigning characters 
        Debug.Log("Assigning characters");
        foreach(Character c in studBuf) {
            c.year -= 1;
            CharacterController.Instance.setCharacter(c);
            //CharacterController.Instance.charactersActive.Add(c);
        }
        foreach(Character c in gradBuf) {
            CharacterController.Instance.setCharacter(c);
            //CharacterController.Instance.charactersActive.Add(c);
        }

        Debug.Log("Assign students");
        foreach (Character c in CharacterController.Instance.studList)
            c.assignBuf();
        foreach (Character c in CharacterController.Instance.gradList)
            c.assignBuf();
            
      
        /*
        //make created characters active
        foreach (Character c in CharacterController.Instance.studList)
            CharacterController.Instance.charactersActive.Add(c);
        foreach (Character c in CharacterController.Instance.gradList)
            CharacterController.Instance.charactersActive.Add(c);*/

        UIController.Instance.openPerfMenu();
        startNextSemester();
        Debug.Log("Finished assigning characters");
        //clear tuples of new students
        CharacterController.Instance.newStudents.Clear();
    }

    public bool scheduleExistingSenior(Character c){
        Goal g = new Goal();
        switch(c.getMajor()){
            case 0:
                g = GoalController.Instance.getCSE7();
                break;
            case 1:
                g = GoalController.Instance.getMEC7();
                break;
            case 2:
                g = GoalController.Instance.getCHE7();
                break;
             case 3:
                g = GoalController.Instance.getELE7();
                break;
             case 4:
                g = GoalController.Instance.getCIV7();
                break;
             case 5: 
                g = GoalController.Instance.getISE7();
                break;
             case 6:
                g = GoalController.Instance.getMAT7();
                break;
             case 7:
                g = GoalController.Instance.getPHY7();
                break;
             case 8:
                g = GoalController.Instance.getCHM7();
                break;
             case 9:
                g = GoalController.Instance.getMATH7();
                break;
             case 12:
                g = GoalController.Instance.getECO7();
                break;
             case 13:
                g = GoalController.Instance.getMKT7();
                break;
             case 14:
                g = GoalController.Instance.getACT7();
                break;
             case 15:
                g = GoalController.Instance.getFIN7();
                break;
        }
        if(simpleCheck(c, g) == false)
            return false;
        return true;
    }

    public bool scheduleExistingSeniorSpring(Character c){
        Goal g = new Goal();
        switch(c.getMajor()){
            case 0:
                g = GoalController.Instance.getCSE8();
                break;
            case 1:
                g = GoalController.Instance.getMEC8();
                break;
            case 2:
                g = GoalController.Instance.getCHE8();
                break;
             case 3:
                g = GoalController.Instance.getELE8();
                break;
             case 4:
                g = GoalController.Instance.getCIV8();
                break;
             case 5:
                g = GoalController.Instance.getISE8();
                break;
             case 6:
                g = GoalController.Instance.getMAT8();
                break;
            case 7:
                g = GoalController.Instance.getPHY8();
                break;
             case 8:
                g = GoalController.Instance.getCHM8();
                break;
             case 9:
                g = GoalController.Instance.getMATH8();
                break;
             case 12:
                g = GoalController.Instance.getECO8();
                break;
             case 13:
                g = GoalController.Instance.getMKT8();
                break;
             case 14:
                g = GoalController.Instance.getACT8();
                break;
             case 15:
                g = GoalController.Instance.getFIN8();
                break;
        }
        if (simpleCheck(c, g) == false)
            return false;
        return true;
    }

    public bool scheduleExistingJunior(Character c){
        Goal g = new Goal();
        switch(c.getMajor()){
            case 0:
                g = GoalController.Instance.getCSE5();
                break;
            case 1:
                g = GoalController.Instance.getMEC5();
                break;
            case 2:
                g = GoalController.Instance.getCHE5();
                break;
            case 3:
                g = GoalController.Instance.getELE5();
                break;
            case 4:
                g = GoalController.Instance.getCIV5();
                break;
            case 5:
                g = GoalController.Instance.getISE5();
                break;
            case 6:
                g = GoalController.Instance.getMAT5();
                break;
            case 7:
                g = GoalController.Instance.getPHY5();
                break;
             case 8:
                g = GoalController.Instance.getCHM5();
                break;
             case 9:
                g = GoalController.Instance.getMATH5();
                break;
             case 10:
                g = GoalController.Instance.getENG5();
                break;
             case 11:
                g = GoalController.Instance.getHIST5();
                break;
             case 12:
                g = GoalController.Instance.getECO5();
                break;
             case 13:
                g = GoalController.Instance.getMKT5();
                break;
             case 14:
                g = GoalController.Instance.getACT5();
                break;
             case 15:
                g = GoalController.Instance.getFIN5();
                break;
        }
        if(simpleCheck(c, g) == false)
            return false;
        return true;
    }

    public bool scheduleExistingJuniorSpring(Character c){
        Goal g = new Goal();
        switch(c.getMajor()){
            case 0:
                g = GoalController.Instance.getCSE6();
                break;
            case 1:
                g = GoalController.Instance.getMEC6();
                break;
            case 2:
                g = GoalController.Instance.getCHE6();
                break;
            case 3:
                g = GoalController.Instance.getELE6();
                break;
            case 4:
                g = GoalController.Instance.getCIV6();
                break;
            case 5:
                g = GoalController.Instance.getISE6();
                break;
            case 6:
                g = GoalController.Instance.getMAT6();
                break;
            case 7:
                g = GoalController.Instance.getPHY6();
                break;
             case 8:
                g = GoalController.Instance.getCHM6();
                break;
             case 9:
                g = GoalController.Instance.getMATH6();
                break;
             case 10:
                g = GoalController.Instance.getENG6();
                break;
             case 11:
                g = GoalController.Instance.getHIST6();
                break;
             case 12:
                g = GoalController.Instance.getECO6();
                break;
             case 13:
                g = GoalController.Instance.getMKT6();
                break;
             case 14:
                g = GoalController.Instance.getACT6();
                break;
             case 15:
                g = GoalController.Instance.getFIN6();
                break;
        }
        if(simpleCheck(c, g) == false)
            return false;
        return true;
    }

    public bool scheduleExistingSoft(Character c){
        Goal g;
        switch(c.getMajor()){
            case 0:
                g = GoalController.Instance.getCSE3();
                electiveQueueId.Add(c.getId());
                if(simpleCheck(c, g) == false || extraCheckS(c, g) == false)
                    return false;
                break;
            case 1:
                g = GoalController.Instance.getMEC3();
                if(simpleCheck(c, g) == false || extraCheckS(c, g) == false)
                    return false;
                break;
            case 2:
                g = GoalController.Instance.getCHE3();
                if(simpleCheck(c, g) == false || extraCheckS(c, g) == false)
                    return false;
                break;
             case 3:
                g = GoalController.Instance.getELE3();
                if(simpleCheck(c, g) == false || extraCheckS(c, g) == false)
                    return false;
                break;
             case 4:
                g = GoalController.Instance.getCIV3();
                if(simpleCheck(c, g) == false || extraCheckS(c, g) == false)
                    return false;
                break;
             case 5:
                g = GoalController.Instance.getISE3();
                if(simpleCheck(c, g) == false || extraCheckS(c, g) == false)
                    return false;
                break;
             case 6:
                g = GoalController.Instance.getMAT3();
                if(simpleCheck(c, g) == false || extraCheckS(c, g) == false)
                    return false;
                break;
            case 7:
                g = GoalController.Instance.getPHY3();
                if (simpleCheck(c, g) == false)
                    return false;
                break;
             case 8:
                g = GoalController.Instance.getCHM3();
                if (simpleCheck(c, g) == false)
                    return false;
                break;
             case 9:
                g = GoalController.Instance.getMATH3();
                if (simpleCheck(c, g) == false)
                    return false;
                break;
             case 10:
                g = GoalController.Instance.getENG3();
                if (simpleCheck(c, g) == false)
                    return false;
                break;
             case 11:
                g = GoalController.Instance.getHIST3();
                if (simpleCheck(c, g) == false)
                    return false;
                break;
             case 12:
                g = GoalController.Instance.getECO3();
                if (simpleCheck(c, g) == false)
                    return false;
                break;
             case 13:
                g = GoalController.Instance.getMKT3();
                if (simpleCheck(c, g) == false)
                    return false;
                break;
             case 14:
                g = GoalController.Instance.getACT3();
                if (simpleCheck(c, g) == false)
                    return false;
                break;
             case 15:
                g = GoalController.Instance.getFIN3();
                if (simpleCheck(c, g) == false)
                    return false;
                break;
        }
        return true;
    }

    public bool scheduleExistingSoftGrad(Character c){
        Goal g;
        switch(c.getMajor()){
            case 2:
                g = GoalController.Instance.getCHEG3();
                if (simpleCheck(c, g) == false)
                    return false;
                break;
            case 13:
                g = GoalController.Instance.getBISG3();
                if (simpleCheck(c, g) == false)
                    return false;
                break;
        }
        return true;
    }

    public bool scheduleExistingSoftSpring(Character c){
        Goal g = new Goal();
        switch(c.getMajor()){
            case 0:
                g = GoalController.Instance.getCSE4();
                break;
            case 1:
                g = GoalController.Instance.getMEC4();
                break;
            case 2:
                g = GoalController.Instance.getCHE4();
                break;
             case 3:
                g = GoalController.Instance.getELE4();
                break;
             case 4:
                g = GoalController.Instance.getCIV4();
                break;
             case 5:
                g = GoalController.Instance.getISE4();
                break;
             case 6:
                g = GoalController.Instance.getMAT4();
                break;
            case 7:
                g = GoalController.Instance.getPHY4();
                break;
             case 8:
                g = GoalController.Instance.getCHM4();
                break;
             case 9:
                g = GoalController.Instance.getMATH4();
                break;
             case 10:
                g = GoalController.Instance.getENG4();
                break;
             case 11:
                g = GoalController.Instance.getHIST4();
                break;
             case 12:
                g = GoalController.Instance.getECO4();
                break;
             case 13:
                g = GoalController.Instance.getMKT4();
                break;
             case 14:
                g = GoalController.Instance.getACT4();
                break;
             case 15:
                g = GoalController.Instance.getFIN4();
                break;
        }
        if(simpleCheck(c, g) == false)
            return false;
        return true;
    }

    public bool scheduleExistingSoftGradSpring(Character c){
        Goal g;
        switch(c.getMajor()){
             case 11:
                g = GoalController.Instance.getHISTG4();
                if (simpleCheck(c, g) == false)
                    return false;
                break;
        }
        return true;
    }

    public bool scheduleExistingFresh(Character c){
        Goal g;
        switch(c.getMajor()){
            case 0:
                g = GoalController.Instance.getCSE1("Flag");
                if( simpleCheck(c, g) == false || complexCheck(c, g) == false || extraCheckF(c, g) == false)
                    return false;
                break;
            case 1:
                g = GoalController.Instance.getMEC1("Flag");
                if( simpleCheck(c, g) == false || complexCheck(c, g) == false || extraCheckF(c, g) == false)
                    return false;
                break;
            case 2:
                g = GoalController.Instance.getCHE1("Flag");
                if( simpleCheck(c, g) == false || complexCheck(c, g) == false || extraCheckF(c, g) == false)
                    return false;
                break;
            case 3:
                g = GoalController.Instance.getELE1("Flag");
                if( simpleCheck(c, g) == false || complexCheck(c, g) == false || extraCheckF(c, g) == false)
                    return false;
                break;
            case 4:
                g = GoalController.Instance.getCIV1("Flag");
                if( simpleCheck(c, g) == false || complexCheck(c, g) == false || extraCheckF(c, g) == false)
                    return false;
                break;
            case 5:
                g = GoalController.Instance.getISE1("Flag");
                if( simpleCheck(c, g) == false || complexCheck(c, g) == false || extraCheckF(c, g) == false)
                    return false;
                break;
            case 6:
                g = GoalController.Instance.getMAT1("Flag");
                if( simpleCheck(c, g) == false || complexCheck(c, g) == false || extraCheckF(c, g) == false)
                    return false;
                break;
            case 7:
                g = GoalController.Instance.getPHY1();
                if (simpleCheck(c, g) == false)
                    return false;
                break;
             case 8:
                g = GoalController.Instance.getCHM1();
                if (simpleCheck(c, g) == false)
                    return false;
                break;
             case 9:
                g = GoalController.Instance.getMATH1();
                if (simpleCheck(c, g) == false)
                    return false;
                break;
             case 10:
                g = GoalController.Instance.getENG1();
                if (simpleCheck(c, g) == false)
                    return false;
                break;
             case 11:
                g = GoalController.Instance.getHIST1();
                if (simpleCheck(c, g) == false)
                    return false;
                break;
             case 12:
                g = GoalController.Instance.getECO1();
                if (simpleCheck(c, g) == false)
                    return false;
                break;
             case 13:
                g = GoalController.Instance.getMKT1();
                if (simpleCheck(c, g) == false)
                    return false;
                break;
             case 14:
                g = GoalController.Instance.getACT1();
                if (simpleCheck(c, g) == false)
                    return false;
                break;
             case 15:
                g = GoalController.Instance.getFIN1();
                if (simpleCheck(c, g) == false)
                    return false;
                break;
        }
        return true;
    }

    public bool scheduleExistingFreshGrad(Character c){
        Goal g = new Goal();
        switch(c.getMajor()){
            case 2:
                g = GoalController.Instance.getCHEG1();
                break;
            case 7:
                g = GoalController.Instance.getPHYG1();
                break;
             case 8:
                g = GoalController.Instance.getCHMG1();
                break;
             case 9:
                g = GoalController.Instance.getMATHG1();
                break;
             case 10:
                g = GoalController.Instance.getENGG1();
                break;
             case 11:
                g = GoalController.Instance.getHISTG1();
                break;
             case 12:
                g = GoalController.Instance.getECOG1();
                break;
            case 13:
                g = GoalController.Instance.getBISG1();
                break;
        }
        if( simpleCheck(c, g) == false)
            return false;
        return true;
    }

    public bool scheduleExistingFreshSpring(Character c){
        Goal g;
        switch(c.getMajor()){
            case 0:
                g = GoalController.Instance.getCSE2();
                if( simpleCheck(c, g) == false || complexCheck(c, g) == false || extraCheckS(c, g) == false){
                    Debug.Log("Returning complex check as flase");
                    return false;
                }
                break;
            case 1:
                g = GoalController.Instance.getMEC2();
                if( simpleCheck(c, g) == false || complexCheck(c, g) == false || extraCheckS(c, g) == false)
                    return false;
                break;
            case 2:
                g = GoalController.Instance.getCHE2();
                if( simpleCheck(c, g) == false || complexCheck(c, g) == false || extraCheckS(c, g) == false)
                    return false;
                break;
            case 3:
                g = GoalController.Instance.getELE2();
                if( simpleCheck(c, g) == false || complexCheck(c, g) == false || extraCheckS(c, g) == false)
                    return false;
                break;
            case 4:
                g = GoalController.Instance.getCIV2();
                if( simpleCheck(c, g) == false || complexCheck(c, g) == false || extraCheckS(c, g) == false)
                    return false;
                break;
            case 5:
                g = GoalController.Instance.getISE2();
                if( simpleCheck(c, g) == false || complexCheck(c, g) == false || extraCheckS(c, g) == false)
                    return false;
                break;
            case 6:
                g = GoalController.Instance.getMAT2();
                if( simpleCheck(c, g) == false || complexCheck(c, g) == false || extraCheckS(c, g) == false)
                    return false;
                break;
            case 7:
                g = GoalController.Instance.getPHY2();
                if (simpleCheck(c, g) == false)
                    return false;
                break;
             case 8:
                g = GoalController.Instance.getCHM2();
                if (simpleCheck(c, g) == false)
                    return false;
                break;
             case 9:
                g = GoalController.Instance.getMATH2();
                if (simpleCheck(c, g) == false)
                    return false;
                break;
             case 10:
                g = GoalController.Instance.getENG2();
                if (simpleCheck(c, g) == false)
                    return false;
                break;
             case 11:
                g = GoalController.Instance.getHIST2();
                if (simpleCheck(c, g) == false)
                    return false;
                break;
             case 12:
                g = GoalController.Instance.getECO2();
                if (simpleCheck(c, g) == false)
                    return false;
                break;
             case 13:
                g = GoalController.Instance.getMKT2();
                if (simpleCheck(c, g) == false)
                    return false;
                break;
             case 14:
                g = GoalController.Instance.getACT2();
                if (simpleCheck(c, g) == false)
                    return false;
                break;
             case 15:
                g = GoalController.Instance.getFIN2();
                if (simpleCheck(c, g) == false)
                    return false;
                break;
        }
        Debug.Log("Returning Existing Fresh Spring as true");
        return true;
    }

    public bool scheduleExistingFreshGradSpring(Character c){
        Goal g;
        switch(c.getMajor()){
            case 7:
                g = GoalController.Instance.getPHYG2();
                if (simpleCheck(c, g) == false)
                    return false;
                break;
             case 9:
                g = GoalController.Instance.getMATHG2();
                if (simpleCheck(c, g) == false)
                    return false;
                break;
             case 12:
                g = GoalController.Instance.getECOG2();
                if (simpleCheck(c, g) == false)
                    return false;
                break;
            case 13:
                g = GoalController.Instance.getBISG2();
                if (simpleCheck(c, g) == false)
                    return false;
                break;
        }
        return true;
    }

    //manages range elective goals
    public bool scheduleRangeSpring(Character c){
        switch(c.getMajor()){
            case 0:
                switch(c.year){
                    case 3:
                        if(rangeCheck(c, GoalController.Instance.getCSE6()) == false)
                            return false;
                        break;
                    case 4:
                        if(rangeCheck(c, GoalController.Instance.getCSE8()) == false)
                            return false;
                        break;
                    }
                break;
            case 1:
                switch(c.year){
                    case 4:
                        if(rangeCheck(c, GoalController.Instance.getMEC8()) == false)
                            return false;
                        break;
                    }
                break;
            case 2:
                switch(c.year){
                    case 3:
                        if(rangeCheck(c, GoalController.Instance.getCHE6()) == false)
                            return false;
                        break;
                    case 4:
                        if(rangeCheck(c, GoalController.Instance.getCHE8()) == false)
                            return false;
                        break;
                    }
                break;
            case 3:
                switch(c.year){
                    case 4:
                        if(rangeCheck(c, GoalController.Instance.getELE8()) == false)
                            return false;
                        break;
                    }
                break;
            case 4:
                switch(c.year){
                    case 4:
                        if(rangeCheck(c, GoalController.Instance.getCIV8()) == false)
                            return false;
                        break;
                    }
                break;
            case 5:
                switch(c.year){
                    case 4:
                        if(rangeCheck(c, GoalController.Instance.getISE8()) == false)
                            return false;
                        break;
                    }
                break;
            case 6:
                switch(c.year){
                    case 4:
                        if(rangeCheck(c, GoalController.Instance.getMAT8()) == false)
                            return false;
                        break;
                    }
                break;
            case 7:
                switch(c.year){
                    case 4:
                        if(rangeCheck(c, GoalController.Instance.getPHY8()) == false)
                            return false;
                        break;
                    }
                break;
            case 8:
                switch(c.year){
                    case 4:
                        if(rangeCheck(c, GoalController.Instance.getCHM8()) == false)
                            return false;
                        break;
                    }
                break;
            case 9:
                switch(c.year){
                    case 1:
                        if(rangeCheck(c, GoalController.Instance.getENG2()) == false)
                            return false;
                        break;
                    case 2:
                        if(rangeCheck(c, GoalController.Instance.getENG4()) == false)
                            return false;
                        break;
                    case 3:
                        if(rangeCheck(c, GoalController.Instance.getENG6()) == false)
                            return false;
                        break;
                    }
                break;
            case 10:
                switch(c.year){
                    case 1:
                        if(rangeCheck(c, GoalController.Instance.getHIST2()) == false)
                            return false;
                        break;
                    case 2:
                        if(rangeCheck(c, GoalController.Instance.getHIST4()) == false)
                            return false;
                        break;
                    case 3:
                        if(rangeCheck(c, GoalController.Instance.getHIST6()) == false)
                            return false;
                        break;
                    }
                break;
            case 11:
                switch(c.year){
                    case 3:
                        if(rangeCheck(c, GoalController.Instance.getMATH6() ) == false)
                            return false;
                        break;
                    case 4:
                        if(rangeCheck(c, GoalController.Instance.getMATH8()) == false)
                            return false;
                        break;
                    }
                break;
            case 12:
                switch(c.year){
                    case 3:
                        if(rangeCheck(c, GoalController.Instance.getECO6()) == false)
                            return false;
                        break;
                    case 4:
                        if(rangeCheck(c, GoalController.Instance.getECO8()) == false)
                            return false;
                        break;
                    }
                break;
            case 13:
                switch(c.year){
                    case 4:
                        if(rangeCheck(c, GoalController.Instance.getMKT8()) == false)
                            return false;
                        break;
                    }
                break;
            case 14:
                switch(c.year){
                    case 4:
                        if(rangeCheck(c, GoalController.Instance.getACT8()) == false)
                            return false;
                        break;
                    }
                break;
            case 15:
                switch(c.year){
                    case 3:
                        if(rangeCheck(c, GoalController.Instance.getFIN6()) == false)
                            return false;
                        break;
                    case 4:
                        if(rangeCheck(c, GoalController.Instance.getFIN8()) == false)
                            return false;
                        break;
                    }
                break;
        }
        Debug.Log("Finished scheduling elective");
        return true;
    }

    public bool scheduleRange(Character c){
        switch(c.getMajor()){
            case 0:
                switch(c.year){
                    case 3:
                        if(rangeCheck(c, GoalController.Instance.getCSE5()) == false)
                            return false;
                        break;
                    case 4:
                        if(rangeCheck(c, GoalController.Instance.getCSE7()) == false)
                            return false;
                        break;
                    }
                break;
            case 1:
                switch(c.year){
                    case 4:
                        if(rangeCheck(c, GoalController.Instance.getMEC7()) == false)
                            return false;
                        break;
                    }
                break;
            case 2:
                switch(c.year){
                    case 4:
                        if(rangeCheck(c, GoalController.Instance.getCHE7()) == false)
                            return false;
                        break;
                    }
                break;
            case 3:
                switch(c.year){
                    case 4:
                        if(rangeCheck(c, GoalController.Instance.getELE7()) == false)
                            return false;
                        break;
                    }
                break;
            case 4:
                switch(c.year){
                    case 4:
                        if(rangeCheck(c, GoalController.Instance.getCIV7()) == false)
                            return false;
                        break;
                    }
                break;
            case 5:
                switch(c.year){
                    case 4:
                        if(rangeCheck(c, GoalController.Instance.getISE7()) == false)
                            return false;
                        break;
                    }
                break;
            case 6:
                switch(c.year){
                    case 4:
                        if(rangeCheck(c, GoalController.Instance.getMAT7()) == false)
                            return false;
                        break;
                    }
                break;
            case 7:
                switch(c.year){
                    case 4:
                        if(rangeCheck(c, GoalController.Instance.getPHY7()) == false)
                            return false;
                        break;
                    }
                break;
            case 8:
                switch(c.year){
                    case 4:
                        if(rangeCheck(c, GoalController.Instance.getCHM7()) == false)
                            return false;
                        break;
                    }
                break;
            case 9:
                switch(c.year){
                    case 1:
                        if(rangeCheck(c, GoalController.Instance.getENG1()) == false)
                            return false;
                        break;
                    case 2:
                        if(rangeCheck(c, GoalController.Instance.getENG3()) == false)
                            return false;
                        break;
                    case 3:
                        if(rangeCheck(c, GoalController.Instance.getENG5()) == false)
                            return false;
                        break;
                    }
                break;
            case 10:
                switch(c.year){
                    case 1:
                        if(rangeCheck(c, GoalController.Instance.getHIST1()) == false)
                            return false;
                        break;
                    case 2:
                        if(rangeCheck(c, GoalController.Instance.getHIST3()) == false)
                            return false;
                        break;
                    case 3:
                        if(rangeCheck(c, GoalController.Instance.getHIST5()) == false)
                            return false;
                        break;
                    }
                break;
            case 11:
                switch(c.year){
                    case 3:
                        if(rangeCheck(c, GoalController.Instance.getMATH5()) == false)
                            return false;
                        break;
                    case 4:
                        if(rangeCheck(c, GoalController.Instance.getMATH7()) == false)
                            return false;
                        break;
                    }
                break;
            case 12:
                switch(c.year){
                    case 3:
                        if(rangeCheck(c, GoalController.Instance.getECO5()) == false)
                            return false;
                        break;
                    case 4:
                        if(rangeCheck(c, GoalController.Instance.getECO7()) == false)
                            return false;
                        break;
                    }
                break;
            case 13:
                switch(c.year){
                    case 4:
                        if(rangeCheck(c, GoalController.Instance.getMKT7()) == false)
                            return false;
                        break;
                    }
                break;
            case 14:
                switch(c.year){
                    case 4:
                        if(rangeCheck(c, GoalController.Instance.getACT7()) == false)
                            return false;
                        break;
                    }
                break;
            case 15:
                switch(c.year){
                    case 4:
                        if(rangeCheck(c, GoalController.Instance.getFIN7()) == false)
                            return false;
                        break;
                    }
                break;
        }
        return true;
    }

    public bool scheduleRangeGrad(Character c){
        switch(c.getMajor()){
            case 0:
                switch(c.year){
                    case 1:
                        if(rangeCheck(c, GoalController.Instance.getCSEG1()) == false)
                            return false;
                        break;
                    case 2:
                        if(rangeCheck(c, GoalController.Instance.getCSEG3()) == false)
                            return false;
                        break;
                    }
                break;
            case 1:
                switch(c.year){
                    case 1:
                        if(rangeCheck(c, GoalController.Instance.getMECG1()) == false)
                            return false;
                        break;
                    case 2:
                        if(rangeCheck(c, GoalController.Instance.getMECG3()) == false)
                            return false;
                        break;
                    }
                break;
            case 2:
                switch(c.year){
                    case 2:
                        if(rangeCheck(c, GoalController.Instance.getCHEG3()) == false)
                            return false;
                        break;
                    }
                break;
            case 3:
                switch(c.year){
                    case 1:
                        if(rangeCheck(c, GoalController.Instance.getELEG1()) == false)
                            return false;
                        break;
                    case 2:
                        if(rangeCheck(c, GoalController.Instance.getELEG3()) == false)
                            return false;
                        break;
                    }
                break;
            case 4:
                switch(c.year){
                    case 1:
                        if(rangeCheck(c, GoalController.Instance.getCIVG1()) == false)
                            return false;
                        break;
                    case 2:
                        if(rangeCheck(c, GoalController.Instance.getCIVG3()) == false)
                            return false;
                        break;
                    }
                break;
            case 5:
                switch(c.year){
                    case 1:
                        if(rangeCheck(c, GoalController.Instance.getISEG1()) == false)
                            return false;
                        break;
                    case 2:
                        if(rangeCheck(c, GoalController.Instance.getISEG3()) == false)
                            return false;
                        break;
                    }
                break;
            case 6:
                switch(c.year){
                    case 1:
                        if(rangeCheck(c, GoalController.Instance.getMATG1()) == false)
                            return false;
                        break;
                    case 2:
                        if(rangeCheck(c, GoalController.Instance.getMATG3()) == false)
                            return false;
                        break;
                    }
                break;
            case 7:
                switch(c.year){
                    case 2:
                        if(rangeCheck(c, GoalController.Instance.getPHYG3()) == false)
                            return false;
                        break;
                    }
                break;
            case 8:
                switch(c.year){
                    case 1:
                        if(rangeCheck(c, GoalController.Instance.getCHMG1()) == false)
                            return false;
                        break;
                    case 2:
                        if(rangeCheck(c, GoalController.Instance.getCHMG3()) == false)
                            return false;
                        break;
                    }
                break;
            case 9:
                switch(c.year){
                    case 1:
                        if(rangeCheck(c, GoalController.Instance.getENGG1()) == false)
                            return false;
                        break;
                    case 2:
                        if(rangeCheck(c, GoalController.Instance.getENGG3()) == false)
                            return false;
                        break;
                    }
                break;
            case 10:
                switch(c.year){
                    case 1:
                        if(rangeCheck(c, GoalController.Instance.getHISTG1()) == false)
                            return false;
                        break;
                    case 2:
                        if(rangeCheck(c, GoalController.Instance.getHISTG3()) == false)
                            return false;
                        break;
                    }
                break;
            case 11:
                switch(c.year){
                    case 2:
                        if(rangeCheck(c, GoalController.Instance.getMATHG3()) == false)
                            return false;
                        break;
                }
                break;
            case 12:
                switch(c.year){
                    case 2:
                        if(rangeCheck(c, GoalController.Instance.getECOG3()) == false)
                            return false;
                        break;
                    }
                break;
            case 13:
                switch(c.year){
                    case 2:
                        if(rangeCheck(c, GoalController.Instance.getBISG3()) == false)
                            return false;
                        break;
                }
                break;
        }
        return true;
    }

    public bool scheduleRangeGradSpring(Character c){
        switch(c.getMajor()){
            case 0:
                switch(c.year){
                    case 1:
                        if(rangeCheck(c, GoalController.Instance.getCSEG2()) == false)
                            return false;
                        break;
                    case 2:
                        if(rangeCheck(c, GoalController.Instance.getCSEG4()) == false)
                            return false;
                        break;
                    }
                break;
            case 1:
                switch(c.year){
                    case 1:
                        if(rangeCheck(c, GoalController.Instance.getMECG2()) == false)
                            return false;
                        break;
                    case 2:
                        if(rangeCheck(c, GoalController.Instance.getMECG4()) == false)
                            return false;
                        break;
                    }
                break;
            case 2:
                switch(c.year){
                    case 1:
                        if(rangeCheck(c, GoalController.Instance.getCHEG2()) == false)
                            return false;
                        break;
                    case 2:
                        if(rangeCheck(c, GoalController.Instance.getCHEG4()) == false)
                            return false;
                        break;
                    }
                break;
            case 3:
                switch(c.year){
                    case 1:
                        if(rangeCheck(c, GoalController.Instance.getELEG2()) == false)
                            return false;
                        break;
                    case 2:
                        if(rangeCheck(c, GoalController.Instance.getELEG4()) == false)
                            return false;
                        break;
                    }
                break;
            case 4:
                switch(c.year){
                    case 1:
                        if(rangeCheck(c, GoalController.Instance.getCIVG2()) == false)
                            return false;
                        break;
                    case 2:
                        if(rangeCheck(c, GoalController.Instance.getCIVG4()) == false)
                            return false;
                        break;
                    }
                break;
            case 5:
                switch(c.year){
                    case 1:
                        if(rangeCheck(c, GoalController.Instance.getISEG2()) == false)
                            return false;
                        break;
                    case 2:
                        if(rangeCheck(c, GoalController.Instance.getISEG4()) == false)
                            return false;
                        break;
                    }
                break;
            case 6:
                switch(c.year){
                    case 1:
                        if(rangeCheck(c, GoalController.Instance.getMATG2()) == false)
                            return false;
                        break;
                    case 2:
                        if(rangeCheck(c, GoalController.Instance.getMATG4()) == false)
                            return false;
                        break;
                    }
                break;
            case 7:
                switch(c.year){
                    case 1:
                        if(rangeCheck(c, GoalController.Instance.getPHYG2()) == false)
                            return false;
                        break;
                    case 2:
                        if(rangeCheck(c, GoalController.Instance.getPHYG4()) == false)
                            return false;
                        break;
                    }
                break;
            case 8:
                switch(c.year){
                    case 1:
                        if(rangeCheck(c, GoalController.Instance.getCHMG2()) == false)
                            return false;
                        break;
                    case 2:
                        if(rangeCheck(c, GoalController.Instance.getCHMG4()) == false)
                            return false;
                        break;
                    }
                break;
            case 9:
                switch(c.year){
                    case 1:
                        if(rangeCheck(c, GoalController.Instance.getENGG2()) == false)
                            return false;
                        break;
                    case 2:
                        if(rangeCheck(c, GoalController.Instance.getENGG4()) == false)
                            return false;
                        break;
                    }
                break;
            case 10:
                switch(c.year){
                    case 1:
                        if(rangeCheck(c, GoalController.Instance.getHISTG2()) == false)
                            return false;
                        break;
                    case 2:
                        if(rangeCheck(c, GoalController.Instance.getHISTG4()) == false)
                            return false;
                        break;
                    }
                break;
            case 11:
                switch(c.year){
                    case 1:
                        if(rangeCheck(c, GoalController.Instance.getMATHG2()) == false)
                            return false;
                        break;
                    case 2:
                        if(rangeCheck(c, GoalController.Instance.getMATHG4()) == false)
                            return false;
                        break;
                    }
                break;
            case 12:
                switch(c.year){
                    case 2:
                        if(rangeCheck(c, GoalController.Instance.getECOG4()) == false)
                            return false;
                        break;
                    }
                break;
            case 13:
                switch(c.year){
                    case 2:
                        if(rangeCheck(c, GoalController.Instance.getBISG4()) == false)
                            return false;
                        break;
                }
                break;
        }
        return true;
    }

    public bool extraCheckF(Character c, Goal g){
        foreach(int i in g.getCourseList()){
            if(i == 4){
                if(AcedemicController.Instance.courses[14].sections.Count > AcedemicController.Instance.courses[4].sections.Count)
                    return extraCheckHelper(14, c);
            }
            if(i == 5){
                if(AcedemicController.Instance.courses[15].sections.Count > AcedemicController.Instance.courses[5].sections.Count)
                    return extraCheckHelper(15, c);
            }
        }
        foreach(List<int> l in g.getComplexList()){
            foreach(int i in l){
                if(i == 9){
                    if(AcedemicController.Instance.courses[507].sections.Count > AcedemicController.Instance.courses[9].sections.Count)
                        return extraCheckHelper(11, c);
                    if(AcedemicController.Instance.courses[13].sections.Count > AcedemicController.Instance.courses[9].sections.Count)
                        return extraCheckHelper(13, c);
                }
                else if(i == 10){
                    if(AcedemicController.Instance.courses[12].sections.Count > AcedemicController.Instance.courses[10].sections.Count)
                        return extraCheckHelper(12, c);
                    if(AcedemicController.Instance.courses[26].sections.Count > AcedemicController.Instance.courses[10].sections.Count)
                        return extraCheckHelper(26, c);
                }
                else if(i == 29){
                    if(AcedemicController.Instance.courses[30].sections.Count > AcedemicController.Instance.courses[29].sections.Count)
                        return extraCheckHelper(30, c);
                    if(AcedemicController.Instance.courses[31].sections.Count > AcedemicController.Instance.courses[29].sections.Count)
                        return extraCheckHelper(31, c);
                }
            }
        }
        return true;
    }

    public bool extraCheckS(Character c, Goal g){
        Debug.Log("Checking Extras");
        foreach(int i in g.getCourseList()){
            if(i == 6){
                if(AcedemicController.Instance.courses[16].sectionsS.Count > AcedemicController.Instance.courses[6].sectionsS.Count)
                    return extraCheckHelperS(16, c);
            }
        }
        foreach(List<int> l in g.getComplexList()){
            foreach(int i in l){
            if(i == 29){
                if(AcedemicController.Instance.courses[30].sectionsS.Count > AcedemicController.Instance.courses[29].sectionsS.Count)
                    return extraCheckHelperS(30, c);
                if(AcedemicController.Instance.courses[31].sectionsS.Count > AcedemicController.Instance.courses[29].sectionsS.Count)
                    return extraCheckHelperS(31, c);
                }
            }
        }
        return true;
    }

    public bool extraCheckHelper(int i, Character c){
        foreach(Section s in AcedemicController.Instance.courses[i].sections){
            Debug.Log(s.getSeatCount() - s.getBufCount() + " seats out of " + s.getSeatCount() + " in " + AcedemicController.Instance.courses[i].courseName);
            if(s.getSeatCount() > s.getBufCount()){
                s.incrementBufCount();
                c.addSectionList(s);
                return true;
            }
            Debug.Log(s.getSeatCount() - s.getBufCount() + " seats out of " + s.getSeatCount() + " in " + AcedemicController.Instance.courses[i].courseName);
        }
        Debug.Log("Error scheduleing: Not enough seets in " + AcedemicController.Instance.courses[i].courseName);
        UIController.Instance.setInfoBar("Error scheduleing: Not enough seets in " + AcedemicController.Instance.courses[i].courseName);
        return false;
    }

    public bool extraCheckHelperS(int i, Character c){
        foreach(Section s in AcedemicController.Instance.courses[i].sectionsS){
            Debug.Log(s.getSeatCount() - s.getBufCount() + " seats out of " + s.getSeatCount() + " in " + AcedemicController.Instance.courses[i].courseName);
            if(s.getSeatCount() > s.getBufCount()){
                s.incrementBufCount();
                c.addSectionList(s);
                return true;
            }
            Debug.Log(s.getSeatCount() - s.getBufCount() + " seats out of " + s.getSeatCount() + " in " + AcedemicController.Instance.courses[i].courseName);
        }
        Debug.Log("Error scheduleing: Not enough seets in " + AcedemicController.Instance.courses[i].courseName);
        UIController.Instance.setInfoBar("Error scheduleing: Not enough seets in " + AcedemicController.Instance.courses[i].courseName);
        return false;
    }

    //check the range of electives
    public bool rangeCheck(Character c, Goal g){
        int rangeCount = 0;
        bool seatFound = false;
        foreach(Tuple<int, int, int> t in g.getRangeGoals()){
            if(c.rangeEmpty() == false){
                return rangeConflictCheck(c, t);
            }
            for(int i = t.Item1; i <= t.Item2; i++){
                //check if course is already in students list
                if(c.sectionListBuf != null){
                    foreach(Section s in c.sectionListBuf){
                        if(s.getCourseId() == i + 1){
                            i++;
                            break;
                        }
                    }
                }
                if(semFall == true){
                    foreach(Section s in AcedemicController.Instance.courses[i].sections){
                        Debug.Log(s.getSeatCount() - s.getBufCount() + " seats out of " + s.getSeatCount() + " in " + AcedemicController.Instance.courses[i].courseName);
                        if(s.getSeatCount() > s.getBufCount()){
                            s.incrementBufCount();
                            c.addSectionList(s);
                            seatFound = true;
                            Debug.Log(s.getSeatCount() - s.getBufCount() + " seats out of " + s.getSeatCount() + " in " + AcedemicController.Instance.courses[i].courseName);
                            break;
                        }
                    }
                }
                else{
                    foreach(Section s in AcedemicController.Instance.courses[i].sectionsS){
                        Debug.Log(s.getSeatCount() - s.getBufCount() + " seats out of " + s.getSeatCount() + " in " + AcedemicController.Instance.courses[i].courseName);
                        if(s.getSeatCount() > s.getBufCount()){
                            s.incrementBufCount();
                            c.addSectionList(s);
                            seatFound = true;
                            Debug.Log(s.getSeatCount() - s.getBufCount() + " seats out of " + s.getSeatCount() + " in " + AcedemicController.Instance.courses[i].courseName);
                            break;
                        }
                    }
                }
                if(seatFound){
                    seatFound = false;
                    rangeCount++;
                    Debug.Log("Exit range goal with rangeCount " + rangeCount + " out of " + g.getRangeGoals().Count);
                    break;
                }
            }
        }
        if(rangeCount == g.getRangeGoals().Count){
            Debug.Log("Returning true");
            return true;
        }
        Debug.Log("Returning false");
        return false;
    }

    public bool rangeConflictCheck(Character c, Tuple<int, int, int> t){
        for(int i = t.Item1; i <= t.Item2; i++){
            if(c.checkRange(i) == false)
                if(rangeConflictHelper(i, c) == true)
                    return true;
            else if(i == 9)
                return rangeConflictCheck(c, new Tuple<int, int, int>(t.Item1 + 1, t.Item2, t.Item3));
            else if(i == 10)
                return rangeConflictCheck(c, new Tuple<int, int, int>(t.Item1 + 1, t.Item2, t.Item3));
            else if(i == 386)
                return rangeConflictCheck(c, new Tuple<int, int, int>(t.Item1 + 1, t.Item2, t.Item3));
            else if(i == 388)
                return rangeConflictCheck(c, new Tuple<int, int, int>(t.Item1 + 1, t.Item2, t.Item3));
            else if(i == 390)
                return rangeConflictCheck(c, new Tuple<int, int, int>(t.Item1 + 1, t.Item2, t.Item3));
            else if(i == 392)
                return rangeConflictCheck(c, new Tuple<int, int, int>(t.Item1 + 1, t.Item2, t.Item3));
            else if(i == 394)
                return rangeConflictCheck(c, new Tuple<int, int, int>(t.Item1 + 1, t.Item2, t.Item3));
            else if(i == 396)
                return rangeConflictCheck(c, new Tuple<int, int, int>(t.Item1 + 1, t.Item2, t.Item3));
        }
        return false;
    }

    public bool rangeConflictHelper(int i, Character c){
        if(semFall == true){
            foreach(Section s in AcedemicController.Instance.courses[i].sections){
                Debug.Log(s.getSeatCount() - s.getBufCount() + " seats out of " + s.getSeatCount() + " in " + AcedemicController.Instance.courses[i].courseName);
                if(s.getSeatCount() > s.getBufCount()){
                    s.incrementBufCount();
                    c.addSectionList(s);
                    return true;
                }
                Debug.Log(s.getSeatCount() - s.getBufCount() + " seats out of " + s.getSeatCount() + " in " + AcedemicController.Instance.courses[i].courseName);
            }
        }
        else{
            foreach(Section s in AcedemicController.Instance.courses[i].sectionsS){
                Debug.Log(s.getSeatCount() - s.getBufCount() + " seats out of " + s.getSeatCount() + " in " + AcedemicController.Instance.courses[i].courseName);
                if(s.getSeatCount() > s.getBufCount()){
                    s.incrementBufCount();
                    c.addSectionList(s);
                    return true;
                }
                Debug.Log(s.getSeatCount() - s.getBufCount() + " seats out of " + s.getSeatCount() + " in " + AcedemicController.Instance.courses[i].courseName);
            }
        }
        return false;
    }

    //check all simple courses
    public bool simpleCheck(Character c, Goal g){
        foreach(int i in g.getCourseList()){
            Debug.Log("simple check for " + AcedemicController.Instance.courses[i].courseName);
            bool foundCourse = false;
            //try to find a seat in each section, debug and clean if false
            if(semFall == true){
                foreach(Section s in AcedemicController.Instance.courses[i].sections){
                    Debug.Log(s.getSeatCount() - s.getBufCount() + " seats out of " + s.getSeatCount() + " in " + AcedemicController.Instance.courses[i].courseName);
                    if(s.getSeatCount() > s.getBufCount()){
                        s.incrementBufCount();
                        c.addSectionList(s);
                        foundCourse = true;
                        break;
                    }
                    Debug.Log(s.getSeatCount() - s.getBufCount() + " seats out of " + s.getSeatCount() + " in " + AcedemicController.Instance.courses[i].courseName);
                }
            }
            else{
                foreach(Section s in AcedemicController.Instance.courses[i].sectionsS){
                    Debug.Log(s.getSeatCount() - s.getBufCount() + " seats out of " + s.getSeatCount() + " in " + AcedemicController.Instance.courses[i].courseName);
                    if(s.getSeatCount() > s.getBufCount()){
                        s.incrementBufCount();
                        c.addSectionList(s);
                        foundCourse = true;
                        break;
                    }
                    Debug.Log(s.getSeatCount() - s.getBufCount() + " seats out of " + s.getSeatCount() + " in " + AcedemicController.Instance.courses[i].courseName);
                }
            }
            if( !(foundCourse)){
                Debug.Log("Error scheduleing: Not enough seets in " + AcedemicController.Instance.courses[i].courseName);
                UIController.Instance.setInfoBar("Error scheduleing: Not enough seets in " + AcedemicController.Instance.courses[i].courseName);
                failureCleanUp();
                return false;
            }
            else{
                foundCourse = false;
            }
        }
        return true;
    }

    public bool complexCheck(Character c, Goal g){
        foreach(List<int> l in g.getComplexList()){
            bool foundSeat = false;
            if(semFall == true){
                foreach(int cId in l){
                    foreach(Section s in AcedemicController.Instance.courses[cId].sections){
                        Debug.Log(s.getSeatCount() - s.getBufCount() + " seats out of " + s.getSeatCount() + " in " + AcedemicController.Instance.courses[cId].courseName);
                        if(s.getSeatCount() > s.getBufCount()){
                            s.incrementBufCount();
                            c.addSectionList(s);
                            foundSeat = true;
                            return true;
                        }
                    }
                }
            }
            else{
                foreach(int cId in l){
                    foreach(Section s in AcedemicController.Instance.courses[cId].sectionsS){
                        Debug.Log(s.getSeatCount() - s.getBufCount() + " seats out of " + s.getSeatCount() + " in " + AcedemicController.Instance.courses[cId].courseName);
                        if(s.getSeatCount() > s.getBufCount()){
                            s.incrementBufCount();
                            c.addSectionList(s);
                            foundSeat = true;
                            return true;
                        }
                    }
                }
            }
            if(foundSeat == false){
                Debug.Log("Error scheduleing: Not enough seets in " + AcedemicController.Instance.courses[l[0]].courseName
                            + " or " + AcedemicController.Instance.courses[l[1]].courseName);
                UIController.Instance.setInfoBar("Error scheduleing: Not enough seets in " + AcedemicController.Instance.courses[l[0]].courseName
                            + " or " + AcedemicController.Instance.courses[l[1]].courseName);
                failureCleanUp();
                return false;
            }
            else
                foundSeat = false;
        }
        Debug.Log("Returning complex check as true");
        return true;
    }
    //Attempt to schedule a humanity elective
    public bool scheduleHumanityElective(Character c){
        //try to find a free seat in any elective course, starting with the course after the last filled
        for(int i = 0; i <= AcedemicController.Instance.electiveList.Length; i++){
            Debug.Log("humanity check for " + AcedemicController.Instance.courses[AcedemicController.Instance.electiveList[humanityListCount]].courseName);
            bool foundCourse = false;
            //try to find a seat in each section
            if(semFall == true){
                foreach(Section s in AcedemicController.Instance.courses[AcedemicController.Instance.electiveList[humanityListCount]].sections){
                    Debug.Log(s.getSeatCount() - s.getBufCount() + " seats out of " + s.getSeatCount() + " in " + AcedemicController.Instance.courses[s.getCourseId()].courseName);
                    if(s.getSeatCount() > s.getBufCount()){
                        s.incrementBufCount();
                        c.addSectionList(s);
                        foundCourse = true;
                        break;
                    }
                    Debug.Log(s.getSeatCount() - s.getBufCount() + " seats out of " + s.getSeatCount() + " in " + AcedemicController.Instance.courses[s.getCourseId()].courseName);
                }
            }
            else{
                foreach(Section s in AcedemicController.Instance.courses[AcedemicController.Instance.electiveList[humanityListCount]].sectionsS){
                    Debug.Log(s.getSeatCount() - s.getBufCount() + " seats out of " + s.getSeatCount() + " in " + AcedemicController.Instance.courses[s.getCourseId()].courseName);
                    if(s.getSeatCount() > s.getBufCount()){
                        s.incrementBufCount();
                        c.addSectionList(s);
                        foundCourse = true;
                        break;
                    }
                    Debug.Log(s.getSeatCount() - s.getBufCount() + " seats out of " + s.getSeatCount() + " in " + AcedemicController.Instance.courses[s.getCourseId()].courseName);
                }
            }
            //increment the starting location of the search for next attempt
            humanityListCount++;
            if(humanityListCount == AcedemicController.Instance.electiveList.Length)
                humanityListCount = 0;
            //return if a course was found
            if(foundCourse != false)
                return true;
        }
        //Log and notify, then clean up if no seat was found
        Debug.Log("Error scheduleing: Not enough seets in humanity electives");
        UIController.Instance.setInfoBar("Error scheduleing: Not enough seets in humanity electives");
        failureCleanUp();
        return false;
    }

    public int getPerf(string s){
        int value;
        archive1Ago.TryGetValue(s, out value);
        return value;
        
    }

    public void checkPerformace(){
        archive4Ago.Clear();
        foreach(KeyValuePair<string, int> entry in archive3Ago){
            archive4Ago.Add(entry.Key, entry.Value);
        }
        archive3Ago.Clear();
        foreach(KeyValuePair<string, int> entry in archive2Ago){
            archive4Ago.Add(entry.Key, entry.Value);
        }
        archive2Ago.Clear();
        foreach(KeyValuePair<string, int> entry in archive1Ago){
            archive4Ago.Add(entry.Key, entry.Value);
        }
        archive1Ago.Clear();

        //find the values for 1st year science and math courses
        int phy110 = checkPerformanceSimpleHelper(9);
        int che110 = checkPerformanceSimpleHelper(10);
        int firstSemSci = ((phy110 + che110) / 2) - 20;
        if(AcedemicController.Instance.courses[11].countTotal() > AcedemicController.Instance.courses[9].countTotal()){
            int extraBonus = checkPerformanceSimpleHelper(507) + checkPerformanceSimpleHelper(13) + 
                             checkPerformanceSimpleHelper(12) + checkPerformanceSimpleHelper(26);
            firstSemSci += (extraBonus / 4) / 5;
        }
        int thirdSemSci = checkPerformanceSimpleHelper(29) - 20;
        if(AcedemicController.Instance.courses[30].countTotal() > AcedemicController.Instance.courses[29].countTotal()){
            thirdSemSci += ((checkPerformanceSimpleHelper(30) + checkPerformanceSimpleHelper(31))/2) / 5;
        }

        int firstSemMath = checkPerformanceSimpleHelper(4) - 20;
        int secondSemMath = checkPerformanceSimpleHelper(5) - 20;
        int thirdSemMath = checkPerformanceSimpleHelper(6) - 20;
        if(AcedemicController.Instance.courses[14].countTotal() > AcedemicController.Instance.courses[4].countTotal()){
            firstSemMath += checkPerformanceSimpleHelper(14)/5;
            secondSemMath += checkPerformanceSimpleHelper(16)/5;
            thirdSemMath += checkPerformanceSimpleHelper(16)/5;
        }

        int rangeECOScore1 = checkPerformanceRangeHelper(479, 489);
        int rangeECOScore2 = checkPerformanceRangeHelper(490, 507);
        int rangeMKTScore = checkPerformanceRangeHelper(546, 558);
        int rangeACTScore = checkPerformanceRangeHelper(563, 567);
        int rangeFINScore1 = checkPerformanceRangeHelper(570, 576);
        int rangeFINScore2 = checkPerformanceRangeHelper(559, 563);
        int engrElective = firstSemSci + firstSemMath + secondSemMath + thirdSemSci + thirdSemMath;
        Debug.Log("firstSemSci " + firstSemSci + "firstSemMath" + firstSemMath +  "secondSemMath" + secondSemMath 
                    + "thirdSemSci " + thirdSemSci + "thirdSemMath" + thirdSemMath + " engrElective " + engrElective);
        int coreBis = checkPerformanceRangeHelper(457, 477);

        archive1Ago.Add("CSE", (checkPerformanceSimpleHelper(1) + checkPerformanceSimpleHelper(7) + checkPerformanceSimpleHelper(8) 
                               + checkPerformanceRangeHelper(131, 160) * 29 + engrElective) / 34);
                               
        Debug.Log("CSEScore: " + checkPerformanceSimpleHelper(1));
        archive1Ago.Add("MEC",(checkPerformanceRangeHelper(20, 22) * 3 + checkPerformanceRangeHelper(198, 233) * 34 + engrElective) / 39);
        archive1Ago.Add("CHE", (checkPerformanceSimpleHelper(24) + checkPerformanceRangeHelper(33, 66) * 33 + engrElective) / 36);
        archive1Ago.Add("ELE", (checkPerformanceRangeHelper(234, 267) * 33 + engrElective) / 35);
        archive1Ago.Add("CIV", (checkPerformanceRangeHelper(67, 103) * 36 + engrElective) / 38);
        archive1Ago.Add("ISE", (checkPerformanceRangeHelper(104, 130) * 26 + engrElective) / 28);
        archive1Ago.Add("MAT", (checkPerformanceRangeHelper(161, 197) * 36 + engrElective) / 28);

        archive1Ago.Add("PHY", (checkPerformanceSimpleHelper(9) + checkPerformanceSimpleHelper(507) + checkPerformanceSimpleHelper(13)
                                + checkPerformanceSimpleHelper(29) + checkPerformanceSimpleHelper(30) + checkPerformanceSimpleHelper(31) 
                                + checkPerformanceRangeHelper(404, 422) * 18 + engrElective) / 24);

        archive1Ago.Add("CHM", (checkPerformanceSimpleHelper(10) + checkPerformanceSimpleHelper(12) + checkPerformanceSimpleHelper(26)
                                + checkPerformanceSimpleHelper(23) + checkPerformanceSimpleHelper(25) 
                                + checkPerformanceRangeHelper(423, 451) * 28 + engrElective) / 34);
        archive1Ago.Add("MATH", (firstSemMath + secondSemMath + thirdSemMath + checkPerformanceRangeHelper(520, 526) * 6 + 
                                + checkPerformanceRangeHelper(527, 541) * 14) / 23);
        archive1Ago.Add("ENG", checkPerformanceRangeHelper(268, 301) / 33);
        //special add to lessen the impact of not having any sections in a history course
        archive1Ago.Add("HIST", 100 - (100 - (checkPerformanceRangeHelper(302, 385) / 83) / 2));
        archive1Ago.Add("ECO", (coreBis + checkPerformanceRangeHelper(479, 507)) / 48);
        archive1Ago.Add("MKT", (coreBis + checkPerformanceRangeHelper(543, 558)) / 35);
        archive1Ago.Add("ACT", (coreBis + checkPerformanceRangeHelper(563, 567)) / 24);
        archive1Ago.Add("FIN", (coreBis + checkPerformanceRangeHelper(568, 576)) / 28);

        archive1Ago.Add("CSEG", (checkPerformanceRangeHelper(577, 597)) / 20);
        archive1Ago.Add("CHEG", (checkPerformanceRangeHelper(598, 619)) / 21);
        archive1Ago.Add("MECG", (checkPerformanceRangeHelper(620, 641)) / 21);
        archive1Ago.Add("ELEG", (checkPerformanceRangeHelper(642, 666)) / 22);
        //special add to lessen the impact of not having any sections in a grad civil eng. course
        archive1Ago.Add("CIVG", 100 - (100 - (checkPerformanceRangeHelper(667, 707) / 40) / 2));
        archive1Ago.Add("ISEG", 100 - (100 - (checkPerformanceRangeHelper(708, 742) / 34) / 2));
        archive1Ago.Add("MATG", (checkPerformanceRangeHelper(642, 666)) / 24);
        archive1Ago.Add("PHYG", (checkPerformanceRangeHelper(769, 786)) / 17);
        archive1Ago.Add("CHMG", (checkPerformanceRangeHelper(787, 818)) / 31);
        archive1Ago.Add("MATHG", (checkPerformanceRangeHelper(823, 841)) / 24);
        archive1Ago.Add("ENGG", (checkPerformanceRangeHelper(842, 861)) / 24);
        archive1Ago.Add("HISTG", (checkPerformanceRangeHelper(862, 877)) / 15);
        archive1Ago.Add("BISG", 100 - (100 - (checkPerformanceRangeHelper(879, 919) / 40) / 2));
        archive1Ago.Add("ECOG", 100 - (100 - (checkPerformanceRangeHelper(920, 952) / 32) / 2));
    }

    public int sciHelperMisc(){
        int phy = 0;
        int chm = 0;
        int bio = 0;
        foreach(Character c in CharacterController.Instance.studList) {
            if(c.getMajor() == 13){
                foreach(SectionLite s in c.getSections()){
                    if(s.courseId == 9)
                        phy++;
                    else if(s.courseId == 10)
                        chm++;
                    else if(s.courseId == 11)
                        bio++;
                }
            }
        }
        foreach(Character c in CharacterController.Instance.charactersActive) {
            if(c.getMajor() == 13 && c.getType() == Character.CharType.Prof){
                foreach(SectionLite s in c.getSections()){
                    if(s.courseId == 9)
                        phy++;
                    else if(s.courseId == 10)
                        chm++;
                    else if(s.courseId == 11)
                        bio++;
                }
            }
        }
        int i = phy * checkPerformanceSimpleHelper(9) + chm * checkPerformanceSimpleHelper(10) + bio * checkPerformanceSimpleHelper(11);
        try{
            return i / (phy + chm + bio);
        }
        catch(DivideByZeroException e){
            Debug.Log("Divide by Zero, no science courses found");
            return i / (4);
        }
    }

    public int checkPerformanceGoalHelper(Goal g){
        int totalCount = 0;
        int totalProfAvg = 0;
        foreach(int i in g.getCourseList()){
            int profAvg = 0;
            int seatCount = 0;
            foreach(Section s in AcedemicController.Instance.courses[i].sections){
                 profAvg += s.getSeatCount() * CharacterController.Instance.profSkill[s.getProf().getId()];
                 seatCount += s.getSeatCount();
            }
            if(seatCount != 0)
                Debug.Log("SeatCount:" + seatCount + " " + profAvg);
            totalCount += seatCount;
            totalProfAvg += profAvg;
        }
        if(totalCount != 0){
            Debug.Log("totalProfAvg:" + totalProfAvg + " " + totalCount);
            return totalProfAvg/totalCount;
        }
        else
            return 0;
    }


    public int checkPerformanceSimpleHelper(int i){
        int profAvg = 0;
        int seatCount = 0;
        foreach(Section s in AcedemicController.Instance.courses[i].sections){
            profAvg += s.getSeatCount() * s.getProf().getSkill();
            seatCount += s.getSeatCount();
            Debug.Log(i + "Score: " + s.getProf().getSkill());
        }

        if(seatCount != 0)
            return profAvg/seatCount;
        else
            return 0;
    }

    public int checkPerformanceRangeHelper(int firstId, int lastId){
        int profAvg = 0;
        int seatCount = 0;
        for(int i = firstId; i < lastId; i++){
            foreach(Section s in AcedemicController.Instance.courses[i].sections){
                profAvg += s.getSeatCount() * CharacterController.Instance.profSkill[s.getProf().getId()];
                seatCount += s.getSeatCount();
            }
        }
        Debug.Log("profAvg: " + profAvg + "seatCount" + seatCount);

        if(seatCount != 0)
            return profAvg/seatCount;
        else
            return 0;
    }

    public Tuple<int, int> findProf(int i){
        int index = 0;
        foreach(Character ch in CharacterController.Instance.profList){
            if(ch.getId() == i)
                return new Tuple<int, int>(index, 1);
            else
                index++;
        }
        index = 0;
        foreach(Character ch in CharacterController.Instance.charactersActive){
            if(ch.getId() == i)
                return new Tuple<int, int>(index, 2);
            else
                index++;
        }
        return null;
    }

    public void startNextSemester(){
        CharacterController.Instance.sortTimes();
        WorldController.Instance.advanceYear();
        foreach(Character c in CharacterController.Instance.charactersActive){
            c.setOccupiedTill(99);
            c.setNextDest();
        }
        foreach(Character c in CharacterController.Instance.studList){
            c.setOccupiedTill(99);
            c.setNextDest();
        }
        foreach(Character c in CharacterController.Instance.gradList){
            c.setOccupiedTill(99);
            c.setNextDest();
        }
        foreach(Character c in CharacterController.Instance.profList){
            c.setOccupiedTill(99);
            c.setNextDest();
        }

        gradBuf.Clear();
        studBuf.Clear();

        //fire professrs set to be fired
        foreach(int i in CharacterController.Instance.firedProfBuf){
            int index, flag = 0;
            Tuple<int, int> t = findProf(i);
            if(t.Item1 == 1){
                GameObject.Destroy(CharacterController.Instance.profList[t.Item2].getGO());
                CharacterController.Instance.profList.RemoveAt(t.Item2);
            }else if(t.Item1 == 2){
                GameObject.Destroy(CharacterController.Instance.charactersActive[t.Item2].getGO());
                CharacterController.Instance.charactersActive.RemoveAt(t.Item2);
            }
        }

        //update totals and shift student counts for graduation
        if(semFall == true){
            WorldController.Instance.world.addTotalCount(intOkStudCountSe + intGoodStudCountSe + intGreatStudCountSe + okStudCountSe + goodStudCountSe + greatStudCountSe,
                                                         intOkStudCountG2 + intGoodStudCountG2 + intGreatStudCountG2 + okStudCountG2 + goodStudCountG2 + greatStudCountG2);
            WorldController.Instance.world.addToIntCount(intGoodStudCountSe, intGreatStudCountSe, intGoodStudCountG2, intGreatStudCountG2);
            WorldController.Instance.world.addSkillCounts(goodStudCountSe, goodStudCountG2, greatStudCountSe, greatStudCountG2);
            
            //sort International students and ranked students
            intOkStudCountG2 = intOkStudCountG;
            intOkStudCountG = intOkStudCountBufG;
            intOkStudCountSe = intOkStudCountBufSe + intOkStudCountJ;
            intOkStudCountJ = intOkStudCountBufJ + intOkStudCountSo;
            intOkStudCountSo = intOkStudCountBufSo + intOkStudCountF;
            intOkStudCountF = intOkStudCountBufF;

            intGoodStudCountG2 = intGoodStudCountG;
            intGoodStudCountG = intGoodStudCountBufG;
            intGoodStudCountSe = intGoodStudCountBufSe + intGoodStudCountJ;
            intGoodStudCountJ = intGoodStudCountBufJ + intGoodStudCountSo;
            intGoodStudCountSo = intGoodStudCountBufSo + intGoodStudCountF;
            intGoodStudCountF = intGoodStudCountBufF;

            intGreatStudCountG2 = intGreatStudCountG;
            intGreatStudCountG = intGreatStudCountBufG;
            intGreatStudCountSe = intGreatStudCountBufSe + intGreatStudCountJ;
            intGreatStudCountJ = intGreatStudCountBufJ + intGreatStudCountSo;
            intGreatStudCountSo = intGreatStudCountBufSo + intGreatStudCountF;
            intGreatStudCountF = intGreatStudCountBufF;

            okStudCountG2 = okStudCountG;
            okStudCountG = okStudCountBufG;
            okStudCountSe = okStudCountBufSe + okStudCountJ;
            okStudCountJ = okStudCountBufJ + okStudCountSo;
            okStudCountSo = okStudCountBufSo + okStudCountF;
            okStudCountF = okStudCountBufF;

            goodStudCountG2 = goodStudCountG;
            goodStudCountG = goodStudCountBufG;
            goodStudCountSe = goodStudCountBufSe + goodStudCountJ;
            goodStudCountJ = goodStudCountBufJ + goodStudCountSo;
            goodStudCountSo = goodStudCountBufSo + goodStudCountF;
            goodStudCountF = goodStudCountBufF;

            greatStudCountG2 = greatStudCountG;
            greatStudCountG = greatStudCountBufG;
            greatStudCountSe = greatStudCountBufSe + greatStudCountJ;
            greatStudCountJ = greatStudCountBufJ + greatStudCountSo;
            greatStudCountSo = greatStudCountBufSo + greatStudCountF;
            greatStudCountF = greatStudCountBufF;

            
            CharacterController.Instance.firedProfBuf.Clear();
            CharacterController.Instance.professorProgress();
            WorldController.Instance.world.reputationTally();
            //set queue of TimeSlots

            UIController.Instance.resetType();
        }
        addIntStudents();
        if(semFall == false)
            UIController.Instance.addLocalStudents();
            //UIController.Instance.addIntStudents(WorldController.Instance.world.getDormCount() - intOkStudCountF - intOkStudCountSo - 
                                                //intOkStudCountJ - intOkStudCountSe - intOkStudCountG - intOkStudCountG2 - intGoodStudCountF - intGoodStudCountSo - 
                                                //intGoodStudCountJ - intGoodStudCountSe - intGoodStudCountG - intGoodStudCountG2 -intGoodStudCountF - 
                                                //intGoodStudCountSo - intGoodStudCountJ - intGoodStudCountSe - intGoodStudCountG - intGoodStudCountG2);
        
        //manage finances andtime change
        manageSemFin();
        setSemToWorld();
        
        //set message and ui for new semester
        if(semFall != true){
            UIController.Instance.setInfoBar("Scheduling Successful welcome to Spring");
            UIController.Instance.setUISemester("S");
        }
        else{
            UIController.Instance.setInfoBar("Scheduling Successful welcome to Fall");
            UIController.Instance.setUISemester("F");
        }
    }
    public void addIntStudents(){
        CharacterController.Instance.newStudents.Clear();
        UIController.Instance.clearStudentCounts();
        if(semFall == true)
            return;
        //Find counts of possilbe new students
        int currentIntOk = intOkStudCountF + intOkStudCountSo + intOkStudCountJ;
        int currentIntGood = intGoodStudCountF + intGoodStudCountSo + intGoodStudCountJ;
        int currentIntGreat = intGreatStudCountF + intGreatStudCountSo + intGreatStudCountJ;
        UIController.Instance.addIntStudents(currentIntOk, currentIntGood, currentIntGreat);
    }
    //handle salary and tuition revenue and expenses
    public void manageSemFin(){
        int money = UIController.Instance.getMoney();
        foreach(Character c in CharacterController.Instance.studList)
            money += 10000;
        foreach(Character c in CharacterController.Instance.gradList)
            money += 10000;
        foreach(Character c in CharacterController.Instance.profList)
            money -= c.getSalary();
        /*foreach (Character c in CharacterController.Instance.charactersActive){
            if (c.getType() == Character.CharType.Student)
                money += 10000;
            if (c.getType() == Character.CharType.Grad)
                money += 10000;
            if (c.getType() == Character.CharType.Prof)
                money -= c.getSalary();
        }*/
        UIController.Instance.setMoney(money);
        UIController.Instance.newSemesterFinance();
    }
}