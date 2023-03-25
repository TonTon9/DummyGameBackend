using Mirror;

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

        public void SetCharacterName(string newName)
        {
            CmdTest(newName);
            //RpcTest(newName);
        }

        [Command]
        private void CmdTest(string newName)
        {
            ServerSetCharacterName(newName);
        }
        
        [Server]
        private void ServerSetCharacterName(string newName)
        {
            characterName = newName;
        }
        
        // [ClientRpc]
        // private void RpcTest(string newName)
        // {
        //     ClientSetCharacterName(newName);
        // }
        //
        // [Client]
        // private void ClientSetCharacterName(string newName)
        // {
        //     characterName = newName;
        // }
    }
}
