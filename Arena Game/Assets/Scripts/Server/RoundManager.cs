using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RoundManager : NetworkBehaviour
{
    public enum RoundState
    {
        PreRound = 1,
        RoundStarting = 2,
        RoundStart = 3,
        RoundEnding = 4,
        RoundEnd = 5,
    }

    //public GameObject LocalPlayerGameObject;
    //public PlayerAction LocalPlayerAction;
    public GameObject RoundEndUi;
    public UnityEngine.UI.Text RoundTextObj;
    [SyncVar]
    public string RoundText;
    public int CurrentRound = 1;
    public int MaxRounds = 20;

    public Players Manager;
    public ReadyScript Ready;

    private int Timer = 0;

    [SyncVar]
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
        ChangeRoundCounter(1);
    }


    private void Start()
    {
        CurrentState = RoundState.PreRound;
        RoundText = $"Ready?";
    }


    // Update is called once per frame
    
    void Update()
    {

        if (isServer)
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

        if (isClient) {
            RoundTextObj.text = RoundText;
        }
    }

    [Server]
    void PreRoundCheck() {

        if (Manager.PlayersGameObjects.Count >= 1)
        {
            var readyPlayers = Manager.PlayersGameObjects.Count;
            foreach (var player in Manager.PlayersGameObjects)
            {
                var unit = player.GetComponent<Unit>();


                if (!unit.IsReady)
                {
                    readyPlayers--;
                }
            }

            if (readyPlayers == Manager.PlayersGameObjects.Count)
            {
                StartCoroutine(StartRoundTimer());
            }
        }
    }

    [Server]
    private void StartRound()
    {
        RoundText = $"Round\n{ToRoman(CurrentRound)}";
        CurrentState = RoundState.RoundStart;

        Skull_Shader_Script.Skull_Texture_Diffusion(true);

        HideReadyButton();
    }

    [Server]
    IEnumerator StartRoundTimer() {
        CurrentState = RoundState.RoundStarting;

        for (int i = 5; i >= 0; i--)
        {
            RoundText = i.ToString();
            //ChangeRoundText(RoundText);
            yield return new WaitForSeconds(1.0f);
        }

        StartRound();
    }


    [Server]
    void RoundCheck() {
        if (Manager.PlayersGameObjects.Count > 1)
        {
            var alivePlayers = Manager.PlayersGameObjects.Count;
            foreach (var player in Manager.PlayersGameObjects)
            {
                var unit = player.GetComponent<Unit>();


                if (unit.IsDead)
                {
                    alivePlayers--;
                }
            }

            if (alivePlayers == 1)
            {
                StartCoroutine(EndRoundTimer());
            }
        }
    }

    [Server]
    IEnumerator EndRoundTimer()
    {
        CurrentState = RoundState.RoundEnding;

        for (int i = 5; i >= 0; i--)
        {
            RoundText = i.ToString();
            //ChangeRoundText(RoundText);
            yield return new WaitForSeconds(1.0f);
        }

        EndRound();
    }

    [Server]
    private void EndRound()
    {
        CurrentState = RoundState.RoundEnd;
    }

    [Server]
    void PostRoundCheck() {
        if (CurrentRound == MaxRounds)
            EndRoundUi();

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
        ChangeRoundCounter(CurrentRound);
        RoundText = $"Ready?";
        ChangeRoundText("Ready?");
    }

    [Server]
    private void ResetPlayers()
    {
        for (int i = 0; i < Manager.PlayersGameObjects.Count; i++)
        {
            var unit = Manager.PlayersGameObjects[i].GetComponent<Unit>();

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
    void ChangeRoundCounter(int currentRound) {
        CurrentRound = currentRound;
        RoundText = $"Round\n{ToRoman(CurrentRound)}";
    }

    [ClientRpc]
    void ChangeRoundText(string text)
    {
        RoundText = text;
        RoundTextObj.text = RoundText;
    }

    [ClientRpc]
    void EndRoundUi()
    {
        RoundEndUi.SetActive(true);
    }
    #endregion


    public string ToRoman(int number)
    {
        if ((number < 0) || (number > 3999)) throw new ArgumentOutOfRangeException("insert value betwheen 1 and 3999");
        if (number < 1) return string.Empty;
        if (number >= 1000) return "M" + ToRoman(number - 1000);
        if (number >= 900) return "CM" + ToRoman(number - 900);
        if (number >= 500) return "D" + ToRoman(number - 500);
        if (number >= 400) return "CD" + ToRoman(number - 400);
        if (number >= 100) return "C" + ToRoman(number - 100);
        if (number >= 90) return "XC" + ToRoman(number - 90);
        if (number >= 50) return "L" + ToRoman(number - 50);
        if (number >= 40) return "XL" + ToRoman(number - 40);
        if (number >= 10) return "X" + ToRoman(number - 10);
        if (number >= 9) return "IX" + ToRoman(number - 9);
        if (number >= 5) return "V" + ToRoman(number - 5);
        if (number >= 4) return "IV" + ToRoman(number - 4);
        if (number >= 1) return "I" + ToRoman(number - 1);
        throw new ArgumentOutOfRangeException("something bad happened");
    }
}
