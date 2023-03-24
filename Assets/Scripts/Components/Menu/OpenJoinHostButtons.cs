using Components.BaseComponent;
using Doozy.Runtime.UIManager.Animators;
using UnityEngine;

namespace Components.Menu
{
    public class OpenJoinHostButtons : BaseButtonComponent
    {
        [SerializeField]
        private UIContainerUIAnimator _mainButtonsAnimator;

        protected override void Button_OnClick()
        {
            _mainButtonsAnimator.Show();
        }
    }
}
