    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class Health : MonoBehaviour
    {
        Slider healthSlider3D;
        Slider healthSlider2D;

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

        void CurrentPlayerHealth(float health)
        {
            Debug.Log($"Unit Health --- {health}");
            healthSlider2D.value = health;
            healthSlider3D.value = health;

            if (health <= 0)
            {
                StartCoroutine(DestroyPlayer());
            }
        }

        IEnumerator DestroyPlayer()
        {
            anim.IsDead();
            unitstat.IsDead = true;
            yield return new WaitForSeconds(30);
            Destroy(player);
        }
    }