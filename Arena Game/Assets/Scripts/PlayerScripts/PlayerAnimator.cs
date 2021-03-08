using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class PlayerAnimator : MonoBehaviour,IUnitAction
{
    //Variable
    float motionSmoothTime = .1f;


    //Referance
    public NavMeshAgent agent;
    public Animator anim;

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
            anim.SetTrigger("Skill1");
        }

        else if (key == KeyCode.Alpha2)
        {
            anim.SetFloat("Blend", 0);
            anim.SetTrigger("Skill2");
        }
    }

    public void Idle()
    {
        throw new System.NotImplementedException();
    }

    public void Move()
    {
        float speed = agent.velocity.magnitude / agent.speed;
        anim.SetFloat("Blend", speed, motionSmoothTime, Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
}
