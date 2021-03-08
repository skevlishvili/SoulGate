using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : AbstractUnitClass
{
    public override float Health {
        get { return Health; }
        set { Health = value; }
    }
    public override float Mana {
        get { return Mana; }
        set { Mana = value; }
    }
    public override int Xp {
        get { return Xp; }
        set { Xp = value; }
    }
    public override int Money {
        get { return Money; }
        set { Money = value; }
    }
    public override float Height {
        get { return Height; }
        set { Height = value; }
    }
    public override float weight {
        get { return weight; }
        set { weight = value; }
    }

    public override int strength {
        get { return strength; }
        set { strength = value; }
    }
    public override int Agility {
        get { return Agility; }
        set { Agility = value; }
    }
    public override int Intelligence {
        get { return Intelligence; }
        set { Intelligence = value; }
    }
    public override int Charisma {
        get { return Charisma; }
        set { Charisma = value; }
    }

    public override bool IsHalfHealth {
        get { return IsHalfHealth; }
        set { IsHalfHealth = value; }
    }
    public override bool IsDead {
        get { return IsDead; }
        set { IsDead = value; }
    }
}
