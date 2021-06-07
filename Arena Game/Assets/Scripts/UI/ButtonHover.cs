using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHover : MonoBehaviour
{
    public Image ButtonImage;

    public void MouseLeave()
    {
        ButtonImage.color = new Color { r = ButtonImage.color.r, g = ButtonImage.color.g, b = ButtonImage.color.b, a = 0.5f };
    }

    public void MouseEnter()
    {
        ButtonImage.color = new Color { r = ButtonImage.color.r, g = ButtonImage.color.g, b = ButtonImage.color.b, a = 1f };
    }
}
