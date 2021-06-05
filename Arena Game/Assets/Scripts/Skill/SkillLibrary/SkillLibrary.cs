 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillLibrary : MonoBehaviour
{
    public static Skill[] Skills = new Skill[20];

    public static Skill[] TowerSkills = new Skill[1];

    // Start is called before the first frame update
    void Awake()
    {
        Sprite PlayergroundVFX = Resources.Load<Sprite>("Design/Skill/Skill Indicator/FireCircle");
        Sprite MaxRangeVFX = Resources.Load<Sprite>("Design/Skill/Skill Indicator/MaxRange");
        Sprite IndicatorVFX = Resources.Load<Sprite>("Design/Skill/Skill Indicator/Indicator");
        Sprite TargetVFX = Resources.Load<Sprite>("Design/Skill/Skill Indicator/Target");
        Sprite BurstVFX = Resources.Load<Sprite>("Design/Skill/Skill Indicator/Burst");

        Skills[0] = new Skill
        {
            SkillName = "EmptySlot",

            SkillImageUIVFX = Resources.Load<Sprite>("unity_builtin_extra"),
        };

        Skills[1] = new Skill
        {
            SkillName = "FireBall",

            PhysicalDamage = 0,
            MagicDamage = 20,
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

            AttackRadius = 0,
            Distance = 250,
            

            SkillImageUIVFX = Resources.Load<Sprite>("Design/Skill/Skill UI Image/500_skillicons/BonusIcons/Basic_Spells/Normal/01_fire_arrow"),
            PlayergroundVFX = PlayergroundVFX,
            MaxRangeVFX = null,
            IndicatorVFX = IndicatorVFX,
            TargetVFX = null,
            BurstVFX = null,
            Skill3DModel = "Prefabs/Skill/Projectile/Fireball_Prefab",
            SkillHitPrefab = "Prefabs/Skill/Hit/Fireball_Hit_Prefab",
            SkillFlashPrefab = null,
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
            HasBurstVFX = false,
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

            AttackRadius = 2,
            Distance = 15,

            SkillImageUIVFX = Resources.Load<Sprite>("Design/Skill/Skill UI Image/500_skillicons/Skill_standart/Engineerskill_06"),
            PlayergroundVFX = null,
            MaxRangeVFX = MaxRangeVFX,
            IndicatorVFX = null,
            TargetVFX = TargetVFX,
            BurstVFX = null,
            Skill3DModel = "Prefabs/Skill/AOE/FireStorm",
            SkillHitPrefab = null,
            SkillFlashPrefab = null,
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
            HasBurstVFX = false,
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

            AttackRadius = 0,
            Distance = 0,
            

            SkillImageUIVFX = Resources.Load<Sprite>("Design/Skill/Skill UI Image/500_skillicons/Skill_standart/Engineerskill_43"),
            PlayergroundVFX = null,
            MaxRangeVFX = null,
            IndicatorVFX = null,
            TargetVFX = null,
            BurstVFX = null,
            Skill3DModel = "Prefabs/Skill/AOE/vfx_heal",
            SkillHitPrefab = null,
            SkillFlashPrefab = null,
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
            HasBurstVFX = false,
            HasRecharging = false
        };

        Skills[4] = new Skill
        {
            SkillName = "FireMeteor",

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

            AttackRadius = 3,
            Distance = 25,
            

            SkillImageUIVFX = Resources.Load<Sprite>("Design/Skill/Skill UI Image/500_skillicons/Skill_standart/Shamanskill_08"),
            PlayergroundVFX = null,
            MaxRangeVFX = MaxRangeVFX,
            IndicatorVFX = null,
            TargetVFX = TargetVFX,
            BurstVFX = null,
            Skill3DModel = "Prefabs/Skill/AOE/FireMeteor",
            SkillHitPrefab = null,
            SkillFlashPrefab = null,
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
            HasBurstVFX = false,
            HasRecharging = false
        };

        Skills[5] = new Skill
        {
            SkillName = "HealthRegen",

            PhysicalDamage = 0,
            MagicDamage = 0,
            SoulDamage = 0,

            HealthBuff = 25,
            ManaBuff = 0,
            SpeedBuff = 0,
            CooldownBuff = 0,

            HealthConsumption = 0,
            ManaConsumption = 50,

            SkillPriceMoney = 100,
            SkillPriceXp = 0,

            Duration = 1,
            Cooldown = 60,
            ActivationTime = 1,
            ProjectileSpeed = 0,
            MaxRechargingTime = 0,

            AttackRadius = 0,
            Distance = 0,
            

            SkillImageUIVFX = Resources.Load<Sprite>("Design/Skill/Skill UI Image/500_skillicons/Skill_standart/Assassinskill_29"),
            PlayergroundVFX = PlayergroundVFX,
            MaxRangeVFX = null,
            IndicatorVFX = null,
            TargetVFX = null,
            BurstVFX = null,
            Skill3DModel = "Prefabs/Skill/AOE/vfx_heal",
            SkillHitPrefab = null,
            SkillFlashPrefab = null,
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
            HasBurstVFX = false,
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

            AttackRadius = 0,
            Distance = 20,
            

            SkillImageUIVFX = Resources.Load<Sprite>("Design/Skill/Skill UI Image/500_skillicons/BonusIcons/10newicons/Standart/Update_04"),
            PlayergroundVFX = null,
            MaxRangeVFX = MaxRangeVFX,
            IndicatorVFX = null,
            TargetVFX = null,
            BurstVFX = null,
            Skill3DModel = null,
            SkillHitPrefab = null,
            SkillFlashPrefab = null,
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
            HasBurstVFX = false,
            HasRecharging = false
        };

        Skills[7] = new Skill
        {
            SkillName = "LightningStrike",

            PhysicalDamage = 0,
            MagicDamage = 100,
            SoulDamage = 20,

            HealthBuff = 0,
            ManaBuff = 0,
            SpeedBuff = 0,
            CooldownBuff = 0,

            HealthConsumption = 0,
            ManaConsumption = 100,

            SkillPriceMoney = 500,
            SkillPriceXp = 0,

            Duration = 3,
            Cooldown = 60,
            ActivationTime = 0,
            ProjectileSpeed = 0,
            MaxRechargingTime = 0,

            AttackRadius = 4,
            Distance = 15,
            

            SkillImageUIVFX = Resources.Load<Sprite>("Design/Skill/Skill UI Image/500_skillicons/Skill_nobg/Shamanskill_27_nobg"),
            PlayergroundVFX = null,
            MaxRangeVFX = MaxRangeVFX,
            IndicatorVFX = null,
            TargetVFX = TargetVFX,
            BurstVFX = null,
            Skill3DModel = "Prefabs/Skill/AOE/LightningStrike",
            SkillHitPrefab = null,
            SkillFlashPrefab = null,
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
            HasBurstVFX = false,
            HasRecharging = false
        };

        Skills[8] = new Skill
        {
            SkillName = "Ice Burst",

            PhysicalDamage = 0,
            MagicDamage = 100,
            SoulDamage = 0,

            HealthBuff = 0,
            ManaBuff = 0,
            SpeedBuff = 0,
            CooldownBuff = 0,

            HealthConsumption = 0,
            ManaConsumption = 200,

            SkillPriceMoney = 1000,
            SkillPriceXp = 0,

            Duration = 3,
            Cooldown = 30,
            ActivationTime = 0,
            ProjectileSpeed = 0,
            MaxRechargingTime = 0,

            AttackRadius = 0,
            Distance = 0,
            

            SkillImageUIVFX = Resources.Load<Sprite>("Design/Skill/Skill UI Image/500_skillicons/Skill_standart/Mageskill_26"),
            PlayergroundVFX = null,
            MaxRangeVFX = null,
            IndicatorVFX = null,
            TargetVFX = null,
            BurstVFX = null,
            Skill3DModel = "Prefabs/Skill/AOE/IceBurstAttack",
            SkillHitPrefab = null,
            SkillFlashPrefab = null,
            Sound = "",
            AnimatorProperty = "Attack_Magic_03",

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
            HasBurstVFX = false,
            HasRecharging = false
        };

        Skills[9] = new Skill
        {
            SkillName = "Knives Rain",

            PhysicalDamage = 0,
            MagicDamage = 50,
            SoulDamage = 0,

            HealthBuff = 0,
            ManaBuff = 0,
            SpeedBuff = 0,
            CooldownBuff = 0,

            HealthConsumption = 0,
            ManaConsumption = 100,

            SkillPriceMoney = 250,
            SkillPriceXp = 0,

            Duration = 3,
            Cooldown = 10,
            ActivationTime = 0,
            ProjectileSpeed = 0,
            MaxRechargingTime = 0,

            AttackRadius = 0,
            Distance = 0,
            

            SkillImageUIVFX = Resources.Load<Sprite>("Design/Skill/Skill UI Image/500_skillicons/Skill_standart/Assassinskill_42"),
            PlayergroundVFX = PlayergroundVFX,
            MaxRangeVFX = null,
            IndicatorVFX = IndicatorVFX,
            TargetVFX = null,
            BurstVFX = null,
            Skill3DModel = "Prefabs/Skill/AOE/KnivesRain",
            SkillHitPrefab = null,
            SkillFlashPrefab = null,
            Sound = "",
            AnimatorProperty = "Attack_Magic_01",

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
            HasBurstVFX = false,
            HasRecharging = false
        };

        Skills[10] = new Skill
        {
            SkillName = "Meteor shower 2",

            PhysicalDamage = 0,
            MagicDamage = 150,
            SoulDamage = 0,

            HealthBuff = 0,
            ManaBuff = 0,
            SpeedBuff = 0,
            CooldownBuff = 0,

            HealthConsumption = 0,
            ManaConsumption = 200,

            SkillPriceMoney = 1000,
            SkillPriceXp = 0,

            Duration = 5,
            Cooldown = 60,
            ActivationTime = 0,
            ProjectileSpeed = 0,
            MaxRechargingTime = 0,

            AttackRadius = 8,
            Distance = 30,
            

            SkillImageUIVFX = Resources.Load<Sprite>("Design/Skill/Skill UI Image/500_skillicons/Skill_standart/Mageskill_02"),
            PlayergroundVFX = null,
            MaxRangeVFX = MaxRangeVFX,
            IndicatorVFX = null,
            TargetVFX = TargetVFX,
            BurstVFX = null,
            Skill3DModel = "Prefabs/Skill/AOE/Meteor shower_2",
            SkillHitPrefab = null,
            SkillFlashPrefab = null,
            Sound = "",
            AnimatorProperty = "Attack_Magic_03",

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
            HasBurstVFX = false,
            HasRecharging = false
        };

        Skills[11] = new Skill
        {
            SkillName = "Wind Arrow",

            PhysicalDamage = 0,
            MagicDamage = 50,
            SoulDamage = 0,

            HealthBuff = 0,
            ManaBuff = 0,
            SpeedBuff = 0,
            CooldownBuff = 0,

            HealthConsumption = 0,
            ManaConsumption = 50,

            SkillPriceMoney = 200,
            SkillPriceXp = 0,

            Duration = 5,
            Cooldown = 15,
            ActivationTime = 0,
            ProjectileSpeed = 50,
            MaxRechargingTime = 0,

            AttackRadius = 0,
            Distance = 0,
            

            SkillImageUIVFX = Resources.Load<Sprite>("Design/Skill/Skill UI Image/500_skillicons/Skill_standart/Archerskill_45"),
            PlayergroundVFX = PlayergroundVFX,
            MaxRangeVFX = null,
            IndicatorVFX = IndicatorVFX,
            TargetVFX = null,
            BurstVFX = null,
            Skill3DModel = "Prefabs/Skill/Projectile/Wind_Arrow",
            SkillHitPrefab = "Prefabs/Skill/Hit/Wind Arrow Hit",
            SkillFlashPrefab = "Prefabs/Skill/Flash/Wind Arrow Flash",
            Sound = "",
            AnimatorProperty = "Attack_Magic_01",

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
            HasBurstVFX = false,
            HasRecharging = false
        };
        
        Skills[12] = new Skill
        {
            SkillName = "Energy Arrow",

            PhysicalDamage = 0,
            MagicDamage = 50,
            SoulDamage = 0,

            HealthBuff = 0,
            ManaBuff = 0,
            SpeedBuff = 0,
            CooldownBuff = 0,

            HealthConsumption = 0,
            ManaConsumption = 50,

            SkillPriceMoney = 200,
            SkillPriceXp = 0,

            Duration = 5,
            Cooldown = 3,
            ActivationTime = 0,
            ProjectileSpeed = 50,
            MaxRechargingTime = 0,

            AttackRadius = 0,
            Distance = 0,
            

            SkillImageUIVFX = Resources.Load<Sprite>("Design/Skill/Skill UI Image/500_skillicons/Skill_standart/Archerskill_05"),
            PlayergroundVFX = PlayergroundVFX,
            MaxRangeVFX = null,
            IndicatorVFX = IndicatorVFX,
            TargetVFX = null,
            Skill3DModel = "Prefabs/Skill/Projectile/Energy_Arrow",
            SkillHitPrefab = "Prefabs/Skill/Hit/Energy_Arrow_Hit",
            SkillFlashPrefab = null,
            Sound = "",
            AnimatorProperty = "Attack_Magic_01",

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
            HasBurstVFX = false,
            HasRecharging = false
        };

        Skills[13] = new Skill
        {
            SkillName = "Ice Bullet",

            PhysicalDamage = 0,
            MagicDamage = 50,
            SoulDamage = 0,

            HealthBuff = 0,
            ManaBuff = 0,
            SpeedBuff = 0,
            CooldownBuff = 0,

            HealthConsumption = 0,
            ManaConsumption = 50,

            SkillPriceMoney = 200,
            SkillPriceXp = 0,

            Duration = 5,
            Cooldown = 3,
            ActivationTime = 0,
            ProjectileSpeed = 50,
            MaxRechargingTime = 0,

            AttackRadius = 0,
            Distance = 0,


            SkillImageUIVFX = Resources.Load<Sprite>("Design/Skill/Skill UI Image/500_skillicons/BonusIcons/Basic_Spells/Normal/13_frost_arrow"),
            PlayergroundVFX = PlayergroundVFX,
            MaxRangeVFX = null,
            IndicatorVFX = IndicatorVFX,
            TargetVFX = null,
            BurstVFX = null,
            Skill3DModel = "Prefabs/Skill/Projectile/Ice_Bullet",
            SkillHitPrefab = "Prefabs/Skill/Hit/Ice_Bullet_Hit",
            SkillFlashPrefab = "Prefabs/Skill/Flash/Ice_Bullet_Flash",
            Sound = "",
            AnimatorProperty = "Attack_Magic_01",

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
            HasBurstVFX = false,
            HasRecharging = false
        };

        Skills[14] = new Skill
        {
            SkillName = "Micro Sun",

            PhysicalDamage = 0,
            MagicDamage = 250,
            SoulDamage = 0,

            HealthBuff = 0,
            ManaBuff = 0,
            SpeedBuff = 0,
            CooldownBuff = 0,

            HealthConsumption = 0,
            ManaConsumption = 50,

            SkillPriceMoney = 1000,
            SkillPriceXp = 0,

            Duration = 5,
            Cooldown = 3,
            ActivationTime = 0,
            ProjectileSpeed = 20,
            MaxRechargingTime = 0,

            AttackRadius = 0,
            Distance = 0,


            SkillImageUIVFX = Resources.Load<Sprite>("Design/Skill/Skill UI Image/500_skillicons/Skill_standart/Mageskill_50"),
            PlayergroundVFX = PlayergroundVFX,
            MaxRangeVFX = null,
            IndicatorVFX = IndicatorVFX,
            TargetVFX = null,
            BurstVFX = null,
            Skill3DModel = "Prefabs/Skill/Projectile/Micro_Sun",
            SkillHitPrefab = "Prefabs/Skill/Hit/Micro_Sun_Hit",
            SkillFlashPrefab = "Prefabs/Skill/Flash/Micro_Sun_Flash",
            Sound = "",
            AnimatorProperty = "Attack_Magic_01",

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
            HasBurstVFX = false,
            HasRecharging = false
        };

        Skills[15] = new Skill
        {
            SkillName = "Ice Spikes",

            PhysicalDamage = 0,
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

            Duration = 5,
            Cooldown = 3,
            ActivationTime = 0,
            ProjectileSpeed = 0,
            MaxRechargingTime = 0,

            AttackRadius = 0,
            Distance = 0,


            SkillImageUIVFX = Resources.Load<Sprite>("Design/Skill/Skill UI Image/500_skillicons/Skill_standart/Mageskill_48"),
            PlayergroundVFX = null,
            MaxRangeVFX = null,
            IndicatorVFX = null,
            TargetVFX = null,
            BurstVFX = BurstVFX,
            Skill3DModel = "Prefabs/Skill/AOE/Ice_spikes",
            SkillHitPrefab = null,
            SkillFlashPrefab = null,
            Sound = "",
            AnimatorProperty = "Attack_Magic_01",

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
            HasBurstVFX = true,
            HasRecharging = false
        };

        Skills[16] = new Skill
        {
            SkillName = "Magic Bullets",

            PhysicalDamage = 0,
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
            Cooldown = 3,
            ActivationTime = 0,
            ProjectileSpeed = 0,
            MaxRechargingTime = 0,

            AttackRadius = 0,
            Distance = 0,


            SkillImageUIVFX = Resources.Load<Sprite>("Design/Skill/Skill UI Image/500_skillicons/Skill_standart/Mageskill_06"),
            PlayergroundVFX = PlayergroundVFX, 
            MaxRangeVFX = null,
            IndicatorVFX = IndicatorVFX,
            TargetVFX = null,
            BurstVFX = null,
            Skill3DModel = "Prefabs/Skill/AOE/Magic_Bullets",
            SkillHitPrefab = null,
            SkillFlashPrefab = null,
            Sound = "",
            AnimatorProperty = "Attack_Magic_04",

            IsRestraining = false,
            IsInvisible = false,
            IsPasive = false,
            IsBuff = false,
            IsProjectile = false,
            IsRecharged = false,
            UsingWeapon = false,

            HasPlayergroundVFX = true,
            HasIndicator = true,
            HasMaxRange = false,
            HasTargetVFX = false,
            HasBurstVFX = false,
            HasRecharging = false
        };

        Skills[17] = new Skill
        {
            SkillName = "Dragon Punch",

            PhysicalDamage = 0,
            MagicDamage = 200,
            SoulDamage = 0,

            HealthBuff = 0,
            ManaBuff = 0,
            SpeedBuff = 0,
            CooldownBuff = 0,

            HealthConsumption = 0,
            ManaConsumption = 100,

            SkillPriceMoney = 1000,
            SkillPriceXp = 0,

            Duration = 5,
            Cooldown = 3,
            ActivationTime = 0,
            ProjectileSpeed = 0,
            MaxRechargingTime = 0,

            AttackRadius = 0,
            Distance = 0,


            SkillImageUIVFX = Resources.Load<Sprite>("Design/Skill/Skill UI Image/500_skillicons/BonusIcons/neutral_skills/dragon_firebreath"),
            PlayergroundVFX = null,
            MaxRangeVFX = null,
            IndicatorVFX = null,
            TargetVFX = null,
            BurstVFX = BurstVFX,
            Skill3DModel = "Prefabs/Skill/AOE/Dragon punch",
            SkillHitPrefab = null,
            SkillFlashPrefab = null,
            Sound = "",
            AnimatorProperty = "Attack_Magic_01",

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
            HasBurstVFX = true,
            HasRecharging = false
        };

        Skills[18] = new Skill
        {
            SkillName = "Dust Burst",

            PhysicalDamage = 0,
            MagicDamage = 20,
            SoulDamage = 0,

            HealthBuff = 0,
            ManaBuff = 0,
            SpeedBuff = 0,
            CooldownBuff = 0,

            HealthConsumption = 0,
            ManaConsumption = 100,

            SkillPriceMoney = 500,
            SkillPriceXp = 0,

            Duration = 2,
            Cooldown = 15,
            ActivationTime = 1,
            ProjectileSpeed = 0,
            MaxRechargingTime = 0,

            AttackRadius = 0,
            Distance = 0,


            SkillImageUIVFX = Resources.Load<Sprite>("Design/Skill/Skill UI Image/500_skillicons/Skill_standart/Mageskill_19"),
            PlayergroundVFX = null,
            MaxRangeVFX = null,
            IndicatorVFX = null,
            TargetVFX = null,
            BurstVFX = null,
            Skill3DModel = "Prefabs/Skill/AOE/Dust_Burst",
            SkillHitPrefab = null,
            SkillFlashPrefab = null,
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
            HasMaxRange = false,
            HasTargetVFX = false,
            HasBurstVFX = false,
            HasRecharging = false
        };

        Skills[19] = new Skill
        {
            SkillName = "Magic Shield",

            PhysicalDamage = 0,
            MagicDamage = 0,
            SoulDamage = 0,

            HealthBuff = 0,
            ManaBuff = 0,
            SpeedBuff = 0,
            CooldownBuff = 0,

            HealthConsumption = 0,
            ManaConsumption = 100,

            SkillPriceMoney = 500,
            SkillPriceXp = 0,

            Duration = 10,
            Cooldown = 15,
            ActivationTime = 0,
            ProjectileSpeed = 0,
            MaxRechargingTime = 0,

            AttackRadius = 5,
            Distance = 0,


            SkillImageUIVFX = Resources.Load<Sprite>("Design/Skill/Skill UI Image/500_skillicons/Skill_standart/Engineerskill_43"),
            PlayergroundVFX = null,
            MaxRangeVFX = null,
            IndicatorVFX = null,
            TargetVFX = null,
            BurstVFX = null,
            Skill3DModel = "Prefabs/Skill/AOE/Shield",
            SkillHitPrefab = null,
            SkillFlashPrefab = null,
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
            HasMaxRange = false,
            HasTargetVFX = false,
            HasBurstVFX = false,
            HasRecharging = false
        };

        //Tower Skills ------------------------------------------------------------------------------------------

        TowerSkills[0] = new Skill
        {
            SkillName = "Laser",

            PhysicalDamage = 0,
            MagicDamage = 2,
            SoulDamage = 0,

            ManaConsumption = 1,

            Duration = 0,
            Cooldown = 0,
            ActivationTime = 0,
            ProjectileSpeed = 1000,
            MaxRechargingTime = 0,

            AttackRadius = 0,
            Distance = 50,

            Skill3DModel = "Prefabs/Skill/Projectile/Fireball_Prefab",
            SkillHitPrefab = "Prefabs/Skill/Hit/Fireball_Hit_Prefab",
            SkillFlashPrefab = null,
            Sound = null,

            IsRestraining = false,
            IsInvisible = false,
            IsPasive = false,
            IsBuff = false,
            IsProjectile = true,
            IsRecharged = false,
        };
    }
}