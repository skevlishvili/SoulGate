using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudConetntController : NetworkBehaviour
{

    #region Referances
    public Abillities Abillities;

    public Image[] imagesObj;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        LoadSkillUiImagesInHUD();
    }

    public void LoadSkillUiImagesInHUD()
    {
        
        for (int i = 0; i < imagesObj.Length; i++)
        {
            Debug.Log(i);
            Debug.Log(Abillities.PlayerAbillities[i / 2]);
            Debug.Log(Abillities.PlayerAbillities[i / 2].Skill.SkillImageUIVFX);
            imagesObj[i].sprite = Abillities.PlayerAbillities[i/2].Skill.SkillImageUIVFX;
        }
    }
}
