using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNickname : NetworkBehaviour
{
    public static string LocalNickname;

    [SyncVar]
    public string Nickname;

    // Start is called before the first frame update
    void Start()
    {
        //Nickname = LocalNickname;
        SetNicknameCmd(LocalNickname);
    }

    [Command]
    public void SetNicknameCmd(string name)
    {
        Nickname = name;
        SetNicknameRpc(name);
    }

    [ClientRpc]
    public void SetNicknameRpc(string name)
    {
        Nickname = name;
    }
}
