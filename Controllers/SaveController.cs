using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveController : MonoBehaviour{
    public static SaveController Instance;
    public GameObject savePanel;
    //SaveSlice is the prefab for save file info in the save menu
    public GameObject SaveSlice;
    public bool loadConfirmPanelActive = false;
    public GameObject loadConfirmPanel;
    
    public string saveName = "";
    static string saveNameToTransfer;
    public bool modeSave = true; 
    List<string> saveNameList = new List<string>();
    void Start() {
        Instance = this;
    }

    //refresh the info for save files on the save menu
    public void refreshSaves(){
        DirectoryInfo dataFolder = new DirectoryInfo("Saves");
        FileInfo[] dataFiles = dataFolder.GetFiles();

        //clear buffer and scroll view
        saveNameList.Clear();
 
        foreach (Transform child in savePanel.transform.GetChild(1).GetChild(0).GetChild(0).transform) {
            GameObject.Destroy(child.gameObject);
        }

        //crating slices for each save file
        foreach (FileInfo f in dataFiles){
            GameObject go = (GameObject)Instantiate(SaveSlice);
            go.transform.SetParent(savePanel.transform.GetChild(1).GetChild(0).GetChild(0));
            go.transform.GetChild(0).GetComponent<Text>().text = f.Name;
            if(ThemeController.Instance.uiTheme == 3){
                go.transform.GetChild(0).GetComponent<Text>().color = Color.white;
                go.transform.GetComponent<Image>().sprite = ThemeController.Instance.forGround3;
            }
            else if(ThemeController.Instance.uiTheme == 2){
                go.transform.GetComponent<Image>().sprite = ThemeController.Instance.forGround2;
                go.transform.GetChild(0).GetComponent<Text>().color = new Color(0.196f, 0.196f, 0.196f, 1f);
            }
            else{
                go.transform.GetComponent<Image>().sprite = ThemeController.Instance.forGround1;
                go.transform.GetChild(0).GetComponent<Text>().color = new Color(0.196f, 0.196f, 0.196f, 1f);
            }
            saveNameList.Add(f.Name);
            //set the saved save name as the name for the clicked save file
            go.transform.GetComponent<Button>().onClick.AddListener(() => {
                saveName = f.Name;
                savePanel.transform.GetChild(2).GetComponent<InputField>().text = f.Name;
            });
        }

        //Set Button text to save or load depending on the set mode
        if(modeSave)
            savePanel.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "Save";
        else
            savePanel.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "Load";
    }
    //attempt to save or load depending on the set mode
    public void actionAttempt(){
        if(modeSave)
            saveAttempt();
        else
            loadAttempt();
    }
    //attempt to save the game with the buffer save name
    public void saveAttempt(){
        //if the input field was changed since the name in buffer was set via button press or last save
        saveName = savePanel.transform.GetChild(2).GetComponent<InputField>().text;

        //spawn save confirm menu of a save by that name exists or save directly
        if(saveNameList.Contains(saveName))
            UIController.Instance.spawnSaveConfirmMenu();
        else
            WorldController.Instance.saveWorld();
    }
    //attempt to load the game
    public void loadAttempt(){
        UIController.Instance.spawnLoadConfirmMenu();
    }
    public void spawnLoadConfirmMenuMM(){
        if(loadConfirmPanelActive){
            loadConfirmPanel.gameObject.SetActive(false);
            loadConfirmPanelActive = false;
        }
        else{
            loadConfirmPanel.gameObject.SetActive(true);
            loadConfirmPanelActive = true;
            //if(lcOpened != true)
                //setLoadConfirmScale();
       }
    }
    public void setTransfer(){
        saveNameToTransfer = saveName;
    }
    public String getTransfer(){
        return saveNameToTransfer;
    }
    public void recallTranfser(){
        saveName = saveNameToTransfer;
    }
}