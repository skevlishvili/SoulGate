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

    public int[] PlayerSkills;

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
        InvokeRepeating("Regeneration", 1.0f, 1.0f);
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
        var hudCanvas = HUD.GetComponent<CanvasGroup>();

        if (!isLocalPlayer)
        {
            ToggleCanvas(hudCanvas, false);
            ReadyText.GetComponent<UnityEngine.UI.Text>().text = "";


            //transform.position = Vector3.MoveTowards(transform.position, networkPosition, Time.deltaTime * speed);
            //Debug.Log(agent.speed);
            //transform.rotation = networkRotation;

            return;
        }

        Vector3 oldPosition = transform.position;
        if (Input.GetKeyDown(KeyCode.Return) && !IsDead && !IsReady)
        {
            ReadyText.GetComponent<UnityEngine.UI.Text>().text = "";
            OnPlayerReady();
            return;
        }


        //if (Input.GetKeyDown(KeyCode.Return)) {
        //    if (IsTyping)
        //    {
        //        try
        //        {
        //            speed = float.Parse(chatInputField.text);
        //        }
        //        catch (Exception)
        //        {
        //            Debug.LogError(chatInputField.text);
        //        }


        //        chatInputField.DeactivateInputField();
        //        chatInputField.text = "";
        //        IsTyping = false;

        //        return;
        //    }

        //    chatInputField.ActivateInputField();
        //    IsTyping = true;

        //    return;
        //}

        if (IsTyping) {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ScoreBoard.SetActive(true);
        }

        if (Input.GetKeyUp(KeyCode.Tab))
        {
            ScoreBoard.SetActive(false);
        }


        if (!IsDead && !IsReady) {
            ReadyText.GetComponent<UnityEngine.UI.Text>().text = "Press \"enter\" when you're ready";
        }

        if (IsDead || !IsReady)
        {
            return;
        }

        if (unitStat.Health <= 0 && !IsDead)
        {
            OnPlayerDeath();
        }

        GameObject canvas = GameObject.Find("EscapeMenu");
        if(canvas != null)
        {
            CanvasGroup canvasTest = GameObject.Find("EscapeMenu").GetComponent<CanvasGroup>();

            if (canvasTest.alpha == 1f)
            {
                ToggleCanvas(hudCanvas, false);
                return;
            }
            else
            {
                ToggleCanvas(hudCanvas, true);
            }
        }

        if (Input.GetKeyDown(KeyCodeController.Moving) && !abilities.isFiring.All(x => x) && !unitStat.IsDead)
        {
            agent.isStopped = false;
            Move();
        }
        
        if (!unitStat.IsDead)
        {
            for (int i = 0; i < KeyCodeController.AbilitiesKeyCodeArray.Length; i++)
            {
                if (Input.GetKeyDown(KeyCodeController.AbilitiesKeyCodeArray[i]))
                {
                    abilities.SpellKeyCode(KeyCodeController.AbilitiesKeyCodeArray[i]);
                }
            }
        }

        movement = transform.position - oldPosition;
    }

    [Client]
    public void Move()
    {
        agent.isStopped = false;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit destination;

        // Checks if raycast hit the navmesh (navmesh is a predefined system where the agent can go)
        if (Physics.Raycast(ray, out destination, Mathf.Infinity))
        {
            StartCoroutine("SpawnMaker", destination);


            CmdMove(destination.point);
        }
    }

    [Command]
    private void CmdMove(Vector3 destination) {
        // make additional checks if needed

        // TODO: check client


        //agent.SetDestination(destination);

        //// ROTATION
        //Quaternion rotationToLook = Quaternion.LookRotation(destination - transform.position);
        //float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationToLook.eulerAngles.y, ref rotateVelocity, rotateSpeedMovement * (Time.deltaTime * 5));
        //transform.eulerAngles = new Vector3(0, rotationY, 0);

        RpcMove(destination);
    }

    [ClientRpc]
    private void RpcMove(Vector3 destination) {
        // MOVE
        // Gets the coordinates of where the mouse clicked and moves the character there
        agent.SetDestination(destination);

        // ROTATION
        Quaternion rotationToLook = Quaternion.LookRotation(destination - transform.position);
        float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationToLook.eulerAngles.y, ref rotateVelocity, rotateSpeedMovement * (Time.deltaTime * 5));
        transform.eulerAngles = new Vector3(0, rotationY, 0);
        Debug.Log("Called");
    }

    [Client]
    IEnumerator SpawnMaker(RaycastHit destination)
    {
        UnityEngine.Object pPrefab = Resources.Load("Prefabs/UI/Marker 1"); // note: not .prefab!
        
        GameObject Marker = (GameObject)GameObject.Instantiate(pPrefab, destination.point, Quaternion.Euler(-90, 0, 0));	
        yield return new WaitForSeconds(1);	    
        Destroy(Marker);	
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (!PV.IsMine)
        //{
        //    return;
        //}
        Projectile projectile = other.GetComponent<Projectile>();
        if (projectile == null)
            return;
        //ProjPV = other.GetComponent<PhotonView>();
        
        float damage = projectile.damage[0] + projectile.damage[1] + projectile.damage[2];//----------------------------gasasworebelia---------------

        Debug.Log("is it triggering?");


        //if (PV.IsMine && !ProjPV.IsMine)
        //{

        //    var score = unitStat.Health - damage <= 0 ? 100 : (int)(damage / 10);
        //    ScoreExtensions.AddScore(ProjPV.Owner, score);
        //    PV.RPC("takeDamage", RpcTarget.All, damage);
        //}

        //PhotonNetwork.Destroy(projectile.gameObject);

        //projectile.gameObject.SetActive(false);

        //projectile.DestroyProjectile();
        //GameObject hitVFX = PhotonNetwork.Instantiate("Prefabs/Skill/Spark/vfx_hit_v1", transform.position + Vector3.up * 2, projectile.transform.rotation);
    }

    void Regeneration()
    {
        float HealthRegen = 2;
        float ManaRegen = 10;

        if ((unitStat.Health + HealthRegen) <= unitStat.MaxHealth)
        {
            unitStat.Health += HealthRegen;
        }
        else
        {
            unitStat.Health = unitStat.MaxHealth;
        }

        if ((unitStat.Mana + ManaRegen) <= unitStat.MaxMana)
        {
            unitStat.Mana += ManaRegen;
        }
        else
        {
            unitStat.Mana = unitStat.MaxMana;
        }

    }

    void initStats() {
        unitStat.Health = 200;
        unitStat.Mana = 200;
        unitStat.Xp = 0;
        unitStat.Money = 5000;
        unitStat.MaxHealth = unitStat.Health;
        unitStat.MaxMana = unitStat.Mana;
        unitStat.PhysicalDefence = 20;
        unitStat.MagicDefence = 20;
        unitStat.Height = 2;
        unitStat.weight = 80;
        unitStat.strength = 20;
        unitStat.Agility = 20;
        unitStat.Intelligence = 20;
        unitStat.Charisma = 20;
        unitStat.IsHalfHealth = false;
        unitStat.IsDead = false;
        agent.speed = unitStat.Agility / 2;
        IsDead = false;
        IsReady = false;

        PlayerSkills = new int[9] { 1, 7, 2, 5, 0, 0, 0, 0, 0 };
    }

    void ReadyAll()
    {
        IsReady = true;
        //PV.RPC("Ready", RpcTarget.All);
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
