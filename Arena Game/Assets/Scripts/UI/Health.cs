using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : NetworkBehaviour
{
    public Text Healthtext;
    public Slider healthSlider3D;
    public Slider healthSlider2D;

    [Header("References")]
    [SerializeField] private Unit unitstat = null;

    void Awake()
    {
        unitstat = GetComponentInParent<Unit>();
    }


    // Start is called before the first frame update
    void Start()
    {
        healthSlider2D = GetComponentInChildren<Slider>();
        healthSlider3D = GetComponentInChildren<Slider>();

        Healthtext.text = $"{unitstat.Health}/{unitstat.MaxHealth}";

        healthSlider2D.maxValue = unitstat.MaxHealth;
        healthSlider3D.maxValue = unitstat.MaxHealth;
    }

    //private void OnEnable()
    //{
    //    unitstat.EventHealthChanged += Unitstat_EventHealthChanged;
    //}
    //private void OnDisable()
    //{
    //    unitstat.EventHealthChanged -= Unitstat_EventHealthChanged;
    //}

    //private void Unitstat_EventHealthChanged(float health, float maxHealth)
    //{
    //    //float currentHealth = Mathf.Min(Health, MaxHealth);
    //    //Debug.Log($"health {health}");
    //    CmdChangeHealth(health);
    //}


    //[Command]
    //private void CmdChangeHealth(float health)
    //{
    //    //float currentHealth = Mathf.Min(Health, MaxHealth);

    //    ChangeHealthRpc(health);
    //}

    //[ClientRpc]
    //private void ChangeHealthRpc(float health)
    //{
    //    Debug.Log($"health {health}");
    //    healthSlider2D.value = health;
    //    healthSlider3D.value = health;
    //}




    //// Update is called once per frame
    void Update()
    {
        Healthtext.text = $"{unitstat.Health}/{unitstat.MaxHealth}";

        healthSlider2D.maxValue = unitstat.MaxHealth;
        healthSlider3D.maxValue = unitstat.MaxHealth; 

        healthSlider2D.value = unitstat.Health;
        healthSlider3D.value = unitstat.Health;
    }
}