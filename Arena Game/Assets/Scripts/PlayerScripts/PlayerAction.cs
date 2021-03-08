using UnityEngine;
using UnityEngine.AI;

public class PlayerAction : MonoBehaviour,IUnitAction
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
    private string[] Pasiive_Buff_Skills;

    public float rotateSpeedMovement = 0.1f;
    float rotateVelocity;
    
    //Referances
    public NavMeshAgent agent;
    public Camera cam;
    public PlayerAnimator animator;
    public Unit UnitStat;
    public Health PlayerHealth;

    private void Start()
    {

        UnitStat = new Unit()
        {
            Health = 100,
            Mana = 100,
            Money = 1000,

            Height = 2,
            weight = 70,

            strength = 20,
            Agility = 20,
            Intelligence = 20,
            Charisma = 20,

            IsWounded = false,
            IsDead = false,
        };

        PlayerHealth.MaxHealth = UnitStat.Health;

        InvokeRepeating("ChangeStats", 0.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {

        //PlayerHealth.MaxHealth = 100;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Attack(KeyCode.Mouse0);
        }


        if (Input.GetKeyDown(KeyCode.Mouse1))
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

    public void Idle()
    {
        animator.Idle();//Calls Animation
    }

    public void Move()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit destination;

        animator.Move();//Calls Animation

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
        animator.Attack(key);//Calls Animation
    }

    void ChangeStats()
    {
        UnitStat.Health = 90;
        //UnitStat.Mana = 100;
        //UnitStat.Money = 1000;

        //UnitStat.Height = 2;
        //UnitStat.weight = 70;

        //UnitStat.strength = 20;
        UnitStat.Agility = 30;
        //UnitStat.Intelligence = 20;
        //UnitStat.Charisma = 20;

        //UnitStat.IsWounded = false;
        //UnitStat.IsDead = false;

        agent.speed = UnitStat.Agility / 2f;
        Debug.Log(agent.speed);
    }

    void OnCollisionEnter(Collision collision)
    {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (collision.gameObject.name == "MyGameObjectName")
        {
            //If the GameObject's name matches the one you suggest, output this message in the console
            Debug.Log("Do something here");
        }

        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Destructable")
        {
            

        }
    }
}
