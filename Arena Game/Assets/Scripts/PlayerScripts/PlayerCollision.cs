using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerCollision : NetworkBehaviour
{
    [SerializeField]
    private Unit unitStat;

    NetworkIdentity LastColliderdObjectid;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    [Server]
    private void OnTriggerEnter(Collider other)
    {

        //if (!PV.IsMine)
        //{
        //    return;
        //}
        Projectile projectile = other.GetComponent<Projectile>();
        if (projectile == null)
            return;

        if (gameObject.GetInstanceID() == projectile.player.GetInstanceID())
            return;

        //ProjPV = other.GetComponent<PhotonView>();

        float damage = (1 - unitStat.PhysicalDefence / 100) * projectile.damage[0] + (1 - unitStat.MagicDefence / 100) * projectile.damage[1] + projectile.damage[2];//----------------------------gasasworebelia---------------

        Debug.Log("is it triggering?");


        //if (PV.IsMine && !ProjPV.IsMine)
        //{

        //    var score = unitStat.Health - damage <= 0 ? 100 : (int)(damage / 10);
        //    ScoreExtensions.AddScore(ProjPV.Owner, score);
        //    PV.RPC("takeDamage", RpcTarget.All, damage);
        //}

        //PhotonNetwork.Destroy(projectile.gameObject);

        //projectile.gameObject.SetActive(false);

        //projectile.DestroyProjectile();
        //GameObject hitVFX = PhotonNetwork.Instantiate("Prefabs/Skill/Spark/vfx_hit_v1", transform.position + Vector3.up * 2, projectile.transform.rotation);
        Debug.Log("Collision");
        //CheckCollision(other.gameObject.GetComponent<SphereCollider>().radius, other.transform.position, damage, projectile);


        projectile.DestroyProjectile(gameObject.transform.position);

        unitStat.TakeDamage(damage, projectile.player);
    }

    [Server]
    private void OnParticleCollision(GameObject other)
    {
        Projectile projectile = other.GetComponent<Projectile>();
        if (projectile == null)
            return;

        float Damage = (1 - unitStat.PhysicalDefence / 100) * projectile.damage[0] + (1 - unitStat.MagicDefence / 100) * projectile.damage[1] + projectile.damage[2];//----------------------------gasasworebelia---------------


        if (other.tag == "Spell")
        {
            if (LastColliderdObjectid == other.gameObject.GetComponent<NetworkIdentity>())
            {
                return;
            }

            LastColliderdObjectid = other.gameObject.GetComponent<NetworkIdentity>();

            unitStat.TakeDamage(Damage, projectile.player);
        }
    }


    [Command]
    private void CheckCollision(float radius, Vector3 position, float damage, Projectile projectile)
    {
        //if ((position - transform.position).magnitude + radius < 2.0f) 
        //    return;

        //DealDamage(damage);
        projectile.DestroyProjectile(gameObject.transform.position);

        unitStat.TakeDamage(damage, projectile.player);        
    }


    //[ClientRpc]
    //private void DealDamage(float damage) {
    //    unitStat.Health -= damage;
    //} 
}