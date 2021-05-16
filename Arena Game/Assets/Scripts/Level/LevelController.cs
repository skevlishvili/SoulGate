using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public GameObject[] Crystals;
    public float ManaGenerationQuantity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    int ActiveLevelElementsCounter(GameObject[] ObjArray)
    {
        int result = 0;

        foreach (var item in ObjArray)
        {
            if(item.gameObject.activeSelf)
            {
                result += 1;
            }
        }

        return result;
    }
}
