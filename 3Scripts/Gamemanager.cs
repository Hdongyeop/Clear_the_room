using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gamemanager : MonoBehaviour
{
    public Transform mainPos;
    public Player player;
    public Itemmanager itemmanager;
    public Number[] number;
    public GameObject uiPannel;
    public bool isUi;
    public bool pauseAble;
    public bool[] thingList;

    private void LateUpdate()
    {
        Pause();
    }

    void Pause()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && pauseAble)
        {
            if (!isUi)
            {
                isUi = true;
                Time.timeScale = 0f;
                uiPannel.SetActive(true);
                uiPannel.GetComponent<Buttonmanager>().FindButton();
            }
            else
            {
                ExitPannel();
            }
        }    
    }

    public void ExitPannel()
    {
        isUi = false;
        Time.timeScale = 1f;
        uiPannel.SetActive(false);
    }

    void OnEnable()
    {
        // 델리게이트 체인 추가
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        uiPannel = GameObject.Find("UI").transform.Find("Pause UI").gameObject;
        
        player = GameObject.Find("Player").GetComponent<Player>();
        player.PlayerDataLoad();
        if (player.playerData.canDoubleJump)
            player.maxJumpCnt = 2;

        if (scene.name != "Main" && scene.name != "Stage 8")
        {
            itemmanager = GameObject.Find("Item").GetComponent<Itemmanager>();
            player.transform.position = GameObject.Find("RespawnPos").transform.position;    
        }
        
        switch (scene.name)
        {
            case "Main":
                // 메인 로비에서 플레이어 위치
                mainPos = GameObject.Find("Main Pos").transform;
                for (int i = 1; i < number.Length; i++)
                    number[i] = GameObject.Find("Number_" + i).GetComponent<Number>();
                player.transform.position = mainPos.position;

                // 7개의 방 데이터 로드
                for (int i = 1; i < 8; i++)
                {
                    Data loadData = JsonUtility.FromJson<Data>(File.ReadAllText(Application.dataPath + "/StageItem" + i + ".json"));
                    if (loadData != null)
                    {
                        if (loadData.curCoin == loadData.maxCoin && loadData.curDia == loadData.maxDia)
                        {
                            number[i].setSprite(1);
                            if (!loadData.thingBool)
                            {
                                number[i].setSprite(2);
                                thingList[i] = true;
                            }
                        }
                    }
                }
                
                if (thingList[1] && thingList[2] && thingList[3] && thingList[4] &&
                    thingList[5] && thingList[6] && thingList[7])
                {
                    player.playerData.endingAble = true;
                    player.PlayerDataSave();
                }
                
                break;
            case "Stage 1":
                itemmanager.DataLoad(1);
                itemmanager.StateLoad();
                break;
            case "Stage 2":
                itemmanager.DataLoad(2);
                itemmanager.StateLoad();
                break;
            case "Stage 3":
                itemmanager.DataLoad(3);
                itemmanager.StateLoad();
                break;
            case "Stage 4":
                itemmanager.DataLoad(4);
                itemmanager.StateLoad();
                break;
            case "Stage 5":
                itemmanager.DataLoad(5);
                itemmanager.StateLoad();
                break;
            case "Stage 6":
                itemmanager.DataLoad(6);
                itemmanager.StateLoad();
                break;
            case "Stage 7":
                itemmanager.DataLoad(7);
                itemmanager.StateLoad();
                break;
        }
    }

    void OnDisable()
    {
        // 델리게이트 체인 제거
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
