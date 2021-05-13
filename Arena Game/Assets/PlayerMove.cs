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


    private ServerMoveState _serverMoveState;
    private ClientMoveState _clientMoveState;
    private uint _lastClientStateReceived = 0;

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
            ProcessReceivedClientMotorState();
        }
    }


    //[Command]
    //private void CmdMove(Vector3 destination)
    //{
    //    // make additional checks if needed

    //    // TODO: check client


    //    //agent.SetDestination(destination);

    //    //// ROTATION
    //    //Quaternion rotationToLook = Quaternion.LookRotation(destination - transform.position);
    //    //float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationToLook.eulerAngles.y, ref rotateVelocity, rotateSpeedMovement * (Time.deltaTime * 5));
    //    //transform.eulerAngles = new Vector3(0, rotationY, 0);

    //    RpcMove(destination);
    //}

    //[ClientRpc]
    //private void RpcMove(Vector3 destination)
    //{
    //    // MOVE
    //    // Gets the coordinates of where the mouse clicked and moves the character there
    //    agent.SetDestination(destination);

    //    // ROTATION
    //    Quaternion rotationToLook = Quaternion.LookRotation(destination - transform.position);
    //    float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationToLook.eulerAngles.y, ref rotateVelocity, rotateSpeedMovement * (Time.deltaTime * 5));
    //    transform.eulerAngles = new Vector3(0, rotationY, 0);
    //    Debug.Log("Called");
    //}


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
        if (_serverMoveState.Position == Vector3.zero)
            return;


        ServerMoveState serverState = _serverMoveState;
        _serverMoveState = new ServerMoveState {
            Position = Vector3.zero,
            Rotation = Quaternion.identity,
        };


        //Snap motor to server values.
        transform.position = serverState.Position;
        transform.rotation = serverState.Rotation;

       
        ProcessInputs(_clientMoveState);
    }

    [Server]
    private void ProcessReceivedClientMotorState()
    {
        if (base.isClient && base.hasAuthority)
            return;

        sbyte timingStepChange = 0;

  
        //If there is input to process.
        //if (_clientMoveState.Destination == Vector3.zero)
        //{

            ProcessInputs(_clientMoveState);

            ServerMoveState responseState = new ServerMoveState
            {
                FixedFrame = 0,
                Position = transform.position,
                Rotation = transform.rotation,
                TimingStepChange = timingStepChange
            };

            //Send results back to the owner.
            TargetServerStateUpdate(base.connectionToClient, responseState);
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

        ClientMoveState state = new ClientMoveState
        {
            FixedFrame = 0,
            Destination = destination.point
        };

        ProcessInputs(state);
        CmdSendInputs(state);
    }


    [Command(channel = 1)]
    private void CmdSendInputs(ClientMoveState state)
    {
        //Only for client host.
        if (base.isClient && base.hasAuthority)
            return;

        /* Go through every new state and if the fixed frame
         * for that state is newer than the last received
         * fixed frame then add it to motor states. */
      
        //if (state.FixedFrame > _lastClientStateReceived)
        //{
            _lastClientStateReceived = state.FixedFrame;
            _clientMoveState = state;
        //}
    }




    [TargetRpc(channel = 1)]
    private void TargetServerStateUpdate(NetworkConnection conn, ServerMoveState state)
    {
        //Exit if received state is older than most current.
        if (_serverMoveState.Position != Vector3.zero && state.FixedFrame < _serverMoveState.FixedFrame)
            return;

        _serverMoveState = state;
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
