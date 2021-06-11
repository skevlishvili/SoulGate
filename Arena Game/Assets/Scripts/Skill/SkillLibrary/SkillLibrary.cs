 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillLibrary : MonoBehaviour
{
    public static Skill[] Skills = new Skill[30];

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

            SkillImageUIVFX = Resources.Load<Sprite>("Design/UI/Shop"),
        };

        Skills[1] = new Skill
        {
            SkillName = "FireBall",

            PhysicalDamage = 5,
            MagicDamage = 20,
            SoulDamage = 0,

            HealthBuff = 0,
            HealthRegenBuff = 0,
            PhysicalDefenceBuff = 0,
            MagicDefenceBuff = 0,
            DamageBuff = 0,
            AgilityBuff = 0,
            CooldownBuff = 0,
            MoneyRegenBuff = 0,

            HealthConsumption = 0,

            SkillPriceMoney = 100,
            SkillPriceXp = 0,

            Duration = 5,
            Cooldown = 5,
            ActivationTime = 0.2f,
            ProjectileSpeed = 50,
            MaxRechargingTime = 0,

            AttackRadius = 0,
            Distance = 250,
            

            SkillImageUIVFX = Resources.Load<Sprite>("Design/Skill/Skill UI Image/Used Skill Ui Image/01_fire_arrow"),
            PlayergroundVFX = PlayergroundVFX,
            MaxRangeVFX = null,
            IndicatorVFX = IndicatorVFX,
            TargetVFX = null,
            BurstVFX = null,
            Skill3DModel = "Prefabs/Skill/Projectile/Fireball_Prefab",
            SkillHitPrefab = "Prefabs/Skill/Hit/Fireball_Hit_Prefab",
            SkillFlashPrefab = null,
            Sound = "Design/Music/Sounds/Fireball_Sound",
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

            PhysicalDamage =20,
            MagicDamage = 100,
            SoulDamage = 0,

            HealthBuff = 0,
            HealthRegenBuff = 0,
            PhysicalDefenceBuff = 0,
            MagicDefenceBuff = 0,
            DamageBuff = 0,
            AgilityBuff = 0,
            CooldownBuff = 0,
            MoneyRegenBuff = 0,

            HealthConsumption = 0,

            SkillPriceMoney = 200,
            SkillPriceXp = 0,

            Duration = 2,
            Cooldown = 15,
            ActivationTime = 0,
            ProjectileSpeed = 0,
            MaxRechargingTime = 0,

            AttackRadius = 2,
            Distance = 15,

            SkillImageUIVFX = Resources.Load<Sprite>("Design/Skill/Skill UI Image/Used Skill Ui Image/Engineerskill_06"),
            PlayergroundVFX = null,
            MaxRangeVFX = MaxRangeVFX,
            IndicatorVFX = null,
            TargetVFX = TargetVFX,
            BurstVFX = null,
            Skill3DModel = "Prefabs/Skill/AOE/FireStorm",
            SkillHitPrefab = null,
            SkillFlashPrefab = null,
            Sound = "Design/Music/Sounds/mage-fireball-skill",
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
            SkillName = "Magic Trap",

            PhysicalDamage = 0,
            MagicDamage = 0,
            SoulDamage = 0,

            HealthBuff = 0,
            HealthRegenBuff = 0,
            PhysicalDefenceBuff = 0,
            MagicDefenceBuff = 0,
            DamageBuff = 0,
            AgilityBuff = 0,
            CooldownBuff = 0,
            MoneyRegenBuff = 0,

            HealthConsumption = 0,

            SkillPriceMoney = 200,
            SkillPriceXp = 0,

            Duration = 5,
            Cooldown = 30,
            ActivationTime = 0,
            ProjectileSpeed = 0,
            MaxRechargingTime = 0,

            AttackRadius = 5,
            Distance = 25,
            

            SkillImageUIVFX = Resources.Load<Sprite>("Design/Skill/Skill UI Image/Used Skill Ui Image/Mageskill_23"),
            PlayergroundVFX = null,
            MaxRangeVFX = MaxRangeVFX,
            IndicatorVFX = null,
            TargetVFX = TargetVFX,
            BurstVFX = null,
            Skill3DModel = "Prefabs/Skill/AOE/Trap",
            SkillHitPrefab = null,
            SkillFlashPrefab = null,
            Sound = "Design/Music/Sounds/effect",
            AnimatorProperty = "Attack_Magic_02",

            IsRestraining = true,
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

        Skills[4] = new Skill
        {
            SkillName = "FireMeteor",

            PhysicalDamage = 50,
            MagicDamage = 100,
            SoulDamage = 20,

            HealthBuff = 0,
            HealthRegenBuff = 0,
            PhysicalDefenceBuff = 0,
            MagicDefenceBuff = 0,
            DamageBuff = 0,
            AgilityBuff = 0,
            CooldownBuff = 0,
            MoneyRegenBuff = 0,

            HealthConsumption = 0,

            SkillPriceMoney = 500,
            SkillPriceXp = 0,

            Duration = 3,
            Cooldown = 120,
            ActivationTime = 0,
            ProjectileSpeed = 0,
            MaxRechargingTime = 0,

            AttackRadius = 3,
            Distance = 25,
            

            SkillImageUIVFX = Resources.Load<Sprite>("Design/Skill/Skill UI Image/Used Skill Ui Image/Shamanskill_08"),
            PlayergroundVFX = null,
            MaxRangeVFX = MaxRangeVFX,
            IndicatorVFX = null,
            TargetVFX = TargetVFX,
            BurstVFX = null,
            Skill3DModel = "Prefabs/Skill/AOE/FireMeteor",
            SkillHitPrefab = null,
            SkillFlashPrefab = null,
            Sound = "Design/Music/Sounds/Skill 4",
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

            HealthBuff = 0,
            HealthRegenBuff = 0,
            PhysicalDefenceBuff = 0,
            MagicDefenceBuff = 0,
            DamageBuff = 0,
            AgilityBuff = 0,
            CooldownBuff = 0,
            MoneyRegenBuff = 0,

            HealthConsumption = 0,

            SkillPriceMoney = 100,
            SkillPriceXp = 0,

            Duration = 1,
            Cooldown = 60,
            ActivationTime = 1,
            ProjectileSpeed = 0,
            MaxRechargingTime = 0,

            AttackRadius = 0,
            Distance = 0,
            

            SkillImageUIVFX = Resources.Load<Sprite>("Design/Skill/Skill UI Image/Used Skill Ui Image/Assassinskill_29"),
            PlayergroundVFX = PlayergroundVFX,
            MaxRangeVFX = null,
            IndicatorVFX = null,
            TargetVFX = null,
            BurstVFX = null,
            Skill3DModel = "Prefabs/Skill/AOE/vfx_heal",
            SkillHitPrefab = null,
            SkillFlashPrefab = null,
            Sound = "Design/Music/Sounds/HealthRegen_Sound",
            AnimatorProperty = "Attack_Magic_03",

            IsRestraining = false,
            IsInvisible = false,
            IsPasive = false,
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
            HealthRegenBuff = 0,
            PhysicalDefenceBuff = 0,
            MagicDefenceBuff = 0,
            DamageBuff = 0,
            AgilityBuff = 0,
            CooldownBuff = 0,
            MoneyRegenBuff = 0,

            HealthConsumption = 0,

            SkillPriceMoney = 200,
            SkillPriceXp = 0,

            Duration = 1,
            Cooldown = 120,
            ActivationTime = 1,
            ProjectileSpeed = 0,
            MaxRechargingTime = 0,

            AttackRadius = 0,
            Distance = 20,
            

            SkillImageUIVFX = Resources.Load<Sprite>("Design/Skill/Skill UI Image/Used Skill Ui Image/Update_04"),
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

            PhysicalDamage = 10,
            MagicDamage = 100,
            SoulDamage = 20,

            HealthBuff = 0,
            HealthRegenBuff = 0,
            PhysicalDefenceBuff = 0,
            MagicDefenceBuff = 0,
            DamageBuff = 0,
            AgilityBuff = 0,
            CooldownBuff = 0,
            MoneyRegenBuff = 0,

            HealthConsumption = 0,

            SkillPriceMoney = 500,
            SkillPriceXp = 0,

            Duration = 3,
            Cooldown = 60,
            ActivationTime = 0,
            ProjectileSpeed = 0,
            MaxRechargingTime = 0,

            AttackRadius = 4,
            Distance = 15,
            

            SkillImageUIVFX = Resources.Load<Sprite>("Design/Skill/Skill UI Image/Used Skill Ui Image/Shamanskill_27"),
            PlayergroundVFX = null,
            MaxRangeVFX = MaxRangeVFX,
            IndicatorVFX = null,
            TargetVFX = TargetVFX,
            BurstVFX = null,
            Skill3DModel = "Prefabs/Skill/AOE/LightningStrike",
            SkillHitPrefab = null,
            SkillFlashPrefab = null,
            Sound = "Design/Music/Sounds/Skill 2",
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

            PhysicalDamage = 20,
            MagicDamage = 100,
            SoulDamage = 10,

            HealthBuff = 0,
            HealthRegenBuff = 0,
            PhysicalDefenceBuff = 0,
            MagicDefenceBuff = 0,
            DamageBuff = 0,
            AgilityBuff = 0,
            CooldownBuff = 0,
            MoneyRegenBuff = 0,

            HealthConsumption = 0,

            SkillPriceMoney = 1000,
            SkillPriceXp = 0,

            Duration = 3,
            Cooldown = 30,
            ActivationTime = 0,
            ProjectileSpeed = 0,
            MaxRechargingTime = 0,

            AttackRadius = 0,
            Distance = 0,
            

            SkillImageUIVFX = Resources.Load<Sprite>("Design/Skill/Skill UI Image/Used Skill Ui Image/Mageskill_26"),
            PlayergroundVFX = null,
            MaxRangeVFX = null,
            IndicatorVFX = null,
            TargetVFX = null,
            BurstVFX = null,
            Skill3DModel = "Prefabs/Skill/AOE/IceBurstAttack",
            SkillHitPrefab = null,
            SkillFlashPrefab = null,
            Sound = "Design/Music/Sounds/Skill 8",
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

            PhysicalDamage = 50,
            MagicDamage = 100,
            SoulDamage = 10,

            HealthBuff = 0,
            HealthRegenBuff = 0,
            PhysicalDefenceBuff = 0,
            MagicDefenceBuff = 0,
            DamageBuff = 0,
            AgilityBuff = 0,
            CooldownBuff = 0,
            MoneyRegenBuff = 0,

            HealthConsumption = 0,

            SkillPriceMoney = 250,
            SkillPriceXp = 0,

            Duration = 3,
            Cooldown = 10,
            ActivationTime = 0,
            ProjectileSpeed = 0,
            MaxRechargingTime = 0,

            AttackRadius = 0,
            Distance = 0,
            

            SkillImageUIVFX = Resources.Load<Sprite>("Design/Skill/Skill UI Image/Used Skill Ui Image/Assassinskill_42"),
            PlayergroundVFX = PlayergroundVFX,
            MaxRangeVFX = null,
            IndicatorVFX = IndicatorVFX,
            TargetVFX = null,
            BurstVFX = null,
            Skill3DModel = "Prefabs/Skill/AOE/KnivesRain",
            SkillHitPrefab = null,
            SkillFlashPrefab = null,
            Sound = "Design/Music/Sounds/Skill 1",
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

            PhysicalDamage = 20,
            MagicDamage = 50,
            SoulDamage = 30,

            HealthBuff = 0,
            HealthRegenBuff = 0,
            PhysicalDefenceBuff = 0,
            MagicDefenceBuff = 0,
            DamageBuff = 0,
            AgilityBuff = 0,
            CooldownBuff = 0,
            MoneyRegenBuff = 0,

            HealthConsumption = 0,

            SkillPriceMoney = 1000,
            SkillPriceXp = 0,

            Duration = 3,
            Cooldown = 15,
            ActivationTime = 0,
            ProjectileSpeed = 0,
            MaxRechargingTime = 0,

            AttackRadius = 8,
            Distance = 30,
            

            SkillImageUIVFX = Resources.Load<Sprite>("Design/Skill/Skill UI Image/Used Skill Ui Image/Mageskill_02"),
            PlayergroundVFX = null,
            MaxRangeVFX = MaxRangeVFX,
            IndicatorVFX = null,
            TargetVFX = TargetVFX,
            BurstVFX = null,
            Skill3DModel = "Prefabs/Skill/AOE/Meteor shower_2",
            SkillHitPrefab = null,
            SkillFlashPrefab = null,
            Sound = "Design/Music/Sounds/Skill 10",
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

            PhysicalDamage = 10,
            MagicDamage = 50,
            SoulDamage = 0,

            HealthBuff = 0,
            HealthRegenBuff = 0,
            PhysicalDefenceBuff = 0,
            MagicDefenceBuff = 0,
            DamageBuff = 0,
            AgilityBuff = 0,
            CooldownBuff = 0,
            MoneyRegenBuff = 0,

            HealthConsumption = 0,

            SkillPriceMoney = 200,
            SkillPriceXp = 0,

            Duration = 5,
            Cooldown = 15,
            ActivationTime = 0,
            ProjectileSpeed = 50,
            MaxRechargingTime = 0,

            AttackRadius = 0,
            Distance = 0,
            

            SkillImageUIVFX = Resources.Load<Sprite>("Design/Skill/Skill UI Image/Used Skill Ui Image/Archerskill_45"),
            PlayergroundVFX = PlayergroundVFX,
            MaxRangeVFX = null,
            IndicatorVFX = IndicatorVFX,
            TargetVFX = null,
            BurstVFX = null,
            Skill3DModel = "Prefabs/Skill/Projectile/Wind_Arrow",
            SkillHitPrefab = "Prefabs/Skill/Hit/Wind Arrow Hit",
            SkillFlashPrefab = "Prefabs/Skill/Flash/Wind Arrow Flash",
            Sound = "Design/Music/Sounds/Wind Arrow",
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

            PhysicalDamage = 10,
            MagicDamage = 50,
            SoulDamage = 0,

            HealthBuff = 0,
            HealthRegenBuff = 0,
            PhysicalDefenceBuff = 0,
            MagicDefenceBuff = 0,
            DamageBuff = 0,
            AgilityBuff = 0,
            CooldownBuff = 0,
            MoneyRegenBuff = 0,

            HealthConsumption = 0,

            SkillPriceMoney = 200,
            SkillPriceXp = 0,

            Duration = 5,
            Cooldown = 3,
            ActivationTime = 0,
            ProjectileSpeed = 50,
            MaxRechargingTime = 0,

            AttackRadius = 0,
            Distance = 0,
            

            SkillImageUIVFX = Resources.Load<Sprite>("Design/Skill/Skill UI Image/Used Skill Ui Image/Archerskill_05"),
            PlayergroundVFX = PlayergroundVFX,
            MaxRangeVFX = null,
            IndicatorVFX = IndicatorVFX,
            TargetVFX = null,
            Skill3DModel = "Prefabs/Skill/Projectile/Energy_Arrow",
            SkillHitPrefab = "Prefabs/Skill/Hit/Energy_Arrow_Hit",
            SkillFlashPrefab = null,
            Sound = "Design/Music/Sounds/Energy arrow",
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

            PhysicalDamage = 10,
            MagicDamage = 50,
            SoulDamage = 0,

            HealthBuff = 0,
            HealthRegenBuff = 0,
            PhysicalDefenceBuff = 0,
            MagicDefenceBuff = 0,
            DamageBuff = 0,
            AgilityBuff = 0,
            CooldownBuff = 0,
            MoneyRegenBuff = 0,

            HealthConsumption = 0,

            SkillPriceMoney = 200,
            SkillPriceXp = 0,

            Duration = 5,
            Cooldown = 3,
            ActivationTime = 0,
            ProjectileSpeed = 50,
            MaxRechargingTime = 0,

            AttackRadius = 0,
            Distance = 0,


            SkillImageUIVFX = Resources.Load<Sprite>("Design/Skill/Skill UI Image/Used Skill Ui Image/13_frost_arrow"),
            PlayergroundVFX = PlayergroundVFX,
            MaxRangeVFX = null,
            IndicatorVFX = IndicatorVFX,
            TargetVFX = null,
            BurstVFX = null,
            Skill3DModel = "Prefabs/Skill/Projectile/Ice_Bullet",
            SkillHitPrefab = "Prefabs/Skill/Hit/Ice_Bullet_Hit",
            SkillFlashPrefab = "Prefabs/Skill/Flash/Ice_Bullet_Flash",
            Sound = "Design/Music/Sounds/wet-spell-shoot",
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

            PhysicalDamage = 50,
            MagicDamage = 100,
            SoulDamage = 50,

            HealthBuff = 0,
            HealthRegenBuff = 0,
            PhysicalDefenceBuff = 0,
            MagicDefenceBuff = 0,
            DamageBuff = 0,
            AgilityBuff = 0,
            CooldownBuff = 0,
            MoneyRegenBuff = 0,

            HealthConsumption = 0,

            SkillPriceMoney = 1000,
            SkillPriceXp = 0,

            Duration = 5,
            Cooldown = 3,
            ActivationTime = 0,
            ProjectileSpeed = 20,
            MaxRechargingTime = 0,

            AttackRadius = 0,
            Distance = 0,


            SkillImageUIVFX = Resources.Load<Sprite>("Design/Skill/Skill UI Image/Used Skill Ui Image/Mageskill_50"),
            PlayergroundVFX = PlayergroundVFX,
            MaxRangeVFX = null,
            IndicatorVFX = IndicatorVFX,
            TargetVFX = null,
            BurstVFX = null,
            Skill3DModel = "Prefabs/Skill/Projectile/Micro_Sun",
            SkillHitPrefab = "Prefabs/Skill/Hit/Micro_Sun_Hit",
            SkillFlashPrefab = "Prefabs/Skill/Flash/Micro_Sun_Flash",
            Sound = "Design/Music/Sounds/Fireball Launch",
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

            PhysicalDamage = 50,
            MagicDamage = 100,
            SoulDamage = 0,

            HealthBuff = 0,
            HealthRegenBuff = 0,
            PhysicalDefenceBuff = 0,
            MagicDefenceBuff = 0,
            DamageBuff = 0,
            AgilityBuff = 0,
            CooldownBuff = 0,
            MoneyRegenBuff = 0,

            HealthConsumption = 0,

            SkillPriceMoney = 500,
            SkillPriceXp = 0,

            Duration = 5,
            Cooldown = 3,
            ActivationTime = 0,
            ProjectileSpeed = 0,
            MaxRechargingTime = 0,

            AttackRadius = 0,
            Distance = 0,


            SkillImageUIVFX = Resources.Load<Sprite>("Design/Skill/Skill UI Image/Used Skill Ui Image/Mageskill_48"),
            PlayergroundVFX = null,
            MaxRangeVFX = null,
            IndicatorVFX = null,
            TargetVFX = null,
            BurstVFX = BurstVFX,
            Skill3DModel = "Prefabs/Skill/AOE/Ice_spikes",
            SkillHitPrefab = null,
            SkillFlashPrefab = null,
            Sound = "Design/Music/Sounds/Skill 8",
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

            PhysicalDamage = 10,
            MagicDamage = 200,
            SoulDamage = 20,

            HealthBuff = 0,
            HealthRegenBuff = 0,
            PhysicalDefenceBuff = 0,
            MagicDefenceBuff = 0,
            DamageBuff = 0,
            AgilityBuff = 0,
            CooldownBuff = 0,
            MoneyRegenBuff = 0,

            HealthConsumption = 0,

            SkillPriceMoney = 500,
            SkillPriceXp = 0,

            Duration = 3,
            Cooldown = 3,
            ActivationTime = 0,
            ProjectileSpeed = 0,
            MaxRechargingTime = 0,

            AttackRadius = 0,
            Distance = 0,


            SkillImageUIVFX = Resources.Load<Sprite>("Design/Skill/Skill UI Image/Used Skill Ui Image/Mageskill_06"),
            PlayergroundVFX = PlayergroundVFX, 
            MaxRangeVFX = null,
            IndicatorVFX = IndicatorVFX,
            TargetVFX = null,
            BurstVFX = null,
            Skill3DModel = "Prefabs/Skill/AOE/Magic_Bullets",
            SkillHitPrefab = null,
            SkillFlashPrefab = null,
            Sound = "Design/Music/Sounds/Skill 9",
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

            PhysicalDamage = 100,
            MagicDamage = 100,
            SoulDamage = 50,

            HealthBuff = 0,
            HealthRegenBuff = 0,
            PhysicalDefenceBuff = 0,
            MagicDefenceBuff = 0,
            DamageBuff = 0,
            AgilityBuff = 0,
            CooldownBuff = 0,
            MoneyRegenBuff = 0,

            HealthConsumption = 0,

            SkillPriceMoney = 1000,
            SkillPriceXp = 0,

            Duration = 5,
            Cooldown = 3,
            ActivationTime = 0,
            ProjectileSpeed = 0,
            MaxRechargingTime = 0,

            AttackRadius = 0,
            Distance = 0,


            SkillImageUIVFX = Resources.Load<Sprite>("Design/Skill/Skill UI Image/Used Skill Ui Image/dragon_coldbreath"),
            PlayergroundVFX = null,
            MaxRangeVFX = null,
            IndicatorVFX = null,
            TargetVFX = null,
            BurstVFX = BurstVFX,
            Skill3DModel = "Prefabs/Skill/AOE/Dragon punch",
            SkillHitPrefab = null,
            SkillFlashPrefab = null,
            Sound = "Design/Music/Sounds/demonic-anger",
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

            PhysicalDamage = 10,
            MagicDamage = 100,
            SoulDamage = 20,

            HealthBuff = 0,
            HealthRegenBuff = 0,
            PhysicalDefenceBuff = 0,
            MagicDefenceBuff = 0,
            DamageBuff = 0,
            AgilityBuff = 0,
            CooldownBuff = 0,
            MoneyRegenBuff = 0,

            HealthConsumption = 0,

            SkillPriceMoney = 500,
            SkillPriceXp = 0,

            Duration = 2,
            Cooldown = 15,
            ActivationTime = 1,
            ProjectileSpeed = 0,
            MaxRechargingTime = 0,

            AttackRadius = 0,
            Distance = 0,


            SkillImageUIVFX = Resources.Load<Sprite>("Design/Skill/Skill UI Image/Used Skill Ui Image/Mageskill_19"),
            PlayergroundVFX = null,
            MaxRangeVFX = null,
            IndicatorVFX = null,
            TargetVFX = null,
            BurstVFX = null,
            Skill3DModel = "Prefabs/Skill/AOE/Dust_Burst",
            SkillHitPrefab = null,
            SkillFlashPrefab = null,
            Sound = "Design/Music/Sounds/Skill 5",
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
            HealthRegenBuff = 0,
            PhysicalDefenceBuff = 0,
            MagicDefenceBuff = 0,
            DamageBuff = 0,
            AgilityBuff = 0,
            CooldownBuff = 0,
            MoneyRegenBuff = 0,

            HealthConsumption = 0,

            SkillPriceMoney = 500,
            SkillPriceXp = 0,

            Duration = 10,
            Cooldown = 30,
            ActivationTime = 0,
            ProjectileSpeed = 0,
            MaxRechargingTime = 0,

            AttackRadius = 5,
            Distance = 0,


            SkillImageUIVFX = Resources.Load<Sprite>("Design/Skill/Skill UI Image/Used Skill Ui Image/Engineerskill_43"),
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



        //Passives
        Skills[20] = new Skill
        {
            SkillName = "Warrior Heath",

            PhysicalDamage = 0,
            MagicDamage = 0,
            SoulDamage = 0,

            HealthBuff = 100,
            HealthRegenBuff = 0,
            PhysicalDefenceBuff = 0,
            MagicDefenceBuff = 0,
            DamageBuff = 0,
            AgilityBuff = 0,
            CooldownBuff = 0,
            MoneyRegenBuff = 0,

            HealthConsumption = 0,

            SkillPriceMoney = 100,
            SkillPriceXp = 0,

            Duration = 0,
            Cooldown = 0,
            ActivationTime = 0,
            ProjectileSpeed = 0,
            MaxRechargingTime = 0,

            AttackRadius = 0,
            Distance = 0,


            SkillImageUIVFX = Resources.Load<Sprite>("Design/Skill/Skill UI Image/Used Skill Ui Image/Engineerskill_43"),
            PlayergroundVFX = null,
            MaxRangeVFX = null,
            IndicatorVFX = null,
            TargetVFX = null,
            BurstVFX = null,
            Skill3DModel = null,
            SkillHitPrefab = null,
            SkillFlashPrefab = null,
            Sound = "",
            AnimatorProperty = "",

            IsRestraining = false,
            IsInvisible = false,
            IsPasive = true,
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

        Skills[21] = new Skill
        {
            SkillName = "Knight Heath",

            PhysicalDamage = 0,
            MagicDamage = 0,
            SoulDamage = 0,

            HealthBuff = 100,
            HealthRegenBuff = 1,
            PhysicalDefenceBuff = 10,
            MagicDefenceBuff = 10,
            DamageBuff = 0,
            AgilityBuff = 0,
            CooldownBuff = 0,
            MoneyRegenBuff = 0,

            HealthConsumption = 0,

            SkillPriceMoney = 500,
            SkillPriceXp = 0,

            Duration = 0,
            Cooldown = 0,
            ActivationTime = 0,
            ProjectileSpeed = 0,
            MaxRechargingTime = 0,

            AttackRadius = 0,
            Distance = 0,


            SkillImageUIVFX = Resources.Load<Sprite>("Design/Skill/Skill UI Image/Used Skill Ui Image/Engineerskill_43"),
            PlayergroundVFX = null,
            MaxRangeVFX = null,
            IndicatorVFX = null,
            TargetVFX = null,
            BurstVFX = null,
            Skill3DModel = null,
            SkillHitPrefab = null,
            SkillFlashPrefab = null,
            Sound = "",
            AnimatorProperty = "",

            IsRestraining = false,
            IsInvisible = false,
            IsPasive = true,
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

        Skills[22] = new Skill
        {
            SkillName = "Grand Knight Heath",

            PhysicalDamage = 0,
            MagicDamage = 0,
            SoulDamage = 0,

            HealthBuff = 200,
            HealthRegenBuff = 3,
            PhysicalDefenceBuff = 20,
            MagicDefenceBuff = 20,
            DamageBuff = 0,
            AgilityBuff = 0,
            CooldownBuff = 0,
            MoneyRegenBuff = 0,

            HealthConsumption = 0,

            SkillPriceMoney = 1000,
            SkillPriceXp = 0,

            Duration = 0,
            Cooldown = 0,
            ActivationTime = 0,
            ProjectileSpeed = 0,
            MaxRechargingTime = 0,

            AttackRadius = 0,
            Distance = 0,


            SkillImageUIVFX = Resources.Load<Sprite>("Design/Skill/Skill UI Image/Used Skill Ui Image/Engineerskill_43"),
            PlayergroundVFX = null,
            MaxRangeVFX = null,
            IndicatorVFX = null,
            TargetVFX = null,
            BurstVFX = null,
            Skill3DModel = null,
            SkillHitPrefab = null,
            SkillFlashPrefab = null,
            Sound = "",
            AnimatorProperty = "",

            IsRestraining = false,
            IsInvisible = false,
            IsPasive = true,
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

        Skills[23] = new Skill
        {
            SkillName = "Warrior Rage",

            PhysicalDamage = 0,
            MagicDamage = 0,
            SoulDamage = 0,

            HealthBuff = 0,
            HealthRegenBuff = 0,
            PhysicalDefenceBuff = 0,
            MagicDefenceBuff = 0,
            DamageBuff = 10,
            AgilityBuff = 0,
            CooldownBuff = 0,
            MoneyRegenBuff = 0,

            HealthConsumption = 0,

            SkillPriceMoney = 100,
            SkillPriceXp = 0,

            Duration = 0,
            Cooldown = 0,
            ActivationTime = 0,
            ProjectileSpeed = 0,
            MaxRechargingTime = 0,

            AttackRadius = 0,
            Distance = 0,


            SkillImageUIVFX = Resources.Load<Sprite>("Design/Skill/Skill UI Image/Used Skill Ui Image/Engineerskill_43"),
            PlayergroundVFX = null,
            MaxRangeVFX = null,
            IndicatorVFX = null,
            TargetVFX = null,
            BurstVFX = null,
            Skill3DModel = null,
            SkillHitPrefab = null,
            SkillFlashPrefab = null,
            Sound = "",
            AnimatorProperty = "",

            IsRestraining = false,
            IsInvisible = false,
            IsPasive = true,
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

        Skills[24] = new Skill
        {
            SkillName = "Knight Discipline",

            PhysicalDamage = 0,
            MagicDamage = 0,
            SoulDamage = 0,

            HealthBuff = 0,
            HealthRegenBuff = 0,
            PhysicalDefenceBuff = 10,
            MagicDefenceBuff = 0,
            DamageBuff = 20,
            AgilityBuff = 5,
            CooldownBuff = 0,
            MoneyRegenBuff = 0,

            HealthConsumption = 0,

            SkillPriceMoney = 500,
            SkillPriceXp = 0,

            Duration = 0,
            Cooldown = 0,
            ActivationTime = 0,
            ProjectileSpeed = 0,
            MaxRechargingTime = 0,

            AttackRadius = 0,
            Distance = 0,


            SkillImageUIVFX = Resources.Load<Sprite>("Design/Skill/Skill UI Image/Used Skill Ui Image/Engineerskill_43"),
            PlayergroundVFX = null,
            MaxRangeVFX = null,
            IndicatorVFX = null,
            TargetVFX = null,
            BurstVFX = null,
            Skill3DModel = null,
            SkillHitPrefab = null,
            SkillFlashPrefab = null,
            Sound = "",
            AnimatorProperty = "",

            IsRestraining = false,
            IsInvisible = false,
            IsPasive = true,
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

        Skills[25] = new Skill
        {
            SkillName = "Mage Defence",

            PhysicalDamage = 0,
            MagicDamage = 0,
            SoulDamage = 0,

            HealthBuff = 0,
            HealthRegenBuff = 0,
            PhysicalDefenceBuff = 0,
            MagicDefenceBuff = 20,
            DamageBuff = 0,
            AgilityBuff = 0,
            CooldownBuff = 0,
            MoneyRegenBuff = 0,

            HealthConsumption = 0,

            SkillPriceMoney = 500,
            SkillPriceXp = 0,

            Duration = 0,
            Cooldown = 0,
            ActivationTime = 0,
            ProjectileSpeed = 0,
            MaxRechargingTime = 0,

            AttackRadius = 0,
            Distance = 0,


            SkillImageUIVFX = Resources.Load<Sprite>("Design/Skill/Skill UI Image/Used Skill Ui Image/Engineerskill_43"),
            PlayergroundVFX = null,
            MaxRangeVFX = null,
            IndicatorVFX = null,
            TargetVFX = null,
            BurstVFX = null,
            Skill3DModel = null,
            SkillHitPrefab = null,
            SkillFlashPrefab = null,
            Sound = "",
            AnimatorProperty = "",

            IsRestraining = false,
            IsInvisible = false,
            IsPasive = true,
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

        Skills[26] = new Skill
        {
            SkillName = "Mage Chant",

            PhysicalDamage = 0,
            MagicDamage = 0,
            SoulDamage = 0,

            HealthBuff = 0,
            HealthRegenBuff = 0,
            PhysicalDefenceBuff = 0,
            MagicDefenceBuff = 0,
            DamageBuff = 0,
            AgilityBuff = 0,
            CooldownBuff = 0.1f,
            MoneyRegenBuff = 0,

            HealthConsumption = 0,

            SkillPriceMoney = 500,
            SkillPriceXp = 0,

            Duration = 0,
            Cooldown = 0,
            ActivationTime = 0,
            ProjectileSpeed = 0,
            MaxRechargingTime = 0,

            AttackRadius = 0,
            Distance = 0,


            SkillImageUIVFX = Resources.Load<Sprite>("Design/Skill/Skill UI Image/Used Skill Ui Image/Engineerskill_43"),
            PlayergroundVFX = null,
            MaxRangeVFX = null,
            IndicatorVFX = null,
            TargetVFX = null,
            BurstVFX = null,
            Skill3DModel = null,
            SkillHitPrefab = null,
            SkillFlashPrefab = null,
            Sound = "",
            AnimatorProperty = "",

            IsRestraining = false,
            IsInvisible = false,
            IsPasive = true,
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

        Skills[27] = new Skill
        {
            SkillName = "Gold Hands",

            PhysicalDamage = 0,
            MagicDamage = 0,
            SoulDamage = 0,

            HealthBuff = 0,
            HealthRegenBuff = 0,
            PhysicalDefenceBuff = 0,
            MagicDefenceBuff = 0,
            DamageBuff = 0,
            AgilityBuff = 0,
            CooldownBuff = 0,
            MoneyRegenBuff = 1,

            HealthConsumption = 0,

            SkillPriceMoney = 500,
            SkillPriceXp = 0,

            Duration = 0,
            Cooldown = 0,
            ActivationTime = 0,
            ProjectileSpeed = 0,
            MaxRechargingTime = 0,

            AttackRadius = 0,
            Distance = 0,


            SkillImageUIVFX = Resources.Load<Sprite>("Design/Skill/Skill UI Image/Used Skill Ui Image/Engineerskill_43"),
            PlayergroundVFX = null,
            MaxRangeVFX = null,
            IndicatorVFX = null,
            TargetVFX = null,
            BurstVFX = null,
            Skill3DModel = null,
            SkillHitPrefab = null,
            SkillFlashPrefab = null,
            Sound = "",
            AnimatorProperty = "",

            IsRestraining = false,
            IsInvisible = false,
            IsPasive = true,
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

        Skills[28] = new Skill
        {
            SkillName = "Berserk",

            PhysicalDamage = 0,
            MagicDamage = 0,
            SoulDamage = 0,

            HealthBuff = 0,
            HealthRegenBuff = 1,
            PhysicalDefenceBuff = 20,
            MagicDefenceBuff = 20,
            DamageBuff = 20,
            AgilityBuff = 5,
            CooldownBuff = 0,
            MoneyRegenBuff = 0,

            HealthConsumption = 0,

            SkillPriceMoney = 1000,
            SkillPriceXp = 0,

            Duration = 0,
            Cooldown = 0,
            ActivationTime = 0,
            ProjectileSpeed = 0,
            MaxRechargingTime = 0,

            AttackRadius = 0,
            Distance = 0,


            SkillImageUIVFX = Resources.Load<Sprite>("Design/Skill/Skill UI Image/Used Skill Ui Image/Engineerskill_43"),
            PlayergroundVFX = null,
            MaxRangeVFX = null,
            IndicatorVFX = null,
            TargetVFX = null,
            BurstVFX = null,
            Skill3DModel = null,
            SkillHitPrefab = null,
            SkillFlashPrefab = null,
            Sound = "",
            AnimatorProperty = "",

            IsRestraining = false,
            IsInvisible = false,
            IsPasive = true,
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

        Skills[29] = new Skill
        {
            SkillName = "Last Breathe",

            PhysicalDamage = 0,
            MagicDamage = 0,
            SoulDamage = 0,

            HealthBuff = 0,
            HealthRegenBuff = 5,
            PhysicalDefenceBuff = 50,
            MagicDefenceBuff = 50,
            DamageBuff = 50,
            AgilityBuff = 10,
            CooldownBuff = 0,
            MoneyRegenBuff = 0,

            HealthConsumption = 0,

            SkillPriceMoney = 1000,
            SkillPriceXp = 0,

            Duration = 0,
            Cooldown = 0,
            ActivationTime = 0,
            ProjectileSpeed = 0,
            MaxRechargingTime = 0,

            AttackRadius = 0,
            Distance = 0,


            SkillImageUIVFX = Resources.Load<Sprite>("Design/Skill/Skill UI Image/Used Skill Ui Image/Engineerskill_43"),
            PlayergroundVFX = null,
            MaxRangeVFX = null,
            IndicatorVFX = null,
            TargetVFX = null,
            BurstVFX = null,
            Skill3DModel = null,
            SkillHitPrefab = null,
            SkillFlashPrefab = null,
            Sound = "",
            AnimatorProperty = "",

            IsRestraining = false,
            IsInvisible = false,
            IsPasive = true,
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