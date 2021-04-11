using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class PlayerAction : MonoBehaviourPun
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

    [SerializeField]
    private Abillities abilities;

    PhotonView PV;

    public Canvas HUD;
    #endregion



    void Awake()
    {
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

        PlayerSkills = new int[4] { 1, 6, 2, 5 };

        // #Important
        // used in GameManager.cs: we keep track of the localPlayer instance to prevent instantiation when levels are synchronized
        if (photonView.IsMine)
        {
            LocalPlayerInstance = this.gameObject;
        }
        // #Critical
        // we flag as don't destroy on load so that instance survives level synchronization, thus giving a seamless experience when levels load.
        DontDestroyOnLoad(this.gameObject);

        PV = gameObject.GetComponent<PhotonView>();
    }

    private void Start()
    {
        GameObject Player = GameObject.Find("Player");
        animator = gameObject.GetComponent<PlayerAnimator>();

        InvokeRepeating("Regeneration", 1.0f, 1.0f);

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
            return;
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

        //if (Input.GetKeyDown(KeyCodeController.BasicAttack))
        //{
        //    abilities.SpellKeyCode(KeyCodeController.BasicAttack);
        //}


        if (Input.GetKeyDown(KeyCodeController.Moving) && !abilities.isFiring && !unitStat.IsDead)
        {
            agent.isStopped = false;
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
        float damage = projectile.damage[0] + projectile.damage[1] + projectile.damage[2];//----------------------------gasasworebelia---------------
        PhotonView ProjPV = other.GetComponent<PhotonView>();
        if (PV.IsMine && !ProjPV.IsMine)
        {
            PV.RPC("takeDamage", RpcTarget.All, damage);
        }
    }

    void Regeneration()
    {
        float HealthRegen = 1;
        float ManaRegen = 1;

        if((unitStat.Health + HealthRegen) <= 200){
            unitStat.Health += HealthRegen;
        }

        if ((unitStat.Mana + ManaRegen) <= 200)
        {
            unitStat.Mana += ManaRegen;
        }

    }

    [PunRPC]
    void takeDamage(float damage)
    {
        unitStat.Health -= damage;
    }
}
