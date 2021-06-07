using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Sound: MonoBehaviour
{
    public int Skill_Index = 0;
    public bool Repeating = true;
    public float RepeatTime = 1.0f;
    public float StartTime = 0.0f;
    public bool RandomVolume;
    public float minVolume = .4f;
    public float maxVolume = 1f;
    private AudioClip clip;
    private AudioSource soundComponent;

    void Start ()
    {
        soundComponent = GetComponent<AudioSource>();
        //clip = soundComponent.clip;
        clip = Resources.Load<AudioClip>(SkillLibrary.Skills[Skill_Index].Sound);
        if (RandomVolume == true)
        {
            soundComponent.volume = Random.Range(minVolume, maxVolume);
            RepeatSound();
        }
        if (Repeating == true)
        {
            InvokeRepeating("RepeatSound", StartTime, RepeatTime);
        }
    }

    void RepeatSound()
    {
        soundComponent.PlayOneShot(clip);
    }
}
