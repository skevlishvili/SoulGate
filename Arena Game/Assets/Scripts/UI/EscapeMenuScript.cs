using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EscapeMenuScript : MonoBehaviour
{
    public GameObject player;
    PhotonView PV;

    public GameObject shopobj;
    public int CounterShop = 0;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCodeController.Menu))
        {
            CanvasGroup canvasGroup = GameObject.Find("EscapeMenu").GetComponent<CanvasGroup>();
            canvasGroup.alpha = 1f;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }

        if (Input.GetKeyDown(KeyCodeController.Shop))
        {
            CounterShop += 1;
        }
    }

    void Start()
    {
        InvokeRepeating("OpenShop", 0f, 0.5f);

    }

    public void Resume() {
        CanvasGroup canvasGroup = this.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    public void Disconnect() {
        Destroy(player);
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel(0);
    }

    public void Exit() {
        Application.Quit();
    }


    void OpenShop()
    {
        if (CounterShop % 2 == 1)
        {
            shopobj.SetActive(true);
        }
        else
        {
            shopobj.SetActive(false);
        }
    }
}
