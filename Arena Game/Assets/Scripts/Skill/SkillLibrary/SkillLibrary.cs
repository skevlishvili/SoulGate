using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillLibrary : MonoBehaviour
{
    public static Skill[] Skills = new Skill[8];

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
            MagicDamage = 10,
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
            ActivationTime = 0.2f,
            ProjectileSpeed = 50,
            MaxRechargingTime = 0,

            Height = 0,
            Distance = 250,
            Thickness = 0,

            SkillImageUIVFX = Resources.Load<Sprite>("Design/Skill/Skill UI Image/500_skillicons/BonusIcons/Basic_Spells/Normal/01_fire_arrow"),
            PlayergroundVFX = Resources.Load<Sprite>("Design/Skill/Skill Indicator/FireCircle"),
            MaxRangeVFX = null,
            IndicatorVFX = Resources.Load<Sprite>("Design/Skill/Skill Indicator/Indicator"),
            TargetVFX = null,
            Skill3DModel = "Projectile/Fireball_Prefab",
            Sound = "Fireball_Sound",
            AnimatorProperty = "Attack_Magic_02",

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
            HasTargetVFX = false,
            HasRecharging = false
        };

        Skills[2] = new Skill
        {
            SkillName = "FireStorm",

            PhysicalDamage = 0,
            MagicDamage = 100,
            SoulDamage = 0,

            HealthBuff = 0,
            ManaBuff = 0,
            SpeedBuff = 0,
            CooldownBuff = 0,

            HealthConsumption = 0,
            ManaConsumption = 50,

            SkillPriceMoney = 200,
            SkillPriceXp = 0,

            Duration = 1,
            Cooldown = 30,
            ActivationTime = 0,
            ProjectileSpeed = 0,
            MaxRechargingTime = 0,

            Height = 0,
            Distance = 10,
            Thickness = 0,

            SkillImageUIVFX = Resources.Load<Sprite>("Design/Skill/Skill UI Image/500_skillicons/Skill_standart/Engineerskill_06"),
            PlayergroundVFX = null,
            MaxRangeVFX = Resources.Load<Sprite>("Design/Skill/Skill Indicator/MaxRange"),
            IndicatorVFX = null,
            TargetVFX = Resources.Load<Sprite>("Design/Skill/Skill Indicator/Target"),
            Skill3DModel = "AOE/FireStorm",
            Sound = "Fireball_Sound",
            AnimatorProperty = "Attack_Magic_04",

            IsRestraining = false,
            IsInvisible = false,
            IsPasive = false,
            IsBuff = false,
            IsProjectile = false,
            IsRecharged = false,
            UsingWeapon = false,
            HasPlayergroundVFX = false,
            HasIndicator = false,
            HasMaxRange = true,
            HasTargetVFX = true,
            HasRecharging = false
        };

        Skills[3] = new Skill
        {
            SkillName = "FireWall",

            PhysicalDamage = 0,
            MagicDamage = 0,
            SoulDamage = 0,

            HealthBuff = 0,
            ManaBuff = 0,
            SpeedBuff = 0,
            CooldownBuff = 0,

            HealthConsumption = 0,
            ManaConsumption = 0,

            SkillPriceMoney = 200,
            SkillPriceXp = 0,

            Duration = 0,
            Cooldown = 0,
            ActivationTime = 0,
            ProjectileSpeed = 0,
            MaxRechargingTime = 0,

            Height = 0,
            Distance = 0,
            Thickness = 0,

            SkillImageUIVFX = Resources.Load<Sprite>("Design/Skill/Skill UI Image/500_skillicons/Skill_standart/Engineerskill_43"),
            PlayergroundVFX = null,
            MaxRangeVFX = null,
            IndicatorVFX = null,
            TargetVFX = null,
            Skill3DModel = "AOE/vfx_heal",
            Sound = "Fireball_Sound",
            AnimatorProperty = "Attack_Magic_02",

            IsRestraining = false,
            IsInvisible = false,
            IsPasive = false,
            IsBuff = false,
            IsProjectile = false,
            IsRecharged = false,
            UsingWeapon = false,
            HasPlayergroundVFX = false,
            HasIndicator = false,
            HasMaxRange = false,
            HasTargetVFX = false,
            HasRecharging = false
        };

        Skills[4] = new Skill
        {
            SkillName = "Meteor",

            PhysicalDamage = 50,
            MagicDamage = 100,
            SoulDamage = 0,

            HealthBuff = 0,
            ManaBuff = 0,
            SpeedBuff = 0,
            CooldownBuff = 0,

            HealthConsumption = 0,
            ManaConsumption = 100,

            SkillPriceMoney = 500,
            SkillPriceXp = 0,

            Duration = 3,
            Cooldown = 120,
            ActivationTime = 0,
            ProjectileSpeed = 0,
            MaxRechargingTime = 0,

            Height = 0,
            Distance = 25,
            Thickness = 0,

            SkillImageUIVFX = Resources.Load<Sprite>("Design/Skill/Skill UI Image/500_skillicons/Skill_standart/Shamanskill_08"),
            PlayergroundVFX = null,
            MaxRangeVFX = Resources.Load<Sprite>("Design/Skill/Skill Indicator/MaxRange"),
            IndicatorVFX = null,
            TargetVFX = Resources.Load<Sprite>("Design/Skill/Skill Indicator/Target"),
            Skill3DModel = "AOE/FireMeteor",
            Sound = "",
            AnimatorProperty = "Attack_Magic_02",

            IsRestraining = false,
            IsInvisible = false,
            IsPasive = false,
            IsBuff = false,
            IsProjectile = false,
            IsRecharged = false,
            UsingWeapon = false,
            HasPlayergroundVFX = false,
            HasIndicator = false,
            HasMaxRange = true,
            HasTargetVFX = true,
            HasRecharging = false
        };

        Skills[5] = new Skill
        {
            SkillName = "HealthRegen",

            PhysicalDamage = 0,
            MagicDamage = 0,
            SoulDamage = 0,

            HealthBuff = 0,
            ManaBuff = 0,
            SpeedBuff = 0,
            CooldownBuff = 0,

            HealthConsumption = 0,
            ManaConsumption = 50,

            SkillPriceMoney = 100,
            SkillPriceXp = 0,

            Duration = 1,
            Cooldown = 120,
            ActivationTime = 1,
            ProjectileSpeed = 0,
            MaxRechargingTime = 0,

            Height = 0,
            Distance = 0,
            Thickness = 0,

            SkillImageUIVFX = Resources.Load<Sprite>("Design/Skill/Skill UI Image/500_skillicons/Skill_standart/Assassinskill_29"),
            PlayergroundVFX = Resources.Load<Sprite>("Design/Skill/Skill Indicator/FireCircle"),
            MaxRangeVFX = null,
            IndicatorVFX = null,
            TargetVFX = null,
            Skill3DModel = "AOE/vfx_heal",
            Sound = "HealthRegen_Sound",
            AnimatorProperty = "Attack_Magic_03",

            IsRestraining = false,
            IsInvisible = false,
            IsPasive = true,
            IsBuff = false,
            IsProjectile = false,
            IsRecharged = false,
            UsingWeapon = false,
            HasPlayergroundVFX = true,
            HasIndicator = false,
            HasMaxRange = false,
            HasTargetVFX = false,
            HasRecharging = false
        };

        Skills[6] = new Skill
        {
            SkillName = "Teleport",

            PhysicalDamage = 0,
            MagicDamage = 0,
            SoulDamage = 0,

            HealthBuff = 0,
            ManaBuff = 0,
            SpeedBuff = 0,
            CooldownBuff = 0,

            HealthConsumption = 0,
            ManaConsumption = 50,

            SkillPriceMoney = 200,
            SkillPriceXp = 0,

            Duration = 1,
            Cooldown = 120,
            ActivationTime = 1,
            ProjectileSpeed = 0,
            MaxRechargingTime = 0,

            Height = 0,
            Distance = 20,
            Thickness = 0,

            SkillImageUIVFX = Resources.Load<Sprite>("Design/Skill/Skill UI Image/500_skillicons/BonusIcons/10newicons/Standart/Update_04"),
            PlayergroundVFX = null,
            MaxRangeVFX = Resources.Load<Sprite>("Design/Skill/Skill Indicator/MaxRange"),
            IndicatorVFX = null,
            TargetVFX = null,
            Skill3DModel = null,
            Sound = "",
            AnimatorProperty = "Attack_Magic_03",

            IsRestraining = false,
            IsInvisible = false,
            IsPasive = true,
            IsBuff = false,
            IsProjectile = false,
            IsRecharged = false,
            UsingWeapon = false,
            HasPlayergroundVFX = false,
            HasIndicator = false,
            HasMaxRange = true,
            HasTargetVFX = false,
            HasRecharging = false
        };

        Skills[7] = new Skill
        {
            SkillName = "LightningStrike",

            PhysicalDamage = 0,
            MagicDamage = 100,
            SoulDamage = 10,

            HealthBuff = 0,
            ManaBuff = 0,
            SpeedBuff = 0,
            CooldownBuff = 0,

            HealthConsumption = 0,
            ManaConsumption = 100,

            SkillPriceMoney = 500,
            SkillPriceXp = 0,

            Duration = 3,
            Cooldown = 90,
            ActivationTime = 0,
            ProjectileSpeed = 0,
            MaxRechargingTime = 0,

            Height = 0,
            Distance = 25,
            Thickness = 0,

            SkillImageUIVFX = Resources.Load<Sprite>("Design/Skill/Skill UI Image/500_skillicons/Skill_nobg/Shamanskill_27_nobg"),
            PlayergroundVFX = null,
            MaxRangeVFX = Resources.Load<Sprite>("Design/Skill/Skill Indicator/MaxRange"),
            IndicatorVFX = null,
            TargetVFX = Resources.Load<Sprite>("Design/Skill/Skill Indicator/Target"),
            Skill3DModel = "AOE/LightningStrike",
            Sound = "",
            AnimatorProperty = "Attack_Magic_02",

            IsRestraining = false,
            IsInvisible = false,
            IsPasive = false,
            IsBuff = false,
            IsProjectile = false,
            IsRecharged = false,
            UsingWeapon = false,
            HasPlayergroundVFX = false,
            HasIndicator = false,
            HasMaxRange = true,
            HasTargetVFX = true,
            HasRecharging = false
        };
    }
}