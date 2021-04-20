using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudConetntController : MonoBehaviour
{

    #region Referances
    public SkillLibrary SkillLibraryObj;

    public GameObject Player_PrefabObj;
    PlayerAction PlayerActionObj;

    public Image[] imagesObj;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        LoadSkillUiImagesInHUD();
    }

    public void LoadSkillUiImagesInHUD()
    {
        Player_PrefabObj = GameObject.FindGameObjectWithTag("Player");
        PlayerActionObj = Player_PrefabObj.GetComponentInChildren<PlayerAction>();
        var HudContent = Player_PrefabObj.GetComponentInChildren<HudConetntController>();

        for (int i = 0; i < imagesObj.Length; i++)
        {
            HudContent.imagesObj[i].sprite = SkillLibrary.Skills[PlayerActionObj.PlayerSkills[i/2]].SkillImageUIVFX;
        }
    }
}
