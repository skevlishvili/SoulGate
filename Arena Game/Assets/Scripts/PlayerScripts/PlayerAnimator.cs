using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class PlayerAnimator : NetworkBehaviour
{

    private NavMeshAgent agent;
    public Animator anim;

    float motionSmoothTime = .1f;

    private void Awake()
    {

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
        //if (PV.IsMine)
        //{
            MoveAnimation();
        //}
    }

    [Client]
    public void MoveAnimation()
    {
        float speed = agent.velocity.magnitude / agent.speed;
        anim.SetFloat("Blend", speed, motionSmoothTime, Time.deltaTime);
    }

    [Client]
    public void Attack(string animationName)
    {
        anim.SetBool("MagicAttack", true);
        anim.SetFloat("Blend", 0);
        anim.SetBool(animationName, true);
    }


    [Client]
    public void StopAttack(string animationName)
    {
        anim.SetBool("MagicAttack", false);
        anim.SetFloat("Blend", 0);
        anim.SetBool(animationName, false);
    }

    [Client]
    public void IsDead()
    {
        anim.SetFloat("Blend", 0);
        anim.SetBool("IsDead", true);
    }

    [Client]
    public void IsAlive()
    {
        anim.SetFloat("Blend", 0);
        anim.SetBool("IsDead", false);
    }

    [Client]
    public void EndAnimation(string animationName)
    {
        anim.SetBool("MagicAttack", false);
        anim.SetBool(animationName, false);
    }
}