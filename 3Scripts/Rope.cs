using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : Object
{
    private SpriteRenderer _playerSpriteRenderer;
    private Rigidbody2D _playerRigid;

    public float ropeSpeed;
    
    private void Awake()
    {
        _playerSpriteRenderer = player.GetComponent<SpriteRenderer>();
        _playerRigid = player.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            float v = Input.GetAxisRaw("Vertical");
            if ((int)v == 1)
            {
                if(!player.isRoping)
                    AudioPlay(0);
                player.isRoping = true;
                player.curJumpCnt = 0;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player.isRoping = false;
            player.GetComponent<SpriteRenderer>().sprite = player.sprites[1];
            other.GetComponent<Rigidbody2D>().gravityScale = 5f;
        }
    }

    void Move()
    {
        if (player.isRoping)
        {
            _playerRigid.velocity = Vector2.zero;
            _playerRigid.gravityScale = 0f;
            _playerSpriteRenderer.sprite = player.sprites[2];

            float v = Input.GetAxisRaw("Vertical");
            if ((int)v == 1)
                player.transform.Translate(0, ropeSpeed * Time.deltaTime, 0);
            else if ((int) v == -1)
                player.transform.Translate(0, -1f * ropeSpeed * Time.deltaTime, 0);
            
        }
    }
    
}
