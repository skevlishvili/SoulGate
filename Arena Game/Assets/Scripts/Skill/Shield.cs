using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    Skill Spell;
    public List<float> damage = new List<float>();
    public int SkillIndex;

    public bool Dissolve;
    Material ShieldMaterial;

    float TextureDeffusion = 0;
    float TextureDeffusionMaximum = 1.5f;
    float TextureDeffusionmMinimum = 0f;

    void Start()
    {
        Spell = SkillLibrary.Skills[SkillIndex];
        damage.Add(Spell.PhysicalDamage);
        damage.Add(Spell.MagicDamage);
        damage.Add(Spell.SoulDamage);

        ShieldMaterial = GetComponent<Renderer>().material;

        gameObject.transform.localScale = new Vector3(Spell.AttackRadius, Spell.AttackRadius, Spell.AttackRadius);

        Destroy(gameObject, Spell.Duration);
    }

    private void Update()
    {
        TextureDiffusion();
    }

    void TextureDiffusion()
    {
        if (Dissolve)
        {
            ShieldMaterial.SetFloat("DiffuseTransition", Mathf.Lerp(TextureDeffusionmMinimum, TextureDeffusionMaximum, TextureDeffusion));
            TextureDeffusion += 0.1f * Time.deltaTime;

            if (TextureDeffusion > 1.5f)
            {
                float temp = TextureDeffusionMaximum;
                TextureDeffusionMaximum = TextureDeffusionmMinimum;
                TextureDeffusionmMinimum = temp;
                TextureDeffusion = 0.0f;
            }
        }
    }
}
