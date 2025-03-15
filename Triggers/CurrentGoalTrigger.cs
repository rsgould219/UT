using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentGoalTrigger : MonoBehaviour
{
    public Text label;
    int goalTheme = 1;
    public bool gpOpened = false;
    
    public GameObject GoalDescPanel;
    public GameObject GoalDescList;
     
    //make desc appear for the course
    public void Hover(){
        Goal curGoal = GoalController.Instance.curGoal;
        if(curGoal != null){
            GoalDescPanel.SetActive(true);
            GameObject.Find("GoalName").transform.GetComponent<Text>().text = curGoal.name;
            GameObject.Find("GoalDesc").transform.GetComponent<Text>().text = curGoal.desc;
            simpleCourseCheck(curGoal);
            complexCourseCheck(curGoal);
            extraCheck(curGoal);
            if(gpOpened != true){
                UIController.Instance.setGoalPanelScale(GoalDescPanel);
                gpOpened = true;
            }
            if(ThemeController.Instance.uiTheme != ThemeController.Instance.goalTheme){
                ThemeController.Instance.setGoalDescPanel();
            }
        }
    }

    //makes a text child for each simple course
    public void simpleCourseCheck(Goal gl){
         int count = 0;
         foreach(int i in gl.getCourseList()){
            GameObject child = new GameObject();
            child.AddComponent<Text>();
            child.GetComponent<Text>().text = String.Concat("New ",AcedemicController.Instance.courses[i].courseName,
                " seats to add: ", gl.getSeatGoals()[count]);
            //child.GetComponent<Text>().text = "Test";
            child.GetComponent<Text>().font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            child.GetComponent<Text>().color = Color.black;
            child.layer = 5;
            child.transform.SetParent(GoalDescList.transform, false);
            count++;
        }
    }

    //makes the child text for course extras
    public void extraCheck(Goal gl){
        foreach(int i in gl.getExtraGoals()){
            GameObject child = new GameObject();
            child.AddComponent<Text>();
            string s = "Add seats in ";
            char[] c = {'/'};
            s = String.Concat(s, AcedemicController.Instance.courses[i].courseName, "/" );
            s = String.Concat(s.TrimEnd(c), " ", AcedemicController.Instance.courses[i].seatCount(), "/");

            switch(i){
                case 11:
                    s = String.Concat(s, AcedemicController.Instance.courses[9].seatCount());
                    break;
                case 13:
                    s = String.Concat(s, AcedemicController.Instance.courses[9].seatCount());
                    break;
                case 12:
                    s = String.Concat(s, AcedemicController.Instance.courses[10].seatCount());
                    break;
                case 26:
                    s = String.Concat(s, AcedemicController.Instance.courses[10].seatCount());
                    break;
                case 30:
                    s = String.Concat(s, AcedemicController.Instance.courses[29].seatCount());
                    break;
                case 31:
                    s = String.Concat(s, AcedemicController.Instance.courses[29].seatCount());
                    break;
                case 14:
                    s = String.Concat(s, AcedemicController.Instance.courses[4].seatCount());
                    break;
                case 15:
                    s = String.Concat(s, AcedemicController.Instance.courses[5].seatCount());
                    break;
                case 16:
                    s = String.Concat(s, AcedemicController.Instance.courses[6].seatCount());
                    break;
            }
            child.GetComponent<Text>().text = s;
            child.GetComponent<Text>().font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            child.GetComponent<Text>().color = Color.black;
            child.layer = 5;
            child.transform.SetParent(GoalDescList.transform, false);
        }
     }

    //makes a text child for the complex course
    public void complexCourseCheck(Goal gl){
         int count = 0;
         foreach(List<int> l in gl.getComplexList()){
            GameObject child = new GameObject();
            child.AddComponent<Text>();

            string s = "";
            if(gl.checkTotalFlag()){
                foreach(int i in l){
                    s = String.Concat(s, 50 - GoalController.Instance.complexCounts[(string) gl.name.Substring(0, 3) + AcedemicController.Instance.courses[i].courseName],
                         " seats remaining for ");
                    Debug.Log("Adding complex corse course");
                    s = String.Concat(s, AcedemicController.Instance.courses[i].courseName,"\n");
                }
            }
            else{
               s = String.Concat(gl.getComplexSeatGoals()[count], " seats remaining for either ");
                foreach(int i in l){
                    Debug.Log("Adding complex corse course");
                    s = String.Concat(s, AcedemicController.Instance.courses[i].courseName, " or " );
                }
                char[] c = {'o',' ', 'r'};
                s = s.TrimEnd(c);
            }

            child.GetComponent<Text>().text = s;
            child.GetComponent<Text>().font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            child.GetComponent<Text>().color = Color.black;
            child.layer = 5;
            child.transform.SetParent(GoalDescList.transform, false);
            count++;
        }
     }

    //hides goal panel and clear children
    public void UnHover(){
        if(GoalDescList.activeSelf == false)
            return;
        foreach (Transform child in GoalDescList.transform) {
            GameObject.Destroy(child.gameObject);
        }
        GoalDescPanel.SetActive(false);
    }
}