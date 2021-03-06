using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{

    public Slider healthSlider3D;
    Slider healthSlider2D;

    public int health; 

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
        healthSlider3D.value = healthSlider2D.value;
    }
}
