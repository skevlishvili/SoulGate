using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.AI;

public class PlayerCollision : NetworkBehaviour
{
    [SerializeField]
    private Unit unitStat;

    [SerializeField]
    private NavMeshAgent navmesh;

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
        var projectile = other.GetComponent<Projectile>();
        var burst = other.GetComponent<Burst>();
        var target = other.GetComponent<Target>();

        //if (!PV.IsMine)
        //{
        //    return;
        //}

        if (projectile == null || burst == null || target == null)
                return;

        if (projectile != null)
            if (gameObject.GetInstanceID() == projectile.player.GetInstanceID())
                return;

        if (burst != null)
            if (gameObject.GetInstanceID() == burst.player.GetInstanceID())
                return;

        if (target != null)
            if (gameObject.GetInstanceID() == target.player.GetInstanceID())
                return;

        float damage = projectile != null ? (unitStat.PhysicalDefence / 100 * projectile.damage[0]) + (unitStat.MagicDefence / 100 * projectile.damage[1]) + projectile.damage[2] :
                       burst != null ? (unitStat.PhysicalDefence / 100 * burst.damage[0]) + (unitStat.MagicDefence / 100 * burst.damage[1]) + burst.damage[2] :
                       target != null ? (unitStat.PhysicalDefence / 100 * target.damage[0]) + (unitStat.MagicDefence / 100 * target.damage[1]) + target.damage[2] : 0;

        //ProjPV = other.GetComponent<PhotonView>();

        //Debug.Log("is it triggering?");


        //if (PV.IsMine && !ProjPV.IsMine)
        //{

        //    var score = unitStat.Health - damage <= 0 ? 100 : (int)(damage / 10);
        //    ScoreExtensions.AddScore(ProjPV.Owner, score);
        //    PV.RPC("takeDamage", RpcTarget.All, damage);
        //}

        //PhotonNetwork.Destroy(projectile.gameObject);

        //projectile.gameObject.SetActive(false);

        //GameObject hitVFX = PhotonNetwork.Instantiate("Prefabs/Skill/Spark/vfx_hit_v1", transform.position + Vector3.up * 2, projectile.transform.rotation);
        Debug.Log("Collision");
        //CheckCollision(other.gameObject.GetComponent<SphereCollider>().radius, other.transform.position, damage, projectile);


        projectile.DestroyProjectile(gameObject.transform.position);

        unitStat.TakeDamage(damage);
    }


    [Command]
    private void CheckCollision(float radius, Vector3 position, float damage, Projectile projectile) {
        //if ((position - transform.position).magnitude + radius < 2.0f) 
        //    return;

        //DealDamage(damage);
        projectile.DestroyProjectile(gameObject.transform.position);

        unitStat.TakeDamage(damage);        
    }


    //[ClientRpc]
    //private void DealDamage(float damage) {
    //    unitStat.Health -= damage;
    //} 
}
