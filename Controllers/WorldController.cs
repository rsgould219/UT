using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using UnityEditor.Compilation;
using System.Xml.Serialization;
using System.Xml;
using System.IO;

//44.0kb 7/10   19
//60.8kb 7/15        1250  lines (on disk)
//91.0kb 7/31        2520  (not on disk) course eng, start of goal
//95.8kb 8/1         2670  save and load for floor and funature
//103kb  8/15        3520  functioning goals, some fixes
//136kb  8/21        3840  all eng. filters for corses in section menu, can save/load characters
//148kb  8/25        4162  improved goal progress, updated/fixed section creation, started scheduler
//163kb  9/1         4631  set time for section, improved scheduler, work on chem/eng/hist professors, saving sections
//182kb  9/10        5117  save/load for sectons/zones, imporved settime, work on mec goals,
//199kb  9/16        5564  improved last save/load, added timeslots to save/load, sectionlite, improved tileslot, work on eng prof, curgoal completion check
//205kb  9/21        6016  improvrd scheduler, checking if prof has courses, che goals, next seat, offices, split courseui
//236KB  10/4        6584  improved scheduler. find next seat in zone, ai students moving to class at correct time, hist prof skill
//259kb  10/18       7109  added labs, professor move to class, new walls/dynamic walll designs, remove objects, destroy fixtures, rotate objects, mat goals 
//288kb  11/3        7810  fixed pathing, head rotation, true fast forward/pause, clock, fleshed out extra courses for goals/scheduler, civ/ise goals, non-cse scheduling, fixed load goal
//328kb  11/26       8760  fixs, performence, better electives, scheduling for more then just cse, buisness courses, goals for sci, infobar, prof skill and age
//396kb  1/3    20   10229 big trip,finished buisness/eng/hist/math/sci courses, schedule, skills, goals
//510kb  3/29        13417 fixes, grad students, including courses, goals, schedules, redid character saving/loading
//529kb  6/1         13904 fixes, redid character visuals, small additions to grad students/schedules, moving between buildings, including ui, other changes i forgot
//544kb  7/7         14289 added economy and deleting zones, schedule/section creations checks for missing zones, incuded money and character counts in save, ui changes, better guide filter
//579kb  8/15        15042 major fixes for switching buildings, awards, added dorms, beds, dressers, system for better students/ out of town students based on reptation/dorm rooms is 99% done
//591kb  9/13        15321 finished awards when hiring, professors can be retired, professors can age/ get awards/ want to retire, shows awards, zones now in sectionLites, small ui fixes
//577kb  9/25        15059 Important rewrite of schedule, much less goal checks, improved performance checks
//587kb  10/15       15114 rewrite of movement, fixing bugs with movement, professors, sceduling, zones, saves now include both section lists
//599kb  11/17       15478 basis main menu, resolution changes, start of UI adjustment to res change, some bug fixes/ tweaks to characters, zones, timeslots
//636kb  2/1    21   16132 UI adjestment, as well as UI scale and full screan, real previews for objects, some tool tip work, changes to building switching, some bug fixes, image updates
//640kb  3/24        16200 finsihed preview, rotations, rotation for previews, fixed bugs for saveing/loading objects, sick again
//652kb  10/18       16447 throut issues delayed, fixed various bugs with visuals, load/save, and switching buidings, more confirm menus, salrys/finance started, more world info saved/loading
//665kb  8/21   22   16753 more acid reflex and covid, paint, better course planning, tutorial, full save/load with confirm windows
//687kb  11/15  23   17212 how has it been a year, haelth problems, wedding, many save, assigning prof, and other bugs, button and UI improvements, main menu actually works,
//           k              many save, sceduling, and other bug fixes along progression testing, finished humanity sceduling, humanity/elective markings, redid loading sections after seleting course, ui for year/semester   
//753kb  6/7         18245 many fixes for saves, characters moving, going to/assigning classes to other buildings, completly redid pathfinding, fleshed out characters moving, going to/assigning classes to other buildings, ui themes
public class WorldController : MonoBehaviour{
    public static WorldController Instance;
    
