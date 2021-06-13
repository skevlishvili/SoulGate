using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class Shop : MonoBehaviour
{
    #region Referances
    public GameObject Shop_Content;
    public GameObject ShopSkillUi_PrefabObj; // This is our prefab object that will be exposed in the inspector
    public GameObject hudObj;
    public GameObject Player_PrefabObj;
    Unit playerUnitStats;
    Abillities playerAbilities;

    public GameObject DetailedPanel;
    public Text DetailedSkillInfo;
    public Image SkillUiImageDetailed;
    public Menu ActiveSkill;
    public Menu PassiveSkill;
    public ChatBehaviour chat;

    public CursorScript cursor;

    [Tooltip("The UI Label to inform the user that the connection is in progress")]
    [SerializeField]
    private MainMenu Menu;

    public Image[] ShopHUDimagesObj;
    public Dropdown[] DropdownObj;

    public bool ShopOpen = false;
    public GameObject Panel; 
    #endregion

    int _SkillIndex;
    Skill[] Skills_Shop;
    static Skill Spell;


    void OnEnable()
    {
        Skills_Shop = SkillLibrary.Skills.OrderBy(x => x.SkillPriceMoney).ToArray();
        SkillScrollContentFill();
    }


    private void Start()
    {
        playerUnitStats = Player_PrefabObj.GetComponent<Unit>();
        playerAbilities = Player_PrefabObj.GetComponent<Abillities>();
    }


    private void Update()
    {
        if (chat.IsTyping)
            return;

        if (Input.GetKeyDown(KeyCodeController.Shop))
        {
            ShopOpen = !ShopOpen;
            Panel.SetActive(ShopOpen);
        }
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
        GameObject.Find("SkillUIImage").GetComponent<Image>().sprite = Spell.SkillImageUIVFX;

        string DetailedInfo = "";
        string Effects = "";

        #region Effects
        Effects += Spell.IsRestraining ? "Restraining," : "";
        Effects += Spell.IsInvisible ? "Invisible," : "";
        Effects += Spell.IsPasive ? "Pasive," : "";
        Effects += Spell.IsBuff ? "Buff/debuff," : "";
        Effects += Spell.IsProjectile ? "Projectile," : "";
        #endregion

        if (!Spell.IsPasive)
        {
            string Damage = "";
            string Consumption = "";

            #region Damage
            Damage += Spell.PhysicalDamage == 0 ? "" : $"\nPhysical: {Spell.PhysicalDamage},";
            Damage += Spell.MagicDamage == 0 ? "" : $"\nMagical: {Spell.MagicDamage},";
            Damage += Spell.SoulDamage == 0 ? "" : $"\nSoul: {Spell.SoulDamage}";
            #endregion


            #region Consumption
            Consumption += Spell.HealthConsumption == 0 ? "" : $"Health: {Spell.HealthConsumption},";
            #endregion

            DetailedInfo = $"Description: {Spell.SkillName}\n\nDamage: {Damage}\n\nConsumtion: {Consumption}\n\nCooldown: {Spell.Cooldown}\n\nEffects: {Effects }";
        }
        else
        {
            string Buffs = "";

            #region Buffs
            Buffs += Spell.HealthBuff == 0 ? "" : $"\nHealth: {Spell.HealthBuff},";
            Buffs += Spell.HealthRegenBuff == 0 ? "" : $"\nHealth Regen: {Spell.HealthRegenBuff},";
            Buffs += Spell.PhysicalDefenceBuff == 0 ? "" : $"\nPhysical Defence: {Spell.PhysicalDefenceBuff},";
            Buffs += Spell.MagicDefenceBuff == 0 ? "" : $"\nMagic Defence: {Spell.MagicDefenceBuff},";
            Buffs += Spell.DamageBuff == 0 ? "" : $"\nDamage: {Spell.DamageBuff},";
            Buffs += Spell.AgilityBuff == 0 ? "" : $"\nAgility: {Spell.AgilityBuff},";
            Buffs += Spell.CooldownBuff == 0 ? "" : $"\nCooldown: {Spell.CooldownBuff},";
            Buffs += Spell.MoneyRegenBuff == 0 ? "" : $"\nMoney Regen: {Spell.MoneyRegenBuff},";
            #endregion

            DetailedInfo = $"Description: {Spell.SkillName}\n\nBuff: {Buffs}\n\nEffects: {Effects }";
        }




        GameObject.Find("SkillInfoText").GetComponent<Text>().text = DetailedInfo;
    }

    public void SearchDropDownChange(Dropdown value)
    {
        var skills = Skills_Shop.Length == 0 ? SkillLibrary.Skills : Skills_Shop;

        switch (value.value)
        {
            case 0:
                Skills_Shop = skills.OrderBy(x => x.SkillPriceMoney).Where(x=> x.SkillName != "EmptySlot").ToArray();
                break;
            case 1:
                Skills_Shop = skills.OrderBy(x => x.SkillName).Where(x => x.SkillName != "EmptySlot").ToArray();
                break;
            case 2:
                Skills_Shop = skills.OrderBy(x => x.PhysicalDamage).Where(x => x.SkillName != "EmptySlot").ToArray();
                break;
            case 3:
                Skills_Shop = skills.OrderBy(x => x.MagicDamage).Where(x => x.SkillName != "EmptySlot").ToArray();
                break;
            case 4:
                Skills_Shop = skills.OrderBy(x => x.SoulDamage).Where(x => x.SkillName != "EmptySlot").ToArray();
                break;
            default:
                break;
        }

        SkillScrollContentFill();
    }

    //search
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

        SearchDropDownChange(DropdownObj[0]);
    }


    //opens menu that asks you to select slot
    public void BuySkill()
    {
        if (!CanBuy())
            return;

        if (Spell.IsPasive)
        {
            PassiveSkill.Open();
            ActiveSkill.Close();
            LoadPassiveSkillUiImages();
        }
        else {
            PassiveSkill.Close();
            ActiveSkill.Open();
            LoadActiveSkillUiImages();
        }
    }


    // assigns passive skill to specific slot
    public void ChangeActiveSkill(int index) {
        playerUnitStats.Money -= Spell.SkillPriceMoney;
        //playerActionObj.PlayerSkills[index] = _SkillIndex;
        //Menu.OpenMenu("");//if it is empty it will close everything
        playerAbilities.ChangeActiveSkill(Spell, index);
        PassiveSkill.Close();
        ActiveSkill.Close();
    }

    // assigns passive skill to specific slot
    public void ChangePassiveSkill(int index)
    {
        playerUnitStats.Money -= Spell.SkillPriceMoney;
        //playerActionObj.PlayerSkills[index] = _SkillIndex;
        //Menu.OpenMenu("");//if it is empty it will close everything
        playerAbilities.ChangePassiveSkill(Spell, index);
        PassiveSkill.Close();
        ActiveSkill.Close();
    }

    //
    //public void ChangeSkillIndex(int index)
    //{
    //    if (index < 4)
    //    {
    //        if (!Spell.IsPasive)
    //        {
    //            playerActionObj.unitStat.Money -= Spell.SkillPriceMoney;
    //            playerActionObj.PlayerSkills[index] = _SkillIndex;
    //            Menu.OpenMenu("");//if it is empty it will close everything
    //        }
    //        else
    //        {
    //            Menu.OpenMenu("ChooseSkillIndexPanel");
    //            //needs error show ---------------------------------------------------------------------------
    //        }

    //    }
    //    else
    //    {
    //        if (Spell.IsPasive)
    //        {
    //            playerActionObj.unitStat.Money -= Spell.SkillPriceMoney;
    //            playerActionObj.PlayerSkills[index] = _SkillIndex;
    //            Menu.OpenMenu("");//if it is empty it will close everything
    //        }
    //        else
    //        {
    //            Menu.OpenMenu("ChooseSkillIndexPanel");
    //            //needs error show ---------------------------------------------------------------------------
    //        }
    //    }

    //    LoadSkillUiImages();
    //}

    void LoadActiveSkillUiImages()
    {
        for (int i = 0; i < 4; i++)
        {
            if (playerAbilities.PlayerAbillities[i].Skill != null)
            {
                ShopHUDimagesObj[i].sprite = playerAbilities.PlayerAbillities[i].Skill.SkillImageUIVFX;
            }
            else
            {
                ShopHUDimagesObj[i].sprite = Resources.Load<Sprite>("Design/UI/Shop"); ;
            }
        }
    }

    void LoadPassiveSkillUiImages()
    {
        for (int i = 0; i < 5; i++)
        {
            if (playerAbilities.PlayerPassives[i].Skill != null)
            {
                ShopHUDimagesObj[4 + i].sprite = playerAbilities.PlayerPassives[i].Skill.SkillImageUIVFX;
            }
            else
            {
                ShopHUDimagesObj[4 + i].sprite = Resources.Load<Sprite>("Design/UI/Shop"); ;
            }
        }
    }


    public void OnMouseOverBuy()
    {
        if (CanBuy()) {
            cursor.OnMouseOver(3);
        } else {
            cursor.OnMouseOver(5);
        }
    }

    public bool CanBuy() {
        if (Spell.SkillName == null)
            return false;

        //Check If player already has skill
        if (playerAbilities.PlayerAbillities.FirstOrDefault(p => p.Skill == Spell) != null)
        {
            //tell player that he already has skill
            Debug.LogWarning("Player already has this skill");
            return false;
        }

        if (playerAbilities.PlayerPassives.FirstOrDefault(p => p.Skill == Spell) != null)
        {
            //tell player that he already has skill
            Debug.LogWarning("Player already has this skill");
            return false;
        }

        //Check If Conditions are met to buy skill
        if (Spell.SkillPriceMoney > playerUnitStats.Money)
        {
            //Conditions are not met please choose another skill
            Debug.LogWarning("Player doesn't have enough money");
            return false;
        }

        return true;
    }
}
