using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJump : Object
{
    public PlayerData playerData;
    private void Awake()
    {
        player.PlayerDataLoad();
        if(player.playerData.canDoubleJump)
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            AudioPlay(0);
            player.playerData.canDoubleJump = true;
            player.maxJumpCnt = 2;
            StartCoroutine(setFalse());
        }
    }

    IEnumerator setFalse()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }
}
