using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class AbilityHelper : MonoBehaviour
{
    private Abillities abilities;
    private Animator anim;

    PhotonView PV;


    void Awake()
    {
        PV = gameObject.GetComponent<PhotonView>();
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject Player = GameObject.Find("Player");
        anim = gameObject.GetComponent<Animator>();
        abilities = gameObject.GetComponent<Abillities>();
    }

    //Called in Animation Event
    public void SpawnSkill()
    {
        if (PV.IsMine)
        {
            PhotonNetwork.Instantiate("Prefabs/Fireball", abilities.projSpawnPoint.transform.position, abilities.projSpawnPoint.transform.rotation);
        }
    }

    public void PlayFireSound()
    {   
        SoundManagerScript sound = GameObject.Find("SoundManager").GetComponent<SoundManagerScript>();
        sound.PlaySound("fire");
    }

     public void EndAnimation()
    {   
        anim.SetBool("SkillOne", false);
        abilities.StopFiring();
    }
}
