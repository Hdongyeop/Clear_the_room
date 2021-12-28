using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Endingmanager : MonoBehaviour
{
    public Player player;
    public GameObject endingDoor;
    
    private void Start()
    {
        PlayerData data = player.PlayerDataReturn();
        Debug.Log(data.canDoubleJump + " " + data.endingAble);
        if(data.endingAble)
            endingDoor.SetActive(true);
    }
}
