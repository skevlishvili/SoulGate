using Mirror;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class Abillities : NetworkBehaviour
{
    //bool[] ActiveCoolDown = new bool[] { false, false, false, false };//checks if cooldown is started

    public List<Abillity> PlayerAbillities;
    public List<Abillity> PlayerPassives;

    /*bool[] SkillIsAvailable = new bool[] { false, false, false, false };*/// controls when to activate skill, also checks if you fullfil demands
    //public bool[] isFiring;
    //bool[] isActivating;

    private int CurrentAbillity;

    private RoundManager roundManager;
    #region Referances
    RaycastHit hit;
    Vector3 position;

    PlayerAnimator anim;

    public Canvas abilityOneCanvas;
    public Image[] SkillImageUIVFX;
    public Transform[] SkillSpawnPoint;


    private Image PlayergroundVFX;
    private Image IndicatorVFX;
    private Image MaxRangeVFX;
    private Image TargetVFX;
    private Image BurstVFX;
    public GameObject PlayergroundVFXGameObject;
    public GameObject IndicatorVFXGameObject;
    public GameObject MaxRangeVFXGameObject;
    public GameObject TargetVFXGameObject;
    public GameObject BurstVFXGameObject;

    public Transform player;
    public Unit unitStat;
    private ChatBehaviour chat;
    [SerializeField]
    private NavMeshAgent _agent;
    #endregion

    void Awake()
    {
        PlayergroundVFX = PlayergroundVFXGameObject.GetComponent<Image>();
        IndicatorVFX = IndicatorVFXGameObject.GetComponent<Image>();
        MaxRangeVFX = MaxRangeVFXGameObject.GetComponent<Image>();
        TargetVFX = TargetVFXGameObject.GetComponent<Image>();
        BurstVFX = BurstVFXGameObject.GetComponent<Image>();


        PlayerAbillities = new List<Abillity>();
        PlayerAbillities.Add(new Abillity { ActiveCoolDown = false, KeyCode = KeyCodeController.AbilitiesKeyCodeArray[0], Skill = SkillLibrary.Skills[1], IsFiring = false, IsActivating = false });
        PlayerAbillities.Add(new Abillity { ActiveCoolDown = false, KeyCode = KeyCodeController.AbilitiesKeyCodeArray[1], Skill = SkillLibrary.Skills[9], IsFiring = false, IsActivating = false });
        PlayerAbillities.Add(new Abillity { ActiveCoolDown = false, KeyCode = KeyCodeController.AbilitiesKeyCodeArray[2], Skill = SkillLibrary.Skills[16], IsFiring = false, IsActivating = false });
        PlayerAbillities.Add(new Abillity { ActiveCoolDown = false, KeyCode = KeyCodeController.AbilitiesKeyCodeArray[3], Skill = SkillLibrary.Skills[19], IsFiring = false, IsActivating = false });

        PlayerPassives = new List<Abillity>();
        PlayerPassives.Add(new Abillity { ActiveCoolDown = false, KeyCode = KeyCode.None, Skill = null, IsFiring = false, IsActivating = false });
        PlayerPassives.Add(new Abillity { ActiveCoolDown = false, KeyCode = KeyCode.None, Skill = null, IsFiring = false, IsActivating = false });
        PlayerPassives.Add(new Abillity { ActiveCoolDown = false, KeyCode = KeyCode.None, Skill = null, IsFiring = false, IsActivating = false });
        PlayerPassives.Add(new Abillity { ActiveCoolDown = false, KeyCode = KeyCode.None, Skill = null, IsFiring = false, IsActivating = false });
        PlayerPassives.Add(new Abillity { ActiveCoolDown = false, KeyCode = KeyCode.None, Skill = null, IsFiring = false, IsActivating = false });
    }

    // Start is called before the first frame update
    void Start()
    {
        // SkillLibrary.Skills[playerActionScript.PlayerSkills[0]];

        var roundMangerObjs = GameObject.FindGameObjectsWithTag("RoundManager");
        if (roundMangerObjs.Length > 0 && roundMangerObjs[0] != null)
        {
            roundManager = roundMangerObjs[0].GetComponent<RoundManager>();
        }

        for (int i = 0; i < SkillImageUIVFX.Length; i++)
        {
            SkillImageUIVFX[i].fillAmount = 0;
        }

        SkillVFXDeActivation();


        unitStat = gameObject.GetComponent<Unit>();
        anim = GetComponentInChildren<PlayerAnimator>();
        chat = GetComponent<ChatBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer || unitStat.IsDead || !unitStat.IsReady || chat.IsTyping || roundManager.CurrentState != RoundManager.RoundState.RoundStart)
            return;


        SkillUiImageCooldown();
        bool skillIsActivating = PlayerAbillities.Any(x => x.IsActivating);
        bool skillIsFiring = PlayerAbillities.Any(x => x.IsFiring);


        if (!skillIsActivating && !skillIsFiring) { 
            for (int i = 0; i < 4; i++)
            {
                if (Input.GetKey(PlayerAbillities[i].KeyCode))
                {
                    AbilityActivation(i);
                    break;
                }
            }
        } else if(skillIsActivating && !skillIsFiring) {          
            if (Input.GetKeyUp((PlayerAbillities[CurrentAbillity].KeyCode)))
            {
                AbilityFire();
            }
        }



        targetLocationDirection();

        //CheckFiring();

        if (isServer)
        {
            ApplyPassives();
        }
    }

    private void ApplyPassives()
    {
        foreach (var item in PlayerPassives)
        {
            if (item.Skill != null)
            {
                item.Skill.Passive(item.Skill, player.gameObject);
            }
        }

        PassiveSkillsfunctionality.UnitStatsPassiveSum = new UnitStruct();
    }


    //void CheckFiring()
    //{
    //    KeyCode? key = KeyCodeController.AbilitiesKeyCodeArray.FirstOrDefault(x => Input.GetKeyDown(x));

    //    if (key != null)
    //    {
    //        SpellKeyCode(key.Value);
    //    }
    //}

    //public void SpellKeyCode(KeyCode key)
    //{
    //    for (int i = 0; i < KeyCodeController.AbilitiesKeyCodeArray.Length; i++)
    //    {
    //        if (key == KeyCodeController.AbilitiesKeyCodeArray[i] && CanCast(i))
    //        {
    //            CurrentAbillity = i;
    //        }
    //    }

    //    SkillVFXSpriteChange();
    //}

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


        if (PlayerAbillities[CurrentAbillity].Skill.HasTargetVFX)
        {
            RaycastHit hit;
           
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                var hitPosDir = (hit.point - transform.position).normalized;
                float distance = (hit.point - transform.position).magnitude;
                distance = Mathf.Min(distance, PlayerAbillities[CurrentAbillity].Skill.Distance);

                var newHitPos = transform.position + hitPosDir * distance;

                SkillSpawnPoint[2].position = new Vector3(newHitPos.x, 0.5f, newHitPos.z);
                TargetVFXGameObject.transform.position = new Vector3(newHitPos.x, 0.5f, newHitPos.z);
            }

        }


        if (IndicatorVFX.enabled || BurstVFX.enabled)
        {
            RotatePlayer();
        }
    }

    void AbilityActivation(int index)
    {
        CurrentAbillity = index;
        SkillVFXSpriteChange();

        if (!PlayerAbillities[index].ActiveCoolDown)
        {
            SkillVFXActivation();
            PlayerAbillities[index].IsActivating = true;
           
        }
    }


    void AbilityFire() {
        PlayerAbillities[CurrentAbillity].ActiveCoolDown = true;
        SkillImageUIVFX[CurrentAbillity].fillAmount = 1;

        StartCoroutine(corSkillShot(CurrentAbillity));
    }

    void RotatePlayer()
    {
        Quaternion rotationToLookAt = Quaternion.LookRotation(position - transform.position);
        float rotateVelocity = 0;
        float rotationY = Mathf.SmoothDamp(transform.eulerAngles.y, rotationToLookAt.eulerAngles.y, ref rotateVelocity, 0);
        transform.eulerAngles = new Vector3(0, rotationY, 0);
        _agent.SetDestination(transform.position);
        _agent.stoppingDistance = 0;
    }



    //Cooldown Effect
    void SkillUiImageCooldown()
    {
        for (int i = 0; i < SkillImageUIVFX.Length; i++)
        {
            if (PlayerAbillities[i].ActiveCoolDown)
            {
                var cooldown = PlayerAbillities[i].Skill.Cooldown - PlayerAbillities[i].Skill.Cooldown * unitStat.AbilityCooldown;
                
                //TODO change
                SkillImageUIVFX[i].fillAmount -= 1 / cooldown * Time.deltaTime;

                if (SkillImageUIVFX[i].fillAmount <= 0)
                {
                    SkillImageUIVFX[i].fillAmount = 0;
                    PlayerAbillities[i].ActiveCoolDown = false;
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
        BurstVFX.sprite = null;

        if (PlayerAbillities[CurrentAbillity].Skill.HasPlayergroundVFX)
        {
            PlayergroundVFX.sprite = PlayerAbillities[CurrentAbillity].Skill.PlayergroundVFX;
        }
        if (PlayerAbillities[CurrentAbillity].Skill.HasIndicator)
        {
            IndicatorVFX.sprite = PlayerAbillities[CurrentAbillity].Skill.IndicatorVFX;
        }
        if (PlayerAbillities[CurrentAbillity].Skill.HasMaxRange)
        {
            MaxRangeVFX.sprite = PlayerAbillities[CurrentAbillity].Skill.MaxRangeVFX;
        }
        if (PlayerAbillities[CurrentAbillity].Skill.HasTargetVFX)
        {
            TargetVFX.sprite = PlayerAbillities[CurrentAbillity].Skill.TargetVFX;
        }
        if (PlayerAbillities[CurrentAbillity].Skill.HasBurstVFX)
        {
            BurstVFX.sprite = PlayerAbillities[CurrentAbillity].Skill.BurstVFX;
        }
    }

    //Makes Skill VFX visible
    void SkillVFXActivation()
    {
        PlayergroundVFX.enabled = PlayerAbillities[CurrentAbillity].Skill.HasPlayergroundVFX;
        IndicatorVFX.enabled = PlayerAbillities[CurrentAbillity].Skill.HasIndicator;
        
        MaxRangeVFX.enabled = PlayerAbillities[CurrentAbillity].Skill.HasMaxRange;
        MaxRangeVFXGameObject.transform.localScale = new Vector3(PlayerAbillities[CurrentAbillity].Skill.Distance, PlayerAbillities[CurrentAbillity].Skill.Distance, PlayerAbillities[CurrentAbillity].Skill.Distance);
        
        TargetVFX.enabled = PlayerAbillities[CurrentAbillity].Skill.HasTargetVFX;
        TargetVFXGameObject.transform.localScale = new Vector3(PlayerAbillities[CurrentAbillity].Skill.AttackRadius, PlayerAbillities[CurrentAbillity].Skill.AttackRadius, PlayerAbillities[CurrentAbillity].Skill.AttackRadius);
        
        BurstVFX.enabled = PlayerAbillities[CurrentAbillity].Skill.HasBurstVFX;
    }

    //Hides Skill VFX
    void SkillVFXDeActivation()
    {
        IndicatorVFX.enabled = false;
        PlayergroundVFX.enabled = false;
        MaxRangeVFX.enabled = false;
        TargetVFX.enabled = false;
        BurstVFX.enabled = false;
    }

    IEnumerator corSkillShot(int index)
    {
        if (PlayerAbillities[index].IsActivating && SkillConsumption(index))
        {
            SkillVFXDeActivation();
            PlayerAbillities[index].IsActivating = false;
            PlayerAbillities[index].IsFiring = true;
            _agent.isStopped = true;
            anim.Attack(PlayerAbillities[CurrentAbillity].Skill.AnimatorProperty);

            yield return new WaitForSeconds(PlayerAbillities[CurrentAbillity].Skill.ActivationTime);


            SpawnSkill();
            PlayerAbillities[index].IsFiring = false;
            PlayerAbillities[index].IsActivating = false;
            anim.StopAttack(PlayerAbillities[CurrentAbillity].Skill.AnimatorProperty);
            //SkillIsAvailable[index] = false;
        }
    }

    [Client]
    public void SpawnSkill()
    {
        if (PlayerAbillities[CurrentAbillity].Skill.Skill3DModel == null)
            return;

        var prefabSrc = PlayerAbillities[CurrentAbillity].Skill.Skill3DModel;
        var position = new Vector3();
        var rotation = new Quaternion();


        var spellType = PlayerAbillities[CurrentAbillity].Skill.IsBuff ? 1 :
                        PlayerAbillities[CurrentAbillity].Skill.HasIndicator ? 0 :
                        PlayerAbillities[CurrentAbillity].Skill.HasTargetVFX ? 2 : 1;

        position = SkillSpawnPoint[spellType].transform.position;
        rotation = SkillSpawnPoint[spellType].transform.rotation;


        //GameObject.Instantiate(Resources.Load(prefabSrc), position, rotation);

        SpawnProj(prefabSrc, position, rotation);
        //CmdSpawnSkill(prefabSrc, position, rotation);
    }


    [Command]//sachiroa projectile Scriptshi HIt da Flash instiatingis dros ----------------------- 
    private void CmdSpawnSkill(string prefabSrc, Vector3 position, Quaternion rotation)
    {
        var projectile = (GameObject)GameObject.Instantiate(Resources.Load(prefabSrc), position, rotation);
        var projScript = projectile.GetComponent<Projectile>();
        if (projScript == null)
            return;
        projectile.GetComponent<Projectile>().player = gameObject;
        NetworkServer.Spawn(projectile, gameObject);
    }

    bool CanCast(int index)
    {
        return PlayerAbillities[index].ActiveCoolDown && unitStat.Health > PlayerAbillities[CurrentAbillity].Skill.HealthConsumption && !unitStat.IsDead;
    }

    bool SkillConsumption(int index)
    {
        bool value = false;

        //if (SkillIsAvailable[index])
        //{
        //unitStat.Mana -= PlayerAbillities[CurrentAbillity].Skill.ManaConsumption;
        //unitStat.Health -= PlayerAbillities[CurrentAbillity].Skill.HealthConsumption;
        value = true;
        //}

        return value;
    }


    public void ChangeActiveSkill(Skill skill, int index) {
        PlayerAbillities[index].Skill = skill;
        HudContentController hudConetnt = gameObject.GetComponentInChildren<HudContentController>();
        hudConetnt.LoadSkillUiImagesInHUD();
    }


    public void ChangePassiveSkill(Skill skill, int index)
    {
        PlayerPassives[index].Skill = skill;
        HudContentController hudConetnt = gameObject.GetComponentInChildren<HudContentController>();
        hudConetnt.LoadPassiveSkillUiImagesInHUD();
    }

    [Client]
    private void SpawnProj(string prefabSrc, Vector3 position, Quaternion rotation)
    {
        if (!base.hasAuthority)
            return;



        /* Only show locally if not server.
         * This is to prevent duplicate projectiles
         * as client host. */
        if (!base.isServer)
        {
            //var projectile = (GameObject)GameObject.Instantiate(Resources.Load(prefabSrc), position, rotation);
            //Spawn the projectile locally.
            GameObject obj = (GameObject)Instantiate(Resources.Load(prefabSrc), position, rotation);
            Projectile p = obj.GetComponent<Projectile>();
            if (p == null)
                return;
            p.player = gameObject;
            //Initialize with a 0f catch up duration.
            p.Initialize(0f);
        }

        /* Ask server to fire a projectile using
         * this transforms position, and current
         * network time. Since network time is synchronized
         * it can be used to determine how long it took 
         * the command to reach the server.  */
        SpawnProjCmd(prefabSrc, transform.position, rotation, NetworkTime.time, gameObject);
    }

    /// <summary>
    /// Fires over the network.
    /// </summary>
    [Command]
    private void SpawnProjCmd(string prefabSrc, Vector3 position, Quaternion rotation, double networkTime, GameObject playerGameObject)
    {
        /* Determine how much time has passed between when the client
         * called the command and when it was received. */
        double timePassed = NetworkTime.time - networkTime;

        /* Instantiate the projectile on the server so that it may
         * register collision, and continue to act as server authoritive.
         * It's important to instantiate at the position passed in so that 
         * the projectile is spawned in the same position on all clients, regardless
         * of their appeared position. Visually the result would be about the same as
         * if spectators were the owner themself. */
        //Initialize the projectile with a catchup value of time passed.
        //var projectile = (GameObject)GameObject.Instantiate(Resources.Load(prefabSrc), position, rotation);
        //Spawn the projectile locally.
        GameObject obj = (GameObject)Instantiate(Resources.Load(prefabSrc), position, rotation);
        Projectile p = obj.GetComponent<Projectile>();
        p.player = playerGameObject;
        p.Initialize((float)timePassed);

        //Fire on other clients using the same data.
        SpawnProjRpc(prefabSrc, position, rotation, networkTime, playerGameObject);
    }


    [ClientRpc]
    private void SpawnProjRpc(string prefabSrc, Vector3 position, Quaternion rotation, double networkTime, GameObject playerGameObject)
    {
        //If has authority ignore this, already fired locally.
        if (base.hasAuthority)
            return;

        //var projectile = (GameObject)GameObject.Instantiate(Resources.Load(prefabSrc), position, rotation);

        /* Like in the command you will get the time passed and initialize
         * the projectile with that value. */
        double timePassed = NetworkTime.time - networkTime;
        GameObject obj = (GameObject)Instantiate(Resources.Load(prefabSrc), position, rotation);
        Projectile p = obj.GetComponent<Projectile>();
        p.player = playerGameObject;
        p.Initialize((float)timePassed);
    }
}