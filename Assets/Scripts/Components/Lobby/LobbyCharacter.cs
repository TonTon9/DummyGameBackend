using System;
using System.Linq;
using Components.Lobby.CharacterSelect;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Enums;
using Mirror;
using Models;
using TMPro;
using UnityEngine;
using NetworkPlayer = Components.Network.NetworkPlayer;

namespace Components.Lobby
{
    public class LobbyCharacter : BaseNetworkMonoBehaviour
    {
        [SyncVar]
        public string userName;

        [SyncVar]
        public string currentCharacterName;

        [SerializeField]
        private TextMeshProUGUI _characterNameText;

        [SerializeField]
        private LobbyCharacterModel[] _lobbyCharacterModels;

        private GameObject _currentModel;

        protected override async void Initialize()
        {
            base.Initialize();
            await UniTask.WaitUntil(() => userName != "");

            SetUIName();
            
            SetModelClient(currentCharacterName);   
        }

        protected override void Subscribe()
        {
            base.Subscribe();
            if (hasAuthority)
            {
                LobbyCharacterSelectView.GetInstance.OnCharacterSelected += LobbyCharacterSelectView_OnCharacterSelected;
            }
        }

        protected override void UnSubscribe()
        {
            base.UnSubscribe();
            if (hasAuthority)
            {
                LobbyCharacterSelectView.GetInstance.OnCharacterSelected += LobbyCharacterSelectView_OnCharacterSelected;
            }
        }
        
        private void LobbyCharacterSelectView_OnCharacterSelected(CharacterModel characterModel)
        {
            if (isServer)
            {
                RpcSetCharacterModel(characterModel.CharacterName.CurrentValue.Value);
            } else
            {
                CmdSetCharacterModel(characterModel.CharacterName.CurrentValue.Value);
            }
        }

        [Command]
        private void CmdSetCharacterModel(string characterName)
        {
            ServerSetCharacterModel(characterName);
        }

        [Server]
        private void ServerSetCharacterModel(string characterName)
        {
            RpcSetCharacterModel(characterName);
        }

        [ClientRpc]
        private void RpcSetCharacterModel(string characterName)
        {
            SetModelClient(characterName);
        }

        [Client]
        private void SetModelClient(string characterName)
        {
            var model = _lobbyCharacterModels.FirstOrDefault(l => l.Type.ToString().Equals(characterName));

            currentCharacterName = characterName;
            if (model == null)
            {
                return;
            }
            if (_currentModel != null)
            {
                _currentModel.SetActive(false);
            }
            _currentModel = model.Model;
            _currentModel.SetActive(true);
            
            if (hasAuthority)
            {
                var player = NetworkClient.connection.identity.GetComponent<NetworkPlayer>();
                player.characterName = currentCharacterName;                
            }
        }

        public void SetCharacterName(string newName)
        {
            userName = newName;
        }

        private void SetUIName()
        {
            _characterNameText.text = userName;
            _characterNameText.DOFade(1, 0.5F);
        }
    }

    [Serializable]
    public class LobbyCharacterModel
    {
        public CharacterType Type;
        public GameObject Model;
    }
}
