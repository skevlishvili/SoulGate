using Assets.Scripts.States;
using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class PlayerMove : NetworkBehaviour
{
    public NavMeshAgent agent;
    Camera cam;
    public float rotateVelocity;
    public float rotateSpeedMovement = 0.1f;


    /// <summary>
    /// Stored client motor states.
    /// </summary>
    private List<ClientMoveState> _clientMoveStates = new List<ClientMoveState>();
    /// <summary>
    /// Move states received from the client.
    /// </summary>
    private Queue<ClientMoveState> _receivedClientMoveStates = new Queue<ClientMoveState>();
    /// <summary>
    /// Last FixedFrame processed from client.
    /// </summary>
    private uint _lastClientStateReceived = 0;
    /// <summary>
    /// Most current motor state received from the server.
    /// </summary>
    private ServerMoveState? _receivedServerMoveState = null;


    //private ServerMoveState _serverMoveState;
    //private ClientMoveState _clientMoveState;

    #region Const
    /// <summary>
    /// Maximum number of entries that may be held within ReceivedClientMoveStates.
    /// </summary>
    private const int MAXIMUM_RECEIVED_CLIENT_MOTOR_STATES = 10;
    /// <summary>
    /// How many past states to send to the server.
    /// </summary>
    private const int PAST_STATES_TO_SEND = 10;
    #endregion

    [SerializeField]
    private Abillities abilities;

    // Start is called before the first frame update
    void Start()
    {
        var cameras = GameObject.FindGameObjectsWithTag("MainCamera");

        cam = cameras[0].GetComponent<Camera>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //if (!base.hasAuthority && !base.isServer)
        //{
        //    CancelVelocity(false);
        //}

        if (base.hasAuthority)
        {
            ProcessReceivedServerMoveState();
            SendInputs();
        }

        if (base.isServer)
        {
            ProcessReceivedClientMoveState();
        }
    }


    private void ProcessInputs(ClientMoveState moveState)
    {
        // MOVE
        // Gets the coordinates of where the mouse clicked and moves the character there
        agent.SetDestination(moveState.Destination);

        // ROTATION
        Quaternion rotationToLook = Quaternion.LookRotation(moveState.Destination - transform.position);
        float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationToLook.eulerAngles.y, ref rotateVelocity, rotateSpeedMovement * (Time.deltaTime * 5));
        transform.eulerAngles = new Vector3(0, rotationY, 0);
    }


    [Client]
    private void ProcessReceivedServerMoveState()
    {
        //if (_serverMoveState.Position == Vector3.zero)
        //    return;


        //ServerMoveState serverState = _serverMoveState;
        //_serverMoveState = new ServerMoveState {
        //    Position = Vector3.zero,
        //    Rotation = Quaternion.identity,
        //};


        ////Snap motor to server values.
        //transform.position = serverState.Position;
        //transform.rotation = serverState.Rotation;

       
        //ProcessInputs(_clientMoveState);


        if (_receivedServerMoveState == null)
            return;

        ServerMoveState serverState = _receivedServerMoveState.Value;
        //FixedUpdateManager.AddTiming(serverState.TimingStepChange);
        _receivedServerMoveState = null;

        //Remove entries which have been handled by the server.
        int index = _clientMoveStates.FindIndex(x => x.FixedFrame == serverState.FixedFrame);
        if (index != -1)
            _clientMoveStates.RemoveRange(0, index);

        //Snap motor to server values.
        if ((transform.position - serverState.Position).magnitude > 15) {
            transform.position = serverState.Position;
        }
        transform.rotation = serverState.Rotation;
        //Physics.SyncTransforms();

        foreach (ClientMoveState clientState in _clientMoveStates)
        {
            ProcessInputs(clientState);
            Physics.Simulate(Time.fixedDeltaTime);
        }
    }

    [Server]
    private void ProcessReceivedClientMoveState()
    {
        //if (base.isClient && base.hasAuthority)
        //    return;

        //sbyte timingStepChange = 0;

  
        ////If there is input to process.
        ////if (_clientMoveState.Destination == Vector3.zero)
        ////{

        //    ProcessInputs(_clientMoveState);

        //    ServerMoveState responseState = new ServerMoveState
        //    {
        //        FixedFrame = 0,
        //        Position = transform.position,
        //        Rotation = transform.rotation,
        //        TimingStepChange = timingStepChange
        //    };

        //    //Send results back to the owner.
        //    TargetServerStateUpdate(base.connectionToClient, responseState);
        ////}


        if (base.isClient && base.hasAuthority)
            return;

        sbyte timingStepChange = 0;

        /* If there are no states then set timing change step
        * to a negative value, which will speed up the client
        * simulation. In result this will increase the chances
        * the client will send a packet which will arrive by every
        * fixed on the server. */
        if (_receivedClientMoveStates.Count == 0)
            timingStepChange = -1;
        /* Like subtracting a step, if there is more than one entry
        * then the client is sending too fast. Send a positive step
        * which will slow the clients send rate. */
        else if (_receivedClientMoveStates.Count > 1)
            timingStepChange = 1;

        //If there is input to process.
        if (_receivedClientMoveStates.Count > 0)
        {
            ClientMoveState state = _receivedClientMoveStates.Dequeue();
            //Process input of last received motor state.
            ProcessInputs(state);

            ServerMoveState responseState = new ServerMoveState
            {
                FixedFrame = state.FixedFrame,
                Position = transform.position,
                Rotation = transform.rotation,
                TimingStepChange = timingStepChange
            };

            //Send results back to the owner.
            TargetServerStateUpdate(base.connectionToClient, responseState);
        }
        ////If there is no input to process.
        //else if (timingStepChange != 0)
        //{
        //    //Send timing step change to owner.
        //    TargetChangeTimingStep(base.connectionToClient, timingStepChange);
        //}
    }

    [Client]
    private void SendInputs()
    {
        if (!(Input.GetKeyDown(KeyCodeController.Moving) && !abilities.isFiring.All(x => x)))
            return;

        agent.isStopped = false;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit destination;

        // Checks if raycast hit the navmesh (navmesh is a predefined system where the agent can go)
        if (Physics.Raycast(ray, out destination, Mathf.Infinity))
        {
            StartCoroutine("SpawnMaker", destination);
        }


        //var vectors = new List<Vector3>();
        //vectors.Add(new Vector3(30, 0, 30));
        //vectors.Add(new Vector3(-30, 0, 30));
        //vectors.Add(new Vector3(-30, 0, -30));
        //vectors.Add(new Vector3(30, 0, -30));
        //System.Random random = new System.Random();
        //var dest = vectors[random.Next(vectors.Count)];

        ClientMoveState state = new ClientMoveState
        {
            FixedFrame = 0,
            Destination = destination.point
        };

        _clientMoveStates.Add(state);

        //Only send at most up to client motor states count.
        int targetArraySize = Mathf.Min(_clientMoveStates.Count, 1 + PAST_STATES_TO_SEND);
        //Resize array to accomodate 
        ClientMoveState[] statesToSend = new ClientMoveState[targetArraySize];
        /* Start at the end of cached inputs, and add to the end of inputs to send.
         * This will add the older inputs first. */
        for (int i = 0; i < targetArraySize; i++)
        {
            //Add from the end of states first.
            statesToSend[targetArraySize - 1 - i] = _clientMoveStates[_clientMoveStates.Count - 1 - i];
        }

        ProcessInputs(state);
        CmdSendInputs(statesToSend);
    }


    [Command(channel = 1)]
    private void CmdSendInputs(ClientMoveState[] states)
    {
        //No states to process.
        if (states == null || states.Length == 0)
            return;
        //Only for client host.
        if (base.isClient && base.hasAuthority)
            return;

        /* Go through every new state and if the fixed frame
         * for that state is newer than the last received
         * fixed frame then add it to motor states. */
        for (int i = 0; i < states.Length; i++)
        {
            if (states[i].FixedFrame >= _lastClientStateReceived)
            {
                _receivedClientMoveStates.Enqueue(states[i]);
                _lastClientStateReceived = states[i].FixedFrame;
            }
        }

        while (_receivedClientMoveStates.Count > MAXIMUM_RECEIVED_CLIENT_MOTOR_STATES)
            _receivedClientMoveStates.Dequeue();
    }




    [TargetRpc(channel = 1)]
    private void TargetServerStateUpdate(NetworkConnection conn, ServerMoveState state)
    {
        //Exit if received state is older than most current.
        if (_receivedServerMoveState != null && state.FixedFrame < _receivedServerMoveState.Value.FixedFrame)
            return;

        _receivedServerMoveState = state;
    }


    [Client]
    IEnumerator SpawnMaker(RaycastHit destination)
    {
        UnityEngine.Object pPrefab = Resources.Load("Prefabs/UI/Marker 1"); // note: not .prefab!

        GameObject Marker = (GameObject)GameObject.Instantiate(pPrefab, destination.point, Quaternion.Euler(-90, 0, 0));
        yield return new WaitForSeconds(1);
        Destroy(Marker);
    }

}
