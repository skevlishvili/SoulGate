using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractUnitClass : MonoBehaviour
{
    public abstract float Health { get; set; }
    public abstract float Stamina { get; set; }
    public abstract float Mana { get; set; }
    public abstract int Xp { get; set; }

    public abstract float Height { get; set; }
    public abstract float weight { get; set; }

    public abstract int strength { get; set; }
    public abstract int Agility { get; set; }
    public abstract int Intelligence { get; set; }
    public abstract int Charisma { get; set; }

    public abstract bool IsWounded { get; set; }
    public abstract bool IsDead { get; set; }
    public abstract bool IsHungry { get; set; }
    public abstract bool IsThirsty { get; set; }
}
