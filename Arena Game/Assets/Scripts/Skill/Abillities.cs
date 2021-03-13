using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Abillities : MonoBehaviour
{
    #region Referances 
    public SkillLibrary Skills;
    public Transform AbilitySpawnPoint;
    public Transform player;
    Animator anim;
    PlayerAction playerActionScript;
    PhotonView PV;
    #endregion


    #region Variables
    RaycastHit hit;
    Vector3 position;


    Skill _currentAbility;

    Image SkillImageUIVFX;
    Canvas abilityOneCanvas;
    Image PlayergroundVFX;
    Image MaxRangeVFX;
    Image IndicatorVFX;
    GameObject AbilityPrefab;


    bool StartCoolDown = false;//checks when to start Cooldown
    public bool isFiring { get; private set; }

    private KeyCode _currentAbilityKeycode;
    #endregion



    public bool canSkillshot = true;
    public KeyCode abilityKeycode;
        
    

    void Awake()
    {
        PV = gameObject.GetComponent<PhotonView>();
        Skills = gameObject.GetComponentInChildren<SkillLibrary>();
                                                               
        Debug.Log($"{Skills.FireBall} this is Damage from Magic Attack");

        _currentAbility = Skills.FireBall;

        SkillImageUIVFX.sprite = _currentAbility.SkillImageUIVFX;
        PlayergroundVFX.sprite = _currentAbility.PlayergroundVFX;
        MaxRangeVFX.sprite = _currentAbility.MaxRangeVFX;
        IndicatorVFX.sprite = _currentAbility.IndicatorVFX;
                          
    }

    // Start is called before the first frame update
    void Start()
    {
        SkillImageUIVFX.fillAmount = 0;

        PlayergroundVFX.GetComponent<Image>().enabled = false;
        IndicatorVFX.GetComponent<Image>().enabled = false;
        isFiring = false;


        playerActionScript = GetComponent<PlayerAction>();
        anim = GetComponentInChildren<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

        if (!PV.IsMine)
        {
            Debug.Log("It's Mine");
            return;
        }

        AbilityActivation();
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
        }

        Quaternion transRot = Quaternion.LookRotation(position - player.transform.position);
        transRot.eulerAngles = new Vector3(0, transRot.eulerAngles.y, transRot.eulerAngles.z);

        abilityOneCanvas.transform.rotation = Quaternion.Lerp(transRot, abilityOneCanvas.transform.rotation, 0f);
    }



    void AbilityActivation()
    {

        if (!_currentAbility.IsPasive)
        {
            if (_currentAbility.HasIndicator && _currentAbility.HasPlayergroundVFX)
            {
                if (Input.GetKey(abilityKeycode) && !StartCoolDown)
                {
                    IndicatorVFX.GetComponent<Image>().enabled = true;
                    PlayergroundVFX.GetComponent<Image>().enabled = true;
                    isFiring = true;
                    _currentAbilityKeycode = abilityKeycode;
                }
                else
                {
                    _currentAbilityKeycode = KeyCode.None;
                }



                if (IndicatorVFX.GetComponent<Image>().enabled && isFiring && !Input.GetKey(_currentAbilityKeycode))
                {
                    Quaternion rotationToLookAt = Quaternion.LookRotation(position - transform.position);

                    float rotationY = Mathf.SmoothDamp(transform.eulerAngles.y, rotationToLookAt.eulerAngles.y, ref playerActionScript.rotateVelocity, 0);

                    transform.eulerAngles = new Vector3(0, rotationY, 0);

                    playerActionScript.agent.SetDestination(transform.position);
                    playerActionScript.agent.stoppingDistance = 0;


                    if (canSkillshot)
                    {
                        StartCoolDown = true;
                        SkillImageUIVFX.fillAmount = 1;

                        StartCoroutine(corSkillShot());
                    }
                }

                if (StartCoolDown)
                {
                    CooldownFun(_currentAbility.Cooldown);
                }
            }
            else if (_currentAbility.HasMaxRange && _currentAbility.HasIndicator)
            {

            }
        }
        else if (_currentAbility.HasIndicator)
        {

        }
    }

    public void CooldownFun(float cooldowntime)
    {
        SkillImageUIVFX.fillAmount -= 1 / cooldowntime * Time.deltaTime;
        IndicatorVFX.GetComponent<Image>().enabled = false;
        PlayergroundVFX.GetComponent<Image>().enabled = false;

        if (SkillImageUIVFX.fillAmount <= 0)
        {
            SkillImageUIVFX.fillAmount = 0;
            StartCoolDown = false;
        }
    }

    public void StopFiring(){
        isFiring = false;
    }

    IEnumerator corSkillShot()
    {
        canSkillshot = false;
        anim.SetBool("SkillOne", true);
        canSkillshot = true;

        yield return new WaitForSeconds(1.5f);
    }


}
