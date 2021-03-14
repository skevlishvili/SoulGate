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

    PhotonView PV;

    private void Update() {
        if(PV.IsMine) {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                CanvasGroup canvasGroup = GameObject.Find("EscapeMenu").GetComponent<CanvasGroup>();
                canvasGroup.alpha = 1f;
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
            }
        }        
    }

    void Awake()
    {
        PV = gameObject.GetComponent<PhotonView>();
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
            Debug.Log("I'm Making a Scene BABYYY");
            PhotonNetwork.Instantiate("Prefabs/PlayerController", Vector3.zero, Quaternion.identity);
        }
    }

}
