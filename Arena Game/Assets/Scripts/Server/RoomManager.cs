using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using System.IO;
using Hashtable = ExitGames.Client.Photon.Hashtable;


public class RoomManager : MonoBehaviourPunCallbacks
{
    public static RoomManager Instance;
    public static bool PlayerLoaded;
    public static int Round;

    void Awake()
    {
        var players = PhotonNetwork.CurrentRoom.Players;
        foreach (var player in players)
        {
            if (player.Value.IsLocal)
            {
                Hashtable hash = new Hashtable();
                hash.Add("IsAlive", true);
                PhotonNetwork.CurrentRoom.Players[player.Key].SetCustomProperties(hash);
            }
        }

        PlayerLoaded = false;
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        Instance = this;
    }

    public override void OnEnable()
    {
        base.OnEnable();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }



    public override void OnDisable()
    {
        base.OnDisable();    
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if(scene.buildIndex == 1 && !PlayerLoaded)
        {
            PlayerLoaded = true;
            Debug.Log("Loading Scene ... ");
            PhotonNetwork.Instantiate("Prefabs/Player/PlayerControllerPrefab", Vector3.zero, Quaternion.identity);
        }
    }

}