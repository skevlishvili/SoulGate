using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public bool Move;
    public bool Reset;

    public float Speed;
    public int Index;
    public float Distance;

    public Vector3[] Positions; 
    public Quaternion[] Rotations;

    public float RotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        Index = 1;
        Move = false;
        Distance = 2f;
        Speed = 20f;
        RotationSpeed = 0.0001f;

        Positions = new Vector3[4] { new Vector3(0, 130, -250), new Vector3(0, 80, -150), new Vector3(-70,40,-70), new Vector3(-80, 30, -15)};
        Rotations = new Quaternion[4] { Quaternion.Euler(33, 0, 0), Quaternion.Euler(33, -20, 0), Quaternion.Euler(40, -10, 0), Quaternion.Euler(60, 0, 0) };
    }

    // Update is called once per frame
    void Update()
    {
        if (Move) {
            MoveCamera();
        }

        if (Reset)
        {
            Index = 1;
            Reset = false;
            gameObject.transform.position = Positions[0];
            gameObject.transform.rotation = Rotations[0];
        }

    }



    private void MoveCamera()
    {
        if (Index == Positions.Length) {
            gameObject.transform.position = Positions[3];
            gameObject.transform.rotation = Rotations[3];
            return;
        }

        var speed = Positions[Index] - Positions[Index - 1];
        gameObject.transform.Translate(speed.normalized * Time.deltaTime * Speed, Space.World);
        gameObject.transform.rotation =  Quaternion.Lerp(transform.rotation, Rotations[Index], Time.time * RotationSpeed);

        if (Vector3.Distance(gameObject.transform.position, Positions[Index]) <= Distance)
            Index++;
    }
}
