using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerCollision : NetworkBehaviour
{
    [SerializeField]
    private Unit unitStat;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    [Client]
    private void OnTriggerEnter(Collider other)
    {
        //if (!PV.IsMine)
        //{
        //    return;
        //}
        Projectile projectile = other.GetComponent<Projectile>();//----------------------------gasasworebelia---------------
        if (projectile == null)
            return;
        //ProjPV = other.GetComponent<PhotonView>();

        float damage = projectile.damage[0] + projectile.damage[1] + projectile.damage[2];//----------------------------gasasworebelia---------------

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
        CheckCollision(other.gameObject.GetComponent<SphereCollider>().radius, other.transform.position, damage);
    }


    [Command]
    private void CheckCollision(float radius, Vector3 position, float damage) {
        if ((position - transform.position).magnitude + radius < 2.0f) 
            return;

        //DealDamage(damage);

        unitStat.TakeDamage(damage);
    }


    //[ClientRpc]
    //private void DealDamage(float damage) {
    //    unitStat.Health -= damage;
    //} 
}
