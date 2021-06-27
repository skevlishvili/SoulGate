using Mirror;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Text Healthtext;
    Slider HealthSlider3D;
    Slider HealthSlider2D;

    public GameObject PastHealthObj;
    Slider PastHealthSlider3D;
    Slider PastHealthSlider2D;

    public GameObject DamageVisual;

    [Header("References")]
    [SerializeField] private Unit unitstat = null;

    void Awake()
    {
        unitstat = GetComponentInParent<Unit>();
    }


    // Start is called before the first frame update
    void Start()
    {
        HealthSlider3D = GetComponentInChildren<Slider>();
        HealthSlider2D = GetComponentInChildren<Slider>();

        PastHealthSlider3D = PastHealthObj.GetComponentInChildren<Slider>();
        PastHealthSlider2D = PastHealthObj.GetComponentInChildren<Slider>();

        if (Healthtext != null)
            Healthtext.text = $"{(int)unitstat.Health}/{(int)unitstat.MaxHealth}";

        HealthSlider3D.maxValue = unitstat.MaxHealth;
        HealthSlider2D.maxValue = unitstat.MaxHealth;

        PastHealthSlider3D.maxValue = unitstat.MaxHealth;
        PastHealthSlider2D.maxValue = unitstat.MaxHealth;
    }

    //// Update is called once per frame
    void Update()
    {
        if (Healthtext != null)
            Healthtext.text = $"{(int)unitstat.Health}/{(int)unitstat.MaxHealth}";

        HealthSlider3D.maxValue = unitstat.MaxHealth;
        HealthSlider2D.maxValue = unitstat.MaxHealth;

        HealthSlider3D.value = unitstat.Health;
        HealthSlider2D.value = unitstat.Health;
    }

    public void PastHealthSliderValue(float Health)
    {
        PastHealthSlider3D.maxValue = unitstat.MaxHealth;
        PastHealthSlider2D.maxValue = unitstat.MaxHealth;
        PastHealthSlider3D.value = Health;
        PastHealthSlider2D.value = Health;

        DamageVisual.SetActive(true);
        Debug.Log("--------------------------------------It Works------------------------------------");
        StartCoroutine(PastHealthSliderSetAvtive());
    }

    IEnumerator PastHealthSliderSetAvtive()
    {
        yield return new WaitForSeconds(0.5f);
        PastHealthSlider3D.gameObject.SetActive(false);
        DamageVisual.SetActive(false);
    }
}