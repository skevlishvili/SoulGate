﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public AudioClip FireSound;
    public AudioSource audioSrc;

    // Start is called before the first frame update
    void Start()
    {
        FireSound = Resources.Load<AudioClip>("fire");
        
        audioSrc = GetComponent<AudioSource>();
        Debug.Log(audioSrc);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound(string clip) {
        Debug.Log(audioSrc);

        switch(clip) {
            case "fire":
                audioSrc.PlayOneShot(FireSound);
                break;
        }
    }
}
