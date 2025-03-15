using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolTipTrigger : MonoBehaviour{
    public GameObject ToolTipPanel;
    public bool hoverCheck = false;

    //hover to start timer that activate tool tip panel
    public void Hover(string label){
        hoverCheck = true;
        StartCoroutine(DelayAction(1.5f, label));
    }

    //timer to show
    IEnumerator DelayAction(float delayTime, string label){   
        //Wait for the specified delay time before continuing.
        yield return new WaitForSeconds(delayTime);
 
        //Do the action after the delay time has finished.
        if(hoverCheck != false){
            ToolTipPanel.SetActive(true);
            switch(label){
                case "studentsAvailable":
                    Transform go = GameObject.Find("studentsAvailable").transform;
                    string s = "Number of available students\n" + go.GetChild(4).GetComponent<Text>().text + " Great, "
                        + go.GetChild(2).GetComponent<Text>().text + " Good, and " + go.GetChild(0).GetComponent<Text>().text 
                        + " ok local students available\n" + go.GetChild(9).GetComponent<Text>().text + " Great, "
                        + go.GetChild(7).GetComponent<Text>().text + " Good, and " + go.GetChild(5).GetComponent<Text>().text 
                        + " ok students available from dorms\n";
                    ToolTipPanel.transform.GetChild(0).GetComponent<Text>().text = s;
                    setPosition(go.gameObject, 45);
                    break;
                case "rep":
                    ToolTipPanel.transform.GetChild(0).GetComponent<Text>().text = "Current Reputation of the school, which determines" 
                        + " the number/quality of students/professors";
                    setPosition(GameObject.Find("Rep"), 0.0f, - 45.0f * UIController.Instance.settings.getScale());
                    break;
                case "guide":
                    ToolTipPanel.transform.GetChild(0).GetComponent<Text>().text = "Course guide for the different majors that details the various classes needed for each semester";
                    setPosition(45, 45);
                    break;
            }
        }
    }
    //Set position of tool tip panel with/without attaching to a game object
    public void setPosition(GameObject go, float x = 0.0f, float y = 0.0f){
        ToolTipPanel.transform.SetParent(go.transform);
        
        ToolTipPanel.GetComponent<RectTransform>().anchoredPosition = new Vector3(x, y, 0);
    }
    public void setPosition(float x = 0.0f, float y = 0.0f){
        ToolTipPanel.GetComponent<RectTransform>().anchoredPosition = new Vector3(x, y, 0);
    }
    //Hide the tool tip when you move the curser away from the gameobject
    public void UnHover(){
        hoverCheck = false;
        ToolTipPanel.SetActive(false);
    }
    //diverts clicks to unhover
    public void Click(){
        UnHover();
    }
}