using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class Health : MonoBehaviour
{
    PhotonView PV;

    Slider healthSlider3D;
    Slider healthSlider2D;

    public float Maxhealth;

    public GameObject player;
    PlayerAnimator anim;
    public Unit unitstat;

    void Awake()
    {
        PV = GetComponent<PhotonView>();

        anim = GetComponentInParent<PlayerAnimator>();
        unitstat = GetComponentInParent<Unit>();
    }


    // Start is called before the first frame update
    void Start()
    {
        healthSlider2D = GetComponent<Slider>();
        healthSlider3D = GetComponent<Slider>();


        Maxhealth = unitstat.Health;
        healthSlider2D.maxValue = Maxhealth;
        healthSlider3D.maxValue = Maxhealth;
    }

    // Update is called once per frame
    void Update()
    {
        CurrentPlayerHealth(unitstat.Health);
    }

    void CurrentPlayerHealth(float Health)
    {
        healthSlider2D.value = Health;
        healthSlider3D.value = Health;

        if (Health <= 0)
        {
            StartCoroutine(DestroyPlayer());
        }
    }

    IEnumerator DestroyPlayer()
    {
        anim.IsDead();
        yield return new WaitForSeconds(30);
        Destroy(player);
    }
}
