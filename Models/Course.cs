using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Course{
    public List<Section> sections = new List<Section>();
    public List<Section> sectionsS = new List<Section>();
    public int courseId;
    public int credits;
    public string courseName;

    public Course(int courseId, string courseName, int credits){
        this.courseId = courseId;
        this.courseName = courseName;
        this.credits = credits;
    }

    public void addSection(Section s){
        sections.Add(s);
    }

    public void addSectionS(Section s){
        sectionsS.Add(s);
    }

    //create a new section and add it the list
    public void newSection(Zone z){
        sections.Add(new Section(courseId, sections.Count + 1, z));
        Debug.Log("added section");
    }

    //get the total count for sections of each semester
    public int countTotal(){
        return sections.Count + sectionsS.Count;
    }

    //get the total number of seats in all sections of the course
    public int seatCount(){
        int count = 0;
        foreach(Section s in sections){
            count += s.getSeatCount();
        }
        return count;
    }
}