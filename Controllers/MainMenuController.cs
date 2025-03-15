using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour{
    //main menu panels
    public GameObject mainMenu, settingsMenu ,loadMenu , menuCan;
    //Settings objects
    public GameObject resolutionBar, uiScaleBar, screenBar;
    public bool mainActive, loadActive = false, settingsActive = false;
    public int width, height, scale;
    // Start is called before the first frame update
    void Start(){
        width = (int)menuCan.GetComponent<CanvasScaler>().referenceResolution.x;
        height = (int)menuCan.GetComponent<CanvasScaler>().referenceResolution.y;
    }
    public void openSettings(){
        if(settingsActive == false){
            settingsActive = true;
            hideMain();
            settingsMenu.gameObject.SetActive(true);
        }
    }
    public void closeSettings(){
        if(settingsActive == true){
            settingsActive = false;
            showMain();
            settingsMenu.gameObject.SetActive(false);
        }
    }
    public void openLoad(){
        if(loadActive == false){
            loadActive = true;
            hideMain();
            loadMenu.gameObject.SetActive(true);
            loadMenu.transform.GetChild(1).GetComponent<ScrollRect>().normalizedPosition = new Vector2(0, 1);
        }
    }
    public void closeLoad(){
        if(loadActive == true){
            loadActive = false;
            showMain();
            loadMenu.gameObject.SetActive(false);
            SaveController.Instance.modeSave = false;
        }
    }
    public void setCorrectButtonText(){
        loadMenu.transform.GetChild(0).GetComponent<Text>().text = "Load";
    }
    public void hideMain(){
            mainActive = false;
            mainMenu.gameObject.SetActive(false);
    }
    public void showMain(){
            mainActive = true;
            mainMenu.gameObject.SetActive(true);
    }
    //Adjust screen resolution and ui
    public void setResolution(){
        switch(resolutionBar.GetComponent<Dropdown>().value){
            case 0:
                width = 1280;
                height = 720;
                break;
            case 1:
                width = 1600;
                height = 900;
                break;
            case 2:
                width = 1920;
                height = 1080;
                break;
        }
        scale = uiScaleBar.GetComponent<Dropdown>().value + 1;
        menuCan.GetComponent<CanvasScaler>().referenceResolution = new Vector2(width, height);
        mainMenu.GetComponent<RectTransform>().SetRight(width / 4.0f);
        mainMenu.GetComponent<RectTransform>().SetLeft(width / 4.0f);
        mainMenu.GetComponent<RectTransform>().SetTop(width / 10.0f);
    }
    //Start a new game from the main menu
    public void newGame(){
        SceneManager.LoadScene("Main");
        SaveController.Instance.saveName= "";
    }
    public void loadGame(){
        SceneManager.LoadScene("Main");
        SaveController.Instance.setTransfer();
    }
    //dev tool nextDay
    public void nextDay(){
        WorldController.Instance.nextDay();
    }
    //apply ui settings
    public void applySettings(){
        setResolution();
        XmlSerializer serializer = new XmlSerializer(typeof(Settings));
        Settings s = new Settings(width, height, scale);
        TextWriter writer = new StringWriter();
        serializer.Serialize(writer, s);
        writer.Close();
        Debug.Log(writer.ToString());
        File.WriteAllText("Settings.xml", writer.ToString());
        s.setFullScreen(screenBar.GetComponent<Dropdown>().value);
    }
    //quit Application
    public void quitApp(){
        Application.Quit();
    }
}