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



    void Awake()
    {
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
        if(scene.buildIndex == 1)
        {
            Debug.Log("Scene Loaded with Player Instantiate");
            PhotonNetwork.Instantiate("Prefabs/PlayerController", Vector3.zero, Quaternion.identity);
        }
    }

}
