using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Players : NetworkBehaviour
{
    public List<GameObject> PlayersGameObjects = new List<GameObject>();
    public delegate void PlayerAddDelegate(GameObject player);
    public event PlayerAddDelegate EventPlayerAdd;


    //private void Update()
    //{
    //    Debug.Log(PlayersGameObjects.Count);
    //}

    [Server]
    public void AddPlayer(GameObject player)
    {
        PlayersGameObjects.Add(player);
        AddPlayerRpc(player);
    }

    [ClientRpc]
    public void AddPlayerRpc(GameObject player)
    {
        PlayersGameObjects.Add(player);
        PlayersGameObjects.Clear();
        PlayersGameObjects.AddRange(GameObject.FindGameObjectsWithTag("Player"));
        EventPlayerAdd?.Invoke(player);
    }
}
