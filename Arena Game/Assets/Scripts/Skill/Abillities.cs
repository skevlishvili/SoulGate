using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Abillities : MonoBehaviour
{

    public Image abilityImageOne;
    public float cooldownOne = 5;
    bool isCoolDown = false;
    public KeyCode abilityOne;


    // AbilityOne Input Variables
    Vector3 position;
    public Canvas abilityOneCanvas;
    public Image targetCircle;
    public Image skillShot;
    public Transform player;


    // Start is called before the first frame update
    void Start()
    {
        abilityImageOne.fillAmount = 0;

        targetCircle.GetComponent<Image>().enabled = false;
        skillShot.GetComponent<Image>().enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        AbilityOne();

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
        }

        Quaternion transRot = Quaternion.LookRotation(position - player.transform.position);
        transRot.eulerAngles = new Vector3(0, transRot.eulerAngles.y, transRot.eulerAngles.z);

        abilityOneCanvas.transform.rotation = Quaternion.Lerp(transRot, abilityOneCanvas.transform.rotation, 0f);


    }



    void AbilityOne()
    {
        if (Input.GetKey(abilityOne) && isCoolDown == false)
        {
            skillShot.GetComponent<Image>().enabled = true;
            targetCircle.GetComponent<Image>().enabled = true;


        }

        if (skillShot.GetComponent<Image>().enabled == true && Input.GetMouseButtonDown(0))
        {
            isCoolDown = true;
            abilityImageOne.fillAmount = 1;
        }

        if (isCoolDown)
        {
            abilityImageOne.fillAmount -= 1 / cooldownOne * Time.deltaTime;
            skillShot.GetComponent<Image>().enabled = false;
            targetCircle.GetComponent<Image>().enabled = false;

            if (abilityImageOne.fillAmount <= 0)
            {
                abilityImageOne.fillAmount = 0;
                isCoolDown = false;
            }
        }
    }
}
