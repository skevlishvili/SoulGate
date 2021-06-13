using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using System;
using UnityEngine;
using Mirror;
using System.Linq;

public class Hovl_Laser : MonoBehaviour
{
    public Tower TowerScript;
    public SpellFire SpellFireScript;

    public GameObject HitEffect;
    public float HitOffset = 0;
    public bool useLaserRotation = false;

    public float MaxLength;
    private LineRenderer Laser;

    public float MainTextureLength = 1f;
    public float NoiseTextureLength = 1f;
    private Vector4 Length = new Vector4(1,1,1,1);
    //private Vector4 LaserSpeed = new Vector4(0, 0, 0, 0); {DISABLED AFTER UPDATE}
    //private Vector4 LaserStartSpeed; {DISABLED AFTER UPDATE}
    //One activation per shoot
    private bool LaserSaver = false;
    private bool UpdateSaver = false;

    private ParticleSystem[] Effects;
    private ParticleSystem[] Hit;

    void Start ()
    {
        Laser = GetComponent<LineRenderer>();
        Effects = GetComponentsInChildren<ParticleSystem>();
        Hit = HitEffect.GetComponentsInChildren<ParticleSystem>();
    }

    [Server]
    void FixedUpdate()
    {
        if (TowerScript.PlayerWithinRange.All(x => !(x != null)))
        {
            SpellFireScript.TowerIsFiring = false;
            Destroy(gameObject);
        }
            

        Laser.material.SetTextureScale("_MainTex", new Vector2(Length[0], Length[1]));                    
        Laser.material.SetTextureScale("_Noise", new Vector2(Length[2], Length[3]));

        if (Laser != null && UpdateSaver == false)
        {
            Laser.SetPosition(0, transform.position);
            RaycastHit hit;  
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, MaxLength)) 
            {
                Laser.SetPosition(1, hit.point);

                    HitEffect.transform.position = hit.point + hit.normal * HitOffset;
                if (useLaserRotation)
                    HitEffect.transform.rotation = transform.rotation;
                else
                    HitEffect.transform.LookAt(hit.point + hit.normal);

                foreach (var AllPs in Effects)
                {
                    if (!AllPs.isPlaying) AllPs.Play();
                }

                Length[0] = MainTextureLength * (Vector3.Distance(transform.position, hit.point));
                Length[2] = NoiseTextureLength * (Vector3.Distance(transform.position, hit.point));
            }
            else
            {
                var EndPos = transform.position + transform.forward * MaxLength;
                Laser.SetPosition(1, EndPos);
                HitEffect.transform.position = EndPos;
                foreach (var AllPs in Hit)
                {
                    if (AllPs.isPlaying) AllPs.Stop();
                }

                Length[0] = MainTextureLength * (Vector3.Distance(transform.position, EndPos));
                Length[2] = NoiseTextureLength * (Vector3.Distance(transform.position, EndPos));

            }

            if (Laser.enabled == false && LaserSaver == false)
            {
                LaserSaver = true;
                Laser.enabled = true;
            }





            RotateLaser();
        }  
    }

    [Server]
    public void RotateLaser()
    {
        var playerPosition = PlayerCoodinates(TowerScript.PlayerWithinRange[UnityEngine.Random.Range(0, TowerScript.PlayerWithinRange.Length)]);

        var Hit = gameObject.transform.GetChild(1).gameObject;

        var laser = gameObject.GetComponent<LineRenderer>();
        laser.SetPositions(new Vector3[2] { gameObject.transform.position, new Vector3(playerPosition.transform.position.x, playerPosition.transform.position.y + 1.5f, playerPosition.transform.position.z) });

        Hit.transform.position = playerPosition.transform.position;
        Hit.transform.position = new Vector3(Hit.transform.position.x, 1, Hit.transform.position.z);

    }

    public Transform PlayerCoodinates(GameObject Player)
    {
        return Player.transform;
    }


    public void DisablePrepare()
    {
        if (Laser != null)
        {
            Laser.enabled = false;
        }
        UpdateSaver = true;

        if (Effects != null)
        {
            foreach (var AllPs in Effects)
            {
                if (AllPs.isPlaying) AllPs.Stop();
            }
        }
    }
}
