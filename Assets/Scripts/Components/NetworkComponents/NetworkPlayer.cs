using Enums;
using Mirror;
using UnityEngine;

namespace Components.Network
{
    public class NetworkPlayer : BaseNetworkMonoBehaviour
    {
        [SyncVar]
        public string playerName;

        [SyncVar]
        public string characterName;
        
        public override void OnStartClient()
        {
            DontDestroyOnLoad(gameObject);
            GameNetworkManager.GetInstance.players.Add(this);
            base.OnStartClient();
        }

        public override void OnStopClient() {
            base.OnStopClient();
            GameNetworkManager.GetInstance.players.Remove(this);
        }
    }
}
