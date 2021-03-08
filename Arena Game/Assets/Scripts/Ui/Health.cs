using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{

    public Slider healthSlider3D;
    Slider healthSlider2D;

    public float MaxHealth;
    public float health; 

    // Start is called before the first frame update
    void Start()
    {
        healthSlider2D = GetComponent<Slider>();

        healthSlider2D.maxValue = MaxHealth;// maqximaluri sicocxlis shecvla sheudzlebelia, slideri unda iyos procentebze

        healthSlider3D.maxValue = MaxHealth;// sachiroa damatebiti texturi informacia sicocxlis sanaxavad

        //InvokeRepeating("ChangeMaxHealth", 10.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        healthSlider2D.value = health;
        healthSlider3D.value = healthSlider2D.value;
    }
}
