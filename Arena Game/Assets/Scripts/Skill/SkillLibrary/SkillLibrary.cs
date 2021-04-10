using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillLibrary : MonoBehaviour
{
    public static Skill[] Skills = new Skill[2];
    
    // Start is called before the first frame update
    void Awake()
    {
        Skills[0] = new Skill
        {
            SkillName = "EmptySlot",

            SkillImageUIVFX = Resources.Load<Sprite>("unity_builtin_extra"),
        };

        Skills[1] = new Skill
        {
            SkillName = "FireBall",

            PhysicalDamage = 0,
            MagicDamage = 2000,
            SoulDamage = 0,

            HealthBuff = 0,
            ManaBuff = 0,
            SpeedBuff = 0,
            CooldownBuff = 0,

            HealthConsumption = 0,
            ManaConsumption = 10,

            SkillPriceMoney = 100,
            SkillPriceXp = 0,

            Duration = 5,
            Cooldown = 5,
            ActivationTime = 0,
            ProjectileSpeed = 50,
            MaxRechargingTime = 0,

            Height = 0,
            Distance = 250,
            Thickness = 0,

            SkillImageUIVFX = Resources.Load<Sprite>("Design/Skill/Skill UI Image/Fireball_Ui_Sprite"),
            PlayergroundVFX = Resources.Load<Sprite>("Design/Skill/Skill Indicator/FireCircle"),
            MaxRangeVFX = null,
            IndicatorVFX = Resources.Load<Sprite>("Design/Skill/Skill Indicator/Indicator"),
            Skill3DModel = "Fireball_Prefab",
            Sound = "Fireball_Sound",
            AnimatorProperty = "SkillOne",

            IsRestraining = false,
            IsInvisible = false,
            IsPasive = false,
            IsBuff = false,
            IsProjectile = true,
            IsRecharged = false,
            UsingWeapon = false,
            HasPlayergroundVFX = true,
            HasIndicator = true,
            HasMaxRange = false,
            HasRecharging = false
        };
    }
}