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

    public float DamageMultiplier;
    public float HealingMultiplier;

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

        IsDestroyed = false;

        DamageMultiplier = 1;
        HealingMultiplier = 1;
    }

    //void TowerEvolution()
    //{
    //    if (TowerMana >= TowerMaxMana)
    //    {
    //        TowerLevel += 1;

    //        TowerMaxHealth *= 2;
    //        TowerMaxMana *= 2;

    //        DamageMultiplier *= 1.5f;
    //        HealingMultiplier *= 1.5f;
    //        ManaRegenMultiplalier *= 1.5f;
    //    }
    //}

    //void Regeneration()
    //{
    //    var Regen = IsDestroyed ? 0 :
    //                   TowerHealth == TowerMaxHealth? TowerMana += ManaRegenMultiplalier * ManaRegen :
    //                   TowerHealth + HealingMultiplier * ManaRegen > TowerMaxHealth ? TowerHealth = TowerMaxHealth :
    //                   TowerHealth += HealingMultiplier * ManaRegen; TowerMana += ManaRegenMultiplalier * ManaRegen / 2;
    //}

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

    [Server]
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Spell")
        {
            Projectile projectile = other.GetComponent<Projectile>();
            if (projectile == null)
                return;

            float Damage = projectile.damage[0] + projectile.damage[1] + projectile.damage[2];

            
            Skill Spell = SkillLibrary.Skills[HelpMethods.GetSkillIndexByName(other.name)];

            // ----------------------------------------------gasasworebelia---------------------------------------------
            //Vector3 contact = other.gameObject.GetComponent<Collider>().ClosestPoint(transform.position);
            Vector3 contact = other.gameObject.transform.position;
            //Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact);
            Quaternion rot = Quaternion.FromToRotation(Vector3.zero, Vector3.zero);
            Vector3 pos = contact + contact.normalized;
            //----------------------------------------------------------------------------------------------------------

            if (Spell.SkillHitPrefab != null)
            {
                GameObject hit = (GameObject)Resources.Load(Spell.SkillHitPrefab);
                var hitInstance = Instantiate(hit, pos, rot);
                hitInstance.transform.LookAt(contact + contact.normalized);
            }

            takeDamage(Damage);

            projectile.DestroyProjectile(gameObject.transform.position);

            Debug.Log(TowerHealth);
        }
    }

    [Server]
    private void OnParticleCollision(GameObject other)
    {
        Projectile projectile = other.GetComponent<Projectile>();
        if (projectile == null)
            return;

        float Damage = projectile.damage[0] + projectile.damage[1] + projectile.damage[2];


        if (other.tag == "Spell")
        {
            if (LastColliderdObjectid == other.gameObject.GetComponent<NetworkIdentity>())
            {
                return;
            }

            LastColliderdObjectid = other.gameObject.GetComponent<NetworkIdentity>();

            takeDamage(Damage);
            Debug.Log(TowerHealth);
        }
    }
}
