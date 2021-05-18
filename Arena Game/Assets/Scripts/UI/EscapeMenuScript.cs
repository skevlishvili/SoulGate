using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeMenuScript : MonoBehaviour
{
    public GameObject player;

    //public GameObject shopobj;
    CanvasGroup canvasGroup;
    public bool ShopOpen = false;

    void Start()
    {
        canvasGroup = GameObject.Find("EscapeMenu").GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCodeController.Menu))
        {
            
            canvasGroup.alpha = 1f;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }
    }

    public void Resume() {
        CanvasGroup canvasGroup = this.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    public void Disconnect() {
        Destroy(player);
        //PhotonNetwork.LeaveRoom();
        //PhotonNetwork.LoadLevel(0);
    }

    public void Exit() {
        Application.Quit();
    }

}
