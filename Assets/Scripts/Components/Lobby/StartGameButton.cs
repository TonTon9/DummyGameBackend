using Components.BaseComponent;
using Components.Network;

namespace Components.Lobby.CharacterSelect
{
    public class StartGameButton : BaseButtonComponent
    {
        protected override void Button_OnClick()
        {
            GameNetworkManager.GetInstance.StartGame();
        }
    }
}
