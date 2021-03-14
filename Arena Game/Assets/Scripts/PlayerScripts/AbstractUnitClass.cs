using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractUnitClass : MonoBehaviour
{
    public abstract float Health { get; set; }
    public abstract float Mana { get; set; }
    public abstract int Xp { get; set; }
    public abstract int Money { get; set; }

    public abstract float PhysicalDefence { get; set; }
    public abstract float MagicDefence { get; set; }

    public abstract float Height { get; set; }//ar moqmedebs tamashze
    public abstract float weight { get; set; }//ar moqmedebs tamashze

    public abstract int strength { get; set; }
    public abstract int Agility { get; set; }
    public abstract int Intelligence { get; set; }
    public abstract int Charisma { get; set; }

    public abstract bool IsHalfHealth { get; set; }
    public abstract bool IsDead { get; set; }
}
