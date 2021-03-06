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

    private void Start()
    {
        InvokeRepeating("Regeneration", 0.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {

        //if (Input.GetKeyDown(KeyCode.Mouse0))
        //{
        //    Attack(KeyCode.Mouse0);
        //}


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
        if (key == KeyCode.Mouse0)
        {
            animator.anim.SetFloat("Blend", 0);
            animator.anim.SetTrigger("BaseAttack");
            unitStat.TStamina -= 0.05f;
        }

        else if (key == KeyCode.Alpha1)
        {
            animator.anim.SetFloat("Blend", 0);
            animator.anim.SetTrigger("Skill1");
            unitStat.TStamina -= 1f;
            unitStat.TMana -= 1f;
        }

        else if (key == KeyCode.Alpha2)
        {
            animator.anim.SetFloat("Blend", 0);
            animator.anim.SetTrigger("Skill2");
            unitStat.TMana -= 5f;
        }
    }


    void Regeneration()
    {
        if (unitStat.THealth <= 100)
        {
            unitStat.THealth += 0.01f;
        }

        if (unitStat.TStamina <= 100)
        {
            unitStat.TStamina += 0.01f;
        }

        if (unitStat.TMana <= 100)
        {
            unitStat.TMana += 0.01f;
        }
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
            float Damage = unitStat.Tstrength / 10 * 2;
            Unit Colobj = collision.gameObject.GetComponent<Unit>();
            Colobj.THealth -= Damage;

            if (Colobj.THealth <= 0)
            {
                Destroy(collision.gameObject);
                unitStat.TXp = Colobj.TXp;
            }


            Debug.Log(Colobj.THealth);
        }
    }
}
