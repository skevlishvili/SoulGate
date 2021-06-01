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

    public GameObject player;

    //public GameObject skillLibraryObj;

    private void Awake()
    {
        Spell = SkillLibrary.Skills[SkillIndex];

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
        Object onHitPref = Resources.Load("Prefabs/Skill/Spark/vfx_hit_v1"); // note: not .prefab!

        GameObject onHitObj = (GameObject)GameObject.Instantiate(onHitPref, vfxPosition, Quaternion.Euler(-90, 0, 0));
        yield return new WaitForSeconds(1);
        Destroy(onHitObj);
    }


    IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(Spell.Duration);
        //PhotonNetwork.Destroy(gameObject);
    }
}