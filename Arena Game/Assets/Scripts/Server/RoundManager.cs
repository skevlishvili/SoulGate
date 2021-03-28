using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class RoundManager : MonoBehaviourPun
{
    PhotonView PV;
    Unit unitStat;
    public GameObject LocalPlayerGameObject;
    static public int Round { get; set; }


    private void Awake()
    {
        Round = 1;
        PV = gameObject.GetComponent<PhotonView>();
        unitStat = LocalPlayerGameObject.GetComponent<Unit>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // giving all players custom property IsAlive, this could be better in awake, idk
        var players = PhotonNetwork.CurrentRoom.Players;
        foreach (var player in players)
        {
            if (player.Value.IsLocal) {
                Hashtable hash = new Hashtable();
                hash.Add("IsAlive", true);
                PhotonNetwork.CurrentRoom.Players[player.Key].SetCustomProperties(hash);
            }
        }       
    }

    // Update is called once per frame
    void Update()
    {
        if(PV.IsMine)
        {
            var players = PhotonNetwork.CurrentRoom.Players;
            int alivePlayers = 0;
            foreach (var player in players)
            {
                if(player.Value.IsLocal && unitStat.Health == 0)
                {
                    Hashtable hash = new Hashtable();
                    hash.Add("IsAlive", false);
                    PhotonNetwork.CurrentRoom.Players[player.Key].SetCustomProperties(hash); // setting current players custom property is alive to false
                }

                // checking alive players
                if((bool)player.Value.CustomProperties["IsAlive"])
                    alivePlayers++;
            }

            // if 1 alive player is left, potential drawback, if everyone leaves round isn't over
            if(alivePlayers <= 1 && players.Count != 1)
            {
                Round++; // get to second round
                
                //and respawn players, probably death needs to be rewritten
            }
        }
    }
}
