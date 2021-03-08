using UnityEngine;
using UnityEngine.AI;

public class PlayerAction : MonoBehaviour
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
    
    //Referances
    public NavMeshAgent agent;
    public Camera cam;
    public PlayerAnimator animator;
    public Unit unitStat;

    Abillities abilities;

    private void Start()
    {
        GameObject Player = GameObject.Find("Player");
        abilities = Player.GetComponent<Abillities>();
    }

    // Update is called once per frame
    void Update()
    {

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
