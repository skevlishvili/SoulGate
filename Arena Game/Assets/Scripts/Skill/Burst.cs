using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burst : MonoBehaviour
{
    public GameObject player;

    Skill Spell;
    public List<float> damage = new List<float>();
    public int SkillIndex;

    public bool CreateCollider;
    public float WaitBeforeCreate;
    public Vector2 ColliderCoordinatesFromParticleSystem;
    Vector3 Collidercoordinates;

    //public GameObject skillLibraryObj;

    void Start()
    {
        Spell = SkillLibrary.Skills[SkillIndex];
        damage.Add(Spell.PhysicalDamage);
        damage.Add(Spell.MagicDamage);
        damage.Add(Spell.SoulDamage);

        Destroy(gameObject, Spell.Duration);

        //if (CreateCollider)
        //{
        //    Collidercoordinates = gameObject.transform.position;
        //    Collidercoordinates.x += ColliderCoordinatesFromParticleSystem.x;
        //    Collidercoordinates.z += ColliderCoordinatesFromParticleSystem.y;
        //    Collidercoordinates.y = 0;

        //    StartCoroutine(CreateColliderAfterXSecond());
        //}
    }

    //IEnumerator CreateColliderAfterXSecond()
    //{
    //    yield return new WaitForSeconds(WaitBeforeCreate);
    //    SphereCollider Colider = gameObject.AddComponent<SphereCollider>();
    //    Colider.radius = 8;
    //    Colider.isTrigger = true;
    //    Colider.center = Collidercoordinates;
    //}

    //Need to attach passive skill if you hit player(life stealing / self harm)
}
