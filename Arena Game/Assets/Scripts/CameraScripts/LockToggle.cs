using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockToggle : MonoBehaviour
{

    public FollowPlayer followPlayer;
    public CameraRoam cameraRoam;

    bool camViewChanged = false;
    // Start is called before the first frame update
    void Start()
    {
        cameraRoam.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(camViewChanged == false)
        {
            if (Input.GetKeyDown(KeyCode.Y))
            {
                if (ClientScene.localPlayer.gameObject.GetComponent<ChatBehaviour>().IsTyping)
                    return;

                camViewChanged = true;

                cameraRoam.enabled = true;
                followPlayer.enabled = false;
            }
        }
        else if(camViewChanged == true)
        {
            if (Input.GetKeyDown(KeyCode.Y))
            {
                if (ClientScene.localPlayer.gameObject.GetComponent<ChatBehaviour>().IsTyping)
                    return;

                camViewChanged = false;

                cameraRoam.enabled = false;
                followPlayer.enabled = true;
            }
        }
    }
}
