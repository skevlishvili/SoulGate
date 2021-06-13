using Mirror;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpellFire : MonoBehaviour
{
    public Tower towerScript;

    public GameObject[] SkillSpawnPoint;
    private int CurrentAbillity;
    public bool TowerIsFiring = false;


    [Server]
    void FixedUpdate()
    {
        if (towerScript.PlayerWithinRange.All(x => !(x != null)))
        {
            if (!TowerIsFiring)
            {
                TowerIsFiring = true;
                SpawnSkill();
            }
        }

        //if (towerScript.PlayerWithinRange[0] == null)
        //    return;

        //if (!TowerIsFiring)
        //{
        //    TowerIsFiring = true;
        //    SpawnSkill();
        //}
           
        
        //AbilityActivation(0);
        //AbilityFire();
    }


    void AbilityActivation(int index)
    {
        if (!towerScript.TowerSpells[index].ActiveCoolDown)
        {
            towerScript.TowerSpells[index].IsActivating = true;
        }
    }

    void AbilityFire()
    {
        towerScript.TowerSpells[CurrentAbillity].ActiveCoolDown = true;
        StartCoroutine(corSkillShot(CurrentAbillity));
    }

    IEnumerator corSkillShot(int index)
    {
        if (towerScript.TowerSpells[index].IsActivating)
        {
            towerScript.TowerSpells[index].IsActivating = false;
            towerScript.TowerSpells[index].IsFiring = true;

            yield return new WaitForSeconds(towerScript.TowerSpells[CurrentAbillity].Skill.ActivationTime);

            SpawnSkill();
            towerScript.TowerSpells[index].IsFiring = false;
            towerScript.TowerSpells[index].IsActivating = false;
        }
    }

    public void SpawnSkill()
    {
        if (towerScript.TowerSpells[0].Skill.Skill3DModel == null)
            return;

        var prefabSrc = towerScript.TowerSpells[0].Skill.Skill3DModel;

        var position = SkillSpawnPoint[0].transform.position;
        position.y = 16;

        var rotation = Quaternion.identity;

        CmdSpawnSkill(prefabSrc, position, rotation);
    }

    private void CmdSpawnSkill(string prefabSrc, Vector3 position, Quaternion rotation)
    {
        var Spell = (GameObject)Instantiate(Resources.Load(prefabSrc), position, rotation);
        Spell.GetComponent<Hovl_Laser>().TowerScript = towerScript;
        Spell.GetComponent<Hovl_Laser>().SpellFireScript = gameObject.GetComponent<SpellFire>();
        NetworkServer.Spawn(Spell, gameObject);
    }
}
