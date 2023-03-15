using System;

namespace UI.Container
{
    public class CharacterInventoryContainer : BaseCharacterViewInstantiateContainer
    {
        public event Action OnHide = delegate {};

        protected override void Initialize()
        {
            InstantiateCards();
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

        protected override void AfterHide()
        {
            OnHide?.Invoke();
        }
    }
}
