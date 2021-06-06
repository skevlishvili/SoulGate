using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : NetworkManager
{
    public Players players;


    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        base.OnServerAddPlayer(conn);
        if (conn.identity == null)
            return;
        if (conn.identity.gameObject == null)
            return;

        players.AddPlayer(conn.identity.gameObject);
    }

}
