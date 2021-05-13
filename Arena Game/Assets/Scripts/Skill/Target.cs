using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    Skill Spell;
    public List<float> damage = new List<float>();
    public int SkillIndex;

    //public GameObject skillLibraryObj;

    void Start()
    {
        Spell = SkillLibrary.Skills[SkillIndex];
        damage.Add(Spell.PhysicalDamage);
        damage.Add(Spell.MagicDamage);
        damage.Add(Spell.SoulDamage);

        StartCoroutine(DestroyObject());
    }

    IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(Spell.Duration);
        //PhotonNetwork.Destroy(gameObject);
    }
}
