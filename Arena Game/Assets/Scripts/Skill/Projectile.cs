using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Mirror;

public class Projectile : MonoBehaviour
{
    Skill Spell;

    public int SkillIndex;
    public int TowerSkillIndex;
    public List<float> damage = new List<float>();
    public float speed;

    public GameObject hit;
    public GameObject flash;
    private Rigidbody rb;
    public GameObject player;


    //-----Audio------------------
    GameObject AudioManagerObj;
    public bool AudioRepeating = false;
    public float AudioStartTime = 0.0f;
    public float AudioRepeatTime = 1f;

    

    //Useless parts
    public bool UseFirePointRotation;
    public float hitOffset = 0f;
    public Vector3 rotationOffset = new Vector3(0, 0, 0);
    public GameObject[] Detached;



    #region Serialized.
    /// <summary>
    /// How quickly to move.
    /// </summary>
    [Tooltip("How quickly to move.")]
    [SerializeField]
    private float _moveRate = 7f;
    #endregion

    #region Private.
    /// <summary>
    /// Time to destroy this projectile.
    /// </summary>
    private float _destroyTime = -1f;
    /// <summary>
    /// Distance remaining to catch up.
    /// </summary>
    private float _catchupDistance = 0f;
    #endregion


    void Start()
    {
        InitSpell();
    }

    void InitSpell() {
        if (SkillIndex == 0)
        {
            Spell = SkillLibrary.TowerSkills[TowerSkillIndex];
            damage.Add(Spell.PhysicalDamage);
            damage.Add(Spell.MagicDamage);
            damage.Add(Spell.SoulDamage);
        }
        else
        {
            Unit unitStats = player.GetComponent<Unit>();
            Spell = SkillLibrary.Skills[SkillIndex];
            float BasicDamage = unitStats.Damage / 3;
            damage.Add(Spell.PhysicalDamage + BasicDamage);
            damage.Add(Spell.MagicDamage + BasicDamage);
            damage.Add(Spell.SoulDamage + BasicDamage);
        }

        rb = GetComponent<Rigidbody>();
        speed = Spell.ProjectileSpeed;

        if (Spell.SkillFlashPrefab != null)
        {
            flash = (GameObject)Resources.Load(Spell.SkillFlashPrefab);
            var flashInstance = Instantiate(flash, transform.position, Quaternion.identity);
            flashInstance.transform.forward = gameObject.transform.forward;
            var flashPs = flashInstance.GetComponent<ParticleSystem>();
            if (flashPs != null)
            {
                Destroy(flashInstance, flashPs.main.duration);
            }
            else
            {
                var flashPsParts = flashInstance.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(flashInstance, flashPsParts.main.duration);
            }
        }

        AudioManagerObj = GameObject.FindGameObjectWithTag("Audio");
        AudioManagerScript audioManager = AudioManagerObj.GetComponent<AudioManagerScript>();
        audioManager.PlaySound(SkillIndex, AudioRepeating, AudioStartTime, AudioRepeatTime);

    }


    void Update()
    {
        //if (speed != 0)
        //{
        //    //rb.velocity = transform.forward * speed;
        //    transform.position += transform.forward * (speed * Time.deltaTime);
        //}




        //Not initialized.
        if (_destroyTime < 0)
            return;

        float moveValue = speed * Time.deltaTime;
        float catchupValue = 0f;

        //Apply catchup time.
        if (_catchupDistance > 0f)
        {
            float step = (_catchupDistance * Time.deltaTime);
            //Subtract step from catchup distance to eliminate traveled distance.
            _catchupDistance -= step;

            catchupValue = step;

            if (_catchupDistance < (moveValue * 0.1f))
            {
                catchupValue += _catchupDistance;
                _catchupDistance = 0f;
            }
        }

        //Move straight up.
        transform.position += transform.forward * (moveValue + catchupValue);

        if (Time.time > _destroyTime)
            Destroy(gameObject);

    }

    public void Initialize(float duration)
    {
        if (Spell == null)
            InitSpell();
        _catchupDistance = (duration * speed);
        _destroyTime = Time.time + Spell.Duration;
    }


    private void SendTransformRpc(Vector3 position)
    {
        if ((position - transform.position).magnitude > 5)
        {
            transform.position = position;

        }
    }

    
    public void DestroyProjectile(Vector3 vfxPosition)
    {
        //DestroyProjectileRpc(vfxPosition);
        StartCoroutine("CreateVFX", vfxPosition);
        Destroy(gameObject);
    }


   
    //public void DestroyProjectileRpc(Vector3 vfxPosition)
    //{
    //    StartCoroutine("CreateVFX", vfxPosition);
    //    Destroy(gameObject);
    //}

    
    IEnumerator CreateVFX(Vector3 vfxPosition)
    {
        Object onHitPref = Resources.Load(Spell.SkillHitPrefab); // note: not .prefab!

        GameObject onHitObj = (GameObject)GameObject.Instantiate(onHitPref, vfxPosition, Quaternion.Euler(-90, 0, 0));
        yield return new WaitForSeconds(1);
        Destroy(onHitObj);
    }

    //IEnumerator DestroyObject()
    //{
    //    yield return new WaitForSeconds(Spell.Duration);

    //    Destroy(gameObject);
    //}
}