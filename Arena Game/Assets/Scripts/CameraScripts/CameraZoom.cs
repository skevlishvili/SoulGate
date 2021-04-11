using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{

    public Camera cam;
    private float FOV;
    public float zoomSpeed;

    private float mouseScrollInput;


    // Start is called before the first frame update
    void Start()
    {
        FOV = cam.fieldOfView;
    }

    // Update is called once per frame
    void Update()
    {
        mouseScrollInput = Input.GetAxis("Mouse ScrollWheel");

        FOV -= mouseScrollInput * zoomSpeed;

        FOV = Mathf.Clamp(FOV, 10, 60);


        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, FOV, zoomSpeed);
    }
}
