using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatAndHudFix : MonoBehaviour
{
    public GameObject Player;


    // Start is called before the first frame update
    void Start()
    {
        if (Player != ClientScene.localPlayer.gameObject) {
            Destroy(gameObject);
        }
    }
}
