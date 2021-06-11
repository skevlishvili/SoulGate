using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerScript : MonoBehaviour
{
    public AudioSource MusicSrc;
    public AudioSource SoundSrc;

    public AudioClip[] MusicClips;
    AudioClip SoundClip;

    public int Start_Music_Id = 0;

    // Start is called before the first frame update
    void Start()
    {
        PlayMusic(Start_Music_Id);
    }

    void Update()
    {
        if (!MusicSrc.isPlaying)
        {
            System.Random random = new System.Random();
            int randomNumber = random.Next(0, MusicClips.Length);

            MusicSrc.PlayOneShot(MusicClips[randomNumber]);
        }
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

    public void PlaySoundPath(string Clip_Path)
    {
        SoundClip = Resources.Load<AudioClip>(Clip_Path);
        SoundSrc.PlayOneShot(SoundClip);
    }

    void RepeatSound()
    {
        SoundSrc.PlayOneShot(SoundClip);
    }
}
