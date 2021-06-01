using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class HideDestroyedLevelGameObject : MonoBehaviour
{
    public GameObject[] DesObjArray;
    public Material Crystal_material;
    public Material Pillar_material;
    public float TimeBeforeDestroy = 30;
    float TIncrement = -2;
    public bool updateOn = true;

    void Start()
    {
        Crystal_material.SetFloat("Dissolve_Fac", -2);
        Pillar_material.SetFloat("Dissolve_Fac", -2);

        foreach (var item in DesObjArray)
        {
            Destroy(item, 30);
        }

        //StartCoroutine(updateOff());
    }

    void Update()
    {
        if (updateOn == true)
        {
            Crystal_material.SetFloat("Dissolve_Fac", HelpMethods.MathLerpFunc(-2, 10, TIncrement, 10));
            Pillar_material.SetFloat("Dissolve_Fac", HelpMethods.MathLerpFunc(-2, 10, TIncrement, 10));
            TIncrement += 0.1f * Time.deltaTime;
        }
    }

    //IEnumerator updateOff()
    //{
    //    yield return new WaitForSeconds(TimeBeforeDestroy);
    //    updateOn = false;
    //}
}
