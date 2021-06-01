using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudContentController : NetworkBehaviour
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
            imagesObj[i].sprite = Abillities.PlayerAbillities[i/2].Skill.SkillImageUIVFX;
        }
    }
}
