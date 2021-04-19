using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderDetectionObject : MonoBehaviour
{
    public Component ColliderObject;

    public Component ReturnColliderScript()
    {
        return ColliderObject;
    }
}
