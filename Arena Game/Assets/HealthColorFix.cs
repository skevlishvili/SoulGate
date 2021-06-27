using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthColorFix : MonoBehaviour
{
    [SerializeField]
    Image Health;
    [SerializeField]
    Image HealthBackground;

    // Start is called before the first frame update
    void Start()
    {
        if (ClientScene.localPlayer.gameObject.GetInstanceID() != gameObject.GetInstanceID()) {
            Health.color = new Color(255, 113, 0);
            HealthBackground.color = new Color(255, 0, 0);
        }
    }
}
