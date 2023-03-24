using Components.BaseComponent;
using Components.Network;

namespace Components.Menu
{
    public class HostGameButtonComponent : BaseButtonComponent
    {
        protected override void Button_OnClick()
        {
            GameNetworkManager.GetInstance.HostLobby();
        }
    }

}