    public GameObject BuildingSlice;
    public GameObject RenameField;
    public GameObject hourTime;
    public GameObject minuteTime;
    public GameObject dayButton;
    private bool paused = false; 
    public World world;
    static bool loadWorldBool = false;
    private int time1 = 0;
    private int time2 = 9;
    int day, year = 1;
    float barTime = 0.0f;
    public double ver = 0.1;
    bool barWaiting = false;
    public int timeScale = 1;
    public int tempMoney = 0;
    public Tuple<int, int> oldCharInfo = new Tuple<int, int>(0, 0);
    List<Character> oddTimingCharacters = new List<Character>();
    // Start is called before the first frame update
    void Start(){
        if(loadWorldBool){
            loadWorldBool = false;
            loadNewWorld();
        }
        else{
            createNewWorld();
        }

        Instance = this;
        updateClock();
        //world.buildings.Add(new Building());
    }
    //goes to the next morning 
    public void nextDay(){
        //if the day is not friday move up, else move to next week
        if(day != 5){
            //Set message for next day and advance
            switch(day){
                case 1:
                    UIController.Instance.setInfoBar("Welcome to Tueday");
                    break;
                case 2:
                    UIController.Instance.setInfoBar("Welcome to Wednesday");
                    break;
                case 3:
                    UIController.Instance.setInfoBar("Welcome to Thursday");
                    break;
                case 4:
                    UIController.Instance.setInfoBar("Welcome to Friday");
                    break;
            }
            day++;
        }
        else{
            day = 1;
            UIController.Instance.setInfoBar("Welcome to Monday");
        }
        //reset time
        time2 = 6;
        time1 = 59;
        hourTime.GetComponent<Text>().text = "7";
        minuteTime.GetComponent<Text>().text = "00";
        dayButton.GetComponent<Text>().text = "D:0" + day;
        //set destinations for new day
        CharacterController.Instance.nextDayUpdates();
    }
    //reset day and time, move to next year after successfully makeing and schedule and making character age if moving to fall
    public void advanceYear(){
        if(world.semFall != true){
            year++;
        }
        else{
            foreach(Character c in CharacterController.Instance.studList)
                c.year++;
            /*foreach(Character c in CharacterController.Instance.charactersActive){
                if(c.getType() == Character.CharType.Student)
                    c.year++;
            }*/
            CharacterController.Instance.professorProgress();
        }
        day = 1;
        time2 = 6;
        time1 = 59;
        hourTime.GetComponent<Text>().text = "7";
        minuteTime.GetComponent<Text>().text = "00";
    }
    //Updates the time
    async void updateClock(){
        if(barWaiting){
            if(barTime < Time.time){
                barWaiting = false;
                UIController.Instance.setInfoBar("");
            }
        }
        //advances time and day
        await Task.Delay(TimeSpan.FromSeconds(1));
        if (!(paused)){
            time1+= timeScale;
            if (time1 >= 60){
                time1 = 0;
                if (time2 == 23){
                    if (day != 5)
                        day++;
                    else
                        day = 1;
                    time2 = 6;
                }
                else
                    time2++;
                CharacterController.Instance.activeCheck();
                world.resetSeatsForZones();
            }
            //sets next destination for charcters if time is approprite
            //task is split in 4 with different times
            /*else if(time1 <= 10 && time1 >= 13){
                for(int i = 0; i < CharacterController.Instance.studList.Count; i += 4){
                    CharacterController.Instance.studList[i].setNextDest();
                }
                for(int i = 0; i < CharacterController.Instance.profList.Count; i += 4){
                    CharacterController.Instance.profList[i].setNextDest();
                }
            }
            else if (time1 <= 20 && time1 >= 23){
                for (int i = 1; i < CharacterController.Instance.studList.Count; i += 4){
                    CharacterController.Instance.studList[i].setNextDest();
                }
                for(int i = 1; i < CharacterController.Instance.profList.Count; i += 4){
                    CharacterController.Instance.profList[i].setNextDest();
                }
            }
            else if (time1 <= 40 && time1 >= 43){
                for (int i = 2; i < CharacterController.Instance.studList.Count; i += 4){
                    CharacterController.Instance.studList[i].setNextDest();
                }
                for(int i = 2; i < CharacterController.Instance.profList.Count; i += 4){
                    CharacterController.Instance.profList[i].setNextDest();
                }
            }
            else if (time1 <= 50 && time1 >= 53){
                for (int i = 3 ; i < CharacterController.Instance.studList.Count; i += 4) {
                    CharacterController.Instance.studList[i].setNextDest();
                }
                for(int i = 3; i < CharacterController.Instance.profList.Count; i += 4){
                    CharacterController.Instance.profList[i].setNextDest();
                }
            }*/
            else if(time1 >= 30 && time1 <= 34) {
                CharacterController.Instance.activeCheckOdd(oddTimingCharacters);
                oddTimingCharacters.Clear();
            }

            Debug.Log("time is " + time2 + ":" + time1);

            //update ui with new time
            if(time2 <= 9)
                hourTime.GetComponent<Text>().text = "0" + time2.ToString();
            else
                hourTime.GetComponent<Text>().text = time2.ToString();
            if(time1 <= 9)
                minuteTime.GetComponent<Text>().text = "0" + time1.ToString();
            else
                minuteTime.GetComponent<Text>().text = time1.ToString();
        }
        else{
            Debug.Log("time is paused");
        }

        updateClock();
    }
    //time and day getters
    public int getTime1(){
        return time1;
    }
    public int getTime2(){
        return time2;
    }
    public int getDay(){
        return day;
    }

