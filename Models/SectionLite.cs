using System;
using System.Xml;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionLite{
    public int sectionId;
    public int courseId;
    public Zone zone;

    public SectionLite(int courseId, Zone z){
        this.courseId = courseId - 1;
        this.zone = z;
    }

    public SectionLite(int courseId, int sectionId, Zone z){
        this.sectionId = sectionId - 1;
        this.courseId = courseId - 1;
        this.zone = z;
    }

    public Section getFullSection(){
        return AcedemicController.Instance.courses[courseId].sections[sectionId];
    }
    //get the length of the class on the current day
    public int getLength(){
        if(WorldController.Instance.world.semFall){
            foreach (TimeSlot ts in AcedemicController.Instance.courses[courseId].sections[sectionId].slotList){
                if(ts.getDay() == WorldController.Instance.getDay())
                    return ts.getLength();
            }
        }
        else{
            foreach (TimeSlot ts in AcedemicController.Instance.courses[courseId].sectionsS[sectionId].slotList){
                if(ts.getDay() == WorldController.Instance.getDay())
                    return ts.getLength();
            }
        }
        return 0;
    }
    public Course getCourse(){
        return AcedemicController.Instance.courses[courseId];
    }
}