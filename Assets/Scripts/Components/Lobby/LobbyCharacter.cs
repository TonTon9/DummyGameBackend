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
        private void CmdSetCharacterModel(string type)
        {
            ServerSetCharacterModel(type);
        }

        [Server]
        private void ServerSetCharacterModel(string type)
        {
            RpcSetCharacterModel(type);
        }

        [ClientRpc]
        private void RpcSetCharacterModel(string type)
        {
            SetModelClient(type);
        }

        [Client]
        private void SetModelClient(string type)
        {
            var model = _lobbyCharacterModels.FirstOrDefault(l => l.Type.ToString().Equals(type));

            currentCharacterName = type;

            Debug.Log(model);
            
            if (model == null)
            {
                return;
            }
            if (_currentModel != null)
            {
                _currentModel.SetActive(false);
            }
            _currentModel = model.Model;
            Debug.Log(_currentModel);   
            _currentModel.SetActive(true);
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
