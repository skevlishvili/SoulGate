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


    // Start is called before the first frame update
    void Update()
    {
        if (true)//!towerScript.PlayerWithinRange[0]
            return;

        bool skillIsActivating = towerScript.TowerSpells.Any(x => x.IsActivating);
        bool skillIsFiring = towerScript.TowerSpells.Any(x => x.IsFiring);


        if (!skillIsActivating && !skillIsFiring)
        {
            for (int i = 0; i < 4; i++)
            {
                if (Input.GetKey(towerScript.TowerSpells[i].KeyCode))
                {
                    AbilityActivation(i);
                    break;
                }
            }
        }
        else if (skillIsActivating && !skillIsFiring)
        {
            if (Input.GetKeyUp((towerScript.TowerSpells[CurrentAbillity].KeyCode)))
            {
                AbilityFire();
            }
        }
    }


    void AbilityActivation(int index)
    {
        CurrentAbillity = index;

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
        if (towerScript.TowerSpells[index].IsActivating && SkillConsumption(index))
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
        if (towerScript.TowerSpells[CurrentAbillity].Skill.Skill3DModel == null)
            return;

        var prefabSrc = towerScript.TowerSpells[CurrentAbillity].Skill.Skill3DModel;

        var spellType = towerScript.TowerSpells[CurrentAbillity].Skill.IsBuff ? 1 :
                        towerScript.TowerSpells[CurrentAbillity].Skill.HasIndicator ? 0 :
                        towerScript.TowerSpells[CurrentAbillity].Skill.HasTargetVFX ? 2 : 1;

        var position = SkillSpawnPoint[spellType].transform.position;
        var rotation = SkillSpawnPoint[spellType].transform.rotation;

        CmdSpawnSkill(prefabSrc, position, rotation);
    }


    private void CmdSpawnSkill(string prefabSrc, Vector3 position, Quaternion rotation)
    {
        NetworkServer.Spawn((GameObject)GameObject.Instantiate(Resources.Load(prefabSrc), position, rotation));
    }

    bool CanCast(int index)
    {
        return towerScript.TowerSpells[index].ActiveCoolDown && !towerScript.IsDestroyed;
    }

    bool SkillConsumption(int index)
    {
        bool value = false;

        //if (SkillIsAvailable[index])
        //{
        //unitStat.Mana -= towerScript.TowerSpells[CurrentAbillity].Skill.ManaConsumption;
        //unitStat.Health -= towerScript.TowerSpells[CurrentAbillity].Skill.HealthConsumption;
        value = true;
        //}

        return value;
    }
}
