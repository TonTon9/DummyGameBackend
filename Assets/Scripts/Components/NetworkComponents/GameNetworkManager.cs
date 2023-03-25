using System;
using System.Collections.Generic;
using Components.Lobby;
using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace Components.Network
{
    public class GameNetworkManager : NetworkManager
    {
        public event Action PlayerRemoved = delegate { };
        public event Action OnLobbySceneLoaded = delegate { };
        public event Action<NetworkPlayer, NetworkManager> ServerAddPlayer = delegate {  };
        
        
        public List<NetworkPlayer> players = new();

        public static GameNetworkManager GetInstance { get; private set; }

        public override void Awake()
        {
            base.Awake();
            GetInstance = this;
        }

        public override void OnServerAddPlayer(NetworkConnectionToClient conn)
        {
            base.OnServerAddPlayer(conn);
            var player = conn.identity.GetComponent<NetworkPlayer>();
            player.playerName = ($"Player {Random.Range(0, 1000)}");

            ServerAddPlayer?.Invoke(player, this);
            Debug.Log("Server add player");
        }

        public override void OnServerDisconnect(NetworkConnectionToClient conn)
        {
            base.OnServerDisconnect(conn);

            Debug.Log("Server REMOVE player");
        }

        public void HostLobby()
        {
            StartHost();
            ServerChangeScene("LobbyScene");
        }

        public void JoinLobby(string address)
        {
            networkAddress = address;
            StartClient();
        }

        public override void OnStopHost()
        {
            base.OnStopHost();
            SceneManager.LoadScene(0);
        }

        public override void OnClientDisconnect()
        {
            base.OnStopClient();
            SceneManager.LoadScene(0);
        }

        public void StartGame()
        {
            ServerChangeScene("FirstLevelScene");
        }

        public override void OnServerSceneChanged(string sceneName)
        {
            base.OnServerSceneChanged(sceneName);
        
            if (SceneManager.GetActiveScene().name.StartsWith("FirstLevelScene"))
            {
                Debug.Log("OnLobbySceneLoaded");
                OnLobbySceneLoaded?.Invoke();
                GameCharactersSpawner.GetInstance.SpawnCharacters();
            }
        }
        
        public override void OnStopServer() {
            base.OnStopServer();
            players.Clear();
            PlayerRemoved?.Invoke();
        }
        
        public override void OnStopClient() {
            base.OnStopClient();
            players.Clear();
            PlayerRemoved?.Invoke();
        }
    }
}
