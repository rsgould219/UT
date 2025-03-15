using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Xml.Serialization;
using System.Text;
using System.Linq;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using System.Data;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO.Compression;
using System.Reflection;

public class TutorialController : MonoBehaviour{
    public static TutorialController Instance;
    public int tutorialProg = 0;
    public bool tutorialActive = true;
    public GameObject tutorialPanel;
    List<string> tutList = new List<string>();
    void Start() {
        Instance = this;
       
       //load list of tutoral text from external xml file into list tutList
        var listRoot = XDocument.Load("TutText.xml");
        var listItems = listRoot.Root.Elements("List").Select(e => e.Attribute("t")).ToList();

        foreach (string s in listItems){
            tutList.Add(s);
            Debug.Log(s);
        }
        tutorialPanel.transform.GetChild(0).GetComponent<Text>().text = tutList[0];
    }
    //Spawn or despawn tutorial menu
    public void spawnTutorialMenu(){
        if(tutorialActive){
            tutorialPanel.gameObject.SetActive(false);
            tutorialActive = false;
        }
        else{
            tutorialPanel.gameObject.SetActive(true);
            tutorialActive = true;
            /*if(ecOpened != true)
                setExitConfirmScale();*/
       }
    }
    //move tutiral to the next step, or close tutorial if the last text blerb is present (aka tutorial is over)
    public void nextTutorial(){
        if(tutorialProg == tutList.Count - 1)
            spawnTutorialMenu();
        else{
            tutorialPanel.transform.GetChild(0).GetComponent<Text>().text = tutList[tutorialProg];
            tutorialProg++;
        }
    }
}