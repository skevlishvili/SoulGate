
using Mirror;
using UnityEngine;

public class Unit : NetworkBehaviour
{
    [Header("Settings")]
    public float MaxHealth = 200;
    public float MaxMana = 200;

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

    public float Money = 5000;
    public float PhysicalDefence = 20;
    public float MagicDefence = 20;
    public float Agility = 20;
    public float Strength = 20;
    public float Intelligence = 20;
    public bool IsDead = false;

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
    public void TakeDamage(float value)
    {
        SetHealth(Mathf.Max(Health - value, 0));
    }



    [Server]
    private void Regeneration()
    {
        float HealthRegen = 2;
        float ManaRegen = 10;


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


}