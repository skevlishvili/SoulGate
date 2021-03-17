using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Abillities : MonoBehaviour
{
    bool isCoolDown = false;
    public bool canSkillshot = true;    
    public bool isFiring {get; private set;}

    private KeyCode _currentAbility;

    #region Referances
    RaycastHit hit;
    Vector3 position;

    PlayerAnimator anim;
    PlayerAction playerActionScript;
    public Transform AbiilitySpawnPoint;

    public Canvas abilityOneCanvas;
    public Image SkillImageUIVFX;


    public Image PlayergroundVFX;
    public Image IndicatorVFX;
    public Image MaxRangeVFX;

    public Transform player;
    PhotonView PV;
    Skill Spell;
    #endregion

    void Awake()
    {
        PV = gameObject.GetComponent<PhotonView>();
    }

    // Start is called before the first frame update
    void Start()
    {
        SkillImageUIVFX.fillAmount = 0;
        PlayergroundVFX.GetComponent<Image>().enabled = false;
        IndicatorVFX.GetComponent<Image>().enabled = false;

        isFiring = false;

        playerActionScript = GetComponent<PlayerAction>();
        anim = GetComponentInChildren<PlayerAnimator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PV.IsMine)
        {
            return;
        }

        AbilityOne();
        targetLocation();
    }

    public void SpellKeyCode(KeyCode key)
    {
        if (key == KeyCodeController.Ability1)
        {
            Spell = SkillLibrary.Skills[0];
            //SkillImageUIVFX.sprite = Resources.Load<Sprite>("Fireball");

            //PlayergroundVFX.sprite = null;
            //IndicatorVFX.sprite = null;
            //MaxRangeVFX.sprite = null;

            //if (Spell.HasPlayergroundVFX)
            //{
            //    PlayergroundVFX.sprite = Resources.Load<Sprite>("FireCircle");
            //}
            //if (Spell.HasIndicator)
            //{
            //    IndicatorVFX.sprite = Spell.IndicatorVFX;
            //}

            //if (Spell.HasMaxRange)
            //{
            //    MaxRangeVFX.sprite = Spell.MaxRangeVFX;
            //}


            //if (Spell.IsBuff)
            //{

            //}

            //if (Spell.IsInvisible)
            //{

            //}

            //if (Spell.IsProjectile)
            //{

            //}

            //if (Spell.IsRecharged)
            //{

            //}

            //if (Spell.IsRestraining)
            //{

            //}
        }
        else if (key == KeyCodeController.Ability2)
        {
            Spell = SkillLibrary.Skills[1];
        }
        else if (key == KeyCodeController.Ability3)
        {
            Spell = SkillLibrary.Skills[2];
        }
        else if (key == KeyCodeController.Ability4)
        {
            Spell = SkillLibrary.Skills[3];
        }
    }

    void targetLocation()
    {
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
        if (Input.GetKey(KeyCodeController.Ability1) && !isCoolDown)
        {
            IndicatorVFX.GetComponent<Image>().enabled = true;
            PlayergroundVFX.GetComponent<Image>().enabled = true;
            isFiring = true;
            _currentAbility = KeyCodeController.Ability1;
        } else {
            _currentAbility = KeyCode.None;
        }

        if (IndicatorVFX.GetComponent<Image>().enabled && isFiring && !Input.GetKey(_currentAbility))
        {
            Quaternion rotationToLookAt = Quaternion.LookRotation(position - transform.position);
            
            float rotationY = Mathf.SmoothDamp(transform.eulerAngles.y, rotationToLookAt.eulerAngles.y, ref playerActionScript.rotateVelocity, 0);

            transform.eulerAngles = new Vector3(0, rotationY, 0);

            playerActionScript.agent.SetDestination(transform.position);
            playerActionScript.agent.stoppingDistance = 0;


            if (canSkillshot)
            {
                isCoolDown = true;
                SkillImageUIVFX.fillAmount = 1;

                StartCoroutine(corSkillShot());                
            }
        }

        if (isCoolDown)
        {
            SkillImageUIVFX.fillAmount -= 1 / Spell.Cooldown * Time.deltaTime;
            IndicatorVFX.GetComponent<Image>().enabled = false;
            PlayergroundVFX.GetComponent<Image>().enabled = false;

            if (SkillImageUIVFX.fillAmount <= 0)
            {
                SkillImageUIVFX.fillAmount = 0;
                isCoolDown = false;
            }
        }
    }

    

    IEnumerator corSkillShot()
    {
        canSkillshot = false;
        anim.Attack(Spell.AnimatorProperty);
        SpawnSkill();
        canSkillshot = true;
        StopFiring();

        yield return new WaitForSeconds(1.5f);
    }

    public void StopFiring()
    {
        isFiring = false;
    }

    public void SpawnSkill()
    {

        if (PV.IsMine)
        {
            PhotonNetwork.Instantiate("Prefabs/"+ Spell.Skill3DModel.name, AbiilitySpawnPoint.transform.position, AbiilitySpawnPoint.transform.rotation);
        }
    }

    public void PlayFireSound()
    {
        SoundManagerScript sound = GameObject.Find("SoundManager").GetComponent<SoundManagerScript>();
        sound.PlaySound(Spell.Sound);
    }

    public void EndOfSkill()
    {
        Debug.Log(Spell.AnimatorProperty);
        anim.EndAnimation(Spell.AnimatorProperty);
        StopFiring();
    }
}
