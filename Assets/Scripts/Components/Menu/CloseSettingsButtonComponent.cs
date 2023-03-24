using System;
using Components.BaseComponent;
using Cysharp.Threading.Tasks;
using Doozy.Runtime.UIManager.Animators;
using UnityEngine;

namespace Components.Menu
{
    public class CloseSettingsButtonComponent : BaseButtonComponent
    {
        [SerializeField]
        private UIContainerUIAnimator _mainButtonsAnimator;
    
        [SerializeField]
        private MainMenuSettingsView _settings;

        [SerializeField]
        private double _delayInSeconds;

        protected override async void Button_OnClick()
        {
            _settings.HideSettings();
        
            await UniTask.Delay(TimeSpan.FromSeconds(_delayInSeconds));

            _mainButtonsAnimator.Show();
        }
    }

}
