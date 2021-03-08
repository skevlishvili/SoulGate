using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class AbstractSkillClass : MonoBehaviour
{
    //Damage Types
    public abstract float PhysicalDamage { get; set; }//Affects only Health
    public abstract float MagicDamage { get; set; }//Affects Health and/or Mana
    public abstract float SoulDamage { get; set; }//Affects Health and/or Mana

    //Buff/Debuff
    public abstract float HealthBuff { get; set; }//Buff and Debuff(10% buff mean Maximum Health will increase)
    public abstract float ManaBuff { get; set; }//Buff and Debuff(10% Debuff of Mana regeneration)
    public abstract float SpeedBuff { get; set; }//Buff and Debuff(affects speed of casting, speed of spell finish, character movement and etc)
    public abstract float CooldownBuff { get; set; }//Buff and Debuff


    //Consumption
    public abstract float HealthConsumption { get; set; }//consumed Health to recreate skill/spell
    public abstract float ManaConsumption { get; set; }//consumed Mana to recreate skill/spell


    //Time
    public abstract float Duration { get; set; }//How long Effect will remain
    public abstract float Cooldown { get; set; }//Time before reusing skill
    public abstract float ActivationTime { get; set; }//Time needed to activate spell/skill
    public abstract float ProjectileSpeed { get; set; }//Speed of magic/Arrow/Sword missles
    public abstract float MaxChargingTime { get; set; }// Maximum time for charging

    //Transform
    public abstract float Height { get; set; }//Height of spell(Barrier Height)
    public abstract float Thickness { get; set; }//Thickness of Magic spell(Barrier Thickness)
    public abstract float Distance { get; set; }//Distance of spell(missle max range)
    public abstract float Radius { get; set; }//Radius of Skill(Barrier radius, Buff/Debuff Radius and etc)


    //Collider And Visual
    public abstract Collider AttackModelType { get; set; }//What Type collider use to detect hit

    public abstract Image SkillUiImage { get; set; }// skill/spell portrait on Ui
    public abstract Image SkillMaxRadiusVFX { get; set; }// Skill Maximum Radius
    public abstract Image SkillDirectionVFX { get; set; }// Skill Direction
    public abstract Image SkillTargetVFX { get; set; }// skill Target



    //Condition
    public abstract bool IsRestraining { get; set; }//If skill/spell affects enemy movement
    public abstract bool IsInvisible { get; set; }//If skill/spell is visible for enemy
    public abstract bool IsPasive { get; set; }//If skill/spell is passive or not
    public abstract bool IsBuff { get; set; }//If skill/spell is Buff or not(if buff is in negative it is Debuff)
    public abstract bool IsProjectile { get; set; }//If skill/spell thows something
    public abstract bool isCoolDown { get; set; }//If CoolDown is over
    public abstract bool isCharging { get; set; }//If skill/spell is charging
    public abstract bool UsingWeapon { get; set; }//If skill/spell needs specific weapon

    public abstract bool HasIndicator { get; set; }//If Skill Has Indicator VFX
    public abstract bool HasMaxRange { get; set; }//If Skill Has Max Range VFX
}