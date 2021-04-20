using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Abillities : MonoBehaviour
{
    bool[] ActiveCoolDown = new bool[] { false, false, false, false };//checks if cooldown is started

    bool[] SkillIsAvailable = new bool[] { false, false, false, false };// controls when to activate skill, also checks if you fullfil demands
    public bool[] isFiring { get; set; }
    bool[] isActivating { get; set; }

    private KeyCode _currentAbillityKey;
    private int _currentAbillityIndex;

    #region Referances
    RaycastHit hit;
    Vector3 position;

    PlayerAnimator anim;
    public PlayerAction playerActionScript;

    public Canvas abilityOneCanvas;
    public Image[] SkillImageUIVFX;
    public Transform[] SkillSpawnPoint;


    private Image PlayergroundVFX;
    private Image IndicatorVFX;
    private Image MaxRangeVFX;
    private Image TargetVFX;
    public GameObject PlayergroundVFXGameObject;
    public GameObject IndicatorVFXGameObject;
    public GameObject MaxRangeVFXGameObject;
    public GameObject TargetVFXGameObject;

    public Transform player;
    public Unit unitStat;
    PhotonView PV;

    public Skill Spell;
    #endregion

    void Awake()
    {
        PV = gameObject.GetComponent<PhotonView>();
        PlayergroundVFX = PlayergroundVFXGameObject.GetComponent<Image>();
        IndicatorVFX = IndicatorVFXGameObject.GetComponent<Image>();
        MaxRangeVFX = MaxRangeVFXGameObject.GetComponent<Image>();
        TargetVFX = TargetVFXGameObject.GetComponent<Image>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Spell = SkillLibrary.Skills[playerActionScript.PlayerSkills[0]];

        for (int i = 0; i < SkillImageUIVFX.Length; i++)
        {
            SkillImageUIVFX[i].fillAmount = 0;
        }

        SkillVFXDeActivation();

        isFiring = new bool[] { false, false, false, false };
        isActivating = new bool[] { false, false, false, false };

        playerActionScript = GetComponent<PlayerAction>();
        unitStat = gameObject.GetComponent<Unit>();
        anim = GetComponentInChildren<PlayerAnimator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PV.IsMine)
        {
            return;
        }

        SkillUiImageCooldown();

        AbilityActivation(0);
        AbilityActivation(1);
        AbilityActivation(2);
        AbilityActivation(3);

        targetLocationDirection();
    }

    public void SpellKeyCode(KeyCode key)
    {
        for (int i = 0; i < KeyCodeController.AbilitiesKeyCodeArray.Length; i++)
        {
            if (key == KeyCodeController.AbilitiesKeyCodeArray[i])
            {
                SetSkill(key, i);
            }
        }
        SkillVFXSpriteChange();
    }

    void targetLocationDirection()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
        }

        Quaternion transRot = Quaternion.LookRotation(position - player.transform.position);
        transRot.eulerAngles = new Vector3(0, transRot.eulerAngles.y, transRot.eulerAngles.z);

        abilityOneCanvas.transform.rotation = Quaternion.Lerp(transRot, abilityOneCanvas.transform.rotation, 0f);


        if (Spell.HasTargetVFX)
        {
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                var hitPosDir = (hit.point - transform.position).normalized;
                float distance = (hit.point - transform.position).magnitude;
                distance = Mathf.Min(distance, Spell.Distance);

                var newHitPos = transform.position + hitPosDir * distance;

                var y = newHitPos.y + 0.1f;
                SkillSpawnPoint[2].position = new Vector3(newHitPos.x, y, newHitPos.z);
                TargetVFXGameObject.transform.position = new Vector3(newHitPos.x, 1, newHitPos.z);
            }
        }
    }

    void AbilityActivation(int index)
    {
        if (SkillIsAvailable[index] && !ActiveCoolDown[index])
        {
            SkillVFXActivation();
            isFiring[index] = true;
        }

        if (isFiring[index] && !Input.GetKey(_currentAbillityKey))
        {
            if ((MaxRangeVFX.GetComponent<Image>().enabled || PlayergroundVFX.GetComponent<Image>().enabled))
            {
                if (IndicatorVFX.GetComponent<Image>().enabled)
                {
                    Quaternion rotationToLookAt = Quaternion.LookRotation(position - transform.position);
                    float rotationY = Mathf.SmoothDamp(transform.eulerAngles.y, rotationToLookAt.eulerAngles.y, ref playerActionScript.rotateVelocity, 0);
                    transform.eulerAngles = new Vector3(0, rotationY, 0);
                    playerActionScript.agent.SetDestination(transform.position);
                    playerActionScript.agent.stoppingDistance = 0;
                }

                ActiveCoolDown[index] = true;
                SkillImageUIVFX[index].fillAmount = 1;

                StartCoroutine(corSkillShot(index));
            }
        }

        if (ActiveCoolDown[index])
        {
            SkillVFXDeActivation();
        }
    }



    // Set Spell Data
    void SetSkill(KeyCode key, int index)
    {
        Debug.Log("Something");
        Spell = SkillLibrary.Skills[playerActionScript.PlayerSkills[index]];
        _currentAbillityIndex = index;

        if (CheckIfSkillCanCast(index))
        {
            _currentAbillityKey = key;
            SkillIsAvailable[index] = true;
        }
    }

    //Cooldown Effect
    void SkillUiImageCooldown()
    {
        for (int i = 0; i < SkillImageUIVFX.Length; i++)
        {
            if (ActiveCoolDown[i])
            {
                SkillImageUIVFX[i].fillAmount -= 1 / SkillLibrary.Skills[playerActionScript.PlayerSkills[i]].Cooldown * Time.deltaTime;

                if (SkillImageUIVFX[i].fillAmount <= 0)
                {
                    SkillImageUIVFX[i].fillAmount = 0;
                    ActiveCoolDown[i] = false;
                }
            }
        }
    }

    //changes sprite in unity
    void SkillVFXSpriteChange()
    {
        PlayergroundVFX.sprite = null;
        IndicatorVFX.sprite = null;
        MaxRangeVFX.sprite = null;
        TargetVFX.sprite = null;

        if (Spell.HasPlayergroundVFX)
        {
            PlayergroundVFX.sprite = Spell.PlayergroundVFX;
        }
        if (Spell.HasIndicator)
        {
            IndicatorVFX.sprite = Spell.IndicatorVFX;
        }
        if (Spell.HasMaxRange)
        {
            MaxRangeVFX.sprite = Spell.MaxRangeVFX;
        }
        if (Spell.HasTargetVFX)
        {
            TargetVFX.sprite = Spell.TargetVFX;
        }
    }

    //Makes Skill VFX visible
    void SkillVFXActivation()
    {
        if (Spell.HasPlayergroundVFX)
        {
            PlayergroundVFX.GetComponent<Image>().enabled = true;
        }
        if (Spell.HasIndicator)
        {
            IndicatorVFX.GetComponent<Image>().enabled = true;
        }
        if (Spell.HasMaxRange)
        {
            MaxRangeVFX.GetComponent<Image>().enabled = true;
            MaxRangeVFXGameObject.transform.localScale = new Vector3(Spell.Distance - 3, Spell.Distance - 3, Spell.Distance - 3);
        }
        if (Spell.HasTargetVFX)
        {
            TargetVFX.GetComponent<Image>().enabled = true;
        }
    }

    //Hides Skill VFX
    void SkillVFXDeActivation()
    {
        IndicatorVFX.GetComponent<Image>().enabled = false;
        PlayergroundVFX.GetComponent<Image>().enabled = false;
        MaxRangeVFX.GetComponent<Image>().enabled = false;
        TargetVFX.GetComponent<Image>().enabled = false;
    }

    IEnumerator corSkillShot(int index)
    {
        if (!isActivating[index])
        {
            if (SkillConsumption(index))
            {
                isActivating[index] = true;
                playerActionScript.agent.isStopped = true;
                anim.Attack(Spell.AnimatorProperty);
                yield return new WaitForSeconds(Spell.ActivationTime);
                SpawnSkill();
                isFiring[index] = false;
                SkillIsAvailable[index] = false;
                isActivating[index] = false;
            }
        }
    }

    public void SpawnSkill()
    {

        if (PV.IsMine && Spell.Skill3DModel != null)
        {
            if (!Spell.IsBuff)
            {
                if (Spell.HasIndicator)
                {
                    PhotonNetwork.Instantiate("Prefabs/Skill/" + Spell.Skill3DModel, SkillSpawnPoint[0].transform.position, SkillSpawnPoint[0].transform.rotation);
                }
                else if (Spell.HasTargetVFX)
                {
                    PhotonNetwork.Instantiate("Prefabs/Skill/" + Spell.Skill3DModel, SkillSpawnPoint[2].transform.position, SkillSpawnPoint[2].transform.rotation);
                }
                else if (Spell.HasPlayergroundVFX || Spell.HasMaxRange)
                {
                    PhotonNetwork.Instantiate("Prefabs/Skill/" + Spell.Skill3DModel, SkillSpawnPoint[1].transform.position, SkillSpawnPoint[1].transform.rotation);
                }
                else
                {
                    PhotonNetwork.Instantiate("Prefabs/Skill/" + Spell.Skill3DModel, SkillSpawnPoint[1].transform.position, SkillSpawnPoint[1].transform.rotation);
                }
            }
        }
    }

    bool CheckIfSkillCanCast(int index)
    {
        bool value = false;

        if (Spell.ManaConsumption <= unitStat.Mana || (Spell.HealthConsumption != 0 && Spell.HealthConsumption <= unitStat.Health))
        {
            SkillIsAvailable[index] = false;
            value = true;
        }

        return value;

    }

    bool SkillConsumption(int index)
    {
        bool value = false;

        if (SkillIsAvailable[index])
        {
            unitStat.Mana -= Spell.ManaConsumption;
            unitStat.Health -= Spell.HealthConsumption;
            value = true;
        }

        return value;
    }
}