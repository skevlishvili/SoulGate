
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class Launcher : MonoBehaviourPunCallbacks
{
    #region Private Serializable Fields

    [Tooltip("The Ui InputField to let the user into a new room")]
    [SerializeField]
    private InputField GameCodeField;


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
        Menu.OpenMenu("Control_Panel");
        // This is an issue needs rework!!!!!!!!
        RoomName = HelpMethods.RandomString(4);
        RoomNameField.text = RoomName;
    }

    void Start()
    {
        Debug.Log("connected to master");

        IsConnecting = PhotonNetwork.ConnectUsingSettings();
    }
    #endregion

    #region Public Methods

    public void JoinRoom()
    {
        Debug.Log(GameCodeField.text);
        PhotonNetwork.JoinRoom(GameCodeField.text);
    }

    public void CloseGame()
    {
        Application.Quit();
    }

    public void CreateRoom()
    {
        if (string.IsNullOrEmpty(RoomNameField.text) || string.IsNullOrEmpty(RoomName))
        {
            return;
        }

        PhotonNetwork.CreateRoom(RoomName, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
    }


    #endregion

    #region MonoBehaviourPunCallbacks Callbacks
    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster()");


        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;

    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Menu.OpenMenu("Create_Room_Panel");

        IsConnecting = false;
        Debug.LogWarningFormat("OnDisconnected() was called with reason {0}", cause);
    }

    public override void OnJoinedLobby()
    {
        //Menu.OpenMenu("Create Room Panel");

        base.OnJoinedLobby();
        Debug.Log("Joined Lobby");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("OnJoinRandomFailed() was called. No random room available, so we create one.\nReturning: Main Menu");

        Menu.OpenMenu("Control_Panel");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom() called, Now this client is in a room.");

        PhotonNetwork.LoadLevel(1);
        Debug.Log(PhotonNetwork.CurrentRoom.Name);
    }
    #endregion
}