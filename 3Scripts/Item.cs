using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public Itemmanager itemmanager;
    
    public enum TYPE
    {
        Coin,
        Diamond,
        Thing
    }
    public TYPE type;
    public int index;
    public AudioSource audioSource;
    public AudioClip[] audioClips;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            AudioPlay(0);
            switch (type)
            {
                case TYPE.Coin:
                    itemmanager.data.coinBool[index] = false;
                    break;
                case TYPE.Diamond:
                    itemmanager.data.diamondBool[index] = false;
                    break;
                case TYPE.Thing:
                    itemmanager.data.thingBool = false;
                    break;
            }
            StartCoroutine(setFalse());
        }
    }

    IEnumerator setFalse()
    {
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }
    
    private void AudioPlay(int n)
    {
        audioSource.clip = audioClips[n];
        audioSource.Play();
    }
}
