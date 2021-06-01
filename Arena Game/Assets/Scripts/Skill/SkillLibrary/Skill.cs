using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill
{
    public string SkillName;// name

    //Damage Types
    public float PhysicalDamage;// affects only health
    public float MagicDamage;// affects Health and Mana
    public float SoulDamage;// True Damage(affects Health and Mana)

    //Buff/Debuff
    public float HealthBuff;// Max health increase/decrease and Health regen
    public float ManaBuff;// Max Mana increase/decrease and Mana regen
    public float SpeedBuff;// Max speed increase
    public float CooldownBuff;// Max cooldown Decrease

    //Consumption
    public float HealthConsumption;// uses health to cast spell/skill
    public float ManaConsumption;// uses mana to cast spell/skill

    //Price
    public float SkillPriceMoney;// uses Money to buy spell/skill
    public float SkillPriceXp;// uses Xp to buy spell/skill

    //Time
    public float Duration;// duration of skill/spell
    public float Cooldown;// Cooldown time
    public float ActivationTime;// time before spell/skill activates
    public float ProjectileSpeed;// speed of thrown object
    public float MaxRechargingTime;// max time for recharging spell

    //Transform
    public float AttackRadius;// Attack radius
    public float Distance;// max distance of spell, can be used as radius of circle

    //Unity Object
    public Sprite SkillImageUIVFX;// image on user interface
    public Sprite PlayergroundVFX;// spell max range visible image
    public Sprite MaxRangeVFX;// spell max range visible image
    public Sprite IndicatorVFX;// direction of attack
    public Sprite TargetVFX;// position of attack
    public Sprite BurstVFX;// direction of attack
    public string Skill3DModel;// 3d model of spell
    public string SkillFlashPrefab;// Flash Prefab address
    public string SkillHitPrefab;// Hit Prefab address
    public string Sound;// spell sound
    public string AnimatorProperty;// spell animator property


    //Condition
    public bool IsRestraining;// if skill affects movement
    public bool IsInvisible;// if spell is invisible for enemy
    public bool IsPasive;// if skill is passive
    public bool IsBuff;// if skill is buff or debuff
    public bool IsProjectile;// if skill/spell throws something
    public bool IsRecharged;// if skill/spell is Recharged
    public bool UsingWeapon;// if skill/spell needs weapon

    public bool HasPlayergroundVFX;// if skill has visual ground where player stands
    public bool HasIndicator;// if skill has visual indicator
    public bool HasMaxRange;// if skill has visual max range
    public bool HasTargetVFX;// if skill has visual Target
    public bool HasBurstVFX;// If skill has visual Burst Vfx

    public bool HasRecharging;// if skill has recharging time
}