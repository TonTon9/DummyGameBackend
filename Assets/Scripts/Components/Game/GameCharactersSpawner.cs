using System.Collections.Generic;
using System.Linq;
using Components.Network;
using Mirror;
using UnityEngine;
using Views.Game;

namespace Components.Lobby
{
    public class GameCharactersSpawner : BaseMonoBehaviour
    {
        [SerializeField]
        private List<SpawnerCharacterPosition> _spawnPositions;
        
        [SerializeField]
        private LobbyCharacterModel[] _characterGameobjets;

        public static GameCharactersSpawner GetInstance;

        protected override void Initialize()
        {
            base.Initialize();
            GetInstance = this;
        }

        public void SpawnCharacters()
        {
            var manager = GameNetworkManager.GetInstance;
            foreach (var player in manager.players)
            {
                var connection = player.connectionToClient;
                var spawnPos = GetFreePosition();
            
                var model = _characterGameobjets.FirstOrDefault(l => l.Type.ToString().Equals(player.characterName));
                if (model != null)
                {
                    var playerInstance = Instantiate(model.Model, spawnPos.Position.position, spawnPos.Position.rotation);
                    spawnPos.IsFree = false;
                    NetworkServer.Spawn(playerInstance, connection);
                    playerInstance.GetComponent<GameCharacterView>().SetPlayerModel(player);
                }
            }
        }
        
        private SpawnerCharacterPosition GetFreePosition()
        {
            SpawnerCharacterPosition position = null;
            foreach (var pos in _spawnPositions)
            {
                if (pos.IsFree)
                {
                    position = pos;
                }
            }
            return position;
        }
    }
}
