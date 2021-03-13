using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Projectile : MonoBehaviour
{
    #region Variables
    public float speed;
    #endregion

    #region Referances
    public
    #endregion

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(DestroyObject());

        gameObject.transform.TransformDirection(Vector3.forward);
        gameObject.transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));
    }

    IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(3);
        PhotonNetwork.Destroy(gameObject);
    }
}