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

    [SerializeField] Menu[] menus;
    #endregion

    #region Public 

    void Awake()
    {
        Instance = this;
    }

    public void OpenMenu(string MenuName)
    {
        for (int i = 0; i < menus.Length; i++)
        {
            CloseMenu(menus[i]);

            if (menus[i].MenuName == MenuName)
            {
                OpenMenu(menus[i]);
            }

        }
    }

    public void Play()
    {
        OpenMenu("Create_Room_Panel");
    }

    public void ReturnToMain()
    {
        OpenMenu("Control_Panel");
    }

    public void OpenMenu(Menu menu)
    {
        menu.Open();
    }


    public void CloseMenu(Menu menu)
    {
        menu.Close();
    }

    #endregion
}