using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCodeController
{
    public static KeyCode Moving = KeyCode.Mouse1;
    public static KeyCode BasicAttack = KeyCode.Mouse0;
    public static KeyCode Menu = KeyCode.Escape;
    public static KeyCode Shop = KeyCode.P;

    static KeyCode Ability1 = KeyCode.Q;
    static KeyCode Ability2 = KeyCode.W;
    static KeyCode Ability3 = KeyCode.E;
    static KeyCode Ability4 = KeyCode.R;

    public static KeyCode[] AbilitiesKeyCodeArray = new KeyCode[]{
        Ability1,
        Ability2,
        Ability3,
        Ability4
    };
}