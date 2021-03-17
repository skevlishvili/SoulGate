using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EscapeMenuScript : MonoBehaviour
{
    public GameObject player;
    PhotonView PV;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //PV = GetComponent<PhotonView>();
        //if (PV.IsMine)
        //{
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                CanvasGroup canvasGroup = GameObject.Find("EscapeMenu").GetComponent<CanvasGroup>();
                canvasGroup.alpha = 1f;
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
            }
        //}
    }


    public void Resume() {
        CanvasGroup canvasGroup = this.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }


    public void Disconnect() {
        //var PV = player.GetComponent<PhotonView>();
        //if (PV.IsMine) {
            //PhotonNetwork.DestroyPlayerObjects(PhotonNetwork.player);
            Destroy(player);
            PhotonNetwork.LeaveRoom();
            PhotonNetwork.LoadLevel(0);
        //}
    }
    public void Exit() {
        Application.Quit();
    }
}
