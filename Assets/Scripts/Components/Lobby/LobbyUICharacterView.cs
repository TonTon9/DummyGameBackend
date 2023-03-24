using Doozy.Runtime.UIManager.Animators;
using Models;
using UnityEngine;
using Views;

namespace Components.Lobby.CharacterSelect
{
    public class LobbyUICharacterView : CharacterView
    {
        [SerializeField]
        private UIContainerUIAnimator _animator;
        
        [SerializeField]
        private UIContainerUIAnimator _backAnimator;

        [SerializeField]
        private CanvasGroup _canvasGroup;

        public void Show()
        {
            _animator.Show();
        }
        
        public void Hide()
        {
            _animator.Hide();
        }
        
        public void BackShow()
        {
            _backAnimator.Show();
        }
        
        public void BackHide()
        {
            _backAnimator.Hide();
        }
        
        public void SetAlpha(float value)
        {
            _canvasGroup.alpha = value;
        }
    }
}
