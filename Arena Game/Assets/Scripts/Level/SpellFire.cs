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
    
    [Server]
    void FixedUpdate()
    {
        if (towerScript.PlayerWithinRange.Length == 0)
            return;

        //bool skillIsActivating = towerScript.TowerSpells.Any(x => x.IsActivating);
        //bool skillIsFiring = towerScript.TowerSpells.Any(x => x.IsFiring);


        //if (!skillIsActivating && !skillIsFiring)
        //{
        //    for (int i = 0; i < towerScript.PlayerWithinRange.Length; i++)
        //    {
                AbilityActivation(0);
        //        break;
        //    }
        //}
        //else if (skillIsActivating && !skillIsFiring)
        //{
            AbilityFire();
        //}
    }

    [Server]
    void AbilityActivation(int index)
    {
        if (!towerScript.TowerSpells[index].ActiveCoolDown)
        {
            towerScript.TowerSpells[index].IsActivating = true;
        }
    }

    [Server]
    void AbilityFire()
    {
        towerScript.TowerSpells[CurrentAbillity].ActiveCoolDown = true;
        StartCoroutine(corSkillShot(CurrentAbillity));
    }

    [Server]
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

    [Server]
    public void SpawnSkill()
    {
        if (towerScript.TowerSpells[CurrentAbillity].Skill.Skill3DModel == null)
            return;

        var prefabSrc = towerScript.TowerSpells[CurrentAbillity].Skill.Skill3DModel;

        var position = SkillSpawnPoint[0].transform.position;
        position.y = 16;

        var playerPosition = PlayerCoodinates(towerScript.PlayerWithinRange[0]);

        var player = playerPosition.transform.GetChild(1).gameObject;

        //Vector3 relative = transform.InverseTransformPoint(playerPosition.position);
        //angle = Mathf.Atan2(relative.x, relative.z) * Mathf.Rad2Deg;

        var direction = player.transform.position - transform.position;
        var rotation = Quaternion.LookRotation(direction);



        CmdSpawnSkill(prefabSrc, position, rotation);
    }


    [Server]
    private void CmdSpawnSkill(string prefabSrc, Vector3 position, Quaternion rotation)
    {
        var Spell = (GameObject)Instantiate(Resources.Load(prefabSrc), position, rotation);
        NetworkServer.Spawn(Spell, gameObject);
    }

    [Server]
    public Transform PlayerCoodinates(GameObject Player)
    {
        return Player.transform;
    }
}
