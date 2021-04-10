﻿using Photon.Pun;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class PlayerAction : MonoBehaviourPun
{
    public float rotateSpeedMovement = 0.1f;
    public float rotateVelocity;

    [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
    public static GameObject LocalPlayerInstance;

    public int[] PlayerSkills;

    #region Referances
    public NavMeshAgent agent;

    [SerializeField]
    private Camera cam;

    public PlayerAnimator animator;

    public Unit unitStat;

    public bool IsDead;
    public bool IsReady;

    [SerializeField]
    private Abillities abilities;

    PhotonView PV;

    public Canvas HUD;

    public event Action OnPlayerDeath;
    public event Action OnPlayerReady;

    public GameObject ReadyText;

    #endregion

    private Vector3 spawnPoint;




    void Awake()
    {
        spawnPoint = gameObject.transform.position;
        initStats();

        PlayerSkills = new int[4] { 1, 0, 0, 0 };

        // #Important
        // used in GameManager.cs: we keep track of the localPlayer instance to prevent instantiation when levels are synchronized
        if (photonView.IsMine)
        {
            LocalPlayerInstance = gameObject;
        }
        // #Critical
        // we flag as don't destroy on load so that instance survives level synchronization, thus giving a seamless experience when levels load.
        DontDestroyOnLoad(gameObject);

        gameObject.tag = "Player";
        PV = gameObject.GetComponent<PhotonView>();

        OnPlayerDeath += DeathAll;        
        OnPlayerReady += ReadyAll;

        //if (PV.IsMine) {
        //    var roundManager = LocalPlayerInstance.GetComponentInChildren<RoundManager>();
        //    roundManager.LocalPlayerAction = LocalPlayerInstance.GetComponent<PlayerAction>();
        //}

        ReadyText.GetComponent<UnityEngine.UI.Text>().text = "Press \"enter\" when you're ready";
    }

    private void Start()
    {
        GameObject Player = GameObject.Find("Player");
        animator = gameObject.GetComponent<PlayerAnimator>();

        InvokeRepeating("Regeneration", 1.0f, 1.0f);

        //RoundManager.RM.AddPlayer(LocalPlayerInstance);
    }

    private void ToggleCanvas(CanvasGroup canvasGroup, bool on) {
        canvasGroup.alpha = on ? 1f : 0f;
        canvasGroup.interactable = on;
        canvasGroup.blocksRaycasts = on;
    }

    // Update is called once per frame
    void Update()
    {
        var hudCanvas = HUD.GetComponent<CanvasGroup>();
        if (!PV.IsMine)
        {
            ToggleCanvas(hudCanvas, false);
            ReadyText.GetComponent<UnityEngine.UI.Text>().text = "";
            return;
        }

        if (Input.GetKeyDown(KeyCode.Return) && !IsDead && !IsReady)
        {
            ReadyText.GetComponent<UnityEngine.UI.Text>().text = "";
            OnPlayerReady();
            return;
        }

        //if (Input.GetKeyDown(KeyCodeController.Ability2))
        //{
        //    RespawnAll();
        //}



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


        //if (unitStat.Health >= 0)
        //{
        //    unitStat.Health -= 10;
        //}



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

        //if (Input.GetKeyDown(KeyCodeController.BasicAttack))
        //{
        //    abilities.SpellKeyCode(KeyCodeController.BasicAttack);
        //}


        if (Input.GetKeyDown(KeyCodeController.Moving) && !abilities.IsFiring && !unitStat.IsDead)
        {
            Move();
        }

        if (Input.GetKeyDown(KeyCodeController.Ability1) && !unitStat.IsDead)
        {
            abilities.SpellKeyCode(KeyCodeController.Ability1);
        }
        else if (Input.GetKeyDown(KeyCodeController.Ability2) && !unitStat.IsDead)
        {
            abilities.SpellKeyCode(KeyCodeController.Ability2);
        }
        else if (Input.GetKeyDown(KeyCodeController.Ability3) && !unitStat.IsDead)
        {
            abilities.SpellKeyCode(KeyCodeController.Ability3);
        }
        else if (Input.GetKeyDown(KeyCodeController.Ability4) && !unitStat.IsDead)
        {
            abilities.SpellKeyCode(KeyCodeController.Ability4);
        }
    }


    public void Move()
    {
        agent.isStopped = false;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit destination;

        // Checks if raycast hit the navmesh (navmesh is a predefined system where the agent can go)
        if (Physics.Raycast(ray, out destination, Mathf.Infinity))
        {
            // MOVE
            // Gets the coordinates of where the mouse clicked and moves the character there
            agent.SetDestination(destination.point);


            // ROTATION
            Quaternion rotationToLook = Quaternion.LookRotation(destination.point - transform.position);

            float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationToLook.eulerAngles.y, ref rotateVelocity, rotateSpeedMovement * (Time.deltaTime * 5));

            transform.eulerAngles = new Vector3(0, rotationY, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        Projectile projectile = other.GetComponent<Projectile>();
        float damage = projectile.damage[0] + projectile.damage[1] + projectile.damage[2]; //----------------------------gasasworebelia---------------
        PhotonView ProjPV = other.GetComponent<PhotonView>();
        if (PV.IsMine && !ProjPV.IsMine)
        {
            PV.RPC("takeDamage", RpcTarget.All, damage);
        }
    }

    void Regeneration()
    {
        float HealthRegen = 0;
        float ManaRegen = 1;

        if ((unitStat.Health + HealthRegen) <= 200)
        {
            unitStat.Health += HealthRegen;
        }

        if ((unitStat.Mana + ManaRegen) <= 200)
        {
            unitStat.Mana += ManaRegen;
        }

    }

    void initStats() {
        unitStat.Health = 200;
        unitStat.Mana = 200;
        unitStat.Xp = 0;
        unitStat.Money = 0;
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
    }

    void ReadyAll()
    {
        PV.RPC("Ready", RpcTarget.All);
    }


    [PunRPC]
    void Ready() {
        IsReady = true;
    }

    void DeathAll() {
        PV.RPC("Death", RpcTarget.All);
    }


    [PunRPC]
    void Death() {
        animator.IsDead();
        unitStat.IsDead = true;
        IsDead = true;
    }

    
    public void RespawnAll()
    {
        if (!PV.IsMine)
            return;

        PV.RPC("Respawn", RpcTarget.All);
    }


    [PunRPC]
    public void Respawn()
    {
        agent.isStopped = true;
        gameObject.transform.position = spawnPoint;


        initStats();
        animator.IsAlive();
    }


    [PunRPC]
    void takeDamage(float damage)
    {
        unitStat.Health -= damage;
    }
}
