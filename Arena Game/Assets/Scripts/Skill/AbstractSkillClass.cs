using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public abstract class AbstractSkillClass : MonoBehaviour
//{
//    //Damage Types
//    public abstract float PhysicalDamage { get; set; }
//    public abstract float MagicDamage { get; set; }
//    public abstract float SoulDamage { get; set; }

//    //Buff/Debuff
//    public abstract float HealthBuff { get; set; }
//    public abstract float ManaBuff { get; set; }
//    public abstract float SpeedBuff { get; set; }
//    public abstract float CooldownBuff { get; set; }

//    //Consumption
//    public abstract float HealthConsumption { get; set; }
//    public abstract float ManaConsumption { get; set; }

//    //Time
//    public abstract float Duration { get; set; }
//    public abstract float Cooldown { get; set; }
//    public abstract float ActivationTime { get; set; }
//    public abstract float ProjectileSpeed { get; set; }

//    //Transform
//    public abstract float Height { get; set; }
//    public abstract float Distance { get; set; }
//    public abstract float Thickness { get; set; }
//    public abstract float Radius { get; set; }

//    //Unity Object
//    public abstract Collider AttackModelType { get; set; }

//    //Condition
//    public abstract bool IsRestraining { get; set; }
//    public abstract bool IsInvisible { get; set; }
//    public abstract bool IsPasive { get; set; }
//    public abstract bool IsBuff { get; set; }
//    public abstract bool IsProjectile { get; set; }
//    public abstract bool UsingWeapon { get; set; }
//}


public abstract class AbstractSkillClass : MonoBehaviour
{
    //Damage Types
    public abstract float PhysicalDamage { get; set; }
    public abstract float MagicDamage { get; set; }
    public abstract float SoulDamage { get; set; }

    //Consumption
    public abstract float HealthConsumption { get; set; }
    public abstract float StaminaConsumption { get; set; }
    public abstract float ManaConsumption { get; set; }

    //Time
    public abstract float Duration { get; set; }
    public abstract float RechargeTime { get; set; }

    //Transform
    public abstract float Height { get; set; }
    public abstract float Weight { get; set; }
    public abstract float Distance { get; set; }
    public abstract float Speed { get; set; }

    //Unity Object
    public abstract Collider AttackModelType { get; set; }

    //Condition
    public abstract bool IsRestraining { get; set; }
    public abstract bool IsInvisible { get; set; }
    public abstract bool IsPasive { get; set; }
    public abstract bool UsingWeapon { get; set; }
}
