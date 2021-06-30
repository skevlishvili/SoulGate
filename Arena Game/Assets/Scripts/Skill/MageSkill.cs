using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MageSkill : AbstractSkillClass
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

    public override float SoulDamage {
        get { return SoulDamage; }
        set { SoulDamage = value; }
    }

    //Buff{
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
    public override float MaxChargingTime {
        get { return MaxChargingTime; }
        set { MaxChargingTime = value; }
    }

    //Transform
    public override float Height {
        get { return Height; }
        set { Height = value; }
    }
    public override float Thickness
    {
        get { return Thickness; }
        set { Thickness = value; }
    }
    public override float Distance {
        get { return Distance; }
        set { Distance = value; }
    }
    public override float Radius
    {
        get { return Radius; }
        set { Radius = value; }
    }

    //Unity Object
    public override Collider AttackModelType {
        get { return AttackModelType; }
        set { AttackModelType = value; }
    }
    public override Image SkillUiImage {
        get { return SkillUiImage; }
        set { SkillUiImage = value; }
    }
    public override Image SkillMaxRadiusVFX {
        get { return SkillMaxRadiusVFX; }
        set { SkillMaxRadiusVFX = value; }
    }
    public override Image SkillDirectionVFX {
        get { return SkillDirectionVFX; }
        set { SkillDirectionVFX = value; }
    }
    public override Image SkillTargetVFX {
        get { return SkillTargetVFX; }
        set { SkillTargetVFX = value; }
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
    public override bool UsingWeapon {
        get { return UsingWeapon; }
        set { UsingWeapon = value; }
    }

    
    

    
    public override bool IsBuff { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public override bool IsProjectile { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public override bool isCoolDown { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public override bool isCharging { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public override bool HasIndicator { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public override bool HasMaxRange { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
}
