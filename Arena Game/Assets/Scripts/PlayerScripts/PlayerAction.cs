using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class PlayerAction : NetworkBehaviour
{
    public float rotateSpeedMovement = 0.1f;
    public float rotateVelocity;

    [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
    public static GameObject LocalPlayerInstance;

    //public int[] PlayerSkills;

    #region Referances
    public NavMeshAgent agent;

    public Camera cam;

    public PlayerAnimator animator;

    public Unit unitStat;

    public bool IsDead;
    public bool IsReady;
    public bool IsTyping;

    public GameObject ScoreBoard;

    public GameObject ChatInput;
    private UnityEngine.UI.InputField chatInputField;

    [SerializeField]
    private Abillities abilities;

    //PhotonView PV;

    public Canvas HUD;

    public event Action OnPlayerDeath;
    public event Action OnPlayerReady;

    public GameObject ReadyText;

    #endregion

    //PhotonView ProjPV;
    private Vector3 spawnPoint;
    private Vector3 movement;
    private Vector3 networkPosition;
    private Quaternion networkRotation;

    public int[] PlayerSkills;


    public float speed = 9.16f;

    //public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
    //    if (stream.IsWriting)
    //    {
    //        stream.SendNext(transform.position);
    //        stream.SendNext(transform.rotation);
    //        stream.SendNext(movement);
    //    }
    //    else
    //    {
    //        networkPosition = (Vector3)stream.ReceiveNext();
    //        networkRotation = (Quaternion)stream.ReceiveNext();
    //        movement = (Vector3)stream.ReceiveNext();


    //        float lag = Mathf.Abs((float)(PhotonNetwork.Time - info.SentServerTime));
    //        networkPosition += (movement * lag);
    //    }
    //}

    void Awake()
    {
        spawnPoint = gameObject.transform.position;
        initStats();

        // #Important
        // used in GameManager.cs: we keep track of the localPlayer instance to prevent instantiation when levels are synchronized
        //if (photonView.IsMine)
        //{
            LocalPlayerInstance = gameObject;
        //}
        // #Critical
        // we flag as don't destroy on load so that instance survives level synchronization, thus giving a seamless experience when levels load.
        DontDestroyOnLoad(gameObject);

        gameObject.tag = "Player";
        //PV = gameObject.GetComponent<PhotonView>();

        OnPlayerDeath += DeathAll;        
        OnPlayerReady += ReadyAll;

        //if (PV.IsMine) {
        //    var roundManager = LocalPlayerInstance.GetComponentInChildren<RoundManager>();
        //    roundManager.LocalPlayerAction = LocalPlayerInstance.GetComponent<PlayerAction>();
        //}

        //ReadyText.GetComponent<UnityEngine.UI.Text>().text = "Press \"enter\" when you're ready";

        IsReady = true;


        var cameras = GameObject.FindGameObjectsWithTag("MainCamera");

        cam = cameras[0].GetComponent<Camera>();
    }

    private void Start()
    {
        GameObject Player = GameObject.Find("Player");
        animator = gameObject.GetComponent<PlayerAnimator>();
        chatInputField = ChatInput.GetComponent<UnityEngine.UI.InputField>();
    }

    private void ToggleCanvas(CanvasGroup canvasGroup, bool on) {
        canvasGroup.alpha = on ? 1f : 0f;
        canvasGroup.interactable = on;
        canvasGroup.blocksRaycasts = on;
    }

    // Update is called once per frame
    [Client]
    void FixedUpdate()
    {

        //var hudCanvas = HUD.GetComponent<CanvasGroup>();

        //if (!isLocalPlayer)
        //{
        //    ToggleCanvas(hudCanvas, false);
        //    ReadyText.GetComponent<UnityEngine.UI.Text>().text = "";


        //    return;
        //}

        //Vector3 oldPosition = transform.position;
        //if (Input.GetKeyDown(KeyCode.Return) && !IsDead && !IsReady)
        //{
        //    ReadyText.GetComponent<UnityEngine.UI.Text>().text = "";
        //    OnPlayerReady();
        //    return;
        //}


        //if (IsTyping) {
        //    return;
        //}

        //if (Input.GetKeyDown(KeyCode.Tab))
        //{
        //    ScoreBoard.SetActive(true);
        //}

        //if (Input.GetKeyUp(KeyCode.Tab))
        //{
        //    ScoreBoard.SetActive(false);
        //}


        //if (!IsDead && !IsReady) {
        //    ReadyText.GetComponent<UnityEngine.UI.Text>().text = "Press \"enter\" when you're ready";
        //}

        //if (IsDead || !IsReady)
        //{
        //    return;
        //}

        //if (unitStat.Health <= 0 && !IsDead)
        //{
        //    OnPlayerDeath();
        //}

        //GameObject canvas = GameObject.Find("EscapeMenu");
        //if(canvas != null)
        //{
        //    CanvasGroup canvasTest = GameObject.Find("EscapeMenu").GetComponent<CanvasGroup>();

        //    if (canvasTest.alpha == 1f)
        //    {
        //        ToggleCanvas(hudCanvas, false);
        //        return;
        //    }
        //    else
        //    {
        //        ToggleCanvas(hudCanvas, true);
        //    }
        //}

        //movement = transform.position - oldPosition;
    }

    void initStats() {
        unitStat.PhysicalDefence = 20;
        unitStat.MagicDefence = 20;
        unitStat.Strength = 20;
        unitStat.Agility = 20;
        unitStat.Intelligence = 20;
        unitStat.IsDead = false;
        agent.speed = unitStat.Agility / 2;
        IsDead = false;
        IsReady = false;

        PlayerSkills = new int[9] { 1, 7, 2, 5, 0, 0, 0, 0, 0 };
    }

    void ReadyAll()
    {
        IsReady = true;
    }


    //[PunRPC]
    //void Ready() {
    //    IsReady = true;
    //}

    void DeathAll()
    {
        //PV.RPC("Death", RpcTarget.All);
    }


    //[PunRPC]
    //void Death() {
    //    animator.IsDead();
    //    unitStat.IsDead = true;
    //    IsDead = true;
    //}


    //public void RespawnAll()
    //{
    //    if (!PV.IsMine)
    //        return;

    //    PV.RPC("Respawn", RpcTarget.All);
    //}


    //[PunRPC]
    //public void Respawn()
    //{
    //    agent.isStopped = true;
    //    gameObject.transform.position = spawnPoint;


    //    initStats();
    //    animator.IsAlive();
    //}


    //[PunRPC]
    //void takeDamage(float damage)
    //{
    //    Debug.Log("IN THE ACTUAL TAKE DAMAGE FUNCTION");

    //    unitStat.Health -= damage;
    //}
}
