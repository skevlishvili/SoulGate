using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Abillities : MonoBehaviour
{
    RaycastHit hit;
    Animator anim;
    PlayerAction playerActionScript;

    [Header("Skillshot Ability")]
    public Image abilityImageOne;
    public float cooldownOne = 5;
    bool isCoolDown = false;
    public bool canSkillshot = true;
    public KeyCode abilityOne;

    public GameObject projPrefab;

    public Transform projSpawnPoint;

    public bool isFiring {get; private set;}

    private KeyCode _currentAbility;

    [Header("Ability Inputs")]
    // AbilityOne Input Variables
    Vector3 position;
    public Canvas abilityOneCanvas;
    public Image targetCircle;
    public Image skillShot;
    public Transform player;

    PhotonView PV;

    void Awake()
    {
        PV = gameObject.GetComponent<PhotonView>();
    }

    // Start is called before the first frame update
    void Start()
    {

        abilityImageOne.fillAmount = 0;

        targetCircle.GetComponent<Image>().enabled = false;
        skillShot.GetComponent<Image>().enabled = false;
        isFiring = false;


        playerActionScript = GetComponent<PlayerAction>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {



        if (!PV.IsMine)
        {
            return;
        }
        


        AbilityOne();
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
        }

        Quaternion transRot = Quaternion.LookRotation(position - player.transform.position);
        transRot.eulerAngles = new Vector3(0, transRot.eulerAngles.y, transRot.eulerAngles.z);

        abilityOneCanvas.transform.rotation = Quaternion.Lerp(transRot, abilityOneCanvas.transform.rotation, 0f);
    }



    void AbilityOne()
    {       

        if (Input.GetKey(abilityOne) && !isCoolDown)
        {
            skillShot.GetComponent<Image>().enabled = true;
            targetCircle.GetComponent<Image>().enabled = true;
            isFiring = true;
            _currentAbility = abilityOne;
        } else {
            _currentAbility = KeyCode.None;
        }

        

        if (skillShot.GetComponent<Image>().enabled && isFiring && !Input.GetKey(_currentAbility))
        {
            Quaternion rotationToLookAt = Quaternion.LookRotation(position - transform.position);
            
            float rotationY = Mathf.SmoothDamp(transform.eulerAngles.y, rotationToLookAt.eulerAngles.y, ref playerActionScript.rotateVelocity, 0);

            transform.eulerAngles = new Vector3(0, rotationY, 0);

            playerActionScript.agent.SetDestination(transform.position);
            playerActionScript.agent.stoppingDistance = 0;


            if (canSkillshot)
            {
                isCoolDown = true;
                abilityImageOne.fillAmount = 1;

                StartCoroutine(corSkillShot());                
            }
        }

        if (isCoolDown)
        {
            abilityImageOne.fillAmount -= 1 / cooldownOne * Time.deltaTime;
            skillShot.GetComponent<Image>().enabled = false;
            targetCircle.GetComponent<Image>().enabled = false;

            if (abilityImageOne.fillAmount <= 0)
            {
                abilityImageOne.fillAmount = 0;
                isCoolDown = false;
            }
        }
    }

    public void StopFiring(){
        isFiring = false;
    }

    IEnumerator corSkillShot()
    {

        canSkillshot = false;
        AbilityHelper shoot = GetComponent<AbilityHelper>();

        anim.SetBool("SkillOne", true);
        shoot.SpawnSkill();
        canSkillshot = true;
        StopFiring();

        yield return new WaitForSeconds(1.5f);
    }
}
