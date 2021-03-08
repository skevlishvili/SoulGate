using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : AbstractSkillClass
{
    //Damage Types
    public override float PhysicalDamage {
        get { return PhysicalDamage; }
        set { PhysicalDamage = value; }
    }
    public override float MagicDamage {
        get { return MagicDamage; }
        set { MagicDamage = value; }
    }
    public override float SoulDamage
    {
        get { return SoulDamage; }
        set { SoulDamage = value; }
    }


    //Buff/Debuff
    public override float HealthBuff {
        get { return HealthBuff; }
        set { HealthBuff = value; }
    }
    public override float ManaBuff {
        get { return ManaBuff; }
        set { ManaBuff = value; }
    }
    public override float SpeedBuff {
        get { return SpeedBuff; }
        set { SpeedBuff = value; }
    }
    public override float CooldownBuff {
        get { return CooldownBuff; }
        set { CooldownBuff = value; }
    }


    //Consumption
    public override float HealthConsumption {
        get { return HealthConsumption; }
        set { HealthConsumption = value; }
    }
    public override float ManaConsumption {
        get { return ManaConsumption; }
        set { ManaConsumption = value; }
    }


    //Time
    public override float Duration {
        get { return Duration; }
        set { Duration = value; }
    }
    public override float Cooldown {
        get { return Cooldown; }
        set { Cooldown = value; }
    }
    public override float ActivationTime {
        get { return ActivationTime; }
        set { ActivationTime = value; }
    }
    public override float ProjectileSpeed {
        get { return ProjectileSpeed; }
        set { ProjectileSpeed = value; }
    }
    public override float MaxRechargingTime {
        get { return MaxRechargingTime; }
        set { MaxRechargingTime = value; }
    }


    //Transform
    public override float Height {
        get { return Height; }
        set { Height = value; }
    }
    public override float Distance {
        get { return Distance; }
        set { Distance = value; }
    }
    public override float Thickness {
        get { return Thickness; }
        set { Thickness = value; }
    }


    //Unity Object
    public override Collider AttackModelType {
        get { return AttackModelType; }
        set { AttackModelType = value; }
    }
    public override Sprite SkillImageUIVFX {
        get { return SkillImageUIVFX; }
        set { SkillImageUIVFX = value; }
    }
    public override Sprite MaxRangeVFX {
        get { return MaxRangeVFX; }
        set { MaxRangeVFX = value; }
    }
    public override Sprite IndicatorVFX {
        get { return IndicatorVFX; }
        set { IndicatorVFX = value; }
    }
    public override GameObject Skill3DModel {
        get { return Skill3DModel; }
        set { Skill3DModel = value; }
    }

    //Condition
    public override bool IsRestraining {
        get { return IsRestraining; }
        set { IsRestraining = value; }
    }
    public override bool IsInvisible {
        get { return IsInvisible; }
        set { IsInvisible = value; }
    }
    public override bool IsPasive {
        get { return IsPasive; }
        set { IsPasive = value; }
    }
    public override bool IsBuff {
        get { return IsBuff; }
        set { IsBuff = value; }
    }
    public override bool IsProjectile {
        get { return IsProjectile; }
        set { IsProjectile = value; }
    }
    public override bool IsRecharged {
        get { return IsRecharged; }
        set { IsRecharged = value; }
    }
    public override bool UsingWeapon {
        get { return UsingWeapon; }
        set { UsingWeapon = value; }
    }
    public override bool HasIndicator {
        get { return HasIndicator; }
        set { HasIndicator = value; }
    }
    public override bool HasMaxRange { 
        get { return HasMaxRange; }
        set { HasMaxRange = value; }
    }
    public override bool HasRecharging {
        get { return HasRecharging; }
        set { HasRecharging = value; }
    }
}
