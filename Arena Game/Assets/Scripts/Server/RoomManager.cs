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
    private static List<Vector3> playerPositions;


    void Awake()
    {
        playerPositions = new List<Vector3>();
        playerPositions.Add(new Vector3(30, 0, 30));
        playerPositions.Add(new Vector3(-30, 0, 30));
        playerPositions.Add(new Vector3(30, 0, -30));
        playerPositions.Add(new Vector3(-30, 0, -30));

        var players = PhotonNetwork.CurrentRoom.Players;
        //foreach (var player in players)
        //{
        //    if (player.Value.IsLocal)
        //    {
        //        Hashtable hash = new Hashtable();
        //        hash.Add("IsAlive", true);
        //        PhotonNetwork.CurrentRoom.Players[player.Key].SetCustomProperties(hash);
        //    }
        //}

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


            //should be change in a way that doesn't require this duplicate code, just taking this out in a function isn't a solution

            var currentPosition = Vector3.zero;
            var players = PhotonNetwork.CurrentRoom.Players;
            foreach (var player in players)
            {
                if (player.Value.IsLocal)
                {
                    currentPosition = playerPositions[players.Count - 1];
                    break;
                }
            }

            PhotonNetwork.Instantiate("Prefabs/Player/PlayerControllerPrefab", currentPosition, Quaternion.identity);

        }
    }

}