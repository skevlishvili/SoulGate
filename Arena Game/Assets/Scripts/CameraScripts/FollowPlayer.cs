using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class FollowPlayer : MonoBehaviour
{


    [SerializeField]
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
        //    {
        //// I don't care how fix this shit plz
        //var oldRotation = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
        //transform.rotation = Quaternion.Inverse(player.rotation);
        //transform.eulerAngles = new Vector3(oldRotation.x, transform.eulerAngles.y, oldRotation.z);


        //var x = Mathf.Sin(Mathf.PI * transform.eulerAngles.y/180) * cameraOffset.z;
        //var z = Mathf.Cos(Mathf.PI * transform.eulerAngles.y/180) * cameraOffset.z;

        //Debug.Log($"{transform.eulerAngles} {360 - (player.eulerAngles.y % 360)}  {(player.eulerAngles.y % 360)} ");
        if (ClientScene.localPlayer == null)
            return;

      

        Vector3 newPos = ClientScene.localPlayer.gameObject.transform.position + cameraOffset;
        //Vector3 newPos = player.position + new Vector3(x, cameraOffset.y, z);
        var slerpedPos = Vector3.Slerp(transform.position, newPos, smoothness);
        transform.position = new Vector3(slerpedPos.x, transform.position.y, slerpedPos.z);
    }
}
