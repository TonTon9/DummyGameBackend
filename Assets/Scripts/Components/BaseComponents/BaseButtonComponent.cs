using UnityEngine;
using UnityEngine.UI;

namespace Components.BaseComponent
{
    [RequireComponent(typeof(Button))]
    public abstract class BaseButtonComponent : BaseMonoBehaviour
    {
        private Button _button;
        
        protected override void Initialize()
        {
            _button = GetComponent<Button>();
        }

        protected override void UnInitialize()
        {
        }

        protected override void Subscribe()
        {
            if (_button != null)
            {
                _button.onClick.AddListener(Button_OnClick);
            }
        }

        protected override void UnSubscribe()
        {
            if (_button != null)
            {
                _button.onClick.RemoveListener(Button_OnClick);
            }
        }

        protected abstract void Button_OnClick();
    }
}
