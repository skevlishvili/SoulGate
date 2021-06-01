using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadyScript : MonoBehaviour
{
    public Text ReadyText;
    public Image ReadyButtonImage;

    private void Start()
    {
    }

    public void ToggleReady() {
        var PlayerUnits = ClientScene.localPlayer.gameObject.GetComponent<Unit>();
        Debug.Log(PlayerUnits.IsReady);

        if (PlayerUnits.IsReady)
        {
            ReadyText.text = "Not ready";
            ReadyButtonImage.color = new Color { r = 255, g = 255, b = 255, a = 0.5f };
            PlayerUnits.UnreadyCmd();
        }
        else {
            ReadyText.text = "Ready";
            ReadyButtonImage.color = new Color { r = 62, g = 255, b = 0, a = 0.5f };
            PlayerUnits.ReadyCmd();
        }
    }

    public void MouseLeave() { 
        ReadyButtonImage.color = new Color { r = ReadyButtonImage.color.r, g = ReadyButtonImage.color.g, b = ReadyButtonImage.color.b, a = 0.5f };
    }

    public void MouseEnter()
    {
        ReadyButtonImage.color = new Color { r = ReadyButtonImage.color.r, g = ReadyButtonImage.color.g, b = ReadyButtonImage.color.b, a = 1f };
    }


    public void Hide() {
        ReadyText.text = "";
        ReadyButtonImage.enabled = false;
    }

    public void Show() {
        ReadyText.text = "Not ready";
        ReadyButtonImage.enabled = true;
    }
}