    //proccess a pause button press
    public void pauseHit(){
        if(paused){
            paused = false;
            UIController.Instance.pauseUnpressed();
        }
        else{
            paused = true;
            UIController.Instance.pausePressed();
        }
    }

    //sets the time scaling of the world
    public void setTimeScale(int i){
        timeScale = i;
    }

    //load the time and day from save file
    public void loadTime(int t2, int t1, int d, int y){
        time2 = t2;
        hourTime.GetComponent<Text>().text = time2.ToString();
        time1 = t1;
        minuteTime.GetComponent<Text>().text = time1.ToString();
        GameObject.Find("Year").GetComponent<Text>().text = "Y:" + y;
        day = d;
        year = y;
    }
    //adds a new building if possible
    public void addBuilding(){
        //check if player has enough money
        if(UIController.Instance.getMoney() >= 10000){
            UIController.Instance.subtractMoney(10000);
        }else{
            UIController.Instance.setInfoBar("Not enough Money");
            return;
        }
        
        //Makes a building and adds basic info
        Building b = new Building();
        b.setId(world.buildings.Count);
        b.setName("Building " +  (world.buildings.Count + 1));

        //add building to list, then update ui elements
        world.buildings.Add(b);
        GameObject go = (GameObject)Instantiate(BuildingSlice);
        go.name = b.getName();
        go.transform.SetParent(UIController.Instance.buildingPanel.transform, false);
        go.transform.GetChild(0).GetComponent<Text>().text = go.name;
        go.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => {
            reNameBuilding(go, go.transform.childCount);
        });
        go.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => {
            goToBuilding(go);
        });
    }
    //create the initial building on world creation
    public void initialBuilding(string name){
        if(name == "")
        name = "Building 1";
        GameObject go = (GameObject)Instantiate(BuildingSlice);
        go.name = name;
        go.transform.SetParent(UIController.Instance.buildingPanel.transform, false);
        go.transform.GetChild(0).GetComponent<Text>().text = name;
        go.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => {
            reNameBuilding(go, go.transform.childCount);
        });
        go.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => {
            goToBuilding(go);
        });
    }
    //rename a building and update ui elements
    public void reNameBuilding(GameObject go, int buildingIndex){
        GameObject field = (GameObject)Instantiate(RenameField);
        field.transform.SetParent(UIController.Instance.buildingPanel.transform, false);
        field.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => {
            go.transform.GetChild(0).GetComponent<Text>().text = 
                field.transform.GetChild(0).GetComponent<InputField>().text;
            WorldController.Instance.world.buildings[buildingIndex].setName(field.transform.GetChild(0).GetComponent<InputField>().text);
            Destroy(field);
        });
    }
    //change the active building
    public void goToBuilding(GameObject go){
        //tries to save the current building, avoids an error when trying to go to last building from 1st building
        int curBuild = world.getCurrentBuilding() - 1;
        int curMoney = UIController.Instance.getMoney();
        if(curBuild > -1){
            Debug.Log("Saving building at " + curBuild);
            world.buildings[curBuild].loadBuilding(BuildingController.Instance.building);
        }
        else
            return;
        //clears building
        CharacterController.Instance.clearActiveChar();
        BuildingController.Instance.clearBuilding();
        //loads new building
		int id = int.Parse(go.name.Substring(go.name.Length - 1)) - 1;
        Debug.Log("Loading building at " + (id));
        BuildingController.Instance.building.loadBuilding(world.buildings[id]);
        world.setCurrentBuilding(id + 1);
        CharacterController.Instance.assignCharactersToBuilding(id + 1);
        UIController.Instance.setMoney(curMoney);
    }
    //add a character to the list of characters that do something at x:30 times
    public void addOddTimingCharacter(Character c){
        oddTimingCharacters.Add(c);
    }
    //create a new world on start up 
    public void createNewWorld(){
        world = new World();
        world.buildings.Add(new Building());
        //world.addBuilding(BuildingController.Instance.building);
    }

    public void loadNewWorld(){
        world = new World();
        world.addBuilding(BuildingController.Instance.building);
    }
    //saves the world to a file
    public void saveWorld(){
        //serializes world to string
        //world.addBuilding(BuildingController.Instance.building);
        tempMoney = UIController.Instance.getMoney();

        //tries to save the current building
        Debug.Log("Saving building at " + (world.getCurrentBuilding() - 1));
        //world.buildings[world.getCurrentBuilding() - 1].loadBuilding(BuildingController.Instance.building);

        //serializes game world
        XmlSerializer serializer = new XmlSerializer(typeof(World));
        TextWriter writer = new StringWriter();
        serializer.Serialize(writer, world);
        writer.Close();

        Debug.Log(writer.ToString());
        //PlayerPrefs.SetString("SaveGame", writer.ToString());
        File.WriteAllText(@"Saves\" + SaveController.Instance.saveName, writer.ToString());
        UIController.Instance.setInfoBar("Saved Game");
        UIController.Instance.setMoney(tempMoney);

        //refresh list of saves
        SaveController.Instance.refreshSaves();
    }
    //create an autosave while saving the stored save name
    public void autoSave(){
        string saveBuf = SaveController.Instance.saveName;
        SaveController.Instance.saveName = "Autosave";
        saveWorld();
        SaveController.Instance.saveName = saveBuf;
    }
    //load the world from a save file after clearing data
    public void loadWorld(){
        UIController.Instance.clearBuildings();
        CharacterController.Instance.clearForLoad();
        foreach(Transform child in GameObject.Find("CharacterController").transform)
            GameObject.Destroy(child.gameObject);
        XmlSerializer serializer = new XmlSerializer(typeof(World));
		//TextReader reader = new StringReader(PlayerPrefs.GetString("SaveGame"));
        TextReader reader = new StringReader(System.IO.File.ReadAllText(@"Saves\" + SaveController.Instance.saveName));
		Debug.Log(reader.ToString());
		world = (World)serializer.Deserialize(reader);
		reader.Close();

        /*//set next destinations after loading world
        foreach(Character c in CharacterController.Instance.charactersActive)
            c.setNextDest();
        foreach(Character c in CharacterController.Instance.studList)
            c.setNextDest();
        foreach(Character c in CharacterController.Instance.gradList)
            c.setNextDest();
        foreach(Character c in CharacterController.Instance.profList)
            c.setNextDest();*/

        foreach (Character c in CharacterController.Instance.studList)
            Debug.Log("Checking sectionlist of size: " + c.sectionList.Count + " for " + c.name);
        
        ScheduleController.Instance.addIntStudents();
        if(world.semFall == false)
            UIController.Instance.addLocalStudents();
        else{
            TimeSlotController.Instance.setSemAsFall();
            CourseUIController.Instance.updateWorkingSemester(true);
        }
    }
    //check if we are loading a world from main menu
    public void loadFromMenuCheck(){
        if(SaveController.Instance.getTransfer()  != null){
            SaveController.Instance.recallTranfser();
            loadWorld();
        }
        GameObject.Destroy(GameObject.Find("CheckController"));
    }
    public void resetStudentCounts(){
        ScheduleController.Instance.addIntStudents();
        if(world.semFall == false)
            UIController.Instance.addLocalStudents();
    }
    public void refreshFinance(){
        Tuple<int, int> charInfo = CharacterController.Instance.calcCharFinance();
        UIController.Instance.updateFinance(charInfo);
    }
    public void testTime(){
        time2 = 9;
        time1 = 56;
    }
    //sets the time of the save file
    public void writeTime(XmlWriter writer){
        writer.WriteStartElement("Time");
        writer.WriteAttributeString("time1", time1.ToString());
        writer.WriteAttributeString("time2", time2.ToString());
        writer.WriteAttributeString("day", day.ToString()); 
        writer.WriteAttributeString("year", year.ToString()); 

        if(world.semFall != true)
            writer.WriteAttributeString("sem", "s"); 
        else
            writer.WriteAttributeString("sem", "f"); 
        writer.WriteEndElement();
    }

    /*public void testRecompile(){
        CompilationPipeline.RequestScriptCompilation();
        Debug.Log(System.DateTime.Now);
    }*/
}