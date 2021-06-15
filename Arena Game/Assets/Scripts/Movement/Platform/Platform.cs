using Mirror;
using UnityEngine;


public class Platform : NetworkBehaviour
{
    #region Public.
    /// <summary>
    /// True if moving up.
    /// </summary>
    public bool MovingUp { get; private set; } = true;
    /// <summary>
    /// Sets MovingUp value.
    /// </summary>
    /// <param name="value"></param>
    public void SetMovingUp(bool value)
    {
        MovingUp = value;
    }
    #endregion

    #region Serialized.
    /// <summary>
    /// Move rate for the platform.
    /// </summary>
    [Tooltip("Move rate for the platform.")]
    [SerializeField]
    private float _moveRate = 3f;
    /// <summary>
    /// How much to move up or down from starting position.
    /// </summary>
    [Tooltip("How much to move up or down from starting position.")]
    [SerializeField]
    private float _moveDistance = 3f;
    #endregion

    #region Private.
    /// <summary>
    /// Where the platform is when it loads on server.
    /// </summary>
    private Vector3 _startPosition;
    #endregion

    private void OnEnable()
    {
        FixedUpdateManager.OnFixedUpdate += FixedUpdateManager_OnFixedUpdate;
    }

    private void OnDisable()
    {
        FixedUpdateManager.OnFixedUpdate -= FixedUpdateManager_OnFixedUpdate;
    }

    public override bool OnSerialize(NetworkWriter writer, bool initialState)
    {
        if (initialState)
        {
            writer.WriteVector3(_startPosition);
            writer.WriteBoolean(MovingUp);
        }
        return base.OnSerialize(writer, initialState);
    }

    public override void OnDeserialize(NetworkReader reader, bool initialState)
    {
        if (initialState)
        {
            _startPosition = reader.ReadVector3();
            SetMovingUp(reader.ReadBoolean());
        }
        base.OnDeserialize(reader, initialState);
    }

    public override void OnStartServer()
    {
        base.OnStartServer();
        _startPosition = transform.position;
    }

    /// <summary>
    /// Received when a simulated fixed update occurs.
    /// </summary>
    private void FixedUpdateManager_OnFixedUpdate()
    {
        MovePlatform();
    }


    /// <summary>
    /// Moves the platform.
    /// </summary>
    private void MovePlatform()
    {
        Vector3 upGoal = _startPosition + new Vector3(0f, _moveDistance, 0f);
        Vector3 downGoal = _startPosition - new Vector3(0f, _moveDistance, 0f);

        //Set move rate and which goal to move towards.
        float rate = Time.fixedDeltaTime * _moveRate;
        Vector3 goal = (MovingUp) ? upGoal : downGoal;
        //Move towards goal.
        transform.position = Vector3.MoveTowards(transform.position, goal, rate);

        //If at goal inverse move direction to begin moving the other way.
        if (transform.position == goal)
            MovingUp = !MovingUp;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0f, _moveDistance, 0f));
        Gizmos.DrawLine(transform.position, transform.position - new Vector3(0f, _moveDistance, 0f));
    }
}
