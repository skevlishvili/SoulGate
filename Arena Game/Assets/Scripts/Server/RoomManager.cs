using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using System.IO;


public class RoomManager : MonoBehaviourPunCallbacks
{
    public static RoomManager Instance;
    public static bool PlayerLoaded;

    void Awake()
    {
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