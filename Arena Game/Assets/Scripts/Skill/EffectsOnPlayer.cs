using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsOnPlayer : MonoBehaviour
{
    Skill Spell;
    public int SkillIndex;

    GameObject Player_PrefabObj;
    Unit unitStat;

    void Start()
    {
        Player_PrefabObj = GameObject.FindGameObjectWithTag("Player");
        unitStat = Player_PrefabObj.GetComponentInChildren<Unit>();

        Spell = SkillLibrary.Skills[SkillIndex];
        StartCoroutine(DestroyObject());

        if (Spell.IsPasive)
        {
            if (Spell.IsBuff)
            {
                PassiveBuffON();
            }
            else
            {
                ActiveEffect();
            }
        }
    }

    void ActiveEffect()
    {
        if (Spell.HealthBuff != 0 || Spell.ManaBuff != 0)
        {
            Regeneration();
        }
    }

    void PassiveBuffON()
    {
        if (Spell.HealthBuff != 0 || Spell.ManaBuff != 0)
        {
            MaxStatChange(true);
        }
    }

    void PassiveBuffOFF()
    {
        if (Spell.HealthBuff != 0 || Spell.ManaBuff != 0)
        {
            MaxStatChange(false);
        }
    }

    void Regeneration()
    {
        //float HealthRegen = unitStat.MaxHealth * Spell.HealthBuff / 100;
        //float ManaRegen = unitStat.MaxMana * Spell.ManaBuff / 100;

        //if ((unitStat.Health + HealthRegen) <= unitStat.MaxHealth)
        //{
        //    unitStat.Health += HealthRegen;
        //}
        //else
        //{
        //    unitStat.Health = unitStat.MaxHealth;
        //}

        //if ((unitStat.Mana + ManaRegen) <= unitStat.MaxMana)
        //{
        //    unitStat.Mana += ManaRegen;
        //}
        //else
        //{
        //    unitStat.Mana = unitStat.MaxMana;
        //}
    }

    void MaxStatChange(bool condition)
    {
        //float MaxHealth = unitStat.MaxMana * Spell.HealthBuff / 100;
        //float MaxMana = unitStat.MaxMana * Spell.ManaBuff / 100;

        //if (condition)
        //{
        //    unitStat.Health += MaxHealth;
        //    unitStat.MaxMana += MaxMana;
        //}
        //else
        //{
        //    unitStat.Health -= MaxHealth;
        //    unitStat.MaxMana -= MaxMana;
        //}
        
    }

    IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(Spell.Duration);
        if (Spell.IsBuff)
        {
            PassiveBuffOFF();
        }
        //PhotonNetwork.Destroy(gameObject);
    }
}
