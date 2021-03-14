using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.Linq;

public class Projectile : MonoBehaviour
{
    public float damage;
    public float speed;

    public GameObject skillLibraryObj;
    

    void Start()
    {
        var Spell = SkillLibrary.Skills[0];

        damage = Spell.PhysicalDamage + Spell.MagicDamage + Spell.SoulDamage;
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
        yield return new WaitForSeconds(15);
        PhotonNetwork.Destroy(gameObject);
    }
}