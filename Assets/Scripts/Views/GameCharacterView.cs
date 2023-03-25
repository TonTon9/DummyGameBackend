using System;
using Components;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Mirror;
using Models.Game;
using TMPro;
using UnityEngine;
using NetworkPlayer = Components.Network.NetworkPlayer;

namespace Views.Game
{
    public class GameCharacterView : BaseNetworkMonoBehaviour
    {
        [SyncVar]
        public string userName;

        [SerializeField]
        private TextMeshProUGUI _characterNameText;

        [SerializeField]
        private GameObject _characterCanvas;
        
        [SyncVar]
        public GameCharacterModel characterModel;

        private GameObject _currentModel;

        public bool IsInitialize { get; private set; }
        
        public override void OnStartAuthority()
        {
            base.OnStartAuthority();
            _isAuthorityInit = true;
        }

        protected override async void Initialize()
        {
            base.Initialize();
            await UniTask.WaitUntil(() => _isAuthorityInit);
            
            if (hasAuthority)
            {
                _characterCanvas.SetActive(false);
                return;
            }
        }

        public void SetPlayerModel(NetworkPlayer networkPlayer)
        {
            SetPlayerName(networkPlayer.playerName);
        }

        public void SetPlayerName(string newName)
        {
            userName = newName;
            RpcSetUIName();
        }
        
        private void RpcSetUIName()
        {
            ClientUIName();
        }
        
        private void ClientUIName()
        {
            _characterNameText.text = userName;
            _characterNameText.DOFade(1, 0.5F);
        }
    }

    [Serializable]
    public class ModelTest
    {
        [SyncVar]
        public float health;

        [ClientRpc]
        public void RpcSetHealth(float heath)
        {
            Client(heath);
        }

        public void Client(float heath)
        {
            health = heath;
        }

        [Command]
        public void CmdSetHeath(float heath)
        {
            Server(heath);
        }

        private void Server(float heath)
        {
            health = heath;
        }
    }
}
