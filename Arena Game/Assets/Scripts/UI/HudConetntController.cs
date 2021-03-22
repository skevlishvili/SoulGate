using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudConetntController : MonoBehaviour
{

    #region Referances
    public SkillLibrary SkillLibraryObj;
    public PlayerAction PlayerActionObj;

    public Image AB1;
    public Image AB1_Overlay;
    public Image AB2;
    public Image AB2_Overlay;
    public Image AB3;
    public Image AB3_Overlay;
    public Image AB4;
    public Image AB4_Overlay;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        LoadSkillUiImages();
    }

    private void LoadSkillUiImages()
    {
        AB1.sprite = SkillLibrary.Skills[PlayerActionObj.PlayerSkills[0]].SkillImageUIVFX;
        AB1_Overlay.sprite = SkillLibrary.Skills[PlayerActionObj.PlayerSkills[0]].SkillImageUIVFX;
    }
}
