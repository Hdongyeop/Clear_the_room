using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    public AudioSource _audioSource;
    public AudioClip[] audioClips;
    public Player player;
    
    protected void AudioPlay(int n)
    {
        _audioSource.clip = audioClips[n];
        _audioSource.Play();
    }
}
