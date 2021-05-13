using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBoardScript : MonoBehaviour
{
    public UnityEngine.UI.Text ScoreBoardText;

    // Start is called before the first frame update
    void Start()
    {
        ScoreBoardText = GetComponentInChildren<UnityEngine.UI.Text>();   
    }

    // Update is called once per frame
    void Update()
    {
        ScoreBoardText.text = "";
        //foreach (var player in PhotonNetwork.PlayerList)
        //{
        //    ScoreBoardText.text += $"{player.NickName} --- {ScoreExtensions.GetScore(player)}\n\n";
        //}
    }
}
