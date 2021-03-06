using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : NetworkBehaviour
{
    Vector3 Tower_Position;
    Quaternion Tower_Rotation;

    #region Tower Stats
    public int TowerLevel;

    public float TowerRange;

    public float TowerMaxHealth;
    public float TowerHealth;

    public float TowerMaxXpMultiplier;
    public float TowerMaxXp;
    public float TowerXp;

    public float DamageMultiplier;
    public float HealingMultiplier;


    public bool IsDestroyed;
    #endregion

    public List<Abillity> TowerSpells;

    public GameObject Target;
    public Players Manager;

    public GameObject[] Crystals;

    public GameObject Laser;
    int ActiveCrystal;


    GameObject lastDamageReceivedFrom;
    int LastColliderdObjectid;


    // Start is called before the first frame update
    void Start()
    {
        Tower_Position = gameObject.transform.localPosition;
        Tower_Position.y = 0.3f;
        Tower_Rotation = gameObject.transform.localRotation;
        Tower_Rotation.z = 0;
        Tower_Rotation.x = 0;

        initStats();

        InvokeRepeating("Regeneration",1,1);//calls funqtion every second
        InvokeRepeating("TowerEvolution", 1, 1);
        InvokeRepeating("CheckAvaliableCrystals", 1, 1);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckIfPlayerIsWithinRange();
    }

    void CheckIfPlayerIsWithinRange()
    {
        //if (Manager.PlayersGameObjects.Count >= 1)
        //{
        //    var readyPlayers = Manager.PlayersGameObjects.Count;
        //    PlayerWithinRange = new GameObject[readyPlayers];

        //    for (int i = 0; i < readyPlayers; i++)
        //    {
        //        if (Vector3.Distance(gameObject.transform.position, Manager.PlayersGameObjects[i].transform.position) < TowerRange)
        //        {
        //            //Debug.Log($"-------------------------------------------{gameObject.name}-------------{Vector3.Distance(gameObject.transform.position, Manager.PlayersGameObjects[i].transform.position)}---------------------------------------");
        //            PlayerWithinRange[i] = Manager.PlayersGameObjects[i];
        //            Laser.SetActive(true);
        //        }
        //    }
        //}

        var center = new Vector3(gameObject.transform.position.x, 0, gameObject.transform.position.z);
        Collider[] hitColliders = Physics.OverlapSphere(center, TowerRange);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.tag.Contains("Player")) {
                Target = hitCollider.gameObject;
                return;
            }
        }

        Target = null;
    }

    void initStats()
    {
        TowerSpells = new List<Abillity>();
        TowerSpells.Add(new Abillity { ActiveCoolDown = false, Skill = SkillLibrary.TowerSkills[0], IsFiring = false, IsActivating = false });

        TowerLevel = 1;
        TowerRange = 20;

        TowerMaxHealth = 100;
        TowerHealth = TowerMaxHealth;

        TowerMaxXpMultiplier = 1;
        TowerMaxXp = TowerMaxXpMultiplier * 1000;
        TowerXp = 0;

        DamageMultiplier = 1;
        HealingMultiplier = 1;

        IsDestroyed = false;
    }

    void TowerEvolution()
    {
        if (TowerXp >= TowerMaxXp)
        {
            TowerLevel += 1;
            TowerRange += 5;

            TowerMaxHealth *= 2;

            TowerMaxXpMultiplier *= 2;
            TowerXp = 0;

            DamageMultiplier *= 2f;
            HealingMultiplier *= 1.5f;

            initStats();
        }
    }

    void Regeneration()
    {
        var XpIncrease = TowerMaxXpMultiplier * ActiveCrystal + TowerMaxXpMultiplier;
        var XpHalfIncrease = TowerMaxXpMultiplier * ActiveCrystal / 2 + TowerMaxXpMultiplier;

        var HealthIncrese = HealingMultiplier * ActiveCrystal + 1;

        var Regen = IsDestroyed ? 0 :
                       TowerHealth == TowerMaxHealth ? TowerXp += XpIncrease :
                       TowerHealth + HealthIncrese > TowerMaxHealth ? TowerHealth = TowerMaxHealth :
                       TowerHealth += HealthIncrese; TowerXp += XpHalfIncrease;
    }

    void takeDamage(float damage)
    {
        TowerHealth -= damage;
        CheckIfDestroyed();
    }

    void CheckIfDestroyed()
    {
        if (TowerHealth <= 0)
        {
            IsDestroyed = true;
            Object DestroyedTower = Resources.Load("Prefabs/Level/DestroyedTower_1");
            Instantiate(DestroyedTower, Tower_Position, Tower_Rotation);
            gameObject.SetActive(false);
            if(base.isServer)
                TowerDestroyRpc(lastDamageReceivedFrom);
        }
    }

    [ClientRpc]
    private void TowerDestroyRpc(GameObject killer) {
        killer.GetComponent<Unit>().PlayerScoreEventScoreChange(500);
    }

    [Server]
    public void CheckAvaliableCrystals()
    {
        ActiveCrystal = 0;

        foreach (var item in Crystals)
        {
            if (item.activeInHierarchy)
            {
                ActiveCrystal += 1;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Spell")
        {
            Projectile projectile = other.GetComponent<Projectile>();
            if (projectile == null)
                return;

            float Damage = projectile.damage[0] + projectile.damage[1] + projectile.damage[2];

            
            Skill Spell = SkillLibrary.Skills[HelpMethods.GetSkillIndexByName(other.name)];

            //----------------------------------------------gasasworebelia---------------------------------------------
            Vector3 contact = other.gameObject.GetComponent<Collider>().ClosestPoint(transform.position);
            //Vector3 contact = other.gameObject.transform.position;

            Quaternion rot = Quaternion.identity;
            Vector3 pos = contact;


            //----------------------------------------------------------------------------------------------------------

            if (Spell.SkillHitPrefab != null)
            {
                GameObject hitPrefab = (GameObject)Resources.Load(Spell.SkillHitPrefab);
                var hitInstance = Instantiate(hitPrefab, pos, rot);
                //hitInstance.transform.LookAt(contact + contact.normalized);
            }

            lastDamageReceivedFrom = projectile.player;
            takeDamage(Damage);

            projectile.DestroyProjectile(gameObject.transform.position);
        }
    }



    private void OnParticleCollision(GameObject other)
    {
        Projectile projectile = other.GetComponent<Projectile>();
        if (projectile == null)
            return;

        float Damage = projectile.damage[0] + projectile.damage[1] + projectile.damage[2];


        if (other.tag == "Spell")
        {
            if (LastColliderdObjectid == other.gameObject.GetInstanceID())
            {
                return;
            }

            LastColliderdObjectid = other.gameObject.GetInstanceID();

            lastDamageReceivedFrom = projectile.player;
            takeDamage(Damage);
        }
    }
}
