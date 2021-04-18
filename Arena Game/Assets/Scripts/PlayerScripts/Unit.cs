
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : AbstractUnitClass
{
    public override float Health
    {
        get;
        set;
    }
    public override float Mana
    {
        get;
        set;
    }
    public override float Xp
    {
        get;
        set;
    }
    public override float Money
    {
        get;
        set;
    }

    public override float PhysicalDefence
    {
        get;
        set;
    }
    public override float MagicDefence
    {
        get;
        set;
    }


    public override float Height
    {
        get;
        set;
    }
    public override float weight
    {
        get;
        set;
    }

    public override int strength
    {
        get;
        set;
    }
    public override int Agility
    {
        get;
        set;
    }
    public override int Intelligence
    {
        get;
        set;
    }
    public override int Charisma
    {
        get;
        set;
    }

    public override bool IsHalfHealth
    {
        get;
        set;
    }
    public override bool IsDead
    {
        get;
        set;
    }
}