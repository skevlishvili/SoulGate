using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractSkillClass : MonoBehaviour
{
    public abstract string SkillName { get; set; }// name


    //Damage Types
    public abstract float PhysicalDamage { get; set; }// affects only health
    public abstract float MagicDamage { get; set; }// affects Health and Mana
    public abstract float SoulDamage { get; set; }// True Damage(affects Health and Mana)

    //Buff/Debuff
    public abstract float HealthBuff { get; set; }// Max health increase/decrease and Health regen
    public abstract float ManaBuff { get; set; }// Max Mana increase/decrease and Mana regen
    public abstract float SpeedBuff { get; set; }// Max speed increase
    public abstract float CooldownBuff { get; set; }// Max cooldown Decrease

    //Consumption
    public abstract float HealthConsumption { get; set; }// uses health to cast spell/skill
    public abstract float ManaConsumption { get; set; }// uses mana to cast spell/skill

    //Time
    public abstract float Duration { get; set; }// duration of skill/spell
    public abstract float Cooldown { get; set; }// Cooldown time
    public abstract float ActivationTime { get; set; }// time before spell/skill activates
    public abstract float ProjectileSpeed { get; set; }// speed of thrown object
    public abstract float MaxRechargingTime { get; set; }// max time for recharging spell

    //Transform
    public abstract float Height { get; set; }// barrier height
    public abstract float Distance { get; set; }// max distance of spell, can be used as radius of circle
    public abstract float Thickness { get; set; }// barrier thickness

    //Unity Object
    public abstract Sprite SkillImageUIVFX { get; set; }// image on user interface
    public abstract Sprite PlayergroundVFX { get; set; }// spell max range visible image
    public abstract Sprite MaxRangeVFX { get; set; }// spell max range visible image
    public abstract Sprite IndicatorVFX { get; set; }// direction of attack
    public abstract GameObject Skill3DModel { get; set; }// 3d model of spell


    //Condition
    public abstract bool IsRestraining { get; set; }// if skill affects movement
    public abstract bool IsInvisible { get; set; }// if spell is invisible for enemy
    public abstract bool IsPasive { get; set; }// if skill is passive
    public abstract bool IsBuff { get; set; }// if skill is buff or debuff
    public abstract bool IsProjectile { get; set; }// if skill/spell throws something
    public abstract bool IsRecharged { get; set; }// if skill/spell is Recharged
    public abstract bool UsingWeapon { get; set; }// if skill/spell needs weapon

    public abstract bool HasPlayergroundVFX { get; set; }// if skill has visual ground where player stands
    public abstract bool HasIndicator { get; set; }// if skill has visual indicator
    public abstract bool HasMaxRange { get; set; }// if skill has visual max range

    public abstract bool HasRecharging { get; set; }// if skill has recharging time
}
