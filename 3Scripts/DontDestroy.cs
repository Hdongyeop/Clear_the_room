using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    private void Awake()
    {
        // 중복 오브젝트 제거
        var mainPos = GameObject.FindGameObjectsWithTag("MainPos");
        if (mainPos.Length == 1) { DontDestroyOnLoad(mainPos[0]); } 
        else { Destroy(mainPos[1]); }
        
        var gameManager = GameObject.FindGameObjectsWithTag("GameManager");
        if (gameManager.Length == 1) { DontDestroyOnLoad(gameManager[0]); } 
        else { Destroy(gameManager[1]); }

        var ui = GameObject.FindGameObjectsWithTag("UI");
        if (ui.Length == 1) { DontDestroyOnLoad(ui[0]); } 
        else { Destroy(ui[1]); }
    }
}
