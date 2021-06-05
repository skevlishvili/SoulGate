
using Mirror;
using System;
using UnityEngine;

public class Unit : NetworkBehaviour
{
    [Header("Settings")]
    public float MaxHealth = 100;
    public float MaxMana = 100;

    [SyncVar]
    [SerializeField] private float _health = 100;
    [SerializeField] private float _mana = 100;

    public float Health
    {
        get { return _health; }
        private set { _health = value; }
    }

    public float Mana
    {
        get { return _mana; }
        private set { _mana = value; }
    }



    public delegate void HealthChangedDelegate(float Health, float MaxHealth);
    public event HealthChangedDelegate EventHealthChanged;


    public delegate void PlayerDeathDelegate(GameObject currentPlayer, GameObject killerPlayer);
    public event PlayerDeathDelegate EventPlayerDeath;


    public float Money = 5000;
    public float PhysicalDefence = 5; //% procentulia
    public float MagicDefence = 10;//% procentulia
    public float Agility = 20;
    public float Strength = 20;
    public float Intelligence = 20;
    public bool IsDead = false;
    public bool IsReady = false;
    public PlayerAnimator Animator;
    private GameObject lastDamageReceivedFrom;

    [ServerCallback]
    private void Update()
    {
        CheckDeath();
    }

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
        SetHealth(Mathf.Max(Health - value, 0));

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

        float HealthRegen = 2;
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
        _mana = MaxMana;
        IsDead = false;

        ReviveRpc();
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

        ReadyRpc();
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

        UnreadyRpc();
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
        IsDead = true;
        IsReady = false;
        EventPlayerDeath?.Invoke(gameObject, lastDamageReceivedFrom);
        DeathRpc();
    }

    [ClientRpc]
    private void ReviveRpc()
    {
        _health = MaxHealth;
        _mana = MaxMana;
        IsDead = false;
        Animator.IsAlive();
    }

    [ClientRpc]
    private void DeathRpc()
    {
        IsReady = false;
        IsDead = true;
        Animator.IsDead();
    }

    [ClientRpc]
    private void ReadyRpc()
    {
        IsReady = true;
    }

    [ClientRpc]
    private void UnreadyRpc()
    {
        IsReady = false;
    }

}