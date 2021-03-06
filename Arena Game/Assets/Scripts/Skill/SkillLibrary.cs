using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillLibrary : MonoBehaviour
{
    public MageSkill FireSpell;
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

        FireSpell = new MageSkill
        {
            PhysicalDamage = 0,
            MagicDamage = 20,
            SoulDamage = 0,

            HealthConsumption = 0,
            StaminaConsumption = 0,
            ManaConsumption = 5,

            Duration = 5,
            RechargeTime = 5,

            Height = 0.5f,
            Weight = 0.5f,
            Distance = 5,
            Speed = 5,

            AttackModelType = capsuleColliderObj,

            IsRestraining = false,
            IsInvisible = false,
            IsPasive = false,
            UsingWeapon = false
        };

        FireWall = new MageSkill
        {
            PhysicalDamage = 0,
            MagicDamage = 20,
            SoulDamage = 0,

            HealthConsumption = 0,
            StaminaConsumption = 0,
            ManaConsumption = 10,

            Duration = 5,
            RechargeTime = 10,

            Height = 2,
            Weight = 5,
            Distance = 2,
            Speed = 5,

            AttackModelType = BoxColliderObj,

            IsRestraining = false,
            IsInvisible = false,
            IsPasive = false,
            UsingWeapon = false
        };

    }
}
