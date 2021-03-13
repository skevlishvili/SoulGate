using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillLibrary:MonoBehaviour
{
    public Skill FireBall;

    // Start is called before the first frame update
    void Awake()
    {
        Spells();
    }

    // Update is called once per frame
    void Spells()
    {
        FireBall = new Skill
        {
            PhysicalDamage = 0,
            MagicDamage = 20,
            SoulDamage = 0,

            HealthBuff = 0,
            ManaBuff = 0,
            SpeedBuff = 0,
            CooldownBuff = 0,

            HealthConsumption = 0,
            ManaConsumption = 40,

            Duration = 5,
            Cooldown = 5,
            ActivationTime = 0,
            ProjectileSpeed = 50,
            MaxRechargingTime = 0,

            Height = 0,
            Distance = 250,
            Thickness = 0,

            SkillImageUIVFX = Resources.Load<Sprite>("Design/Skill/Skill UI Image/Fireball"),
            PlayergroundVFX = Resources.Load<Sprite>("Design/Skill/Skill Indicator/FireCircle"),
            MaxRangeVFX = null,
            IndicatorVFX = Resources.Load<Sprite>("Design/Skill/Skill Indicator/Indicator"),
            Skill3DModel = GameObject.Find("Assets/Resources/Prefabs/Carrot.prefab"),

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
            HasRecharging = false
        };
    }
}
