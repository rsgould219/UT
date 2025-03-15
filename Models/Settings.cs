using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings: IXmlSerializable{
    int width, height;
    float scale;
    public Settings(){
    }
    //settings creator
    public Settings(int w, int h, float scale){
        this.width = w;
        this.height = h;
        this.scale = 1.0f + scale * 0.25f;
        Debug.Log("Setting scale at " + this.scale);
    }
    //getters and setters
    public int getWidth(){
        return width;
    }
    public int getHeight(){
        return height;
    }
    public float getScale(){
        return scale;
    }
    public void setFullScreen(int setting){
        if(setting != 0 && Screen.fullScreen != false)
            Screen.fullScreen = false; 
        else if(setting != 1 && Screen.fullScreen != true)
            Screen.fullScreen = true;
    }
    //writer for saving settings and reader for loading settings
    public void WriteXml(XmlWriter writer){
        writer.WriteStartElement("Res");
        writer.WriteAttributeString("width", width.ToString());
        writer.WriteAttributeString("height", height.ToString());
        writer.WriteAttributeString("UIscale", ((scale - 1) / 0.25f).ToString());
        writer.WriteAttributeString("UItheme", ThemeController.Instance.uiTheme.ToString());
        writer.WriteEndElement();
    }
    public void ReadXml(XmlReader reader){
        reader.Read();
        width = int.Parse(reader.GetAttribute("width"));
        height = int.Parse(reader.GetAttribute("height"));
        scale = 1.0f + float.Parse(reader.GetAttribute("UIscale")) * 0.25f;
        //ThemeController.Instance.setUITheme(int.Parse(reader.GetAttribute("UItheme")));
    }
    public XmlSchema GetSchema(){
        return null;
    }
}