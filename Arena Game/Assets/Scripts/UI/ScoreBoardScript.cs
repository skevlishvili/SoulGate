using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoardScript : NetworkBehaviour
{
    public CanvasGroup Canvas;
    public Players Manager;
    public CanvasGroup[] PlayerPanels;
    public Text[] Nicknames;
    public Text[] Scores;
    public Text[] KD;

    [SerializeField]
    private RoundManager roundManager;
    private int currentPlayers = 0;

    // Start is called before the first frame update
    void Start()
    {
        //Manager.EventPlayerAdd += PlayerAdded;

    }

    // Update is called once per frame
    void Update()
    {
        if (Manager == null || Manager?.PlayersGameObjects == null)
            return;

        if (Input.GetKey(KeyCode.Tab) || roundManager.CurrentState == RoundManager.RoundState.RoundEnding)
        {
            Canvas.alpha = 1;
        }
        else {
            Canvas.alpha = 0;
        }

        if (Manager.PlayersGameObjects.Count > currentPlayers) {
            PlayerAdded(Manager.PlayersGameObjects[currentPlayers]);
            currentPlayers++;
        }

        for (int i = 0; i < Manager.PlayersGameObjects.Count; i++)
        {
            if (Nicknames[i].text == "") {
                Nicknames[i].text = Manager.PlayersGameObjects[i].GetComponent<PlayerNickname>().Nickname;
            }
        }
    }



    void PlayerAdded(GameObject player) {
        for (int i = 0; i < Manager.PlayersGameObjects.Count; i++)
        {
            PlayerPanels[i].alpha = 1;
            Nicknames[i].text = Manager.PlayersGameObjects[i].GetComponent<PlayerNickname>().Nickname;
            Manager.PlayersGameObjects[i].GetComponent<PlayerScore>().EventScoreChange += ScoreChanged;
        }
    }


    [Server]
    void ScoreChanged(int addedScore)
    {
        for (int i = 0; i < Manager.PlayersGameObjects.Count; i++)
        {
            string score = Manager.PlayersGameObjects[i].GetComponent<PlayerScore>().Score.ToString();
            string kd = Manager.PlayersGameObjects[i].GetComponent<PlayerScore>().Kills.ToString() + " / " + Manager.PlayersGameObjects[i].GetComponent<PlayerScore>().Deaths.ToString();
            ScoreChangedRpc(score, kd, i);
        }
    }


    [ClientRpc]
    void ScoreChangedRpc(string score, string kd, int index)
    {        
            Scores[index].text = score;
            KD[index].text = kd;
    }
}
