using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class Launcher : MonoBehaviourPunCallbacks
{
    #region Private Serializable Fields
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


    [Tooltip("The UI Label to inform the user that the connection is in progress")]
    [SerializeField]
    private GameObject progressLabel;

    //-----------------------------------------


    [Tooltip("The UI Label to inform the user that the connection is in progress")]
    [SerializeField]
    private MainMenu Menu;


    private bool IsInMainMenu = true;
    private bool IsInRoomMenu = false;
    #endregion


    #region Private Fields
    /// <summary>
    /// This client's version number. Users are separated from each other by gameVersion (which allows you to make breaking changes).
    /// </summary>
    string gameVersion = "1";
    bool IsConnecting;
    #endregion


    #region MonoBehaviour CallBacks


    /// <summary>
    /// MonoBehaviour method called on GameObject by Unity during early initialization phase.
    /// </summary>
    void Awake()
    {
        // #Critical
        // this makes sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room sync their level automatically
        PhotonNetwork.AutomaticallySyncScene = true;

        Menu.OpenMenu("Control Panel");

        RoomName = HelpMethods.RandomString(4);
        RoomNameField.text = RoomName;
        //MenuButtons(0);
    }

    #endregion


    #region Public Methods
    //public void MenuButtons(int value)
    //{
    //    if (value == 0)
    //    {
    //        progressLabel.SetActive(false);
    //        controlPanel.SetActive(true);
    //        RoomcontrolPanel.SetActive(false);
    //    }
    //    else if (value == 1)
    //    {
    //        progressLabel.SetActive(false);
    //        controlPanel.SetActive(false);
    //        RoomcontrolPanel.SetActive(true);
    //    }
    //    else if (value == 2)
    //    {
    //        progressLabel.SetActive(true);
    //        controlPanel.SetActive(false);
    //        RoomcontrolPanel.SetActive(false);
    //    }
    //}

    /// <summary>
    /// Start the connection process.
    /// - If already connected, we attempt joining a random room
    /// - if not yet connected, Connect this application instance to Photon Cloud Network
    /// </summary>
    public void Connect()
    {
        //MenuButtons(2);

        // we check if we are connected or not, we join if we are , else we initiate the connection to the server.
        if (PhotonNetwork.IsConnected)
        {
            // #Critical we need at this point to attempt joining a Random Room. If it fails, we'll get notified in OnJoinRandomFailed() and we'll create one.
            PhotonNetwork.JoinRandomRoom();

        }
        else
        {
            // #Critical, we must first and foremost connect to Photon Online Server.
            // keep track of the will to join a room, because when we come back from the game we will get a callback that we are connected, so we need to know what to do then
            IsConnecting = PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = gameVersion;
        }
    }

    public void CloseGame()
    {
        
    }

    public void CreateRoom()
    {
        if (string.IsNullOrEmpty(RoomNameField.text) || string.IsNullOrEmpty(RoomName))
        {
            return;
        }

        PhotonNetwork.CreateRoom(RoomName, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
        Menu.OpenMenu("Connecting...");
    }


    #endregion

    #region MonoBehaviourPunCallbacks Callbacks
    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster()");

        // we don't want to do anything if we are not attempting to join a room.
        // this case where isConnecting is false is typically when you lost or quit the game, when this level is loaded, OnConnectedToMaster will be called, in that case
        // we don't want to do anything.
        if (IsConnecting)
        {
            // #Critical: The first we try to do is to join a potential existing room. If there is, good, else, we'll be called back with OnJoinRandomFailed()
            PhotonNetwork.JoinRandomRoom();
            IsConnecting = false;
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        progressLabel.SetActive(false);
        controlPanel.SetActive(false);
        RoomcontrolPanel.SetActive(true);

        IsConnecting = false;
        Debug.LogWarningFormat("OnDisconnected() was called with reason {0}", cause);
    }

    public override void OnJoinedLobby()
    {
        Menu.OpenMenu("Create Room Panel");

        base.OnJoinedLobby();
        Debug.Log("Joined Lobby");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("OnJoinRandomFailed() was called. No random room available, so we create one.\nReturning: Main Menu");

        // #Critical: we failed to join a random room, maybe none exists or they are all full. No worries, we create a new room.
        Menu.OpenMenu("Control Panel");
        //PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom() called, Now this client is in a room.");

        // #Critical: We only load if we are the first player, else we rely on `PhotonNetwork.AutomaticallySyncScene` to sync our instance scene.
        //if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        //{
        //    Debug.Log("We load the 'Room for 1' ");


        //    // #Critical
        //    // Load the Room Level.
        //    PhotonNetwork.LoadLevel("SampleScene");
        //}

        PhotonNetwork.LoadLevel("SampleScene");
        Debug.Log(PhotonNetwork.CurrentRoom.Name);
    }
    #endregion
}
