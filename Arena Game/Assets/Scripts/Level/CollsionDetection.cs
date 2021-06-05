using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollsionDetection : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Spell")
        {
            Projectile projectile = other.GetComponent<Projectile>();
            if (projectile == null)
                return;

            Skill Spell = SkillLibrary.Skills[HelpMethods.GetSkillIndexByName(other.name)];

            // ----------------------------------------------gasasworebelia---------------------------------------------
            Vector3 contact = other.gameObject.GetComponent<Collider>().ClosestPoint(transform.position);
            //Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact);
            Quaternion rot = Quaternion.FromToRotation(Vector3.zero, Vector3.zero);
            Vector3 pos = contact + contact.normalized;
            //----------------------------------------------------------------------------------------------------------


            if (Spell.SkillHitPrefab != null)
            {
                GameObject hit = (GameObject)Resources.Load(Spell.SkillHitPrefab);
                var hitInstance = Instantiate(hit, pos, rot);
                hitInstance.transform.LookAt(contact + contact.normalized);
            }

            projectile.DestroyProjectile(gameObject.transform.position);

        }
    }

    private void OnParticleCollision(GameObject other)
    {
        Projectile projectile = other.GetComponent<Projectile>();
        if (projectile == null)
            return;

        if (other.tag == "Spell")
        {
            //something
        }
    }
}
