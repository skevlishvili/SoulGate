using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skull_Shader_Script : MonoBehaviour
{
    public Material Skull_material;
    static bool IncreaseTextureDiffusion = false;
    float TextureDeffusion = 0;
    float TextureDeffusionMaximum = 1.5f;
    float TextureDeffusionmMinimum = 0f;

    public int Skull_Light_Count;


    void Start()
    {
        Skull_material.SetFloat("DiffuseTransition", 0);
    }

    // Update is called once per frame
    void Update()
    {
        SkullTextureDiffusion();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Skull_Texture_Diffusion(true);
        }
    }

    public static void Skull_Texture_Diffusion(bool On_Off)
    {
        IncreaseTextureDiffusion = On_Off;
    }

    void SkullTextureDiffusion()
    {
        if (IncreaseTextureDiffusion)
        {
            Skull_material.SetFloat("DiffuseTransition", Mathf.Lerp(TextureDeffusionmMinimum, TextureDeffusionMaximum, TextureDeffusion));
            TextureDeffusion += 0.2f * Time.deltaTime;

            if (TextureDeffusion > 1.5f && Skull_Light_Count < 3 )
            {
                float temp = TextureDeffusionMaximum;
                TextureDeffusionMaximum = TextureDeffusionmMinimum;
                TextureDeffusionmMinimum = temp;
                TextureDeffusion = 0.0f;
            }
        } 
    }
}
