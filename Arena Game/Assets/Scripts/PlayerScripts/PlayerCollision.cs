using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerCollision : NetworkBehaviour
{
    [SerializeField]
    private Unit unitStat;
    public Health HealthScript;

    int LastColliderdObjectid;

    [Server]
    private void OnTriggerEnter(Collider other)
    {
        Projectile projectile = other.GetComponent<Projectile>();

        if (projectile == null)
            return;

        if (gameObject.GetInstanceID() == projectile.player.GetInstanceID())
            return;

        float damage = (1 - unitStat.PhysicalDefence / 100) * projectile.damage[0] + (1 - unitStat.MagicDefence / 100) * projectile.damage[1] + projectile.damage[2];

        projectile.DestroyProjectile(gameObject.transform.position);

        if (base.isServer)
        {
            HealthScript.PastHealthObj.SetActive(true);
            HealthScript.PastHealthSliderValue(unitStat.Health);
            unitStat.TakeDamage(damage, projectile.player);
        }
    }

    [Server]
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Tower_Spell")
        {
            var towerScript = other.GetComponentInParent<Tower>();
            float damage = (1 - unitStat.PhysicalDefence / 100) * towerScript.DamageMultiplier * SkillLibrary.TowerSkills[0].PhysicalDamage + (1 - unitStat.MagicDefence / 100) * towerScript.DamageMultiplier * SkillLibrary.TowerSkills[0].MagicDamage + towerScript.DamageMultiplier * SkillLibrary.TowerSkills[0].SoulDamage;

            if (base.isServer)
            {
                HealthScript.PastHealthObj.SetActive(true);
                HealthScript.PastHealthSliderValue(unitStat.Health);
                unitStat.TakeDamage(damage, null);
            } 
        }
    }


    [Server]
    private void OnParticleCollision(GameObject other)
    {
        Projectile projectile = other.GetComponent<Projectile>();
        if (projectile == null)
            return;

        if (gameObject.GetInstanceID() == projectile.player.GetInstanceID())
            return;

        float Damage = (1 - unitStat.PhysicalDefence / 100) * projectile.damage[0] + (1 - unitStat.MagicDefence / 100) * projectile.damage[1] + projectile.damage[2];

        if (other.tag == "Spell")
        {
            if (LastColliderdObjectid == other.gameObject.GetInstanceID())
            {
                return;
            }

            LastColliderdObjectid = other.gameObject.GetInstanceID();

            HealthScript.PastHealthObj.SetActive(true);
            HealthScript.PastHealthSliderValue(unitStat.Health);
            unitStat.TakeDamage(Damage, projectile.player);
        }
    }
}