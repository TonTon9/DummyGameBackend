using System;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Components.Network
{
    public class GameNetworkManager : NetworkManager
    {
        public event Action<List<NetworkPlayer>, NetworkManager> OnLobbySceneLoaded = delegate {  };
        
        [SerializeField]
        private List<NetworkPlayer> _players = new();

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
            
            _players.Add(player);
        }

        public override void OnServerDisconnect(NetworkConnectionToClient conn)
        {
            base.OnServerDisconnect(conn);
            var player = conn.identity.GetComponent<NetworkPlayer>();

            _players.Remove(player);
        }

        public void HostLobby()
        {
            StartHost();
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
            ServerChangeScene("Multiplayer");
        }

        public override void OnServerSceneChanged(string sceneName)
        {
            base.OnServerSceneChanged(sceneName);

            if (SceneManager.GetActiveScene().name.StartsWith("Multiplayer"))
            {
                OnLobbySceneLoaded?.Invoke(_players, this);
            }
        }
    }
}
