using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Teleport : MonoBehaviour
{
    [SerializeField]
    private Camera cam;
    public NavMeshAgent agent;
    public Shader teleportShader;
    Renderer rend;
    Skill Spell;

    public int SkillIndex = 6;
    float dissolveOverTime;
    float dissolveSpeed;
    private bool shouldDissolve = false;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rend = GetComponentInChildren<Renderer>();

        Spell = SkillLibrary.Skills[SkillIndex];
    }

    // Update is called once per frame
    void Update()
    {
        //StartCoroutine("Teleportation");
        dissolve();
    }

    public void ActivateSkill(int index)
    {
        if (index == 6)
        {
            Teleportation();
        }
    }


    IEnumerator Teleportation()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            Vector3 toHit = hit.point - transform.position;
            float distance = toHit.magnitude;
            shouldDissolve = true;
            yield return new WaitForSeconds(Spell.ActivationTime);
            checkDistance(distance, hit, toHit);
            shouldDissolve = false;
        }
    }


    void checkDistance(float distance, RaycastHit hit, Vector3 toHit)
    {
        if (distance < Spell.Distance)
        {
            transform.position = hit.point;
            agent.isStopped = true;
        }
        else
        {
            Vector3 direction = toHit.normalized * Spell.Distance;
            transform.position += direction;
            agent.isStopped = true;
        }
    }

    void dissolve()
    {
        if (shouldDissolve && rend.material.GetFloat("Vector1_9AE89CB0") < 2)
        {
            dissolveOverTime += Time.deltaTime * dissolveSpeed;
            rend.material.SetFloat("Vector1_9AE89CB0", dissolveOverTime);
        }
        if (!shouldDissolve && rend.material.GetFloat("Vector1_9AE89CB0") > -2)
        {
            dissolveOverTime -= Time.deltaTime * dissolveSpeed;
            rend.material.SetFloat("Vector1_9AE89CB0", dissolveOverTime);
        }
    }
}


