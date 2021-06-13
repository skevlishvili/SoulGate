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
        try
        {
            if (Player != ClientScene.localPlayer.gameObject)
            {
                Destroy(gameObject);
            }
        }
        catch (System.Exception)
        {
            Destroy(gameObject);
        }
        
    }
}
