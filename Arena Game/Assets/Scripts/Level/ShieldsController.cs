using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShieldsController : NetworkBehaviour
{
    public GameObject[] Crystals;
    public Arena_Shield[] Shields;
    [SyncVar]
    public float ShieldDuration;

    [SyncVar]
    List<int> _avaliableShields;
    [SyncVar]
    List<int> _activeShields;

    [SyncVar]
    int _activeCrystal;
    [SyncVar]
    bool _isShieldActive;


    void Start()
    {
        ShieldDuration = 30;

        foreach (var item in Shields)
        {
            item.Render.material.SetFloat("DiffuseTransition", 0);
        }
        _avaliableShields = new List<int>();
        _activeShields = new List<int>();

        //needs to move where Round starts
        _isShieldActive = false;//Set true After you move your code where round starts

        //CheckAvaliableShields();
        //StartCoroutine(ShieldsActivation(16));
        //StartCoroutine(ShieldsDeactivation());
    }



    [Server]
    void FixedUpdate()
    {
        if (!_isShieldActive)
        {
            CheckAvaliableShields();
            CheckAvaliableCrystals();
            StartCoroutine(ShieldsActivation(_activeCrystal+2));
            _isShieldActive = true;
            StartCoroutine(ShieldsDeactivation());
        }

        CheckActiveShields();
    }

    [Server]
    public void CheckActiveShields()
    {
        for (int i = 0; i < _activeShields.Count; i++)
        {
            var item = Shields[_activeShields[i]];
            if (!item.Towers[0].activeInHierarchy || !item.Towers[1].activeInHierarchy)
            {
                item.Shield.SetActive(false);
                item.DiffuseTransitionOn = false;

                DeActivateShieldRpc(_activeShields[i]);
            }
        }
    }

    [ClientRpc]
    private void DeActivateShieldRpc(int index)
    {     
        var item = Shields[index];
        item.Shield.SetActive(false);
        item.DiffuseTransitionOn = false;        
    }

   

    [ClientRpc]
    private void ActivateShieldRpc(int index)
    {
        var item = Shields[index];
        item.Shield.SetActive(true);
        item.Shield_Duration = ShieldDuration;
        item.ShieldActivatinTime = DateTime.Now;

        item.DiffuseTransitionOn = true;
        item.TextureDiffusion = 0;
        item.TextureDiffusionMaximum = 1f;
        item.TextureDiffusionmMinimum = 0.15f;
    }

    [Server]
    public void CheckAvaliableShields()
    {
        _avaliableShields.Clear();

        for (int i = 0; i < Shields.Length; i++)
        {
            if (Shields[i].Towers[0].activeInHierarchy && Shields[i].Towers[1].activeInHierarchy)
            {
                _avaliableShields.Add(i);
            }
        }
    }

    [Server]
    public void CheckAvaliableCrystals()
    {
        _activeCrystal = 0;

        foreach (var item in Crystals)
        {
            if (item.activeInHierarchy)
            {
                _activeCrystal += 1;
            }
        }
    }

    [Server]
    public IEnumerator ShieldsActivation(int Quantity)
    {       
        _activeShields = _avaliableShields.AsEnumerable().OrderBy(n => Guid.NewGuid()).Take(Mathf.Min(Quantity, _avaliableShields.Count)).ToList();


        for (int i = 0; i < _activeShields.Count; i++)
        {
            var item = Shields[_activeShields[i]];
            item.Shield.SetActive(true);
            item.Shield_Duration = ShieldDuration;
            item.ShieldActivatinTime = DateTime.Now;

            item.DiffuseTransitionOn = true;
            item.TextureDiffusion = 0;
            item.TextureDiffusionMaximum = 1f;
            item.TextureDiffusionmMinimum = 0.15f;

            ActivateShieldRpc(_activeShields[i]);
        }

        yield return new WaitForSeconds(ShieldDuration);
    }


   

    [Server]
    public IEnumerator ShieldsDeactivation()
    {
        yield return new WaitForSeconds(ShieldDuration);

        for (int i = 0; i < _activeShields.Count; i++)
        {
            var item = Shields[_activeShields[i]];

            item.Shield.SetActive(false);

            item.DiffuseTransitionOn = false;
            DeActivateShieldRpc(_activeShields[i]);
        }

        _isShieldActive = false;
    }
}
