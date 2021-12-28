using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : Object
{
    private bool _enterStart;
    
    public int doorNum;
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            float v = Input.GetAxisRaw("Vertical");
            if ((int)v == 1 && !_enterStart)
            {
                _enterStart = true;
                player.mainPos.position = player.transform.position;
                StartCoroutine(DoorOpen(doorNum));
            }
        }
    }

    IEnumerator DoorOpen(int n)
    {
        AudioPlay(0);
        player.speed = 0;
        player.jumpPower = 0;
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(n);
    }
}
