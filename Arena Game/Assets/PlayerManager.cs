using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : NetworkManager
{
    public List<GameObject> Players = new List<GameObject>();


    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        base.OnServerAddPlayer(conn);
        if (conn.identity == null)
            return;
        if (conn.identity.gameObject == null)
            return;

        Players.Add(conn.identity.gameObject);
    }
}
