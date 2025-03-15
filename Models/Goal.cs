using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal{
    int complexFlag;
    int totalFlag;
    int humanityFlag;
    int electiveFlag;
    int upFrontFunds;
    public string name;
    public string desc;
    
    List<int> courses = new List<int>();
    List<List<int>> complexCourses = new List<List<int>>();
    List<int> seatGoals = new List<int>();
    List<int> complexGoals = new List<int>();
    List<int> extrasGoals = new List<int>();
    List<Tuple<int, int, int>> rangeGoals = new List<Tuple<int, int, int>>();

    public Goal(string name, string desc, int funds = 0){
        this.name = name;
        this.desc = desc;
        this.humanityFlag = 0;
        this.electiveFlag = 0;
        this.totalFlag = 0;
        this.upFrontFunds = funds;
    }
    //empty goal contructor used for scheduling
    public Goal(){
    }

    //add a simple course goal
    public void addCourseGoal(int course, int goal){
        courses.Add(course);
        seatGoals.Add(goal);
    }

    //adds a simple course goal with a hunanity mark
    public void addCourseGoal(string flag, int goal){
        if(flag.StartsWith("H")){
            humanityFlag += goal;
        }
        if(flag.StartsWith("E")){
            electiveFlag += goal;
        }
    }

    //complex goal where there is a choice over 2 semester, where each option must be taken over the two
    public void addComplexGoal(List<int> courses, int goal){
        complexCourses.Add(courses);
        complexGoals.Add(goal);
        complexFlag = 1;
    }

    public void addComplexGoal(List<int> courses, int goal, string flag){
        complexCourses.Add(courses);
        complexGoals.Add(goal);
        totalFlag = courses.Count + complexCourses.Count;
        complexFlag = 2;
    }

    public void addRangeGoal(int start, int end, int count){
        rangeGoals.Add(new Tuple<int, int, int>(start, end, count));
    }

    public void removeCourseGoal(int id){
        int i = courses.IndexOf(id);
        seatGoals.RemoveAt(i);
        courses.RemoveAt(i);
    }

    public List<int> getCourseList(){
        return courses;
    }

    public List<int> getComplexSeatGoals(){
        return complexGoals;
    }

    public List<List<int>> getComplexList(){
        return complexCourses;
    }

    public int getComplexFlag(){
        return complexFlag;
    }
    public int getHCheck(){
        return humanityFlag;
    }
    public int getECheck(){
        return electiveFlag;
    }
    public List<int> getExtraGoals(){
        return extrasGoals;
    }

    public List<int> getSeatGoals(){
        return seatGoals;
    }

    public List<Tuple<int, int, int>> getRangeGoals(){
        return rangeGoals;
    }

    public void removeHGoal(){
        humanityFlag = 0;
    }

    public void removeCGoal(int i){
        complexCourses.RemoveAt(i);
        complexGoals.RemoveAt(i);
    }

    public void removeEGoal(){ 
        electiveFlag = 0;
    }
    public void reduceHGoal(int i){
        humanityFlag -= i;
    }
    public void reduceEGoal(int i){
        electiveFlag -= i;
    }
    public void removeExtra(int i){
        extrasGoals.Remove(i);
    }

    public void setHGoal(int i){
        humanityFlag = i;
    }
    public void setEGoal(int i){
        electiveFlag = i;
    }
    public void setExtraGoal(int i){
        extrasGoals.Add(i);
    }
    public void setRangeGoal(int count, int start, int end, int seats){
        rangeGoals[count] = new Tuple<int, int, int>(start, end, seats);
    }

    public bool checkTotalFlag(){
        if(totalFlag != 0){
            return true;
        }
        return false;
    }

    public int checkComplexFlag(){
        if (complexFlag != 0){
            return complexFlag;
        }
        else{
            return 1;
        }
    }
}