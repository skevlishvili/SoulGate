using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillActivate : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        SkillLibrary SkLObj = new SkillLibrary();
        MageSkill Spell = SkLObj.FireSpell;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
