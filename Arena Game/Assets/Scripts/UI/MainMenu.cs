using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public static MainMenu Instance;

    #region Private Serializable Fields
    [Tooltip("The Ui Panel to let the user enter name, connect and play")]
    [SerializeField]
    private GameObject Menu;

    [SerializeField] Menu[] menus;

    #endregion

    #region Public 

    public Text Nickname;

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

    public void OpenMenuButton(string Menuname)
    {
        OpenMenu(Menuname);
    }

    public void OpenMenu(Menu menu)
    {
        menu.Open();
    }


    public void StartGame() {
        if (Nickname != null && Nickname.text != "") {
            PlayerNickname.LocalNickname = Nickname.text;

            SceneManager.LoadScene("Map_1");
        }
    }


    public void CloseMenu(Menu menu)
    {
        menu.Close();
    }

    #endregion
}