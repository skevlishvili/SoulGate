using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public AudioMixer MasteraudioMixer;

    Resolution[] resolutions;
    public Dropdown resolutionDropdown;

    public Slider Masterslider;
    public Slider Musicslider;
    public Slider Soundslider;

    void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> ResolutionOptions = new List<string>();
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            ResolutionOptions.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(ResolutionOptions);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        Masterslider.value = PlayerPrefs.GetFloat("Master", 0.75f);
        Musicslider.value = PlayerPrefs.GetFloat("Music", 0.75f);
        Soundslider.value = PlayerPrefs.GetFloat("Sound", 0.75f);
    }

    public void SetVolumeMaster(float volume)
    {
        MasteraudioMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
    }
    
    public void SetVolumeMusic(float volume)
    {
        MasteraudioMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
    }
    
    public void SetVolumeSound(float volume)
    {
        MasteraudioMixer.SetFloat("Sound", Mathf.Log10(volume) * 20);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int Index)
    {
        Resolution resolution = resolutions[Index];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
