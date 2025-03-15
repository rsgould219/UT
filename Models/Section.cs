using System;
using System.Xml;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Section
{
    public int sectionId;
    Zone classRoom;
    int courseId;
    Character prof;
    int curSeatCount;
    int roomSeatCount;
    //buf for scheduling
    int bufSeatCount = 0;
    public List<TimeSlot> slotList = new List<TimeSlot>();

    public Section(int courseId, Zone z){
        this.courseId = courseId;
        setZone(z);
    }

    public Section(int courseId, int sectionId, Zone z){
        this.sectionId = sectionId;
        this.courseId = courseId;
        setZone(z);
    }

    //set the class zone as the assigned class for this section
    public void setZone(Zone z){
        Debug.Log("Setting zone " + z.getId());  
        UIController.Instance.setInfoBar("Setting zone " + z.getId() + " as class for section " + sectionId +" with " + z.getSeatCount() + " seats");  
        this.classRoom = z;
        this.roomSeatCount = z.getSeatCount();
        this.curSeatCount = 0;
    }

    
    public Zone getZone(){
        return classRoom;
    }
    public void removeZone(){
        this.classRoom = null;
    }
    public int getSeatCount(){
        return this.roomSeatCount;
    }
    public int getCurSeat(){
        return this.curSeatCount;
    }
    public int getCourseId(){
        return this.courseId;
    }
    public int getSectionId(){
        return this.sectionId;
    }

    public int getBufCount(){
        return bufSeatCount;
    }
    public void setCurSeatCount(int i){
        this.curSeatCount = i;
    }
    public void setRoomSeatCount(int i){
        this.roomSeatCount = i;
    }
    public void incrementSeatCount(){
        curSeatCount++;
    }
    public void incrementBufCount(){
        bufSeatCount++;
    } 
    public List<TimeSlot> getSlotList(){
        return slotList;
    }
    public void setProf(Character p){
        prof = p;
    }  
    public Character getProf(){
        return this.prof;
    }
    //clears scheduling buf
    public void clearBuf(){
        bufSeatCount = 0;
    }

    //adds a timeslot list as the section's list
    public void addTimeSlot(List<TimeSlot> sl){
        foreach(TimeSlot ts in sl){
            slotList.Add(ts);
            Debug.Log("Timeslot: " + ts.getDay() + " " + ts.getStartTime() + " with length " + ts.getLength());
        }
    }

    //adds a timeslot list as the section's list, but also clears
    public void addTimeSlot(List<TimeSlot> sl, string flag){
        slotList.Clear();
        foreach(TimeSlot ts in sl){
            slotList.Add(ts);
            Debug.Log("Timeslot: " + ts.getDay() + " " + ts.getStartTime() + " with length " + ts.getLength());
        }
    }

    //returns a timeslot given a timeslot gameobject name
    public  TimeSlot getTimeSlot(string s){
        int day = Int32.Parse(s.Substring(8, 1));
        int startTime = Int32.Parse(s.Substring(10, 1));
        foreach(TimeSlot ts in slotList){
            if(ts.getDay() == day && ts.getStartTime() == startTime){
                return ts;
            }
        }
        return null;
    }

    public void removeTimeSlot(int day, int startTime){
        slotList.Remove(new TimeSlot(day, startTime, 2));
        slotList.Remove(new TimeSlot(day, startTime, 3));
    }   

    public bool zoneTypeWrong(){
        bool labId = AcedemicController.Instance.checkLabId(courseId);
        bool typeCheck;
        if (BuildingController.Instance.selectedZ.getType() == Zone.ZoneType.Lab)
            typeCheck = true;
        else
            typeCheck = false;

        if(labId && typeCheck)
            return false;
        else if (!(labId) && !(typeCheck))
            return false;
        else
            return true;
    }  

    public void writeXml(XmlWriter writer){
         writer.WriteAttributeString("cId", this.courseId.ToString());
         writer.WriteAttributeString("sId", this.sectionId.ToString());
         writer.WriteAttributeString("curSeat", this.curSeatCount.ToString());
         writer.WriteAttributeString("totalSeat", this.roomSeatCount.ToString());
         if(classRoom != null){
              writer.WriteAttributeString("zId", classRoom.getId().ToString());
         }
         if(prof != null){
              writer.WriteAttributeString("pId", prof.getId().ToString());
         }
         writer.WriteEndElement();
         foreach(TimeSlot ts in slotList){
             ts.writeXml(writer);
         }
    }

    public void readXml(XmlReader reader){
        Debug.Log("reading sectionxml");
        this.sectionId = int.Parse(reader.GetAttribute("sId"));
        setCurSeatCount(int.Parse(reader.GetAttribute("curSeat")));
        setRoomSeatCount(int.Parse(reader.GetAttribute("totalSeat")));
        int i = int.Parse(reader.GetAttribute("zId"));
        foreach(Character c in CharacterController.Instance.profList){
            Debug.Log("matching saved section with pId" + (reader.GetAttribute("pId") + 1) + " and cId " + c.getId());
            if(int.Parse(reader.GetAttribute("pId")) + 1 == c.getId())
                this.prof = c;
        }
        foreach(Character c in CharacterController.Instance.charactersActive){
            if(int.Parse(reader.GetAttribute("pId")) + 1 == c.getId())
                this.prof = c;
        }
    }

    public void readXmlTS(XmlReader reader){
        TimeSlot ts = new TimeSlot(
                                    int.Parse(reader.GetAttribute("day")),
                                    int.Parse(reader.GetAttribute("time")),
                                    int.Parse(reader.GetAttribute("length"))
        );
        slotList.Add(ts);
    }
}