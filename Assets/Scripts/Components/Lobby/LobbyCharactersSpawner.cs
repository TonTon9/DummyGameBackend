using System.Collections.Generic;
using Components.Network;
using Cysharp.Threading.Tasks;
using Mirror;
using UnityEngine;
using NetworkPlayer = Components.Network.NetworkPlayer;

namespace Components.Lobby
{
    public class LobbyCharactersSpawner : BaseMonoBehaviour
    {
        [SerializeField]
        private GameObject _lobbyCharacterPrefab;

        protected override async void Subscribe()
        {
            base.Subscribe();
            await UniTask.WaitUntil(() => GameNetworkManager.GetInstance != null);
            GameNetworkManager.GetInstance.OnLobbySceneLoaded += SpawnPlayer;
        }

        protected override void UnSubscribe()
        {
            base.UnSubscribe();
            if (GameNetworkManager.GetInstance != null)
            {
                GameNetworkManager.GetInstance.OnLobbySceneLoaded -= SpawnPlayer;    
            }
        }

        private void SpawnPlayer(List<NetworkPlayer> players, NetworkManager networkManager)
        {
            foreach (var player in players)
            {
                var connection = player.connectionToClient;
                var playerInstance = Instantiate(_lobbyCharacterPrefab, networkManager.GetStartPosition().position, Quaternion.identity);
                NetworkServer.ReplacePlayerForConnection(connection, playerInstance);
            }
        }
    }
}

