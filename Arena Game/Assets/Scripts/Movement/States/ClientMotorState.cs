﻿using UnityEngine;
/// <summary>
/// State of the motor sent from the client.
/// </summary>
public struct ClientMotorState
{
    public uint FixedFrame;
    public float Horizontal;
    public float Forward;
    public byte ActionCodes;
    public Vector3 Destination;
}