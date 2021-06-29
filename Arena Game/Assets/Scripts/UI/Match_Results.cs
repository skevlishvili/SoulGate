using Mirror;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Match_Results : MonoBehaviour
{
    struct PlayerInScoreBoard
    {
        public GameObject player;
        public PlayerNickname playerNickname;
        public PlayerScore playerScore;
        public int playerDamage;
    }


    public Players Manager;
    List<PlayerInScoreBoard> SortedPlayers = new List<PlayerInScoreBoard>();

    Text[][] Player_Text_Fields;
    public Text[] Player_1_TextField;
    public Text[] Player_2_TextField;
    public Text[] Player_3_TextField;
    public Text[] Player_4_TextField;

    public GameObject[] Players_Visual_Stroke;

    private void Awake()// weird problem: Something activcates gameobject after scene loading
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        Player_Text_Fields = new Text[][] { Player_1_TextField, Player_2_TextField, Player_3_TextField, Player_4_TextField };

        for (int i = 0; i < 4; i++)
        {
            if (i != 0)
                Players_Visual_Stroke[i - 1].SetActive(false);

            for (int j = 0; j < 3; j++)
            {
                Player_Text_Fields[i][j].text = "";
            }
        }

        FillRatingUiWithData();
    }

    void SortPlayersByScore()
    {
        for (int i = 0; i < Manager.PlayersGameObjects.Count; i++)
        {
            System.Random r = new System.Random();

            PlayerInScoreBoard obj = new PlayerInScoreBoard();
            obj.player = Manager.PlayersGameObjects[i];
            obj.playerNickname = Manager.PlayersGameObjects[i].GetComponent<PlayerNickname>();
            obj.playerScore = Manager.PlayersGameObjects[i].GetComponent<PlayerScore>();
            obj.playerDamage = obj.playerScore.Score*r.Next(1,4);
            SortedPlayers.Add(obj);
        }

        SortedPlayers = SortedPlayers.OrderBy(x => x.playerScore.Score).Reverse().ToList();
    }

    public void FillRatingUiWithData()
    {
        SortPlayersByScore();
        for (int i = 0; i < SortedPlayers.Count; i++)
        {
            if (i !=0 )
                Players_Visual_Stroke[i-1].SetActive(true);

            Player_Text_Fields[i][0].text = SortedPlayers[i].playerNickname.Nickname;
            Player_Text_Fields[i][1].text = SortedPlayers[i].playerScore.Score.ToString();
            Player_Text_Fields[i][2].text = SortedPlayers[i].playerDamage.ToString();
        }
    }


    public void ReturnLauncher()
    {
        GameObject.FindGameObjectsWithTag("PlayerManager")[0].GetComponent<Players>().RemovePlayerCmd(ClientScene.localPlayer.gameObject);
        NetworkManager.singleton.StopClient(); 
        Destroy(GameObject.FindGameObjectsWithTag("NetworkManager")[0]);
        SceneManager.LoadScene("Launcher");
    }
}
