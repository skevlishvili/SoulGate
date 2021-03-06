using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : AbstractUnitClass
{
    [SerializeField] public float THealth;
    [SerializeField] public float TStamina;
    [SerializeField] public float TMana;
    [SerializeField] public int TXp;


    [SerializeField] public int Tstrength;
    [SerializeField] public int TAgility;
    
    
    
    //REFERANCES
    [SerializeField] private TextMesh StatDisplay;


    private void Start()
    {
        InvokeRepeating("DisplayStats", 0.0f, 0.1f);
    }

    void DisplayStats()
    {
        string text = "Health: {0:n1} \nStamina: {1:n1} \nMana: {1:n1}";
        StatDisplay.text = string.Format(text, THealth, TStamina, TMana, 32);
    }

    public override float Health { get => throw new System.NotImplementedException();  set => throw new System.NotImplementedException(); }
    public override float Stamina { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public override float Mana { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public override int Xp { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public override float Height { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public override float weight { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public override int strength { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public override int Agility { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public override int Intelligence { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public override int Charisma { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public override bool IsWounded { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public override bool IsDead { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public override bool IsHungry { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public override bool IsThirsty { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
}
