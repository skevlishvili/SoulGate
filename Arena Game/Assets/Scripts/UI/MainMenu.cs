using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class MainMenu : MonoBehaviourPunCallbacks
{

    public static MainMenu Instance;

    #region Private Serializable Fields
    [Tooltip("The Ui Panel to let the user enter name, connect and play")]
    [SerializeField]
    private GameObject Menu;

    [HideInInspector]
    public int Counter = 1;

    [SerializeField] Menu[] menus;
    #endregion

    #region Public 

    void Awake()
    {
        Instance = this;
    }

    void start()
    {
        OpenMenuInGame(Counter);
    }


    public void OpenMenuInGame(int Counter)
    {
        if (Counter % 2 == 1)
        {
            Menu.SetActive(false);
        }
        else{
            Menu.SetActive(true);
        }
    }


    public void OpenMenu(string MenuName)
    {
        for(int i = 0; i < menus.Length; i++)
        {
            if (menus[i].open)
            {
                Debug.Log(i);
                CloseMenu(menus[i]);
            }

            else if (menus[i].MenuName == MenuName)
            {
                OpenMenu(menus[i]);
            }
        }
    }

    public void OpenMenu(Menu menu)
    {
        //for (int i = 0; i < menus.Length; i++)
        //{
        //    if (menus[i].open)
        //    {
        //        CloseMenu(menus[i]);
        //    }
        //}
        menu.Open();
    }


    public void CloseMenu(Menu menu)
    {
        menu.Close();
    }

    #endregion
}