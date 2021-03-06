using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageSkill : AbstractSkillClass
{
    //Damage Types
    public override float PhysicalDamage {
        get { return PhysicalDamage; }
        set{ PhysicalDamage = value; }
    }

    public override float MagicDamage {
        get { return MagicDamage; }
        set { MagicDamage = value; }
    }

    public override float SoulDamage {
        get { return SoulDamage; }
        set { SoulDamage = value; }
    }


    //Consumption
    public override float HealthConsumption {
        get { return HealthConsumption; }
        set { HealthConsumption = value; }
    }

    public override float StaminaConsumption {
        get { return StaminaConsumption; }
        set { StaminaConsumption = value; }
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
    public override float RechargeTime {
        get { return RechargeTime; }
        set { RechargeTime = value; }
    }

    //Transform
    public override float Height {
        get { return Height; }
        set { Height = value; }
    }
    public override float Weight {
        get { return Weight; }
        set { Weight = value; }
    }
    public override float Distance {
        get { return Distance; }
        set { Distance = value; }
    }
    public override float Speed {
        get { return Speed; }
        set { Speed = value; }
    }

    //Unity Object
    public override Collider AttackModelType {
        get { return AttackModelType; }
        set { AttackModelType = value; }
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
}
