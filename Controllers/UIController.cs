using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour{
    const int repForGreatStud = 100;
    bool zoneMenuActive, buildMenuActive, buyMenuActive, paintMenuActive = false;
    bool mainMenuActive = false, settingsActive = false, savePanelActive = false;
    public bool sectionMenuActive, coursePanelActive, perfPanelActive = false;
    bool charMenuActive, studentPanelActive, profHirePanelActive = false;
    bool guidePanelActive, financePanelActive = false;
    bool buildingPanelActive = false, profListActive = false, ProfAchiveActive = false;
    bool buildingConfirmPanelActive, loadConfirmPanelActive, saveConfirmPanelActive, exitConfirmPanelActive = false;
    bool smOpened = false, phOpened = false, stpOpened = false, mpOpened = false;
    bool bcOpened = false, lcOpened = false, scOpened = false, ecOpened = false;
    bool cpOpened = false, bpOpened = false, ppOpened = false, mmOpened = false, sepOpened = false;
    bool plOpened = false, gpOpened = false;
    int barCount = 0;
    int money = 100000;
    public int studBuf = 0;
    public Sprite buttonPressed, buttonPressed2, buttonUnpressed, fireButton, unfireButton;

    public static UIController Instance;
    public GameObject buildMenu, buyMenu, zoneMenu, paintMenu;
    public GameObject sectionMenu;
    //CourseText is the prefab for a line of professor info in the hiring menu
    public GameObject CourseText;
    //ProfSlice is the prefab for prof info when firing a professor
    public GameObject ProfSlice, profAchiveSlice;
    public GameObject ZoneMarker;
    public GameObject charMenu;
    public GameObject profList, profHirePanel, awardPanel, achiveList;
    public GameObject studentPanel, coursePanel;
    public GameObject perfPanel;
    public GameObject guidePanel, financePanel;
    public GameObject buildingPanel, profListPanel;
    public GameObject markerHolder;
    public GameObject buildingConfirmPanel, loadConfirmPanel, saveConfirmPanel, exitConfirmPanel;
    public GameObject CoursesList;
    public GameObject repIndicate;
    public GameObject mainMenuPanel, settingsPanel, savePanel, loadPanel;

    public GameObject freshPerf, softPerf, juniorPerf, seniorPerf, gradPerf;
    public GameObject FreshmenCount, SofCount, JuniorCount, SeniorCount, GradCount;
    public GameObject ageText, skillText, salaryText;

    public GameObject studentAvailable;

    public GameObject infoBar;
    public GameObject infoBox;
    public GameObject moneyBar;

    private Character curChar;
    public Settings settings;
    public Tuple<int,int> oldFinanceInfo = Tuple.Create(0,0);
    public List<Tuple<int, int>> studTypeList = new List<Tuple<int, int>>();

    // Start is called before the first frame update
    void Start(){
        //Load game settings on main scene startup
        Instance = this;
        XmlSerializer serializer = new XmlSerializer(typeof(Settings));
		//TextReader reader = new StringReader(PlayerPrefs.GetString("SaveGame"));
        TextReader reader = new StringReader(System.IO.File.ReadAllText("Settings.xml"));
		settings = (Settings)serializer.Deserialize(reader);
        Debug.Log("Setting res at:" + settings.getWidth() + "x" + settings.getHeight());
		reader.Close();
        updateUIScale(settings.getWidth(), settings.getHeight(), settings.getScale());
    }

    // Update is called once per frame
    void Update(){
        if(barCount > 1)
                barCount--;
        else if(barCount != 0)
            resetInfoBar();
    }
    //Activates the build menu
    public void spawnBuildMenu(){
       if(buildMenuActive){
           buildMenu.gameObject.SetActive(false);
           buildMenuActive = false;
       }
       else{
           buildMenu.gameObject.SetActive(true);
           buildMenuActive = true;
           if(mpOpened != true)
                setMenuPanels();
       }
    }
    //Activates the build menu
    public void spawnPaintMenu(){
       if(paintMenuActive){
           paintMenu.gameObject.SetActive(false);
           paintMenuActive = false;
       }
       else{
           paintMenu.gameObject.SetActive(true);
           paintMenuActive = true;
           MouseController.Instance.SetMouseModeAsPaint();
           if(mpOpened != true)
                setMenuPanels();
       }
    }
    //activates the buy menu
    public void spawnBuyMenu(){
       if(buyMenuActive){
           buyMenu.gameObject.SetActive(false);
           buyMenuActive = false;
       }
       else{
           buyMenu.gameObject.SetActive(true);
           buyMenuActive = true;
           if(mpOpened != true)
                setMenuPanels();
       }
    }
    //activates the zone menu
    public void spawnZoneMenu(){
       if(zoneMenuActive){
            zoneMenu.gameObject.SetActive(false);
            zoneMenuActive = false;
       }
       else{
            zoneMenu.gameObject.SetActive(true);
            zoneMenuActive = true;
            if(mpOpened != true)
                setMenuPanels();
       }
    }
    //activates menu for managing/creating sections
    public void spawnSectionMenu(){
         if(sectionMenuActive){
            sectionMenu.gameObject.SetActive(false);
            sectionMenuActive = false;
       }
       else{
            sectionMenu.gameObject.SetActive(true);
            sectionMenuActive = true;
            TimeSlotController.Instance.resetTimeSlots();
            profHirePanelActive = false;
            profHirePanel.gameObject.SetActive(false);
            CourseUIController.Instance.setInitProfConflict();
            //set scale if first opening
            if(smOpened != true)
                setSectionMenuScale();
       }
    }
    //activates menu for quiting/loading/saving
    public void spawnMainMenu(){
         if(mainMenuActive){
            mainMenuPanel.gameObject.SetActive(false);
            mainMenuActive = false;
       }
       else{
            mainMenuPanel.gameObject.SetActive(true);
            mainMenuActive = true;
            //set scale if first opening
            if(mmOpened != true)
                setMainMenuScale();
       }
    }
    //spawning menus
    public void spawnSectionMenu(string flag){
        if(sectionMenuActive == false){
            spawnSectionMenu();
        }
    }

    public void spawnCourseMenu(){
        if(coursePanelActive){
           coursePanel.gameObject.SetActive(false);
           coursePanelActive = false;
           sectionMenu.gameObject.SetActive(false);
           sectionMenuActive = false;
        }
        else{
            if(profListActive != false){
                profListPanel.gameObject.SetActive(false);
                profListActive = false;
            }
            if(buildingPanelActive != false){
                buildingPanel.gameObject.SetActive(false);
                buildingPanelActive = false;
            }
            coursePanel.gameObject.SetActive(true);
            coursePanelActive = true;
            CourseUIController.Instance.onDepartmentChange();
            if(cpOpened != true)
                setCoursesPanel();
       }
    }

    public void spawnCharacterMenu(){
        if(charMenuActive){
            charMenu.gameObject.SetActive(false);
            charMenuActive = false;
        }
        else{
            charMenu.gameObject.SetActive(true);
            charMenuActive = true;
            if(mpOpened != true)
                setMenuPanels();
       }
    }

public void spawnSavePanel(bool saveMode){
        if(savePanelActive){
            savePanel.gameObject.SetActive(false);
            savePanelActive = false;
        }
        else{
            savePanel.gameObject.SetActive(true);
            //pass if the save mode is save(true) or not
            SaveController.Instance.modeSave = saveMode;
            savePanelActive = true;
            SaveController.Instance.refreshSaves();
            /*if(spOpened != true)
                setMenuPanels();*/
            savePanel.transform.GetChild(1).GetComponent<ScrollRect>().normalizedPosition = new Vector2(0, 1);
            //savePanel.transform.GetChild(1).gameObject.SetActive(true);
       }
    }

    public void spawnProfHireMenu(){
        if(profHirePanelActive){
            profHirePanel.gameObject.SetActive(false);
            profHirePanelActive = false;
        }
        else{
            profHirePanel.gameObject.SetActive(true);
            profHirePanelActive = true;
            sectionMenu.gameObject.SetActive(false);
            sectionMenuActive = false;
            studentPanel.gameObject.SetActive(false);
            studentPanelActive = false;
            refreshProf();
            //set scale if first opening
            if(phOpened != true)
                setProfHireScale();
       }
    }

    public void spawnStudentMenu(){
        if(studentPanelActive){
            studentPanel.gameObject.SetActive(false);
            studentPanelActive = false;
            sectionMenu.gameObject.SetActive(false);
            sectionMenuActive = false;
        }
        else{
            studentPanel.gameObject.SetActive(true);
            studentPanelActive = true;
            profHirePanel.gameObject.SetActive(false);
            profHirePanelActive = false;
            if(stpOpened != true)
                setStudPanelScale();
       }
    }

    public void spawnGuideMenu(){
        if(guidePanelActive){
            guidePanel.gameObject.SetActive(false);
            guidePanelActive = false;
        }
        else{
            guidePanel.gameObject.SetActive(true);
            guidePanelActive = true;
            if(gpOpened != true)
                setGuidePanelScale();
       }
    }
    public void spawnBuildingMenu(){
        if(buildingPanelActive){
            buildingPanel.gameObject.SetActive(false);
            buildingPanelActive = false;
        }
        else{
            if(coursePanelActive != false){
                coursePanelActive = false;
                coursePanel.gameObject.SetActive(false);
            }
            if(profListActive != false){
                profListPanel.gameObject.SetActive(false);
                profListActive = false;
            }
            buildingPanel.gameObject.SetActive(true);
            buildingPanelActive = true;
            if(buildingConfirmPanel.transform.GetChild(1).name != "Building 1")
                   buildingConfirmPanel.transform.GetChild(1).name =  "Building 1";
            if(bpOpened != true)
                setBuildingPanelScale();
       }
    }
    public void spawnBuildingConfirmMenu(){
        if(buildingConfirmPanelActive){
            buildingConfirmPanel.gameObject.SetActive(false);
            buildingConfirmPanelActive = false;
        }
        else{
            buildingConfirmPanel.gameObject.SetActive(true);
            buildingConfirmPanelActive = true;
            if(bcOpened != true)
                setBuildingConfirmScale();
       }
    }

    public void spawnLoadConfirmMenu(){
        if(loadConfirmPanelActive){
            loadConfirmPanel.gameObject.SetActive(false);
            loadConfirmPanelActive = false;
        }
        else{
            loadConfirmPanel.gameObject.SetActive(true);
            loadConfirmPanelActive = true;
            if(lcOpened != true)
                setLoadConfirmScale();
       }
    }
    public void spawnSaveConfirmMenu(){
        if(saveConfirmPanelActive){
            saveConfirmPanel.gameObject.SetActive(false);
            saveConfirmPanelActive = false;
        }
        else{
            saveConfirmPanel.gameObject.SetActive(true);
            saveConfirmPanelActive = true;
            if(scOpened != true)
                setLoadConfirmScale();
       }
    }
    public void spawnExitConfirmMenu(){
        if(exitConfirmPanelActive){
            exitConfirmPanel.gameObject.SetActive(false);
            exitConfirmPanelActive = false;
        }
        else{
            exitConfirmPanel.gameObject.SetActive(true);
            exitConfirmPanelActive = true;
            if(ecOpened != true)
                setExitConfirmScale();
       }
    }
    //Open the list of professors and closes section and course menus if open
    public void spawnProfListMenu(){
        if(profListActive){
            profListPanel.gameObject.SetActive(false);
            profListActive = false;
        }
        else{
            if(sectionMenuActive != false){
                sectionMenu.gameObject.SetActive(false);
                sectionMenuActive = false;
            }
            if(coursePanelActive != false){
                coursePanel.gameObject.SetActive(false);
                coursePanelActive = false;
            }
            profListPanel.gameObject.SetActive(true);
            profListActive = true;
            if(plOpened != true)
                setProfListScale();
            setProfList();
       }
    }
    //opens the settings menu
    public void openSettings(){
        if(settingsActive == false){
            settingsActive = true;
            settingsPanel.gameObject.SetActive(true);
            if(sepOpened != true)
                setSettingsPanelScale(); 
        }
    }
    //closes the settings menu
    public void closeSettings(){
        settingsActive = false;
        settingsPanel.gameObject.SetActive(false);
    }
    public void openFinance(){
        financePanelActive = true;
        financePanel.gameObject.SetActive(true);
        WorldController.Instance.refreshFinance();
    }
    public void closeFinance(){
        financePanelActive = false;
        financePanel.gameObject.SetActive(false);
    }
    //updates finacne info and recalcuates total
    public void updateFinance(Tuple<int, int> charInfo){
        Debug.Log("Updating finance info");
        financePanel.transform.GetChild(6).GetComponent<Text>().text = oldFinanceInfo.Item1.ToString();
        financePanel.transform.GetChild(7).GetComponent<Text>().text = oldFinanceInfo.Item2.ToString();
        financePanel.transform.GetChild(8).GetComponent<Text>().text = (oldFinanceInfo.Item1 - oldFinanceInfo.Item2).ToString();
        financePanel.transform.GetChild(9).GetComponent<Text>().text = charInfo.Item1.ToString();
        financePanel.transform.GetChild(10).GetComponent<Text>().text = charInfo.Item2.ToString();
        //total, tution - salary
        String total = (charInfo.Item1 - charInfo.Item2).ToString();
        financePanel.transform.GetChild(11).GetComponent<Text>().text = total;
    }
    public void newSemesterFinance(){
        oldFinanceInfo = CharacterController.Instance.calcCharFinance();
    }
    public void openPerfMenu(){
        perfPanel.gameObject.SetActive(true);
        perfPanelActive = true;
        if(ppOpened != null)
            setPerfPanelScale();
        int curMajor = GameObject.Find("PerfDropdown").gameObject.GetComponent<Dropdown>().value;
        string s = "";
        switch(curMajor){
            case 0: //CSE
                s = "CSE";
                break;
            case 1: //MEC
                s = "MEC";
                break;
            case 2: //CHE
                s = "CHE";
                break;
            case 3: //ELE
                s = "ELE";
                break;
            case 4: //CIV
                s = "CIV";
                break;
            case 5: //ISE
                s = "ISE";
                break;
            case 6: //MAT
                s = "MAT";
                break;
            case 7: //PHY
                s = "PHY";
                break;
            case 8: //CHM
                s = "CHM";
                break;
            case 9:
                s = "ENG";
                break;
            case 10:
                s = "HIST";
                break;
            case 11:
                s = "MATH";
                break;
            case 12:
                s = "ECO";
                break;
            case 13:
                s = "MKT";
                break;
            case 14:
                s = "ACT";
                break;
            case 15:
                s = "FIN";
                break;
            case 16:
                s = "BIS";
                break;
            case 17:
                s = "LAW";
                break;
        }
            int i = ScheduleController.Instance.getPerf(s);
            if(i < 1)
                freshPerf.GetComponent<Text>().text = @"N\A";
            else
                freshPerf.GetComponent<Text>().text = i.ToString();
            Debug.Log(s + "Score: " + i);
    }
    //show list of professor achivements
     public void showAchieve(){
        achiveList.transform.parent.gameObject.SetActive(true);
        ProfAchiveActive = true;
    }

    //close UI panels
    public void closeSectionPanel(){
        sectionMenuActive = false;
        sectionMenu.gameObject.SetActive(false);
        BuildingController.Instance.selectedZ = null;
    }

    public void closeProfPanel(){
        profHirePanelActive = false;
        profHirePanel.gameObject.SetActive(false);
    }

    public void closeStudentPanel(){
        studentPanelActive = false;
        studentPanel.gameObject.SetActive(false);
    }

    public void closeGuidePanel(){
        guidePanelActive = false;
        guidePanel.gameObject.SetActive(false);
    }
    public void closePerfMenu(){
        perfPanel.gameObject.SetActive(false);
        perfPanelActive = false;
    }
    public void closeAchive(){
        achiveList.transform.parent.gameObject.SetActive(false);
        foreach(Transform child in achiveList.transform)
            GameObject.Destroy(child.gameObject);
        ProfAchiveActive = false;
    }

    public void loadWorldPress(){

    }

    //grabs a new professor
    public void refreshProf(){
        int count = 0;
        Character ch = CharacterController.Instance.addProfessor(GameObject.Find("ProfDepDropdown").gameObject.GetComponent<Dropdown>().value + 1);
        GameObject.Find("ProfNameText").GetComponent<Text>().text = ch.name;
        //clear course list
        foreach (Transform child in CoursesList.transform)
            GameObject.Destroy(child.gameObject);
        //set course list
        foreach(int i in ch.skilledCourses){
            if(i != 5 && i != 6 && i != 15 && i != 16){
                GameObject go = (GameObject)Instantiate(CourseText);
                go.transform.SetParent(CoursesList.transform, false);
                go.GetComponent<Text>().text = AcedemicController.Instance.courses[i].courseName;
                count++;
                if(i == 4)
                    go.GetComponent<Text>().text = "MATH110/120/130";
                if(i == 14)
                    go.GetComponent<Text>().text = "MATH111/121/131";
                if(ThemeController.Instance.uiTheme == 3)
                    go.GetComponent<Text>().color = Color.white;
                else
                    go.GetComponent<Text>().color = new Color(0.196f, 0.196f, 0.196f, 1f);
            }
        }
        //set list of accomplishments
        int majorAwards = 0;
        int majorNoms = 0;
        int minorAwards = 0;
        int minorNoms = 0;
        int accomplishments = 0;
        
        foreach(int i in ch.getAwards()){
            count++;
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
        awardHelper(majorAwards, majorNoms, minorAwards, minorNoms, accomplishments);
        //Set name/skill/age
        ch.setName("TestProf");
        skillText.GetComponent<Text>().text = CharacterController.Instance.skillBuf.ToString();
        salaryText.GetComponent<Text>().text = ch.getSalary().ToString();
        ageText.GetComponent<Text>().text = CharacterController.Instance.ageBuf.ToString();
        curChar = ch;

        //set list to correct size for scroll view
        RectTransform rt = CoursesList.GetComponent<RectTransform>();
        Debug.Log("New courselist: " + count + " " + count * 25);
        if(count * 25 > 330)
            rt.sizeDelta = new Vector2(rt.sizeDelta.x, count * 25);
        else
            rt.sizeDelta = new Vector2(rt.sizeDelta.x, 330);
    }
    //Helper for displaying awards on the prof hire screen
    public void awardHelper(int majorAwards, int majorNoms, int minorAwards, int minorNoms, int accomplishments){
        if(majorAwards > 0){
            GameObject go = (GameObject)Instantiate(CourseText);
            go.transform.SetParent(CoursesList.transform, false);
            if(majorAwards == 1)
                go.GetComponent<Text>().text = "One Major Award";
            else
                go.GetComponent<Text>().text = majorAwards + " Major Awards";
            setTextColor(go);
                
        }
        if(majorNoms > 0){
            GameObject go = (GameObject)Instantiate(CourseText);
            go.transform.SetParent(CoursesList.transform, false);
            if(majorNoms == 1)
                go.GetComponent<Text>().text = "One Major Award Nomination";
            else
                go.GetComponent<Text>().text = majorNoms + " Major Award Nominations";
            setTextColor(go);
        }
        if(minorAwards > 0){
            GameObject go = (GameObject)Instantiate(CourseText);
            go.transform.SetParent(CoursesList.transform, false);
            if(minorAwards == 1)
                go.GetComponent<Text>().text = "One Minor Award";
            else
                go.GetComponent<Text>().text = minorAwards + " Minor Awards";
            setTextColor(go);
        }
        if(minorNoms > 0){
            GameObject go = (GameObject)Instantiate(CourseText);
            go.transform.SetParent(CoursesList.transform, false);
            if(minorNoms == 1)
                go.GetComponent<Text>().text = "One Minor Award Nomination";
            else
                go.GetComponent<Text>().text = minorNoms + " Minor Award Nominations";
            setTextColor(go);
        }
        if(accomplishments > 0){
            GameObject go = (GameObject)Instantiate(CourseText);
            go.transform.SetParent(CoursesList.transform, false);
            if(accomplishments == 1)
                go.GetComponent<Text>().text = "One Significant Patent, Published Work, or Discovery";
            else
                go.GetComponent<Text>().text = accomplishments + " Significant Patents, Published Works, and/or Discoveries";
            setTextColor(go);
        }
    }
    public void setTextColor(GameObject go){
        if(ThemeController.Instance.uiTheme == 3)
            go.GetComponent<Text>().color = Color.white;
        else 
            go.GetComponent<Text>().color = new Color(0.196f, 0.196f, 0.196f, 1f);
    }

    //Create a char
    public void addChar(string s){
        //check if there are enough offices
        if(WorldController.Instance.world.offices.Count <= CharacterController.Instance.profList.Count){
            Debug.Log("Not enough offices");
            setInfoBar("Not enough offices");
            return;
        }
        CharacterController.Instance.setCharacter(curChar);
        if(curChar.getType() == Character.CharType.Prof){
            CourseUIController.Instance.onDepartmentChange();
        }
    }

    public void spawnStudentRecruitMenu(){
        GameObject.Find("StudentRecuitPanel").gameObject.SetActive(true);
    }

    public void spawnGradRecruitMenu(){
        GameObject.Find("GradRecruitePanel").gameObject.SetActive(true);
    }
    public void  clearStudentCounts(){
        studentAvailable.transform.GetChild(4).GetComponent<Text>().text = "0";
        studentAvailable.transform.GetChild(2).GetComponent<Text>().text = "0";
        studentAvailable.transform.GetChild(0).GetComponent<Text>().text = "0";
        studentAvailable.transform.GetChild(5).GetComponent<Text>().text = "0";
        studentAvailable.transform.GetChild(7).GetComponent<Text>().text = "0";
        studentAvailable.transform.GetChild(9).GetComponent<Text>().text = "0";
        FreshmenCount.GetComponent<Text>().text = "0";
        SofCount.GetComponent<Text>().text = "0";
        JuniorCount.GetComponent<Text>().text= "0";
        SeniorCount.GetComponent<Text>().text = "0";
        GradCount.GetComponent<Text>().text = "0";
    }
    //handle button press to add to an incoming student count
    public void increaseStudentCounts(string s){
        //(4) = great (2) = good (0) = ok
        int studType = 0;
        if(Int32.Parse(studentAvailable.transform.GetChild(4).GetComponent<Text>().text) > 0){
            studType = 1;
            studentAvailable.transform.GetChild(4).GetComponent<Text>().text = (Int32.Parse(studentAvailable.transform.GetChild(4).GetComponent<Text>().text) - 1).ToString();
        }
        else if (Int32.Parse(studentAvailable.transform.GetChild(2).GetComponent<Text>().text) > 0){
            studType = 2;
            studentAvailable.transform.GetChild(2).GetComponent<Text>().text = (Int32.Parse(studentAvailable.transform.GetChild(2).GetComponent<Text>().text) - 1).ToString();
        }
        else if (Int32.Parse(studentAvailable.transform.GetChild(0).GetComponent<Text>().text) > 0){
            studType = 3;
            studentAvailable.transform.GetChild(0).GetComponent<Text>().text = (Int32.Parse(studentAvailable.transform.GetChild(0).GetComponent<Text>().text) - 1).ToString();
        }
        else if(Int32.Parse(studentAvailable.transform.GetChild(9).GetComponent<Text>().text) > 0){
            studType = 11;
            studentAvailable.transform.GetChild(9).GetComponent<Text>().text = (Int32.Parse(studentAvailable.transform.GetChild(9).GetComponent<Text>().text) - 1).ToString();
        }
        else if (Int32.Parse(studentAvailable.transform.GetChild(7).GetComponent<Text>().text) > 0){
            studType = 12;
            studentAvailable.transform.GetChild(7).GetComponent<Text>().text = (Int32.Parse(studentAvailable.transform.GetChild(7).GetComponent<Text>().text) - 1).ToString();
        }
        else if (Int32.Parse(studentAvailable.transform.GetChild(5).GetComponent<Text>().text) > 0){
            studType = 13;
            studentAvailable.transform.GetChild(5).GetComponent<Text>().text = (Int32.Parse(studentAvailable.transform.GetChild(5).GetComponent<Text>().text) - 1).ToString();
        }
        else{
            UIController.Instance.setInfoBar("No more students to add");
            return;
        }
        //parse the string for the number of students
        switch(Int32.Parse(s)){
            case 1:
                FreshmenCount.GetComponent<Text>().text = (Int32.Parse(FreshmenCount.GetComponent<Text>().text) + 1).ToString();
                break;
            case 2:
                SofCount.GetComponent<Text>().text = (Int32.Parse(SofCount.GetComponent<Text>().text) + 1).ToString();
                break;
            case 3:
                JuniorCount.GetComponent<Text>().text = (Int32.Parse(JuniorCount.GetComponent<Text>().text) + 1).ToString();
                break;
            case 4:
                SeniorCount.GetComponent<Text>().text = (Int32.Parse(SeniorCount.GetComponent<Text>().text) + 1).ToString();
                break;
            case 5:
                GradCount.GetComponent<Text>().text = (Int32.Parse(GradCount.GetComponent<Text>().text) + 1).ToString();
                break;
        }
        studTypeList.Add(new Tuple<int, int>(studType, Int32.Parse(s)));
        studBuf++;
    }
    //helpers to change display of available good and great students to recruit
    public void addGoodStudent(){
        studentAvailable.transform.GetChild(2).GetComponent<Text>().text = (Int32.Parse(studentAvailable.transform.GetChild(2).GetComponent<Text>().text) + 1).ToString();
    }
    public void addGreatStudent(){
        studentAvailable.transform.GetChild(4).GetComponent<Text>().text = (Int32.Parse(studentAvailable.transform.GetChild(4).GetComponent<Text>().text) + 1).ToString();
    }
    public void add10(string s){
        increaseStudentCounts(s);
        increaseStudentCounts(s);
        increaseStudentCounts(s);
        increaseStudentCounts(s);
        increaseStudentCounts(s);
        increaseStudentCounts(s);
        increaseStudentCounts(s);
        increaseStudentCounts(s);
        increaseStudentCounts(s);
        increaseStudentCounts(s);
    }
    //handle button press to add to an incoming student count
    public void decreaseStudentCounts(string s){
        if(studTypeList.Count == 0)
            return;
        switch(Int32.Parse(s)){
            case 1:
                if(FreshmenCount.GetComponent<Text>().Equals("0") == false)
                    FreshmenCount.GetComponent<Text>().text = (Int32.Parse(FreshmenCount.GetComponent<Text>().text) - 1).ToString();
                else
                    return;
                break;
            case 2:
                if(SofCount.GetComponent<Text>().Equals("0") == false)
                    SofCount.GetComponent<Text>().text = (Int32.Parse(SofCount.GetComponent<Text>().text) - 1).ToString();
                else
                    return;
                break;
            case 3:
                if(JuniorCount.GetComponent<Text>().Equals("0") == false)
                    JuniorCount.GetComponent<Text>().text = (Int32.Parse(JuniorCount.GetComponent<Text>().text) - 1).ToString();
                else
                    return;
                break;
            case 4:
                if(SeniorCount.GetComponent<Text>().Equals("0") == false)
                    SeniorCount.GetComponent<Text>().text = (Int32.Parse(SeniorCount.GetComponent<Text>().text) - 1).ToString();
                else
                    return;
                break;
            case 5:
                if(GradCount.GetComponent<Text>().Equals("0") == false)
                    GradCount.GetComponent<Text>().text = (Int32.Parse(GradCount.GetComponent<Text>().text) - 1).ToString();
                else
                    return;
                break;
        }
        switch (studTypeList[studTypeList.Count - 1].Item1){
            case 1:
                studentAvailable.transform.GetChild(4).GetComponent<Text>().text = (Int32.Parse(studentAvailable.transform.GetChild(4).GetComponent<Text>().text) + 1).ToString();
                break;
            case 2:
                studentAvailable.transform.GetChild(2).GetComponent<Text>().text = (Int32.Parse(studentAvailable.transform.GetChild(2).GetComponent<Text>().text) + 1).ToString();
                break;
            case 3:
                studentAvailable.transform.GetChild(0).GetComponent<Text>().text = (Int32.Parse(studentAvailable.transform.GetChild(0).GetComponent<Text>().text) + 1).ToString();
                break;
            case 11:
                studentAvailable.transform.GetChild(9).GetComponent<Text>().text = (Int32.Parse(studentAvailable.transform.GetChild(9).GetComponent<Text>().text) + 1).ToString();
                break;
            case 12:
                studentAvailable.transform.GetChild(7).GetComponent<Text>().text = (Int32.Parse(studentAvailable.transform.GetChild(7).GetComponent<Text>().text) + 1).ToString();
                break;
            case 13:
                studentAvailable.transform.GetChild(5).GetComponent<Text>().text = (Int32.Parse(studentAvailable.transform.GetChild(5).GetComponent<Text>().text) + 1).ToString();
                break;
        }
        studTypeList.RemoveAt(studTypeList.Count - 1);
        studBuf--;
    }
    public void remove10(string s){
        decreaseStudentCounts(s);
        decreaseStudentCounts(s);
        decreaseStudentCounts(s);
        decreaseStudentCounts(s);
        decreaseStudentCounts(s);
        decreaseStudentCounts(s);
        decreaseStudentCounts(s);
        decreaseStudentCounts(s);
        decreaseStudentCounts(s);
        decreaseStudentCounts(s);
    }
    public void resetType(){
        studTypeList.Clear();
    }
    //add international students after adding a dorm
    public void addIntStudent(){
        Debug.Log("Adding resident");
        int goodCount = (Int32.Parse(studentAvailable.transform.GetChild(7).GetComponent<Text>().text));
        int greatCount = (Int32.Parse(studentAvailable.transform.GetChild(9).GetComponent<Text>().text));
        //add more great students
        if(greatCount < (WorldController.Instance.world.reputation - repForGreatStud) / (repForGreatStud/2)){
            studentAvailable.transform.GetChild(9).GetComponent<Text>().text = (Int32.Parse(studentAvailable.transform.GetChild(9).GetComponent<Text>().text) + 1).ToString();
            return;
            }
        //add good students or ok students
        if(goodCount < (WorldController.Instance.world.reputation / 20) + 2){
            studentAvailable.transform.GetChild(7).GetComponent<Text>().text = (Int32.Parse(studentAvailable.transform.GetChild(7).GetComponent<Text>().text) + 1).ToString();
            return;
        }
        else
            studentAvailable.transform.GetChild(5).GetComponent<Text>().text = (Int32.Parse(studentAvailable.transform.GetChild(5).GetComponent<Text>().text) + 1).ToString();
    }
    public void addIntStudents(int currentIntOk, int currentIntGood, int currentIntGreat){
        //find values to add and adjust if any are more then available dorms
        int freeUpcoming = WorldController.Instance.world.getDormCount() - currentIntOk - currentIntGood - currentIntGreat;
        int freeGood = WorldController.Instance.world.reputation / 500 - currentIntGood;
        int freeGreat = WorldController.Instance.world.reputation / 1000 - currentIntGood;
        if(freeUpcoming - freeGreat < freeGood)
            freeGood = freeUpcoming - freeGreat;
        if(freeUpcoming < freeGreat){
            freeGreat = freeUpcoming;
            freeGood = 0;
        }
        //add great students
        studentAvailable.transform.GetChild(5).GetComponent<Text>().text = (Int32.Parse(studentAvailable.transform.GetChild(5).GetComponent<Text>().text) + freeGreat).ToString();
        freeUpcoming = freeUpcoming - freeGreat;
        studentAvailable.transform.GetChild(7).GetComponent<Text>().text = (Int32.Parse(studentAvailable.transform.GetChild(7).GetComponent<Text>().text) + freeGood).ToString();
        freeUpcoming = freeUpcoming - freeGood;
        studentAvailable.transform.GetChild(9).GetComponent<Text>().text = (Int32.Parse(studentAvailable.transform.GetChild(7).GetComponent<Text>().text) + freeUpcoming).ToString();
 
    }
    public void addIntStudents(int i){
        for(int j = i; j < 0; j--){
            Debug.Log("Adding resident");
            addIntStudent();
        }
    }
    public void addLocalStudents(){
        studentAvailable.transform.GetChild(0).GetComponent<Text>().text = "50";
        int goodCount = 10 + WorldController.Instance.world.reputation / 750;
        if(goodCount > 50)
            goodCount = 50;
        studentAvailable.transform.GetChild(2).GetComponent<Text>().text = goodCount.ToString();
        int greatCount = WorldController.Instance.world.reputation / 1500;
        if(greatCount > 20)
            greatCount = 20;
        studentAvailable.transform.GetChild(4).GetComponent<Text>().text = greatCount.ToString();
    }
    public void destroyGameObject(GameObject gameObject){
        Destroy(gameObject);
    }

    //set the scale time passes
    public void timeScale(string s){
        WorldController.Instance.setTimeScale(Int32.Parse(s));
    }

    //set the infobar text
    public void setInfoBar(string s){ 
        infoBar.GetComponent<Text>().text = s;
        barCount = 180;
    }

    //resets info bar
    public void resetInfoBar(){
        infoBar.GetComponent<Text>().text = "";
        barCount = 0;
    }

    //edit money
    public void setMoney(int i){
        moneyBar.GetComponent<Text>().text = ("$" + i.ToString());
        Debug.Log("Setting money to " + money);
        money = i;
    }
    public void setMoney(string s){
        moneyBar.GetComponent<Text>().text = ("$" + s);
    }
    public int getMoney(){
        //Debug.Log("Getting money at " + money);
        return money;
    }
    public void addMoney(int i){
        money += i;
        moneyBar.GetComponent<Text>().text = ("$" + money.ToString());
    }
    public void addMoneyOnLoad(int i){
        money += i;
    }
    public void subtractMoney(int i){
        money -= i;
        moneyBar.GetComponent<Text>().text = ("$" + money.ToString());
    }
    //makes markers for zones when trying to delete a zone
    public void makeZoneMarkers(int startX, int startY, int endX, int endY){
        for(int i = startX; i <= endX; i++) {
            for(int j = startY; j <= endY; j++) {
                GameObject go = (GameObject)Instantiate(ZoneMarker, new Vector3(i, j, 0), Quaternion.identity);
                go.transform.SetParent(markerHolder.transform);
            }
        }
    }
    //clean up zone
    public void deleteZoneMarkers(){
        foreach (Transform child in markerHolder.transform){
            GameObject.Destroy(child.gameObject);
        }   
    }
    //updates the list of professors for the prof. list panel when it is activated
    public void setProfList(){
        foreach(Character ch in CharacterController.Instance.profList){
            GameObject go = (GameObject)Instantiate(ProfSlice);
            int i = (int)(16 * settings.getScale());
            //Add info
            go.transform.GetChild(0).GetComponent<Text>().text = ch.name;
            setTextColor(go.transform.GetChild(5).gameObject);
            go.transform.GetChild(3).name = ch.getId().ToString();
            go.transform.GetChild(4).GetComponent<Text>().text = "Skill: " + ch.getSkill().ToString();
            setTextColor(go.transform.GetChild(4).gameObject);
            go.transform.GetChild(5).GetComponent<Text>().text = "Age: " + ch.year.ToString();
            setTextColor(go.transform.GetChild(5).gameObject);
            go.transform.SetParent(profListPanel.transform, false);
 
            // Add callback to button
            UnityEngine.Events.UnityAction buttonCallback;
            buttonCallback = () => {
                CharacterController.Instance.fireProfessor(ch.getId());
                markFireButton(go.transform.GetChild(1).GetComponent<Image>());
            };
            go.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(buttonCallback);

            //makes list of xourss tought
            string s = go.transform.GetChild(2).GetComponent<Text>().text;
            foreach(SectionLite sl in ch.sectionList)
                s = s + sl.getCourse().courseName + ", ";
            foreach(SectionLite sl in ch.sectionListS)
                s = s + sl.getCourse().courseName + ", ";
            go.transform.GetChild(2).GetComponent<Text>().text = s.Substring(0, s.Length - 2);
        }
        /*foreach(Character ch in CharacterController.Instance.charactersActive){
            if(ch.getType().Equals(Character.CharType.Prof)){
                GameObject go = (GameObject)Instantiate(ProfSlice);
                //Add info
                go.transform.GetChild(0).GetComponent<Text>().text = ch.name;
                go.transform.GetChild(0).GetComponent<Text>().color = ThemeController.Instance.textTheme;
                go.transform.GetChild(3).name = ch.getId().ToString();
                go.transform.SetParent(profListPanel.transform, false);
                go.transform.GetChild(4).GetComponent<Text>().text += CharacterController.Instance.profSkill[ch.getId() - 1].ToString();
                go.transform.GetChild(5).GetComponent<Text>().text += CharacterController.Instance.profAge[ch.getId() - 1].ToString();
    
                // Add callback to button
                UnityEngine.Events.UnityAction buttonCallback;
                buttonCallback = () => CharacterController.Instance.fireProfessor(ch.getId());
                go.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(buttonCallback);

                //makes list of courses tought
                string s = go.transform.GetChild(2).GetComponent<Text>().text;
                foreach(SectionLite sl in ch.sectionList)
                    s = s + sl.getCourse().courseName + ", ";
                foreach(SectionLite sl in ch.sectionListS)
                    s = s + sl.getCourse().courseName + ", ";
                go.transform.GetChild(2).GetComponent<Text>().text = s.Substring(0, s.Length - 2);
            }
        }*/
    }

    //add notification when a prof wins an awards/ achives something
    public void addAchiveSlice(Character ch, int awardType, int z){
        Debug.Log("Adding slice");
        GameObject go = (GameObject)Instantiate(profAchiveSlice);
        go.transform.SetParent(achiveList.transform, false);
        go.transform.GetChild(0).GetComponent<Text>().text = ch.name + z.ToString();
        //makes list of courses tought
        string s = go.transform.GetChild(1).GetComponent<Text>().text;
        foreach(SectionLite sl in ch.sectionList)
            s = s + sl.getCourse().courseName + ", ";
        foreach(SectionLite sl in ch.sectionListS)
            s = s + sl.getCourse().courseName + ", ";
        go.transform.GetChild(1).GetComponent<Text>().text = s.Substring(0, s.Length - 2);
        switch(awardType){
            case 1:
                go.transform.GetChild(1).GetComponent<Text>().text = "Won Major Award";
                break;
            case 2:
                go.transform.GetChild(1).GetComponent<Text>().text = "Major Award Nomination";
                break;
            case 3:
                go.transform.GetChild(1).GetComponent<Text>().text = "Won Minor Award";
                break;
            case 4:
                go.transform.GetChild(1).GetComponent<Text>().text = "Minor Award Nomination";
                break;
            case 5:
                go.transform.GetChild(1).GetComponent<Text>().text = "Major Work Completed";
                break;
        }
    }
    //update reputation indicator
    public void updateRepUI(int rep){
        repIndicate.GetComponent<Text>().text = "Rep: " + rep;
    }
    //clear list of buildings
    public void clearBuildings(){
        int i = 0;
        foreach (Transform child in buildingPanel.transform){
            if(i != 0)
                GameObject.Destroy(child.gameObject);
            else
                i++;
        }
    }
    //set text next to paint sliders
    public void setPaintText(int i){
        float value = paintMenu.transform.GetChild(i).GetComponent<Scrollbar>().value * 255;
        paintMenu.transform.GetChild(i + 1).GetChild(0).GetComponent<Text>().text = value.ToString("0");
        //set preview
        byte v1 = (byte)(paintMenu.transform.GetChild(0).GetComponent<Scrollbar>().value * 255);
        byte v2 = (byte)(paintMenu.transform.GetChild(2).GetComponent<Scrollbar>().value * 255);
        byte v3 = (byte)(paintMenu.transform.GetChild(4).GetComponent<Scrollbar>().value * 255);
        paintMenu.transform.GetChild(6).GetComponent<Image>().color = new Color32(v1, v2, v3, 255);
        MouseController.Instance.wallPaint = new Tuple<byte, byte, byte>(v1, v2, v3);
    }
    public void resetPaintText(){
        paintMenu.transform.GetChild(6).GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        MouseController.Instance.wallPaint = new Tuple<byte, byte, byte>(255, 255, 255);
    }

    //changes semesster button's sprite when pressed in course panel
    public void repaintButtonAsPressed(Image i){
        switch(ThemeController.Instance.uiTheme){
            case 1:
                coursePanel.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = buttonUnpressed;
                coursePanel.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().color = Color.black;
                coursePanel.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>().color = Color.black;
                coursePanel.transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite = buttonUnpressed;
                i.sprite = buttonPressed;
                i.gameObject.transform.GetChild(0).GetComponent<Text>().color = Color.white;
                break;
            case 2:
                coursePanel.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = ThemeController.Instance.forGroundShort2;
                coursePanel.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().color = Color.black;
                coursePanel.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>().color = Color.black;
                coursePanel.transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite = ThemeController.Instance.forGroundShort2;
                i.sprite = buttonPressed2;
                i.gameObject.transform.GetChild(0).GetComponent<Text>().color = Color.black;
                break;
            case 3:
                coursePanel.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = ThemeController.Instance.forGroundShort3;
                coursePanel.transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite = ThemeController.Instance.forGroundShort3;
                i.sprite = buttonPressed2;
                break;
        }
    }
    public void pauseUnpressed(){
        GameObject go = GameObject.Find("PauseButton");
        switch(ThemeController.Instance.uiTheme){
            case 1:
                go.GetComponent<Image>().sprite = buttonUnpressed;
                go.transform.GetChild(0).GetComponent<Text>().color = Color.black;
                break;
            case 2:
                go.GetComponent<Image>().sprite = ThemeController.Instance.forGroundShort2;
                go.transform.GetChild(0).GetComponent<Text>().color = Color.black;
                break;
            case 3:
                go.GetComponent<Image>().sprite = ThemeController.Instance.forGroundShort3;
                go.transform.GetChild(0).GetComponent<Text>().color = Color.white;
                break;
        }
    }
    public void pausePressed(){
        GameObject go = GameObject.Find("PauseButton");
        switch(ThemeController.Instance.uiTheme){
            case 1:
                go.GetComponent<Image>().sprite = buttonPressed;
                break;
            default:
                go.GetComponent<Image>().sprite = buttonPressed2;
                break;
        }
        go.transform.GetChild(0).GetComponent<Text>().color = Color.white;
    }
    public void markFireButton(Image i){
        if(i.sprite == unfireButton)
            i.sprite = fireButton;
        else
            i.sprite = unfireButton;
    }

    //changes size of UI elements when resolution changes
    public void updateUIScale(int width, int height, float scale){
        //Set lower button menu and info bar res.
        GameObject.Find("Canvas").GetComponent<CanvasScaler>().referenceResolution = new Vector2(width, height);
        GameObject go = GameObject.Find("BuildMenu");
        go.GetComponent<RectTransform>().SetSidesAsZero();
        go.GetComponent<RectTransform>().SetBottom(0.0f);
        go.GetComponent<RectTransform>().SetTop(height - 30.0f * scale);
        foreach (Transform child in go.transform)
            child.transform.GetChild(0).GetComponent<Text>().fontSize = (int)(16 * scale);

        go = GameObject.Find("AlertBar");
        go.GetComponent<RectTransform>().SetSidesAsZero();
        go.GetComponent<RectTransform>().SetBottom(30.0f * scale);
        go.GetComponent<RectTransform>().SetTop(height - 60.0f * scale);
        go.transform.GetChild(0).GetComponent<Text>().fontSize = (int)(16 * scale);
        //set timebar res
        go = GameObject.Find("TimeMenu"); 
        go.GetComponent<RectTransform>().SetSidesAsZero();
        go.GetComponent<RectTransform>().SetBottom(height - 30.0f * scale);
        go.GetComponent<RectTransform>().SetTop(0.0f);

        if(settingsActive)
            setSettingsPanelScale();
        if(mainMenuActive)
            setMainMenuScale();
        if(zoneMenuActive || buildMenuActive || buyMenuActive || charMenuActive)
            setMenuPanels();
        if(sectionMenuActive)
            setSectionMenuScale();
        if(coursePanelActive)
            setCoursesPanel();
        if(perfPanelActive)
            setPerfPanelScale();
        if(studentPanelActive)
            setStudPanelScale();
        if(profHirePanelActive)
            setProfHireScale();
        if(guidePanelActive)
            setGuidePanelScale();
        if(buildingPanelActive)
            setBuildingPanelScale();
        if(profListActive)
            setProfListScale();
        if(buildingConfirmPanelActive)
            setBuildingConfirmScale();
        if(loadConfirmPanelActive)
            setLoadConfirmScale();
        if(exitConfirmPanelActive)
            setExitConfirmScale();
        if(financePanelActive)
            setFinanceScale();

        //set text size and height for timebar buttons
        if(go.transform.GetChild(0).GetChild(0).GetComponent<Text>().fontSize != (int)(16 * scale)){
            Debug.Log("Setting scale: " + scale);
            int i = (int)(16 * scale);
            go.transform.GetChild(0).GetChild(0).GetComponent<Text>().fontSize = i;
            go.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<Text>().fontSize = i;
            go.transform.GetChild(1).GetChild(0).GetChild(1).GetComponent<Text>().fontSize = i;
            go.transform.GetChild(1).GetChild(0).GetChild(2).GetComponent<Text>().fontSize = i;
            go.transform.GetChild(1).GetChild(1).GetChild(0).GetComponent<Text>().fontSize = i;
            go.transform.GetChild(2).GetChild(0).GetChild(0).GetComponent<Text>().fontSize = i;
            go.transform.GetChild(2).GetChild(1).GetChild(0).GetComponent<Text>().fontSize = i;
            go.transform.GetChild(2).GetChild(2).GetChild(0).GetComponent<Text>().fontSize = i;
            go.transform.GetChild(3).GetChild(0).GetComponent<Text>().fontSize = i;
            go.transform.GetChild(4).GetChild(0).GetComponent<Text>().fontSize = i;
            go.transform.GetChild(5).GetChild(0).GetComponent<Text>().fontSize = i;
            go.transform.GetChild(6).GetChild(0).GetComponent<Text>().fontSize = i;
            go.transform.GetChild(7).GetChild(0).GetComponent<Text>().fontSize = i;
            go.transform.GetChild(8).GetChild(0).GetComponent<Text>().fontSize = i;
        }
    }
    public void cycleGOsOn(){
        savePanel.SetActive(true);
        exitConfirmPanel.SetActive(true);
        saveConfirmPanel.SetActive(true);
        loadConfirmPanel.SetActive(true);
        buildingConfirmPanel.SetActive(true);
        buildingPanel.SetActive(true);
        guidePanel.SetActive(true);
        studentPanel.SetActive(true);
        profHirePanel.SetActive(true);
        charMenu.SetActive(true);
        sectionMenu.SetActive(true);
        coursePanel.SetActive(true);
        mainMenuPanel.SetActive(true);
        zoneMenu.SetActive(true);
        buyMenu.SetActive(true);
        paintMenu.SetActive(true);
        buildMenu.SetActive(true);
    
    }public void cycleGOsOff(){
        savePanel.SetActive(false);
        exitConfirmPanel.SetActive(false);
        saveConfirmPanel.SetActive(false);
        loadConfirmPanel.SetActive(false);
        buildingConfirmPanel.SetActive(false);
        buildingPanel.SetActive(false);
        guidePanel.SetActive(false);
        studentPanel.SetActive(false);
        profHirePanel.SetActive(false);
        charMenu.SetActive(false);
        sectionMenu.SetActive(false);
        coursePanel.SetActive(false);
        mainMenuPanel.SetActive(false);
        zoneMenu.SetActive(false);
        buyMenu.SetActive(false);
        paintMenu.SetActive(false);
        buildMenu.SetActive(false);
    }
    //set course panels
    public void setCoursesPanel(){
        coursePanel.GetComponent<RectTransform>().SetLeft(settings.getWidth() - 160.0f);
        coursePanel.GetComponent<RectTransform>().SetRight(0.0f);
        coursePanel.GetComponent<RectTransform>().SetTop(settings.getHeight() / 2.7f);
        coursePanel.GetComponent<RectTransform>().SetBottom(60.0f);
        cpOpened = true;
        if(coursePanel.transform.GetChild(3).GetChild(0).GetComponent<Text>().fontSize != (int)(16 * settings.getScale())){
            int i = (int)(16 * settings.getScale());
            coursePanel.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().fontSize = i;
            coursePanel.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>().fontSize = i;
            coursePanel.transform.GetChild(1).GetChild(0).GetComponent<Text>().fontSize = i;
            coursePanel.transform.GetChild(2).GetChild(0).GetComponent<Text>().fontSize = i;
            coursePanel.transform.GetChild(3).GetChild(0).GetComponent<Text>().fontSize = i;
        }
    }
    //set menu panels
    public void setMenuPanels(){
        int height = settings.getHeight();
        float scale = settings.getScale();
        int i = (int)(16 * settings.getScale());
        float width = GameObject.Find("BuildMenu").GetComponent<RectTransform>().sizeDelta.x;
        buildMenu.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -height + 75 + 60 * scale, 0);
        buildMenu.GetComponent<RectTransform>().sizeDelta = new Vector2(width, 91);
        buyMenu.GetComponent<RectTransform>().anchoredPosition = new Vector3(160, -height + 105 + 60 * scale, 0);
        buyMenu.GetComponent<RectTransform>().sizeDelta = new Vector2(width, 216);
        zoneMenu.GetComponent<RectTransform>().anchoredPosition = new Vector3(320, -height + 75 + 60 * scale, 0);
        zoneMenu.GetComponent<RectTransform>().sizeDelta = new Vector2(width, 152);
        charMenu.GetComponent<RectTransform>().anchoredPosition = new Vector3(480, -height + 45 + 60 * scale, 0);
        charMenu.GetComponent<RectTransform>().sizeDelta = new Vector2(width, 91);
        mpOpened = true;
        
        if(buildMenu.transform.GetChild(0).GetChild(0).GetComponent<Text>().fontSize != (int)(16 * settings.getScale())){
            foreach(Transform child in buildMenu.transform)
                child.GetChild(0).GetComponent<Text>().fontSize = i;
            foreach(Transform child in buyMenu.transform)
                child.GetChild(0).GetComponent<Text>().fontSize = i;
            foreach(Transform child in zoneMenu.transform)
                child.GetChild(0).GetComponent<Text>().fontSize = i;
            foreach(Transform child in charMenu.transform)
                child.GetChild(0).GetComponent<Text>().fontSize = i;
        }
    }
    public void setSectionMenuScale(){
        switch(settings.getHeight()){
            case 720:
                sectionMenu.GetComponent<RectTransform>().SetLeft(settings.getWidth() / 1.89f);//set
                sectionMenu.GetComponent<RectTransform>().SetRight(settings.getWidth() / 8.31f);
                sectionMenu.GetComponent<RectTransform>().SetTop(settings.getHeight() / 3.58f);
                sectionMenu.GetComponent<RectTransform>().SetBottom(settings.getHeight() / 3.81f);
                break;
            case 900:
                sectionMenu.GetComponent<RectTransform>().SetLeft(settings.getWidth() / 1.726f); //set
                sectionMenu.GetComponent<RectTransform>().SetRight(settings.getWidth() / 7.34f);
                sectionMenu.GetComponent<RectTransform>().SetTop(settings.getHeight() / 2.85f);
                sectionMenu.GetComponent<RectTransform>().SetBottom(settings.getHeight() / 3.3f);
                break;
            case 1080: 
                sectionMenu.GetComponent<RectTransform>().SetLeft(settings.getWidth() / 1.616f); //set
                sectionMenu.GetComponent<RectTransform>().SetRight(settings.getWidth() / 7.16f);
                sectionMenu.GetComponent<RectTransform>().SetTop(settings.getHeight() / 2.97f);
                sectionMenu.GetComponent<RectTransform>().SetBottom(settings.getHeight() / 2.9f);
                break;
            case 1440:
                sectionMenu.GetComponent<RectTransform>().SetLeft(settings.getWidth() / 1.379f); //set
                sectionMenu.GetComponent<RectTransform>().SetRight(settings.getWidth() / 10.17f);
                sectionMenu.GetComponent<RectTransform>().SetTop(settings.getHeight() / 2.7f);
                sectionMenu.GetComponent<RectTransform>().SetBottom(settings.getHeight() / 2.535f);
                break;
        }
        if(sectionMenu.transform.GetChild(0).GetChild(0).GetComponent<Text>().fontSize != (int)(16 * settings.getScale())){
            int i = (int)(16 * settings.getScale());
            sectionMenu.transform.GetChild(0).GetChild(0).GetComponent<Text>().fontSize = i;
            sectionMenu.transform.GetChild(1).GetChild(0).GetComponent<Text>().fontSize = i;
            sectionMenu.transform.GetChild(2).GetChild(0).GetComponent<Text>().fontSize = i;
            sectionMenu.transform.GetChild(5).GetComponent<Text>().fontSize = i + 2;
            sectionMenu.transform.GetChild(6).GetComponent<Text>().fontSize = i + 2;
            sectionMenu.transform.GetChild(7).GetComponent<Text>().fontSize = i + 2;
            sectionMenu.transform.GetChild(8).GetComponent<Text>().fontSize = i + 2;
            Transform t = sectionMenu.transform.GetChild(4).GetChild(0).GetChild(0);
            for(int j = 0; j < 18; j++){
                t.GetChild(j).GetComponent<Text>().fontSize = i;
            }

        }
        smOpened = true;
    }
    public void setProfListScale(){
        profListPanel.GetComponent<RectTransform>().SetLeft(settings.getWidth() - 185.0f);
        profListPanel.GetComponent<RectTransform>().SetRight(0.0f);
        profListPanel.GetComponent<RectTransform>().SetTop(settings.getHeight() / 2.7f);
        profListPanel.GetComponent<RectTransform>().SetBottom(60.0f);
        plOpened = true;
    }
    public void setProfHireScale(){
        switch(settings.getHeight()){
            case 720:
                profHirePanel.GetComponent<RectTransform>().SetLeft(700.0f);//set
                profHirePanel.GetComponent<RectTransform>().SetRight(100.0f);
                profHirePanel.GetComponent<RectTransform>().SetTop(200.0f);
                profHirePanel.GetComponent<RectTransform>().SetBottom(110.0f);
                break;
            case 900:
                profHirePanel.GetComponent<RectTransform>().SetLeft(910.0f); //set
                profHirePanel.GetComponent<RectTransform>().SetRight(210.0f);
                profHirePanel.GetComponent<RectTransform>().SetTop(275.0f);
                profHirePanel.GetComponent<RectTransform>().SetBottom(230.0f);
                break;
            case 1080: 
                profHirePanel.GetComponent<RectTransform>().SetLeft(1140.0f); //set
                profHirePanel.GetComponent<RectTransform>().SetRight(300.0f);
                profHirePanel.GetComponent<RectTransform>().SetTop(330.0f);
                profHirePanel.GetComponent<RectTransform>().SetBottom(340.0f);
                break;
            case 1440:
                profHirePanel.GetComponent<RectTransform>().SetLeft(1520.0f); //set
                profHirePanel.GetComponent<RectTransform>().SetRight(566.0f);
                profHirePanel.GetComponent<RectTransform>().SetTop(500.0f);
                profHirePanel.GetComponent<RectTransform>().SetBottom(535.0f);
                break;
        }
        if(profHirePanel.transform.GetChild(1).GetChild(0).GetComponent<Text>().fontSize != (int)(16 * settings.getScale())){
            int i = (int)(16 * settings.getScale());
            profHirePanel.transform.GetChild(1).GetChild(0).GetComponent<Text>().fontSize = i;
            profHirePanel.transform.GetChild(2).GetChild(0).GetComponent<Text>().fontSize = i;
            profHirePanel.transform.GetChild(3).GetChild(0).GetComponent<Text>().fontSize = i;
            profHirePanel.transform.GetChild(4).GetComponent<Text>().fontSize = i;
            profHirePanel.transform.GetChild(5).GetComponent<Text>().fontSize = i;
            profHirePanel.transform.GetChild(6).GetComponent<Text>().fontSize = i;
            profHirePanel.transform.GetChild(7).GetComponent<Text>().fontSize = i;
            profHirePanel.transform.GetChild(8).GetComponent<Text>().fontSize = i;
        }
        phOpened = true;
    }
    public void setStudPanelScale(){
        switch(settings.getHeight()){
            case 720:
                studentPanel.GetComponent<RectTransform>().SetLeft(750.0f);//set
                studentPanel.GetComponent<RectTransform>().SetRight(120.0f);
                studentPanel.GetComponent<RectTransform>().SetTop(200.0f);
                studentPanel.GetComponent<RectTransform>().SetBottom(270.0f);
                break;
            case 900:
                studentPanel.GetComponent<RectTransform>().SetLeft(1000.0f); //set
                studentPanel.GetComponent<RectTransform>().SetRight(200.0f);
                studentPanel.GetComponent<RectTransform>().SetTop(290.0f);
                studentPanel.GetComponent<RectTransform>().SetBottom(360.0f);
                break;
            case 1080: 
                studentPanel.GetComponent<RectTransform>().SetLeft(1230.0f); //set
                studentPanel.GetComponent<RectTransform>().SetRight(280.0f);
                studentPanel.GetComponent<RectTransform>().SetTop(350.0f);
                studentPanel.GetComponent<RectTransform>().SetBottom(450.0f);
                break;
            case 1440:
                studentPanel.GetComponent<RectTransform>().SetLeft(1700.0f); //set
                studentPanel.GetComponent<RectTransform>().SetRight(450.0f);
                studentPanel.GetComponent<RectTransform>().SetTop(540.0f);
                studentPanel.GetComponent<RectTransform>().SetBottom(640.0f);
                break;
        }
        if(studentPanel.transform.GetChild(6).GetChild(0).GetComponent<Text>().fontSize != (int)(16 * settings.getScale())){
            int i = (int)(16 * settings.getScale());
            studentPanel.transform.GetChild(22).GetChild(0).GetComponent<Text>().fontSize = i;
            studentPanel.transform.GetChild(5).GetChild(0).GetComponent<Text>().fontSize = i;
            studentPanel.transform.GetChild(6).GetChild(0).GetComponent<Text>().fontSize = i;
            studentPanel.transform.GetChild(7).GetChild(0).GetComponent<Text>().fontSize = i;
            studentPanel.transform.GetChild(8).GetChild(0).GetComponent<Text>().fontSize = i;
            studentPanel.transform.GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(2).GetComponent<Text>().fontSize = i;
        }
        stpOpened = true;
    }
    public void setBuildingConfirmScale(){
        buildingConfirmPanel.GetComponent<RectTransform>().SetLeft(settings.getWidth() / 2.35f);
        buildingConfirmPanel.GetComponent<RectTransform>().SetRight(settings.getWidth() / 2.46f);
        buildingConfirmPanel.GetComponent<RectTransform>().SetTop(settings.getHeight() / 2.9f);
        buildingConfirmPanel.GetComponent<RectTransform>().SetBottom(settings.getHeight() / 2.16f);

        RectTransform rt = buildingConfirmPanel.transform.GetChild(2).GetComponent<RectTransform>();
        rt.SetLeft(settings.getWidth() / 116.3f);
        rt.SetRight(settings.getWidth() / 116.3f);
        rt.SetTop(settings.getHeight() / 54.0f - 0.3f);
        rt.SetBottom(settings.getHeight() / 8.17f - 16.1f);
        if(settings.getHeight() == 720)
            buildingConfirmPanel.transform.GetChild(2).GetChild(0).GetComponent<RectTransform>().SetTop(-1.7f);
        rt = buildingConfirmPanel.transform.GetChild(1).GetComponent<RectTransform>();
        rt.SetLeft(settings.getWidth() / 9.0f);
        rt.SetRight(settings.getWidth() / 60.0f);
        rt.SetTop(settings.getHeight() / 8.0f);
        rt.SetBottom(settings.getHeight() / 31.7f);
        rt = buildingConfirmPanel.transform.GetChild(0).GetComponent<RectTransform>();
        rt.SetLeft(settings.getWidth() / 60.0f);
        rt.SetRight(settings.getWidth() / 9.0f);
        rt.SetTop(settings.getHeight() / 8.0f);
        rt.SetBottom(settings.getHeight() / 31.7f);
        if(buildingConfirmPanel.transform.GetChild(2).GetChild(0).GetComponent<Text>().fontSize != (int)(16 * settings.getScale())){
            if(settings.getHeight() != 720){
                buildingConfirmPanel.transform.GetChild(2).GetComponent<RectTransform>().SetBottom(settings.getHeight() / 13.0f);
                buildingConfirmPanel.transform.GetChild(2).GetChild(0).GetComponent<Text>().fontSize = (int)(16 * settings.getScale());
            }
            buildingConfirmPanel.transform.GetChild(1).GetComponent<RectTransform>().SetTop(settings.getHeight() / 9.0f);
            buildingConfirmPanel.transform.GetChild(0).GetComponent<RectTransform>().SetTop(settings.getHeight() / 9.0f);
        }

        bcOpened = true;
    }
    public void setLoadConfirmScale(){
        loadConfirmPanel.GetComponent<RectTransform>().SetLeft(settings.getWidth() / 2.35f);
        loadConfirmPanel.GetComponent<RectTransform>().SetRight(settings.getWidth() / 2.46f);
        loadConfirmPanel.GetComponent<RectTransform>().SetTop(settings.getHeight() / 2.9f);
        loadConfirmPanel.GetComponent<RectTransform>().SetBottom(settings.getHeight() / 2.16f);

        RectTransform rt = loadConfirmPanel.transform.GetChild(2).GetComponent<RectTransform>();
        rt.SetLeft(settings.getWidth() / 116.3f);
        rt.SetRight(settings.getWidth() / 116.3f);
        rt.SetTop(settings.getHeight() / 54.0f - 0.3f);
        rt.SetBottom(settings.getHeight() / 8.17f - 16.1f);
        if(settings.getHeight() == 720)
            loadConfirmPanel.transform.GetChild(2).GetChild(0).GetComponent<RectTransform>().SetTop(-1.7f);
        rt = loadConfirmPanel.transform.GetChild(1).GetComponent<RectTransform>();
        rt.SetLeft(settings.getWidth() / 9.0f);
        rt.SetRight(settings.getWidth() / 60.0f);
        rt.SetTop(settings.getHeight() / 8.0f);
        rt.SetBottom(settings.getHeight() / 31.7f);
        rt = loadConfirmPanel.transform.GetChild(0).GetComponent<RectTransform>();
        rt.SetLeft(settings.getWidth() / 60.0f);
        rt.SetRight(settings.getWidth() / 9.0f);
        rt.SetTop(settings.getHeight() / 8.0f);
        rt.SetBottom(settings.getHeight() / 31.7f);
        if(loadConfirmPanel.transform.GetChild(2).GetChild(0).GetComponent<Text>().fontSize != (int)(16 * settings.getScale())){
            if(settings.getHeight() != 720){
                loadConfirmPanel.transform.GetChild(2).GetComponent<RectTransform>().SetBottom(settings.getHeight() / 13.0f);
                loadConfirmPanel.transform.GetChild(2).GetChild(0).GetComponent<Text>().fontSize = (int)(16 * settings.getScale());
            }
            loadConfirmPanel.transform.GetChild(1).GetComponent<RectTransform>().SetTop(settings.getHeight() / 9.0f);
            loadConfirmPanel.transform.GetChild(0).GetComponent<RectTransform>().SetTop(settings.getHeight() / 9.0f);
        }

        lcOpened = true;
    }
    public void setSaveConfirmScale(){
        saveConfirmPanel.GetComponent<RectTransform>().SetLeft(settings.getWidth() / 2.35f);
        saveConfirmPanel.GetComponent<RectTransform>().SetRight(settings.getWidth() / 2.46f);
        saveConfirmPanel.GetComponent<RectTransform>().SetTop(settings.getHeight() / 2.9f);
        saveConfirmPanel.GetComponent<RectTransform>().SetBottom(settings.getHeight() / 2.16f);

        RectTransform rt = saveConfirmPanel.transform.GetChild(2).GetComponent<RectTransform>();
        rt.SetLeft(settings.getWidth() / 116.3f);
        rt.SetRight(settings.getWidth() / 116.3f);
        rt.SetTop(settings.getHeight() / 54.0f - 0.3f);
        rt.SetBottom(settings.getHeight() / 8.17f - 16.1f);
        if(settings.getHeight() == 720)
            saveConfirmPanel.transform.GetChild(2).GetChild(0).GetComponent<RectTransform>().SetTop(-1.7f);
        rt = saveConfirmPanel.transform.GetChild(1).GetComponent<RectTransform>();
        rt.SetLeft(settings.getWidth() / 9.0f);
        rt.SetRight(settings.getWidth() / 60.0f);
        rt.SetTop(settings.getHeight() / 8.0f);
        rt.SetBottom(settings.getHeight() / 31.7f);
        rt = saveConfirmPanel.transform.GetChild(0).GetComponent<RectTransform>();
        rt.SetLeft(settings.getWidth() / 60.0f);
        rt.SetRight(settings.getWidth() / 9.0f);
        rt.SetTop(settings.getHeight() / 8.0f);
        rt.SetBottom(settings.getHeight() / 31.7f);
        if(saveConfirmPanel.transform.GetChild(2).GetChild(0).GetComponent<Text>().fontSize != (int)(16 * settings.getScale())){
            if(settings.getHeight() != 720){
                saveConfirmPanel.transform.GetChild(2).GetComponent<RectTransform>().SetBottom(settings.getHeight() / 13.0f);
                saveConfirmPanel.transform.GetChild(2).GetChild(0).GetComponent<Text>().fontSize = (int)(16 * settings.getScale());
            }
            saveConfirmPanel.transform.GetChild(1).GetComponent<RectTransform>().SetTop(settings.getHeight() / 9.0f);
            saveConfirmPanel.transform.GetChild(0).GetComponent<RectTransform>().SetTop(settings.getHeight() / 9.0f);
        }

        scOpened = true;
    }
    public void setExitConfirmScale(){
        exitConfirmPanel.GetComponent<RectTransform>().SetLeft(settings.getWidth() / 2.35f);
        exitConfirmPanel.GetComponent<RectTransform>().SetRight(settings.getWidth() / 2.46f);
        exitConfirmPanel.GetComponent<RectTransform>().SetTop(settings.getHeight() / 2.9f);
        exitConfirmPanel.GetComponent<RectTransform>().SetBottom(settings.getHeight() / 2.16f);

        RectTransform rt = exitConfirmPanel.transform.GetChild(2).GetComponent<RectTransform>();
        rt.SetLeft(settings.getWidth() / 116.3f);
        rt.SetRight(settings.getWidth() / 116.3f);
        rt.SetTop(settings.getHeight() / 54.0f - 0.3f);
        rt.SetBottom(settings.getHeight() / 8.17f - 16.1f);
        if(settings.getHeight() == 720)
            exitConfirmPanel.transform.GetChild(2).GetChild(0).GetComponent<RectTransform>().SetTop(-1.7f);
        rt = exitConfirmPanel.transform.GetChild(1).GetComponent<RectTransform>();
        rt.SetLeft(settings.getWidth() / 9.0f);
        rt.SetRight(settings.getWidth() / 60.0f);
        rt.SetTop(settings.getHeight() / 8.0f);
        rt.SetBottom(settings.getHeight() / 31.7f);
        rt = exitConfirmPanel.transform.GetChild(0).GetComponent<RectTransform>();
        rt.SetLeft(settings.getWidth() / 60.0f);
        rt.SetRight(settings.getWidth() / 9.0f);
        rt.SetTop(settings.getHeight() / 8.0f);
        rt.SetBottom(settings.getHeight() / 31.7f);
        if(exitConfirmPanel.transform.GetChild(2).GetChild(0).GetComponent<Text>().fontSize != (int)(16 * settings.getScale())){
            if(settings.getHeight() != 720){
                exitConfirmPanel.transform.GetChild(2).GetComponent<RectTransform>().SetBottom(settings.getHeight() / 13.0f);
                exitConfirmPanel.transform.GetChild(2).GetChild(0).GetComponent<Text>().fontSize = (int)(16 * settings.getScale());
            }
            exitConfirmPanel.transform.GetChild(1).GetComponent<RectTransform>().SetTop(settings.getHeight() / 9.0f);
            exitConfirmPanel.transform.GetChild(0).GetComponent<RectTransform>().SetTop(settings.getHeight() / 9.0f);
        }

        ecOpened = true;
    }
    public void setBuildingPanelScale(){
        buildingPanel.GetComponent<RectTransform>().SetLeft(settings.getWidth() - 185.0f);
        buildingPanel.GetComponent<RectTransform>().SetRight(0.0f);
        buildingPanel.GetComponent<RectTransform>().SetTop(settings.getHeight() / 2.7f);
        buildingPanel.GetComponent<RectTransform>().SetBottom(60.0f);
        Debug.Log("zzz" + buildingPanel.GetComponentsInChildren<Transform>().Length);
        if(buildingPanel.GetComponentsInChildren<Transform>().Length == 3)
            WorldController.Instance.initialBuilding("Buidling 1");
        bpOpened = true;
    }
    public void setPerfPanelScale(){
        perfPanel.GetComponent<RectTransform>().SetLeft(settings.getWidth() / 2.43f);
        perfPanel.GetComponent<RectTransform>().SetRight(settings.getWidth() / 2.43f);
        perfPanel.GetComponent<RectTransform>().SetTop(settings.getHeight() / 2.51f);
        perfPanel.GetComponent<RectTransform>().SetBottom(settings.getHeight() / 2.51f);
        ppOpened = true;
    }
    //fininsh later
    public void setMainMenuScale(){ 
        mainMenuPanel.GetComponent<RectTransform>().SetLeft(settings.getWidth() / 2.18f);
        Debug.Log("Setting main menu: " + settings.getWidth() / 2.18f);
        mainMenuPanel.GetComponent<RectTransform>().SetRight(settings.getWidth() / 2.18f);
        mainMenuPanel.GetComponent<RectTransform>().SetTop(settings.getHeight() / 2.38f);
        mainMenuPanel.GetComponent<RectTransform>().SetBottom(settings.getHeight() / 2.38f);
        RectTransform rt = mainMenuPanel.transform.GetChild(0).GetChild(0).GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(settings.getWidth() / 12 - 40, rt.sizeDelta.y);
        
        //set manu menu, this requires making it slightly longer
        if(mainMenuPanel.transform.GetChild(1).GetChild(0).GetComponent<Text>().fontSize != (int)(16 * settings.getScale())){
            Debug.Log("Scale math: " + (3.38f - settings.getScale()));
            rt.sizeDelta = new Vector2(settings.getWidth() / 12 - 40 + (settings.getScale() - 1) * 40, rt.sizeDelta.y);
            mainMenuPanel.GetComponent<RectTransform>().SetTop(settings.getHeight() / (1.38f + settings.getScale()));
            mainMenuPanel.GetComponent<RectTransform>().SetBottom(settings.getHeight() / (1.38f + settings.getScale()));
            mainMenuPanel.GetComponent<RectTransform>().SetLeft(settings.getWidth() / 2.23f);
            mainMenuPanel.GetComponent<RectTransform>().SetRight(settings.getWidth() / 2.23f);
            int i = (int)(16 * settings.getScale());
            mainMenuPanel.transform.GetChild(1).GetChild(0).GetComponent<Text>().fontSize = i;
            mainMenuPanel.transform.GetChild(2).GetChild(0).GetComponent<Text>().fontSize = i;
            mainMenuPanel.transform.GetChild(3).GetChild(0).GetComponent<Text>().fontSize = i;
            mainMenuPanel.transform.GetChild(4).GetChild(0).GetComponent<Text>().fontSize = i;
            mainMenuPanel.transform.GetChild(5).GetChild(0).GetComponent<Text>().fontSize = i;
        }
        mmOpened = true;
    }
    public void setGoalPanelScale(GameObject goalPanel){
        goalPanel.GetComponent<RectTransform>().SetLeft(settings.getWidth() / 4.47f);
        goalPanel.GetComponent<RectTransform>().SetRight(settings.getWidth() / 2.2f - 80.0f);
        goalPanel.GetComponent<RectTransform>().SetTop(32.0f * settings.getScale());
        goalPanel.GetComponent<RectTransform>().SetBottom(settings.getHeight() / 2.45f);
        goalPanel.transform.GetChild(0).GetComponent<RectTransform>().SetLeft(5.0f);
        goalPanel.transform.GetChild(0).GetComponent<RectTransform>().SetTop(5.0f);
        goalPanel.transform.GetChild(0).GetComponent<RectTransform>().SetRight(settings.getWidth() / 9.14f);
        goalPanel.transform.GetChild(0).GetComponent<RectTransform>().SetBottom(settings.getHeight() / 4.28f);
        goalPanel.transform.GetChild(1).GetComponent<RectTransform>().SetLeft(5.0f);
        goalPanel.transform.GetChild(1).GetComponent<RectTransform>().SetTop(settings.getHeight() / 30.0f);
        goalPanel.transform.GetChild(1).GetComponent<RectTransform>().SetRight(0.0f);
        goalPanel.transform.GetChild(1).GetComponent<RectTransform>().SetBottom(settings.getHeight() / 5.7f);
        int i = (int)(16 * settings.getScale());
        if(goalPanel.transform.GetChild(0).GetComponent<Text>().fontSize != i){
            goalPanel.transform.GetChild(0).GetComponent<Text>().fontSize = i;
            goalPanel.transform.GetChild(1).GetComponent<Text>().fontSize = i;
        }
    }
    public void setSettingsPanelScale(){
        settingsPanel.GetComponent<RectTransform>().SetLeft(settings.getWidth() / 2.4f);
        settingsPanel.GetComponent<RectTransform>().SetRight(settings.getWidth() / 2.4f);
        settingsPanel.GetComponent<RectTransform>().SetTop(settings.getHeight() / 3.4f);
        settingsPanel.GetComponent<RectTransform>().SetBottom(settings.getHeight() / 3.4f);
        int i = (int)(16 * settings.getScale());
        if(settingsPanel.transform.GetChild(0).GetComponent<Text>().fontSize != i){
            settingsPanel.transform.GetChild(0).GetComponent<Text>().fontSize = i;
            settingsPanel.transform.GetChild(2).GetComponent<Text>().fontSize = i;
            settingsPanel.transform.GetChild(4).GetComponent<Text>().fontSize = i;
            settingsPanel.transform.GetChild(1).GetChild(0).GetComponent<Text>().fontSize = i;
            settingsPanel.transform.GetChild(3).GetChild(0).GetComponent<Text>().fontSize = i;
            settingsPanel.transform.GetChild(5).GetChild(0).GetComponent<Text>().fontSize = i;
            settingsPanel.transform.GetChild(6).GetChild(0).GetComponent<Text>().fontSize = i;
            settingsPanel.transform.GetChild(7).GetChild(0).GetComponent<Text>().fontSize = i;
        }
        //Set the settings menu
        switch(settings.getHeight()){
            case 720:
                settingsPanel.transform.GetChild(1).GetComponent<Dropdown>().value = 0;
                break;
            case 900:
                settingsPanel.transform.GetChild(1).GetComponent<Dropdown>().value = 1;
                break;
            case 1080:
                settingsPanel.transform.GetChild(1).GetComponent<Dropdown>().value = 2;
                break;
            case 1440:
                settingsPanel.transform.GetChild(1).GetComponent<Dropdown>().value = 3;
                break;
        }
        switch(settings.getScale()){
            case 1.0f:
                settingsPanel.transform.GetChild(3).GetComponent<Dropdown>().value = 0;
                break;
            case 1.25f:
                settingsPanel.transform.GetChild(3).GetComponent<Dropdown>().value = 1;
                break;
            case 1.5f:
                settingsPanel.transform.GetChild(3).GetComponent<Dropdown>().value = 2;
                break;
        }
        if(Screen.fullScreen != true)
            settingsPanel.transform.GetChild(5).GetComponent<Dropdown>().value = 1;
        sepOpened = true;
    }
    public void setGuidePanelScale(){
        guidePanel.transform.GetChild(0).GetComponent<RectTransform>().SetBottom(settings.getHeight() - 60.0f *  settings.getScale());
        //Set dropdown and exit button on upper panel
        RectTransform rt = guidePanel.transform.GetChild(0).GetChild(0).GetComponent<RectTransform>();
        rt.SetBottom(0.0f);
        rt.SetTop(0.0f);
        rt.SetLeft(0.0f);
        rt.SetRight(settings.getWidth() - 250.0f * settings.getScale());
        rt = guidePanel.transform.GetChild(0).GetChild(1).GetComponent<RectTransform>();
        rt.SetBottom(0.0f);
        rt.SetTop(0.0f);
        rt.SetLeft(settings.getWidth() - 30.0f * settings.getScale());
        rt.SetRight(0.0f);
        gpOpened = true;
        //Scale UI if needed
        if(guidePanel.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().fontSize != (int)(16 * settings.getScale())){
            guidePanel.transform.GetChild(0).GetComponent<RectTransform>().SetTop(30.0f * settings.getScale());
            guidePanel.transform.GetChild(0).GetComponent<RectTransform>().SetBottom(settings.getHeight() - 60.0f * settings.getScale());
            int i = (int)(16 * settings.getScale());
            guidePanel.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().fontSize = i;
            setGuideHelper(guidePanel.transform.GetChild(1).GetChild(0).GetChild(0), i);
            setGuideHelper(guidePanel.transform.GetChild(1).GetChild(0).GetChild(1), i);
            setGuideHelper(guidePanel.transform.GetChild(1).GetChild(0).GetChild(2), i);
            setGuideHelper(guidePanel.transform.GetChild(1).GetChild(0).GetChild(3), i);
            setGuideHelper(guidePanel.transform.GetChild(1).GetChild(1).GetChild(0), i);
            setGuideHelper(guidePanel.transform.GetChild(1).GetChild(1).GetChild(1), i);
            setGuideHelper(guidePanel.transform.GetChild(1).GetChild(1).GetChild(2), i);
            setGuideHelper(guidePanel.transform.GetChild(1).GetChild(1).GetChild(3), i);
            guidePanel.transform.GetChild(1).GetComponent<RectTransform>().SetTop(60.0f * settings.getScale());
        }
    }
    public void setFinanceScale(){
        int i = (int)(16 * settings.getScale());
        if(financePanel.transform.GetChild(1).GetComponent<Text>().fontSize != i){
            financePanel.transform.GetChild(1).GetComponent<Text>().fontSize = i;
            financePanel.transform.GetChild(2).GetComponent<Text>().fontSize = i;
            financePanel.transform.GetChild(3).GetComponent<Text>().fontSize = i;
        }

    }
    public void setGuideHelper(Transform t, int i){
        t.GetChild(0).GetChild(0).GetComponent<Text>().fontSize = i;
        t.GetChild(1).GetComponent<Text>().fontSize = i;
        t.GetChild(2).GetComponent<Text>().fontSize = i;
        t.GetChild(3).GetComponent<Text>().fontSize = i;
        t.GetChild(4).GetComponent<Text>().fontSize = i;
        t.GetChild(5).GetComponent<Text>().fontSize = i;
        t.GetChild(6).GetComponent<Text>().fontSize = i;
        //t.GetChild(7).GetComponent<Text>().fontSize = i;
        //t.GetChild(8).GetComponent<Text>().fontSize = i;
    }
    //Load the main menu/intro scene
    public void toMainMenu(){
        SceneManager.LoadScene("Menu");
    }
    //quit Application
    public void quitApp(){
        Application.Quit();
    }
    //apply a settings change
    public void applySettingChange(){
        int width = 0, height = 0, scale = 0;
        switch(settingsPanel.transform.GetChild(1).GetComponent<Dropdown>().value){
            case 0:
                width = 1280;
                height = 720;
                break;
            case 1:
                width = 1600;
                height = 900;
                break;
            case 2:
                width = 1920;
                height = 1080;
                break;
            case 3:
                width = 2560;
                height = 1440;
                break;
        }
        scale = settingsPanel.transform.GetChild(3).GetComponent<Dropdown>().value;
        if(width == settings.getWidth() && height == settings.getHeight() && 1.0f + scale * 0.25f == settings.getScale())
            return;
        settings.setFullScreen(settingsPanel.transform.GetChild(5).GetComponent<Dropdown>().value);

        XmlSerializer serializer = new XmlSerializer(typeof(Settings));
        Settings s = new Settings(width, height, scale);
        TextWriter writer = new StringWriter();
        serializer.Serialize(writer, s);
        writer.Close();
        Debug.Log(writer.ToString());
        File.WriteAllText("Settings.xml", writer.ToString());
        settings = s;
        smOpened = false; phOpened = false; stpOpened = false; mpOpened = false; bcOpened = false;
        cpOpened = false; bpOpened = false; ppOpened = false; mmOpened = false; sepOpened = false;
        updateUIScale(width, height, s.getScale());
    }

    public void setUISemester(string sem){
        GameObject.Find("Semester").GetComponent<Text>().text = sem;
    }
}