using System;
using HTTP.WebRequest;
using JsonModels;
using UnityEngine;

namespace UI.Container
{
    public class CharacterInventoryContainer : BaseCharacterViewInstantiateContainer
    {
        public event Action OnHide = delegate {};

        protected override void Initialize()
        {
            InstantiateCards();
            // foreach (var character in characters.Characters)
            // {
            //     Instantiate(_prefab, _parent);
            // }
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
