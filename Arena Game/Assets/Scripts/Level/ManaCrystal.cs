using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaCrystal : MonoBehaviour
{
    Vector3 Crystal_Position;
    Quaternion Crystal_Rotation;
    float CrystalHealth;
    bool IsDestroyed;

    public float Moving_Speed;
    public float StartPosition;

    void Start()
    {
        CrystalHealth = 1000;
        IsDestroyed = false;

        Crystal_Position = gameObject.transform.position;
        Crystal_Rotation = gameObject.transform.rotation;
        Crystal_Rotation.z = 0;
        Crystal_Rotation.x = 0;

        //InvokeRepeating("Regeneration",1,1);//calls funqtion every second
        //InvokeRepeating("TowerEvolution", 1, 1);
    }


    // Update is called once per frame
    void Update()
    {
        float y = Mathf.PingPong(Time.time * Moving_Speed, 1);
        gameObject.transform.position = new Vector3(transform.position.x, StartPosition + 1 + y, transform.position.z);
        float Zvalue = transform.localEulerAngles.z;
        Zvalue += 5f * Time.deltaTime;
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, Zvalue);

        CheckIfDestroyed();
    }

    void CheckIfDestroyed()
    {
        if (CrystalHealth <= 0)
        {
            IsDestroyed = true;
            Object DestroyedCrystal = Resources.Load("Prefabs/Level/DestroyedCrystal_1");
            Instantiate(DestroyedCrystal, Crystal_Position, Crystal_Rotation);
            gameObject.SetActive(false);
        }
    }

    void takeDamage(float damage)
    {
        CrystalHealth -= damage;
        Debug.Log(CrystalHealth);
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Spell")
        {
            Projectile projectile = other.GetComponent<Projectile>();
            if (projectile == null)
                return;

            float Damage = projectile.damage[0] + projectile.damage[1] + projectile.damage[2];

            
            Skill Spell = SkillLibrary.Skills[HelpMethods.GetSkillIndexByName(other.name)];

            // ----------------------------------------------gasasworebelia---------------------------------------------
            Vector3 contact = other.gameObject.GetComponent<Collider>().ClosestPoint(transform.position);
            Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact);
            Vector3 pos = contact;
            //----------------------------------------------------------------------------------------------------------

            if (Spell.SkillHitPrefab != null)
            {
                GameObject hit = (GameObject)Resources.Load(Spell.SkillHitPrefab);
                var hitInstance = Instantiate(hit, pos, rot);
                hitInstance.transform.LookAt(contact);
            }

            takeDamage(Damage);

            projectile.DestroyProjectile(gameObject.transform.position);
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        Projectile projectile = other.GetComponent<Projectile>();
        if (projectile == null)
            return;

        float Damage = projectile.damage[0] + projectile.damage[1] + projectile.damage[2];

        if (other.tag == "Spell")
        {
            takeDamage(Damage);
        }
    }
}