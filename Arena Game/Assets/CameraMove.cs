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


public class MovingPlatform : MonoBehaviour
{
    public Vector3 MoveBy;
    Vector3 pointA;
    Vector3 pointB;
    public float time_to_wait;
    float delay;
    public Vector3 SpeedAndDirection;
    void Start()
    {
        this.pointA = this.transform.position;
        this.pointB = this.pointA + MoveBy;
        delay = time_to_wait;
    }
    bool isArrived(Vector3 pos, Vector3 target)
    {
        pos.z = 0;
        target.z = 0;
        return Vector3.Distance(pos, target) <= 0.2f;
    }
    bool toB = true;
    void FixedUpdate()
    {
        time_to_wait -= Time.deltaTime;
        if (time_to_wait <= 0)
        {
            if (toB)
            {
                if (isArrived(this.transform.position, this.pointB) == false)
                {
                    this.transform.Translate(SpeedAndDirection * Time.deltaTime);
                }
                else
                {
                    toB = false;
                    time_to_wait = delay;
                }
            }
            if (toB == false)
            {
                if (isArrived(this.pointA, this.transform.position) == false)
                {
                    this.transform.Translate(-SpeedAndDirection * Time.deltaTime);
                }
                else
                {
                    toB = true;
                    time_to_wait = delay;
                }
            }
        }
    }
}