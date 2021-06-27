using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapScript : NetworkBehaviour
{
    private GameObject[] _towers = new GameObject[0];
    private GameObject[] _crystals = new GameObject[0];

    private List<GameObject> _towerImgs = new List<GameObject>();
    private List<GameObject> _crystalImgs = new List<GameObject>();

    [SerializeField]
    private GameObject _map;
    [SerializeField]
    private GameObject _zero;
    [SerializeField]
    private float _scale;
    [SerializeField]
    private Players _playerManager;
    private List<GameObject> players = new List<GameObject>();


    // Update is called once per frame
    void Update()
    {
        

        if (_towers.Length == 0 || _crystals.Length == 0)
            initialize();






        if (players.Count != _playerManager.PlayersGameObjects.Count)
        {
            initializePlayers();
        }


        for (int i = 0; i < players.Count; i++)
        {
            players[i].transform.position = newPos(_playerManager.PlayersGameObjects[i].transform.position);
            players[i].transform.eulerAngles = new Vector3(180, 0, _playerManager.PlayersGameObjects[i].transform.eulerAngles.y + 180);
        }

    }

    private void initializePlayers()
    {
        var _mapRect = _map.GetComponent<RectTransform>();
        Debug.Log($"{players.Count} {_playerManager.PlayersGameObjects.Count}");
        for (int i = players.Count; i < _playerManager.PlayersGameObjects.Count; i++)
        {
            var prefabSrc = "Prefabs/UI/" + (_playerManager.PlayersGameObjects[i].GetInstanceID() == ClientScene.localPlayer?.gameObject.GetInstanceID() ? "PlayerImage" : "EnemyImage");

            var position = _playerManager.PlayersGameObjects[i].transform.position;
            var image = (GameObject)Resources.Load(prefabSrc);
            var pos = newPos(position);
            GameObject player = Instantiate(image, pos, Quaternion.identity);
            player.transform.parent = _map.transform;
            player.transform.position = pos;
            var playerRect = player.GetComponent<RectTransform>();

            playerRect.anchorMin = _mapRect.anchorMin;
            playerRect.anchorMax = _mapRect.anchorMax;
            playerRect.pivot = _mapRect.pivot;
            playerRect.position = pos;
            players.Add(player);
        }
    }

    private void initialize()
    {
        _towers = GameObject.FindGameObjectsWithTag("Tower");
        _crystals = GameObject.FindGameObjectsWithTag("Crystal");
        var _mapRect = _map.GetComponent<RectTransform>();

        for (int i = 0; i < _towers.Length; i++)
        {
            var position = _towers[i].transform.position;
            var image = (GameObject)Resources.Load("Prefabs/UI/TowerImage");
            var pos = newPos(position);
            GameObject tower = Instantiate(image, pos, Quaternion.identity);
            tower.transform.parent = _map.transform;
            tower.transform.position = pos;
            var towerRect = tower.GetComponent<RectTransform>();
            
            towerRect.anchorMin = _mapRect.anchorMin;
            towerRect.anchorMax = _mapRect.anchorMax;
            towerRect.pivot = _mapRect.pivot;
            towerRect.position = pos;
            _towerImgs.Add(tower);
        }

        for (int i = 0; i < _crystals.Length; i++)
        {
            var position = _crystals[i].transform.position;
            var image = (GameObject)Resources.Load("Prefabs/UI/CrystalImage");
            var pos = newPos(position);
            GameObject crystal = Instantiate(image, pos, Quaternion.identity);
            crystal.transform.parent = _map.transform;
            crystal.transform.position = pos;
            var crystalRect = crystal.GetComponent<RectTransform>();

            crystalRect.anchorMin = _mapRect.anchorMin;
            crystalRect.anchorMax = _mapRect.anchorMax;
            crystalRect.pivot = _mapRect.pivot;
            crystalRect.position = pos;
            _crystalImgs.Add(crystal);
        }
    }

    private Vector3 newPos(Vector3 oldPos) {
        var zero = _zero.transform.position;
        var newPos = new Vector3(zero.x+(oldPos.x/_scale), zero.y + (oldPos.z/_scale), 0);
        

        return newPos;
    }
}
