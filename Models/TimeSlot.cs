using System.Collections;
using System;
using System.Xml;
using System.Collections.Generic;
using UnityEngine;

public class TimeSlot{
    int day;
    int startTime;
    int length;

    public TimeSlot(int day, int startTime, int length){
        this.day = day;
        this.startTime = startTime;
        this.length = length;
    }

    public Tuple<int, int, int> getTimeData(){
        return new Tuple<int, int, int>(this.day, this.startTime, this.length);
    }

    public int getDay(){
        return this.day;
    }
    public int getStartTime(){
        return this.startTime;
    }
    public int getLength(){
        return this.length;
    }
    public void setLength(int i){
        this.length = i;
    }

    public void writeXml(XmlWriter writer){
         writer.WriteStartElement("TS");
         writer.WriteAttributeString("day", this.day.ToString());
         writer.WriteAttributeString("time", this.startTime.ToString());
         writer.WriteAttributeString("length", this.length.ToString());
         writer.WriteEndElement();
    }
}