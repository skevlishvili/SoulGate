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

    float dissolveOverTime;

    public float dissolveSpeed = 0.75f;


    private bool shouldDissolve = false;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        rend = GetComponentInChildren<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine("Teleportation");
        dissolve();
    }


    IEnumerator Teleportation()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                Vector3 toHit = hit.point - transform.position;
                float distance = toHit.magnitude;

                //Debug.Log(rend.material.shader);
                //rend.material.shader = teleportShader;


                shouldDissolve = true;
                Debug.Log($"Above Yield {shouldDissolve}");
                yield return new WaitForSeconds(1f);
                checkDistance(distance, hit, toHit);
                shouldDissolve = false;
                Debug.Log($"Below Yield {shouldDissolve}");
            }
        }
    }


    void checkDistance(float distance, RaycastHit hit, Vector3 toHit)
    {
        if (distance < 20f)
        {
            transform.position = hit.point;
            agent.isStopped = true;
        }
        else
        {
            Vector3 direction = toHit.normalized * 20f;
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


