using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityHelper : MonoBehaviour
{
    Abillities abilities;


    // Start is called before the first frame update
    void Start()
    {
        GameObject Player = GameObject.Find("Player");
        abilities = Player.GetComponent<Abillities>();

    }

    //Called in Animation Event
    public void SpawnSkill()
    {

        // Debug.Log(abilities.projSpawnPoint.transform.rotation);

        abilities.canSkillshot = true;
        Instantiate(abilities.projPrefab, abilities.projSpawnPoint.transform.position, abilities.projSpawnPoint.transform.rotation);
    }


    public void PlayFireSound()
    {        
        SoundManagerScript.PlaySound("fire");
    }
}
