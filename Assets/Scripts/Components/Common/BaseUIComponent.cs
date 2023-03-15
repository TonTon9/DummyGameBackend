using UnityEngine;

namespace UI
{
    public abstract class BaseContainer : BaseMonoBehaviour, IUIWindow
    {
        [SerializeField]
        private GameObject _containerContent;
        
        public void Hide()
        {
            _containerContent.SetActive(false);
            AfterHide();
        }

        public void Show()
        {
            _containerContent.SetActive(true);
            AfterShow();
        }

        protected virtual void AfterShow()
        {
        }
        
        protected virtual void AfterHide()
        {
        }
    }

}
