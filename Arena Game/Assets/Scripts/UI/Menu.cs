﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public string MenuName;

    public bool open = true;

    public void Open()
    {
        open = true;
        gameObject.SetActive(true);
    }

    public void Close()
    {
        Debug.Log("helloo");
        open = false;
        gameObject.SetActive(false);
    }
}
