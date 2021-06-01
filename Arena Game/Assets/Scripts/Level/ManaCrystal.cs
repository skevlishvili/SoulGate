using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaCrystal : MonoBehaviour
{
    public float Moving_Speed;
    public float StartPosition;

    // Update is called once per frame
    void Update()
    {
        float y = Mathf.PingPong(Time.time * Moving_Speed, 1);
        gameObject.transform.position = new Vector3(transform.position.x, StartPosition + 1 + y, transform.position.z);
        float Zvalue = transform.localEulerAngles.z;
        Zvalue += 5f * Time.deltaTime;
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, Zvalue);
    }
}