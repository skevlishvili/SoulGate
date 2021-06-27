using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudContentController : NetworkBehaviour
{

    #region Referances
    public Abillities Abillities;

    public Unit UnitStats;
    public Text[] UnitStatTexts;

    public Image[] ActiveSkillimagesObj;
    public Image[] PassiveSkillimagesObj;

    //public
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        LoadSkillUiImagesInHUD();
        LoadPassiveSkillUiImagesInHUD();
        LoadUnitStats();
    }
    private void Update()
    {
        LoadUnitStats();
    }

    public void LoadSkillUiImagesInHUD()
    {        
        for (int i = 0; i < ActiveSkillimagesObj.Length; i++)
        {
            ActiveSkillimagesObj[i].sprite = Abillities.PlayerAbillities[i/2].Skill.SkillImageUIVFX;
        }
    }

    public void LoadPassiveSkillUiImagesInHUD()
    {
        for (int i = 0; i < 5; i++)
        {
            if (Abillities.PlayerPassives[i].Skill != null)
            {
                PassiveSkillimagesObj[i].gameObject.SetActive(true);
                PassiveSkillimagesObj[i + 5].gameObject.SetActive(true);

                PassiveSkillimagesObj[i].sprite = Abillities.PlayerPassives[i].Skill.SkillImageUIVFX;
            }
            else
            {
                PassiveSkillimagesObj[i].gameObject.SetActive(false);
                PassiveSkillimagesObj[i + 5].gameObject.SetActive(false);
            }
        }
    }

    public void LoadUnitStats()
    {
        UnitStatTexts[0].text = UnitStats.Damage.ToString();
        UnitStatTexts[1].text = UnitStats.PhysicalDefence.ToString();
        UnitStatTexts[2].text = UnitStats.MagicDefence.ToString();
        UnitStatTexts[3].text = Mathf.FloorToInt(UnitStats.Money).ToString();
    }
}
