using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpSpring : Object
{
    public float jumpPower;
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.maxSpeedY = jumpPower;
        }
    }
}
