using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mana : MonoBehaviour
{
    Slider ManaSlider3D;
    Slider ManaSlider2D;

    public GameObject player;
    public Unit unitstat;

    void Awake()
    {
        unitstat = player.GetComponent<Unit>();
    }


    // Start is called before the first frame update
    void Start()
    {
        ManaSlider2D = GetComponent<Slider>();
        ManaSlider3D = GetComponent<Slider>();

        ManaSlider2D.maxValue = unitstat.MaxMana;
        ManaSlider3D.maxValue = unitstat.MaxMana;
    }

    // Update is called once per frame
    void Update()
    {
        ManaSlider2D.value = unitstat.Mana;
        ManaSlider3D.value = unitstat.Mana;
    }
}