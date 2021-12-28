using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Flag : Object
{
    public Itemmanager _itemmanager;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Clear());
        }
    }

    IEnumerator Save()
    {
        _itemmanager.StateSave();
        _itemmanager.DataSave();
        player.PlayerDataSave();
        yield return null;
    }
    
    IEnumerator Clear()
    {
        AudioPlay(0);
        player.speed = 0;
        player.jumpPower = 0;
        yield return StartCoroutine(Save());
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(0);
    }
}
