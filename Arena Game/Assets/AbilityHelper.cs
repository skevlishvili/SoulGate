using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class AbilityHelper : MonoBehaviour
{
    Abillities abilities;
    public Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        GameObject Player = ClientScene.localPlayer.gameObject;
        abilities = Player.GetComponent<Abillities>();
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
