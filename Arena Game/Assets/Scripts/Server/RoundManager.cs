using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class RoundManager : MonoBehaviourPun
{
    public enum RoundState
    {
        PreRound = 1,
        RoundStart = 2,
        RoundEnd = 3
    }


    public PhotonView PV;
    public GameObject LocalPlayerGameObject;
    public PlayerAction LocalPlayerAction;
    public UnityEngine.UI.Text RoundText;
    static public int CurrentRound;
    static public int RoundCounter;


    private RoundState _currentState;

    public RoundState CurrentState
    {
        get { return _currentState; }
        set {
            var prevVal = _currentState;
            _currentState = value;


            if (_currentState != prevVal)
            {
                switch (value)
                {
                    case RoundState.PreRound:
                        break;
                    case RoundState.RoundStart:
                        CurrentRound = RoundCounter;
                        break;
                    case RoundState.RoundEnd:
                        RoundCounter = CurrentRound + 1;
                        break;
                    default:
                        _currentState = value;
                        break;
                }
            }
        }
    }


    public int AlivePlayers;
    public int ReadyPlayers;



    private void Awake()
    {
        PV = gameObject.GetComponent<PhotonView>();
        if (!PV.IsMine)
            return;

        LocalPlayerAction = LocalPlayerGameObject.GetComponent<PlayerAction>();
        RoundText = GetComponentInChildren<UnityEngine.UI.Text>();

        RoundCounter = 1;
        CurrentRound = 1;
        ReadyPlayers = 0;
        AlivePlayers = 0;
        CurrentState = RoundState.PreRound;
    }
    

    // Update is called once per frame
    void Update()
    {
        if (!PV.IsMine)
            return;


        //Debug.Log($"this is current state {CurrentState}");
        
        RoundText.text = $"Round #{RoundCounter}";
        //RoundText.text = PhotonNetwork.LocalPlayer.NickName;
        if (CurrentState == RoundState.PreRound) {

            int readyPlayers = 0;

            GameObject[] scenePlayers = GameObject.FindGameObjectsWithTag("Player");

            foreach (var scenePlayer in scenePlayers)
            {
                if (scenePlayer.GetComponent<PlayerAction>().IsReady)
                    readyPlayers++;
            }

            if (readyPlayers == PhotonNetwork.CurrentRoom.Players.Count)
            {
                CurrentState = RoundState.RoundStart;
            }
            return;
        }


        if (CurrentState == RoundState.RoundStart)
        {
            GameObject[] scenePlayers = GameObject.FindGameObjectsWithTag("Player");

            int aliveplayers = 0;
            foreach (var scenePlayer in scenePlayers)
            {
                if (!scenePlayer.GetComponent<PlayerAction>().IsDead)
                    aliveplayers++;
            }

            if (aliveplayers <= 1 && PhotonNetwork.CurrentRoom.Players.Count != 1)
            {
                CurrentState = RoundState.RoundEnd;
            }

            return;
        }


        if (CurrentState == RoundState.RoundEnd)
        {
            PV.RPC("Respawn", RpcTarget.All);

            return;
        }
    }



    void RoundStart()
    {
        if (!PV.IsMine)
            return;
     
        StartCoroutine(RespawnAll());        
    }

    IEnumerator RespawnAll() {
        yield return new WaitForSeconds(20f);
        PV.RPC("ChangeAlivePlayers", RpcTarget.AllBuffered, PhotonNetwork.CurrentRoom.Players.Count);
        PV.RPC("ChangeReadyPlayers", RpcTarget.AllBuffered, 0);
        PV.RPC("Respawn", RpcTarget.AllBuffered);
    }

    void RoundEnd() {
        if (!PV.IsMine)
            return;

        RoundStart();
    }


    void PlayerDeath() {
        if (!PV.IsMine)
            return;
    }


    #region RPC calls
    [PunRPC]
    void ChangeReadyPlayers(int readyPlayers)
    {
        if (!PV.IsMine)
            return;

        ReadyPlayers = readyPlayers;
    }


    [PunRPC]
    void ChangeAlivePlayers(int alivePlayers)
    {
        if (!PV.IsMine)
            return;

        AlivePlayers = alivePlayers;
    }


    [PunRPC]
    void Respawn()
    {
        //if (!PV.IsMine)
        //    return;

        CurrentState = RoundState.PreRound;
        LocalPlayerAction.RespawnAll();
    }
    #endregion
}
