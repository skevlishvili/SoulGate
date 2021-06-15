using UnityEngine;

/// <summary>
/// State of the motor sent from the server.
/// </summary>
public struct SpectatorMotorState
{
    public Vector3 Position;
    public Quaternion Rotation;
    public Vector3 Velocity;
    public Vector3 AngularVelocity;
}