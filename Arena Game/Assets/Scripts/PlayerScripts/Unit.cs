
using Mirror;
using System;
using UnityEngine;
using UnityEngine.AI;

public struct UnitStruct
{
    public float Health;
    public float HealthRegen;
    public float PhysicalDefence;
    public float MagicDefence;
    public float Damage;
    public float Agility;
    public float MoneyRegen;
    public float AbilityCooldown;
}


public class Unit : NetworkBehaviour
{
    [Header("Settings")]
    public float baseMaxHealth = 100;
    public float baseHealthRegen = 1;
    public float basePhysicalDefence = 20;
    public float baseMagicDefence = 20;
    public float baseDamage = 20;
    public float baseAgility = 20;
    public float baseMoneyRegen = 1;
    public float baseAbilityCooldown = 0;


    public float MaxHealth;
    public float HealthRegen;
    public float PhysicalDefence;
    public float MagicDefence;
    public float Damage;
    public float Agility;
    public float MoneyRegen;
    public float AbilityCooldown;

    public float Money = 5000;

    [SyncVar]
    public bool IsDead = false;
    [SyncVar]
    public bool IsReady = false;


    [SyncVar]
    [SerializeField] private float _health = 100;
    [SerializeField] private NavMeshAgent _agent;

    public float Health
    {
        get { return _health; }
        private set { _health = value; }
    }

    public Vector3 SpawnPoint;

    public delegate void HealthChangedDelegate(float Health, float MaxHealth);
    public event HealthChangedDelegate EventHealthChanged;


    public delegate void PlayerDeathDelegate(GameObject currentPlayer, GameObject killerPlayer);
    public event PlayerDeathDelegate EventPlayerDeath;

    public delegate void PlayerDamageDelegate(float damage);
    public event PlayerDamageDelegate EventPlayerDamage;

    public delegate void PlayerMoneyDelegate(float money);
    public event PlayerMoneyDelegate EventPlayerMoney;

    public PlayerAnimator Animator;
    private GameObject lastDamageReceivedFrom;
    private RoundManager roundManager;
    private PlayerScore playerScore;

    private void Start()
    {
        SpawnPoint = gameObject.transform.position;
        MaxHealth = baseMaxHealth;
        HealthRegen = baseHealthRegen;
        PhysicalDefence = basePhysicalDefence;
        MagicDefence = baseMagicDefence;
        Damage = baseDamage;
        Agility = baseAgility;
        MoneyRegen = baseMoneyRegen;
        AbilityCooldown = baseAbilityCooldown;

        IsDead = false;

        var roundMangerObjs = GameObject.FindGameObjectsWithTag("RoundManager");
        if (roundMangerObjs.Length > 0 && roundMangerObjs[0] != null)
        {
            roundManager = roundMangerObjs[0].GetComponent<RoundManager>();
            if (isClient)
            {
                InvokeRepeating("PassiveIncome", 1.0f, 1.0f);
                playerScore = gameObject.GetComponent<PlayerScore>();
                //playerScore.EventScoreChange += PlayerScore_EventScoreChange;
            }
        }

    }

    [Client]
    public void PlayerScoreEventScoreChange(int score)
    {
        EventPlayerMoney?.Invoke(score);
        Money += score;
    }
    

    private void Update()
    {
        if (isServer)
            CheckDeath();

        PassiveIncome();
    }

    public void ChangeUnitStats(float[] Values)
    {
        MaxHealth = baseMaxHealth + Values[0];
        HealthRegen = baseHealthRegen + Values[1];
        PhysicalDefence = basePhysicalDefence + Values[2];
        MagicDefence = baseMagicDefence + Values[3];
        Damage = baseDamage + Values[4];
        Agility = baseAgility + Values[5];
        MoneyRegen = baseMoneyRegen + Values[6];
        AbilityCooldown = baseAbilityCooldown + Values[7];
    }

    private void PassiveIncome()
    {
        if (roundManager.CurrentState == RoundManager.RoundState.RoundStart)
        {
            Money += MoneyRegen * Time.deltaTime;
        }
    }


    //private void PassiveIncome() {
    //    if (roundManager.CurrentState == RoundManager.RoundState.RoundStart)
    //    {
    //        Money += 1;
    //    }
    //}

    [Server]
    private void SetHealth(float value)
    {
        _health = value;
        EventHealthChanged?.Invoke(_health, MaxHealth);
    }

    [Server]
    public void Regen(float value)
    {
        SetHealth(Mathf.Min(Health + value, MaxHealth));
    }


    [Server]
    public void TakeDamage(float value, GameObject player)
    {
        lastDamageReceivedFrom = player;
        TakeDamageRpc(value);
        SetHealth(Mathf.Max(Health - value, 0));
    }

    [ClientRpc]
    private void TakeDamageRpc(float damage)
    {
        EventPlayerDamage?.Invoke(damage);
    }

    public override void OnStartClient()
    {
        if (!hasAuthority)
            return;
        StartRegen();
    }


    public override void OnStartServer()
    {
        SetHealth(MaxHealth);
    }

    [Command]
    private void StartRegen() {
        InvokeRepeating("Regeneration", 1.0f, 1.0f);
    }

    [Server]
    private void Regeneration()
    {
        if (IsDead)
            return;

        //float ManaRegen = 10;

        Regen(HealthRegen);

        //if ((unitStat.Mana + ManaRegen) <= unitStat.MaxMana)
        //{
        //    unitStat.Mana += ManaRegen;
        //}
        //else
        //{
        //    unitStat.Mana = unitStat.MaxMana;
        //}

    }


    [Server]
    public void Revive() {
        _health = MaxHealth;
        IsDead = false;

        _agent.SetDestination(SpawnPoint);
        _agent.Warp(SpawnPoint);
        gameObject.transform.position = SpawnPoint;
        ReviveRpc();
        IsReady = false;
    }

    [Command]
    public void ReadyCmd()
    {
        Ready();
    }

    [Server]
    public void Ready()
    {
        IsReady = true;

        //ReadyRpc();
    }


    [Command]
    public void UnreadyCmd()
    {
        Unready();
    }

    [Server]
    public void Unready()
    {
        IsReady = false;

        //UnreadyRpc();
    }



    [Server]
    private void CheckDeath()
    {
        if (Health <= 0)
        {
            Death();
        }
    }

    [Server]
    public void Death() {
        if (IsDead)
            return;
        IsDead = true;
        IsReady = false;
        EventPlayerDeath?.Invoke(gameObject, lastDamageReceivedFrom);
        DeathRpc(lastDamageReceivedFrom);
    }

    [ClientRpc]
    private void ReviveRpc()
    {
        _health = MaxHealth;
        IsDead = false;
        //_agent.isStopped = true;
        Animator.IsAlive();
        _agent.SetDestination(SpawnPoint);
        _agent.Warp(SpawnPoint);
        gameObject.transform.position = SpawnPoint;
    }

    [ClientRpc]
    private void DeathRpc(GameObject killer)
    {
        killer.GetComponent<Unit>().PlayerScoreEventScoreChange(1000);
        IsReady = false;
        IsDead = true;
        Animator.IsDead();
    }

    //[ClientRpc]
    //private void ReadyRpc()
    //{
    //    IsReady = true;
    //}

    //[ClientRpc]
    //private void UnreadyRpc()
    //{
    //    IsReady = false;
    //}

}