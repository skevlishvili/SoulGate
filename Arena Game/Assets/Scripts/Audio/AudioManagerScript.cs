using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerScript : MonoBehaviour
{
    public AudioSource MusicSrc;
    public AudioSource SoundSrc;

    public AudioClip[] MusicClips;
    AudioClip SoundClip;

    // Start is called before the first frame update
    void Start()
    {
        PlayMusic(1);
    }

    public void PlayMusic(int Music_Id)
    {
        MusicSrc.PlayOneShot(MusicClips[Music_Id]);
    }

    //--------------Sound-----------------------------
    public void PlaySound(int Skill_Index, bool AudioRepeating, float StartTime, float RepeatTime)
    {
        SoundClip = Resources.Load<AudioClip>(SkillLibrary.Skills[Skill_Index].Sound);

        if (AudioRepeating)
        {
            InvokeRepeating("RepeatSound", StartTime, RepeatTime);
        }
        else
        {
            SoundSrc.PlayOneShot(SoundClip);
        }

    }

    void RepeatSound()
    {
        SoundSrc.PlayOneShot(SoundClip);
    }
}
