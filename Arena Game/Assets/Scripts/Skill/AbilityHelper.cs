using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityHelper : MonoBehaviour
{
    private Abillities abilities;
    private Animator anim;

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
        //abilities.projSpawnPoint.transform.rotation
        //abilities.canSkillshot = true;
        Instantiate(abilities.projPrefab, abilities.projSpawnPoint.transform.position, abilities.projSpawnPoint.transform.rotation);
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
