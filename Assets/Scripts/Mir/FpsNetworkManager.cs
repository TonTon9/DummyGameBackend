using System;
using System.Collections.Generic;
using Mirror;
using Steamworks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FpsNetworkManager : NetworkManager
{
    [SerializeField] private GameObject _enterAdressPanel;
    [SerializeField] private GameObject _landingPage;
    [SerializeField] private GameObject _lobbyUI;

    [SerializeField] private TMP_InputField _adressField;

    [SerializeField] private GameObject _startGameButton;

    public List<PlayerScript> _playersList = new List<PlayerScript>();

    [SerializeField] private GameObject _playerGameObject;

    [SerializeField]
    private bool _useSteam = true;

    protected Callback<LobbyCreated_t> LobbyCreated;
    protected Callback<GameLobbyJoinRequested_t> LobbyJoinRequest;
    protected Callback<LobbyEnter_t> LobbyEnter;

    public static CSteamID LobbyID;

    private void Start()
    {
        //base.Start();
        if (!_useSteam)
        {
            return;
        }
        LobbyCreated = Callback<LobbyCreated_t>.Create(OnLobbyCreated);
        LobbyJoinRequest = Callback<GameLobbyJoinRequested_t>.Create(OnGameLobbyJoinRequested);
        LobbyEnter = Callback<LobbyEnter_t>.Create(OnLobbyEntered);
    }

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        base.OnServerAddPlayer(conn);

        CSteamID steamID = SteamMatchmaking.GetLobbyMemberByIndex(LobbyID, numPlayers - 1);

        var playerScripts = conn.identity.GetComponent<PlayerScript>();
        
        playerScripts.SetSteamID(steamID.m_SteamID);
        
        PlayerScript playerStartPrefab = conn.identity.GetComponent<PlayerScript>();
        
        _playersList.Add(playerStartPrefab);

        if (_playersList.Count >= 2)
        {
            _startGameButton.SetActive(true);
        }
    }

    public override void OnServerDisconnect(NetworkConnectionToClient conn)
    {
        base.OnServerDisconnect(conn);
        
        PlayerScript playerStartPrefab = conn.identity.GetComponent<PlayerScript>();
        _playersList.Remove(playerStartPrefab);
        
        if (_playersList.Count < 2)
        {
            _startGameButton.SetActive(false);
        }
    }

    public void HostLobby()
    {
        _landingPage.SetActive(false);
        if (_useSteam)
        {
            SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypeFriendsOnly, 2);
            return;
        }
        NetworkManager.singleton.StartHost();
    }

    public void JoinButton()
    {
        _enterAdressPanel.SetActive(true);
        _landingPage.SetActive(false);
    }

    public void JoinLobby()
    {
        NetworkManager.singleton.networkAddress = _adressField.text;
        NetworkManager.singleton.StartClient();
    }

    public override void OnClientConnect()
    {
        base.OnClientConnect();
        
        _lobbyUI.SetActive(true);
        _enterAdressPanel.SetActive(false);
        _landingPage.SetActive(false);
    }

    public override void OnClientDisconnect()
    {
        base.OnClientDisconnect();
        SceneManager.LoadScene(0);
        try
        {
            _landingPage.SetActive(true);
            _lobbyUI.SetActive(false);
            _enterAdressPanel.SetActive(false);
        } catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    public override void OnStopHost()
    {
        base.OnStopHost();
        SceneManager.LoadScene(0);
    }

    public void LeaveLobby()
    {
        if (NetworkServer.active && NetworkClient.isConnected)
        {
            NetworkManager.singleton.StopHost();
        } else
        {
            NetworkManager.singleton.StopClient();
        }
    }

    public void StartGame()
    {
        ServerChangeScene("Multiplayer");
    }

    public override void OnServerSceneChanged(string sceneName)
    {
        base.OnServerSceneChanged(sceneName);

        if (SceneManager.GetActiveScene().name.StartsWith("Multiplayer"))
        {
            foreach (var player in _playersList)
            {
                var connectionToClient = player.connectionToClient;
                GameObject playerP = Instantiate(_playerGameObject, GetStartPosition().transform.position, Quaternion.identity);

                NetworkServer.ReplacePlayerForConnection(connectionToClient, playerP);
                
                Destroy(player.gameObject);
            }
        }
    }

    private void OnLobbyCreated(LobbyCreated_t callback)
    {
        if (callback.m_eResult != EResult.k_EResultOK)
        {
            _landingPage.SetActive(true);
            return;
        }
        
        NetworkManager.singleton.StartHost();

        SteamMatchmaking.SetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), "HostIP", SteamUser.GetSteamID().ToString());
    }

    private void OnGameLobbyJoinRequested(GameLobbyJoinRequested_t callback)
    {
        SteamMatchmaking.JoinLobby(callback.m_steamIDLobby);
    }

    private void OnLobbyEntered(LobbyEnter_t callback)
    {
        LobbyID = new CSteamID(callback.m_ulSteamIDLobby);
        if (NetworkServer.active)
        {
            return;
        }
        string HostIP = SteamMatchmaking.GetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), "HostIP");

        NetworkManager.singleton.networkAddress = HostIP;
        NetworkManager.singleton.StartClient();
        
        _landingPage.SetActive(false);
    }

    public void CloseAddressButton()
    {
        _enterAdressPanel.SetActive(false);
        _landingPage.SetActive(true);
    }
}
