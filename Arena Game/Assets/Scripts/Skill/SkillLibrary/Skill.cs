using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : AbstractSkillClass
{
    public override string SkillName
    {
        get;
        set;
    }

    //Damage Types
    public override float PhysicalDamage
    {
        get;
        set;
    }
    public override float MagicDamage
    {
        get;
        set;
    }
    public override float SoulDamage
    {
        get;
        set;
    }

    //Buff/Debuff
    public override float HealthBuff
    {
        get;
        set;
    }
    public override float ManaBuff
    {
        get;
        set;
    }
    public override float SpeedBuff
    {
        get;
        set;
    }
    public override float CooldownBuff
    {
        get;
        set;
    }

    //Consumption
    public override float HealthConsumption
    {
        get;
        set;
    }
    public override float ManaConsumption
    {
        get;
        set;
    }

    //Price
    public override float SkillPriceMoney
    {
        get;
        set;
    }
    public override float SkillPriceXp
    {
        get;
        set;
    }

    //Time
    public override float Duration
    {
        get;
        set;
    }
    public override float Cooldown
    {
        get;
        set;
    }
    public override float ActivationTime
    {
        get;
        set;
    }
    public override float ProjectileSpeed
    {
        get;
        set;
    }
    public override float MaxRechargingTime
    {
        get;
        set;
    }

    //Transform
    public override float Height
    {
        get;
        set;
    }
    public override float Distance
    {
        get;
        set;
    }
    public override float Thickness
    {
        get;
        set;
    }


    //Unity Object
    public override Sprite SkillImageUIVFX
    {
        get;
        set;
    }
    public override Sprite PlayergroundVFX
    {
        get;
        set;
    }
    public override Sprite MaxRangeVFX
    {
        get;
        set;
    }
    public override Sprite IndicatorVFX
    {
        get;
        set;
    }
    public override Sprite TargetVFX
    {
        get;
        set;
    }
    public override string Skill3DModel
    {
        get;
        set;
    }

    public override string Sound
    {
        get;
        set;
    }
    public override string AnimatorProperty
    {
        get;
        set;
    }

    public override string HitVFX
    {
        get;
        set;
    }
    public override string FlashVFX
    {
        get;
        set;
    }

    //Condition
    public override bool IsRestraining
    {
        get;
        set;
    }
    public override bool IsInvisible
    {
        get;
        set;
    }
    public override bool IsPasive
    {
        get;
        set;
    }
    public override bool IsBuff
    {
        get;
        set;
    }
    public override bool IsProjectile
    {
        get;
        set;
    }

    public override bool IsRecharged
    {
        get;
        set;
    }
    public override bool UsingWeapon
    {
        get;
        set;
    }
}