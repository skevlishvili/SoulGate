using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public AudioClip Fireball_Sound;
    public AudioClip HealthRegen_Sound;
    public AudioSource audioSrc;

    // Start is called before the first frame update
    void Start()
    {
        Fireball_Sound = Resources.Load<AudioClip>("Design/Music/Fireball_Sound");
        HealthRegen_Sound = Resources.Load<AudioClip>("Design/Music/HealthRegen_Sound");
        audioSrc = gameObject.GetComponent<AudioSource>();
    }

    public void PlaySound(string clip) {
        switch(clip) {
            case "Fireball_Sound":
                audioSrc.PlayOneShot(Fireball_Sound);
                break;
            case "HealthRegen_Sound":
                audioSrc.PlayOneShot(HealthRegen_Sound);
                break;
        }
    }
}
