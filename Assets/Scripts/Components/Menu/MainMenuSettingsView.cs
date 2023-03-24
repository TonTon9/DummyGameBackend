using Cysharp.Threading.Tasks;
using Doozy.Runtime.UIManager.Animators;
using UI;
using UnityEngine;

namespace Components.Menu
{
    public class MainMenuSettingsView : BaseContainer
    {
        [SerializeField]
        private UIContainerUIAnimator _UIAnimator;

        public void ShowSettings()
        {
            Show();
            _UIAnimator.Show();
        }

        public async void HideSettings()
        {
            _UIAnimator.Hide();
            await UniTask.WaitUntil(() => !_UIAnimator.anyAnimationIsActive);
            Hide();
        }
    }
}

