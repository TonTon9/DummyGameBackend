using System;
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

        [SerializeField]
        private List<LobbyCharactersPositions> _positions;

        protected override async void Subscribe()
        {
            base.Subscribe();
            await UniTask.WaitUntil(() => GameNetworkManager.GetInstance != null);
            GameNetworkManager.GetInstance.ServerAddPlayer += SpawnPlayer;
        }

        protected override void UnSubscribe()
        {
            base.UnSubscribe();
            if (GameNetworkManager.GetInstance != null)
            {
                GameNetworkManager.GetInstance.ServerAddPlayer -= SpawnPlayer;    
            }
        }

        private void SpawnPlayer(NetworkPlayer player, NetworkManager networkManager)
        {
            var connection = player.connectionToClient;
            //var spawnPosition = networkManager.GetStartPosition();
            var spawnPos = GetFreePosition();
            
            var playerInstance = Instantiate(_lobbyCharacterPrefab, spawnPos.Position.position, spawnPos.Position.rotation);
            spawnPos.IsFree = false;
            NetworkServer.Spawn(playerInstance, connection);
            playerInstance.GetComponent<LobbyCharacter>().SetCharacterName(player.playerName);
        }

        [Server]
        private LobbyCharactersPositions GetFreePosition()
        {
            LobbyCharactersPositions position = null;
            foreach (var pos in _positions)
            {
                if (pos.IsFree)
                {
                    position = pos;
                }
            }
            return position;
        }
    }

    [Serializable]
    public class LobbyCharactersPositions
    {
        public Transform Position;
        public bool IsFree = true;
    }
}

