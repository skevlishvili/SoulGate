using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mana : MonoBehaviour
{
    PhotonView PV;

    Slider ManaSlider3D;
    Slider ManaSlider2D;

    public float MaxMana;

    public GameObject player;
    public Unit unitstat;

    void Awake()
    {
        PV = GetComponentInParent<PhotonView>();
        unitstat = player.GetComponent<Unit>();
    }


    // Start is called before the first frame update
    void Start()
    {
        ManaSlider2D = GetComponent<Slider>();
        ManaSlider3D = GetComponent<Slider>();

        MaxMana = unitstat.Mana;
        ManaSlider2D.maxValue = MaxMana;
        ManaSlider3D.maxValue = MaxMana;
    }

    // Update is called once per frame
    void Update()
    {
        ManaSlider2D.value = unitstat.Mana;
        ManaSlider3D.value = unitstat.Mana;
    }
}