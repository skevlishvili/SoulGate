using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : AbstractSkillClass
{
    #region Private variables
    private string _SkillName;
    private float _PhysicalDamage;
    private float _MagicDamage;
    private float _SoulDamage;

    private float _HealthBuff;
    private float _ManaBuff;
    private float _SpeedBuff;
    private float _CooldownBuff;


    private float _HealthConsumption;
    private float _ManaConsumption;

    private float _Duration;
    private float _Cooldown;
    private float _ActivationTime;
    private float _ProjectileSpeed;
    private float _MaxRechargingTime;

    private float _Height;
    private float _Distance;
    private float _Thickness;

    private Sprite _SkillImageUIVFX;
    private Sprite _PlayergroundVFX;
    private Sprite _MaxRangeVFX;
    private Sprite _IndicatorVFX;
    private GameObject _Skill3DModel;
    private string _Sound;
    private string _AnimatorProperty;

    private bool _IsRestraining;
    private bool _IsInvisible;
    private bool _IsPasive;
    private bool _IsBuff;
    private bool _IsProjectile;
    private bool _IsRecharged;
    private bool _UsingWeapon;
    private bool _HasPlayergroundVFX;
    private bool _HasIndicator;
    private bool _HasMaxRange;
    private bool _HasRecharging;
    #endregion

    public override string SkillName
    {
        get { return _SkillName; }
        set { _SkillName = value; }
    }

    //Damage Types
    public override float PhysicalDamage
    {
        get { return _PhysicalDamage; }
        set { _PhysicalDamage = value; }
    }
    public override float MagicDamage
    {
        get { return _MagicDamage; }
        set { _MagicDamage = value; }
    }
    public override float SoulDamage
    {
        get { return _SoulDamage; }
        set { _SoulDamage = value; }
    }

    //Buff/Debuff
    public override float HealthBuff
    {
        get { return _HealthBuff; }
        set { _HealthBuff = value; }
    }
    public override float ManaBuff
    {
        get { return _ManaBuff; }
        set { _ManaBuff = value; }
    }
    public override float SpeedBuff
    {
        get { return _SpeedBuff; }
        set { _SpeedBuff = value; }
    }
    public override float CooldownBuff
    {
        get { return _CooldownBuff; }
        set { _CooldownBuff = value; }
    }

    //Consumption
    public override float HealthConsumption
    {
        get { return _HealthConsumption; }
        set { _HealthConsumption = value; }
    }
    public override float ManaConsumption
    {
        get { return _ManaConsumption; }
        set { _ManaConsumption = value; }
    }

    //Time
    public override float Duration
    {
        get { return _Duration; }
        set { _Duration = value; }
    }
    public override float Cooldown
    {
        get { return _Cooldown; }
        set { _Cooldown = value; }
    }
    public override float ActivationTime
    {
        get { return _ActivationTime; }
        set { _ActivationTime = value; }
    }
    public override float ProjectileSpeed
    {
        get { return _ProjectileSpeed; }
        set { _ProjectileSpeed = value; }
    }
    public override float MaxRechargingTime
    {
        get { return _MaxRechargingTime; }
        set { _MaxRechargingTime = value; }
    }

    //Transform
    public override float Height
    {
        get { return _Height; }
        set { _Height = value; }
    }
    public override float Distance
    {
        get { return _Distance; }
        set { _Distance = value; }
    }
    public override float Thickness
    {
        get { return _Thickness; }
        set { _Thickness = value; }
    }


    //Unity Object
    public override Sprite SkillImageUIVFX
    {
        get { return _SkillImageUIVFX; }
        set { _SkillImageUIVFX = value; }
    }
    public override Sprite PlayergroundVFX
    {
        get { return _PlayergroundVFX; }
        set { _PlayergroundVFX = value; }
    }
    public override Sprite MaxRangeVFX
    {
        get { return _MaxRangeVFX; }
        set { _MaxRangeVFX = value; }
    }
    public override Sprite IndicatorVFX
    {
        get { return _IndicatorVFX; }
        set { _IndicatorVFX = value; }
    }
    public override GameObject Skill3DModel
    {
        get { return _Skill3DModel; }
        set { _Skill3DModel = value; }
    }

    public override string Sound
    {
        get { return _Sound; }
        set { _Sound = value; }
    }
    public override string AnimatorProperty
    {
        get { return _AnimatorProperty; }
        set { _AnimatorProperty = value; }
    }

    //Condition
    public override bool IsRestraining
    {
        get { return _IsRestraining; }
        set { _IsRestraining = value; }
    }
    public override bool IsInvisible
    {
        get { return _IsInvisible; }
        set { _IsInvisible = value; }
    }
    public override bool IsPasive
    {
        get { return _IsPasive; }
        set { _IsPasive = value; }
    }
    public override bool IsBuff
    {
        get { return _IsBuff; }
        set { _IsBuff = value; }
    }
    public override bool IsProjectile
    {
        get { return _IsProjectile; }
        set { _IsProjectile = value; }
    }
    public override bool IsRecharged
    {
        get { return _IsRecharged; }
        set { _IsRecharged = value; }
    }
    public override bool UsingWeapon
    {
        get { return _UsingWeapon; }
        set { _UsingWeapon = value; }
    }
    public override bool HasPlayergroundVFX
    {
        get { return _HasPlayergroundVFX; }
        set { _HasPlayergroundVFX = value; }
    }
    public override bool HasIndicator
    {
        get { return _HasIndicator; }
        set { _HasIndicator = value; }
    }
    public override bool HasMaxRange
    {
        get { return _HasMaxRange; }
        set { _HasMaxRange = value; }
    }
    public override bool HasRecharging
    {
        get { return _HasRecharging; }
        set { _HasRecharging = value; }
    }
}