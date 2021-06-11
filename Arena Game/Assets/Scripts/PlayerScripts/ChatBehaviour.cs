using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ChatBehaviour : NetworkBehaviour
{
    public bool IsTyping = false;
    public PlayerNickname Nickname;
    public CanvasGroup ChatCanvasGroup;
    [SerializeField] private GameObject chatUI = null;
    [SerializeField] private TMP_Text chatText = null;
    [SerializeField] private TMP_InputField inputField = null;
    

  

    private static event Action<string> OnMessage;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && !inputField.isFocused) {
            inputField.Select();
            inputField.ActivateInputField();
            ChatCanvasGroup.alpha = 1;
        }

        IsTyping = inputField.isFocused;
    }

    public override void OnStartAuthority()
    {
        chatUI.SetActive(true);

        OnMessage += HandleNewMessage;
    }

    [ClientCallback]
    private void OnDestroy()
    {
        if (!hasAuthority) { return; }

        OnMessage -= HandleNewMessage;
    }

    private void HandleNewMessage(string message)
    {

        ChatCanvasGroup.alpha = 1;
        chatText.text += message;
        ChatOff();
    }

    [Client]
    public void Send(string message)
    {
        if (!Input.GetKeyDown(KeyCode.Return)) { return; }

        if (string.IsNullOrWhiteSpace(message)) { return; }

        CmdSendMessage(message);

        StartCoroutine(ChatOff());

        inputField.text = string.Empty;
    }

    [Command]
    private void CmdSendMessage(string message)
    {
        RpcHandleMessage($"{Nickname.Nickname} : {message}");
    }

    [ClientRpc]
    private void RpcHandleMessage(string message)
    {
        OnMessage?.Invoke($"\n{message}");
    }

    private IEnumerator ChatOff()
    {
        yield return new WaitForSeconds(5);

        ChatCanvasGroup.alpha = 0.1f;
    }
}
