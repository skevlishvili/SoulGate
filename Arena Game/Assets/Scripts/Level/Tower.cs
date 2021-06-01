using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    Vector3 Tower_Position;
    Quaternion Tower_Rotation;

    #region Tower Stats
    public int TowerLevel;

    public float TowerRange;

    public float TowerMaxHealth;
    public float TowerHealth;
    public float TowerMaxMana;
    public float TowerMana;
    public float ManaRegen;

    public float DamageMultiplier;
    public float HealingMultiplier;
    public float ManaRegenMultiplalier;

    public bool IsDestroyed;
    #endregion

    GameObject[] Players;
    public List<Abillity> TowerSpells;
    public bool[] PlayerWithinRange;

    NetworkIdentity LastColliderdObjectid;


    // Start is called before the first frame update
    void Start()
    {
         //Players[0] = ClientScene.localPlayer.gameObject;//Gadasaweria mirrorze-----------------------------------------------------------

        Tower_Position = gameObject.transform.localPosition;
        Tower_Position.y = 0.3f;
        Tower_Rotation = gameObject.transform.localRotation;
        Tower_Rotation.z = 0;
        Tower_Rotation.x = 0;

        initStats();

        //InvokeRepeating("Regeneration",1,1);//calls funqtion every second
        //InvokeRepeating("TowerEvolution", 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        //CheckIfPlayerIsWithinRange(0);
        //CheckIfPlayerIsWithinRange(1);
        //CheckIfPlayerIsWithinRange(2);
        //CheckIfPlayerIsWithinRange(3);

        CheckIfDestroyed();
    }

    void CheckIfPlayerIsWithinRange(int PlayerIndex)
    {
        if (Vector3.Distance(gameObject.transform.position, Players[PlayerIndex].transform.position) < TowerRange)
        {
            PlayerWithinRange[PlayerIndex] = true;
            PlayerCoodinates(PlayerIndex);

            Debug.Log("---------------------------Player Within Range ------------------------------");
        }
    }
    public Vector3 PlayerCoodinates(int PlayerIndex)
    {
        return Players[PlayerIndex].transform.position;
    }

    void initStats()
    {
        TowerSpells = new List<Abillity>();
        TowerSpells.Add(new Abillity { ActiveCoolDown = false, Skill = SkillLibrary.TowerSkills[0], IsFiring = false, IsActivating = false });

        TowerLevel = 1;
        TowerRange = 40;

        TowerMaxHealth = 1000;
        TowerHealth = TowerMaxHealth;
        TowerMaxMana = 100;
        TowerMana = 0;

        IsDestroyed = false;

        DamageMultiplier = 1;
        HealingMultiplier = 1;
        ManaRegenMultiplalier = 1;
    }

    void TowerEvolution()
    {
        if (TowerMana >= TowerMaxMana)
        {
            TowerLevel += 1;

            TowerMaxHealth *= 2;
            TowerMaxMana *= 2;

            DamageMultiplier *= 1.5f;
            HealingMultiplier *= 1.5f;
            ManaRegenMultiplalier *= 1.5f;
        }
    }

    void Regeneration()
    {
        var Regen = IsDestroyed ? 0 :
                       TowerHealth == TowerMaxHealth? TowerMana += ManaRegenMultiplalier * ManaRegen :
                       TowerHealth + HealingMultiplier * ManaRegen > TowerMaxHealth ? TowerHealth = TowerMaxHealth :
                       TowerHealth += HealingMultiplier * ManaRegen; TowerMana += ManaRegenMultiplalier * ManaRegen / 2;
    }

    void takeDamage(float damage)
    {
        TowerHealth -= damage;
    }

    void CheckIfDestroyed()
    {
        if (TowerHealth <= 0)
        {
            IsDestroyed = true;
            Object DestroyedTower = Resources.Load("Prefabs/Level/DestroyedTower_1");
            Instantiate(DestroyedTower, Tower_Position, Tower_Rotation);
            gameObject.SetActive(false);
        }
    }

    float CalculateDamage(Collider other)
    {
        var projectile = other.GetComponent<Projectile>();
        var burst = other.GetComponent<Burst>();
        var target = other.GetComponent<Target>();

        float Damage = 0;

        if (projectile != null)
        {
            Damage = projectile.damage[0] + projectile.damage[1] + projectile.damage[2];
        }
        if (burst != null)
        {
            Damage = burst.damage[0] + burst.damage[1] + burst.damage[2];
        }
        if (target != null)
        {
            Damage = target.damage[0] + target.damage[1] + target.damage[2];
        }

        return Damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Spell")
        {
            if (LastColliderdObjectid == other.gameObject.GetComponent<NetworkIdentity>())
            {
                return;
            }

            takeDamage(CalculateDamage(other));
            Debug.Log(TowerHealth);
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        var projectile = other.GetComponent<Projectile>();
        var burst = other.GetComponent<Burst>();
        var target = other.GetComponent<Target>();

        float Damage = 0;

        if (projectile != null)
        {
            Damage = projectile.damage[0] + projectile.damage[1] + projectile.damage[2];
        }
        if (burst != null)
        {
            Damage = burst.damage[0] + burst.damage[1] + burst.damage[2];
        }
        if (target != null)
        {
            Damage = target.damage[0] + target.damage[1] + target.damage[2];
        }

        if (other.tag == "Spell")
        {
            if (LastColliderdObjectid == other.gameObject.GetComponent<NetworkIdentity>())
            {
                return;
            }

            takeDamage(Damage);
        }
    }
}
