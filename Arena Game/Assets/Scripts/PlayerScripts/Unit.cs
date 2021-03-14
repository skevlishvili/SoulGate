using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : AbstractUnitClass
{
    #region Private variables
    private float _Health;
    private float _Mana;
    private int _Xp;
    private int _Money;

    private float _PhysicalDefence;
    private float _MagicDefence;

    private float _Height;
    private float _weight;

    private int _strength;
    private int _Agility;
    private int _Intelligence;
    private int _Charisma;

    private bool _IsHalfHealth;
    private bool _IsDead;

    #endregion

    public override float Health
    {
        get { return _Health; }
        set { _Health = value; }
    }
    public override float Mana
    {
        get { return _Mana; }
        set { _Mana = value; }
    }
    public override int Xp
    {
        get { return _Xp; }
        set { _Xp = value; }
    }
    public override int Money
    {
        get { return _Money; }
        set { _Money = value; }
    }

    public override float PhysicalDefence
    {
        get { return _PhysicalDefence; }
        set { _PhysicalDefence = value; }
    }
    public override float MagicDefence
    {
        get { return _MagicDefence; }
        set { _MagicDefence = value; }
    }


    public override float Height
    {
        get { return _Height; }
        set { _Height = value; }
    }
    public override float weight
    {
        get { return _weight; }
        set { _weight = value; }
    }

    public override int strength
    {
        get { return _strength; }
        set { _strength = value; }
    }
    public override int Agility
    {
        get { return _Agility; }
        set { _Agility = value; }
    }
    public override int Intelligence
    {
        get { return _Intelligence; }
        set { _Intelligence = value; }
    }
    public override int Charisma
    {
        get { return _Charisma; }
        set { _Charisma = value; }
    }

    public override bool IsHalfHealth
    {
        get { return _IsHalfHealth; }
        set { _IsHalfHealth = value; }
    }
    public override bool IsDead
    {
        get { return _IsDead; }
        set { _IsDead = value; }
    }
}
