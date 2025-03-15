using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeSlotController : MonoBehaviour{
    public static TimeSlotController Instance = null;
    TimeSlot selectedTimeSlot = null;
    
    public bool changeInOverWrite = false;
    public bool semFall = true;
    string selectedTimeString = null;
    List<TimeSlot> slotList = new List<TimeSlot>();

    // Start is called before the first frame update
    void Start(){
        Instance = this;  
    }
    //set the course hours on the section menu when a new section is loaded, with either the section or course id
    public void setTotalCourseHours(Section s){
        string str = AcedemicController.Instance.courses[s.getCourseId() - 1].credits.ToString();
        GameObject.Find("TotalCourseHours").GetComponent<Text>().text = str;
        GameObject.Find("CurrentCourseHours").GetComponent<Text>().text = "0";
    }
    public void setTotalCourseHours(int courseId){
        string str = AcedemicController.Instance.courses[courseId - 1].credits.ToString();
        GameObject.Find("TotalCourseHours").GetComponent<Text>().text = str;
        GameObject.Find("CurrentCourseHours").GetComponent<Text>().text = "0";
    }

    public void setSemAsFall(){
        semFall = true;
        UIController.Instance.repaintButtonAsPressed(UIController.Instance.coursePanel.transform.GetChild(0).GetChild(1).GetComponent<Image>());
    }
    public void setSemAsSpring(){
        semFall = false;
    }

    //sets the timeslots when loading a section
    public void setTimeSlots(Section s){
        int credits = 0;
        foreach (TimeSlot ts in s.getSlotList()){
            if (ts.getStartTime() > 9){
                GameObject.Find("TimeSlot" + ts.getStartTime() + "_" + ts.getDay()).GetComponent<Image>().color = Color.blue;
            }
            else{
                GameObject.Find("TimeSlot0" + ts.getStartTime() + "_" + ts.getDay()).GetComponent<Image>().color = Color.blue;
            }

            if (ts.getStartTime() + 1 > 9){
                GameObject.Find("TimeSlot" + (ts.getStartTime() + 1) + "_" + ts.getDay()).GetComponent<Image>().color = Color.blue;
            }
            else{
                GameObject.Find("TimeSlot0" + (ts.getStartTime() + 1) + "_" + ts.getDay()).GetComponent<Image>().color = Color.blue;
            }

            if (ts.getLength() == 3 && ts.getStartTime() + 2 > 9){
                GameObject.Find("TimeSlot" + (ts.getStartTime() + 2) + "_" + ts.getDay()).GetComponent<Image>().color = Color.blue;
            }
            else if (ts.getLength() == 3){
                GameObject.Find("TimeSlot0" + (ts.getStartTime() + 2) + "_" + ts.getDay()).GetComponent<Image>().color = Color.blue;
            }

            Debug.Log("Timeslot: Day: " + ts.getDay() + " Time:" + ts.getStartTime() + " with length " + ts.getLength());
            credits += ts.getLength();

            //adds timeslot to stored list for current section
            slotList.Add(ts);
        }
        if (credits % 2 == 0){
            GameObject.Find("CurrentCourseHours").GetComponent<Text>().text = (credits / 2).ToString() + ".0";
        }
        else{
            GameObject.Find("CurrentCourseHours").GetComponent<Text>().text = (credits / 2).ToString() + ".5";
        }
    }

    public void setTimeSlot(string s){
        changeInOverWrite = true;
        GameObject go = GameObject.Find(s);
        if(go.GetComponent<Image>().color != Color.blue)
            Debug.Log("Ts color not blue");
        if(selectedTimeSlot != null)
            Debug.Log("selectedTimeslot: Day: " + selectedTimeSlot.getDay() + " Time:" + selectedTimeSlot.getStartTime() + " with length " + selectedTimeSlot.getLength());
        //if the slot is empty and a slot is selected, remove selected and turn blue and add a lengthed slot
        if(go.GetComponent<Image>().color != Color.blue && selectedTimeSlot != null){
            //handle if selected are on different days
            if(selectedTimeSlot.getDay() != Int32.Parse(s.Substring(11, 1))){
                selectedTimeSlot = null;
                setTimeSlot(s);
                return;
            }
            //handel if selected is the 2nd slot instead of the base slot
            removeFromSlotList(selectedTimeSlot);
            if(s.Substring(8, 2).Equals((selectedTimeSlot.getStartTime() + 3).ToString())){
                slotList.Add(new TimeSlot(selectedTimeSlot.getDay(), selectedTimeSlot.getStartTime(), 4));
                if (Int32.Parse(s.Substring(8, 2)) - 1 < 10){
                    Debug.Log("TimeSlot0" + (Int32.Parse(s.Substring(8, 2)) - 1).ToString() + s.Substring(10, 2));
                    GameObject g = GameObject.Find("TimeSlot0" + (Int32.Parse(s.Substring(8, 2)) - 1).ToString() + s.Substring(10, 2));
                    g.GetComponent<Image>().color = Color.blue;
                }
                else{
                    Debug.Log("TimeSlot" + (Int32.Parse(s.Substring(8, 2)) - 1).ToString() + s.Substring(10, 2));
                    GameObject g = GameObject.Find("TimeSlot" + (Int32.Parse(s.Substring(8, 2)) - 1).ToString() + s.Substring(10, 2));
                    g.GetComponent<Image>().color = Color.blue;
                }
            }
            //handle if selected is the 3rd slot instead of base slot
            else if(selectedTimeSlot.getLength() == 3){
                Debug.Log("Looking for 3Length timeslot");
                foreach(TimeSlot ts in slotList){
                    if(ts.getDay() == selectedTimeSlot.getDay()){
                        ts.setLength(4);
                        resetCourseHours();
                    }
                }
            }

            else
                slotList.Add(new TimeSlot(selectedTimeSlot.getDay(), selectedTimeSlot.getStartTime(), selectedTimeSlot.getLength() + 1));
            selectedTimeSlot = null;
            go.GetComponent<Image>().color = Color.blue;
            resetCourseHours();
        }
        //if an empty tile is selected and there is no selected timeslot, make a new slot
        else if (go.GetComponent<Image>().color != Color.blue){
            go.GetComponent<Image>().color = Color.blue;
            if (Int32.Parse(s.Substring(8, 2)) + 1 < 10){
                go = GameObject.Find("TimeSlot0" + (Int32.Parse(s.Substring(9, 1)) + 1).ToString() + s.Substring(10, 2));
            }
            else{
                go = GameObject.Find("TimeSlot" + (Int32.Parse(s.Substring(8, 2)) + 1).ToString() + s.Substring(10, 2));
            }
            go.GetComponent<Image>().color = Color.blue;
            //selectedTimeSlot = new TimeSlot(Int32.Parse(s.Substring(8, 1)), Int32.Parse(s.Substring(10, 1)), 2);
            slotList.Add(new TimeSlot(Int32.Parse(s.Substring(11, 1)), Int32.Parse(s.Substring(8, 2)), 2));
            resetCourseHours();
        }
        //remove slot if click is same as selected(aka double click)
        else if(selectedTimeSlot != null){
            if(Int32.Parse(s.Substring(11, 1)) == selectedTimeSlot.getDay() && Int32.Parse(s.Substring(8, 2)) == selectedTimeSlot.getStartTime()){
                go.GetComponent<Image>().color = Color.white;
                foreach (TimeSlot ts in slotList){
                    //if base bar is selected
                    if (ts.getDay() == Int32.Parse(s.Substring(11, 1)) && ts.getStartTime() == Int32.Parse(s.Substring(8, 2))){
                        baseBarSelectHelper(s, ts);
                        break;
                    }
                    //if 2nd bar below base is selected
                    if (ts.getDay() == Int32.Parse(s.Substring(11, 1)) && ts.getStartTime() == Int32.Parse(s.Substring(8, 2)) - 1){
                        bar2SelectHelper(s, ts);
                        break;
                    }
                    //if 3rd bar is selected
                    if (ts.getDay() == Int32.Parse(s.Substring(11, 1)) && ts.getStartTime() == Int32.Parse(s.Substring(8, 2)) - 2){
                        bar3SelectHelper(s, ts);
                        break;
                    }
                }
                resetCourseHours();
                selectedTimeSlot = null;
            }
        }
        //if a marked slot is click with no selected slot, recognize the slot as selected
        else{
            selectedTimeSlot = new TimeSlot(Int32.Parse(s.Substring(11, 1)), Int32.Parse(s.Substring(8, 2)), 2);
            //check if the slot is a base slot or not
            foreach(TimeSlot ts in slotList){
                if(ts.getDay() == selectedTimeSlot.getDay() && ts.getStartTime() == selectedTimeSlot.getStartTime() - 1){
                    Debug.Log("Selecting base as selected base");
                    selectedTimeSlot = new TimeSlot(ts.getDay(), ts.getStartTime(), 2);
                }
                else if(ts.getDay() == selectedTimeSlot.getDay() && ts.getLength() == 3 && ts.getStartTime() == selectedTimeSlot.getStartTime() - 2){
                    Debug.Log("Selecting base as selected base");
                    selectedTimeSlot = new TimeSlot(ts.getDay(), ts.getStartTime(), 3);
                }
            }
        }
        debugSlots();
    }

    //helper called by setTimeSlot for selecting a base bar
    public void baseBarSelectHelper(string s, TimeSlot ts){
        if (Int32.Parse(s.Substring(8, 2)) + 1 < 10){
            removeSlotGOHelper("0" + (Int32.Parse(s.Substring(9, 1)) + 1).ToString() + s.Substring(10, 2));
            if(ts.getLength() == 3){
                if(Int32.Parse(s.Substring(8, 2)) + 2 < 10)
                    removeSlotGOHelper("0" + (Int32.Parse(s.Substring(9, 1)) + 2).ToString() + s.Substring(10, 2));
                else
                    removeSlotGOHelper((Int32.Parse(s.Substring(8, 2)) + 2).ToString() + s.Substring(10, 2));
            }
            else if(ts.getLength() == 4){
                if(Int32.Parse(s.Substring(8, 2)) + 2 < 10)
                    removeSlotGOHelper("0" + (Int32.Parse(s.Substring(9, 1)) + 2).ToString() + s.Substring(10, 2));
                else
                    removeSlotGOHelper((Int32.Parse(s.Substring(8, 2)) + 2).ToString() + s.Substring(10, 2));
                Debug.Log("Tilted to " + (Int32.Parse(s.Substring(8, 2))));
                if (Int32.Parse(s.Substring(8, 2)) + 3 < 10)
                    removeSlotGOHelper("0" + (Int32.Parse(s.Substring(9, 1)) + 3).ToString() + s.Substring(10, 2));
                else
                    removeSlotGOHelper((Int32.Parse(s.Substring(8, 2)) + 3).ToString() + s.Substring(10, 2));
            }
        }
        else{
            removeSlotGOHelper((Int32.Parse(s.Substring(8, 2)) + 1).ToString() + s.Substring(10, 2));
            if(ts.getLength() == 3)
                removeSlotGOHelper((Int32.Parse(s.Substring(8, 2)) + 2).ToString() + s.Substring(10, 2));
            else if(ts.getLength() == 4){
                removeSlotGOHelper((Int32.Parse(s.Substring(8, 2)) + 2).ToString() + s.Substring(10, 2));
                removeSlotGOHelper((Int32.Parse(s.Substring(8, 2)) + 3).ToString() + s.Substring(10, 2));
            }
        }
        removeFromSlotList(selectedTimeSlot);
    }

    //helper called by setTimeSlot for selecting the 2nd bar
    public void bar2SelectHelper(string s, TimeSlot ts){
        if (Int32.Parse(s.Substring(8, 2)) - 1 > 9){
            removeSlotGOHelper((Int32.Parse(s.Substring(8, 2)) - 1).ToString() + s.Substring(10, 2));
            removeSlotGOHelper((Int32.Parse(s.Substring(8, 2))).ToString() + s.Substring(10, 2));
            if (ts.getLength() == 3)
            {
                removeSlotGOHelper((Int32.Parse(s.Substring(8, 2)) + 1).ToString() + s.Substring(10, 2));
            }
        }
        else{
            removeSlotGOHelper("0" + (Int32.Parse(s.Substring(9, 1)) - 1).ToString() + s.Substring(10, 2));
        }
        if (Int32.Parse(s.Substring(8, 2)) < 10)
            removeSlotGOHelper("0" + (Int32.Parse(s.Substring(9, 1))).ToString() + s.Substring(10, 2));
        else
            removeSlotGOHelper((Int32.Parse(s.Substring(8, 2))).ToString() + s.Substring(10, 2));
        if (ts.getLength() == 3){
            if (Int32.Parse(s.Substring(8, 2)) + 1 < 10)
                removeSlotGOHelper("0" + (Int32.Parse(s.Substring(9, 1)) + 1).ToString() + s.Substring(10, 2));
            else
                removeSlotGOHelper((Int32.Parse(s.Substring(8, 2)) + 1).ToString() + s.Substring(10, 2));
        }
        removeFromSlotList(ts);
    }

    //helper called by setTimeSlot for selecting the 3rd bar
    public void bar3SelectHelper(string s, TimeSlot ts){
        if (Int32.Parse(s.Substring(8, 2)) - 1 > 9)
            removeSlotGOHelper((Int32.Parse(s.Substring(8, 2)) - 1).ToString() + s.Substring(10, 2));
        else
            removeSlotGOHelper("0" + (Int32.Parse(s.Substring(9, 1)) - 1).ToString() + s.Substring(10, 2));

        if (Int32.Parse(s.Substring(8, 2)) - 2 > 9)
            removeSlotGOHelper((Int32.Parse(s.Substring(8, 2)) - 2).ToString() + s.Substring(10, 2));
        else
            removeSlotGOHelper("0" + (Int32.Parse(s.Substring(9, 1)) - 2).ToString() + s.Substring(10, 2));
        removeFromSlotList(ts);
    }

    //helper to remove gameobjects
    public void removeSlotGOHelper(string s){
        GameObject go = null;
        if (Int32.Parse(s.Substring(0, 2)) + 1 < 11){
            go = GameObject.Find("TimeSlot0" + s.Substring(1));
        }
        else{
            go = GameObject.Find("TimeSlot" + s);
        }
        go.GetComponent<Image>().color = Color.white;
    }

    //recalculate the current course hours on section menu
    public void resetCourseHours(){
        int credits = 0;
        foreach (TimeSlot ts in slotList){
            credits += ts.getLength();
        }
        if (credits % 2 == 0){
            GameObject.Find("CurrentCourseHours").GetComponent<Text>().text = (credits / 2).ToString() + ".0";
        }
        else{
            GameObject.Find("CurrentCourseHours").GetComponent<Text>().text = (credits / 2).ToString() + ".5";
        }
    }

    //helper for setTimeSlot to remove slots
    public void removeFromSlotList(TimeSlot baseTS){
        //find and remove from list
        int i = 0;
        int j = -1;
        foreach (TimeSlot ts in slotList){
            if (ts.getStartTime() == baseTS.getStartTime() && ts.getDay() == baseTS.getDay())
                j = i;
            else
                i++;
        }
        if (j != -1){
            slotList.RemoveAt(j);
        }
    }

    //clear and reset timeslots gameobjects
    public void resetTimeSlots(){
        foreach (Transform child in GameObject.Find("ContentSlots").transform){
            if (child.GetComponent<Button>() != null){
                child.GetComponent<Image>().color = Color.white;
            }
        }
        slotList.Clear();
    }

    //prints each slot in the list
    public void debugSlots(){
        foreach (TimeSlot ts in slotList){
            Debug.Log("Timeslot: Day: " + ts.getDay() + " Time:" + ts.getStartTime() + " with length " + ts.getLength());
        }
    }

    //marks slots where the room is used
    public void setInvalidSlots(){
        if(semFall){
            foreach(Course c in AcedemicController.Instance.courses){
                foreach (Section s in c.sections){
                    Debug.Log("Getting Id" + BuildingController.Instance.selectedZ.getId());
                    Debug.Log("Getting Id" + s.getZone().getId());
                    if(BuildingController.Instance.selectedZ.getId() == s.getZone().getId()){
                        foreach(TimeSlot ts in s.getSlotList()){
                            invalidHelper(ts, s, Color.red); 
                        }
                    }
                }
            }
        }
        else{
            foreach(Course c in AcedemicController.Instance.courses){
                foreach (Section s in c.sectionsS){
                    Debug.Log("Getting Id" + BuildingController.Instance.selectedZ.getId());
                    Debug.Log("Getting Id" + s.getZone().getId());
                    if(BuildingController.Instance.selectedZ.getId() == s.getZone().getId()){
                        foreach(TimeSlot ts in s.getSlotList()){
                            invalidHelper(ts, s, Color.red); 
                        }
                    }
                }
            }
        }
    }
    //marks slots where the selected profesor is busy
    public void setInvalidSlotsProf(Character selected){
        if(selected == null)
            return;
        if(semFall){
            foreach(Course c in AcedemicController.Instance.courses){
                foreach(Section s in c.sections){
                    Debug.Log("Getting Prof Id" + selected.getId());
                    Debug.Log("Getting Prof Id" + s.getProf().getId());
                    if(selected.getId() == s.getProf().getId()){
                        Debug.Log("Characters ID match for timeslot check");
                        foreach (TimeSlot ts in s.getSlotList()){
                            invalidHelper(ts, s, Color.yellow);
                        }
                    }
                }
            }
        }
        else{
           foreach(Course c in AcedemicController.Instance.courses){
                foreach(Section s in c.sectionsS){
                    Debug.Log("Getting Prof Id" + selected.getId());
                    Debug.Log("Getting Prof Id" + s.getProf().getId());
                    if(selected.getId() == s.getProf().getId()){
                        Debug.Log("Characters ID match for timeslot check");
                        foreach (TimeSlot ts in s.getSlotList()){
                            invalidHelper(ts, s, Color.yellow);
                        }
                    }
                }
            }
        }
    }
    //turns all timeslots white
    public void refreshSlots(){
        for(int i = 1; i < 10; i++){
            for(int j = 1; j < 6; j++){
                GameObject.Find("TimeSlot0" + i + "_" + j).GetComponent<Image>().color = Color.white;
            }
        }
        for(int i = 10; i < 19; i++){
            for(int j = 1; j < 6; j++){
                GameObject.Find("TimeSlot" + i + "_" + j).GetComponent<Image>().color = Color.white;
            }
        }
    }
    //refresh the list of timeslots and credits
    public void refreshSlotList(){
        GameObject.Find("CurrentCourseHours").GetComponent<Text>().text = "0";
        slotList.Clear();
    }
    //marks slots as invalid by changing their color
    public void invalidHelper(TimeSlot ts, Section s, Color c){
        GameObject go;
        if (ts.getStartTime() < 10)
            go = GameObject.Find("TimeSlot0" + ts.getStartTime() + "_" + ts.getDay());
         else
            go = GameObject.Find("TimeSlot" + ts.getStartTime() + "_" + ts.getDay());
        go.GetComponent<Image>().color = c;

        if (ts.getStartTime() + 1 < 10)
            go = GameObject.Find("TimeSlot0" + (ts.getStartTime() + 1) + "_" + ts.getDay());
        else
            go = GameObject.Find("TimeSlot" + (ts.getStartTime() + 1) + "_" + ts.getDay());
        go.GetComponent<Image>().color = c;

        if (ts.getLength() == 3){
            if (ts.getStartTime() + 2 < 10)
                go = GameObject.Find("TimeSlot0" + (ts.getStartTime() + 2) + "_" + ts.getDay());
            else
                go = GameObject.Find("TimeSlot" + (ts.getStartTime() + 2) + "_" + ts.getDay());
            go.GetComponent<Image>().color = c;
        }
    }

    public List<TimeSlot> getSlotList(){
        return this.slotList;
    }

    //check if the selected zone already has a class at selected time
    public bool zoneCheck(){
        if(BuildingController.Instance.selectedZ == null)
            return false;

        foreach (Course c in AcedemicController.Instance.courses){
            if(semFall){
                foreach (Section s in c.sections){
                    if (BuildingController.Instance.selectedZ.getId() == s.getZone().getId()){
                        foreach (TimeSlot ts in slotList){
                            foreach (TimeSlot ts2 in s.getSlotList()){
                                //check if same day
                                if (ts.getDay() == ts2.getDay()){
                                    if (ts.getStartTime() == ts2.getStartTime() || ts.getStartTime() == ts2.getStartTime() + 1 || ts.getStartTime() + 1 == ts2.getStartTime()){
                                        Debug.Log("Timeslot: 1 Day: " + ts.getDay() + " Time:" + ts.getStartTime() + " with length " + ts.getLength());
                                        Debug.Log("Timeslot: 1 Day: " + ts2.getDay() + " Time:" + ts2.getStartTime() + " with length " + ts2.getLength());
                                        return false;
                                    }
                                    //special checks for long timeslots
                                    if (ts.getLength() == 3 && ts.getStartTime() + 2 == ts2.getStartTime()){
                                        Debug.Log("Timeslot: 3 Day: " + ts.getDay() + " Time:" + ts.getStartTime() + " with length " + ts.getLength());
                                        Debug.Log("Timeslot: 2 Day: " + ts2.getDay() + " Time:" + ts2.getStartTime() + " with length " + ts2.getLength());
                                        return false;
                                    }
                                    if (ts2.getLength() == 3 && ts.getStartTime() == ts2.getStartTime() + 2){
                                        Debug.Log("Timeslot: 3 Day: " + ts.getDay() + " Time:" + ts.getStartTime() + " with length " + ts.getLength());
                                        Debug.Log("Timeslot: 3 Day: " + ts2.getDay() + " Time:" + ts2.getStartTime() + " with length " + ts2.getLength());
                                        return false;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else{
                foreach (Section s in c.sectionsS){
                    if (BuildingController.Instance.selectedZ.getId() == s.getZone().getId()){
                        foreach (TimeSlot ts in slotList){
                            foreach (TimeSlot ts2 in s.getSlotList()){
                                //check if same day
                                if (ts.getDay() == ts2.getDay()){
                                    if (ts.getStartTime() == ts2.getStartTime() || ts.getStartTime() == ts2.getStartTime() + 1 || ts.getStartTime() + 1 == ts2.getStartTime()){
                                        Debug.Log("Timeslot: 1 Day: " + ts.getDay() + " Time:" + ts.getStartTime() + " with length " + ts.getLength());
                                        Debug.Log("Timeslot: 1 Day: " + ts2.getDay() + " Time:" + ts2.getStartTime() + " with length " + ts2.getLength());
                                        return false;
                                    }
                                    //special checks for long timeslots
                                    if (ts.getLength() == 3 && ts.getStartTime() + 2 == ts2.getStartTime()){
                                        Debug.Log("Timeslot: 3 Day: " + ts.getDay() + " Time:" + ts.getStartTime() + " with length " + ts.getLength());
                                        Debug.Log("Timeslot: 2 Day: " + ts2.getDay() + " Time:" + ts2.getStartTime() + " with length " + ts2.getLength());
                                        return false;
                                    }
                                    if (ts2.getLength() == 3 && ts.getStartTime() == ts2.getStartTime() + 2){
                                        Debug.Log("Timeslot: 3 Day: " + ts.getDay() + " Time:" + ts.getStartTime() + " with length " + ts.getLength());
                                        Debug.Log("Timeslot: 3 Day: " + ts2.getDay() + " Time:" + ts2.getStartTime() + " with length " + ts2.getLength());
                                        return false;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        return true;
    }
}