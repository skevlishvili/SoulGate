using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : NetworkBehaviour
{

    public Unit unitStat;

    [SyncVar]
    private int kills = 0;
    [SyncVar]
    private int deaths = 0;
    [SyncVar]
    private int score = 0;


    public delegate void ScoreChangeDelegate(int score);
    public event ScoreChangeDelegate EventScoreChange;


    [Server]
    private void Start()
    {
        unitStat.EventPlayerDeath += OnPlayerDeath;
    }

    public int Kills {
        get {
            return kills;
        }
        private set {
            kills = value;
        }    
    }

    public int Deaths
    {
        get
        {
            return deaths;
        }
        private set
        {
            deaths = value;
        }
    }

    public int Score
    {
        get
        {
            return score;
        }
        private set
        {
            score = value;
        }
    }

    [Server]
    public void IncrementKill() {
        kills++;
        EventScoreChange?.Invoke(0);
        //ScoreChangedRpc();
    }

    [Server]
    public void IncrementDeath()
    {
        deaths++;
        EventScoreChange?.Invoke(0);
        //ScoreChangedRpc();
    }

    [Server]
    public void AddScore(int addedScore)
    {
        score += addedScore;
        EventScoreChange?.Invoke(addedScore);
        ScoreChangedRpc(addedScore);
    }

    [Server]
    public void OnPlayerDeath(GameObject current, GameObject killer) {
        var playerScore = killer.GetComponent<PlayerScore>();

        playerScore.AddScore(100);

        IncrementDeath();
        playerScore.IncrementKill();
    }

    [ClientRpc]
    private void ScoreChangedRpc(int addedScore)
    {
        EventScoreChange?.Invoke(addedScore);
    }
}
