using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuideController : MonoBehaviour
{
    public void refreshGuide(){
        switch (GameObject.Find("GuideDropdown").GetComponent<Dropdown>().value){
            case 0:
                setLine(1, GoalController.Instance.getCSE1());
                setLine(2, GoalController.Instance.getCSE2());
                setLine(3, GoalController.Instance.getCSE3());
                setLine(4, GoalController.Instance.getCSE4());
                setLine(5, GoalController.Instance.getCSE5());
                setLine(6, GoalController.Instance.getCSE6());
                setLine(7, GoalController.Instance.getCSE7());
                setLine(8, GoalController.Instance.getCSE8());
                break;
            case 1:
                setLine(1, GoalController.Instance.getMEC1());
                setLine(2, GoalController.Instance.getMEC2());
                setLine(3, GoalController.Instance.getMEC3());
                setLine(4, GoalController.Instance.getMEC4());
                setLine(5, GoalController.Instance.getMEC5());
                setLine(6, GoalController.Instance.getMEC6());
                setLine(7, GoalController.Instance.getMEC7());
                setLine(8, GoalController.Instance.getMEC8());
                break;
            case 2:
                setLine(1, GoalController.Instance.getCHE1());
                setLine(2, GoalController.Instance.getCHE2());
                setLine(3, GoalController.Instance.getCHE3());
                setLine(4, GoalController.Instance.getCHE4());
                setLine(5, GoalController.Instance.getCHE5());
                setLine(6, GoalController.Instance.getCHE6());
                setLine(7, GoalController.Instance.getCHE7());
                setLine(8, GoalController.Instance.getCHE8());
                break;
            case 3:
                setLine(1, GoalController.Instance.getELE1());
                setLine(2, GoalController.Instance.getELE2());
                setLine(3, GoalController.Instance.getELE3());
                setLine(4, GoalController.Instance.getELE4());
                setLine(5, GoalController.Instance.getELE5());
                setLine(6, GoalController.Instance.getELE6());
                setLine(7, GoalController.Instance.getELE7());
                setLine(8, GoalController.Instance.getELE8());
                break;
            case 4:
                setLine(1, GoalController.Instance.getCIV1());
                setLine(2, GoalController.Instance.getCIV2());
                setLine(3, GoalController.Instance.getCIV3());
                setLine(4, GoalController.Instance.getCIV4());
                setLine(5, GoalController.Instance.getCIV5());
                setLine(6, GoalController.Instance.getCIV6());
                setLine(7, GoalController.Instance.getCIV7());
                setLine(8, GoalController.Instance.getCIV8());
                break;
            case 5:
                setLine(1, GoalController.Instance.getISE1());
                setLine(2, GoalController.Instance.getISE2());
                setLine(3, GoalController.Instance.getISE3());
                setLine(4, GoalController.Instance.getISE4());
                setLine(5, GoalController.Instance.getISE5());
                setLine(6, GoalController.Instance.getISE6());
                setLine(7, GoalController.Instance.getISE7());
                setLine(8, GoalController.Instance.getISE8());
                break;
            case 6:
                setLine(1, GoalController.Instance.getMAT1());
                setLine(2, GoalController.Instance.getMAT2());
                setLine(3, GoalController.Instance.getMAT3());
                setLine(4, GoalController.Instance.getMAT4());
                setLine(5, GoalController.Instance.getMAT5());
                setLine(6, GoalController.Instance.getMAT6());
                setLine(7, GoalController.Instance.getMAT7());
                setLine(8, GoalController.Instance.getMAT8());
                break;
            case 7:
                setLine(1, GoalController.Instance.getPHY1());
                setLine(2, GoalController.Instance.getPHY2());
                setLine(3, GoalController.Instance.getPHY3());
                setLine(4, GoalController.Instance.getPHY4());
                setLine(5, GoalController.Instance.getPHY5());
                setLine(6, GoalController.Instance.getPHY6());
                setLine(7, GoalController.Instance.getPHY7());
                setLine(8, GoalController.Instance.getPHY8());
                break;
            case 8:
                setLine(1, GoalController.Instance.getCHM1());
                setLine(2, GoalController.Instance.getCHM2());
                setLine(3, GoalController.Instance.getCHM3());
                setLine(4, GoalController.Instance.getCHM4());
                setLine(5, GoalController.Instance.getCHM5());
                setLine(6, GoalController.Instance.getCHM6());
                setLine(7, GoalController.Instance.getCHM7());
                setLine(8, GoalController.Instance.getCHM8());
                break;
            case 9:
                setLine(1, GoalController.Instance.getENG1(), 'f');
                setLine(2, GoalController.Instance.getENG2(), 'f');
                setLine(3, GoalController.Instance.getENG3(), 'f');
                setLine(4, GoalController.Instance.getENG4(), 'f');
                setLine(5, GoalController.Instance.getENG5(), 'f');
                setLine(6, GoalController.Instance.getENG6(), 'f');
                cleanSemesters(2);
                break;
            case 10:
                setLine(1, GoalController.Instance.getHIST1(), 'f');
                setLine(2, GoalController.Instance.getHIST2(), 'f');
                setLine(3, GoalController.Instance.getHIST3(), 'f');
                setLine(4, GoalController.Instance.getHIST4(), 'f');
                setLine(5, GoalController.Instance.getHIST5(), 'f');
                setLine(6, GoalController.Instance.getHIST6(), 'f');
                cleanSemesters(2);
                break;
            case 11:
                setLine(1, GoalController.Instance.getMATH1());
                setLine(2, GoalController.Instance.getMATH2());
                setLine(3, GoalController.Instance.getMATH3());
                setLine(4, GoalController.Instance.getMATH4());
                setLine(5, GoalController.Instance.getMATH5());
                setLine(6, GoalController.Instance.getMATH6());
                setLine(7, GoalController.Instance.getMATH7());
                setLine(8, GoalController.Instance.getMATH8());
                break;
            case 12:
                setLine(1, GoalController.Instance.getECO1());
                setLine(2, GoalController.Instance.getECO2());
                setLine(3, GoalController.Instance.getECO3());
                setLine(4, GoalController.Instance.getECO4());
                setLine(5, GoalController.Instance.getECO5());
                setLine(6, GoalController.Instance.getECO6());
                setLine(7, GoalController.Instance.getECO7());
                setLine(8, GoalController.Instance.getECO8());
                break;
            case 13:
                setLine(1, GoalController.Instance.getMKT1());
                setLine(2, GoalController.Instance.getMKT2());
                setLine(3, GoalController.Instance.getMKT3());
                setLine(4, GoalController.Instance.getMKT4());
                setLine(5, GoalController.Instance.getMKT5());
                setLine(6, GoalController.Instance.getMKT6());
                setLine(7, GoalController.Instance.getMKT7());
                setLine(8, GoalController.Instance.getMKT8());
                break;
            case 14:
                setLine(1, GoalController.Instance.getACT1());
                setLine(2, GoalController.Instance.getACT2());
                setLine(3, GoalController.Instance.getACT3());
                setLine(4, GoalController.Instance.getACT4());
                setLine(5, GoalController.Instance.getACT5());
                setLine(6, GoalController.Instance.getACT6());
                setLine(7, GoalController.Instance.getACT7());
                setLine(8, GoalController.Instance.getACT8());
                break;
            case 15:
                setLine(1, GoalController.Instance.getFIN1());
                setLine(2, GoalController.Instance.getFIN2());
                setLine(3, GoalController.Instance.getFIN3());
                setLine(4, GoalController.Instance.getFIN4());
                setLine(5, GoalController.Instance.getFIN5());
                setLine(6, GoalController.Instance.getFIN6());
                setLine(7, GoalController.Instance.getFIN7());
                setLine(8, GoalController.Instance.getFIN8());
                break;
            case 16:
                setLine(1, GoalController.Instance.getCSEG1());
                setLine(2, GoalController.Instance.getCSEG2());
                setLine(3, GoalController.Instance.getCSEG3());
                setLine(4, GoalController.Instance.getCSEG4());
                cleanSemesters(4);
                break;
            case 17:
                setLine(1, GoalController.Instance.getMECG1());
                setLine(2, GoalController.Instance.getMECG2());
                setLine(3, GoalController.Instance.getMECG3());
                setLine(4, GoalController.Instance.getMECG4());
                cleanSemesters(4);
                break;
            case 18:
                setLine(1, GoalController.Instance.getCHEG1());
                setLine(2, GoalController.Instance.getCHEG2());
                setLine(3, GoalController.Instance.getCHEG3());
                setLine(4, GoalController.Instance.getCHEG4());
                cleanSemesters(4);
                break;
            case 19:
                setLine(1, GoalController.Instance.getELEG1());
                setLine(2, GoalController.Instance.getELEG2());
                setLine(3, GoalController.Instance.getELEG3());
                setLine(4, GoalController.Instance.getELEG4());
                cleanSemesters(4);
                break;
            case 20:
                setLine(1, GoalController.Instance.getCIVG1());
                setLine(2, GoalController.Instance.getCIVG2());
                setLine(3, GoalController.Instance.getCIVG3());
                setLine(4, GoalController.Instance.getCIVG4());
                cleanSemesters(4);
                break;
            case 21:
                setLine(1, GoalController.Instance.getISEG1());
                setLine(2, GoalController.Instance.getISEG2());
                setLine(3, GoalController.Instance.getISEG3());
                setLine(4, GoalController.Instance.getISEG4());
                cleanSemesters(4);
                break;
            case 22:
                setLine(1, GoalController.Instance.getMATG1());
                setLine(2, GoalController.Instance.getMATG2());
                setLine(3, GoalController.Instance.getMATG3());
                setLine(4, GoalController.Instance.getMATG4());
                cleanSemesters(4);
                break;
            case 23:
                setLine(1, GoalController.Instance.getPHYG1());
                setLine(2, GoalController.Instance.getPHYG2());
                setLine(3, GoalController.Instance.getPHYG3());
                setLine(4, GoalController.Instance.getPHYG4());
                cleanSemesters(4);
                break;
            case 24:
                setLine(1, GoalController.Instance.getCHMG1());
                setLine(2, GoalController.Instance.getCHMG2());
                setLine(3, GoalController.Instance.getCHMG3());
                setLine(4, GoalController.Instance.getCHMG4());
                cleanSemesters(4);
                break;
            case 25:
                setLine(1, GoalController.Instance.getENGG1());
                setLine(2, GoalController.Instance.getENGG2());
                setLine(3, GoalController.Instance.getENGG3());
                setLine(4, GoalController.Instance.getENGG4());
                cleanSemesters(4);
                break;
            case 26:
                setLine(1, GoalController.Instance.getHISTG1());
                setLine(2, GoalController.Instance.getHISTG2());
                setLine(3, GoalController.Instance.getHISTG3());
                setLine(4, GoalController.Instance.getHISTG4());
                cleanSemesters(4);
                break;
            case 27:
                setLine(1, GoalController.Instance.getMATHG1());
                setLine(2, GoalController.Instance.getMATHG2());
                setLine(3, GoalController.Instance.getMATHG3());
                setLine(4, GoalController.Instance.getMATHG4());
                cleanSemesters(4);
                break;
            case 28:
                setLine(1, GoalController.Instance.getBISG1());
                setLine(2, GoalController.Instance.getBISG2());
                setLine(3, GoalController.Instance.getBISG3());
                setLine(4, GoalController.Instance.getBISG4());
                cleanSemesters(4);
                break;
            case 29:
                setLine(1, GoalController.Instance.getECOG1());
                setLine(2, GoalController.Instance.getECOG2());
                setLine(3, GoalController.Instance.getECOG3());
                setLine(4, GoalController.Instance.getECOG4());
                cleanSemesters(4);
                break;
        }
    }

    public void setLine(int semester, Goal goal){
        int nextLine = 1;
        nextLine = setLineHelper(semester, goal, nextLine);
        foreach(Tuple<int, int, int> t in  goal.getRangeGoals()){
            string s = AcedemicController.Instance.courses[t.Item1].courseName;
            GameObject.Find("Guide" + semester + "_" + nextLine).GetComponent<Text>().text
                 = s.Substring(0, s.Length - 2) + "XX (Elective)";
            nextLine++;
        }
        if(nextLine != 7)
            cleanUpGuide(semester, nextLine);
    }

    //Override with flag for different method for handling electives
    public void setLine(int semester, Goal goal, char f){
        int nextLine = 1;
        nextLine = setLineHelper(semester, goal, nextLine);
        switch(f){
            case 'e':
                foreach(Tuple<int, int, int> t in  goal.getRangeGoals()){
                    string s = AcedemicController.Instance.courses[t.Item2].courseName;
                    GameObject.Find("Guide" + semester + "_" + nextLine).GetComponent<Text>().text
                        = AcedemicController.Instance.courses[t.Item1].courseName + "-" + s.Substring(s.Length - 3);
                    nextLine++;
                }
                break;
            case 'h':
                foreach(Tuple<int, int, int> t in  goal.getRangeGoals()){
                    string sTemp1 = AcedemicController.Instance.courses[t.Item1].courseName;
                    string s1 = "HISTX" + sTemp1.Substring(sTemp1.Length - 2);
                    string sTemp2 = AcedemicController.Instance.courses[t.Item2].courseName;
                    string s2 = "-X" + sTemp2.Substring(sTemp2.Length - 2);
                    GameObject.Find("Guide" + semester + "_" + nextLine).GetComponent<Text>().text= s1 + s2;
                    nextLine++;
                }
                break;
        }
        if(nextLine != 7)
            cleanUpGuide(semester, nextLine);
    }

    //handles seting the descrition for non-range goals
    public int setLineHelper(int semester, Goal goal, int nextLine){
        foreach(int i in goal.getCourseList()){
            GameObject.Find("Guide" + semester + "_" + nextLine).GetComponent<Text>().text
                 = AcedemicController.Instance.courses[i].courseName;
            nextLine++;
        }
        if(goal.getComplexSeatGoals().Count != 0){
            GameObject.Find("Guide" + semester + "_" + nextLine).GetComponent<Text>().text 
                = "PHY110 or CHE110";
            nextLine++;
        }
        if(goal.getHCheck() != 0){
            GameObject.Find("Guide" + semester + "_" + nextLine).GetComponent<Text>().text 
                = "Humanity Course";
            nextLine++;
        }
        else if(goal.getHCheck() == 100){
            GameObject.Find("Guide" + semester + "_" + nextLine).GetComponent<Text>().text 
                = "Humanity Course";
            nextLine++;
        }
        return nextLine;
    }

    //cleans up unused lines
    public void cleanUpGuide(int semester,int nextLine){
        while(nextLine != 7){
            Debug.Log("Guide" + semester + "_" + nextLine);
             GameObject.Find("Guide" + semester + "_" + nextLine).GetComponent<Text>().text = "";
             nextLine++;
        }
    }

    //empties semesters that aren't needed, including for grad programs
    public void cleanSemesters(int semesters){
        GameObject.Find("Guide7_1").GetComponent<Text>().text = "";
        GameObject.Find("Guide7_2").GetComponent<Text>().text = "";
        GameObject.Find("Guide7_3").GetComponent<Text>().text = "";
        GameObject.Find("Guide7_4").GetComponent<Text>().text = "";
        GameObject.Find("Guide7_5").GetComponent<Text>().text = "";
        GameObject.Find("Guide7_6").GetComponent<Text>().text = "";
        GameObject.Find("Guide8_1").GetComponent<Text>().text = "";
        GameObject.Find("Guide8_2").GetComponent<Text>().text = "";
        GameObject.Find("Guide8_3").GetComponent<Text>().text = "";
        GameObject.Find("Guide8_4").GetComponent<Text>().text = "";
        GameObject.Find("Guide8_5").GetComponent<Text>().text = "";
        GameObject.Find("Guide8_6").GetComponent<Text>().text = "";
        if(semesters == 4){
            GameObject.Find("Guide5_1").GetComponent<Text>().text = "";
            GameObject.Find("Guide5_2").GetComponent<Text>().text = "";
            GameObject.Find("Guide5_3").GetComponent<Text>().text = "";
            GameObject.Find("Guide5_4").GetComponent<Text>().text = "";
            GameObject.Find("Guide5_5").GetComponent<Text>().text = "";
            GameObject.Find("Guide5_6").GetComponent<Text>().text = "";
            GameObject.Find("Guide6_1").GetComponent<Text>().text = "";
            GameObject.Find("Guide6_2").GetComponent<Text>().text = "";
            GameObject.Find("Guide6_3").GetComponent<Text>().text = "";
            GameObject.Find("Guide6_4").GetComponent<Text>().text = "";
            GameObject.Find("Guide6_5").GetComponent<Text>().text = "";
            GameObject.Find("Guide6_6").GetComponent<Text>().text = "";
        }
    }
}