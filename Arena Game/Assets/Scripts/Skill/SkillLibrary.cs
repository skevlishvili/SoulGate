using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillLibrary : MonoBehaviour
{
    public MageSkill FireBall;
    public MageSkill FireWall;



    // Start is called before the first frame update
    void Start()
    {
        Spells(); 
    }

    // Update is called once per frame
    void Spells()
    {
        CapsuleCollider capsuleColliderObj = new CapsuleCollider();
        BoxCollider BoxColliderObj = new BoxCollider();
        SphereCollider SphereColliderObj = new SphereCollider();

        FireBall = new MageSkill
        {
            PhysicalDamage = 0,
            MagicDamage = 20,
            SoulDamage = 0,

            HealthBuff = 0,
            ManaBuff = 0,
            SpeedBuff = 0,
            CooldownBuff = 0,

            HealthConsumption = 0,
            ManaConsumption = 5,

            Duration = 5,
            Cooldown = 5,
            ActivationTime = 0,
            ProjectileSpeed = 10,
            MaxChargingTime = 0,

            Height = 0,
            Thickness = 0,
            Distance = 20,
            Radius = 1,

            AttackModelType = SphereColliderObj,
            //SkillUiImage = ,
            //SkillMaxRadiusVFX = ,
            //SkillDirectionVFX = ,
            //SkillTargetVFX = ,

            IsRestraining = false,
            IsInvisible = false,
            IsPasive = false,
            IsBuff = false,
            IsProjectile = true,
            isCoolDown = true,
            isCharging = false,
            UsingWeapon = false,

            HasIndicator = true,
            HasMaxRange = true,
        };
    }
}
