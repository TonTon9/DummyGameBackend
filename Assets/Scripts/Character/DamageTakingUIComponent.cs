using System;
using System.Collections.Generic;
using Components;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Models.Game;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Character.Game.Hud
{
    public class DamageTakingUIComponent : BaseMonoBehaviour
    {
        [SerializeField]
        private GameCharacterModel _model;
        
        [SerializeField]
        private Image _bloodImage;

        [SerializeField]
        private Image _scarImagePrefab;

        [SerializeField]
        private int _poolSize;

        [SerializeField]
        private Vector2 _scarsXAreaToSpawn;
        
        [SerializeField]
        private Vector2 _scarsYAreaToSpawn;

        private Queue<Image> _scarImages = new();

        private Sequence _takeDamageSequence;
        private Sequence _scarSequence;

        protected override void Initialize()
        {
            base.Initialize();
            _takeDamageSequence = DOTween.Sequence();
            _scarSequence = DOTween.Sequence();
            CreatePoolFromScars();
        }

        protected override void Subscribe()
        {
            base.Subscribe();
            if (_model != null)
            {
                _model.OnHealthChanged += TakeDamageAnimation;
            }
        }

        protected override void UnSubscribe()
        {
            if (_model != null)
            {
                _model.OnHealthChanged -= TakeDamageAnimation;
            }
        }

        private void CreatePoolFromScars()
        {
            for (int i = 0; i < _poolSize; i++)
            {
                var image = Instantiate(_scarImagePrefab, transform);
                image.gameObject.SetActive(false);
                _scarImages.Enqueue(image);
                
            }
        }

        private void TakeDamageAnimation(float health, HealthChangeType healthChangeType)
        {
            if (healthChangeType == HealthChangeType.Damage)
            {
                _takeDamageSequence.Append(_bloodImage.DOFade(1, 0.25f));
                _takeDamageSequence.Insert(0.5f,_bloodImage.DOFade(0, 3f));
                var healthRatio = _model.MaxHealth / health;
                ShowScarsBasedOnCharacterHealthRatio(healthRatio);
            }
        }

        private void ShowScarsBasedOnCharacterHealthRatio(float healthRatio)
        {
            if (healthRatio < 1.5)
            {
                ScarDamageAnimation(1);    
            } else if(healthRatio >= 1.5f && healthRatio < 3)
            {
                ScarDamageAnimation(2);
            } else
            {
                ScarDamageAnimation(3);
            }
        }

        private async void ScarDamageAnimation(float scarCount)
        {
            for (int i = 0; i < scarCount; i++)
            {
                var scarImage = _scarImages.Dequeue();
                scarImage.gameObject.SetActive(true);
            
                scarImage.transform.localPosition = new Vector2(
                    Random.Range(_scarsXAreaToSpawn.x, _scarsXAreaToSpawn.y),
                    Random.Range(_scarsYAreaToSpawn.x, _scarsYAreaToSpawn.y));
                
                scarImage.transform.Rotate(0, 0, Random.Range(0, 360));
                scarImage.transform.localScale = Vector3.one * Random.Range(1f, 1.8f); 
            
                _scarSequence.Append(scarImage.DOFade(1, 0.25f));
                _scarSequence.Insert(0.5f,scarImage.DOFade(0, 3f));
                await UniTask.Delay(TimeSpan.FromSeconds(4f));
                _scarSequence.Kill();
                AddScarImageToPool(scarImage);
            }
        }

        private void AddScarImageToPool(Image scarImage)
        {
            scarImage.gameObject.SetActive(false);
            _scarImages.Enqueue(scarImage);
        }
    }
}
