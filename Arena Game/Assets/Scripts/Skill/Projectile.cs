using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Mirror;

public class Projectile : NetworkBehaviour
{
    Skill Spell;
    public List<float> damage = new List<float>();
    public float speed;
    public int SkillIndex;

    //public GameObject skillLibraryObj;

    void Start()
    {
        Spell = SkillLibrary.Skills[SkillIndex];
        rb = GetComponent<Rigidbody>();

        damage.Add(Spell.PhysicalDamage);
        damage.Add(Spell.MagicDamage);
        damage.Add(Spell.SoulDamage);

        speed = Spell.ProjectileSpeed; 

        if (Spell.SkillFlashPrefab != null)
        {
            flash = (GameObject)Resources.Load(Spell.SkillFlashPrefab);
            var flashInstance = Instantiate(flash, transform.position, Quaternion.identity);
            flashInstance.transform.forward = gameObject.transform.forward;
            var flashPs = flashInstance.GetComponent<ParticleSystem>();
            if (flashPs != null)
            {
                Destroy(flashInstance, flashPs.main.duration);
            }
            else
            {
                var flashPsParts = flashInstance.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(flashInstance, flashPsParts.main.duration);
            }
        }
        Destroy(gameObject, Spell.Duration);
    }

    void FixedUpdate()
    {
        if (speed != 0)
        {
            rb.velocity = transform.forward * speed;
            //transform.position += transform.forward * (speed * Time.deltaTime);         
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        //Lock all axes movement and rotation
        rb.constraints = RigidbodyConstraints.FreezeAll;
        speed = 0;

        ContactPoint contact = collision.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point + contact.normal * hitOffset;


    public void DestroyProjectile()
    {
        Destroy(gameObject);
    }

    IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(Spell.Duration);
        //PhotonNetwork.Destroy(gameObject);
    }
}