using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public AudioClip Fireball_Sound;
    public AudioSource audioSrc;

    // Start is called before the first frame update
    void Start()
    {
        Fireball_Sound = Resources.Load<AudioClip>("Design/Music/Resources/Fireball_Sound");
        
        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound(string clip) {
        switch(clip) {
            case "Fireball_Sound":
                audioSrc.PlayOneShot(Fireball_Sound);
                break;
        }
    }
}
