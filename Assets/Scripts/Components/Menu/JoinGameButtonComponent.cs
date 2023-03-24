using Components.BaseComponent;
using Components.Network;

namespace Components.Menu
{
    public class JoinGameButtonComponent : BaseButtonComponent
    {
        protected override void Button_OnClick()
        {
            GameNetworkManager.GetInstance.JoinLobby("localhost");
        }
    }
}
