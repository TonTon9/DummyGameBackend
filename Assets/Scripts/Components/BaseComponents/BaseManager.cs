using Components;

namespace Managers
{
    public abstract class BaseManager<T> : BaseMonoBehaviour where T : BaseManager<T>
    {
        public static T Instance;
        public bool IsInitialize { get; set; }

        protected override void Initialize()
        {
            Instance = GetComponent<T>();
        }

        protected override void UnInitialize()
        {
        }

        protected override void Subscribe()
        {
        }

        protected override void UnSubscribe()
        {
        }
    }
}
