using Mirror;

namespace Components
{
    public abstract class BaseNetworkMonoBehaviour : NetworkBehaviour
    {
        protected abstract void Initialize();
        protected abstract void UnInitialize();
    
        protected abstract void Subscribe();
        protected abstract void UnSubscribe();
    
        private void Awake()
        {
            Initialize();
        }

        private void Start()
        {
            Subscribe();
        }

        private void OnDestroy()
        {
            UnInitialize();
            UnSubscribe();
        }
    }
}