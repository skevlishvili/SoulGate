using Mirror;
using UnityEngine;

public class PlatformStateController : NetworkBehaviour
{
    #region Private.
    /// <summary>
    /// Last SpectatorPlatformState received from the server.
    /// </summary>
    private SpectatorPlatformState? _receivedSpectatorPlatformState = null;
    /// <summary>
    /// Platform component on this object.
    /// </summary>
    private Platform _platform = null;
    #endregion

    private void Awake()
    {
        _platform = GetComponent<Platform>();
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
    /// Changes subscriptions required for this script.
    /// </summary>
    /// <param name="subscribe"></param>
    private void SubscribeToEvents(bool subscribe)
    {
        if (subscribe)
            SpectatorRollbackManager.OnRollbackStart += SpectatorRollbackManager_OnRollbackStart;
        else
            SpectatorRollbackManager.OnRollbackStart -= SpectatorRollbackManager_OnRollbackStart;
    }

    /// <summary>
    /// Received when a rollback starts.
    /// </summary>
    private void SpectatorRollbackManager_OnRollbackStart()
    {
        if (base.hasAuthority)
            return;
        if (_receivedSpectatorPlatformState == null)
            return;

        //Reset to last received server update so position will match up after rollback completes.
        transform.position = _receivedSpectatorPlatformState.Value.Position;
        _platform.SetMovingUp(_receivedSpectatorPlatformState.Value.MovingUp);
    }

    /// <summary>
    /// Sends current states of this object to client.
    /// </summary>
    [Server]
    private void SendStates()
    {
        SpectatorPlatformState state = new SpectatorPlatformState
        {
            Position = transform.position,
            MovingUp = _platform.MovingUp
        };

        RpcSendState(state);
    }

    /// <summary>
    /// Sends transform and rigidbody state to spectators.
    /// </summary>
    /// <param name="state"></param>
    [ClientRpc(channel = 1, includeOwner = false)]
    private void RpcSendState(SpectatorPlatformState state)
    {
        if (base.isServer)
            return;

        _receivedSpectatorPlatformState = state;
    }
}
