using Photon.Pun;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAction : MonoBehaviourPun
{
    //Variables
    private KeyCode[] keyCodes = {
         KeyCode.Alpha0,
         KeyCode.Alpha1,
         KeyCode.Alpha2,
         KeyCode.Alpha3,
         KeyCode.Alpha4,
         KeyCode.Alpha5,
         KeyCode.Alpha6,
         KeyCode.Alpha7,
         KeyCode.Alpha8,
         KeyCode.Alpha9,
     };

    public float rotateSpeedMovement = 0.1f;
    public float rotateVelocity;

    [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
    public static GameObject LocalPlayerInstance;

    #region Referances
    public NavMeshAgent agent;

    [SerializeField]
    private Camera cam;

    public PlayerAnimator animator;

    public Unit unitStat;

    [SerializeField]
    private Abillities abilities;

    [SerializeField]
    private MainMenu menu;

    PhotonView PV;
    #endregion

    void Awake()
    {
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

        unitStat = new Unit
        {
            Health = 1000,
            Mana = 200,
            Xp = 0,
            Money = 0,
            PhysicalDefence = 20,
            MagicDefence = 20,
            Height = 2,
            weight = 80,
            strength = 20,
            Agility = 20,
            Intelligence = 20,
            Charisma = 20,
            IsHalfHealth = false,
            IsDead = false,
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (!PV.IsMine)
        {
            return;
        }


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menu.Counter = menu.Counter + 1;
            menu.OpenMenuInGame(menu.Counter);
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Attack(KeyCode.Mouse0);
        }


        if (Input.GetKeyDown(KeyCode.Mouse1) && !abilities.isFiring)
        {
            Move();
        }

        for (int i = 0; i < keyCodes.Length; i++)
        {
            if (Input.GetKeyDown(keyCodes[i]))
            {
                Attack(keyCodes[i]);
            }
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

    public void Attack(KeyCode key)
    {
        animator.Attack(key);
    }
}
