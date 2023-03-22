using Models;
using TMPro;
using UnityEngine;

namespace Components.BaseComponent
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public abstract class BaseTextComponent<T> : BaseCharacterComponentBaseOnView<T> where T : IModel
    {
        private TextMeshProUGUI _text;
        
        [SerializeField]
        private string _prefix = "";
        [SerializeField]
        
        private string _suffix = "";
        
        protected override void Initialize()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }

        protected override void UnInitialize(){}
        protected override void Subscribe(){}
        protected override void UnSubscribe(){}

        protected virtual void Redraw(string value)
        {
            if (_text == null) return;
            
            string newText = value;
            
            if (!string.IsNullOrEmpty(_prefix))
            {
                newText = _prefix + newText;
            }
            if (!string.IsNullOrEmpty(_suffix))
            {
                newText = newText + _suffix;
            }
            
            _text.text = newText;
        }
    }
}
