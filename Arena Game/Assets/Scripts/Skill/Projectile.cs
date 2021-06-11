using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Mirror;

public class Projectile : NetworkBehaviour
{
    Skill Spell;

    public int SkillIndex;
    public List<float> damage = new List<float>();
    public float speed;

    public GameObject hit;
    public GameObject flash;
    private Rigidbody rb;
    public GameObject player;


    //-----Audio------------------
    GameObject AudioManagerObj;
    public bool AudioRepeating = false;
    public float AudioStartTime = 0.0f;
    public float AudioRepeatTime = 1f;

    

    //Useless parts
    public bool UseFirePointRotation;
    public float hitOffset = 0f;
    public Vector3 rotationOffset = new Vector3(0, 0, 0);
    public GameObject[] Detached;



    void Start()
    {
        player = ClientScene.localPlayer.gameObject;
        Unit unitStats = player.GetComponent<Unit>(); 

        Spell = SkillLibrary.Skills[SkillIndex];
        rb = GetComponent<Rigidbody>();
        AudioManagerObj = GameObject.FindGameObjectWithTag("Audio");
        AudioManagerScript audioManager = AudioManagerObj.GetComponent<AudioManagerScript>();
        audioManager.PlaySound(SkillIndex, AudioRepeating, AudioStartTime, AudioRepeatTime);

        float BasicDamage = unitStats.Damage / 3;
        damage.Add(Spell.PhysicalDamage + BasicDamage);
        damage.Add(Spell.MagicDamage + BasicDamage);
        damage.Add(Spell.SoulDamage + BasicDamage);

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
            //rb.velocity = transform.forward * speed;
            transform.position += transform.forward * (speed * Time.deltaTime);         
        }
    }

    //void OnCollisionEnter(Collision collision)
    //{


    //    Destroy(gameObject);
    //}

    [Server]
    public void DestroyProjectile(Vector3 vfxPosition)
    {
        DestroyProjectileRpc(vfxPosition);
    }


    [ClientRpc]
    public void DestroyProjectileRpc(Vector3 vfxPosition)
    {
        StartCoroutine("CreateVFX", vfxPosition);
        Destroy(gameObject);
    }

    [Client]
    IEnumerator CreateVFX(Vector3 vfxPosition)
    {
        //Object onHitPref = Resources.Load("Prefabs/Skill/Spark/vfx_hit_v1"); // note: not .prefab!
        Object onHitPref = Resources.Load(Spell.SkillHitPrefab); // note: not .prefab!

        GameObject onHitObj = (GameObject)GameObject.Instantiate(onHitPref, vfxPosition, Quaternion.Euler(-90, 0, 0));
        yield return new WaitForSeconds(1);
        Destroy(onHitObj);
    }

    IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(Spell.Duration);


        ////Lock all axes movement and rotation
        //rb.constraints = RigidbodyConstraints.FreezeAll;
        //speed = 0;

        //ContactPoint contact = collision.contacts[0];
        //Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        //Vector3 pos = contact.point + contact.normal * hitOffset;

        //if (Spell.SkillHitPrefab != null)
        //{
        //    hit = (GameObject)Resources.Load(Spell.SkillHitPrefab);
        //    var hitInstance = Instantiate(hit, pos, rot);
        //    if (UseFirePointRotation) { hitInstance.transform.rotation = gameObject.transform.rotation * Quaternion.Euler(0, 180f, 0); }
        //    else if (rotationOffset != Vector3.zero) { hitInstance.transform.rotation = Quaternion.Euler(rotationOffset); }
        //    else { hitInstance.transform.LookAt(contact.point + contact.normal); }

        //    var hitPs = hitInstance.GetComponent<ParticleSystem>();
        //    if (hitPs != null)
        //    {
        //        Destroy(hitInstance, hitPs.main.duration);
        //    }
        //    else
        //    {
        //        var hitPsParts = hitInstance.transform.GetChild(0).GetComponent<ParticleSystem>();
        //        Destroy(hitInstance, hitPsParts.main.duration);
        //    }
        //}
        //foreach (var detachedPrefab in Detached)
        //{
        //    if (detachedPrefab != null)
        //    {
        //        detachedPrefab.transform.parent = null;
        //    }
        //}

        Destroy(gameObject);
    }
}