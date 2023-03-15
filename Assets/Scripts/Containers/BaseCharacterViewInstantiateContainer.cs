using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Managers;
using Models;
using UnityEngine;
using Views;

namespace UI.Container
{
    public abstract class BaseCharacterViewInstantiateContainer : BaseContainer
    {
        [SerializeField]
        private int _poolCount;

        [SerializeField]
        protected RectTransform _optinalContainer;

        [SerializeField]
        protected CharacterView _cardPrefab;

        private readonly List<CharacterView> _currentSpawnedCards = new();

        private readonly Queue<CharacterView> _cardsPool = new();

        protected override void Initialize()
        {
            CreateCardPool();
        }

        protected async void InstantiateCards()
        {
            await UniTask.WaitUntil(() => CharactersInventoryManager.Instance != null &&
                                          CharactersInventoryManager.Instance.IsPlayerCharactersFilled);
            DestroyNonexistentCards();
            InstantiateNewCards();
        }

        private void CreateCardPool()
        {
            for (int i = 0; i < _poolCount; i++)
            {
                AddElementInPool();
            }
        }

        private void AddElementInPool()
        {
            var card = Instantiate(_cardPrefab, _optinalContainer);
            card.gameObject.SetActive(false);
            _cardsPool.Enqueue(card);
        }

        private void DestroyNonexistentCards()
        {
            var currentSpawnedCardsNotInInventory = _currentSpawnedCards.Where(cv =>
                !CharactersInventoryManager.Instance.PlayerCharacters.Contains(cv.Model.Value));
            
            foreach (var characterView in currentSpawnedCardsNotInInventory)
            {
                characterView.gameObject.SetActive(false);
                _cardsPool.Enqueue(characterView);
            }
        }

        private async void InstantiateNewCards()
        {
            foreach (var characterModel in CharactersInventoryManager.Instance.PlayerCharacters)
            {
                bool isCardExist = false;
                foreach (var spawnedCard in _currentSpawnedCards)
                {
                    if (spawnedCard.Model.Value == characterModel)
                    {
                        isCardExist = true;
                        break;
                    }
                }
                if (!isCardExist)
                {
                    _currentSpawnedCards.Add(await AddCardAsync(characterModel));
                }
            }
        }
        
        private async UniTask<CharacterView> AddCardAsync(CharacterModel characterModel)
        {
            if (_cardsPool.Count <= 0)
            {
                AddElementInPool();
            }
            var card = _cardsPool.Dequeue();
            card.gameObject.SetActive(true);
            await UniTask.WaitUntil( () => card.IsInitialize && card.Model != null);
            
            card.Model.Value = characterModel;
            return card;
        }

        protected void ClearListOfCharacters()
        {
            foreach (var characterView in _currentSpawnedCards)
            {
                Destroy(characterView.gameObject);
            }
            _currentSpawnedCards.Clear();
        }
    }
}