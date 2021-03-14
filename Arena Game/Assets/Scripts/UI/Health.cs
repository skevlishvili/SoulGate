using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class Health : MonoBehaviour
{

    PhotonView PV;

    public Slider healthSlider3D;
    Slider healthSlider2D;

    public float health;

    public GameObject player;


    void Awake()
    {
        PV = GetComponentInParent<PhotonView>();    
    }


    // Start is called before the first frame update
    void Start()
    {
        healthSlider2D = GetComponent<Slider>();

        healthSlider2D.maxValue = health;

        healthSlider3D.maxValue = health;
    }

    // Update is called once per frame
    void Update()
    {
        healthSlider2D.value = health;
        healthSlider3D.value = health;



        if(health <= 0)
        {
            Destroy(player);
        }
    }
}
