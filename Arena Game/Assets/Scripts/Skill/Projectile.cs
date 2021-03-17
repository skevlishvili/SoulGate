using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.Linq;

public class Projectile : MonoBehaviour
{
    Skill Spell;
    public List<float> damage = new List<float>();
    public float speed;

    public GameObject skillLibraryObj;
    

    void Start()
    {
        Spell = SkillLibrary.Skills[0];

        damage.Add(Spell.PhysicalDamage);
        damage.Add(Spell.MagicDamage);
        damage.Add(Spell.SoulDamage);

        speed = Spell.ProjectileSpeed;
    }



    // Update is called once per frame
    void Update()
    {
        StartCoroutine(DestroyObject());

        gameObject.transform.TransformDirection(Vector3.forward);
        gameObject.transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));
    }

    IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(Spell.Duration);
        PhotonNetwork.Destroy(gameObject);
    }
}