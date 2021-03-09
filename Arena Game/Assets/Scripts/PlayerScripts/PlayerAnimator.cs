﻿using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class PlayerAnimator : MonoBehaviourPun
{

    private NavMeshAgent agent;
    private Animator anim;

    float motionSmoothTime = .1f;


    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        agent = gameObject.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        //if(this.photonView.IsMine == true)
        //{
        //    return;
        //}

        Move();
    }

    public void Move()
    {
        Debug.Log("bhjbhc");
        float speed = agent.velocity.magnitude / agent.speed;
        anim.SetFloat("Blend", speed, motionSmoothTime, Time.deltaTime);
    }

    public void Attack(KeyCode key)
    {
        if (key == KeyCode.Mouse0)
        {
            anim.SetFloat("Blend", 0);
            anim.SetTrigger("BaseAttack");
        }

        else if (key == KeyCode.Alpha1)
        {
            anim.SetFloat("Blend", 0);
            anim.SetTrigger("SkillOne");
        }
    }
}
