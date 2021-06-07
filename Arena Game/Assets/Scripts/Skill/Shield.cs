using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    Skill Spell;
    public int SkillIndex;
    public Renderer Render;
    public bool Dissolve;

    DateTime ShieldActivatinTime;

    float TextureDiffusion = 0;
    float TextureDiffusionMaximum = 1.5f;
    float TextureDiffusionmMinimum = 0f;

    private void Awake()
    {
        Render.material.SetFloat("DiffuseTransition", 0);
        ShieldActivatinTime = DateTime.Now;
    }

    void Start()
    {
        Spell = SkillLibrary.Skills[SkillIndex];
        gameObject.transform.localScale = new Vector3(Spell.AttackRadius, Spell.AttackRadius, Spell.AttackRadius);
    }

    private void Update()
    {
        DiffuseTransition();
    }

    void DiffuseTransition()
    {
        Render.material.SetFloat("DiffuseTransition", Mathf.Lerp(TextureDiffusionmMinimum, TextureDiffusionMaximum, TextureDiffusion));
        var TimeAfterActivation = DateTime.Now - ShieldActivatinTime;

        if (TimeAfterActivation.TotalSeconds >= Spell.Duration - 3f)
        {
            TextureDiffusion += 0.35f * Time.deltaTime;
        }
        else
        {
            TextureDiffusion += 0.2f * Time.deltaTime;
        }



        if (TextureDiffusion > 1 && TimeAfterActivation.TotalSeconds >= Spell.Duration - 3f)
        {
            float temp = TextureDiffusionMaximum;
            TextureDiffusionMaximum = TextureDiffusionmMinimum;
            TextureDiffusionmMinimum = temp;
            TextureDiffusion = 0.0f;
        }
    }
}
