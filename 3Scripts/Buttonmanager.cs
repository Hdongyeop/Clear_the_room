using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Buttonmanager : MonoBehaviour
{
    public Gamemanager gamemanager;
    
    public Button exit;
    public Button robby;
    public Button quit;
    public Toggle BGM;
    
    public void FindButton()
    {
        gamemanager = GameObject.FindWithTag("GameManager").GetComponent<Gamemanager>();
        exit = GameObject.Find("Exit Button").GetComponent<Button>();
        exit.onClick.AddListener(gamemanager.ExitPannel);
        robby = GameObject.Find("Robby Button").GetComponent<Button>();
        robby.onClick.AddListener(GotoRobby);
        quit = GameObject.Find("Quit Button").GetComponent<Button>();
        quit.onClick.AddListener(Quit);
        BGM = GameObject.Find("BGM Toggle").GetComponent<Toggle>();
        BGM.onValueChanged.AddListener(PlayBGM);
    }
    
    void GotoRobby()
    {
        gamemanager.ExitPannel();
        SceneManager.LoadScene(0);
    }

    void Quit()
    {
        Application.Quit();
    }

    void PlayBGM(bool on)
    {
        if (!on)
            gamemanager.GetComponent<AudioSource>().mute = true;
        else
            gamemanager.GetComponent<AudioSource>().mute = false;
    }
}
