using UnityEngine;

namespace Assets.Scripts.States
{
    public struct ServerMoveState
    {
        public uint FixedFrame;
        public Vector3 Position;
        public Quaternion Rotation;
        public sbyte TimingStepChange;
    }
}
