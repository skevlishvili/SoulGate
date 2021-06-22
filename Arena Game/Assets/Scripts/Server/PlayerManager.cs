using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : NetworkManager
{
    public Players players;

    private int _maxPlayers = 4;

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        if (players.PlayersGameObjects.Count + 1 > _maxPlayers) {
            StopClient();
            return;
        }
       

        base.OnServerAddPlayer(conn);
        if (conn.identity == null)
            return;
        if (conn.identity.gameObject == null)
            return;

        players.AddPlayer(conn.identity.gameObject);
    }

    //public override void OnServerDisconnect(NetworkConnection conn)
    //{
    //    base.OnServerDisconnect(conn);
    //    players.PlayersGameObjects.Remove(conn.identity.gameObject);
    //}

}
