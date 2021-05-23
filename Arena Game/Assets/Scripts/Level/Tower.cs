using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    Vector3 Tower_Position;
    Quaternion Tower_Rotation;

    // Start is called before the first frame update
    void Start()
    {
        Tower_Position = gameObject.transform.localPosition;
        Tower_Position.y = 0.3f;

        Tower_Rotation = gameObject.transform.localRotation;
        Tower_Rotation.z = 0;
        Tower_Rotation.x = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Spell")
        {

            Object DestroyedTower = Resources.Load("Prefabs/Level/DestroyedTower_1");
            Instantiate(DestroyedTower, Tower_Position, Tower_Rotation);
            gameObject.SetActive(false);
        }
    }
}
