using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arena_Shield : MonoBehaviour
{
    public GameObject Shield;
    public GameObject[] Towers;
    public float Shield_Duration;

    public MeshRenderer Render;
    public float TextureDiffusion = 0;
    public float TextureDiffusionMaximum = 1f;
    public float TextureDiffusionmMinimum = 0.15f;

    public DateTime ShieldActivatinTime;
    public bool DiffuseTransitionOn = false;

    private void FixedUpdate()
    {
        if (DiffuseTransitionOn)
        {
            DiffuseTransition();
        }
    }

    void DiffuseTransition()
    {
        Render.material.SetFloat("DiffuseTransition", Mathf.Lerp(TextureDiffusionmMinimum, TextureDiffusionMaximum, TextureDiffusion));
        TextureDiffusion += 0.2f * Time.deltaTime;

        var TimeAfterActivation = DateTime.Now - ShieldActivatinTime;

        if (TextureDiffusion > 1 && TimeAfterActivation.TotalSeconds >= Shield_Duration - 3.5f)
        {
            float temp = TextureDiffusionMaximum;
            TextureDiffusionMaximum = TextureDiffusionmMinimum;
            TextureDiffusionmMinimum = temp;
            TextureDiffusion = 0.0f;
        }
    }
}
