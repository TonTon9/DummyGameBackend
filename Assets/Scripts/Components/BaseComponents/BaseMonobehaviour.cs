using UnityEngine;

namespace Components
{
    public abstract class BaseMonoBehaviour : MonoBehaviour
    {
        protected virtual void Initialize()
        {
        }

        protected virtual void UnInitialize()
        {
        }

        protected virtual void Subscribe()
        {
        }

        protected virtual void UnSubscribe()
        {
        }
    
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
