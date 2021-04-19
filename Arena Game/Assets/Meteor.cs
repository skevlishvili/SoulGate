using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{

    public Camera cam;

    // Update is called once per frame
    void Update()
    {
        SpawnSkill();
    }




    void SpawnSkill()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit destination;

        if (Input.GetKeyDown(KeyCode.M) && Physics.Raycast(ray, out destination, Mathf.Infinity))
        {
            PhotonNetwork.Instantiate("Prefabs/Skill/AOE/Meteor", destination.point, Quaternion.identity);
        }else if (Input.GetKeyDown(KeyCode.L) && Physics.Raycast(ray, out destination, Mathf.Infinity))
        {
            PhotonNetwork.Instantiate("Prefabs/Skill/AOE/Lightning strike", destination.point, Quaternion.identity);
        }
    }
}
