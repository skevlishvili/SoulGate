
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Launcher : MonoBehaviour
{
    #region Private Serializable Fields

    [Tooltip("The Ui InputField to let the user into a new room")]
    [SerializeField]
    private InputField GameCodeField;

    [Tooltip("Nickname for users to distinguish each other ")]
    [SerializeField]
    private InputField NicknameFieldJoin;


    [Tooltip("Nickname for users to distinguish each other ")]
    [SerializeField]
    private InputField NicknameFieldCreate;


    [Tooltip("The maximum number of players per room. When a room is full, it can't be joined by new players, and so new room will be created")]
    [SerializeField]
    private byte maxPlayersPerRoom = 4;

    [Tooltip("The Ui InputField to let the user see room name")]
    [SerializeField]
    private InputField RoomNameField;
    private string RoomName;

    //wasashlelia testirebis mere
    [Tooltip("The Ui Panel to let the user enter name, connect and play")]
    [SerializeField]
    private GameObject controlPanel;

    [Tooltip("The Ui Panel to let the user enter name, connect and play")]
    [SerializeField]
    private GameObject RoomcontrolPanel;

    //-----------------------------------------

    [Tooltip("The UI Label to inform the user that the connection is in progress")]
    [SerializeField]
    private MainMenu Menu;
    #endregion


    #region Private Fields

    bool IsConnecting;
    #endregion


    #region MonoBehaviour CallBacks
    void Awake()
    {
        //Menu.OpenMenu("Main_Panel");
    }
    #endregion

    #region Public Methods

    public void JoinRoom()
    {
        if (string.IsNullOrEmpty(NicknameFieldJoin.text))
            return;
    }

    public void CloseGame()
    {
        Application.Quit();
    }

    public void CreateRoom()
    {
        if (string.IsNullOrEmpty(RoomNameField.text) || string.IsNullOrEmpty(RoomName) || string.IsNullOrEmpty(NicknameFieldCreate.text))
        {
            return;
        }
    }
    #endregion 
}