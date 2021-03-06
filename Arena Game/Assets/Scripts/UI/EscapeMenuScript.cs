using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapeMenuScript : MonoBehaviour
{
    public GameObject player;

    //public GameObject shopobj;
    CanvasGroup canvasGroup;
    bool EscapeOpen = false;
    public GameObject SettingsPanel;
    public bool SettingsOpen = false;
    [SerializeField]
    Players _playersManager;

    void Start()
    {
        canvasGroup = GameObject.Find("EscapeMenu").GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCodeController.Menu))
        {
            if (!EscapeOpen)
            {
                canvasGroup.alpha = 1f;
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
                EscapeOpen = !EscapeOpen;
            }
            else
            {
                Resume();
            }
        }
    }

    public void Resume() {
        CanvasGroup canvasGroup = this.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        EscapeOpen = !EscapeOpen;
    }

    public void Settings()
    {
        SettingsOpen = !SettingsOpen;
        SettingsPanel.SetActive(SettingsOpen);
    }

    public void Disconnect() {
        _playersManager.RemovePlayerCmd(ClientScene.localPlayer.gameObject);
        NetworkManager.singleton.StopClient();
        Destroy(GameObject.FindGameObjectsWithTag("NetworkManager")[0]);
        SceneManager.LoadScene("Launcher");
        //Destroy(player);
    }
}
