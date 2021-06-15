using Mirror;
using UnityEngine;


public class SpectatorStateController : NetworkBehaviour
{
    #region Serialized.
    /// <summary>
    /// How much to predict movement. A Value of 1f will result in this object moving at the same rate as it's last known value.
    /// </summary>
    [Tooltip("How much to predict movement. A Value of 1f will result in this object moving at the same rate as it's last known value.")]
    [Range(0f, 1f)]
    [SerializeField]
    private float _predictionRatio = 0.9f;
    #endregion

    #region Private.
    /// <summary>
    /// Rigidbody on this object.
    /// </summary>
    private Rigidbody _rigidbody;
    /// <summary>
    /// Last SpectatorMotorState received from the server.
    /// </summary>
    private SpectatorMotorState? _receivedSpectatorMotorState = null;
    /// <summary>
    /// Velocity from previous simulation.
    /// </summary>
    private Vector3 _lastVelocity;
    /// <summary>
    /// Angular velocity from previous simulation.
    /// </summary>
    private Vector3 _lastAngularVelocity;
    /// <summary>
    /// Baseline for velocity magnitude.
    /// </summary>
    private float? _velocityBaseline = null;
    /// <summary>
    /// Baseline for angular velocity magnitude.
    /// </summary>
    private float? _angularVelocityBaseline = null;
    #endregion

    private void Awake()
    {
        FirstInitialize();
    }

    private void OnEnable()
    {
        if (!NetworkServer.active)
            SubscribeToEvents(true);
        else
            FixedUpdateManager.OnFixedUpdate += FixedUpdateManager_OnFixedUpdate;
    }

    private void OnDisable()
    {
        SubscribeToEvents(false);
        FixedUpdateManager.OnFixedUpdate -= FixedUpdateManager_OnFixedUpdate;
    }

    /// <summary>
    /// Received every FixedUpdate.
    /// </summary>
    private void FixedUpdateManager_OnFixedUpdate()
    {
        if (base.isServer)
        {
            SendStates();
        }
    }

    /// <summary>
    /// Initializes this script for use. Should only be completed once.
    /// </summary>
    private void FirstInitialize()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// Changes subscriptions required for this script.
    /// </summary>
    /// <param name="subscribe"></param>
    private void SubscribeToEvents(bool subscribe)
    {
        if (subscribe)
        {
            SpectatorRollbackManager.OnRollbackStart += SpectatorRollbackManager_OnRollbackStart;
            FixedUpdateManager.OnPostSimulate += FixedUpdateManager_OnPostSimulate;
        }
        else
        {
            SpectatorRollbackManager.OnRollbackStart -= SpectatorRollbackManager_OnRollbackStart;
            FixedUpdateManager.OnPostSimulate -= FixedUpdateManager_OnPostSimulate;
        }
    }

    /// <summary>
    /// Received after a simulated fixed update occurs. Physics would have simulated prior to this event.
    /// </summary>
    private void FixedUpdateManager_OnPostSimulate(float deltaTime, bool replay)
    {
        if (base.hasAuthority)
            return;
        if (_predictionRatio == 0f)
            return;

        PredictVelocity(ref _velocityBaseline, ref _lastVelocity, _rigidbody.velocity, false);
        PredictVelocity(ref _angularVelocityBaseline, ref _lastAngularVelocity, _rigidbody.angularVelocity, true);

        _lastVelocity = _rigidbody.velocity;
        _lastAngularVelocity = _rigidbody.angularVelocity;
    }

    /// <summary>
    /// Tries to predict velocity.
    /// </summary>
    /// <param name="velocityBaseline"></param>
    /// <param name="lastVelocity"></param>
    /// <param name="velocity"></param>
    /// <param name="angular"></param>
    private void PredictVelocity(ref float? velocityBaseline, ref Vector3 lastVelocity, Vector3 velocity, bool angular)
    {
        float velocityDifference;
        float directionDifference;

        /* Velocity. */
        directionDifference = (velocityBaseline != null) ?
            Vector3.SqrMagnitude(lastVelocity.normalized - velocity.normalized) :
            0f;
        //If direction has changed too much then reset the baseline.
        if (directionDifference > 0.01f)
        {
            velocityBaseline = null;
        }
        //Direction hasn't changed enough to reset baseline.
        else
        {
            //Difference in velocity since last simulation.
            velocityDifference = Vector3.Magnitude(lastVelocity - velocity);
            //If there is no baseline.
            if (velocityBaseline == null)
            {
                if (velocityDifference > 0)
                    velocityBaseline = velocityDifference;
            }
            //If there is a baseline.
            else
            {
                //If the difference exceeds the baseline by 10% then reset baseline so another will be calculated.
                if (velocityDifference > (velocityBaseline.Value * 1.1f) || velocityDifference < (velocityBaseline.Value * 0.9f))
                { 
                    velocityBaseline = null;
                }
                //Velocity difference is close enough to the baseline to where it doesn't need to be reset, so use prediction.
                else
                { 
                    if (!angular)
                        _rigidbody.velocity = Vector3.Lerp(velocity, lastVelocity, _predictionRatio);
                    else
                        _rigidbody.angularVelocity = Vector3.Lerp(velocity, lastVelocity, _predictionRatio);
                }
            }
        }
    }

    /// <summary>
    /// Received when a rollback starts.
    /// </summary>
    private void SpectatorRollbackManager_OnRollbackStart()
    {
        if (base.hasAuthority)
            return;
        if (_receivedSpectatorMotorState == null)
            return;

        //Update transform and rigidbody.
        transform.position = _receivedSpectatorMotorState.Value.Position;
        transform.rotation = _receivedSpectatorMotorState.Value.Rotation;
        _rigidbody.velocity = _receivedSpectatorMotorState.Value.Velocity;
        _rigidbody.angularVelocity = _receivedSpectatorMotorState.Value.AngularVelocity;
        //Set prediction defaults.
        _velocityBaseline = null;
        _angularVelocityBaseline = null;
        _lastVelocity = _rigidbody.velocity;
        _lastAngularVelocity = _rigidbody.angularVelocity;
    }

    /// <summary>
    /// Sends current states of this object to client.
    /// </summary>
    private void SendStates()
    {
        SpectatorMotorState state = new SpectatorMotorState
        {
            Position = transform.position,
            Rotation = transform.rotation,
            Velocity = _rigidbody.velocity,
            AngularVelocity = _rigidbody.angularVelocity
        };

        RpcSendState(state);
    }

    /// <summary>
    /// Sends transform and rigidbody state to spectators.
    /// </summary>
    /// <param name="state"></param>
    [ClientRpc(channel = 1, includeOwner = false)]
    private void RpcSendState(SpectatorMotorState state)
    {
        if (base.isServer)
            return;

        _receivedSpectatorMotorState = state;
    }
}