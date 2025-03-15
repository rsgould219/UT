using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CourseUIController : MonoBehaviour
{
    public static CourseUIController Instance;
    public GameObject department;
    public GameObject profList;
    public GameObject courses;
    public GameObject coursePanel;
    public GameObject sectionMenu;
    public GameObject addButton;
    public GameObject SectionButton;
    public GameObject setRoom;
    Course sectionBuf = null;
    public Character selectedProf;
    int ogProfInt;
    bool overwriteSectionMode = false;
    public bool semesterFall = true;
    Section clickedSection;
    Section ogSection;
    List<Character> listedProfs = new List<Character>();
    List<int> courseIdValues = new List<int>();
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        addButton.GetComponent<Button>().onClick.AddListener(() => {
            TimeSlotController.Instance.setTotalCourseHours(sectionBuf.courseId);
        });
    }

    
    //Creates the options for the courses menu after a department is selected and list of possible professors
    //TODO: update to dynamicly add
    public void onDepartmentChange(){
        List<string> dropOptions = new List<string>();
        courseIdValues.Clear();
        switch(department.GetComponent<Dropdown>().value){
            case 1: //Engeneering
                dropOptions.Add("ENGR101");
                courseIdValues.Add(0);
                break;
            case 2: //cse
                toCSE(dropOptions, CharacterController.Instance.profList);
                break;
            case 3: //Mec
                toMEC(dropOptions, CharacterController.Instance.profList);
                break;
            case 4: //chem
                toCHE(dropOptions, CharacterController.Instance.profList);
                break;
            case 5: //elec
                toELE(dropOptions, CharacterController.Instance.profList);
                break;
            case 6: //CIV
                toCIV(dropOptions, CharacterController.Instance.profList);
                break;
            case 7: //ISE
                toISE(dropOptions, CharacterController.Instance.profList);
                break;
            case 8: //MAT
                toMAT(dropOptions, CharacterController.Instance.profList);
                break;
            case 9: //PHY
                toPHY(dropOptions, CharacterController.Instance.profList);
                break;
            case 10: //CHM
                toCHM(dropOptions, CharacterController.Instance.profList);
                break;
            case 11: //ENG
                toENG(dropOptions, CharacterController.Instance.profList);
                break;
            case 12: //HIST
                toHIST(dropOptions, CharacterController.Instance.profList);
                break;
            case 13: //MATH
                toMATH(dropOptions, CharacterController.Instance.profList);
                break;
            case 14: //ECO
                toECO(dropOptions, CharacterController.Instance.profList);
                break;
            case 15: //MKT
                toMKT(dropOptions, CharacterController.Instance.profList);
                break;
            case 16: //ACT
                toACT(dropOptions, CharacterController.Instance.profList);
                break;
            case 17: //FIN
                toFIN(dropOptions, CharacterController.Instance.profList);
                break;
            case 18: //BIS
                toBIS(dropOptions, CharacterController.Instance.profList);
                break;
            case 19: //LAW
                toLAW(dropOptions, CharacterController.Instance.profList);
                break;
        }
        courses.GetComponent<Dropdown>().ClearOptions();
        if(dropOptions.Count != 0){
            courses.GetComponent<Dropdown>().AddOptions(dropOptions);
        }
        else{
            dropOptions.Add("No Valid Courses");
            courses.GetComponent<Dropdown>().AddOptions(dropOptions);
        }
        onCourseChange();
    }

    public void dropHelper(int a, int b, int e, List<string> dropOptions, List<Character> tempList){
        for (int i = a; i < b; i++){
            foreach (Character c in tempList){
                if (c.skilledCourses != null && c.skilledCourses.Contains(i)){
                    courseIdValues.Add(i);
                    if(i > e)
                        dropOptions.Add(AcedemicController.Instance.courses[i].courseName + "E");
                    else
                        dropOptions.Add(AcedemicController.Instance.courses[i].courseName);
                    break;
                }
            }
        }
    }

    //onDepartmentChange helper for cse courses
    public void toCSE(List<string> dropOptions, List<Character> profList){
        foreach(Character c in profList){
            if(c.getMajor() == 1){
                dropOptions.Add("CSE101");
                dropOptions.Add("CSE102");
                dropOptions.Add("CSE103");
                courseIdValues.Add(1);
                courseIdValues.Add(7);
                courseIdValues.Add(8);
                break;
            }
        }
        if(dropOptions.Count == 0)
            return;
        for(int i = 132; i < 162; i++){
            foreach(Character c in profList){
                if(c.getMajor() == 1 && c.skilledCourses != null && c.skilledCourses.Contains(i)){
                    courseIdValues.Add(i);
                    if(i > 143 && i < 161)
                        dropOptions.Add(AcedemicController.Instance.courses[i].courseName + "E");
                    else
                        dropOptions.Add(AcedemicController.Instance.courses[i].courseName);
                    break;
                }
            }
        }
        for(int i = 577; i < 616; i++){
            foreach(Character c in profList){
                if(c.getMajor() == 1 && c.skilledCourses != null && c.skilledCourses.Contains(i)){
                    courseIdValues.Add(i);
                    dropOptions.Add(AcedemicController.Instance.courses[i].courseName);
                    break;
                }
            }
        }
    }

    //onDepartmentChange helper for cse courses
    public void toMEC(List<string> dropOptions, List<Character> profList){
        List<Character> tempList= new List<Character>();
        foreach(Character c in profList){
            if(c.getMajor() == 2){
                tempList.Add(c);
            }
        }
        if(tempList.Count == 0)
            return;
        else{
            dropOptions.Add("MEC101");
            dropOptions.Add("MEC102");
            dropOptions.Add("MEC103");
            courseIdValues.Add(20);
            courseIdValues.Add(21);
            courseIdValues.Add(22);
        }
        
        for(int i = 198; i < 234; i++){
            foreach(Character c in tempList){
                if(c.skilledCourses != null && c.skilledCourses.Contains(i)){
                    courseIdValues.Add(i);
                    if(i > 208)
                        dropOptions.Add(AcedemicController.Instance.courses[i].courseName + "E");
                    else
                        dropOptions.Add(AcedemicController.Instance.courses[i].courseName);
                    break;
                }
            }
        }
        for(int i = 620; i < 642; i++){
            foreach(Character c in tempList){
                if(c.skilledCourses != null && c.skilledCourses.Contains(i)){
                    courseIdValues.Add(i);
                    dropOptions.Add(AcedemicController.Instance.courses[i].courseName);
                    break;
                }
            }
        }
        foreach(Character c in tempList){ 
            if(c.skilledCourses != null && c.skilledCourses.Contains(452)){
                courseIdValues.Add(452);
                dropOptions.Add(AcedemicController.Instance.courses[452].courseName);
                break;
            }
        }
        foreach(Character c in tempList){
            if(c.skilledCourses != null && c.skilledCourses.Contains(453)){
                courseIdValues.Add(453);
                dropOptions.Add(AcedemicController.Instance.courses[453].courseName);
                break;
            }
        }
    }

    //onDepartmentChange helper for che courses
    public void toCHE(List<string> dropOptions, List<Character> profList){
        List<Character> tempList= new List<Character>();
        foreach(Character c in profList){
            if(c.getMajor() == 3)
                tempList.Add(c);
        }
        if(tempList.Count == 0)
            return;
        else{
            dropOptions.Add("CHE101");
            dropOptions.Add("CHE150");
            dropOptions.Add("CHE210");
            dropOptions.Add("CHE342");
            courseIdValues.Add(24);
            courseIdValues.Add(33);
            courseIdValues.Add(34);
            courseIdValues.Add(35);
        }
        for(int i = 36; i < 67; i++){
            foreach(Character c in tempList){
                if(c.skilledCourses != null && c.skilledCourses.Contains(i)){
                    courseIdValues.Add(i);
                    if(i > 45)
                        dropOptions.Add(AcedemicController.Instance.courses[i].courseName + "E");
                    else
                        dropOptions.Add(AcedemicController.Instance.courses[i].courseName);
                    break;
                }
            }
        }
        for(int i = 598; i < 616; i++){
            foreach(Character c in tempList){
                if(c.skilledCourses != null && c.skilledCourses.Contains(i)){
                    courseIdValues.Add(i);
                    dropOptions.Add(AcedemicController.Instance.courses[i].courseName);
                    break;
                }
            }
        }
    }

  //onDepartmentChange helper for elec courses
    public void toELE(List<string> dropOptions, List<Character> profList){
        List<Character> tempList= new List<Character>();
        foreach(Character c in profList){
            if(c.getMajor() == 4)
                tempList.Add(c);
        }
        if(tempList.Count == 0)
            return;
        for(int i = 234; i < 268; i++){
            foreach(Character c in profList){
                if(c.skilledCourses != null && c.skilledCourses.Contains(i)){
                    courseIdValues.Add(i);
                    if(i > 250)
                        dropOptions.Add(AcedemicController.Instance.courses[i].courseName + "E");
                    else
                        dropOptions.Add(AcedemicController.Instance.courses[i].courseName);
                    break;
                }
            }
        }
        for(int i = 642; i < 667; i++){
            foreach(Character c in profList){
                if(c.skilledCourses != null && c.skilledCourses.Contains(i)){
                    courseIdValues.Add(i);
                    dropOptions.Add(AcedemicController.Instance.courses[i].courseName);
                    break;
                }
            }
        }
    }

    //onDepartmentChange helper for civ courses
    public void toCIV(List<string> dropOptions, List<Character> profList){
        List<Character> tempList= new List<Character>();
        foreach(Character c in profList){
            if(c.getMajor() == 5)
                tempList.Add(c);
        }
        if(tempList.Count == 0)
            return;
        for(int i = 66; i < 104; i++){
            foreach(Character c in profList){
                if(c.skilledCourses != null && c.skilledCourses.Contains(i)){
                    courseIdValues.Add(i);
                    if(i > 83)
                        dropOptions.Add(AcedemicController.Instance.courses[i].courseName + "E");
                    else
                        dropOptions.Add(AcedemicController.Instance.courses[i].courseName);
                    break;
                }
            }
        }
        for(int i = 667; i < 708; i++){
            foreach(Character c in profList){
                if(c.skilledCourses != null && c.skilledCourses.Contains(i)){
                    courseIdValues.Add(i);
                    dropOptions.Add(AcedemicController.Instance.courses[i].courseName);
                    break;
                }
            }
        }
    }

    //onDepartmentChange helper for ISE courses
    public void toISE(List<string> dropOptions, List<Character> profList){
        List<Character> tempList= new List<Character>();
        foreach(Character c in profList){
            if(c.getMajor() == 6)
                tempList.Add(c);
        }
        if(tempList.Count == 0)
            return;
        for(int i = 103; i < 131; i++){
            foreach(Character c in tempList){
                if(c.skilledCourses != null && c.skilledCourses.Contains(i)){
                    courseIdValues.Add(i);
                    if(i > 114)
                        dropOptions.Add(AcedemicController.Instance.courses[i].courseName + "E");
                    else
                        dropOptions.Add(AcedemicController.Instance.courses[i].courseName);
                    break;
                }
            }
        }
        for(int i = 708; i < 743; i++){
            foreach(Character c in tempList){
                if(c.skilledCourses != null && c.skilledCourses.Contains(i)){
                    courseIdValues.Add(i);
                    dropOptions.Add(AcedemicController.Instance.courses[i].courseName);
                    break;
                }
            }
        }
    }

    //onDepartmentChange helper for MAT courses
    public void toMAT(List<string> dropOptions, List<Character> profList){
        List<Character> tempList= new List<Character>();
        foreach(Character c in profList){
            if(c.getMajor() == 7)
                tempList.Add(c);
        }
        if(tempList.Count == 0)
            return;
        for(int i = 161; i < 197; i++){
            foreach(Character c in tempList){
                if(c.skilledCourses != null && c.skilledCourses.Contains(i)){
                    courseIdValues.Add(i);
                    if(i > 178)
                        dropOptions.Add(AcedemicController.Instance.courses[i].courseName + "E");
                    else
                        dropOptions.Add(AcedemicController.Instance.courses[i].courseName);
                    break;
                }
            }
        }
        for(int i = 743; i < 769; i++){
            foreach(Character c in tempList){
                if(c.skilledCourses != null && c.skilledCourses.Contains(i)){
                    courseIdValues.Add(i);
                    dropOptions.Add(AcedemicController.Instance.courses[i].courseName);
                    break;
                }
            }
        }
    }

    public void toPHY(List<string> dropOptions, List<Character> profList){
        List<Character> tempList= new List<Character>();
        foreach(Character c in profList){
            if(c.getMajor() == 8)
                tempList.Add(c);
        }
        if(tempList.Count == 0)
            return;
        else{
            dropOptions.Add("PHY110");
            dropOptions.Add("PHY111");
            dropOptions.Add("PHY112");
            courseIdValues.Add(9);
            courseIdValues.Add(507);
            courseIdValues.Add(13);
        }
        for (int i = 29; i < 32; i++){
            foreach (Character c in tempList){
                if(c.skilledCourses != null && c.skilledCourses.Contains(i)){
                    courseIdValues.Add(i);
                    dropOptions.Add(AcedemicController.Instance.courses[i].courseName);
                    break;
                }
            }
        }
        for (int i = 404; i < 423; i++){
            foreach (Character c in tempList){
                if(c.skilledCourses != null && c.skilledCourses.Contains(i)){
                    courseIdValues.Add(i);
                    if(i > 417)
                        dropOptions.Add(AcedemicController.Instance.courses[i].courseName + "E");
                    else
                        dropOptions.Add(AcedemicController.Instance.courses[i].courseName);
                    break;
                }
            }
        }
        for (int i = 769; i < 787; i++){
            foreach (Character c in tempList){
                if(c.skilledCourses != null && c.skilledCourses.Contains(i)){
                    courseIdValues.Add(i);
                    dropOptions.Add(AcedemicController.Instance.courses[i].courseName);
                    break;
                }
            }
        }
    }

    public void toCHM(List<string> dropOptions, List<Character> profList){
        List<Character> tempList= new List<Character>();
        foreach(Character c in profList){
            if(c.getMajor() == 9)
                tempList.Add(c);
        }
        if(tempList.Count == 0)
            return;
        else{
            dropOptions.Add("CHM110");
            dropOptions.Add("CHM111");
            dropOptions.Add("CHM112");
            dropOptions.Add(AcedemicController.Instance.courses[23].courseName);
            dropOptions.Add(AcedemicController.Instance.courses[25].courseName);
            courseIdValues.Add(10);
            courseIdValues.Add(12);
            courseIdValues.Add(26);
            courseIdValues.Add(23);
            courseIdValues.Add(25);
        }
        for (int i = 398; i < 404; i++){
            foreach (Character c in tempList){
                if(c.skilledCourses != null && c.skilledCourses.Contains(i)){
                    courseIdValues.Add(i);
                    dropOptions.Add(AcedemicController.Instance.courses[i].courseName);
                    break;
                }
            }
        }
        for (int i = 424; i < 453; i++){
            foreach (Character c in tempList){
                if(c.skilledCourses != null && c.skilledCourses.Contains(i)){
                    courseIdValues.Add(i);
                    if(i > 432)
                        dropOptions.Add(AcedemicController.Instance.courses[i].courseName + "E");
                    else
                        dropOptions.Add(AcedemicController.Instance.courses[i].courseName);
                    break;
                }
            }
        }
        for (int i = 787; i < 819 ; i++){
            foreach (Character c in tempList){
                if(c.skilledCourses != null && c.skilledCourses.Contains(i)){
                    courseIdValues.Add(i);
                    dropOptions.Add(AcedemicController.Instance.courses[i].courseName);
                    break;
                }
            }
        }
    }

    public void toENG(List<string> dropOptions, List<Character> profList){
        List<Character> tempList= new List<Character>();
        foreach(Character c in profList){
            if(c.getMajor() == 10)
                tempList.Add(c);
        }
        if(tempList.Count == 0)
            return;
        else{
            dropOptions.Add("ENG101");
            dropOptions.Add("ENG102");
            courseIdValues.Add(2);
            courseIdValues.Add(3);
        }
        for (int i = 268; i < 302; i++){
            foreach (Character c in profList){
                if(c.skilledCourses != null && c.skilledCourses.Contains(i)){
                    courseIdValues.Add(i);
                    if(i < 274 && i != 268)
                        dropOptions.Add(AcedemicController.Instance.courses[i].courseName + " (H)");
                    else if(i > 273 && i < 299){
                        dropOptions.Add(AcedemicController.Instance.courses[i].courseName + "E");
                        break;
                    }
                    else
                        dropOptions.Add(AcedemicController.Instance.courses[i].courseName);
                    break;
                }
            }
        }
        for (int i = 842; i < 862; i++){
            foreach (Character c in profList){
                if(c.skilledCourses != null && c.skilledCourses.Contains(i)){
                    courseIdValues.Add(i);
                    dropOptions.Add(AcedemicController.Instance.courses[i].courseName);
                    break;
                }
            }
        }
    }

    //onDepartmentChange helper for HIST courses
    public void toHIST(List<string> dropOptions, List<Character> profList){
        List<Character> tempList= new List<Character>();
        foreach(Character c in profList){
            if(c.getMajor() == 11){
                tempList.Add(c);
            }
        }
        if(tempList.Count == 0)
            return;
        for (int i = 302; i < 386; i++){
            bool courseAdded = false;
            foreach (Character c in profList){
                if(c.skilledCourses != null && c.skilledCourses.Contains(i)){
                     courseIdValues.Add(i);
                    //check if elective or not then add course
                    if(i < 306 || i > 330){
                         for(int j = 3; j < 18; j++){
                            if(i == AcedemicController.Instance.electiveList[j]){
                                dropOptions.Add(AcedemicController.Instance.courses[i].courseName + " (H)");
                                courseAdded = true;
                                break;
                            }
                        }
                    }
                    else if(i > 297 && i < 298){
                        dropOptions.Add(AcedemicController.Instance.courses[i].courseName + "E");
                        break;
                    }
                    else{    
                        if(courseAdded != true)
                            dropOptions.Add(AcedemicController.Instance.courses[i].courseName);
                        break;
                    }
                }
            }
        }
        for (int i = 862; i < 879; i++){
            foreach (Character c in profList){
                if (c.skilledCourses.Contains(i)){
                    courseIdValues.Add(i);
                    dropOptions.Add(AcedemicController.Instance.courses[i].courseName);
                    break;
                }
            }
        }
    }

    //onDepartmentChange helper for MATH courses
    public void toMATH(List<string> dropOptions, List<Character> profList){
        List<Character> tempList= new List<Character>();
        foreach(Character c in profList){
            if(c.getMajor() == 12)
                tempList.Add(c);
        }
        if(tempList.Count == 0)
            return;
        else{
            dropOptions.Add("MATH110");
            dropOptions.Add("MATH120");
            dropOptions.Add("MATH130");
            courseIdValues.Add(4);
            courseIdValues.Add(5);
            courseIdValues.Add(6);
            dropOptions.Add("MATH111");
            courseIdValues.Add(14);
            dropOptions.Add("MATH121");
            courseIdValues.Add(15);
            dropOptions.Add("MATH131");
            courseIdValues.Add(16);
            dropOptions.Add("MATH230");
            courseIdValues.Add(28);
            dropOptions.Add("MATH208");
            courseIdValues.Add(32);
        }

        dropHelper(510, 544, 526, dropOptions, profList);
        dropHelper(819, 842, 822, dropOptions, profList);
    }

    public void toECO(List<string> dropOptions, List<Character> profList){
        List<Character> tempList= new List<Character>();
        foreach(Character c in profList){
            if(c.getMajor() == 13)
                tempList.Add(c);
        }
        if(tempList.Count == 0)
            return;
        else{
            dropOptions.Add("ECO101");
            dropOptions.Add("ECO145");
            dropOptions.Add("ECO146");
            dropOptions.Add("ECO147");
            courseIdValues.Add(17);
            courseIdValues.Add(462);
            courseIdValues.Add(470);
            courseIdValues.Add(471);
        }

        dropHelper(479, 507, 489, dropOptions, profList);
        dropHelper(920, 953, 927, dropOptions, profList);
    }

    public void toMKT(List<string> dropOptions, List<Character> profList){
        List<Character> tempList= new List<Character>();
        foreach(Character c in profList){
            if(c.getMajor() == 14)
                tempList.Add(c);
        }
        if(tempList.Count == 0)
            return;
        else{
            dropOptions.Add("MKT111");
            courseIdValues.Add(467);
        }

        dropHelper(543, 559, 545, dropOptions, profList);
    }

    public void toACT(List<string> dropOptions, List<Character> profList){
        List<Character> tempList= new List<Character>();
        foreach(Character c in profList){
            if(c.getMajor() == 15)
                tempList.Add(c);
        }
        if(tempList.Count == 0)
            return;
        else{
            dropOptions.Add("ACT151");
            dropOptions.Add("ACT152");
            courseIdValues.Add(464);
            courseIdValues.Add(465);
        }

        dropHelper(559, 568, 562, dropOptions, profList);
    }

    public void toFIN(List<string> dropOptions, List<Character> profList){
        List<Character> tempList= new List<Character>();
        foreach(Character c in profList){
            if(c.getMajor() == 16)
                tempList.Add(c);
        }
        if(tempList.Count == 0)
            return;
        else{
            dropOptions.Add("FIN125");
            courseIdValues.Add(469);
        }

        dropHelper(568, 577, 571, dropOptions, profList);
        dropHelper(479, 489, 478, dropOptions, profList);
        dropHelper(546, 558, 545, dropOptions, profList);
    }

    public void toBIS(List<string> dropOptions, List<Character> profList){
        List<Character> tempList= new List<Character>();
        foreach(Character c in profList){
            if(c.getMajor() == 17)
                tempList.Add(c);
        }
        if(tempList.Count == 0)
            return;
        else{
            dropOptions.Add("BIS101");
            dropOptions.Add("BIS103");
            dropOptions.Add("BIS144");
            dropOptions.Add("MAN143");
            dropOptions.Add("BIS111");
            dropOptions.Add("MAN186");
            dropOptions.Add("BIS203");
            dropOptions.Add("BIS204");
            courseIdValues.Add(459);
            courseIdValues.Add(460);
            courseIdValues.Add(461);
            courseIdValues.Add(463);
            courseIdValues.Add(468);
            courseIdValues.Add(474);
            courseIdValues.Add(475);
        }

        dropHelper(568, 577, 587, dropOptions, profList);
    }

    public void toLAW(List<string> dropOptions, List<Character> profList){
        List<Character> tempList= new List<Character>();
        foreach(Character c in profList){
            if(c.getMajor() == 18)
                tempList.Add(c);
        }
        if(tempList.Count == 0)
            return;
        else{
            dropOptions.Add("LAW201");
            dropOptions.Add("LAW202");
            courseIdValues.Add(472);
            courseIdValues.Add(473);
        }
    }

    //Updates the semester you are managing and then refreshes the course list and Timeslots if the timeslot menu is active
    public void updateWorkingSemester(bool fall){
        if(semesterFall != fall){
            Debug.Log("Flipping semester in section creation");
            semesterFall = fall;
            onCourseChange();
        }
    }

    //Creates the buttons for sections when a course is selected
    public void onCourseChange(){
        //clear existsing section buttons after skiping other UI element in Courses Panel
        int count = 0;
        foreach (Transform child in coursePanel.transform) {
            if(count > 3)
                GameObject.Destroy(child.gameObject);
            else
                count++;
            
        }
        //Debug.Log(department.GetComponent<Dropdown>().itemText.text + "   and   " + AcedemicController.Instance.courses[0]);
        
        //Go through cource array to find correct course
        //for(int i = 0; i < AcedemicController.Instance.courses.Length; i++){
            //if(AcedemicController.Instance.courses[i] != null){
                if (courseIdValues.Count == 0)
                    return;

                int i = courseIdValues[courses.GetComponent<Dropdown>().value];
                if(AcedemicController.Instance.courses[i] != null){
                //Create buttons for each section
                //Debug.Log(courses.GetComponent<Dropdown>().captionText.text + "   and   " + AcedemicController.Instance.courses[i].courseName);
                //if(courses.GetComponent<Dropdown>().captionText.text.Equals(AcedemicController.Instance.courses[i].courseName)) {
                    Debug.Log("Found matching course");
                    Course course = AcedemicController.Instance.courses[i];
                    sectionBuf = course;
                    updateProfList(course);

                    List<Section> semList = null;
                    if (semesterFall == true)
                        semList = course.sections;
                    else
                        semList = course.sectionsS;

                    foreach(Section s in semList){
                        Debug.Log("section Found");
                        GameObject go = (GameObject)Instantiate(SectionButton);
                        if(ThemeController.Instance.uiTheme == 2){
                            go.transform.GetChild(0).GetComponent<Image>().sprite = ThemeController.Instance.forGround2;
                            go.transform.GetChild(0).GetComponent<Image>().sprite = ThemeController.Instance.forGroundShort2;
                        }

                        go.transform.SetParent(coursePanel.transform, false);
                        go.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = "section " + s.sectionId;
                        //check if zone is missing
                        if(s.getZone() == null){
                               Debug.Log("No zone");
                               UIController.Instance.setInfoBar("A section is missing a classroom");
                               go.GetComponent<Image>().color = Color.red;
                               setRoom.GetComponent<Image>().color = Color.red;
                        }
                        //check if prof is missing
                        if(s.getProf() != null && CharacterController.Instance.firedProfBuf.Contains(s.getProf().getId()) == true){
                            Debug.Log("No prof");
                                UIController.Instance.setInfoBar("A section is missing a professor");
                                go.GetComponent<Image>().color = Color.red;
                                if(setRoom != null)
                                setRoom.GetComponent<Image>().color = Color.red;
                        }
                        //add on Click event to open section menu with section info
                        go.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => {
                            foreach(TimeSlot ts in s.getSlotList()){
                                 Debug.Log("Timeslot: " + ts.getDay() + " " + ts.getStartTime() + " with length " + ts.getLength());
                            }
                            Debug.Log("Section Data: " + s.getCurSeat() + " " + " out of " + s.getSeatCount());
                            UIController.Instance.spawnSectionMenu("Flag");
                            TimeSlotController.Instance.setTotalCourseHours(s);
                            //updateProfList(course);
                            TimeSlotController.Instance.resetTimeSlots();
                            TimeSlotController.Instance.setTimeSlots(s);
                            clickedSection = s;
                            ogSection = s;
                            ogProfInt = s.getProf().getId();
                            overwriteSectionMode = true;
                            BuildingController.Instance.selectedZ = s.getZone();
                            TimeSlotController.Instance.changeInOverWrite = false;
                        });
                        //Add an onClick to delete the section its button on the list
                        go.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => {
                            GameObject.Destroy(go);
                            deleteSection(s, course);
                            });
                    }
                //}
            //}
        }
    }

    //update list of prof that can be assigned to a section
    public void updateProfList(Course course){
        listedProfs.Clear();
        List<string> profStrings = new List<string>();
        profList.GetComponent<Dropdown>().ClearOptions();
        foreach(Character c in CharacterController.Instance.profList){
            if(c.skilledCourses != null && c.skilledCourses.Contains(course.courseId - 1)){
                Debug.Log("Matched skilled courses with " + course.courseId + " " + profStrings.Count);
                //check if matche prof is set to be fire, add to list other wise
                if(CharacterController.Instance.firedProfBuf.Contains(c.getId()) == true ){
                    Debug.Log("Prof Matched skilled courses but is set to be fired " + c.name + " " + profStrings.Count);
                }
                else{
                    listedProfs.Add(c);
                    profStrings.Add(c.name + " (" + c.getSkill() + ")");
                    Debug.Log("Matched skilled courses " + c.name + " " + profStrings.Count);
                }
            }
            else if(c.skilledCourses != null){
                Debug.Log("Prof doesn't have course");
            }
            else{
                Debug.Log("Null skilled courses");
            }
        }
        /*if(AcedemicController.Instance.checkLabId(course.courseId)){
            foreach(Character c in CharacterController.Instance.gradList){
                if(c.skilledCourses != null && c.skilledCourses.Contains(course.courseId - 1)){
                    Debug.Log("Matched skilled courses " + c.name);
                    listedProfs.Add(c);
                    profStrings.Add(c.name);
                }
                else if(c.skilledCourses != null){
                    Debug.Log("Grad Student doesn't have course");
                }
                else{
                    Debug.Log("Null skilled courses");
                }
            }
        }*/
        
        Debug.Log("ProfStrings Count " + profStrings.Count);
        if(profStrings.Count != 0)
            profList.GetComponent<Dropdown>().AddOptions(profStrings);
        else{
            profStrings.Add("No Valid Prof.");
            profList.GetComponent<Dropdown>().AddOptions(profStrings);
        }
    }

    public Character getSelectedCharacter(){
        try{
             return listedProfs[GameObject.Find("SectionProfDropdown").GetComponent<Dropdown>().value];
        }
        catch(ArgumentOutOfRangeException e){
            return null;
        }
    }

    public void changeInSelectedProf(){
        selectedProf = getSelectedCharacter();
        TimeSlotController.Instance.refreshSlots();
        TimeSlotController.Instance.setInvalidSlotsProf(selectedProf);
    }
    //helper to set invalid slots when creating a new section
    public void setInitProfConflict(){
        TimeSlotController.Instance.setInvalidSlotsProf(getSelectedCharacter());
    }

    //assign a section
    public void assignSection(){
        selectedProf = listedProfs[GameObject.Find("SectionProfDropdown").GetComponent<Dropdown>().value];
        if(TimeSlotController.Instance.zoneCheck() == false){
            Debug.Log("Zone check failed 0. Conficting section times");
            UIController.Instance.setInfoBar("No zone selected");
            return;
        }
        // for new section
        if(overwriteSectionMode == false && BuildingController.Instance.selectedZ != null){
            if(TimeSlotController.Instance.changeInOverWrite == false){
                UIController.Instance.setInfoBar("No change in section");
                return;
            }
            Section s = new Section(sectionBuf.courseId, BuildingController.Instance.selectedZ);
            if(TimeSlotController.Instance.semFall)
                s.sectionId = AcedemicController.Instance.courses[s.getCourseId() - 1].sections.Count + 1;
            else
                s.sectionId = AcedemicController.Instance.courses[s.getCourseId() - 1].sectionsS.Count + 1;
            clickedSection = s;
            clickedSection.setZone(BuildingController.Instance.selectedZ);

            //if zone type isn't correct
            if(clickedSection.zoneTypeWrong()){
                Debug.Log("Error assigning section: wrong zone type");
                return;
            }

            //selectedProf.addSection(clickedSection);
            //clickedSection.setZone(BuildingController.Instance.selectedZ);
            clickedSection.setProf(CourseUIController.Instance.selectedProf);
            GoalController.Instance.sectionAddedChecks(clickedSection.getCourseId() - 1, clickedSection.getSeatCount());
            clickedSection.addTimeSlot(TimeSlotController.Instance.getSlotList());
            if(TimeSlotController.Instance.semFall){
                AcedemicController.Instance.courses[clickedSection.getCourseId() - 1].addSection(clickedSection);
                selectedProf.addSection(clickedSection);
            }
            else{
                AcedemicController.Instance.courses[clickedSection.getCourseId() - 1].addSectionS(clickedSection);
                selectedProf.addSectionS(clickedSection);
            }
            TimeSlotController.Instance.resetTimeSlots();
            UIController.Instance.closeSectionPanel();
        }
        //if there is no selected zone
        else if (BuildingController.Instance.selectedZ == null){
            Debug.Log("Error assigning section: no zone selected");
        }
        //if zone type isn't correct for overwrite
        else if(clickedSection != null && clickedSection.zoneTypeWrong()){
            Debug.Log("Error assigning section: wrong zone type");
        }
        //for overwriting an existing section section
        else{
            foreach(Character c in CharacterController.Instance.profList){
                if (c.getId() == ogProfInt){
                    if(semesterFall == true)
                        c.removeSection(ogSection);
                    else
                        c.removeSectionS(ogSection);
                }
            }
            /*TimeSlotController.Instance.setTotalCourseHours(s);
            TimeSlotController.Instance.resetTimeSlots();
            TimeSlotController.Instance.setInvalidSlotsProf();*/
            clickedSection.setProf(CourseUIController.Instance.selectedProf);
            GoalController.Instance.sectionAddedChecks(clickedSection.getCourseId() - 1, clickedSection.getSeatCount());
            clickedSection.addTimeSlot(TimeSlotController.Instance.getSlotList(), "Flag");
            if(TimeSlotController.Instance.semFall){
                AcedemicController.Instance.courses[clickedSection.getCourseId() - 1].sections.Remove(ogSection);
                AcedemicController.Instance.courses[clickedSection.getCourseId() - 1].addSection(clickedSection);
            }
            else{
                AcedemicController.Instance.courses[clickedSection.getCourseId() - 1].sectionsS.Remove(ogSection);
                AcedemicController.Instance.courses[clickedSection.getCourseId() - 1].addSectionS(clickedSection);
            }

            UIController.Instance.closeSectionPanel();
        }
        BuildingController.Instance.selectedZ = null;
    }
    //delete a section in the course data from the button on the Course Panel
    public void deleteSection(Section sDeleting, Course c){
        int i = 0;
        if(TimeSlotController.Instance.semFall){
            foreach(Section s in c.sections){
                Debug.Log("Deleting at " + s.sectionId + " " + sDeleting.sectionId);
                if(s.sectionId == sDeleting.sectionId){
                    c.sections.RemoveAt(i);
                    return;
                }
                else
                    i++;
            }
        }
        else{
            foreach(Section s in c.sectionsS){
                if(s.sectionId == sDeleting.sectionId){
                    c.sectionsS.RemoveAt(i);
                    return;
                }
                else
                    i++;
            }
        }
    }
}