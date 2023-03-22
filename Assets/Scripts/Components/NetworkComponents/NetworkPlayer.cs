
namespace Components.Network
{
    public class NetworkPlayer : BaseNetworkMonoBehaviour
    {
        public override void OnStartClient()
        {
            DontDestroyOnLoad(gameObject);
            base.OnStartClient();
        }
    }
}
