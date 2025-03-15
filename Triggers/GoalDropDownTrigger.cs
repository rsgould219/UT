using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalDropDownTrigger : MonoBehaviour{
    public Text label;
    public bool gpOpened;
    public GameObject GoalDescPanel;
     
     //make desc appear for the course
    public void Hover(){
         if (!label){
             Debug.LogError("please assign both the label and the toggle in the inspector");
             return;
         }
 
         Debug.LogFormat("label: {0}", label.text);

        //Grab the goal with the name of int option
        Goal gl = null;
        foreach(Goal g in GoalController.Instance.goalList){
            if(g.name.Equals(label.text)){
                gl = g;
            }
        }

        //Assign the goal info panal
        if(gl != null){
            GoalDescPanel.SetActive(true);
            GameObject.Find("GoalName").transform.GetComponent<Text>().text = gl.name;
            GameObject.Find("GoalDesc").transform.GetComponent<Text>().text = gl.desc;
            simpleCourseCheck(gl);
            complexCourseCheck(gl);
            extraCheck(gl);
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
            child.GetComponent<Text>().text = String.Concat("Add ",gl.getSeatGoals()[count], 
                " seats to ", AcedemicController.Instance.courses[i].courseName);
            //child.GetComponent<Text>().text = "Test";
            child.GetComponent<Text>().font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            child.GetComponent<Text>().fontSize = (int)(16 * UIController.Instance.settings.getScale());
            child.GetComponent<Text>().color = Color.black;
            child.layer = 5;
            child.transform.SetParent(GameObject.Find("GoalDescList").transform, false);
            count++;
        }
     }

    //makes a text child for each range
    public void rangeCheck(Goal gl){
        foreach(Tuple<int, int, int> t in  gl.getRangeGoals()){
            string s = AcedemicController.Instance.courses[t.Item1].courseName;
            //GameObject.Find("Guide" + semester + "_" + nextLine).GetComponent<Text>().text = s.Substring(0, s.Length - 2) + "XX (Elective)";
        }
    }

    //makes a text child for the complex course
     public void complexCourseCheck(Goal gl){
         int count = 0;
         foreach(List<int> l in gl.getComplexList()){
            GameObject child = new GameObject();
            child.AddComponent<Text>();
            string s = "Add seats in ";
            foreach(int i in l){
                Debug.Log("Adding complex corse course");
                s = String.Concat(s, AcedemicController.Instance.courses[i].courseName, "/" );
            }
            char[] c = {'/'};

            //determines if the 2nd semester check is flaged or no and concats accordingly
            if(gl.checkTotalFlag()){
                s = String.Concat(s.TrimEnd(c), " so that each have ", gl.getComplexSeatGoals()[count]);
            }
            else{
                s = String.Concat(s.TrimEnd(c), " to get a total of ", gl.getComplexSeatGoals()[count]);
            }
            child.GetComponent<Text>().text = s;
            child.GetComponent<Text>().font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            child.GetComponent<Text>().fontSize = (int)(16 * UIController.Instance.settings.getScale());
            child.GetComponent<Text>().color = Color.black;
            child.layer = 5;
            child.transform.SetParent(GameObject.Find("GoalDescList").transform, false);
            count++;
        }
     }

    //makes the child text for course extras
    public void extraCheck(Goal gl){
        foreach(int i in gl.getExtraGoals()){
            int type = 0;
            GameObject child = new GameObject();
            child.AddComponent<Text>();
            string s = "Add seats in ";
            char[] c = {'/'};
            s = String.Concat(s, AcedemicController.Instance.courses[i].courseName, "/" );
            s = String.Concat(s.TrimEnd(c), " ", AcedemicController.Instance.courses[i].seatCount(), "/");

            switch(i){
                case 11:
                    type = 1;
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
                    type = 2;
                    s = String.Concat(s, AcedemicController.Instance.courses[4].seatCount());
                    break;
                case 15:
                    s = String.Concat(s, AcedemicController.Instance.courses[5].seatCount());
                    break;
                case 16:
                    s = String.Concat(s, AcedemicController.Instance.courses[6].seatCount());
                    break;
            }
            //add prefix description
            switch(type){
                case 1:
                    s = String.Concat("Add recitations and lab courses for basic physics and chemistry courses.\n", s);
                    break;
                case 2:
                    s = String.Concat("Add recitations courses for basic math courses.\n", s);
                    break;
            }
            child.GetComponent<Text>().text = s;
            child.GetComponent<Text>().font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            child.GetComponent<Text>().fontSize = (int)(16 * UIController.Instance.settings.getScale());
            child.GetComponent<Text>().color = Color.black;
            child.layer = 5;
            child.transform.SetParent(GameObject.Find("GoalDescList").transform, false);
        }
     }

    //hides goal panel and clear children
    public void UnHover(){
        foreach (Transform child in  GameObject.Find("GoalDescList").transform) {
            GameObject.Destroy(child.gameObject);
        }
        GoalDescPanel.SetActive(false);
    }

    public void Click(){
        GoalController.Instance.setCurGoal(label.text);
        UnHover();
    }
}