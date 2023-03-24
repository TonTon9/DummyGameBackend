using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Models;
using UnityEngine;
using UnityEngine.UI;

namespace Components.Lobby.CharacterSelect
{
    public class LobbyCharacterSelectView : BaseMonoBehaviour
    {
        public event Action<CharacterModel> OnCharacterSelected = delegate { };
        
        [SerializeField]
        private LobbyUICharacterView _characterViewPrefab;

        [SerializeField]
        private Button _previousCharacterButton;
        
        [SerializeField]
        private Button _nextCharacterButton;

        [SerializeField]
        private Transform _parrent;

        [SerializeField]
        private double _delayBtfHideAndShowAnimation;

        private List<LobbyUICharacterView> _characters = new();

        private int _currentCharacterIndex = 0;

        private List<CharacterModel> _models = new()
        {
            new CharacterModel("BoldMan", 100, 100, 5),
            new CharacterModel("Alcoholic", 100, 100, 5),
            new CharacterModel("Racer", 100, 100, 5),
            new CharacterModel("Swat", 100, 100, 5),
            new CharacterModel("WomanScientist", 100, 100, 5),
        };

        public static LobbyCharacterSelectView GetInstance { get; private set; }

        protected override void Initialize()
        {
            base.Initialize();
            if (GetInstance == null)
            {
                GetInstance = this;
            }
            
            foreach (var characterModel in _models)
            {
                var character = Instantiate(_characterViewPrefab, _parrent);
                character.Model.Value = characterModel;
                _characters.Add(character);
                character.SetAlpha(0);
            }

            if (_characters.Count == 0)
            {
                OffButtons();
            } else
            {
                _characters[0].SetAlpha(1);
                SetButtonsInteractable();
            }
        }

        protected override void Subscribe()
        {
            _nextCharacterButton.onClick.AddListener(NextCharacter);
            _previousCharacterButton.onClick.AddListener(PreviousCharacter);
        }

        private async void NextCharacter()
        {
            if (_characters.Count == 0 || _characters.Count-1 == _currentCharacterIndex)
            {
                return;
            }

            _characters[_currentCharacterIndex].Hide();
            _currentCharacterIndex++;
            await UniTask.Delay(TimeSpan.FromSeconds(_delayBtfHideAndShowAnimation));

            var newCharacter = _characters[_currentCharacterIndex];
            newCharacter.Show();
            OnCharacterSelected?.Invoke(newCharacter.Model.Value);
            
            SetButtonsInteractable();
        }
        
        private async void PreviousCharacter()
        {
            if (_characters.Count == 0 || _currentCharacterIndex == 0)
            {
                return;
            }

            _characters[_currentCharacterIndex].BackHide();
            _currentCharacterIndex--;
            await UniTask.Delay(TimeSpan.FromSeconds(_delayBtfHideAndShowAnimation));
            
            var newCharacter = _characters[_currentCharacterIndex];
            newCharacter.BackShow();
            OnCharacterSelected?.Invoke(newCharacter.Model.Value);
 
            SetButtonsInteractable();
        }

        private void SetButtonsInteractable()
        {
            var currentIndex = _characters.Count - 1;

            if (_currentCharacterIndex == 0)
            {
                _previousCharacterButton.interactable = false;
            } 
            else
            {
                _previousCharacterButton.interactable = true;
            }
            
            if (_currentCharacterIndex == currentIndex || _characters.Count-1 == _currentCharacterIndex)
            {
                _nextCharacterButton.interactable = false;
            }
            else
            {
                _nextCharacterButton.interactable = true;
            }
        }

        private void OffButtons()
        {
            _previousCharacterButton.interactable = false;
            _nextCharacterButton.interactable = false;
        }
    }

}
