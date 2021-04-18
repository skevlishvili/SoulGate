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
        LoadSkillUiImages();
    }

    public void LoadSkillUiImages()
    {
        Player_PrefabObj = GameObject.FindGameObjectWithTag("Player");
        PlayerActionObj = Player_PrefabObj.GetComponentInChildren<PlayerAction>();

        for (int i = 0; i < imagesObj.Length; i++)
        {
            imagesObj[i].sprite = SkillLibrary.Skills[PlayerActionObj.PlayerSkills[i/2]].SkillImageUIVFX;
        }
    }
}
