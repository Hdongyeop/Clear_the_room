using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Number : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    public Sprite[] sprites;
    
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void setSprite(int n)
    {
        _spriteRenderer.sprite = sprites[n];
    }

}
