using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    #region Referances
    public GameObject ShopSkillUi_PrefabObj; // This is our prefab object that will be exposed in the inspector
    public GameObject Player_PrefabObj;
    PlayerAction playerActionObj;
    public SkillLibrary SkillLibraryObj;

    public Text DetailedSkillInfo;
    public Image SkillUiImageDetailed;


    [Tooltip("The UI Label to inform the user that the connection is in progress")]
    [SerializeField]
    private MainMenu Menu;
    #endregion



    public int SkillQuantity; // number of objects to create. 
    int _SkillIndex;

    Skill[] Skills;

    void Start()
    {
        playerActionObj = Player_PrefabObj.GetComponentInChildren<PlayerAction>();
        SkillLibrary.Skills.OrderBy(x => x.SkillPriceMoney).ToArray();
        Skills = SkillLibrary.Skills;
        SkillQuantity = Skills.Length;

        SkillScrollContentFill();
    }

    void SkillScrollContentFill()
    {
        GameObject newObj;
        SkillQuantity = Skills.Length;


        for (int i = 0; i < SkillQuantity; i++)
        {
            // Create new instances of our prefab until we've created as many as we specified
            newObj = (GameObject)Instantiate(ShopSkillUi_PrefabObj, transform);
            Image newObjImage = newObj.GetComponent<Image>();
            newObjImage.sprite = SkillLibrary.Skills[i].SkillImageUIVFX;
        }

    }

    public void DetailedInformation(Image img)
    {
        for (int i = 0; i < Skills.Length; i++)
        {
            if (img.sprite.name == Skills[i].SkillImageUIVFX.name)
            {
                _SkillIndex = i;
            }
        }

        Skill Spell = Skills[_SkillIndex];

        SkillUiImageDetailed.sprite = Skills[_SkillIndex].SkillImageUIVFX;

        string Damage = "";
        if (Spell.PhysicalDamage != 0)
        {
            Damage += $"Physical: {Spell.PhysicalDamage},";
        }
        if (Spell.MagicDamage != 0)
        {
            Damage += $" Magical: {Spell.MagicDamage},";
        }
        if (Spell.SoulDamage != 0)
        {
            Damage += $" Soul: {Spell.SoulDamage}";
        }

        string Consumption = "";
        if (Spell.HealthConsumption != 0)
        {
            Consumption += $"Health: {Spell.HealthConsumption},";
        }
        if (Spell.ManaConsumption != 0)
        {
            Consumption += $" Mana: {Spell.ManaConsumption}";
        }

        string Effects = "";
        if (Spell.IsRestraining)
        {
            Effects += "Restraining,";
        }
        if (Spell.IsInvisible)
        {
            Effects += " Invisible,";
        }
        if (Spell.IsPasive)
        {
            Effects += " Pasive,";
        }
        if (Spell.IsBuff)
        {
            Effects += " Buff/debuff,";
        }
        if (Spell.IsProjectile)
        {
            Effects += " Projectile,";
        }
        if (Spell.HasRecharging)
        {
            Effects += " Recharging,";
        }

        string DetailedInfo = $"Description: {Spell.SkillName}\n\nDamage: {Damage}\n\nConsumtion: {Consumption}\n\nCooldown: {Spell.Cooldown}\n\nEffects: {Effects }";
        DetailedSkillInfo.text = DetailedInfo;
    }

    public void SearchDropDownChange(Dropdown value)
    {
        if (value.value == 0)
        {
            SkillLibrary.Skills.OrderBy(x => x.SkillPriceMoney).ToArray();
        }
        else if (value.value == 1)
        {
            SkillLibrary.Skills.OrderBy(x => x.SkillName).ToArray();
        }
        else if (value.value == 2)
        {
            SkillLibrary.Skills.OrderBy(x => x.PhysicalDamage).ToArray();
        }
        else if (value.value == 3)
        {
            SkillLibrary.Skills.OrderBy(x => x.MagicDamage).ToArray();
        }
        else if (value.value == 3)
        {
            SkillLibrary.Skills.OrderBy(x => x.SoulDamage).ToArray();
        }
    }

    public void BuySkill(Image img)
    {
        Menu.OpenMenu("ChooseSkillIndexPanel");
    }

    public void ChangeSkillIndex(int index)
    {
        playerActionObj.PlayerSkills[index] = _SkillIndex;
        Menu.OpenMenu("");//if it is empty it will close everything
    }

    void LoadVisualDataHud()
    {

    }
}
