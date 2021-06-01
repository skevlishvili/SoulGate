using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RoundManager : NetworkBehaviour
{
    public enum RoundState
    {
        PreRound = 1,
        RoundStart = 2,
        RoundEnd = 3
    }

    //public GameObject LocalPlayerGameObject;
    //public PlayerAction LocalPlayerAction;
    public UnityEngine.UI.Text RoundText;
    public int CurrentRound = 1;
    public int MaxRounds = 20;

    public PlayerManager Manager;
    public ReadyScript Ready;

    private RoundState _currentState;

    public RoundState CurrentState
    {
        get { return _currentState; }
        set
        {
            var prevVal = _currentState;
            _currentState = value;


            if (_currentState != prevVal)
            {
                switch (value)
                {
                    //case RoundState.PreRound:
                    //    break;
                    //case RoundState.RoundStart:
                    //    CurrentRound = RoundCounter;
                    //    break;
                    //case RoundState.RoundEnd:
                    //    RoundCounter = CurrentRound + 1;
                    //    break;
                    default:
                        _currentState = value;
                        break;
                }
            }
        }
    }


    private void OnServerInitialized()
    {
        CurrentState = RoundState.RoundStart;
        ChangeRountCounter(1);
    }


    private void Start()
    {
        CurrentState = RoundState.PreRound;
        RoundText.text = $"Round 1";
    }


    // Update is called once per frame
    [Server]
    void Update()
    {
        switch (CurrentState)
        {
            case RoundState.PreRound:
                PreRoundCheck();
                break;
            case RoundState.RoundStart:
                RoundCheck();
                break;
            case RoundState.RoundEnd:
                PostRoundCheck();
                break;
            default:
                break;
        }
    }

    [Server]
    void PreRoundCheck() {

        if (Manager.Players.Count > 1)
        {
            var readyPlayers = Manager.Players.Count;
            foreach (var player in Manager.Players)
            {
                var unit = player.GetComponent<Unit>();


                if (!unit.IsReady)
                {
                    readyPlayers--;
                }
            }

            if (readyPlayers == Manager.Players.Count)
            {
                StartRound();
            }
        }
    }

    [Server]
    private void StartRound()
    {
        CurrentState = RoundState.RoundStart;
        HideReadyButton();
    }


    [Server]
    void RoundCheck() {
        if (Manager.Players.Count > 1)
        {
            var alivePlayers = Manager.Players.Count;
            foreach (var player in Manager.Players)
            {
                var unit = player.GetComponent<Unit>();


                if (unit.IsDead)
                {
                    alivePlayers--;
                }
            }

            if (alivePlayers == 1)
            {
                EndRound();
            }
        }
    }

    [Server]
    private void EndRound()
    {
        CurrentState = RoundState.RoundEnd;
    }

    [Server]
    void PostRoundCheck() {

        if (CurrentRound < MaxRounds) {
            StartPreRound();

            return;
        } 
    }

    [Server]
    private void StartPreRound()
    {
        CurrentState = RoundState.PreRound;
        CurrentRound++;
        ResetPlayers();
        ShowReadyButton();
        ChangeRountCounter(CurrentRound);
    }

    [Server]
    private void ResetPlayers()
    {
        for (int i = 0; i < Manager.Players.Count; i++)
        {
            var unit = Manager.Players[i].GetComponent<Unit>();

            unit.Revive();
        }
    }

    [ClientRpc]
    private void ShowReadyButton() {
        Ready.Show();
    }

    [ClientRpc]
    private void HideReadyButton()
    {
           Ready.Hide();
    }


    #region RPC calls
    [ClientRpc]
    void ChangeRountCounter(int currentRound) {
        CurrentRound = currentRound;

        RoundText.text = $"Round {CurrentRound}";
    }
    #endregion
}
