using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThemeController : MonoBehaviour{
    public int uiTheme = 1, goalTheme = 1;
    public Color textTheme = new Color(0.196f, 0.196f, 0.196f, 1f);
    public Sprite forGround1, forGroundShort1, background1, forGround2, forGroundShort2, background2;
    public Sprite forGround3, forGroundShort3, background3;
    
    public Sprite forGround, forGroundShort, background;
    public static ThemeController Instance;
    void Start(){
        Instance = this;
    }

    public void setUITheme(int desiredTheme){
        uiTheme = desiredTheme;

        if(desiredTheme == 3)
            textTheme = Color.white;
        else
            textTheme = new Color(0.196f, 0.196f, 0.196f, 1f);

        switch(desiredTheme){
            case 2:
                forGround = forGround2;
                forGroundShort = forGroundShort2;
                background = background2;
                break;
            case 3:
                forGround = forGround3;
                forGroundShort = forGroundShort3;
                background = background3;
                break;
            default:
                forGround = forGround1;
                forGroundShort = forGroundShort1;
                background = background1;
                break;
        }

        setsTextColor();
        
        Transform saveP = UIController.Instance.savePanel.transform;
        saveP.GetComponent<Image>().sprite = background;
        saveP.GetChild(0).GetComponent<Image>().sprite = forGround;
        saveP.GetChild(1).GetChild(2).GetChild(0).GetChild(0).GetComponent<Image>().sprite = forGround;
        saveP.GetChild(1).GetChild(2).GetComponent<Image>().sprite = background;
        saveP.GetChild(2).GetComponent<Image>().sprite = background;
        saveP.GetChild(3).GetComponent<Image>().sprite = forGroundShort;

        GameObject settingsPanel = UIController.Instance.settingsPanel;
        settingsPanel.GetComponent<Image>().sprite = background;
        settingsPanel.transform.GetChild(1).GetComponent<Image>().sprite = forGround;
        settingsPanel.transform.GetChild(1).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().sprite = forGround;
        settingsPanel.transform.GetChild(1).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(2).GetComponent<Text>().color = textTheme;
        settingsPanel.transform.GetChild(3).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(2).GetComponent<Text>().color = textTheme;
        settingsPanel.transform.GetChild(5).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(2).GetComponent<Text>().color = textTheme;
        settingsPanel.transform.GetChild(3).GetComponent<Image>().sprite = forGround;
        settingsPanel.transform.GetChild(3).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().sprite = forGround;
        settingsPanel.transform.GetChild(5).GetComponent<Image>().sprite = forGround;
        settingsPanel.transform.GetChild(5).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().sprite = forGround;
        settingsPanel.transform.GetChild(8).GetComponent<Image>().sprite = forGround;
        settingsPanel.transform.GetChild(7).GetComponent<Image>().sprite = forGround;
        settingsPanel.transform.GetChild(6).GetChild(0).GetComponent<Image>().sprite = forGround;
        settingsPanel.transform.GetChild(6).GetChild(1).GetComponent<Image>().sprite = forGround;
        settingsPanel.transform.GetChild(6).GetChild(2).GetComponent<Image>().sprite = forGround;
        settingsPanel.transform.GetChild(9).GetComponent<Image>().sprite = forGround;

        foreach(Transform child in GameObject.Find("BuildMenu").transform)
            child.GetComponent<Image>().sprite = forGround;

        foreach(Transform child in UIController.Instance.buildMenu.transform)
            child.GetComponent<Image>().sprite = forGround;
        foreach(Transform child in UIController.Instance.buyMenu.transform)
            child.GetComponent<Image>().sprite = forGround;
        foreach(Transform child in UIController.Instance.zoneMenu.transform)
            child.GetComponent<Image>().sprite = forGround;
        foreach(Transform child in UIController.Instance.charMenu.transform)
            child.GetComponent<Image>().sprite = forGround;
        
        GameObject paintMenu = UIController.Instance.paintMenu;
        paintMenu.transform.GetChild(0).GetComponent<Image>().sprite = background;
        paintMenu.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().sprite = forGround;
        paintMenu.transform.GetChild(1).GetComponent<Image>().sprite = forGroundShort;
        paintMenu.transform.GetChild(2).GetComponent<Image>().sprite = background;
        paintMenu.transform.GetChild(2).GetChild(0).GetChild(0).GetComponent<Image>().sprite = forGround;
        paintMenu.transform.GetChild(3).GetComponent<Image>().sprite = forGroundShort;
        paintMenu.transform.GetChild(4).GetComponent<Image>().sprite = background;
        paintMenu.transform.GetChild(4).GetChild(0).GetChild(0).GetComponent<Image>().sprite = forGround;
        paintMenu.transform.GetChild(5).GetComponent<Image>().sprite = forGroundShort;
        paintMenu.transform.GetChild(7).GetComponent<Image>().sprite = forGround;

        Transform bm = UIController.Instance.mainMenuPanel.transform;
        bm.GetChild(0).GetChild(0).GetComponent<Image>().sprite = forGround;
        bm.GetChild(0).GetChild(1).GetComponent<Image>().sprite = forGroundShort;
        bm.GetChild(1).GetComponent<Image>().sprite = forGround;
        bm.GetChild(2).GetComponent<Image>().sprite = forGround;
        bm.GetChild(3).GetComponent<Image>().sprite = forGround;
        bm.GetChild(4).GetComponent<Image>().sprite = forGround;
        bm.GetChild(5).GetComponent<Image>().sprite = forGround;

        setTimeMenu(forGround, forGroundShort, background);
        setCourseMenu(forGround, forGroundShort, background);
        setStudentPanel(forGround, forGroundShort, background);
        setSectionMenu(forGround, forGroundShort, background);
        setGuidePanel(forGround, forGroundShort, background);

        GameObject bp = UIController.Instance.buildingPanel;
        bp.transform.GetComponent<Image>().sprite = background;
        bp.transform.GetChild(0).GetComponent<Image>().sprite = forGround;
        Transform sc = UIController.Instance.saveConfirmPanel.transform;
        sc.GetComponent<Image>().sprite = background;
        sc.GetChild(0).GetComponent<Image>().sprite = forGround;
        sc.GetChild(1).GetComponent<Image>().sprite = forGround;
        sc.GetChild(2).GetComponent<Image>().sprite = forGround;
        Transform ec = UIController.Instance.exitConfirmPanel.transform;
        ec.GetComponent<Image>().sprite = background;
        ec.GetChild(0).GetComponent<Image>().sprite = forGround;
        ec.GetChild(1).GetComponent<Image>().sprite = forGround;
        ec.GetChild(2).GetComponent<Image>().sprite = forGround;
        

        GameObject profHirePanel = UIController.Instance.profHirePanel;
        profHirePanel.transform.GetChild(0).GetComponent<Image>().sprite = forGroundShort;
        profHirePanel.transform.GetChild(1).GetComponent<Image>().sprite = forGroundShort;
        profHirePanel.transform.GetChild(2).GetComponent<Image>().sprite = forGround;
        profHirePanel.transform.GetChild(3).GetComponent<Image>().sprite = forGround;
        profHirePanel.transform.GetChild(3).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().sprite = forGround;
        profHirePanel.transform.GetChild(3).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(2).GetComponent<Text>().color = textTheme;
        profHirePanel.transform.GetChild(3).GetChild(2).GetChild(1).GetComponent<Image>().sprite = background;
        profHirePanel.transform.GetChild(3).GetChild(2).GetChild(1).GetChild(0).GetChild(0).GetComponent<Image>().sprite = forGround;
        profHirePanel.transform.GetChild(9).GetComponent<Image>().sprite = background;
        profHirePanel.transform.GetChild(9).GetChild(1).GetComponent<Image>().sprite = background;
        profHirePanel.transform.GetChild(9).GetChild(1).GetChild(0).GetChild(0).GetComponent<Image>().sprite = forGround;
        Transform ap = UIController.Instance.awardPanel.transform;
        if(uiTheme == 1){
            ap.transform.GetComponent<Image>().sprite = null;
            ap.GetComponent<Image>().color = new Color(0.0f, 0.635f, 0.936f, 1f);
            profHirePanel.transform.GetComponent<Image>().sprite = null;
            profHirePanel.GetComponent<Image>().color = new Color(0.0f, 0.635f, 0.936f, 1f);
        }
        else{
            ap.GetComponent<Image>().sprite = background2;
            profHirePanel.GetComponent<Image>().sprite = background2;
        }
        ap.GetChild(0).GetChild(0).GetComponent<Image>().sprite = forGround;
    }
    public void setsTextColor(){
        UIController.Instance.cycleGOsOn();

        foreach(Text go in FindObjectsOfType<Text>()){
                go.color = textTheme;
        }
        
        foreach(Transform child in UIController.Instance.savePanel.transform.GetChild(1).GetChild(0).GetChild(0)){
            //child.GetComponent<Image>().sprite = forGround;
            child.GetChild(0).GetComponent<Text>().color = textTheme;
        }

        UIController.Instance.cycleGOsOff();
    }
    public void setGuidePanel(Sprite forGround, Sprite forGroundShort, Sprite background){
        GameObject guidePanel = UIController.Instance.guidePanel;
        guidePanel.transform.GetChild(0).GetComponent<Image>().sprite = background;
        guidePanel.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = forGround;
        guidePanel.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().sprite = forGround;
        guidePanel.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(1).GetComponent<Image>().sprite = background;
        guidePanel.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(1).GetChild(0).GetChild(0).GetComponent<Image>().sprite = forGround;
        guidePanel.transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite = forGround;
        guidePanel.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<Image>().sprite = background;
        guidePanel.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().sprite = forGround;
        guidePanel.transform.GetChild(1).GetChild(0).GetChild(1).GetComponent<Image>().sprite = background;
        guidePanel.transform.GetChild(1).GetChild(0).GetChild(1).GetChild(0).GetComponent<Image>().sprite = forGround;
        guidePanel.transform.GetChild(1).GetChild(0).GetChild(2).GetComponent<Image>().sprite = background;
        guidePanel.transform.GetChild(1).GetChild(0).GetChild(2).GetChild(0).GetComponent<Image>().sprite = forGround;
        guidePanel.transform.GetChild(1).GetChild(0).GetChild(3).GetComponent<Image>().sprite = background;
        guidePanel.transform.GetChild(1).GetChild(0).GetChild(3).GetChild(0).GetComponent<Image>().sprite = forGround;
        guidePanel.transform.GetChild(1).GetChild(1).GetChild(0).GetComponent<Image>().sprite = background;
        guidePanel.transform.GetChild(1).GetChild(1).GetChild(0).GetChild(0).GetComponent<Image>().sprite = forGround;
        guidePanel.transform.GetChild(1).GetChild(1).GetChild(1).GetComponent<Image>().sprite = background;
        guidePanel.transform.GetChild(1).GetChild(1).GetChild(1).GetChild(0).GetComponent<Image>().sprite = forGround;
        guidePanel.transform.GetChild(1).GetChild(1).GetChild(2).GetComponent<Image>().sprite = background;
        guidePanel.transform.GetChild(1).GetChild(1).GetChild(2).GetChild(0).GetComponent<Image>().sprite = forGround;
        guidePanel.transform.GetChild(1).GetChild(1).GetChild(3).GetComponent<Image>().sprite = background;
        guidePanel.transform.GetChild(1).GetChild(1).GetChild(3).GetChild(0).GetComponent<Image>().sprite = forGround;
    }
    
    public void setGoalDescPanel(){
        if(uiTheme == 2){
            GameObject.Find("GoalDescPanel").transform.GetComponent<Image>().color = Color.white;
            GameObject.Find("GoalDescPanel").GetComponent<Image>().sprite = background2;
        }
        else if(uiTheme == 3){
            GameObject.Find("GoalDescPanel").transform.GetComponent<Image>().color = Color.white;
            GameObject.Find("GoalDescPanel").GetComponent<Image>().sprite = background3;
        }
        else{
            GameObject.Find("GoalDescPanel").transform.GetComponent<Image>().sprite = null;
            GameObject.Find("GoalDescPanel").transform.GetComponent<Image>().color = new Color(0, 162, 232, 225);
        }
        goalTheme = uiTheme;
    }
    public void setSectionMenu(Sprite forGround, Sprite forGroundShort, Sprite background){
        GameObject sectionMenu = UIController.Instance.sectionMenu;
        if(uiTheme != 1)
            sectionMenu.transform.GetComponent<Image>().sprite = background;
        else{
            sectionMenu.transform.GetComponent<Image>().sprite = null;
            sectionMenu.transform.GetComponent<Image>().color = new Color(0, 162, 232, 152);
        }
        sectionMenu.transform.GetChild(0).GetComponent<Image>().sprite = forGround;
        sectionMenu.transform.GetChild(1).GetComponent<Image>().sprite = forGround;
        sectionMenu.transform.GetChild(1).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().sprite = forGround;
        sectionMenu.transform.GetChild(1).GetChild(2).GetChild(1).GetComponent<Image>().sprite = background;
        sectionMenu.transform.GetChild(1).GetChild(2).GetChild(1).GetChild(0).GetChild(0).GetComponent<Image>().sprite = forGround;
        sectionMenu.transform.GetChild(1).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(2).GetComponent<Text>().color = textTheme;
        sectionMenu.transform.GetChild(2).GetComponent<Image>().sprite = forGround;
        sectionMenu.transform.GetChild(3).GetComponent<Image>().sprite = forGroundShort;
        sectionMenu.transform.GetChild(4).GetComponent<Image>().sprite = background;
        sectionMenu.transform.GetChild(4).GetChild(1).GetComponent<Image>().sprite = background;
        sectionMenu.transform.GetChild(4).GetChild(1).GetChild(0).GetChild(0).GetComponent<Image>().sprite = forGround;
    }
    public void setStudentPanel(Sprite forGround, Sprite forGroundShort, Sprite background){
        GameObject studentPanel = UIController.Instance.studentPanel;
        studentPanel.transform.GetChild(0).GetComponent<Image>().sprite = forGround;
        studentPanel.transform.GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().sprite = forGround;
        studentPanel.transform.GetChild(0).GetChild(2).GetChild(1).GetComponent<Image>().sprite = background;
        studentPanel.transform.GetChild(0).GetChild(2).GetChild(1).GetChild(0).GetChild(0).GetComponent<Image>().sprite = forGround;
        studentPanel.transform.GetChild(1).GetComponent<Image>().sprite = forGround;
        studentPanel.transform.GetChild(2).GetComponent<Image>().sprite = forGround;
        studentPanel.transform.GetChild(3).GetComponent<Image>().sprite = forGround;
        studentPanel.transform.GetChild(4).GetComponent<Image>().sprite = forGround;
        studentPanel.transform.GetChild(5).GetComponent<Image>().sprite = forGround;
        studentPanel.transform.GetChild(10).GetComponent<Image>().sprite = forGround;
        studentPanel.transform.GetChild(11).GetComponent<Image>().sprite = forGround;
        studentPanel.transform.GetChild(12).GetComponent<Image>().sprite = forGround;
        studentPanel.transform.GetChild(13).GetComponent<Image>().sprite = forGround;
        studentPanel.transform.GetChild(14).GetComponent<Image>().sprite = forGround;
        studentPanel.transform.GetChild(15).GetComponent<Image>().sprite = forGround;
        studentPanel.transform.GetChild(16).GetComponent<Image>().sprite = forGround;
        studentPanel.transform.GetChild(18).GetComponent<Image>().sprite = forGround;
        studentPanel.transform.GetChild(19).GetComponent<Image>().sprite = forGround;
        studentPanel.transform.GetChild(20).GetComponent<Image>().sprite = forGround;
        studentPanel.transform.GetChild(21).GetComponent<Image>().sprite = forGround;
        studentPanel.transform.GetChild(22).GetComponent<Image>().sprite = forGround;
        studentPanel.transform.GetChild(23).GetComponent<Image>().sprite = forGround;
        studentPanel.transform.GetChild(24).GetComponent<Image>().sprite = forGround;
        studentPanel.transform.GetChild(25).GetComponent<Image>().sprite = forGround;
        studentPanel.transform.GetChild(26).GetComponent<Image>().sprite = forGround;
        studentPanel.transform.GetChild(27).GetComponent<Image>().sprite = forGround;
        studentPanel.transform.GetChild(28).GetComponent<Image>().sprite = forGround;
        studentPanel.transform.GetChild(29).GetComponent<Image>().sprite = forGround;
        studentPanel.transform.GetChild(30).GetComponent<Image>().sprite = forGround;
        studentPanel.transform.GetChild(31).GetComponent<Image>().sprite = forGround;
        studentPanel.transform.GetChild(32).GetComponent<Image>().sprite = forGround;
        studentPanel.transform.GetChild(33).GetComponent<Image>().sprite = forGround;
        studentPanel.transform.GetChild(34).GetComponent<Image>().sprite = forGround;
    }
    public void setCourseMenu(Sprite forGround, Sprite forGroundShort, Sprite background){
        GameObject coursePanel = UIController.Instance.coursePanel;
        if(uiTheme != 1)
            coursePanel.transform.GetComponent<Image>().sprite = background;
        else{
            coursePanel.transform.GetComponent<Image>().sprite = null;
            coursePanel.transform.GetComponent<Image>().color = new Color(0, 162, 232, 152);
        }
        coursePanel.transform.GetChild(0).GetComponent<Image>().sprite = background;
        coursePanel.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = forGround;
        coursePanel.transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite = forGround;
        coursePanel.transform.GetChild(1).GetComponent<Image>().sprite = forGround;
        coursePanel.transform.GetChild(1).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().sprite = forGround;
        coursePanel.transform.GetChild(1).GetChild(2).GetChild(1).GetComponent<Image>().sprite = background;
        coursePanel.transform.GetChild(1).GetChild(2).GetChild(1).GetChild(0).GetChild(0).GetComponent<Image>().sprite = forGround;
        coursePanel.transform.GetChild(2).GetComponent<Image>().sprite = forGround;
        coursePanel.transform.GetChild(2).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().sprite = forGround;
        coursePanel.transform.GetChild(2).GetChild(2).GetChild(1).GetComponent<Image>().sprite = background;
        coursePanel.transform.GetChild(2).GetChild(2).GetChild(1).GetChild(0).GetChild(0).GetComponent<Image>().sprite = forGround;
        coursePanel.transform.GetChild(3).GetComponent<Image>().sprite = forGround;
        coursePanel.transform.GetChild(1).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(2).GetComponent<Text>().color = textTheme;
        coursePanel.transform.GetChild(2).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(2).GetComponent<Text>().color = textTheme;
        int i = 0;
        foreach(Transform child in coursePanel.transform){
            if(i < 4)
                i++;
            else{
                child.GetChild(0).GetComponent<Image>().sprite = forGround;
                child.GetChild(1).GetComponent<Image>().sprite = forGroundShort;
            }
        }
    }

    public void setTimeMenu(Sprite forGround, Sprite forGroundShort, Sprite background){
        Transform tm = GameObject.Find("TimeMenu").transform;
        tm.GetChild(0).GetComponent<Image>().sprite = forGround;
        tm.GetComponent<Image>().sprite = background;
        tm.GetChild(1).GetChild(0).GetComponent<Image>().sprite = forGround;
        tm.GetChild(1).GetChild(1).GetComponent<Image>().sprite = forGroundShort;
        tm.GetChild(1).GetChild(2).GetComponent<Image>().sprite = forGroundShort;
        tm.GetChild(1).GetChild(3).GetComponent<Image>().sprite = forGround;
        tm.GetChild(2).GetChild(0).GetComponent<Image>().sprite = forGroundShort;
        tm.GetChild(2).GetChild(1).GetComponent<Image>().sprite = forGroundShort;
        tm.GetChild(2).GetChild(2).GetComponent<Image>().sprite = forGroundShort;
        tm.GetChild(3).GetComponent<Image>().sprite = forGround;
        tm.GetChild(4).GetComponent<Image>().sprite = forGround;
        tm.GetChild(5).GetComponent<Image>().sprite = forGround;
        tm.GetChild(5).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().sprite = forGround;
        tm.GetChild(5).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(2).GetComponent<Text>().color = textTheme;
        tm.GetChild(5).GetChild(2).GetChild(1).GetComponent<Image>().sprite = background;
        tm.GetChild(5).GetChild(2).GetChild(1).GetChild(0).GetChild(0).GetComponent<Image>().sprite = forGround;
        tm.GetChild(6).GetComponent<Image>().sprite = forGround;
        tm.GetChild(7).GetComponent<Image>().sprite = forGround;
        tm.GetChild(8).GetComponent<Image>().sprite = forGround;
    }
}