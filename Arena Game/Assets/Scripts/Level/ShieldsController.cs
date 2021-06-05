using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShieldsController : MonoBehaviour
{
    public GameObject[] Crystals;

    public Arena_Shield[] Shields;
    public List<Arena_Shield> Avaliable_Shields;
    public List<Arena_Shield> Active_Shields;

    public float Shield_Duration;
    int ActiveCrystal;
    bool IsShieldActive;


    // Start is called before the first frame update
    void Start()
    {
        Shield_Duration = 30;

        foreach (var item in Shields)
        {
            item.Render.material.SetFloat("DiffuseTransition", 0);
        }


        //needs to move where Round starts
        IsShieldActive = false;//Set true After you move your code where round starts

        //CheckAvaliableShields();
        //StartCoroutine(ShieldsActivation(16));
        //StartCoroutine(ShieldsDeactivation());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!IsShieldActive)
        {
            CheckAvaliableShields();
            CheckAvaliableCrystals();
            StartCoroutine(ShieldsActivation(ActiveCrystal+2));
            IsShieldActive = true;
            StartCoroutine(ShieldsDeactivation());
        }

        CheckActiveShields();
    }

    public void CheckActiveShields()
    {
        foreach (var item in Active_Shields)
        {
            if (!item.Towers[0].activeInHierarchy || !item.Towers[1].activeInHierarchy)
            {
                item.Shield.SetActive(false);
                item.DiffuseTransitionOn = false;
            }
        }
    }

    public void CheckAvaliableShields()
    {
        Avaliable_Shields.Clear();

        foreach (var item in Shields)
        {
            if (item.Towers[0].activeInHierarchy && item.Towers[1].activeInHierarchy)
            {
                Avaliable_Shields.Add(item);
            }
        }
    }

    public void CheckAvaliableCrystals()
    {
        ActiveCrystal = 0;

        foreach (var item in Crystals)
        {
            if (item.activeInHierarchy)
            {
                ActiveCrystal += 1;
            }
        }
    }

    IEnumerator ShieldsActivation(int Quantity)
    {

        if (Quantity <= Avaliable_Shields.Count)
        {
            Active_Shields = Avaliable_Shields.AsEnumerable().OrderBy(n => Guid.NewGuid()).Take(Quantity).ToList();
        }
        else
        {
            Active_Shields = Avaliable_Shields.AsEnumerable().OrderBy(n => Guid.NewGuid()).Take(Avaliable_Shields.Count).ToList();
        }

        foreach (var item in Active_Shields)
        {
            item.Shield.SetActive(true);
            item.Shield_Duration = Shield_Duration;
            item.ShieldActivatinTime = DateTime.Now;

            item.DiffuseTransitionOn = true;
            item.TextureDiffusion = 0;
            item.TextureDiffusionMaximum = 1f;
            item.TextureDiffusionmMinimum = 0.15f;
        }

        yield return new WaitForSeconds(Shield_Duration);
    }

    IEnumerator ShieldsDeactivation()
    {
        yield return new WaitForSeconds(Shield_Duration);

        foreach (var item in Active_Shields)
        {
            item.Shield.SetActive(false);
            item.DiffuseTransitionOn = false;
        }

        IsShieldActive = false;
    }
}
