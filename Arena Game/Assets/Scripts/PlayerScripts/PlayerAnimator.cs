using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class PlayerAnimator : MonoBehaviourPun
{

    private NavMeshAgent agent;
    public Animator anim;

    float motionSmoothTime = .1f;
    PhotonView PV;

    private void Awake()
    {
        PV = gameObject.GetComponent<PhotonView>();
    }

    private void Start()
    {
        anim = gameObject.GetComponentInChildren<Animator>();
        agent = gameObject.GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {
        // if(!PV.IsMine) {
        //     Debug.Log(anim.GetFloat("Blend"));
        // }
        if (PV.IsMine)
        {
            MoveAnimation();
        }
    }

    public void MoveAnimation()
    {
        float speed = agent.velocity.magnitude / agent.speed;
        anim.SetFloat("Blend", speed, motionSmoothTime, Time.deltaTime);
    }

    public void Attack(string animationName)
    {
        anim.SetBool("MagicAttack", true);
        anim.SetFloat("Blend", 0);
        anim.SetBool(animationName, true);
    }

    public void IsDead()
    {
        anim.SetFloat("Blend", 0);
        anim.SetBool("IsDead", true);
    }

    public void IsAlive()
    {
        anim.SetFloat("Blend", 0);
        anim.SetBool("IsDead", false);
    }

    public void EndAnimation(string animationName)
    {
        anim.SetBool("MagicAttack", false);
        anim.SetBool(animationName, false);
    }
}