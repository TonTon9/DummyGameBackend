using System;
using Mirror;
using UnityEngine;

public class NetworkStuff : NetworkBehaviour
{
    [SerializeField] private GameObject _fpsCamera;
    [SerializeField] private GameObject _tpMesh;
    [SerializeField] private GameObject _tpModelWeapon;
    //[SerializeField] private GameObject _tp;

    private void Start()
    {
        if (isLocalPlayer)
        {
            _fpsCamera.SetActive(true);
            _tpMesh.SetActive(false);
            _tpModelWeapon.SetActive(false);
        } else
        {
            _fpsCamera.gameObject.SetActive(false);
            _tpMesh.gameObject.SetActive(true);
            _tpModelWeapon.SetActive(true);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (!isLocalPlayer)
            {
                return;
            }
            LeaveGame();
        }
    }

    public void LeaveGame()
    {
        if (isServer)
        {
            NetworkManager.singleton.StopHost();
        } else
        {
            NetworkManager.singleton.StopClient();
        }
    }
}
