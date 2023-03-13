
using System;
using HTTP.WebRequest;
using JsonModels;
using UnityEngine;

namespace Views
{
    public class CharacterInventoryView : BaseMonoBehaviour
    {
        public event Action OnHide = delegate {};

        [SerializeField]
        private GameObject _prefab;

        [SerializeField]
        private Transform _parent;

        [SerializeField]
        private GameObject _content;
        
        protected override async void Initialize()
        {
            var charactersResponse = await WebRequestWrapper.GetCharacters();
            var json = "{\"Characters\":" + charactersResponse.content + "}";
            var characters = JsonUtility.FromJson<CharactersJsonModel>(json);
            
            foreach (var character in characters.Characters)
            {
                Instantiate(_prefab, _parent);
            }
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

        public void Show()
        {
            _content.SetActive(true);
        }

        public void Hide()
        {
            _content.SetActive(false);
            OnHide?.Invoke();
        }
    }
}