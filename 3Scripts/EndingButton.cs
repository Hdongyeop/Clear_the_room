using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingButton : MonoBehaviour
{
    public GameObject password;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            password.SetActive(true);
            password.GetComponent<Password>().isPassword = true;
            Time.timeScale = 0f;
        }
    }
}
