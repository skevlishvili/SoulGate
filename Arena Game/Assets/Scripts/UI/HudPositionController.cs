using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class HudController : MonoBehaviour
{
    PhotonView PV;

    #region Referances
    public GameObject Hud;
    public PlayerAction Player;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        PV = gameObject.GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PV.IsMine)
        {
            Hud.transform.position = new Vector3(Hud.transform.position.x, -10000, Hud.transform.position.z);
        }
    }
}
