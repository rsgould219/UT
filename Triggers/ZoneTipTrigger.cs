using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoneTipTrigger : MonoBehaviour{
    public GameObject ZoneTipPanel;
    public bool hoverCheck = false;

    public void hoverZone(int zoneType){
        hoverCheck = true;
        StartCoroutine(delayActionZone(1.5f, zoneType));
    }
    //timer to show zone requirements
    IEnumerator delayActionZone(float delayTime, int zoneType){   
        //Wait for the specified delay time before continuing.
        yield return new WaitForSeconds(delayTime);
 
        //Do the action after the delay time has finished.
        if(hoverCheck != false){
            ZoneTipPanel.SetActive(true);
            switch(zoneType){
                //Office
                case 0:
                    ZoneTipPanel.transform.GetChild(0).GetComponent<Text>().text = "Requires one Desk and one Door";
                    break;
                //Class
                case 1:
                    string s = "Requires one Desk, one Blackboard, one Door, and at least one Student Chair";
                    ZoneTipPanel.transform.GetChild(0).GetComponent<Text>().text = s;
                    break;
                //Lab
                case 2:
                    ZoneTipPanel.transform.GetChild(0).GetComponent<Text>().text = "Requires two Desks and one Door";
                    break;
                //Dorm
                case 3:
                    ZoneTipPanel.transform.GetChild(0).GetComponent<Text>().text = "Requires one Dresser, one Bed, and one Door";
                    break;
            }
        }
    }
    //Hide the tool tip when you move the curser away from the gameobject
    public void unHover(){
        hoverCheck = false;
        ZoneTipPanel.SetActive(false);
    }
    //diverts clicks to unhover
    public void Click(){
        unHover();
    }
}