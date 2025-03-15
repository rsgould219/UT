using System;
using System.Xml;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalController : MonoBehaviour{
    public const int goalReward = 10000;
    public const int repReward = 5;
    public static GoalController Instance;
    public GameObject goalDesc;
    public int curGoalId;
    public Goal curGoal;
    public List<Goal> goalList = new List<Goal>();
    public List<int> goalIntList = new List<int>();
    public List<int> completeGoals = new List<int>();
    public Dictionary<string, int> complexCounts = new Dictionary<string, int>();

    // Start is called before the first frame update
    void Start(){
        Instance = this;
        loadGoalList();
        updateGoalList();
       
    }

    public void loadGoalList(){ 
        goalList.Clear();
        goalIntList.Clear();
        for(int i = 1; i < 189; i++){
            if(!(completeGoals.Contains(i))){
                goalIntList.Add(i);
                switch(i){
                    case 1:
                        goalList.Add(getCSE1());
                        i = 10;
                        break;
                    case 2:
                        goalList.Add(getCSE2());
                        i = 10;
                        break;
                    case 3:
                        goalList.Add(getCSE3());
                        i = 10;
                        break;
                    case 4:
                        goalList.Add(getCSE4());
                        i = 10;
                        break;
                    case 5:
                        goalList.Add(getCSE5());
                        i = 10;
                        break;
                    case 6:
                        goalList.Add(getCSE6());
                        i = 10;
                        break;
                    case 7:
                        goalList.Add(getCSE7());
                        i = 8;
                        break;
                    case 8:
                        goalList.Add(getCSE8());
                        break;
                    case 9:
                        goalList.Add(getCSEMathRec());
                        break;
                    case 10:
                        goalList.Add(getCSEsci());
                        break;
                    case 11:
                        goalList.Add(getMEC1());
                        i = 21; 
                        break;
                    case 12:
                        goalList.Add(getMEC2());
                        i = 21; 
                        break;
                    case 13:
                        goalList.Add(getMEC3());
                        i = 21; 
                        break;
                    case 14:
                        goalList.Add(getMEC4());
                        i = 21; 
                        break;
                    case 15:
                        goalList.Add(getMEC5());
                        i = 21; 
                        break;
                    case 16:
                        goalList.Add(getMEC6());
                        i = 21; 
                        break;
                    case 17:
                        goalList.Add(getMEC7());
                        i = 18; 
                        break;
                    case 18:
                        goalList.Add(getMEC8());
                        break;
                    case 19:
                        goalList.Add(getMECLab());
                        break;
                    case 20:
                        goalList.Add(getMECMathRec());
                        break;
                    case 21:
                        goalList.Add(getMECsci());
                        break;
                    case 22:
                        goalList.Add(getCHE1());
                        i = 32;
                        break;
                    case 23:
                        goalList.Add(getCHE2());
                        i = 32;
                        break;
                    case 24:
                        goalList.Add(getCHE3());
                        i = 32;
                        break;
                    case 25:
                        goalList.Add(getCHE4());
                        i = 32;
                        break;
                    case 26:
                        goalList.Add(getCHE5());
                        i = 32;
                        break;
                    case 27:
                        goalList.Add(getCHE6());
                        i = 32;
                        break;
                    case 28:
                        goalList.Add(getCHE7());
                        i = 29;
                        break;
                    case 29:
                        goalList.Add(getCHE8());
                        break;
                    case 30:
                        goalList.Add(getCHELab());
                        break;
                    case 31:
                        goalList.Add(getCHEMathRec());
                        break;
                    case 32:
                        goalList.Add(getCHEsci());
                        break;
                    case 33:
                        goalList.Add(getELE1());
                        i = 42;
                        break;
                    case 34:
                        goalList.Add(getELE2());
                        i = 42;
                        break;
                    case 35:
                        goalList.Add(getELE3());
                        i = 42;
                        break;
                    case 36:
                        goalList.Add(getELE4());
                        i = 42;
                        break;
                    case 37:
                        goalList.Add(getELE5());
                        i = 42;
                        break;
                    case 38:
                        goalList.Add(getELE6());
                        i = 42;
                        break;
                    case 39:
                        goalList.Add(getELE7());
                        i = 40;
                        break;
                    case 40:
                        goalList.Add(getELE8());
                        break;
                    case 41:
                        goalList.Add(getELEMathRec());
                        break;
                    case 42:
                        goalList.Add(getELEsci());
                        break;
                    case 43:
                        goalList.Add(getCIV1());
                        i = 52;
                        break;
                    case 44:
                        goalList.Add(getCIV2());
                        i = 52;
                        break;
                    case 45:
                        goalList.Add(getCIV3());
                        i = 52;
                        break;
                    case 46:
                        goalList.Add(getCIV4());
                        i = 52;
                        break;
                    case 47:
                        goalList.Add(getCIV5());
                        i = 52;
                        break;
                    case 48:
                        goalList.Add(getCIV6());
                        i = 52;
                        break;
                    case 49:
                        goalList.Add(getCIV7());
                        i = 50;
                        break;
                    case 50:
                        goalList.Add(getCIV8());
                        break;
                    case 51:
                        goalList.Add(getCIVMathRec());
                        break;
                    case 52:
                        goalList.Add(getCIVsci());
                        break;
                    case 53:
                        goalList.Add(getISE1());
                        i = 62;
                        break;
                    case 54:
                        goalList.Add(getISE2());
                        i = 62;
                        break;
                    case 55:
                        goalList.Add(getISE3());
                        i = 62;
                        break;
                    case 56:
                        goalList.Add(getISE4());
                        i = 62;
                        break;
                    case 57:
                        goalList.Add(getISE5());
                        i = 62;
                        break;
                    case 58:
                        goalList.Add(getISE6());
                        i = 62;
                        break;
                    case 59:
                        goalList.Add(getISE7());
                        i = 60;
                        break;
                    case 60:
                        goalList.Add(getISE8());
                        break;
                    case 61:
                        goalList.Add(getISEMathRec());
                        break;
                    case 62:
                        goalList.Add(getISEsci());
                        break;
                    case 63:
                        goalList.Add(getMAT1());
                        i = 72;
                        break;
                    case 64:
                        goalList.Add(getMAT2());
                        i = 72;
                        break;
                    case 65:
                        goalList.Add(getMAT3());
                        i = 72;
                        break;
                    case 66:
                        goalList.Add(getMAT4());
                        i = 72;
                        break;
                    case 67:
                        goalList.Add(getMAT5());
                        i = 72;
                        break;
                    case 68:
                        goalList.Add(getMAT6());
                        i = 72;
                        break;
                    case 69:
                        goalList.Add(getMAT7());
                        i = 70;
                        break;
                    case 70:
                        goalList.Add(getMAT8());
                        break;
                    case 71:
                        goalList.Add(getMATMathRec());
                        break;
                    case 72:
                        goalList.Add(getMATsci());
                        break;
                    case 73:
                        goalList.Add(getPHY1());
                        i = 82;
                        break;
                    case 74:
                        goalList.Add(getPHY2());
                        i = 82;
                        break;
                    case 75:
                        goalList.Add(getPHY3());
                        i = 82;
                        break;
                    case 76:
                        goalList.Add(getPHY4());
                        i = 82;
                        break;
                    case 77:
                        goalList.Add(getPHY5());
                        i = 82;
                        break;
                    case 78:
                        goalList.Add(getPHY6());
                        i = 82;
                        break;
                    case 79:
                        goalList.Add(getPHY7());
                        i = 80;
                        break;
                    case 80:
                        goalList.Add(getPHY8());
                        break;
                    case 81:
                        goalList.Add(getPHYMathRec());
                        break;
                    case 82:
                        goalList.Add(getPHYsci());
                        break;
                    case 83:
                        goalList.Add(getCHE1());
                        i = 92;
                        break;
                    case 84:
                        goalList.Add(getCHE2());
                        i = 92;
                        break;
                    case 85:
                        goalList.Add(getCHE3());
                        i = 92;
                        break;
                    case 86:
                        goalList.Add(getCHE4());
                        i = 92;
                        break;
                    case 87:
                        goalList.Add(getCHE5());
                        i = 92;
                        break;
                    case 88:
                        goalList.Add(getCHE6());
                        i = 92;
                        break;
                    case 89:
                        goalList.Add(getCHE7());
                        i = 90;
                        break;
                    case 90:
                        goalList.Add(getCHE8());
                        break;
                    case 91:
                        goalList.Add(getCHEMathRec());
                        break;
                    case 92:
                        goalList.Add(getCHEsci());
                        break;
                    case 93:
                        goalList.Add(getENG1());
                        i = 98;
                        break;
                    case 94:
                        goalList.Add(getENG2());
                        i = 98;
                        break;
                    case 95:
                        goalList.Add(getENG3());
                        i = 98;
                        break;
                    case 96:
                        goalList.Add(getENG4());
                        i = 98;
                        break;
                    case 97:
                        goalList.Add(getENG5());
                        i = 98;
                        break;
                    case 98:
                        goalList.Add(getENG6());
                        break;
                    case 99:
                        goalList.Add(getHIST1());
                        i = 104;
                        break;
                    case 100:
                        goalList.Add(getHIST2());
                        i = 104;
                        break;
                    case 101:
                        goalList.Add(getHIST3());
                        i = 104;
                        break;
                    case 102:
                        goalList.Add(getHIST4());
                        i = 104;
                        break;
                    case 103:
                        goalList.Add(getHIST5());
                        i = 104;
                        break;
                    case 104:
                        goalList.Add(getHIST6());
                        break;
                    case 105:
                        goalList.Add(getECO1());
                        i = 112;
                        break;
                    case 106:
                        goalList.Add(getECO2());
                        i = 112;
                        break;
                    case 107:
                        goalList.Add(getECO3());
                        i = 112;
                        break;
                    case 108:
                        goalList.Add(getECO4());
                        i = 112;
                        break;
                    case 109:
                        goalList.Add(getECO5());
                        i = 112;
                        break;
                    case 110:
                        goalList.Add(getECO6());
                        i = 112;
                        break;
                    case 111:
                        goalList.Add(getECO7());
                        i = 112;
                        break;
                    case 112:
                        goalList.Add(getECO8());
                        break;
                    case 113:
                        goalList.Add(getMKT1());
                        i = 120;
                        break;
                    case 114:
                        goalList.Add(getMKT2());
                        i = 120;
                        break;
                    case 115:
                        goalList.Add(getMKT3());
                        i = 120;
                        break;
                    case 116:
                        goalList.Add(getMKT4());
                        i = 120;
                        break;
                    case 117:
                        goalList.Add(getMKT5());
                        i = 120;
                        break;
                    case 118:
                        goalList.Add(getMKT6());
                        i = 120;
                        break;
                    case 119:
                        goalList.Add(getMKT7());
                        i = 120;
                        break;
                    case 120:
                        goalList.Add(getMKT8());
                        break;
                    case 121:
                        goalList.Add(getACT1());
                        i = 128;
                        break;
                    case 122:
                        goalList.Add(getACT2());
                        i = 128;
                        break;
                    case 123:
                        goalList.Add(getACT3());
                        i = 128;
                        break;
                    case 124:
                        goalList.Add(getACT4());
                        i = 128;
                        break;
                    case 125:
                        goalList.Add(getACT5());
                        i = 128;
                        break;
                    case 126:
                        goalList.Add(getACT6());
                        i = 128;
                        break;
                    case 127:
                        goalList.Add(getACT7());
                        i = 128;
                        break;
                    case 128:
                        goalList.Add(getACT8());
                        break;
                    case 129:
                        goalList.Add(getFIN1());
                        i = 136;
                        break;
                    case 130:
                        goalList.Add(getFIN2());
                        i = 136;
                        break;
                    case 131:
                        goalList.Add(getFIN3());
                        i = 136;
                        break;
                    case 132:
                        goalList.Add(getFIN4());
                        i = 136;
                        break;
                    case 133:
                        goalList.Add(getFIN5());
                        i = 136;
                        break;
                    case 134:
                        goalList.Add(getFIN6());
                        i = 136;
                        break;
                    case 135:
                        goalList.Add(getFIN7());
                        i = 136;
                        break;
                    case 136:
                        goalList.Add(getFIN8());
                        break;
                    case 137:
                        if(goalIntList.Contains(8))
                            goalList.Add(getCSEG1());
                        i = 140;
                        break;
                    case 138:
                        goalList.Add(getCSEG2());
                        i = 140;
                        break;
                    case 139:
                        goalList.Add(getCSEG3());
                        i = 140;
                        break;
                    case 140:
                        goalList.Add(getCSEG4());
                        i = 140;
                        break;
                    case 141:
                        if(goalIntList.Contains(18))
                            goalList.Add(getMECG1());
                        i = 144;
                        break;
                    case 142:
                        goalList.Add(getMECG2());
                        i = 144;
                        break;
                    case 143:
                        goalList.Add(getMECG3());
                        i = 144;
                        break;
                    case 144:
                        goalList.Add(getMECG4());
                        break;
                    case 145:
                        if(goalIntList.Contains(29))
                            goalList.Add(getCHEG1());
                        i = 148;
                        break;
                    case 146:
                        goalList.Add(getCHEG2());
                        i = 148;
                        break;
                    case 147:
                        goalList.Add(getCHEG3());
                        i = 148;
                        break;
                    case 148:
                        goalList.Add(getCHEG4());
                        break;
                    case 149:
                        if(goalIntList.Contains(40))
                            goalList.Add(getELEG1());
                        i = 152;
                        break;
                    case 150:
                        goalList.Add(getELEG2());
                        i = 152;
                        break;
                    case 151:
                        goalList.Add(getELEG3());
                        i = 152;
                        break;
                    case 152:
                        goalList.Add(getELEG4());
                        break;
                    case 153:
                        if(goalIntList.Contains(50))
                            goalList.Add(getCIVG1());
                        i = 156;
                        break;
                    case 154:
                        goalList.Add(getCIVG2());
                        i = 156;
                        break;
                    case 155:
                        goalList.Add(getCIVG3());
                        i = 156;
                        break;
                    case 156:
                        goalList.Add(getCIVG4());
                        break;
                    case 157:
                        if(goalIntList.Contains(60))
                            goalList.Add(getISEG1());
                        i = 160;
                        break;
                    case 158:
                        goalList.Add(getISEG2());
                        i = 160;
                        break;
                    case 159:
                        goalList.Add(getISEG3());
                        i = 160;
                        break;
                    case 160:
                        goalList.Add(getISEG4());
                        break;
                    case 161:
                        if(goalIntList.Contains(70))
                            goalList.Add(getMATG1());
                        i = 164;
                        break;
                    case 162:
                        goalList.Add(getMATG2());
                        i = 164;
                        break;
                    case 163:
                        goalList.Add(getMATG3());
                        i = 164;
                        break;
                    case 164:
                        goalList.Add(getMATG4());
                        break;
                    case 165:
                        if(goalIntList.Contains(80))
                            goalList.Add(getPHYG1());
                        i = 168;
                        break;
                    case 166:
                        goalList.Add(getPHYG2());
                        i = 168;
                        break;
                    case 167:
                        goalList.Add(getPHYG3());
                        i = 168;
                        break;
                    case 168:
                        goalList.Add(getPHYG4());
                        break;
                    case 169:
                        if(goalIntList.Contains(90))
                            goalList.Add(getCHEG1());
                        i = 172;
                        break;
                    case 170:
                        goalList.Add(getCHEG2());
                        i = 172;
                        break;
                    case 171:
                        goalList.Add(getCHEG3());
                        i = 172;
                        break;
                    case 172:
                        goalList.Add(getCHEG4());
                        break;
                    case 173:
                        if(goalIntList.Contains(98))
                            goalList.Add(getENGG1());
                        i = 176;
                        break;
                    case 174:
                        goalList.Add(getENGG2());
                        i = 176;
                        break;
                    case 175:
                        goalList.Add(getENGG3());
                        i = 176;
                        break;
                    case 176:
                        goalList.Add(getENGG4());
                        break;
                    case 177:
                        if(goalIntList.Contains(104))
                            goalList.Add(getHISTG1());
                        i = 180;
                        break;
                    case 178:
                        goalList.Add(getHISTG2());
                        i = 180;
                        break;
                    case 179:
                        goalList.Add(getHISTG3());
                        i = 180;
                        break;
                    case 180:
                        goalList.Add(getHISTG4());
                        break;
                    case 181:
                        if(goalIntList.Contains(112))
                            goalList.Add(getECOG1());
                        i = 184;
                        break;
                    case 182:
                        goalList.Add(getECOG2());
                        i = 184;
                        break;
                    case 183:
                        goalList.Add(getECOG3());
                        i = 184;
                        break;
                    case 184:
                        goalList.Add(getECOG4());
                        break;
                    case 185:
                        if(goalIntList.Contains(112))
                            goalList.Add(getBISG1());
                        i = 188;
                        break;
                    case 186:
                        goalList.Add(getBISG2());
                        i = 188;
                        break;
                    case 187:
                        goalList.Add(getBISG3());
                        i = 188;
                        break;
                    case 188:
                        goalList.Add(getBISG4());
                        break;
                    }
            }
        }
    }

    public bool goalsMet(int i){
        if(completeGoals.Contains(i) && completeGoals.Contains(i+1) && completeGoals.Contains(i+2) &&
            completeGoals.Contains(i+3) && completeGoals.Contains(i+4) && completeGoals.Contains(i+5) &&
            completeGoals.Contains(i+6) && completeGoals.Contains(i+7)){
            return true;
        }
        else
            return false;
    }

    public void updateGoalList(){
        List<string> sList = new List<string>();
        foreach(Goal g in goalList){
            sList.Add(g.name);
        }
        GameObject goalDrop = GameObject.Find("GoalDropdown");
        goalDrop.GetComponent<Dropdown>().ClearOptions();
        goalDrop.GetComponent<Dropdown>().AddOptions(sList);
    }

    public void sectionAddedChecks(int courseId, int seats){
        if(curGoal == null){
            return;
        }
        Debug.Log("Checking section added with cId: " + courseId + " and seats = " +seats);
        //chack extra courses (aka labs and recitations)
        if(curGoal.getExtraGoals().Count != 0){
            optionalExtraCheck(courseId, seats);
        }
        if(curGoal.getRangeGoals().Count != 0){
            rangeCheck(courseId, seats);
        }
        //check cse elective
        else if(courseId > 143 && courseId < 161){
            if(curGoal.name.StartsWith("CSE") && curGoal.getECheck() > 0){
                electiveCheck(seats);
            }
        }            
        //check if hist or language huminity
        else if(courseId > 301 && courseId < 404){
            if( !(curGoal.name.StartsWith("HIST")) && !(AcedemicController.Instance.courses[courseId].courseName.StartsWith("HIST3"))){
                humanityCheck(seats);
                return;
            }
            else if((curGoal.name.StartsWith("HIST") && courseId > 385)){
                humanityCheck(seats);
                return;
            }
        }
        //checks if eng humanity
        else if(courseId > 267 && courseId < 276 && courseId != 270 && courseId != 272){
            if( !(curGoal.name.StartsWith("ENG"))){
                humanityCheck(seats);
                return;
            }
        }
        if(courseId == 9 || courseId == 10){
            if (curGoal.getComplexSeatGoals().Count != 0){
                complexCheckHelper(courseId, seats);
                return;
            }
        }
        foreach (int i in curGoal.getCourseList()){
            if (courseId == i){
                Debug.Log("Checking simple course");
                if(curGoal.getSeatGoals()[curGoal.getCourseList().IndexOf(i)] <= seats){
                    curGoal.getSeatGoals()[curGoal.getCourseList().IndexOf(i)] = 0;
                    goalCompleteCheck();
                }
                else{
                    curGoal.getSeatGoals()[curGoal.getCourseList().IndexOf(i)] -= seats;
                }
                return;
            }
        }
    }

    //check for an extra to a course(aka labs, recitations)
    public void optionalExtraCheck(int courseId, int seats){
        int mainCourseId = 0;
        switch(courseId){
            case 507:
                mainCourseId = 9;
                break;
            case 13:
                mainCourseId = 9;
                break;
            case 12:
                mainCourseId = 10;
                break;
            case 26:
                mainCourseId = 10;
                break;
            case 30:
                mainCourseId = 29;
                break;
            case 31:
                mainCourseId = 29;
                break;
        }
        if(AcedemicController.Instance.courses[mainCourseId].seatCount() < AcedemicController.Instance.courses[courseId].seatCount()){
            curGoal.removeExtra(courseId);
            goalCompleteCheck();
        }
    }

    //checks for a requirement with a range of options
    public void rangeCheck(int courseId, int seats){
        int count = 0;
        foreach(Tuple<int, int, int> t in curGoal.getRangeGoals()){
            if(t.Item1 < courseId && courseId < t.Item2){
                if(seats > t.Item3)
                    curGoal.setRangeGoal(count, t.Item1, t.Item2, 0);
                else
                    curGoal.setRangeGoal(count,  t.Item1, t.Item2, t.Item3 - seats);
            }
            count++;
        }
    }

    public void complexCheckHelper(int courseId, int seats){
        if(curGoal.getComplexFlag() == 1){
            complexCheck1(courseId, seats);
        }
        else if (curGoal.getComplexFlag() == 2){
            complexCheck2(courseId, seats);
        }
    }


    //helper for a 1st semester flag
    public void complexCheck1(int courseId, int seats){
        Debug.Log("Checking complex course with flag 1");
        foreach(List<int> l in curGoal.getComplexList()){
            foreach(int cId in l){
                //if the list contains the courseid and a 1st semester flag 
                if (courseId == cId && curGoal.getComplexFlag() == 1){
                    if(curGoal.getComplexSeatGoals()[0] <= seats){
                        complexCounts[(string) curGoal.name.Substring(0, 3) + AcedemicController.Instance.courses[courseId].courseName] += seats; 
                        curGoal.getComplexSeatGoals()[0] = 0;
                        goalCompleteCheck();
                    }
                    else{
                        curGoal.getComplexSeatGoals()[0] -= seats;
                        complexCounts[(string) curGoal.name.Substring(0, 3) + AcedemicController.Instance.courses[courseId].courseName] += seats;
                    }
                    return;
                    }
                }
            }
             //if the list contains the courseid and a 2nd semester flag 
    }

    //helper for a 2nd semester flag
    public void complexCheck2(int courseId, int seats){
        Debug.Log("Checking complex course with flag 2");
        int i = 0;
        //foreach(List<int> l in curGoal.getComplexList()){
            if (courseId == curGoal.getComplexList()[0][0]){
                string s1 = (string) curGoal.name.Substring(0, 3);
                string s2 = AcedemicController.Instance.courses[courseId].courseName;
                string s3 = AcedemicController.Instance.courses[courseId + 1].courseName;
                //check the number of seats of the course and next course to see if the goal
                //is met
                if(complexCounts[s1 + s2] + seats > 50 && complexCounts[s1 + s3] > 50){
                    Debug.Log("Enough Seats");
                    complexCounts[s1 + s2] += seats;
                    curGoal.removeCGoal(i);
                    goalCompleteCheck();
                }
                else{
                    Debug.Log("Not enough seat, incrementing");
                    complexCounts[s1 + s2] += seats;
                }
            }
            else if (courseId == curGoal.getComplexList()[0][1]){
                string s1 = (string) curGoal.name.Substring(0, 3);
                string s2 = AcedemicController.Instance.courses[courseId].courseName;
                string s3 = AcedemicController.Instance.courses[courseId - 1].courseName;
                if(complexCounts[s1 + s2] + seats > 50 && complexCounts[s1 + s3] > 50){
                    curGoal.removeCGoal(i);
                    complexCounts[s1 + s2] += seats;
                    Debug.Log("Enough Seats");
                    goalCompleteCheck();
                }
                else{
                    Debug.Log("Not enough seat, incrementing");
                    complexCounts[s1 + s2] += seats;
                    
                }
            }
        //}
    }

    //check and adjust the seat goal for humanities 
    public void humanityCheck(int added){
        if(curGoal.getHCheck() == 0){
            return;
        }
        else{
            if(curGoal.getHCheck() <= added){
                curGoal.removeHGoal();
                goalCompleteCheck();
            }
            else{
                curGoal.reduceHGoal(added);
            }
        }
    }

    //check and adjust the seat goal for electives
    public void electiveCheck(int seats){
        if(curGoal.getECheck() <= seats){
            curGoal.removeEGoal();
        }
        else{
            curGoal.reduceEGoal(seats);
        }
    }

    //checks if the goal is complete
    public void goalCompleteCheck(){
        int cCount = 0;
        int cInc = 0;
        if(curGoal.getComplexSeatGoals() != null){
            cCount = curGoal.getComplexSeatGoals().Count;
             foreach(int i in curGoal.getComplexSeatGoals()){
                if(i == 0)
                    cInc++;
            }
        }
        int sCount = curGoal.getSeatGoals().Count;
        int eCount = curGoal.getExtraGoals().Count;
        int sInc = 0;
        foreach(int j in curGoal.getSeatGoals()){
            if(j == 0)
                sInc++;
        }
        if(cInc == cCount && sInc == sCount && curGoal.getHCheck() == 0 && curGoal.getECheck() == 0 && eCount == 0){
            Debug.Log("cInc: " + cInc + " cCount: " + cCount + " sInc: " + sInc + " sCount: " + sCount);
            GameObject.Find("CurGoalText").GetComponent<Text>().text = "";
            completeGoals.Add(curGoalId);
            goalList = new List<Goal>();
            loadGoalList();
            updateGoalList();
            UIController.Instance.addMoney(goalReward);
            UIController.Instance.updateRepUI(repReward);
            curGoal = null;
            UIController.Instance.setInfoBar("Objective Complete");
        }
        else{
            Debug.Log("cInc: " + cInc + " cCount: " + cCount + " sInc: " + sInc + " sCount: " + sCount);
        }
    }

    //sets current goal after a goal is clicked in the dropdown and it is the 1st sem of a complex goal
    public void setCurGoal(string s){
        int i = 0;
        foreach(Goal g in goalList){
            if(g.name.Equals(s)){
                Debug.Log("setting " + g.name + " as goal");
                curGoal = g;
                curGoalId = goalIntList[i];
                GameObject.Find("CurGoalText").GetComponent<Text>().text = g.name;
                if(g.checkComplexFlag() == 1 && g.getComplexList().Count > 0){
                    foreach(int j in g.getComplexList()[0]){
                        complexCounts.Add(AcedemicController.Instance.courses[j].courseName, 0);
                    }
                }
                return;
            }
            else
                i++;
        }
    }

    public int getComplexCount(int i ){
        return complexCounts[AcedemicController.Instance.courses[i].courseName];
    }

    //untility to add a complex count
    public void addCount(string s){
        if (complexCounts.ContainsKey(s))
            complexCounts[s] =  0;
        else
            complexCounts.Add(s, 0);
    }

    //untility to add a complex count when loading
    public void addCount(string s, int i){
        if (complexCounts.ContainsKey(s))
            complexCounts[s] =  i;
        else
            complexCounts.Add(s, i);
    }

    public void writeXml(XmlWriter writer){
        if(curGoal == null){
            return;
        }
        foreach(KeyValuePair<string, int> entry in complexCounts){
            // do something with entry.Value or entry.Key
            writer.WriteStartElement("CC");
            writer.WriteAttributeString("str", entry.Key);
            writer.WriteAttributeString("count", entry.Value.ToString());
            writer.WriteEndElement();
        }
        foreach (int i in  curGoal.getSeatGoals()){
            writer.WriteStartElement("SimpleGoal");
            writer.WriteAttributeString("num", i.ToString());
            writer.WriteEndElement();
        }
        foreach (int i in  curGoal.getComplexSeatGoals()){
            writer.WriteStartElement("ComplexGoal");
            writer.WriteAttributeString("num", i.ToString());
            writer.WriteAttributeString("flag", curGoal.getComplexFlag().ToString());
            writer.WriteEndElement();
        }
        foreach (int i in curGoal.getExtraGoals()){
            writer.WriteStartElement("ExtraGoal");
            writer.WriteAttributeString("num", i.ToString());
            writer.WriteEndElement();
        }
        foreach (Tuple<int, int, int> t in curGoal.getRangeGoals()){
            writer.WriteStartElement("RangeGoal");
            writer.WriteAttributeString("id1", t.Item1.ToString());
            writer.WriteAttributeString("id2", t.Item2.ToString());
            writer.WriteAttributeString("num", t.Item3.ToString());
            writer.WriteEndElement();
        }
        writer.WriteStartElement("HGoal");
        writer.WriteAttributeString("num", curGoal.getHCheck().ToString());
        writer.WriteEndElement();
        writer.WriteStartElement("EGoal");
        writer.WriteAttributeString("num", curGoal.getECheck().ToString());
        writer.WriteEndElement();
    }

    public void readXml(XmlReader reader){
        if(int.Parse(reader.GetAttribute("id")) == -1){
            return;
        }
        Goal g = goalList[int.Parse(reader.GetAttribute("id"))];
        Debug.Log("setting " + g.name + " as goal");
        curGoal = g;
        GameObject.Find("CurGoalText").GetComponent<Text>().text = g.name;

        int simpleCount = 0;
        int complexCount = 0;
        int extraCount = 0;

        while(reader.Read()) {
            switch(reader.Name){
                case "SimpleGoal":
                    if(simpleCount <  curGoal.getSeatGoals().Count){
                        Debug.Log("Setting simple course");
                        curGoal.getSeatGoals()[simpleCount] = int.Parse(reader.GetAttribute("num"));
                        simpleCount++;
                    }
                    break;
                case "ComplexGoal":
                    if(complexCount <  curGoal.getComplexSeatGoals().Count){
                        Debug.Log("Setting complex course");
                        curGoal.getComplexSeatGoals()[complexCount] = int.Parse(reader.GetAttribute("num"));
                        complexCount++;
                    }
                    break;
                case "ExtraGoal":
                    if(extraCount <  curGoal.getExtraGoals().Count){
                        Debug.Log("Setting extra course");
                        curGoal.getExtraGoals()[extraCount] = int.Parse(reader.GetAttribute("num"));
                        extraCount++;
                    }
                    break;
                case "RangeGoal":
                    if(extraCount <  curGoal.getRangeGoals().Count){
                        Debug.Log("Setting range course");
                        curGoal.getRangeGoals().Add(new Tuple<int, int, int>(int.Parse(reader.GetAttribute("id1")), 
                                                                           int.Parse(reader.GetAttribute("id2")),
                                                                           int.Parse(reader.GetAttribute("num")))
                                                                        );
                    }
                    break;
                case "CC": 
                    complexCounts[reader.GetAttribute("str")] = int.Parse(reader.GetAttribute("count"));
                    break;
                case "HGoal":
                    curGoal.setHGoal(int.Parse(reader.GetAttribute("num")));
                    break;
                case "EGoal":
                    curGoal.setEGoal(int.Parse(reader.GetAttribute("num")));
                    break;
                default:
                    return;
            }
        }
    }

    public Goal getCSEMathRec(){
        return addMathRec("CSE");
    }

    public Goal getCSEsci(){
        Goal g = new Goal("CSE - Improved Science", "Adds recitation and labs to the science courses");
        g = addPhyRec(g);
        g = addChemRec(g);
        return g;
    }

    public Goal getCSE1(){
        Goal g = new Goal("CSE - 1st Semester", "The basic requirement for the first semester of 50 student's computer sciense curriculum");
        g.addCourseGoal(0, 50); //engr101
        g.addCourseGoal(2, 50); //eng101
        g.addCourseGoal(4, 50); //math110
        int[] s = new int[2] {9, 10};
        g.addComplexGoal(new List<int>(s), 50); //phy110 and chem
        addCount("CSEPHY110");
        addCount("CSECHM110");
        g.addCourseGoal(1, 50); //cse101

        return g;
    }

    public Goal getCSE1(string flag){
        Goal g = new Goal("CSE - 1st Semester", "The basic requirement for the first semester of 50 student's computer sciense curriculum");
        g.addCourseGoal(0, 50); //engr101
        g.addCourseGoal(2, 50); //eng101
        g.addCourseGoal(4, 50); //math110
        int[] s = new int[2] {9, 10};
        g.addComplexGoal(new List<int>(s), 50); //phy110 and chem 
        g.addCourseGoal(1, 50); //cse101

        return g;
    }

    public Goal getCSE2(){
        Goal g = new Goal("CSE - 2nd Semester", "The basic requirement for the second semester of 50 student's computer sciense curriculum");
        g.addCourseGoal(3, 50); //eng102
        g.addCourseGoal(5, 50); //math120
        int[] s = new int[2] {9, 10};
        g.addComplexGoal(new List<int>(s), 50, "Total Flag"); //phy110 and chem
        g.addCourseGoal(7, 50); //cse102
        g.addCourseGoal(17, 50);//econ101

        return g;
    }

    public Goal getCSE3(){
        Goal g = new Goal("CSE - 3rd Semester", "The basic requirement for the third semester of 50 student's computer sciense curriculum");
        g.addCourseGoal(6, 50); //math130
        g.addCourseGoal(132, 50); //cse103
        g.addCourseGoal(8, 50); //cse109
        g.addCourseGoal("HFlag", 50);
        return g;
    }

    public Goal getCSE4(){
        Goal g = new Goal("CSE - 4th Semester", "The basic requirement for the fourth semester of 50 student's computer sciense curriculum");
        g.addCourseGoal(32, 50); //math208
        g.addCourseGoal(133, 50); //cse
        g.addCourseGoal(134, 50); 
        g.addCourseGoal(135, 50);
        return g;
    }

    public Goal getCSE5(){
        Goal g = new Goal("CSE - 5th Semester", "The basic requirement for the fifth semester of 50 student's computer sciense curriculum");
        g.addCourseGoal(28, 50); //math230
        g.addCourseGoal(136, 50); //cse
        g.addCourseGoal(137, 50); //cse
        g.addRangeGoal(144, 160, 50);
        return g;
    }

    public Goal getCSE6(){
        Goal g = new Goal("CSE - 6th Semester", "The basic requirement for the sixth semester of 50 student's computer sciense curriculum");
        g.addCourseGoal(138, 50); //cse
        g.addCourseGoal(139, 50); //cse
        g.addCourseGoal(140, 50); //cse
        g.addRangeGoal(144, 160, 50);
        return g;
    }

    public Goal getCSE7(){
        Goal g = new Goal("CSE - 7th Semester", "The basic requirement for the seventh semester of 50 student's computer sciense curriculum");
        g.addCourseGoal(141, 50); //cse
        g.addCourseGoal(142, 50); //cse
        g.addCourseGoal("EFlag", 50);
        g.addRangeGoal(144, 160, 50);
        g.addCourseGoal("HFlag", 50);
        return g;
    }

    public Goal getCSE8(){
        Goal g = new Goal("CSE - 8th Semester", "The basic requirement for the eighth semester of 50 student's computer sciense curriculum");
        g.addCourseGoal(143, 50); //cse
        g.addRangeGoal(144, 160, 50);
        g.addRangeGoal(144, 160, 50);  
        g.addCourseGoal("HFlag", 50);
        return g;
    }

    public Goal getCSEG1(){
        Goal g = new Goal("CSE - 1st Grad Semester", "The basic requirement for the eighth semester of 50 student's computer sciense curriculum");
        g.addRangeGoal(577, 597, 10);
        g.addRangeGoal(577, 597, 10);
        g.addRangeGoal(577, 597, 10);
        return g;
    }

    public Goal getCSEG2(){
        Goal g = new Goal("CSE - 2nd Grad Semester", "The basic requirement for the eighth semester of 50 student's computer sciense curriculum");
        g.addRangeGoal(577, 597, 10);
        g.addRangeGoal(577, 597, 10);
        g.addRangeGoal(577, 597, 10); 
        return g;
    }

    public Goal getCSEG3(){
        Goal g = new Goal("CSE - 3rd Grad Semester", "The basic requirement for the eighth semester of 50 student's computer sciense curriculum");
        g.addRangeGoal(577, 597, 10);
        g.addRangeGoal(577, 597, 10);
        g.addRangeGoal(577, 597, 10);
        return g;
    }

    public Goal getCSEG4(){
        Goal g = new Goal("CSE - 4th Grad Semester", "The basic requirement for the eighth semester of 50 student's computer sciense curriculum");
        g.addRangeGoal(577, 597, 10);
        g.addRangeGoal(577, 597, 10);
        g.addRangeGoal(577, 597, 10);
        return g;
    }

    public Goal getMEC1(){
        Goal g = new Goal("MEC - 1st Semester", "The basic requirement for the first semester of 50 student's computer sciense curriculum");
        g.addCourseGoal(0, 50); //engr101
        g.addCourseGoal(2, 50); //eng101
        g.addCourseGoal(4, 50); //math110
        int[] s = new int[2] {9, 10};
        g.addComplexGoal(new List<int>(s), 50); //phy110 and chem
        addCount("MECPHY110");
        addCount("MECCHM110");
        g.addCourseGoal(20, 50); //mec101

        return g;
    }

    public Goal getMEC1(string flag){
        Goal g = new Goal("MEC - 1st Semester", "The basic requirement for the first semester of 50 student's computer sciense curriculum");
        g.addCourseGoal(0, 50); //engr101
        g.addCourseGoal(2, 50); //eng101
        g.addCourseGoal(4, 50); //math110
        int[] s = new int[2] {9, 10};
        g.addComplexGoal(new List<int>(s), 50); //phy110 and chem 
        g.addCourseGoal(20, 50); //mec101

        return g;
    }
    

    public Goal getMEC2(){
        Goal g = new Goal("MEC - 2nd Semester", "The basic requirement for the second semester of 50 student's computer sciense curriculum");
        g.addCourseGoal(3, 50); //eng102
        g.addCourseGoal(5, 50); //math120
        int[] s = new int[2] {9, 10};
        g.addComplexGoal(new List<int>(s), 50, "Total Flag"); //phy110 and chem
        g.addCourseGoal(21, 50); //mee102
        g.addCourseGoal(17, 50);//econ101

        return g;
    }

    public Goal getMEC3(){
        Goal g = new Goal("MEC - 3rd Semester", "The basic requirement for the third semester of 50 student's computer sciense curriculum");
        g.addCourseGoal(6, 50); //math130
        g.addCourseGoal(22, 50); //mec103
        g.addCourseGoal(29, 50); //phy120
        g.addCourseGoal(161, 50); //mec170
        g.addCourseGoal(198, 50); //mat101

        return g;
    }

    public Goal getMEC4(){
        Goal g = new Goal("MEC - 4th Semester", "The basic requirement for the fourth semester of 50 student's computer sciense curriculum");
        g.addCourseGoal(32, 50); //math208
        g.addCourseGoal(200, 50); //mec144
        g.addCourseGoal(29, 50); //mec112
        g.addCourseGoal(161, 50); //mat102 11

        return g;
    }

    public Goal getMEC5(){
        Goal g = new Goal("MEC - 5th Semester", "The basic requirement for the fifth semester of 50 student's computer sciense curriculum");
        g.addCourseGoal(27, 50); //engr120
        g.addCourseGoal(202, 50); //mec230
        g.addCourseGoal(201, 50); //mec121
        g.addCourseGoal(204, 50); //mec215
        g.addCourseGoal(203, 50); //mec192

        return g;
    }

    public Goal getMEC6(){
        Goal g = new Goal("MEC - 6th Semester", "The basic requirement for the sixth semester of 50 student's computer sciense curriculum");
        g.addCourseGoal("HFlag", 50);
        g.addCourseGoal(201, 50); //mec113
        g.addCourseGoal(206, 50); //mec240
        g.addCourseGoal(207, 50); //mec252
        g.addCourseGoal(208, 50); //mec242

        return g;
    }

    public Goal getMEC7(){
        Goal g = new Goal("MEC - 7th Semester", "The basic requirement for the seventh semester of 50 student's computer sciense curriculum");
        g.addCourseGoal("HFlag", 50);
        g.addRangeGoal(212, 233, 50);
        g.addCourseGoal(209, 50); //mec311
        g.addCourseGoal(452, 50); //mec245

        return g;
    }

    public Goal getMEC8(){
        Goal g = new Goal("MEC - 8th Semester", "The basic requirement for the eighth semester of 50 student's computer sciense curriculum");
        g.addRangeGoal(212, 233, 50);
        g.addRangeGoal(212, 233, 50);
        g.addCourseGoal("EFlag", 50);
        g.addCourseGoal(210, 50); //mec312
        g.addCourseGoal(211, 50); //mec313

        return g;
    }

    public Goal getMECLab(){
        Goal g = new Goal("MEC - Lab", "Adds addition labs to the mechanical engineeriing curriculum");
        g.addCourseGoal(453, 50);
        return g;
    }

    public Goal getMECMathRec() {
        return addMathRec("MEC");
        }

    public Goal getMECsci() {
        Goal g = new Goal("MEC - Improved Science", "Adds recitation and labs to the science courses");
        g = addPhyRec(g);
        g = addChemRec(g);
        return g;
        }

    public Goal getMECG1(){
        Goal g = new Goal("MEC - 1st Grad Semester", "The basic requirement for the first semester of 50 student's computer sciense curriculum");
        g.addRangeGoal(620, 641, 10);
        g.addRangeGoal(620, 641, 10);
        g.addRangeGoal(620, 641, 10);
        return g;
    }

    public Goal getMECG2(){
        Goal g = new Goal("MEC - 2nd Grad Semester", "The basic requirement for the first semester of 50 student's computer sciense curriculum");
        g.addRangeGoal(620, 641, 10);
        g.addRangeGoal(620, 641, 10);
        g.addRangeGoal(620, 641, 10);
        return g;
    }

    public Goal getMECG3(){
        Goal g = new Goal("MEC - 3rd Grad Semester", "The basic requirement for the first semester of 50 student's computer sciense curriculum");
        g.addRangeGoal(620, 641, 10);
        g.addRangeGoal(620, 641, 10);
        g.addRangeGoal(620, 641, 10);
        return g;
    }

    public Goal getMECG4(){
        Goal g = new Goal("MEC - 4th Grad Semester", "The basic requirement for the first semester of 50 student's computer sciense curriculum");
        g.addRangeGoal(620, 641, 10);
        g.addRangeGoal(620, 641, 10);
        g.addRangeGoal(620, 641, 10);
        return g;
    }

    public Goal getCHE1(){
        Goal g = new Goal("CHE - 1st Semester", "The basic requirement for the first semester of 50 student's chemical engineering curriculum");
        g.addCourseGoal(23, 50); //che 101
        g.addCourseGoal(0, 50); //engr101
        g.addCourseGoal(2, 50); //eng101
        g.addCourseGoal(4, 50); //math110
        int[] s = new int[2] {9, 10};
        addCount("CHEPHY110");
        addCount("CHECHM110");
        g.addComplexGoal(new List<int>(s), 50); //phy110 and chem
        return g;
    }

    public Goal getCHE1(string flags){
        Goal g = new Goal("CHE - 1st Semester", "The basic requirement for the first semester of 50 student's chemical engineering curriculum");
        g.addCourseGoal(23, 50); //che 101
        g.addCourseGoal(0, 50); //engr101
        g.addCourseGoal(2, 50); //eng101
        g.addCourseGoal(4, 50); //math110
        int[] s = new int[2] {9, 10};
        g.addComplexGoal(new List<int>(s), 50); //phy110 and chem
        return g;
    }

    public Goal getCHE2(){
        Goal g = new Goal("CHE - 2nd Semester", "The basic requirement for the second semester of 50 student's computer sciense curriculum");
        g.addCourseGoal(3, 50); //eng102
        g.addCourseGoal(5, 50); //math120
        int[] s = new int[2] {9, 10};
        g.addComplexGoal(new List<int>(s), 50, "Total Flag"); //phy110 and chem
        g.addCourseGoal(17, 50);//econ101
        g.addCourseGoal(12, 50);//chem111
        g.addCourseGoal(26, 50);//chem112
        return g;
    }

    public Goal getCHE3(){
        Goal g = new Goal("CHE - 3rd Semester", "The basic requirement for the third semester of 50 student's computer sciense curriculum");
        g.addCourseGoal(24, 50);//chm120
        g.addCourseGoal(25, 50);//chem121
        g.addCourseGoal(6, 50);//math130
        g.addCourseGoal(29, 50);//phy120
        g.addCourseGoal(36, 50);//che131
        return g;
    }

    public Goal getCHE4(){
        Goal g = new Goal("CHE - 4th Semester", "The basic requirement for the fourth semester of 50 student's computer sciense curriculum");
        g.addCourseGoal(42, 50);//che210
        g.addCourseGoal(39, 50);//che140
        g.addCourseGoal(32, 50);//math208
        g.addCourseGoal(398, 50);//chm180
        g.addCourseGoal(399, 50);//chm181
        return g;
    }

    public Goal getCHE5(){
        Goal g = new Goal("CHE - 5th Semester", "The basic requirement for the fifth semester of 50 student's computer sciense curriculum");
        g.addCourseGoal(33, 50);//che150
        g.addCourseGoal(37, 50);//che205
        g.addCourseGoal(28, 50);//math230
        g.addCourseGoal(400, 50);//chm190
        g.addCourseGoal(401, 50);//chm191
        return g;
    }

    public Goal getCHE6(){
        Goal g = new Goal("CHE - 6th Semester", "The basic requirement for the sixth semester of 50 student's computer sciense curriculum");
        g.addCourseGoal(34, 50);//che210
        g.addCourseGoal(39, 50);//che244
        g.addCourseGoal(437, 50);//chm340
        g.addRangeGoal(46, 66, 50);
        g.addCourseGoal("HFlag", 50);
        return g;
    }

    public Goal getCHE7(){
        Goal g = new Goal("CHE - 7th Semester", "The basic requirement for the seventh semester of 50 student's computer sciense curriculum");
        g.addCourseGoal(35, 50);//che342
        g.addCourseGoal(40, 50);//che202
        g.addCourseGoal(41, 50);//che333
        g.addRangeGoal(46, 66, 50);
        g.addCourseGoal("HFlag", 50);
        return g;
    }

    public Goal getCHE8(){
        Goal g = new Goal("CHE - 8th Semester", "The basic requirement for the eighth semester of 50 student's computer sciense curriculum");
        g.addCourseGoal(43, 50);//che203
        g.addCourseGoal(44, 50);//che334
        g.addRangeGoal(46, 66, 50);
        g.addCourseGoal("HFlag", 50);
        return g;
    }

    public Goal getCHELab(){
        Goal g = new Goal("CHE - Lab", "Adds addition labs to the chemical engineeriing curriculum");
        g.addCourseGoal(430, 50);
        return g;
    }

    public Goal getCHEMathRec() {
        return addMathRec("CHE");
    }

    public Goal getCHEsci() {
        Goal g = new Goal("CHE - Improved Science", "Adds recitation and labs to the science courses");
        g = addPhyRec(g);
        return g;
    }

    public Goal getCHEG1(){
        Goal g = new Goal("CHE - 1st Grad Semester", "The basic requirement for the first semester of 50 student's chemical engineering curriculum");
        g.addCourseGoal(616, 10); 
        g.addCourseGoal(617, 10);
        g.addCourseGoal(618, 50);
        return g;
    }

    public Goal getCHEG2(){
        Goal g = new Goal("CHE - 1st Grad Semester", "The basic requirement for the first semester of 50 student's chemical engineering curriculum");
        g.addRangeGoal(598, 615, 10);
        g.addRangeGoal(598, 615, 10);
        g.addRangeGoal(598, 615, 10);
        return g;
    }

    public Goal getCHEG3(){
        Goal g = new Goal("CHE - 1st Grad Semester", "The basic requirement for the first semester of 50 student's chemical engineering curriculum");
        g.addCourseGoal(619, 10);
        g.addRangeGoal(598, 615, 10);
        g.addRangeGoal(598, 615, 10);
        return g;
    }

    public Goal getCHEG4(){
        Goal g = new Goal("CHE - 1st Grad Semester", "The basic requirement for the first semester of 50 student's chemical engineering curriculum");
        g.addRangeGoal(598, 615, 10);
        g.addRangeGoal(598, 615, 10);
        g.addRangeGoal(598, 615, 10);
        return g;
    }

    public Goal getELE1() {
        Goal g = new Goal("ELE - 1st Semester", "The basic requirement for the first semester of 50 student's electrical engineering curriculum");
        g.addCourseGoal(234, 50); //ele 101
        g.addCourseGoal(0, 50); //engr101
        g.addCourseGoal(2, 50); //eng101
        g.addCourseGoal(4, 50); //math110
        int[] s = new int[2] { 9, 10 };
        addCount("ELEPHY110");
        addCount("ELECHM110");
        g.addComplexGoal(new List<int>(s), 50); //phy110 and chem
        return g;
    }

    public Goal getELE1(string flag) {
        Goal g = new Goal("ELE - 1st Semester", "The basic requirement for the first semester of 50 student's electrical engineering curriculum");
        g.addCourseGoal(234, 50); //ele 101
        g.addCourseGoal(0, 50); //engr101
        g.addCourseGoal(2, 50); //eng101
        g.addCourseGoal(4, 50); //math110
        int[] s = new int[2] { 9, 10 };
        g.addComplexGoal(new List<int>(s), 50); //phy110 and chem
        return g;
    }

    public Goal getELE2() {
        Goal g = new Goal("ELE - 2nd Semester", "The basic requirement for the second semester of 50 student's electrical engineering curriculum");
        g.addCourseGoal(3, 50); //eng102
        g.addCourseGoal(5, 50); //math120
        int[] s = new int[2] { 9, 10 };
        g.addComplexGoal(new List<int>(s), 50, "Total Flag"); //phy110 and chem
        g.addCourseGoal(17, 50);//econ101
        g.addCourseGoal(235, 50);//ele102
        return g;
    }

    public Goal getELE3() {
        Goal g = new Goal("ELE - 3rd Semester", "The basic requirement for the third semester of 50 student's electrical engineering  curriculum");
        g.addCourseGoal(236, 50);//elec103
        g.addCourseGoal(237, 50);//elec104
        g.addCourseGoal(6, 50);//math130
        g.addCourseGoal(29, 50);//phy120
        g.addCourseGoal(1, 50); //cse101
        return g;
        }

    public Goal getELE4() {
        Goal g = new Goal("ELE - 4th Semester", "The basic requirement for the fourth semester of 50 student's electrical engineering  curriculum");
        g.addCourseGoal(238, 50);//ele123
        g.addCourseGoal(239, 50);//ele121
        g.addCourseGoal(3, 50);//math208
        g.addCourseGoal(240, 50);//ele196
        g.addCourseGoal(7, 50);//cse102
        return g;
        }

    public Goal getELE5() {
        Goal g = new Goal("ELE - 5th Semester", "The basic requirement for the fifth semester of 50 student's electrical engineering  curriculum");
        g.addCourseGoal(241, 50);//ELE208
        g.addCourseGoal(242, 50);
        g.addCourseGoal(243, 50);
        g.addCourseGoal(28, 50);//math230
        return g;
    }

    public Goal getELE6() {
        Goal g = new Goal("ELE - 6th Semester", "The basic requirement for the sixth semester of 50 student's electrical engineering  curriculum");
        g.addCourseGoal(244, 50);//ELE225
        g.addCourseGoal(245, 50);
        g.addCourseGoal(246, 50);
        g.addCourseGoal("HFlag", 50);
        return g;
    }

    public Goal getELE7() {
        Goal g = new Goal("ELE - 7th Semester", "The basic requirement for the seventh semester of 50 student's electrical engineering  curriculum");
        g.addCourseGoal(248, 50);//ELE225
        g.addCourseGoal(249, 50);
        g.addCourseGoal("HFlag", 50);
        g.addRangeGoal(251, 267, 50);
        return g;
    }

    public Goal getELE8() {
        Goal g = new Goal("ELE - 8th Semester", "The basic requirement for the eighth semester of 50 student's electrical engineering  curriculum");
        g.addCourseGoal(249, 50);
        g.addCourseGoal("HFlag", 50);
        g.addRangeGoal(251, 267, 50);
        g.addRangeGoal(251, 267, 50);
        return g;
    }

    public Goal getELEMathRec() {
        return addMathRec("ELE");
    }

    public Goal getELEsci() {
        Goal g = new Goal("ELE - Improved Science", "Adds recitation and labs to the science courses");
        g = addPhyRec(g);
        g = addChemRec(g);
        return g;
    }

    public Goal getELEG1(){
        Goal g = new Goal("ELE - 1st Grad Semester", "The basic requirement for the first semester of 50 student's computer sciense curriculum");
        g.addRangeGoal(642, 666, 10);
        g.addRangeGoal(642, 666, 10);
        g.addRangeGoal(642, 666, 10);
        return g;
    }

    public Goal getELEG2(){
        Goal g = new Goal("ELE - 2nd Grad Semester", "The basic requirement for the first semester of 50 student's computer sciense curriculum");
        g.addRangeGoal(642, 666, 10);
        g.addRangeGoal(642, 666, 10);
        g.addRangeGoal(642, 666, 10);
        return g;
    }

    public Goal getELEG3(){
        Goal g = new Goal("ELE - 3rd Grad Semester", "The basic requirement for the first semester of 50 student's computer sciense curriculum");
        g.addRangeGoal(642, 666, 10);
        g.addRangeGoal(642, 666, 10);
        g.addRangeGoal(642, 666, 10);
        return g;
    }

    public Goal getELEG4(){
        Goal g = new Goal("ELE - 4th Grad Semester", "The basic requirement for the first semester of 50 student's computer sciense curriculum");
        g.addRangeGoal(620, 641, 10);
        g.addRangeGoal(620, 641, 10);
        g.addRangeGoal(620, 641, 10);
        return g;
    }

    public Goal getCIV1() {
        Goal g = new Goal("CIV - 1st Semester", "The basic requirement for the first semester of 50 student's civil engeneering curriculum");
        g.addCourseGoal(0, 50); //engr101
        g.addCourseGoal(2, 50); //eng101
        g.addCourseGoal(4, 50); //math110
        int[] s = new int[2] { 9, 10 };
        addCount("CIVPHY110");
        addCount("CIVCHM110");
        g.addComplexGoal(new List<int>(s), 50); //phy110 and chem
        return g;
    }

    public Goal getCIV1(string flag) {
        Goal g = new Goal("CIV - 1st Semester", "The basic requirement for the first semester of 50 student's civil engeneering curriculum");
        g.addCourseGoal(0, 50); //engr101
        g.addCourseGoal(2, 50); //eng101
        g.addCourseGoal(4, 50); //math110
        int[] s = new int[2] { 9, 10 };
        g.addComplexGoal(new List<int>(s), 50); //phy110 and chem
        return g;
    }

    public Goal getCIV2() {
        Goal g = new Goal("CIV - 2nd Semester", "The basic requirement for the second semester of 50 student's civil engeneering curriculum");
        g.addCourseGoal(3, 50); //eng102
        g.addCourseGoal(5, 50); //math120
        int[] s = new int[2] { 9, 10 };
        g.addComplexGoal(new List<int>(s), 50, "Total Flag"); //phy110 and chem
        g.addCourseGoal(17, 50);//econ101
        g.addCourseGoal(68 , 50);//civ120
        return g;
    }

    public Goal getCIV3() {
        Goal g = new Goal("CIV - 3rd Semester", "The basic requirement for the third semester of 50 student's civil engineering curriculum");
        g.addCourseGoal(6, 50);//math130
        g.addCourseGoal(29, 50);//phy120
        g.addCourseGoal(69, 50);//cvi 3rd
        g.addCourseGoal(67, 50);
        g.addCourseGoal(27, 50);
        return g;
    }

    public Goal getCIV4() {
        Goal g = new Goal("CIV - 4th Semester", "The basic requirement for the fourth semester of 50 student's civil engineering curriculum");
        g.addCourseGoal(32, 50);//math208
        g.addCourseGoal(70, 50);//civ 4th
        g.addCourseGoal(71, 50);
        g.addCourseGoal(72, 50);
        return g;
    }

    public Goal getCIV5() {
        Goal g = new Goal("CIV - 5th Semester", "The basic requirement for the fifth semester of 50 student's civil engineering curriculum");
        g.addCourseGoal(73, 50);//civ 5th
        g.addCourseGoal(74, 50);
        g.addCourseGoal(75, 50);
        g.addCourseGoal(76, 50);
        g.addCourseGoal(28, 50);//math230
        return g;
    }

    public Goal getCIV6() {
        Goal g = new Goal("CIV - 6th Semester", "The basic requirement for the sixth semester of 50 student's civil engineering curriculum");
        g.addCourseGoal(77, 50);//civ 6th
        g.addCourseGoal(78, 50);
        g.addCourseGoal(79, 50);
        g.addCourseGoal(80, 50);  
        g.addCourseGoal("HFlag", 50);
        return g;
    }

    public Goal getCIV7() {
        Goal g = new Goal("CIV - 7th Semester", "The basic requirement for the seventh semester of 50 student's civil engineering curriculum");
        g.addCourseGoal(81, 50);//civ 7th
        g.addCourseGoal(82, 50);//civ 7th
        g.addCourseGoal("HFlag", 50);
        g.addRangeGoal(84, 103, 50);
        return g;
    }

    public Goal getCIV8() {
        Goal g = new Goal("CIV - 8th Semester", "The basic requirement for the eighth semester of 50 student's civil engineering curriculum");
        g.addCourseGoal(83, 50);//civ 56h
        g.addCourseGoal("HFlag", 100);
        g.addRangeGoal(84, 103, 50);
        g.addRangeGoal(84, 103, 50);
        return g;
    }

    public Goal getCIVMathRec() {
        return addMathRec("CIV");
    }

    public Goal getCIVsci() {
        Goal g = new Goal("CIV - Improved Science", "Adds recitation and labs to the civil engineering courses");
        g = addPhyRec(g);
        g = addChemRec(g);
        return g;
    }

    public Goal getCIVG1(){
        Goal g = new Goal("CIV - 1st Grad Semester", "The basic requirement for the first semester of 50 student's computer sciense curriculum");
        g.addRangeGoal(667, 707, 10);
        g.addRangeGoal(667, 707, 10);
        g.addRangeGoal(667, 707, 10);
        return g;
    }

    public Goal getCIVG2(){
        Goal g = new Goal("CIV - 2nd Grad Semester", "The basic requirement for the first semester of 50 student's computer sciense curriculum");
        g.addRangeGoal(667, 707, 10);
        g.addRangeGoal(667, 707, 10);
        g.addRangeGoal(667, 707, 10);
        return g;
    }

    public Goal getCIVG3(){
        Goal g = new Goal("CIV - 3rd Grad Semester", "The basic requirement for the first semester of 50 student's computer sciense curriculum");
        g.addRangeGoal(667, 707, 10);
        g.addRangeGoal(667, 707, 10);
        g.addRangeGoal(667, 707, 10);
        return g;
    }

    public Goal getCIVG4(){
        Goal g = new Goal("CIV - 4th Grad Semester", "The basic requirement for the first semester of 50 student's computer sciense curriculum");
        g.addRangeGoal(667, 707, 10);
        g.addRangeGoal(667, 707, 10);
        g.addRangeGoal(667, 707, 10);
        return g;
    }

    public Goal getISE1() {
        Goal g = new Goal("ISE - 1st Semester", "The basic requirement for the first semester of 50 student's industial engineering curriculum");
        g.addCourseGoal(0, 50); //engr101
        g.addCourseGoal(2, 50); //eng101
        g.addCourseGoal(4, 50); //math110
        int[] s = new int[2] { 9, 10 };
        addCount("ISEPHY110");
        addCount("ISECHM110");
        g.addComplexGoal(new List<int>(s), 50); //phy110 and chem
        g.addCourseGoal(104, 50);
        g.addCourseGoal(1, 50); //cse101
        return g;
    }

    public Goal getISE1(string flag) {
        Goal g = new Goal("ISE - 1st Semester", "The basic requirement for the first semester of 50 student's industial engineering curriculum");
        g.addCourseGoal(0, 50); //engr101
        g.addCourseGoal(2, 50); //eng101
        g.addCourseGoal(4, 50); //math110
        int[] s = new int[2] { 9, 10 };
        g.addComplexGoal(new List<int>(s), 50); //phy110 and chem
        g.addCourseGoal(104, 50);//ise111
        g.addCourseGoal(1, 50); //cse101
        return g;
    }

    public Goal getISE2() {
        Goal g = new Goal("ISE - 2nd Semester", "The basic requirement for the second semester of 50 student's industial engineering curriculum");
        g.addCourseGoal(3, 50); //eng102
        g.addCourseGoal(5, 50); //math120
        int[] s = new int[2] { 9, 10 };
        g.addComplexGoal(new List<int>(s), 50, "Total Flag"); //phy110 and chem
        g.addCourseGoal(17, 50);//econ101
        g.addCourseGoal(105, 50);//ise130
        g.addCourseGoal(8, 50); //cse102
        return g;
    }

    public Goal getISE3() {
        Goal g = new Goal("ISE - 3rd Semester", "The basic requirement for the third semester of 50 student's industial engineering curriculum");
        g.addCourseGoal(6, 50);//math130
        g.addCourseGoal(29, 50);//phy120
        g.addCourseGoal(104, 50);//ise111
        g.addCourseGoal(106, 50);//ise131
        g.addCourseGoal(131, 50); //cse103
        return g;
    }

    public Goal getISE4() {
        Goal g = new Goal("ISE - 4th Semester", "The basic requirement for the fourth semester of 50 student's industial engineering curriculum");
        g.addCourseGoal(32, 50);//math208
        g.addCourseGoal(161, 50);//mat101
        g.addCourseGoal(107, 50);//ise160
        g.addCourseGoal(108, 50);//ise161
        return g;
    }

    public Goal getISE5() {
        Goal g = new Goal("ISE - 5th Semester", "The basic requirement for the fifth semester of 50 student's industial engineering curriculum");
        g.addCourseGoal(28, 50);//math230
        g.addCourseGoal(109, 50);//ise216
        g.addCourseGoal(110, 50);
        g.addCourseGoal(111, 50);
        g.addCourseGoal(112, 50);
        return g;
    }

    public Goal getISE6() {
        Goal g = new Goal("ISE - 6th Semester", "The basic requirement for the sixth semester of 50 student's industial engineering curriculum");
        g.addCourseGoal(113, 50);//ise240
        g.addCourseGoal(114, 50);
        g.addCourseGoal(115, 50);
        g.addCourseGoal("HFlag", 50);
        return g;
    }

    public Goal getISE7() {
        Goal g = new Goal("ISE - 7th Semester", "The basic requirement for the seventh semester of 50 student's industial engineering curriculum");
        g.addCourseGoal(117, 50);//ise240
        g.addCourseGoal(116, 50);
        g.addCourseGoal("HFlag", 50);
        g.addRangeGoal(115, 130, 50);
        return g;
    }

    public Goal getISE8() {
        Goal g = new Goal("ISE - 8th Semester", "The basic requirement for the eighth semester of 50 student's industial engineering curriculum");
        g.addCourseGoal(118, 50);//ise240
        g.addCourseGoal("HFlag", 100);
        g.addRangeGoal(115, 130, 50);
        return g;
    }

    public Goal getISEMathRec() {
        return addMathRec("ISE");
    }

    public Goal getISEsci() {
        Goal g = new Goal("ISE - Improved Science", "Adds recitation and labs to the science courses");
        g = addPhyRec(g);
        g = addChemRec(g);
        return g;
    }

    public Goal getISEG1(){
        Goal g = new Goal("ISE - 1st Grad Semester", "The basic requirement for the first semester of 50 student's computer sciense curriculum");
        g.addRangeGoal(708, 742, 10);
        g.addRangeGoal(708, 742, 10);
        g.addRangeGoal(708, 742, 10);
        return g;
    }

    public Goal getISEG2(){
        Goal g = new Goal("ISE - 2nd Grad Semester", "The basic requirement for the first semester of 50 student's computer sciense curriculum");
        g.addRangeGoal(708, 742, 10);
        g.addRangeGoal(708, 742, 10);
        g.addRangeGoal(708, 742, 10);
        return g;
    }

    public Goal getISEG3(){
        Goal g = new Goal("ISE - 3rd Grad Semester", "The basic requirement for the first semester of 50 student's computer sciense curriculum");
        g.addRangeGoal(708, 742, 10);
        g.addRangeGoal(708, 742, 10);
        g.addRangeGoal(708, 742, 10);
        return g;
    }

    public Goal getISEG4(){
        Goal g = new Goal("ISE - 4th Grad Semester", "The basic requirement for the first semester of 50 student's computer sciense curriculum");
        g.addRangeGoal(708, 742, 10);
        g.addRangeGoal(708, 742, 10);
        g.addRangeGoal(708, 742, 10);
        return g;
    }

    public Goal getMAT1() {
        Goal g = new Goal("MAT - 1st Semester", "The basic requirement for the first semester of 50 student's material science curriculum");
        g.addCourseGoal(0, 50); //engr101
        g.addCourseGoal(2, 50); //eng101
        g.addCourseGoal(4, 50); //math110
        int[] s = new int[2] { 9, 10 };
        addCount("MATPHY110");
        addCount("MATCHM110");
        g.addComplexGoal(new List<int>(s), 50); //phy110 and chem
        return g;
    }

    public Goal getMAT1(string flag) {
        Goal g = new Goal("MAT - 1st Semester", "The basic requirement for the first semester of 50 student's material science curriculum");
        g.addCourseGoal(0, 50); //engr101
        g.addCourseGoal(2, 50); //eng101
        g.addCourseGoal(4, 50); //math110
        int[] s = new int[2] { 9, 10 };
        g.addComplexGoal(new List<int>(s), 50); //phy110 and chem
        return g;
    }

    public Goal getMAT2() {
        Goal g = new Goal("MAT - 2nd Semester", "The basic requirement for the second semester of 50 student's material science curriculum");
        g.addCourseGoal(3, 50); //eng102
        g.addCourseGoal(5, 50); //math120
        int[] s = new int[2] { 9, 10 };
        g.addComplexGoal(new List<int>(s), 50, "Total Flag"); //phy110 and chem
        g.addCourseGoal(17, 50);//econ101
        return g;
    }

    public Goal getMAT3() {
        Goal g = new Goal("MAT - 3rd Semester", "The basic requirement for the third semester of 50 student's material science curriculum");
        g.addCourseGoal(6, 50);//math130
        g.addCourseGoal(29, 50);//phy120
        g.addCourseGoal(30, 50);//phy121
        g.addCourseGoal(161, 50);//mat101
        g.addCourseGoal(162, 50);//mat102
        return g;
    }

    public Goal getMAT4() {
        Goal g = new Goal("MAT - 4th Semester", "The basic requirement for the fourth semester of 50 student's material science curriculum");
        g.addCourseGoal(32, 50);//math208
        g.addCourseGoal(163, 50);//mat
        g.addCourseGoal(164, 50);
        g.addCourseGoal(165, 50);
        g.addCourseGoal(166, 50);
        return g;
    }

    public Goal getMAT5() {
        Goal g = new Goal("MAT - 5th Semester", "The basic requirement for the fifth semester of 50 student's material science curriculum");
        g.addCourseGoal(28, 50);//math230
        g.addCourseGoal(167, 50);//mat
        g.addCourseGoal(168, 50);
        g.addCourseGoal(169, 50);
        g.addCourseGoal("HFlag", 50);
        return g;
    }

    public Goal getMAT6() {
        Goal g = new Goal("MAT - 6th Semester", "The basic requirement for the fifth semester of 50 student's material science curriculum");
        g.addCourseGoal(170, 50);//mat
        g.addCourseGoal(171, 50);
        g.addCourseGoal(172, 50);
        g.addCourseGoal(173, 50);
        g.addCourseGoal("HFlag", 50);
        return g;
    }

    public Goal getMAT7() {
        Goal g = new Goal("MAT - 7th Semester", "The basic requirement for the fifth semester of 50 student's material science curriculum");
        g.addCourseGoal(174, 50);//mat
        g.addCourseGoal(175, 50);
        g.addCourseGoal(176, 50);
        g.addRangeGoal(179, 197, 50);
        g.addCourseGoal("HFlag", 50);
        return g;
    }

    public Goal getMAT8() {
        Goal g = new Goal("MAT - 8th Semester", "The basic requirement for the fifth semester of 50 student's material science curriculum");
        g.addCourseGoal(177, 50);//mat
        g.addCourseGoal("HFlag", 50);
        g.addRangeGoal(179, 197, 50);
        g.addRangeGoal(179, 197, 50);
        return g;
    }

    public Goal getMATMathRec() {
        return addMathRec("MAT");
    }

    public Goal getMATsci() {
        Goal g = new Goal("MAT - Improved Science", "Adds recitation and labs to the science courses");
        g = addPhyRec(g);
        g = addChemRec(g);
        return g;
    }

    public Goal getMATG1(){
        Goal g = new Goal("MAT - 1st Grad Semester", "The basic requirement for the first semester of 50 student's computer sciense curriculum");
        g.addRangeGoal(743, 768, 10);
        g.addRangeGoal(743, 768, 10);
        g.addRangeGoal(743, 768, 10);
        return g;
    }

    public Goal getMATG2(){
        Goal g = new Goal("MAT - 2nd Grad Semester", "The basic requirement for the first semester of 50 student's computer sciense curriculum");
        g.addRangeGoal(743, 768, 10);
        g.addRangeGoal(743, 768, 10);
        g.addRangeGoal(743, 768, 10);
        return g;
    }

    public Goal getMATG3(){
        Goal g = new Goal("MAT - 3rd Grad Semester", "The basic requirement for the first semester of 50 student's computer sciense curriculum");
        g.addRangeGoal(743, 768, 10);
        g.addRangeGoal(743, 768, 10);
        g.addRangeGoal(743, 768, 10);
        return g;
    }

    public Goal getMATG4(){
        Goal g = new Goal("MAT - 4th Grad Semester", "The basic requirement for the first semester of 50 student's computer sciense curriculum");
        g.addRangeGoal(743, 768, 10);
        g.addRangeGoal(743, 768, 10);
        g.addRangeGoal(743, 768, 10);
        return g;
    }

    public Goal getPHY1(){
         Goal g = new Goal("PHY - 1st Semester", "The basic requirement for the first semester of 50 student's material science curriculum");
        g.addCourseGoal(0, 50); //engr101
        g.addCourseGoal(2, 50); //eng101
        g.addCourseGoal(4, 50); //math110
        g.addCourseGoal(9, 50); //phy110 and Ectras
        g.addCourseGoal(507, 50);
        g.addCourseGoal(13, 50);
        return g;
    }

    public Goal getPHY2() {
        Goal g = new Goal("PHY - 2nd Semester", "The basic requirement for the second semester of 50 student's material science curriculum");
        g.addCourseGoal(3, 50); //eng102
        g.addCourseGoal(5, 50); //math120
        g.addCourseGoal(10, 50); //chem110
        g.addCourseGoal(29, 50);//phy2
        g.addCourseGoal(30, 50);
        g.addCourseGoal(31, 50);
        return g;
    }

    public Goal getPHY3() {
        Goal g = new Goal("PHY - 3rd Semester", "The basic requirement for the second semester of 50 student's material science curriculum");
        g.addCourseGoal(17, 50); //eco
        g.addCourseGoal(6, 50); //math130
        g.addCourseGoal(404, 50);//phy
        g.addCourseGoal(405, 50);
        return g;
    }

    public Goal getPHY4() {
        Goal g = new Goal("PHY - 4th Semester", "The basic requirement for the second semester of 50 student's material science curriculum");
        g.addCourseGoal(32, 50); //math130
        g.addCourseGoal(406, 50);//phy
        g.addCourseGoal(407, 50);
        g.addCourseGoal("HFlag", 50);
        return g;
    }

    public Goal getPHY5() {
        Goal g = new Goal("PHY - 5th Semester", "The basic requirement for the second semester of 50 student's material science curriculum");
        g.addCourseGoal(28, 50);//math230
        g.addCourseGoal(408, 50);//phy
        g.addCourseGoal(409, 50);
        g.addCourseGoal(410, 50);
        return g;
    }

    public Goal getPHY6() {
        Goal g = new Goal("PHY - 6th Semester", "The basic requirement for the second semester of 50 student's material science curriculum");
        g.addCourseGoal(411, 50);//phy
        g.addCourseGoal(412, 50);
        g.addCourseGoal(413, 50);
        g.addCourseGoal("HFlag", 50);
        return g;
    }

    public Goal getPHY7() {
        Goal g = new Goal("PHY - 7th Semester", "The basic requirement for the second semester of 50 student's material science curriculum");
        g.addCourseGoal(414, 50);//phy
        g.addCourseGoal(415, 50);
        g.addRangeGoal(417, 422, 50);
        g.addCourseGoal("HFlag", 50);
        return g;
    }

    public Goal getPHY8() {
        Goal g = new Goal("PHY - 8th Semester", "The basic requirement for the second semester of 50 student's material science curriculum");
        g.addCourseGoal(416, 50);//phy
        g.addRangeGoal(417, 422, 50);
        g.addCourseGoal("HFlag", 50);
        return g;
    }

    public Goal getPHYMathRec() {
        return addMathRec("PHY");
    }

    public Goal getPHYsci() {
        Goal g = new Goal("PHY - Improved Science", "Adds recitation and labs to the science courses");
        g = addChemRec(g);
        return g;
    }

    public Goal getPHYG1(){
        Goal g = new Goal("PHY - 1st Grad Semester", "The basic requirement for the first semester of 50 student's computer sciense curriculum");
        g.addCourseGoal(769, 10);
        g.addCourseGoal(770, 10);
        g.addCourseGoal(772, 10);
        return g;
    }

    public Goal getPHYG2(){
        Goal g = new Goal("PHY - 2nd Grad Semester", "The basic requirement for the first semester of 50 student's computer sciense curriculum");
        g.addCourseGoal(771, 10);
        g.addCourseGoal(773, 10);
        g.addRangeGoal(774, 786, 10);
        return g;
    }

    public Goal getPHYG3(){
        Goal g = new Goal("PHY - 3rd Grad Semester", "The basic requirement for the first semester of 50 student's computer sciense curriculum");
        g.addRangeGoal(774, 786, 10);
        g.addRangeGoal(774, 786, 10);
        g.addRangeGoal(774, 786, 10);
        return g;
    }

    public Goal getPHYG4(){
        Goal g = new Goal("PHY - 4th Grad Semester", "The basic requirement for the first semester of 50 student's computer sciense curriculum");
        g.addRangeGoal(774, 786, 10);
        g.addRangeGoal(774, 786, 10);
        return g;
    }

    public Goal getCHM1(){
         Goal g = new Goal("CHM - 1st Semester", "The basic requirement for the first semester of 50 student's material science curriculum");
        g.addCourseGoal(0, 50); //engr101
        g.addCourseGoal(2, 50); //eng101
        g.addCourseGoal(4, 50); //math110
        g.addCourseGoal(10, 50); //CHM110
        g.addCourseGoal(12, 50);
        g.addCourseGoal(26, 50);
        return g;
    }

    public Goal getCHM2() {
        Goal g = new Goal("CHM - 2nd Semester", "The basic requirement for the second semester of 50 student's material science curriculum");
        g.addCourseGoal(3, 50); //eng102
        g.addCourseGoal(5, 50); //math120
        g.addCourseGoal(10, 50); //chem110
        g.addCourseGoal(29, 50);//phy2
        g.addCourseGoal(12, 50);
        g.addCourseGoal(26, 50);
        return g;
    }

    public Goal getCHM3() {
        Goal g = new Goal("CHM - 3rd Semester", "The basic requirement for the second semester of 50 student's material science curriculum");
        g.addCourseGoal(17, 50); //eco
        g.addCourseGoal(6, 50); //math130
        g.addCourseGoal(23, 50);//CHM120
        g.addCourseGoal(25, 50);
        g.addCourseGoal(24, 50);
        g.addCourseGoal("HFlag", 50);
        return g;
    }

    public Goal getCHM4() {
        Goal g = new Goal("CHM - 4th Semester", "The basic requirement for the second semester of 50 student's material science curriculum");
        g.addCourseGoal(32, 50); //math130
        g.addCourseGoal(398, 50);//CHM
        g.addCourseGoal(399, 50);
        g.addCourseGoal(423, 50);
        g.addCourseGoal("HFlag", 50);
        return g;
    }

    public Goal getCHM5() {
        Goal g = new Goal("CHM - 5th Semester", "The basic requirement for the second semester of 50 student's material science curriculum");
        g.addCourseGoal(28, 50);//math230
        g.addCourseGoal(429, 50);//CHM
        g.addCourseGoal(430, 50);
        g.addCourseGoal(426, 50);
        return g;
    }

    public Goal getCHM6() {
        Goal g = new Goal("CHM - 6th Semester", "The basic requirement for the second semester of 50 student's material science curriculum");
        g.addCourseGoal(427, 50);//CHM
        g.addCourseGoal(428, 50);
        g.addCourseGoal(425, 50);
        g.addCourseGoal(430, 50);
        return g;
    }

    public Goal getCHM7() {
        Goal g = new Goal("CHM - 7th Semester", "The basic requirement for the second semester of 50 student's material science curriculum");
        g.addCourseGoal(431, 50);//CHM
        g.addCourseGoal(432, 50);
        g.addRangeGoal(433, 451, 50);
        g.addCourseGoal("HFlag", 50);
        return g;
    }

    public Goal getCHM8() {
        Goal g = new Goal("CHM - 8th Semester", "The basic requirement for the second semester of 50 student's material science curriculum");
        g.addCourseGoal(424, 50);//CHM
        g.addRangeGoal(433, 451, 100);
        g.addCourseGoal("HFlag", 50);
        return g;
    }

    public Goal getCHMMathRec() {
        return addMathRec("CHM");
    }

    public Goal getCHMsci() {
        Goal g = new Goal("CHM - Improved Science", "Adds recitation and labs to the science courses");
        g.setExtraGoal(30);
        g.setExtraGoal(31);
        return g;
    }

    public Goal getCHMG1(){
        Goal g = new Goal("CHM - 1st Grad Semester", "The basic requirement for the first semester of 50 student's computer sciense curriculum");
        g.addCourseGoal(817, 10);
        g.addCourseGoal(818, 10);
        g.addRangeGoal(787, 816, 10);
        return g;
    }

    public Goal getCHMG2(){
        Goal g = new Goal("CHM - 2nd Grad Semester", "The basic requirement for the first semester of 50 student's computer sciense curriculum");
        g.addRangeGoal(787, 816, 10);
        g.addRangeGoal(787, 816, 10);
        g.addRangeGoal(787, 816, 10);
        return g;
    }

    public Goal getCHMG3(){
        Goal g = new Goal("CHM - 3rd Grad Semester", "The basic requirement for the first semester of 50 student's computer sciense curriculum");
        g.addRangeGoal(787, 816, 10);
        g.addRangeGoal(787, 816, 10);
        g.addRangeGoal(787, 816, 10);
        return g;
    }

    public Goal getCHMG4(){
        Goal g = new Goal("CHM - 4th Grad Semester", "The basic requirement for the first semester of 50 student's computer sciense curriculum");
        g.addRangeGoal(787, 816, 10);
        g.addRangeGoal(787, 816, 10);
        return g;
    }

    public Goal getMATH1(){
        Goal g = new Goal("MATH - 1st Semester", "The basic requirement for the first semester of 50 student's math curriculum");
        g.addCourseGoal(17, 50);//eco
        g.addCourseGoal(4, 50);//math
        g.addCourseGoal(14, 50);
        g.addCourseGoal(2, 50);
        g.addRangeGoal(9, 11, 50);//sci
        return g;
    }

    public Goal getMATH2(){
        Goal g = new Goal("MATH - 2nd Semester", "The basic requirement for the second semester of 50 student's math curriculum");
        g.addCourseGoal(5, 50);//math
        g.addCourseGoal(15, 50);
        g.addCourseGoal(3, 50);//eng
        g.addRangeGoal(9, 11, 50);//sci
        g.addCourseGoal(510, 50);
        return g;
    }

    public Goal getMATH3(){
        Goal g = new Goal("MATH - 3rd Semester", "The basic requirement for the third semester of 50 student's math curriculum");
        g.addCourseGoal(511, 50);
        g.addCourseGoal(511, 50);
        g.addCourseGoal(516, 50);
        g.addCourseGoal(6, 50);//math
        g.addCourseGoal(16, 50);
        g.addCourseGoal(1, 50);//cse
        return g;
    }

    public Goal getMATH4(){
        Goal g = new Goal("MATH - 4th Semester", "The basic requirement for the fourth semester of 50 student's math curriculum");
        g.addCourseGoal(513, 50);
        g.addCourseGoal(517, 50);
        g.addCourseGoal(7, 50);//cse
        g.addCourseGoal("HFlag", 50);
        return g;
    }

    public Goal getMATH5(){
        Goal g = new Goal("MATH - 5th Semester", "The basic requirement for the fifth semester of 50 student's math curriculum");
        g.addCourseGoal(514, 50);
        g.addRangeGoal(520, 526, 50);
        g.addCourseGoal(8, 50);//cse
        g.addCourseGoal("HFlag", 50);
        return g;
    }

    public Goal getMATH6(){
        Goal g = new Goal("MATH - 6th Semester", "The basic requirement for the sixth semester of 50 student's math curriculum");
        g.addCourseGoal(515, 50);
        g.addRangeGoal(520, 526, 50);
        g.addRangeGoal(144, 160, 50);
        g.addCourseGoal("HFlag", 50);
        return g;
    }

    public Goal getMATH7(){
        Goal g = new Goal("MATH - 7th Semester", "The basic requirement for the seventh semester of 50 student's math curriculum");
        g.addCourseGoal(518, 50);
        g.addRangeGoal(527, 541, 50);
        g.addRangeGoal(144, 160, 50);
        g.addCourseGoal("HFlag", 50);
        return g;
    }

    public Goal getMATH8(){
        Goal g = new Goal("MATH - 8th Semester", "The basic requirement for the eighth semester of 50 student's math curriculum");
        g.addCourseGoal(519, 50);
        g.addRangeGoal(527, 541, 100);
        g.addCourseGoal("HFlag", 50);
        return g;
    }

    public Goal getMATHG1(){
        Goal g = new Goal("MATH - 1st Grad Semester", "The basic requirement for the first semester of 50 student's computer sciense curriculum");
        g.addCourseGoal(819, 10);
        g.addCourseGoal(820, 10);
        g.addCourseGoal(821, 10);
        return g;
    }

    public Goal getMATHG2(){
        Goal g = new Goal("MATH - 2nd Grad Semester", "The basic requirement for the first semester of 50 student's computer sciense curriculum");
        g.addCourseGoal(822, 10);
        g.addRangeGoal(823, 841, 10);
        g.addRangeGoal(823, 841, 10);
        return g;
    }

    public Goal getMATHG3(){
        Goal g = new Goal("MATH - 3rd Grad Semester", "The basic requirement for the first semester of 50 student's computer sciense curriculum");
        g.addRangeGoal(823, 841, 10);
        g.addRangeGoal(823, 841, 10);
        g.addRangeGoal(823, 841, 10);
        return g;
    }

    public Goal getMATHG4(){
        Goal g = new Goal("MATH - 4th Grad Semester", "The basic requirement for the first semester of 50 student's computer sciense curriculum");
        g.addRangeGoal(823, 841, 10);
        g.addRangeGoal(823, 841, 10);
        return g;
    }

    //ENG

    public Goal getENG1(){
        Goal g = new Goal("ENG - 1st Semester", "The basic requirement for the first semester of 50 student's math curriculum");
        g.addCourseGoal(17, 50);//eco
        g.addCourseGoal(542, 50);//math
        g.addCourseGoal(2, 50);
        g.addRangeGoal(9, 11, 50);//sci
        return g;
    }

    public Goal getENG2(){
        Goal g = new Goal("ENG - 2nd Semester", "The basic requirement for the second semester of 50 student's math curriculum");
        g.addCourseGoal(3, 50);//eng
        g.addRangeGoal(9, 11, 50);//sci
        g.addCourseGoal(543, 50);//math
        g.addCourseGoal(268, 50);
        return g;
    }

    public Goal getENG3(){
        Goal g = new Goal("ENG - 2nd Semester", "The basic requirement for the second semester of 50 student's math curriculum");
        g.addCourseGoal(269, 50);
        g.addCourseGoal(271, 50);
        g.addCourseGoal(273, 50);
        return g;
    }
    public Goal getENG4(){
        Goal g = new Goal("ENG - 4th Semester", "The basic requirement for the second semester of 50 student's math curriculum");
        g.addCourseGoal(270, 50);
        g.addCourseGoal(272, 50);
        g.addRangeGoal(274, 298, 50);
        return g;
    }

    public Goal getENG5(){
        Goal g = new Goal("ENG - 5th Semester", "The basic requirement for the second semester of 50 student's math curriculum");;
        g.addRangeGoal(276, 297, 50);
        g.addRangeGoal(299, 301, 50);
        g.addCourseGoal("HFlag", 50);
        return g;
    }

    public Goal getENG6(){
        Goal g = new Goal("ENG - 5th Semester", "The basic requirement for the second semester of 50 student's math curriculum");;
        g.addRangeGoal(276, 297, 50);
        g.addRangeGoal(276, 297, 50);
        g.addCourseGoal("HFlag", 50);
        return g;
    }
    
    public Goal getENGG1(){
        Goal g = new Goal("ENG - 1st Grad Semester", "The basic requirement for the first semester of 50 student's computer sciense curriculum");
        g.addCourseGoal(842, 10);
        g.addCourseGoal(843, 10);
        g.addRangeGoal(844, 861, 10);
        return g;
    }

    public Goal getENGG2(){
        Goal g = new Goal("ENG - 2nd Grad Semester", "The basic requirement for the first semester of 50 student's computer sciense curriculum");
        g.addRangeGoal(844, 861, 10);
        g.addRangeGoal(844, 861, 10);
        g.addRangeGoal(844, 861, 10);
        return g;
    }

    public Goal getENGG3(){
        Goal g = new Goal("ENG - 3rd Grad Semester", "The basic requirement for the first semester of 50 student's computer sciense curriculum");
        g.addRangeGoal(844, 861, 10);
        g.addRangeGoal(844, 861, 10);
        g.addRangeGoal(844, 861, 10);
        return g;
    }

    public Goal getENGG4(){
        Goal g = new Goal("ENG - 4th Grad Semester", "The basic requirement for the first semester of 50 student's computer sciense curriculum");
        g.addRangeGoal(844, 861, 10);
        g.addRangeGoal(844, 861, 10);
        g.addRangeGoal(844, 861, 10);
        return g;
    }

    public Goal getHIST1(){
        Goal g = new Goal("HIST - 1st Semester", "The basic requirement for the first semester of 50 student's math curriculum");
        g.addCourseGoal(17, 50);//eco
        g.addCourseGoal(542, 50);//math
        g.addCourseGoal(2, 50);
        g.addRangeGoal(9, 11, 50);//sci
        return g;
    }

    public Goal getHIST2(){
        Goal g = new Goal("HIST - 2nd Semester", "The basic requirement for the second semester of 50 student's math curriculum");
        g.addCourseGoal(3, 50);//eng
        g.addCourseGoal(543, 50);//math
        g.addCourseGoal(302, 50);
        g.addCourseGoal(303, 50);
        return g;
    }

    public Goal getHIST3(){
        Goal g = new Goal("HIST - 2nd Semester", "The basic requirement for the second semester of 50 student's math curriculum");
        g.addRangeGoal(9, 11, 50);//sci
        g.addCourseGoal(304, 50);
        g.addRangeGoal(330, 346, 50);
        return g;
    }
    public Goal getHIST4(){
        Goal g = new Goal("HIST - 4th Semester", "The basic requirement for the second semester of 50 student's math curriculum");
        g.addRangeGoal(305, 329, 50);
        g.addRangeGoal(330, 346, 50);
        g.addCourseGoal("HFlag", 50);
        return g;
    }

    public Goal getHIST5(){
        Goal g = new Goal("HIST - 5th Semester", "The basic requirement for the second semester of 50 student's math curriculum");;
        g.addRangeGoal(305, 329, 50);
        g.addRangeGoal(348, 372, 50);
        g.addRangeGoal(373, 385, 50);
        return g;
    }

    public Goal getHIST6(){
        Goal g = new Goal("HIST - 6th Semester", "The basic requirement for the second semester of 50 student's math curriculum");;
        g.addRangeGoal(348, 372, 50);
        g.addRangeGoal(373, 385, 50);
        g.addRangeGoal(330, 346, 50);
        g.addRangeGoal(305, 329, 50);
        return g;
    }

    public Goal getHISTG1(){
        Goal g = new Goal("HIST - 1st Grad Semester", "The basic requirement for the first semester of 50 student's computer sciense curriculum");
        g.addCourseGoal(876, 10);
        g.addRangeGoal(862, 875, 10);
        g.addRangeGoal(862, 875, 10);
        return g;
    }

    public Goal getHISTG2(){
        Goal g = new Goal("HIST - 2nd Grad Semester", "The basic requirement for the first semester of 50 student's computer sciense curriculum");
        g.addRangeGoal(862, 875, 10);
        g.addRangeGoal(862, 875, 10);
        g.addRangeGoal(862, 875, 10);
        return g;
    }

    public Goal getHISTG3(){
        Goal g = new Goal("HIST - 3rd Grad Semester", "The basic requirement for the first semester of 50 student's computer sciense curriculum");
        g.addRangeGoal(862, 875, 10);
        g.addRangeGoal(862, 875, 10);
        g.addRangeGoal(862, 875, 10);
        return g;
    }

    public Goal getHISTG4(){
        Goal g = new Goal("HIST - 4th Grad Semester", "The basic requirement for the first semester of 50 student's computer sciense curriculum");
        g.addRangeGoal(862, 875, 10);
        g.addRangeGoal(862, 875, 10);
        g.addCourseGoal(877, 10);
        return g;
    }

    public Goal getBISG1(){
        Goal g = new Goal("BIS - 1st Grad Semester", "The basic requirement for the first semester of 50 student's computer sciense curriculum");
        g.addCourseGoal(879, 10);
        g.addCourseGoal(880, 10);
        g.addCourseGoal(881, 10);
        g.addCourseGoal(882, 10);
        g.addCourseGoal(883, 10);
        g.addCourseGoal(884, 10);
        return g;
    }

    public Goal getBISG2(){
        Goal g = new Goal("BIS - 2nd Grad Semester", "The basic requirement for the first semester of 50 student's computer sciense curriculum");
        g.addCourseGoal(885, 10);
        g.addCourseGoal(886, 10);
        g.addCourseGoal(887, 10);
        g.addCourseGoal(888, 10);
        g.addCourseGoal(889, 10);
        g.addCourseGoal(890, 10);
        return g;
    }

    public Goal getBISG3(){
        Goal g = new Goal("BIS - 3rd Grad Semester", "The basic requirement for the first semester of 50 student's computer sciense curriculum");
        g.addCourseGoal(891, 10);
        g.addCourseGoal(892, 10);
        g.addRangeGoal(893, 919, 10);
        g.addRangeGoal(893, 919, 10);
        return g;
    }

    public Goal getBISG4(){
        Goal g = new Goal("BIS - 4th Grad Semester", "The basic requirement for the first semester of 50 student's computer sciense curriculum");
        g.addRangeGoal(893, 919, 10);
        g.addRangeGoal(893, 919, 10);
        g.addRangeGoal(893, 919, 10);
        g.addRangeGoal(893, 919, 10);
        return g;
    }

    public Goal getECO1(){
        return getGeneralBis1(new Goal("Eco - 1st Semester", "The basic requirement for the first semester of 50 student's material science curriculum"));
    }

    public Goal getECO2(){
        return getGeneralBis2(new Goal("Eco - 2nd Semester", "The basic requirement for the second semester of 50 student's material science curriculum"));
    }

    public Goal getECO3(){
        return getGeneralBis3(new Goal("Eco - 3rd Semester", "The basic requirement for the third semester of 50 student's material science curriculum"));
    }

    public Goal getECO4(){
        Goal g = new Goal("Eco - 4th Semester", "The basic requirement for the third semester of 50 student's material science curriculum");
        g.addCourseGoal(478, 50);
        return getGeneralBis4(g);
    }

    public Goal getECO5(){
        Goal g = new Goal("Eco - 5th Semester", "The basic requirement for the third semester of 50 student's material science curriculum");
        g.addRangeGoal(479, 489, 50);
        return getGeneralBis5(g);
    }

    public Goal getECO6(){
        Goal g = new Goal("Eco - 6th Semester", "The basic requirement for the third semester of 50 student's material science curriculum");
        g.addRangeGoal(479, 489, 50);
        return getGeneralBis6(g);
    }

    public Goal getECO7(){
        Goal g = new Goal("Eco - 7th Semester", "The basic requirement for the third semester of 50 student's material science curriculum");
        g.addRangeGoal(479, 489, 50);
        g.addRangeGoal(490, 507, 50);
        return getGeneralBis7(g);
    }
    
    public Goal getECO8(){
        Goal g = new Goal("Eco - 8th Semester", "The basic requirement for the third semester of 50 student's material science curriculum");
        g.addRangeGoal(490, 507, 50);
        g.addRangeGoal(490, 507, 50);
        return getGeneralBis8(g);
    }

    public Goal getECOG1(){
        Goal g = new Goal("ECO - 1st Grad Semester", "The basic requirement for the first semester of 50 student's computer sciense curriculum");
        g.addCourseGoal(920, 10);
        g.addCourseGoal(921, 10);
        g.addCourseGoal(922, 10);
        g.addCourseGoal(923, 10);
        return g;
    }

    public Goal getECOG2(){
        Goal g = new Goal("ECO - 2nd Grad Semester", "The basic requirement for the first semester of 50 student's computer sciense curriculum");
        g.addCourseGoal(924, 10);
        g.addCourseGoal(925, 10);
        g.addCourseGoal(926, 10);
        g.addCourseGoal(927, 10);
        return g;
    }

    public Goal getECOG3(){
        Goal g = new Goal("ECO - 3rd Grad Semester", "The basic requirement for the first semester of 50 student's computer sciense curriculum");
        g.addRangeGoal(928, 952, 10);
        g.addRangeGoal(928, 952, 10);
        g.addRangeGoal(928, 952, 10);
        g.addRangeGoal(928, 952, 10);
        return g;
    }

    public Goal getECOG4(){
        Goal g = new Goal("ECO - 4th Grad Semester", "The basic requirement for the first semester of 50 student's computer sciense curriculum");
        g.addRangeGoal(928, 952, 10);
        g.addRangeGoal(928, 952, 10);
        g.addRangeGoal(928, 952, 10);
        g.addRangeGoal(928, 952, 10);
        return g;
    }

    public Goal getMKT1(){
        return getGeneralBis1(new Goal("Mkt - 1st Semester", "The basic requirement for the first semester of 50 student's material science curriculum"));
    }

    public Goal getMKT2(){
        return getGeneralBis2(new Goal("Mkt - 2nd Semester", "The basic requirement for the second semester of 50 student's material science curriculum"));
    }

    public Goal getMKT3(){
        return getGeneralBis3(new Goal("Mkt - 3rd Semester", "The basic requirement for the third semester of 50 student's material science curriculum"));
    }

    public Goal getMKT4(){
        Goal g = new Goal("Mkt - 4th Semester", "The basic requirement for the third semester of 50 student's material science curriculum");
        g.addCourseGoal(543, 50);
        return getGeneralBis4(g);
    }

    public Goal getMKT5(){
        Goal g = new Goal("Mkt - 5th Semester", "The basic requirement for the third semester of 50 student's material science curriculum");
        g.addCourseGoal(544, 50);
        return getGeneralBis5(g);
    }

    public Goal getMKT6(){
        Goal g = new Goal("Mkt - 6th Semester", "The basic requirement for the third semester of 50 student's material science curriculum");
        g.addCourseGoal(545, 50);
        return getGeneralBis6(g);
    }

    public Goal getMKT7(){
        Goal g = new Goal("Mkt - 7th Semester", "The basic requirement for the third semester of 50 student's material science curriculum");
        g.addRangeGoal(546, 558, 50);
        g.addRangeGoal(546, 558, 50);
        return getGeneralBis7(g);
    }
    
    public Goal getMKT8(){
        Goal g = new Goal("Mkt - 8th Semester", "The basic requirement for the third semester of 50 student's material science curriculum");
        g.addRangeGoal(546, 558, 50);
        g.addRangeGoal(546, 558, 50);
        return getGeneralBis8(g);
    }

    public Goal getACT1(){
        return getGeneralBis1(new Goal("Act - 1st Semester", "The basic requirement for the first semester of 50 student's material science curriculum"));
    }

    public Goal getACT2(){
        return getGeneralBis2(new Goal("Act - 2nd Semester", "The basic requirement for the second semester of 50 student's material science curriculum"));
    }

    public Goal getACT3(){
        return getGeneralBis3(new Goal("Act - 3rd Semester", "The basic requirement for the third semester of 50 student's material science curriculum"));
    }

    public Goal getACT4(){
        Goal g = new Goal("Act - 4th Semester", "The basic requirement for the third semester of 50 student's material science curriculum");
        g.addCourseGoal(559, 50);
        return getGeneralBis4(g);
    }

    public Goal getACT5(){
        Goal g = new Goal("Act - 5th Semester", "The basic requirement for the third semester of 50 student's material science curriculum");
        g.addCourseGoal(560, 50);
        return getGeneralBis5(g);
    }

    public Goal getACT6(){
        Goal g = new Goal("Act - 6th Semester", "The basic requirement for the third semester of 50 student's material science curriculum");
        g.addCourseGoal(561, 50);
        return getGeneralBis6(g);
    }

    public Goal getACT7(){
        Goal g = new Goal("Act - 7th Semester", "The basic requirement for the third semester of 50 student's material science curriculum");
        g.addCourseGoal(562, 50);
        g.addRangeGoal(563, 567, 50);
        return getGeneralBis7(g);
    }
    
    public Goal getACT8(){
        Goal g = new Goal("Act - 8th Semester", "The basic requirement for the third semester of 50 student's material science curriculum");
        g.addRangeGoal(563, 567, 50);
        g.addRangeGoal(563, 567, 50);
        return getGeneralBis8(g);
    }

    public Goal getFIN1(){
        return getGeneralBis1(new Goal("Fin - 1st Semester", "The basic requirement for the first semester of 50 student's material science curriculum"));
    }

    public Goal getFIN2(){
        return getGeneralBis2(new Goal("Fin - 2nd Semester", "The basic requirement for the second semester of 50 student's material science curriculum"));
    }

    public Goal getFIN3(){
        return getGeneralBis3(new Goal("Fin - 3rd Semester", "The basic requirement for the third semester of 50 student's material science curriculum"));
    }

    public Goal getFIN4(){
        Goal g = new Goal("Fin - 4th Semester", "The basic requirement for the third semester of 50 student's material science curriculum");
        g.addCourseGoal(568, 50);
        return getGeneralBis4(g);
    }

    public Goal getFIN5(){
        Goal g = new Goal("Fin - 5th Semester", "The basic requirement for the third semester of 50 student's material science curriculum");
        g.addCourseGoal(569, 50);
        return getGeneralBis5(g);
    }

    public Goal getFIN6(){
        Goal g = new Goal("Fin - 6th Semester", "The basic requirement for the third semester of 50 student's material science curriculum");
        g.addRangeGoal(479, 489, 50);
        return getGeneralBis6(g);
    }

    public Goal getFIN7(){
        Goal g = new Goal("Fin - 7th Semester", "The basic requirement for the third semester of 50 student's material science curriculum");
        g.addRangeGoal(570, 576, 50);
        g.addRangeGoal(546, 558, 50);
        return getGeneralBis7(g);
    }
    
    public Goal getFIN8(){
        Goal g = new Goal("Fin - 8th Semester", "The basic requirement for the third semester of 50 student's material science curriculum");
        g.addRangeGoal(570, 576, 50);
        g.addRangeGoal(559, 563, 50);
        return getGeneralBis8(g);
    }

    public Goal getGeneralBis1(Goal g){
        g.addCourseGoal(2, 50);
        g.addCourseGoal(19, 50);  
        g.addCourseGoal(457, 50); 
        g.addCourseGoal(459, 50); 
        g.addCourseGoal(463, 50);
        return g;
    }

    public Goal getGeneralBis2(Goal g){
        //add sci
        g.addCourseGoal(460, 50);
        g.addCourseGoal(462, 50);  
        g.addCourseGoal(461, 50); 
        g.addCourseGoal(3, 50); 
        return g;
    }

    public Goal getGeneralBis3(Goal g){
        g.addCourseGoal(464, 50);  
        g.addCourseGoal(466, 50); 
        g.addCourseGoal(469, 50);
        g.addCourseGoal(470, 50);
        return g;
    }

    public Goal getGeneralBis4(Goal g){
        g.addCourseGoal(465, 50);
        g.addCourseGoal(467, 50);  
        g.addCourseGoal(471, 50); 
        return g;
    }

    public Goal getGeneralBis5(Goal g){
        g.addCourseGoal(472, 50);
        g.addCourseGoal(474, 50);  
        g.addCourseGoal(471, 50); 
        return g;
    }

    public Goal getGeneralBis6(Goal g){
        g.addCourseGoal(473, 50);
        g.addCourseGoal(475, 50);
        g.addCourseGoal("HFlag", 50);
        return g;
    }

    public Goal getGeneralBis7(Goal g){
        g.addCourseGoal(476, 50);
        g.addCourseGoal("HFlag", 50);
        return g;
    }

    public Goal getGeneralBis8(Goal g){
        g.addCourseGoal(477, 50);
        g.addCourseGoal("HFlag", 50);
        return g;
    }

    public Goal addMathRec(string s){
        Goal g = new Goal(s + " - Improved Math", "Adds recitation to the math courses");
        g.setExtraGoal(14);
        g.setExtraGoal(15);
        g.setExtraGoal(16);
        return g;
    }

    public Goal addPhyRec(Goal g){
        g.setExtraGoal(507);
        g.setExtraGoal(13);
        g.setExtraGoal(30);
        g.setExtraGoal(31);
        return g;
    }

    public Goal addChemRec(Goal g){
        g.setExtraGoal(12);
        g.setExtraGoal(26);
        return g;
    }
}