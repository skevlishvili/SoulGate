using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ChatBehaviour : NetworkBehaviour
{
    public bool IsTyping = false;
    [SerializeField] private GameObject chatUI = null;
    [SerializeField] private TMP_Text chatText = null;
    [SerializeField] private TMP_InputField inputField = null;


  

    private static event Action<string> OnMessage;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && !inputField.isFocused) {
            inputField.Select();
            inputField.ActivateInputField();
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
        chatText.text += message;
    }

    [Client]
    public void Send(string message)
    {
        if (!Input.GetKeyDown(KeyCode.Return)) { return; }

        if (string.IsNullOrWhiteSpace(message)) { return; }

        CmdSendMessage(message);

        inputField.text = string.Empty;
    }

    [Command]
    private void CmdSendMessage(string message)
    {
        RpcHandleMessage($"[{connectionToClient.connectionId}]: {message}");
    }

    [ClientRpc]
    private void RpcHandleMessage(string message)
    {
        OnMessage?.Invoke($"\n{message}");
    }
}
