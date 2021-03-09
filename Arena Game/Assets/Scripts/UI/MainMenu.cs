using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class MainMenu : MonoBehaviourPunCallbacks
{
    #region Private Serializable Fields
    [Tooltip("The Ui Panel to let the user enter name, connect and play")]
    [SerializeField]
    private GameObject Menu;

    [HideInInspector]
    public int Counter = 1;
    #endregion

    #region Public 
    void start()
    {
        OpenMenu(Counter);
    }


    public void OpenMenu(int Counter)
    {
        if (Counter % 2 == 1)
        {
            Menu.SetActive(false);
        }
        else{
            Menu.SetActive(true);
        }
    }

    #endregion
}