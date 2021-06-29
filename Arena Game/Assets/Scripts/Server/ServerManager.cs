using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerManager : MonoBehaviour
{
    
    enum NetworkType
    {
        Host = 1,
        Client = 2,
        Server = 3,
    }

    [SerializeField]
    private NetworkType _networkType;


    NetworkManager manager;

    void Awake()
    {
        manager = GetComponent<NetworkManager>();

        //try
        //{
            //if (_networkType == NetworkType.Host)
            //{
            //    manager.StartHost();
            //}
            //else if (_networkType == NetworkType.Client)
            //{
            //    manager.StartClient();
            //}
            //else if (_networkType == NetworkType.Server)
            //{
            //    manager.StartServer();
            //}
        //}
        //catch (System.Exception)
        //{

        //    manager.StartClient();
        //}

    }

    // Start is called before the first frame update
    void Start()
    {
        if (_networkType == NetworkType.Host)
        {
            manager.StartHost();
        }
        else if (_networkType == NetworkType.Client)
        {
            manager.StartClient();
        }
        else if (_networkType == NetworkType.Server)
        {
            manager.StartServer();
        }
    }

}
