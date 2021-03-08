using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillLibrary : MonoBehaviour
{
    public Skill FireSpell;

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

        FireSpell = new Skill
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
            MaxRechargingTime = 0,

            Height = 0.5f,
            Distance = 100,
            Thickness = 0,

            AttackModelType = capsuleColliderObj,

            IsRestraining = false,
            IsInvisible = false,
            IsPasive = false,
            UsingWeapon = false
        };
    }
}
