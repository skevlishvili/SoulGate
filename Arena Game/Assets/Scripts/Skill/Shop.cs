using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    #region Referances
    public GameObject Shop_Content;
    public GameObject ShopSkillUi_PrefabObj; // This is our prefab object that will be exposed in the inspector
    public GameObject hudObj;
    GameObject Player_PrefabObj;
    PlayerAction playerActionObj;

    public GameObject DetailedPanel;
    public Text DetailedSkillInfo;
    public Image SkillUiImageDetailed;

    [Tooltip("The UI Label to inform the user that the connection is in progress")]
    [SerializeField]
    private MainMenu Menu;

    public Image[] imagesObj;
    public Dropdown[] DropdownObj;
    #endregion

    int _SkillIndex;
    Skill[] Skills_Shop;
    static Skill Spell;

    void OnEnable()
    {
        Player_PrefabObj = GameObject.FindGameObjectWithTag("Player");
        playerActionObj = Player_PrefabObj.GetComponentInChildren<PlayerAction>();
        Skills_Shop = SkillLibrary.Skills.OrderBy(x => x.SkillPriceMoney).ToArray();
        SkillScrollContentFill();
        LoadSkillUiImages();
    }

    void SkillScrollContentFill()
    {
        foreach (Transform child in Shop_Content.transform)
        {
            Destroy(child.gameObject);
        }

        GameObject newObj;

        for (int i = 1; i < Skills_Shop.Length; i++)
        {
            // Create new instances of our prefab until we've created as many as we specified
            newObj = Instantiate(ShopSkillUi_PrefabObj, transform);

            Image newObjImage = newObj.GetComponent<Image>();
            Text[] mewObjText = newObj.GetComponentsInChildren<Text>();

            newObjImage.sprite = Skills_Shop[i].SkillImageUIVFX;
            mewObjText[0].text = Skills_Shop[i].SkillPriceMoney.ToString();
            mewObjText[1].text = Skills_Shop[i].SkillName;

            newObj.transform.SetParent(Shop_Content.transform, false);
        }
    }

    public void DetailedInformation(Text value)
    {
        _SkillIndex = HelpMethods.GetSkillIndexByName(value.text);

        Spell = SkillLibrary.Skills[_SkillIndex];
        SkillUiImageDetailed.sprite = Spell.SkillImageUIVFX;

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
        if(Skills_Shop.Length == 0)
        {
            if (value.value == 0)
            {
                Skills_Shop = SkillLibrary.Skills.OrderBy(x => x.SkillPriceMoney).ToArray();
            }
            else if (value.value == 1)
            {
                Skills_Shop = SkillLibrary.Skills.OrderBy(x => x.SkillName).ToArray();
            }
            else if (value.value == 2)
            {
                Skills_Shop = SkillLibrary.Skills.OrderBy(x => x.PhysicalDamage).ToArray();
            }
            else if (value.value == 3)
            {
                Skills_Shop = SkillLibrary.Skills.OrderBy(x => x.MagicDamage).ToArray();
            }
            else if (value.value == 4)
            {
                Skills_Shop = SkillLibrary.Skills.OrderBy(x => x.SoulDamage).ToArray();
            }

        }
        else
        {
            if (value.value == 0)
            {
                Skills_Shop = Skills_Shop.OrderBy(x => x.SkillPriceMoney).ToArray();
            }
            else if (value.value == 1)
            {
                Skills_Shop = Skills_Shop.OrderBy(x => x.SkillName).ToArray();
            }
            else if (value.value == 2)
            {
                Skills_Shop = Skills_Shop.OrderBy(x => x.PhysicalDamage).ToArray();
            }
            else if (value.value == 3)
            {
                Skills_Shop = Skills_Shop.OrderBy(x => x.MagicDamage).ToArray();
            }
            else if (value.value == 4)
            {
                Skills_Shop = Skills_Shop.OrderBy(x => x.SoulDamage).ToArray();
            }
        }

        SkillScrollContentFill();
    }

    public void SearchDetailedSearch()
    {
        Skills_Shop = SkillLibrary.Skills.ToArray();

        if (DropdownObj[1].value == 1)
        {
            Skills_Shop = SkillLibrary.Skills.Where(x => x.IsPasive == false).ToArray();
        }
        else if (DropdownObj[1].value == 2)
        {
            Skills_Shop = SkillLibrary.Skills.Where(x => x.IsPasive == true).ToArray();
        }


        if (DropdownObj[2].value == 1)
        {
            Skills_Shop = SkillLibrary.Skills.Where(x => x.IsBuff == false).ToArray();
        }
        else if (DropdownObj[2].value == 2)
        {
            Skills_Shop = SkillLibrary.Skills.Where(x => x.IsBuff == true).ToArray();
        }

        //-------------------------------------------Attribute search is not complete--------------------------------------------
        //if (DropdownObj[3].value == 1)
        //{
        //    Skills_Shop = SkillLibrary.Skills.Where(x => x.IsPasive == false).ToArray();
        //}
        //else if (DropdownObj[3].value == 2)
        //{
        //    Skills_Shop = SkillLibrary.Skills.Where(x => x.IsPasive == true).ToArray();
        //}

        SearchDropDownChange(DropdownObj[0]);
    }

    public void BuySkill()
    {
        if (Spell.SkillName != null)
        {
            if (Spell.SkillPriceMoney != 0 && Spell.SkillPriceMoney <= playerActionObj.unitStat.Money)
            {
                if (true)//Check If Conditions are met to buy skill--------------------------------------------------------------
                {
                    Menu.OpenMenu("ChooseSkillIndexPanel");
                }
            }
            else if (Spell.SkillPriceXp != 0 && Spell.SkillPriceXp <= playerActionObj.unitStat.Xp)
            {
                if (true)//Check If Conditions are met to buy skill--------------------------------------------------------------
                {
                    Menu.OpenMenu("ChooseSkillIndexPanel");
                }
            }
        }
    }

    public void ChangeSkillIndex(int index)
    {
        if (index < 4)
        {
            if (!Spell.IsPasive)
            {
                playerActionObj.unitStat.Money -= Spell.SkillPriceMoney;
                _SkillIndex = HelpMethods.GetSkillIndexByName(Spell.SkillName);
                playerActionObj.PlayerSkills[index] = _SkillIndex;
                Menu.OpenMenu("");//if it is empty it will close everything
            }
            else{
                Menu.OpenMenu("ChooseSkillIndexPanel");
                //needs error show ---------------------------------------------------------------------------
            }
            
        }
        else
        {
            if (Spell.IsPasive)
            {
                if (Spell.SkillPriceMoney <= playerActionObj.unitStat.Money)
                {
                    playerActionObj.unitStat.Money -= Spell.SkillPriceMoney;
                    playerActionObj.PlayerSkills[index] = _SkillIndex;
                    Menu.OpenMenu("");//if it is empty it will close everything
                }
            }
            else
            {
                Menu.OpenMenu("ChooseSkillIndexPanel");
                //needs error show ---------------------------------------------------------------------------
            }
        }

        LoadSkillUiImages();
    }

    void LoadSkillUiImages()
    {
        HudConetntController hudConetnt = hudObj.GetComponentInChildren<HudConetntController>();
        hudConetnt.LoadSkillUiImages();

        for (int i = 0; i < 9; i++)
        {
            imagesObj[i].sprite = SkillLibrary.Skills[playerActionObj.PlayerSkills[i]].SkillImageUIVFX;
        }
    }
}
