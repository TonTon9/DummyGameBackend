using Models;
using UnityEngine;
using UnityEngine.UI;

namespace Components.BaseComponent
{
    [RequireComponent(typeof(Image))]
    public class BaseImageComponent<T> : BaseCharacterComponentBaseOnView<T> where T : IModel
    {
        private Image _image;
        protected override void Initialize()
        {
            _image = GetComponent<Image>();
        }

        protected override void UnInitialize()
        {
        }

        protected override void Subscribe()
        {
        }

        protected override void UnSubscribe()
        {
        }

        protected void Redraw(Sprite sprite)
        {
            if (_image != null && sprite != null)
            {
                _image.sprite = sprite;
            }
        }
    }

}