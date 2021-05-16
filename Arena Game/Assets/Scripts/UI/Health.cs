    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class Health : MonoBehaviour
    {
        public Slider healthSlider3D;
        public Slider healthSlider2D;

        public Unit unitstat;

        void Awake()
        {
            unitstat = GetComponentInParent<Unit>();
        }


        // Start is called before the first frame update
        void Start()
        {
            healthSlider2D = GetComponentInChildren<Slider>();
            healthSlider3D = GetComponentInChildren<Slider>();

            healthSlider2D.maxValue = unitstat.MaxHealth;
            healthSlider3D.maxValue = unitstat.MaxHealth;
        }

        // Update is called once per frame
        void Update()
        {
            healthSlider2D.value = unitstat.Health;
            healthSlider3D.value = unitstat.Health;
        }
    }