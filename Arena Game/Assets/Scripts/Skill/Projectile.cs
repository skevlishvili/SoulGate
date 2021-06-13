using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Mirror;

public class Projectile : NetworkBehaviour
{
    Skill Spell;

    public int SkillIndex;
    public int TowerSkillIndex;
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
        if (SkillIndex == 0)
        {
            Spell = SkillLibrary.TowerSkills[TowerSkillIndex];
            damage.Add(Spell.PhysicalDamage);
            damage.Add(Spell.MagicDamage);
            damage.Add(Spell.SoulDamage);
        }
        else
        {
            Unit unitStats = player.GetComponent<Unit>();
            Spell = SkillLibrary.Skills[SkillIndex];
            float BasicDamage = unitStats.Damage / 3;
            damage.Add(Spell.PhysicalDamage + BasicDamage);
            damage.Add(Spell.MagicDamage + BasicDamage);
            damage.Add(Spell.SoulDamage + BasicDamage);
        }

        rb = GetComponent<Rigidbody>();
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

        AudioManagerObj = GameObject.FindGameObjectWithTag("Audio");
        AudioManagerScript audioManager = AudioManagerObj.GetComponent<AudioManagerScript>();
        audioManager.PlaySound(SkillIndex, AudioRepeating, AudioStartTime, AudioRepeatTime);

        Destroy(gameObject, Spell.Duration);

    }

    [Server]
    void FixedUpdate()
    {
        if (speed != 0)
        {
            //rb.velocity = transform.forward * speed;
            transform.position += transform.forward * (speed * Time.deltaTime);         
        }
    }

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
        Object onHitPref = Resources.Load(Spell.SkillHitPrefab); // note: not .prefab!

        GameObject onHitObj = (GameObject)GameObject.Instantiate(onHitPref, vfxPosition, Quaternion.Euler(-90, 0, 0));
        yield return new WaitForSeconds(1);
        Destroy(onHitObj);
    }

    IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(Spell.Duration);

        Destroy(gameObject);
    }
}