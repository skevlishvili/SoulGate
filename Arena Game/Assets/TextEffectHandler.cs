using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextEffectHandler : MonoBehaviour
{
    [SerializeField]
    GameObject TextPrefab;

    [SerializeField]
    Unit unitStat;

    float damage;
    
    // Start is called before the first frame update
    void Start()
    {
        unitStat.EventPlayerDamage += AddDamage;
        unitStat.EventPlayerMoney += AddMoney;
        damage = 0;
        InvokeRepeating("CheckDamage", 1, 1);
    }

    private void AddMoney(float money)
    {
        ShowText(Mathf.CeilToInt(money).ToString(), new Color(255, 204, 0));
    }


    private void CheckDamage() {
        if (damage > 0) {
            ShowText(Mathf.CeilToInt(damage).ToString(), new Color(255, 0, 0));
            damage = 0;
        }
    }


    private void AddDamage(float damage)
    {
        this.damage += damage;        
    }

    private void ShowText(string text, Color color) {
        var textGameObject = Instantiate(TextPrefab, gameObject.transform.position, gameObject.transform.rotation);
        textGameObject.transform.parent = gameObject.transform;
        textGameObject.GetComponentInChildren<TextMesh>().text = text;
        textGameObject.GetComponentInChildren<TextMesh>().color = color;
    }
}
