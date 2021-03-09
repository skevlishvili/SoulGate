using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class FollowPlayer : MonoBehaviour
{

    //public Transform player;

    private Vector3 cameraOffset;


    [Range(0.01f, 1.0f)]
    public float smoothness = 0.5f;


    // Start is called before the first frame update
    void Start()
    {
        //cameraOffset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(ClientScene.localPlayer != null) {
            GameObject playerObj = ClientScene.localPlayer.gameObject;

            if(playerObj != null) {
                Transform player = playerObj.transform;
                
                if(cameraOffset == new Vector3(0,0,0)) {
                    cameraOffset = transform.position - player.transform.position;
                } else
                {
                    Vector3 newPos = player.position + cameraOffset;
                    transform.position = Vector3.Slerp(transform.position, newPos, smoothness);   
                }
            }
        }
    }
}
