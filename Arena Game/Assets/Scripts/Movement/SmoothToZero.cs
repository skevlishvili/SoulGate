using Mirror;
using UnityEngine;

public class SmoothToZero : NetworkBehaviour
{
    #region Serialized.
    /// <summary>
    /// How quickly to smooth to zero.
    /// </summary>
    [Tooltip("How quickly to smooth to zero.")]
    [SerializeField]
    private float _smoothRate = 20f;
    #endregion

    #region Private.
    /// <summary>
    /// Position before simulation is performed.
    /// </summary>
    private Vector3 _position;
    /// <summary>
    /// Rotation before simulation is performed.
    /// </summary>
    private Quaternion _rotation;
    #endregion

    private void OnEnable()
    {
        if (NetworkClient.active)
            SubscribeToFixedUpdateManager(true);
    }
    private void OnDisable()
    {
        SubscribeToFixedUpdateManager(false);
    }

    private void Update()
    {
        Smooth();
    }

    /// <summary>
    /// Smooths position and rotation to zero values.
    /// </summary>
    private void Smooth()
    {
        float distance;
        distance = Mathf.Max(0.01f, Vector3.Distance(transform.localPosition, Vector3.zero));
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, Vector3.zero, distance * _smoothRate * Time.deltaTime);
        distance = Mathf.Max(1f, Quaternion.Angle(transform.localRotation, Quaternion.identity));
        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, Quaternion.identity, distance * _smoothRate * Time.deltaTime);
    }

    /// <summary>
    /// Changes event subscriptions on the FixedUpdateManager.
    /// </summary>
    /// <param name="subscribe"></param>
    private void SubscribeToFixedUpdateManager(bool subscribe)
    {
        if (subscribe)
        {
            FixedUpdateManager.OnPreFixedUpdate += FixedUpdateManager_OnPreFixedUpdate;
            FixedUpdateManager.OnPostFixedUpdate += FixedUpdateManager_OnPostFixedUpdate;
        }
        else
        {
            FixedUpdateManager.OnPreFixedUpdate -= FixedUpdateManager_OnPreFixedUpdate;
            FixedUpdateManager.OnPostFixedUpdate -= FixedUpdateManager_OnPostFixedUpdate;
        }
    }

    private void FixedUpdateManager_OnPostFixedUpdate()
    {
        transform.position = _position;
        transform.rotation = _rotation;
    }

    private void FixedUpdateManager_OnPreFixedUpdate()
    {
        _position = transform.position;
        _rotation = transform.rotation;
    }
}