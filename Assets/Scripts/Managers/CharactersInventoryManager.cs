using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using HTTP.WebRequest;
using JsonModels;
using Models;
using UnityEngine;

namespace Managers
{
    public class CharactersInventoryManager : BaseManager<CharactersInventoryManager>
    {
        public event Action OnPlayerCharactersChanged = delegate {};
        public List<CharacterModel> PlayerCharacters { get; private set; }
        public bool IsPlayerCharactersFilled { get; private set; }

        protected override void Initialize()
        {
            base.Initialize();
            PlayerCharacters = new();
            IsInitialize = true;
        }

        protected override async void Subscribe()
        {
            base.Subscribe();
            await UniTask.WaitUntil(() => LoginManager.Instance != null);

            LoginManager.Instance.OnLoginSuccess += LoginManager_OnLoginSuccess;
        }

        protected override void UnSubscribe()
        {
            base.UnSubscribe();

            if (LoginManager.Instance != null)
            {
                LoginManager.Instance.OnLoginSuccess += LoginManager_OnLoginSuccess;
            }
        }

        private void LoginManager_OnLoginSuccess()
        {
            LoadPlayerCharactersFromServer();
        }

        private async void LoadPlayerCharactersFromServer()
        {
            var charactersResponse = await WebRequestWrapper.GetCharacters();
            var json = "{\"Characters\":" + charactersResponse.content + "}";
            var playerCharacters = JsonUtility.FromJson<CharactersJsonModel>(json);

            foreach (var playerCharacter in playerCharacters.Characters)
            {
                AddCharacterByJsonModel(playerCharacter);
            }
            IsPlayerCharactersFilled = true;
            OnPlayerCharactersChanged?.Invoke();
        }

        private void AddCharacterByJsonModel(CharacterJsonModel playerCharacter)
        {
            CharacterModel characterModel = new CharacterModel(playerCharacter.Name,playerCharacter.MaxHealth, 1, 1);
            PlayerCharacters.Add(characterModel);
        }
    }
}
