using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadyScript : MonoBehaviour
{
    public void PlayerReady() {
        this.GetComponentInParent<PlayerAction>().IsReady = true;
    }
}
