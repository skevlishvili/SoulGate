using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class PlayerAnimator : MonoBehaviourPun
{

    private NavMeshAgent agent;
    private Animator anim;

    float motionSmoothTime = .1f;
    PhotonView PV;

    private void Awake()
    {
        PV = gameObject.GetComponent<PhotonView>();
    }

    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
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
            Move();
        }
    }

    public void Move()
    {
        float speed = agent.velocity.magnitude / agent.speed;

        // if(anim.GetFloat("Blend") < 0.01) {
        //     anim.SetFloat("Blend", 0);
        // } else {
        anim.SetFloat("Blend", speed, motionSmoothTime, Time.deltaTime);
        // }
    }

    public void Attack(string animationName)
    {
        if (animationName == "SkillOne")
        {
            anim.SetFloat("Blend", 0);
            anim.SetBool("SkillOne",true);
        }

        else if (animationName == "SkillTwo")
        {
            anim.SetFloat("Blend", 0);
            anim.SetBool("SkillTwo", true);
        }

        else if (animationName == "SkillThree")
        {
            anim.SetFloat("Blend", 0);
        }
    }

    public void IsDead()
    {
        anim.SetFloat("Blend", 0);
        anim.SetBool("IsDead",true);
    }

    public void EndAnimation(string animationName)
    {
        anim.SetBool(animationName, false);
    }
}
