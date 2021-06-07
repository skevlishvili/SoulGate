using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    #region Referances
    public PlayerAnimator anim;
    public Abillities abillitiesScript;
    #endregion

    public void EndOfSkill()
    {        
        anim.EndAnimation(abillitiesScript.PlayerAbillities[abillitiesScript.ComponentIndex].Skill.AnimatorProperty);
    }
}
