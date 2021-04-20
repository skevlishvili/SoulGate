    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class Health : MonoBehaviour
    {
        public Slider healthSlider3D;
        public Slider healthSlider2D;

        public float Maxhealth;

        public GameObject player;
        PlayerAnimator anim;
        public Unit unitstat;

        void Awake()
        {
            anim = GetComponentInParent<PlayerAnimator>();
            unitstat = GetComponentInParent<Unit>();
        }


        // Start is called before the first frame update
        void Start()
        {
            healthSlider2D = GetComponentInChildren<Slider>();
            healthSlider3D = GetComponentInChildren<Slider>();

            Maxhealth = unitstat.Health;
            healthSlider2D.maxValue = Maxhealth;
            healthSlider3D.maxValue = Maxhealth;
        }

        // Update is called once per frame
        void Update()
        {
            healthSlider2D.value = unitstat.Health;
            healthSlider3D.value = unitstat.Health;
        }
    }