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
    GameObject Player_PrefabObj;
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
        var player = ClientScene.localPlayer.gameObject;
        playerUnitStats = player.GetComponent<Unit>();
        playerAbilities = player.GetComponent<Abillities>();
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


        string Damage = "";
        string Consumption = "";
        string Effects = "";

        #region Damage
        Damage += Spell.PhysicalDamage == 0 ? "" : $"Physical: {Spell.PhysicalDamage},";
        Damage += Spell.MagicDamage == 0 ? "" : $" Magical: {Spell.MagicDamage},";
        Damage += Spell.SoulDamage == 0 ? "" : $" Soul: {Spell.SoulDamage}";

        #endregion


        #region Consumption
        Consumption += Spell.HealthConsumption == 0 ? "" : $"Health: {Spell.HealthConsumption},";
        Consumption += Spell.ManaConsumption == 0 ? "" : $" Mana: {Spell.ManaConsumption}";
        #endregion

        #region Effects
        Effects += Spell.IsRestraining ? "Restraining," : "";
        Effects += Spell.IsInvisible ? "Invisible," : "";
        Effects += Spell.IsPasive ? "Pasive," : "";
        Effects += Spell.IsBuff ? "Buff/debuff," : "";
        Effects += Spell.IsProjectile ? "Projectile," : "";
        #endregion



        string DetailedInfo = $"Description: {Spell.SkillName}\n\nDamage: {Damage}\n\nConsumtion: {Consumption}\n\nCooldown: {Spell.Cooldown}\n\nEffects: {Effects }";
        GameObject.Find("SkillInfoText").GetComponent<Text>().text = DetailedInfo;
    }

    public void SearchDropDownChange(Dropdown value)
    {
        var skills = Skills_Shop.Length == 0 ? SkillLibrary.Skills : Skills_Shop;

        switch (value.value)
        {
            case 0:
                Skills_Shop = skills.OrderBy(x => x.SkillPriceMoney).ToArray();
                break;
            case 1:
                Skills_Shop = skills.OrderBy(x => x.SkillName).ToArray();
                break;
            case 2:
                Skills_Shop = skills.OrderBy(x => x.PhysicalDamage).ToArray();
                break;
            case 3:
                Skills_Shop = skills.OrderBy(x => x.MagicDamage).ToArray();
                break;
            case 4:
                Skills_Shop = skills.OrderBy(x => x.SoulDamage).ToArray();
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
            ShopHUDimagesObj[i].sprite = playerAbilities.PlayerAbillities[i].Skill.SkillImageUIVFX;
        }
    }

    void LoadPassiveSkillUiImages()
    {
        for (int i = 0; i < 5; i++)
        {
            ShopHUDimagesObj[4+i].sprite = playerAbilities.PlayerPassives[i].Skill.SkillImageUIVFX;
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
        //_SkillIndex = HelpMethods.GetSkillIndexByName(Spell.SkillName);

        //Check If player already has skill
        if (playerAbilities.PlayerAbillities.FirstOrDefault(p => p.Skill == Spell) != null)
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
