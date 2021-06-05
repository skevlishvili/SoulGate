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
    }

    [Server]
    public void IncrementDeath()
    {
        deaths++;
    }

    [Server]
    public void AddScore(int addedScore)
    {
        score += addedScore;
    }
}
